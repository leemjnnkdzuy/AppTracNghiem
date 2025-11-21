const MultipleChoiceQuestion = require("../models/multipleChoiceQuestionsModel");
const EssayQuestion = require("../models/essayQuestionsModel");

/**
 * Service để tạo câu hỏi thi sử dụng AI
 */
class GenerateExamQuestionService {
	/**
	 * Gọi Gemini API để tạo câu hỏi
	 */
	async generateWithGemini(documentContent, numMultipleChoice, numEssay) {
		try {
			// TODO: Implement Gemini API call
			// Đây là mock data để test
			const questions = {
				multipleChoice:
					this.generateMockMultipleChoice(numMultipleChoice),
				essay: this.generateMockEssay(numEssay),
			};

			return questions;
		} catch (error) {
			throw new Error(`Lỗi khi tạo câu hỏi với Gemini: ${error.message}`);
		}
	}

	/**
	 * Gọi DeepSeek API để tạo câu hỏi
	 */
	async generateWithDeepSeek(documentContent, numMultipleChoice, numEssay) {
		try {
			// TODO: Implement DeepSeek API call
			// Đây là mock data để test
			const questions = {
				multipleChoice:
					this.generateMockMultipleChoice(numMultipleChoice),
				essay: this.generateMockEssay(numEssay),
			};

			return questions;
		} catch (error) {
			throw new Error(
				`Lỗi khi tạo câu hỏi với DeepSeek: ${error.message}`
			);
		}
	}

	/**
	 * Tạo câu hỏi trắc nghiệm từ tài liệu
	 */
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

			// Chọn AI model
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

			// Lưu câu hỏi vào database
			const savedQuestions = {
				multipleChoice: [],
				essay: [],
			};

			// Lưu câu hỏi trắc nghiệm
			for (const mcq of generatedQuestions.multipleChoice) {
				const question = new MultipleChoiceQuestion({
					...mcq,
					createdBy: userId,
					contest: contestId,
				});
				const saved = await question.save();
				savedQuestions.multipleChoice.push(saved);
			}

			// Lưu câu hỏi tự luận
			for (const eq of generatedQuestions.essay) {
				const question = new EssayQuestion({
					...eq,
					createdBy: userId,
					contest: contestId,
				});
				const saved = await question.save();
				savedQuestions.essay.push(saved);
			}

			return savedQuestions;
		} catch (error) {
			throw new Error(`Lỗi khi tạo câu hỏi: ${error.message}`);
		}
	}

	/**
	 * Mock data cho câu hỏi trắc nghiệm (để test)
	 */
	generateMockMultipleChoice(count) {
		const questions = [];
		for (let i = 1; i <= count; i++) {
			questions.push({
				question: `Câu hỏi trắc nghiệm số ${i} được tạo từ tài liệu?`,
				options: [
					{text: "Đáp án A", isCorrect: true},
					{text: "Đáp án B", isCorrect: false},
					{text: "Đáp án C", isCorrect: false},
					{text: "Đáp án D", isCorrect: false},
				],
				explanation: `Giải thích cho câu hỏi ${i}`,
				difficulty: "medium",
			});
		}
		return questions;
	}

	/**
	 * Mock data cho câu hỏi tự luận (để test)
	 */
	generateMockEssay(count) {
		const questions = [];
		for (let i = 1; i <= count; i++) {
			questions.push({
				question: `Câu hỏi tự luận số ${i}: Hãy phân tích và trình bày quan điểm của bạn về vấn đề được đề cập trong tài liệu?`,
				sampleAnswer: `Câu trả lời mẫu cho câu hỏi ${i}`,
				gradingCriteria:
					"Đánh giá dựa trên tính logic, sâu sắc và liên quan đến nội dung",
				maxLength: 1000,
				minLength: 100,
				difficulty: "medium",
			});
		}
		return questions;
	}

	/**
	 * Parse nội dung file (PDF, DOCX, TXT)
	 */
	async parseDocument(fileBuffer, fileType) {
		try {
			// TODO: Implement document parsing
			// Có thể sử dụng thư viện như pdf-parse, mammoth (docx), etc.

			// Mock: Return dummy content
			return "Nội dung tài liệu được parse thành công...";
		} catch (error) {
			throw new Error(`Lỗi khi parse tài liệu: ${error.message}`);
		}
	}
}

module.exports = new GenerateExamQuestionService();
