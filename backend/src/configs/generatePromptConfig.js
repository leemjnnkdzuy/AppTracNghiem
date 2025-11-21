const PROMPTS = {
	MULTIPLE_CHOICE_ONLY: (documentContent, numQuestions) => `
Bạn là một chuyên gia ra đề thi. Hãy đọc kỹ tài liệu sau và tạo ${numQuestions} câu hỏi trắc nghiệm chất lượng cao.

TÀI LIỆU:
${documentContent}

⚠️ YÊU CẦU QUAN TRỌNG:
1. Tạo CHÍNH XÁC ${numQuestions} câu hỏi trắc nghiệm - KHÔNG NHIỀU HƠN, KHÔNG ÍT HƠN
2. Đếm kỹ để đảm bảo có đúng ${numQuestions} câu trong mảng "multipleChoice"
3. Mỗi câu hỏi phải có 4 đáp án (A, B, C, D)
4. Chỉ có DUY NHẤT 1 đáp án đúng
5. Câu hỏi phải liên quan trực tiếp đến nội dung tài liệu
6. Độ khó đa dạng: easy, medium, hard
7. Câu hỏi phải rõ ràng, không mơ hồ
8. Đáp án sai phải hợp lý, không quá dễ loại trừ
9. MỖI CÂU HỎI PHẢI DUY NHẤT - KHÔNG ĐƯỢC TRÙNG LẶP NỘI DUNG VỚI NHAU

ĐỊNH DẠNG JSON TRẢ VỀ (BẮT BUỘC):
{
  "multipleChoice": [
    // PHẢI CÓ CHÍNH XÁC ${numQuestions} ITEMS Ở ĐÂY
    {
      "question": "Nội dung câu hỏi?",
      "options": [
        {
          "text": "Đáp án A",
          "isCorrect": true
        },
        {
          "text": "Đáp án B",
          "isCorrect": false
        },
        {
          "text": "Đáp án C",
          "isCorrect": false
        },
        {
          "text": "Đáp án D",
          "isCorrect": false
        }
      ],
      "difficulty": "medium"
    }
  ],
  "essay": []
}

⚠️ CHÚ Ý QUAN TRỌNG:
- Mảng "multipleChoice" PHẢI CÓ CHÍNH XÁC ${numQuestions} câu hỏi
- Trả về ĐÚNG JSON format, không thêm markdown hay text khác
- Mảng "essay" PHẢI là mảng rỗng []
- Chỉ 1 option có isCorrect: true
- difficulty phải là: "easy", "medium", hoặc "hard"
- KIỂM TRA LẠI: Đếm số câu hỏi trước khi trả về để đảm bảo = ${numQuestions}
- KIỂM TRA TRÙNG LẶP: Đảm bảo không có 2 câu hỏi nào giống nhau về nội dung
`,
	ESSAY_ONLY: (documentContent, numQuestions) => `
Bạn là một chuyên gia ra đề thi. Hãy đọc kỹ tài liệu sau và tạo ${numQuestions} câu hỏi tự luận chất lượng cao.

TÀI LIỆU:
${documentContent}

YÊU CẦU:
1. Tạo CHÍNH XÁC ${numQuestions} câu hỏi tự luận
2. Câu hỏi phải yêu cầu phân tích, giải thích, so sánh hoặc đánh giá
3. Phải có câu trả lời mẫu chi tiết
4. Câu hỏi phải liên quan trực tiếp đến nội dung tài liệu
5. Độ khó đa dạng: easy, medium, hard
6. Câu hỏi phải mở, khuyến khích tư duy phản biện
7. MỖI CÂU HỎI PHẢI DUY NHẤT - KHÔNG ĐƯỢC TRÙNG LẶP NỘI DUNG VỚI NHAU

ĐỊNH DẠNG JSON TRẢ VỀ (BẮT BUỘC):
{
  "multipleChoice": [],
  "essay": [
    {
      "question": "Câu hỏi tự luận yêu cầu phân tích, giải thích?",
      "sampleAnswer": "Câu trả lời mẫu chi tiết, đầy đủ cho câu hỏi",
      "difficulty": "medium"
    }
  ]
}

CHÚ Ý:
- Trả về ĐÚNG JSON format, không thêm markdown hay text khác
- Mảng "multipleChoice" PHẢI là mảng rỗng []
- difficulty phải là: "easy", "medium", hoặc "hard"
- KIỂM TRA TRÙNG LẶP: Đảm bảo không có 2 câu hỏi nào giống nhau về nội dung
`,
	MIXED: (documentContent, numMultipleChoice, numEssay) => `
Bạn là một chuyên gia ra đề thi. Hãy đọc kỹ tài liệu sau và tạo câu hỏi thi hỗn hợp.

TÀI LIỆU:
${documentContent}

⚠️ YÊU CẦU QUAN TRỌNG:
1. Tạo CHÍNH XÁC ${numMultipleChoice} câu hỏi trắc nghiệm - KHÔNG NHIỀU HƠN, KHÔNG ÍT HƠN
2. Tạo CHÍNH XÁC ${numEssay} câu hỏi tự luận - KHÔNG NHIỀU HƠN, KHÔNG ÍT HƠN
3. Đếm kỹ để đảm bảo có đúng ${numMultipleChoice} câu trong "multipleChoice" và ${numEssay} câu trong "essay"
4. Câu hỏi trắc nghiệm: 4 đáp án, chỉ 1 đáp án đúng
5. Câu hỏi tự luận: yêu cầu phân tích, có câu trả lời mẫu
6. Tất cả câu hỏi phải liên quan trực tiếp đến nội dung tài liệu
7. Độ khó đa dạng: easy, medium, hard
8. Cân bằng giữa kiến thức cơ bản và nâng cao
9. MỖI CÂU HỎI PHẢI DUY NHẤT - KHÔNG ĐƯỢC TRÙNG LẶP NỘI DUNG VỚI NHAU (cả trắc nghiệm và tự luận)

ĐỊNH DẠNG JSON TRẢ VỀ (BẮT BUỘC):
{
  "multipleChoice": [
    // PHẢI CÓ CHÍNH XÁC ${numMultipleChoice} ITEMS Ở ĐÂY
    {
      "question": "Nội dung câu hỏi trắc nghiệm?",
      "options": [
        {
          "text": "Đáp án A",
          "isCorrect": true
        },
        {
          "text": "Đáp án B",
          "isCorrect": false
        },
        {
          "text": "Đáp án C",
          "isCorrect": false
        },
        {
          "text": "Đáp án D",
          "isCorrect": false
        }
      ],
      "difficulty": "medium"
    }
  ],
  "essay": [
    // PHẢI CÓ CHÍNH XÁC ${numEssay} ITEMS Ở ĐÂY
    {
      "question": "Câu hỏi tự luận yêu cầu phân tích?",
      "sampleAnswer": "Câu trả lời mẫu chi tiết",
      "difficulty": "medium"
    }
  ]
}

⚠️ CHÚ Ý QUAN TRỌNG:
- Mảng "multipleChoice" PHẢI CÓ CHÍNH XÁC ${numMultipleChoice} câu hỏi
- Mảng "essay" PHẢI CÓ CHÍNH XÁC ${numEssay} câu hỏi
- Trả về ĐÚNG JSON format, không thêm markdown hay text khác
- Mỗi câu trắc nghiệm: 4 options, chỉ 1 isCorrect: true
- Mỗi câu tự luận: phải có sampleAnswer đầy đủ
- difficulty phải là: "easy", "medium", hoặc "hard"
- KIỂM TRA LẠI: Đếm số câu hỏi trước khi trả về để đảm bảo = ${numMultipleChoice} (MC) và ${numEssay} (Essay)
- KIỂM TRA TRÙNG LẶP: Đảm bảo không có 2 câu hỏi nào giống nhau về nội dung (cả trong và giữa các loại câu hỏi)
`,
	SYSTEM_PROMPT: `
Bạn là một giảng viên đại học chuyên nghiệp với kinh nghiệm ra đề thi.
Nhiệm vụ của bạn là tạo câu hỏi thi từ tài liệu được cung cấp.

NGUYÊN TẮC:
1. Câu hỏi phải chính xác, dựa trên nội dung tài liệu
2. Không tạo câu hỏi về thông tin không có trong tài liệu
3. Đảm bảo chất lượng học thuật cao
4. Câu hỏi phải công bằng, không gây nhầm lẫn
5. Trả về ĐÚNG định dạng JSON được yêu cầu
6. KHÔNG thêm bất kỳ text, markdown, hoặc giải thích nào ngoài JSON
7. Đảm bảo JSON hợp lệ, có thể parse được

ĐỊNH DẠNG OUTPUT:
- CHỈ trả về JSON object
- KHÔNG wrap trong markdown code block
- KHÔNG thêm chú thích hay giải thích
- Đảm bảo cú pháp JSON đúng (dấu ngoặc, dấu phẩy, quotes)
`,
	JSON_PARSING_GUIDE: `
Khi nhận response từ AI:
1. Loại bỏ markdown code block nếu có (\`\`\`json ... \`\`\`)
2. Trim whitespace
3. Parse JSON
4. Validate structure:
   - Phải có key "multipleChoice" (array)
   - Phải có key "essay" (array)
   - Mỗi item trong multipleChoice phải có: question, options, difficulty
   - Mỗi option phải có: text, isCorrect
   - Mỗi item trong essay phải có: question, sampleAnswer, difficulty
5. Validate số lượng câu hỏi phải đúng với yêu cầu
`,
};

