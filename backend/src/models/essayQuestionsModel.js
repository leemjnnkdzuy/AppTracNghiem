const mongoose = require("mongoose");

const essayQuestionSchema = new mongoose.Schema(
	{
		question: {
			type: String,
			required: true,
			trim: true,
		},
		sampleAnswer: {
			type: String,
			trim: true,
		},
		gradingCriteria: {
			type: String,
			trim: true,
		},
		maxLength: {
			type: Number,
			default: 1000, // Số ký tự tối đa
		},
		minLength: {
			type: Number,
			default: 50,
		},
		difficulty: {
			type: String,
			enum: ["easy", "medium", "hard"],
			default: "medium",
		},
		keywords: [
			{
				type: String,
				trim: true,
			},
		], // Từ khóa quan trọng cần có trong câu trả lời
		createdBy: {
			type: mongoose.Schema.Types.ObjectId,
			ref: "User",
		},
		contest: {
			type: mongoose.Schema.Types.ObjectId,
			ref: "Contest",
		},
	},
	{
		timestamps: true,
	}
);

const EssayQuestion = mongoose.model("EssayQuestion", essayQuestionSchema);

module.exports = EssayQuestion;
