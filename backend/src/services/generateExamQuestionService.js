const MultipleChoiceQuestion = require("../models/multipleChoiceQuestionsModel");
const EssayQuestion = require("../models/essayQuestionsModel");
const {
	getPrompt,
	determinePromptType,
	parseAIResponse,
	PROMPTS,
} = require("../configs/generatePromptConfig");
const OpenAI = require("openai");
const {GoogleGenerativeAI} = require("@google/generative-ai");
const pdfParse = require("pdf-parse");
const mammoth = require("mammoth");

class GenerateExamQuestionService {
	async generateWithGemini(documentContent, numMultipleChoice, numEssay) {
		try {
			const promptType = determinePromptType(numMultipleChoice, numEssay);

			const userPrompt = getPrompt(
				promptType,
				documentContent,
				numMultipleChoice,
				numEssay
			);

			const genAI = new GoogleGenerativeAI(process.env.GEMINI_API);
			const model = genAI.getGenerativeModel({model: "gemini-2.5-flash"});

			const fullPrompt = `${PROMPTS.SYSTEM_PROMPT}\n\n${userPrompt}`;

			console.log(
				`[Gemini] Requesting ${numMultipleChoice} MC and ${numEssay} Essay questions`
			);

			const result = await model.generateContent(fullPrompt);
			const response = await result.response;
			const aiText = response.text();

			console.log("[Gemini] Response received, parsing...");
			const questions = parseAIResponse(aiText);

			const actualMC = questions.multipleChoice?.length || 0;
			const actualEssay = questions.essay?.length || 0;

			console.log(
				`[Gemini] Generated ${actualMC} MC (expected ${numMultipleChoice}) and ${actualEssay} Essay (expected ${numEssay})`
			);

			if (actualMC !== numMultipleChoice || actualEssay !== numEssay) {
				console.warn(
					`[Gemini] Warning: AI generated wrong number of questions!`
				);
				console.warn(
					`[Gemini] Expected: MC=${numMultipleChoice}, Essay=${numEssay}`
				);
				console.warn(
					`[Gemini] Got: MC=${actualMC}, Essay=${actualEssay}`
				);
			}

			return questions;
		} catch (error) {
			console.error("Gemini API Error:", error);
			throw new Error(`Lỗi khi tạo câu hỏi với Gemini: ${error.message}`);
		}
	}

	async generateWithDeepSeek(documentContent, numMultipleChoice, numEssay) {
		try {
			// Limit document content length for DeepSeek to avoid issues
			const maxContentLength = 30000; // DeepSeek might have stricter limits
			let processedContent = documentContent;

			if (documentContent.length > maxContentLength) {
				console.log(
					`[DeepSeek] Document too long (${documentContent.length}), truncating to ${maxContentLength}`
				);
				processedContent =
					documentContent.substring(0, maxContentLength) +
					"\n\n[Nội dung đã được cắt ngắn]";
			}

			const promptType = determinePromptType(numMultipleChoice, numEssay);

			const userPrompt = getPrompt(
				promptType,
				processedContent,
				numMultipleChoice,
				numEssay
			);

			console.log(
				`[DeepSeek] Requesting ${numMultipleChoice} MC and ${numEssay} Essay questions`
			);
			console.log(
				`[DeepSeek] Content length: ${processedContent.length} chars`
			);

			if (!process.env.DEEPSEEK_API) {
				throw new Error("DEEPSEEK_API key not configured");
			}

			const openai = new OpenAI({
				baseURL: "https://api.deepseek.com",
				apiKey: process.env.DEEPSEEK_API,
				timeout: 120000, // 2 minutes timeout
				maxRetries: 2,
			});

			console.log("[DeepSeek] Sending request to API...");

			const completion = await openai.chat.completions.create({
				messages: [
					{
						role: "system",
						content: PROMPTS.SYSTEM_PROMPT,
					},
					{
						role: "user",
						content: userPrompt,
					},
				],
				model: "deepseek-chat",
				temperature: 0.7,
				max_tokens: 8192,
				stream: false,
			});

			console.log("[DeepSeek] Response received, parsing...");

			if (!completion.choices || completion.choices.length === 0) {
				throw new Error("No response from DeepSeek API");
			}

			const aiText = completion.choices[0].message.content;

			if (!aiText) {
				throw new Error("Empty response from DeepSeek API");
			}

			const questions = parseAIResponse(aiText);

			const actualMC = questions.multipleChoice?.length || 0;
			const actualEssay = questions.essay?.length || 0;

			console.log(
				`[DeepSeek] Generated ${actualMC} MC (expected ${numMultipleChoice}) and ${actualEssay} Essay (expected ${numEssay})`
			);

			if (actualMC !== numMultipleChoice || actualEssay !== numEssay) {
				console.warn(
					`[DeepSeek] Warning: AI generated wrong number of questions!`
				);
				console.warn(
					`[DeepSeek] Expected: MC=${numMultipleChoice}, Essay=${numEssay}`
				);
				console.warn(
					`[DeepSeek] Got: MC=${actualMC}, Essay=${actualEssay}`
				);
			}

			return questions;
		} catch (error) {
			console.error(
				"DeepSeek API Error:",
				error.response?.data || error.message
			);
			console.error("DeepSeek Error details:", {
				name: error.name,
				message: error.message,
				code: error.code,
				status: error.status,
			});

			// Provide more specific error messages
			let errorMessage = error.message;
			if (error.message === "terminated" || error.code === "ECONNRESET") {
				errorMessage =
					"Kết nối đến DeepSeek bị gián đoạn. Vui lòng thử lại hoặc sử dụng Gemini.";
			} else if (error.status === 401) {
				errorMessage =
					"DeepSeek API key không hợp lệ. Vui lòng kiểm tra cấu hình.";
			} else if (error.status === 429) {
				errorMessage =
					"DeepSeek API đã đạt giới hạn. Vui lòng thử lại sau hoặc sử dụng Gemini.";
			} else if (error.code === "ETIMEDOUT") {
				errorMessage =
					"DeepSeek API quá thời gian chờ. Vui lòng thử lại hoặc sử dụng Gemini.";
			}

			throw new Error(
				`Lỗi khi tạo câu hỏi với DeepSeek: ${errorMessage}`
			);
		}
	}