function determinePromptType(numMultipleChoice, numEssay) {
	if (numMultipleChoice > 0 && numEssay > 0) {
		return "MIXED";
	} else if (numMultipleChoice > 0) {
		return "MULTIPLE_CHOICE_ONLY";
	} else {
		return "ESSAY_ONLY";
	}
}

function getPrompt(promptType, documentContent, numMultipleChoice, numEssay) {
	switch (promptType) {
		case "MULTIPLE_CHOICE_ONLY":
			return PROMPTS.MULTIPLE_CHOICE_ONLY(
				documentContent,
				numMultipleChoice
			);
		case "ESSAY_ONLY":
			return PROMPTS.ESSAY_ONLY(documentContent, numEssay);
		case "MIXED":
			return PROMPTS.MIXED(documentContent, numMultipleChoice, numEssay);
		default:
			throw new Error("Invalid prompt type");
	}
}

function parseAIResponse(aiText) {
	try {
		let cleanedText = aiText.trim();
		if (cleanedText.startsWith("```json")) {
			cleanedText = cleanedText
				.replace(/```json\n?/g, "")
				.replace(/```\n?/g, "");
		} else if (cleanedText.startsWith("```")) {
			cleanedText = cleanedText.replace(/```\n?/g, "");
		}

		cleanedText = cleanedText.trim();

		const parsed = JSON.parse(cleanedText);

		if (!parsed.multipleChoice || !Array.isArray(parsed.multipleChoice)) {
			throw new Error(
				"Invalid response: missing or invalid 'multipleChoice' array"
			);
		}
		if (!parsed.essay || !Array.isArray(parsed.essay)) {
			throw new Error(
				"Invalid response: missing or invalid 'essay' array"
			);
		}

		return parsed;
	} catch (error) {
		console.error("[parseAIResponse] Error parsing AI response:", error);
		console.error("[parseAIResponse] AI text:", aiText);
		throw new Error(`Không thể parse response từ AI: ${error.message}`);
	}
}

module.exports = {
	PROMPTS,
	determinePromptType,
	getPrompt,
	parseAIResponse,
};
