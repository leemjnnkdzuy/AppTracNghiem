const EssayQuestion = require("../models/essayQuestionsModel");
const Contest = require("../models/contestModel");
const {NotFoundError, BadRequestError} = require("../utils/errors");

const updateEssayQuestion = async (req, res, next) => {
	try {
		const {questionId} = req.params;
		const {question, gradingCriteria} = req.body;

		const existingQuestion = await EssayQuestion.findById(questionId);
		if (!existingQuestion) {
			throw new NotFoundError("Không tìm thấy câu hỏi");
		}

		if (question !== undefined) existingQuestion.question = question;
		if (gradingCriteria !== undefined)
			existingQuestion.gradingCriteria = gradingCriteria;

		await existingQuestion.save();

		res.status(200).json({
			success: true,
			message: "Cập nhật câu hỏi tự luận thành công",
			data: existingQuestion,
		});
	} catch (error) {
		next(error);
	}
};

const deleteEssayQuestion = async (req, res, next) => {
	try {
		const {questionId} = req.params;

		const question = await EssayQuestion.findById(questionId);
		if (!question) {
			throw new NotFoundError("Không tìm thấy câu hỏi");
		}

		// Xóa câu hỏi khỏi contest
		if (question.contest) {
			await Contest.findByIdAndUpdate(question.contest, {
				$pull: {essayQuestions: questionId},
			});
		}

		await EssayQuestion.findByIdAndDelete(questionId);

		res.status(200).json({
			success: true,
			message: "Xóa câu hỏi tự luận thành công",
		});
	} catch (error) {
		next(error);
	}
};

module.exports = {
	updateEssayQuestion,
	deleteEssayQuestion,
};
