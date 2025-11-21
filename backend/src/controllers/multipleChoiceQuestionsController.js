const MultipleChoiceQuestion = require("../models/multipleChoiceQuestionsModel");
const Contest = require("../models/contestModel");
const {NotFoundError, BadRequestError} = require("../utils/errors");

const updateMultipleChoiceQuestion = async (req, res, next) => {
	try {
		const {questionId} = req.params;
		const {question, options, difficulty} = req.body;

		const existingQuestion = await MultipleChoiceQuestion.findById(
			questionId
		);
		if (!existingQuestion) {
			throw new NotFoundError("Không tìm thấy câu hỏi");
		}

		if (options && options.length > 0) {
			const hasCorrectAnswer = options.some(
				(opt) => opt.isCorrect === true
			);
			if (!hasCorrectAnswer) {
				throw new BadRequestError("Phải có ít nhất một đáp án đúng");
			}
		}

		if (question !== undefined) existingQuestion.question = question;
		if (options !== undefined) existingQuestion.options = options;
		if (difficulty !== undefined) existingQuestion.difficulty = difficulty;

		await existingQuestion.save();

		res.status(200).json({
			success: true,
			message: "Cập nhật câu hỏi trắc nghiệm thành công",
			data: existingQuestion,
		});
	} catch (error) {
		next(error);
	}
};

const deleteMultipleChoiceQuestion = async (req, res, next) => {
	try {
		const {questionId} = req.params;

		const question = await MultipleChoiceQuestion.findById(questionId);
		if (!question) {
			throw new NotFoundError("Không tìm thấy câu hỏi");
		}

		// Xóa câu hỏi khỏi contest
		if (question.contest) {
			await Contest.findByIdAndUpdate(question.contest, {
				$pull: {multipleChoiceQuestions: questionId},
			});
		}

		await MultipleChoiceQuestion.findByIdAndDelete(questionId);

		res.status(200).json({
			success: true,
			message: "Xóa câu hỏi trắc nghiệm thành công",
		});
	} catch (error) {
		next(error);
	}
};

module.exports = {
	updateMultipleChoiceQuestion,
	deleteMultipleChoiceQuestion,
};