	async generateQuestions(
		documentContent,
		aiModel,
		numMultipleChoice,
		numEssay,
		userId,
		contestId
	) {
		try {
			let generatedQuestions;

			if (aiModel === "gemini") {
				generatedQuestions = await this.generateWithGemini(
					documentContent,
					numMultipleChoice,
					numEssay
				);
			} else if (aiModel === "deepseek") {
				generatedQuestions = await this.generateWithDeepSeek(
					documentContent,
					numMultipleChoice,
					numEssay
				);
			} else {
				throw new Error("AI model không hợp lệ");
			}

			// KHÔNG lưu vào database, chỉ trả về JSON để frontend preview
			// Chỉ lưu khi user chọn câu hỏi và gọi addQuestionsToContest
			console.log(
				"[generateQuestions] Returning questions without saving to DB"
			);
			console.log(
				`[generateQuestions] MC: ${generatedQuestions.multipleChoice.length}, Essay: ${generatedQuestions.essay.length}`
			);

			return {
				multipleChoice: generatedQuestions.multipleChoice.map(
					(mcq) => ({
						...mcq,
						createdBy: userId,
						contest: contestId,
					})
				),
				essay: generatedQuestions.essay.map((eq) => ({
					...eq,
					createdBy: userId,
					contest: contestId,
				})),
			};
		} catch (error) {
			throw new Error(`Lỗi khi tạo câu hỏi: ${error.message}`);
		}
	}

	async parseDocument(fileBuffer, fileType) {
		try {
			let extractedText = "";

			if (fileType === "application/pdf") {
				console.log("[parseDocument] Parsing PDF file...");
				const pdfData = await pdfParse(fileBuffer);
				extractedText = pdfData.text;
				console.log(
					`[parseDocument] Extracted ${extractedText.length} characters from PDF`
				);
			} else if (
				fileType ===
				"application/vnd.openxmlformats-officedocument.wordprocessingml.document"
			) {
				console.log("[parseDocument] Parsing DOCX file...");
				const result = await mammoth.extractRawText({
					buffer: fileBuffer,
				});
				extractedText = result.value;
				console.log(
					`[parseDocument] Extracted ${extractedText.length} characters from DOCX`
				);
			} else if (fileType === "text/plain") {
				console.log("[parseDocument] Parsing TXT file...");
				extractedText = fileBuffer.toString("utf-8");
				console.log(
					`[parseDocument] Extracted ${extractedText.length} characters from TXT`
				);
			} else {
				throw new Error(`Loại file không được hỗ trợ: ${fileType}`);
			}

			if (!extractedText || extractedText.trim().length === 0) {
				throw new Error("Không thể trích xuất nội dung từ tài liệu");
			}

			const maxLength = 100000;
			if (extractedText.length > maxLength) {
				console.log(
					`[parseDocument] Text too long (${extractedText.length}), truncating to ${maxLength}`
				);
				extractedText =
					extractedText.substring(0, maxLength) +
					"\n\n[Nội dung đã được cắt ngắn do quá dài]";
			}

			return extractedText;
		} catch (error) {
			console.error("[parseDocument] Error:", error);
			throw new Error(`Lỗi khi parse tài liệu: ${error.message}`);
		}
	}
}

module.exports = new GenerateExamQuestionService();
