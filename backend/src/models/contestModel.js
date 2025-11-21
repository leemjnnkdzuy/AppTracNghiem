const mongoose = require("mongoose");

const contestSchema = new mongoose.Schema(
	{
		title: {
			type: String,
			required: true,
			trim: true,
		},
		description: {
			type: String,
			trim: true,
		},
		startDate: {
			type: Date,
			required: true,
		},
		endDate: {
			type: Date,
		},
		duration: {
			type: Number,
			required: true,
		},
		visibility: {
			type: String,
			enum: ["public", "private"],
			default: "private",
		},
		isDurationLimited: {
			type: Boolean,
			default: false,
		},
		isEndDateSet: {
			type: Boolean,
			default: false,
		},
		allowReview: {
			type: String,
			enum: ["immediately", "afterEnd", "never"],
			default: "afterEnd",
		},
		shuffleQuestions: {
			type: Boolean,
			default: false,
		},
		shuffleAnswers: {
			type: Boolean,
			default: false,
		},
		hasMultipleChoice: {
			type: Boolean,
			default: true,
		},
		hasEssay: {
			type: Boolean,
			default: false,
		},
		showScore: {
			type: Boolean,
			default: false,
		},
		showAnswers: {
			type: Boolean,
			default: false,
		},
		lockScreen: {
			type: Boolean,
			default: false,
		},
		maxAttempts: {
			type: Number,
			default: 1,
		},
		countMultipleChoiceQuestions: {
			type: Number,
			default: 0,
		},
		countEssayQuestions: {
			type: Number,
			default: 0,
		},
		members: [
			{
				_id: {
					type: mongoose.Schema.Types.ObjectId,
					ref: "User",
				},
				studentId: String,
				fullName: String,
				email: String,
				class_name: String,
			},
		],
		multipleChoiceQuestions: [
			{
				type: mongoose.Schema.Types.ObjectId,
				ref: "MultipleChoiceQuestion",
			},
		],
		essayQuestions: [
			{
				type: mongoose.Schema.Types.ObjectId,
				ref: "EssayQuestion",
			},
		],
		createdBy: {
			type: mongoose.Schema.Types.ObjectId,
			ref: "User",
			required: true,
		},
		status: {
			type: String,
			enum: ["draft", "active", "completed", "archived"],
			default: "draft",
		},
	},
	{
		timestamps: true,
	}
);

const Contest = mongoose.model("Contest", contestSchema);

module.exports = Contest;
