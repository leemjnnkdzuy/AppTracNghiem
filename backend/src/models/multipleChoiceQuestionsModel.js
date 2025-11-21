const mongoose = require("mongoose");

const multipleChoiceQuestionSchema = new mongoose.Schema(
	{
		question: {
			type: String,
			required: true,
			trim: true,
		},
		options: [
			{
				text: {
					type: String,
					required: true,
				},
				isCorrect: {
					type: Boolean,
					default: false,
				},
			},
		],
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

multipleChoiceQuestionSchema.pre("save", function (next) {
	const hasCorrectAnswer = this.options.some((option) => option.isCorrect);
	if (!hasCorrectAnswer) {
		return next(new Error("Phải có ít nhất một đáp án đúng"));
	}
	next();
});

const MultipleChoiceQuestion = mongoose.model(
	"MultipleChoiceQuestion",
	multipleChoiceQuestionSchema
);

module.exports = MultipleChoiceQuestion;
