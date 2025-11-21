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
		difficulty: {
			type: String,
			enum: ["easy", "medium", "hard"],
			default: "medium",
		},
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
