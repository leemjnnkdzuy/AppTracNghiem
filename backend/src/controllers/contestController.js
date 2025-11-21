const Contest = require("../models/contestModel");
const {User} = require("../models/userModel");
const MultipleChoiceQuestion = require("../models/multipleChoiceQuestionsModel");
const EssayQuestion = require("../models/essayQuestionsModel");
const generateExamQuestionService = require("../services/generateExamQuestionService");
const AppError = require("../utils/errors");

const uploadDocument = async (req, res) => {
	try {
		if (!req.file) {
			throw new AppError("Không có file được upload", 400);
		}

		const fileBuffer = req.file.buffer;
		const fileType = req.file.mimetype;

		const documentContent = await generateExamQuestionService.parseDocument(
			fileBuffer,
			fileType
		);

		res.status(200).json({
			success: true,
			message: "Upload file thành công",
			data: {
				fileName: req.file.originalname,
				fileSize: req.file.size,
				content: documentContent,
			},
		});
	} catch (error) {
		console.error("[uploadDocument] Error:", error);
		throw error;
	}
};

const generateQuestionBank = async (req, res) => {
	try {
		const {
			documentContent,
			aiModel,
			numMultipleChoice,
			numEssay,
			contestId,
		} = req.body;
		const userId = req.user.id;

		if (!documentContent) {
			throw new AppError("Thiếu nội dung tài liệu", 400);
		}

		if (!aiModel || !["gemini", "deepseek"].includes(aiModel)) {
			throw new AppError(
				"AI model không hợp lệ. Chọn 'gemini' hoặc 'deepseek'",
				400
			);
		}

		if (!contestId) {
			throw new AppError("Thiếu contestId", 400);
		}

		const contest = await Contest.findById(contestId);
		if (!contest) {
			throw new AppError("Không tìm thấy contest", 404);
		}

		if (contest.createdBy.toString() !== userId) {
			throw new AppError(
				"Bạn không có quyền tạo câu hỏi cho đề thi này",
				403
			);
		}

		const requestedMC = parseInt(numMultipleChoice) || 0;
		const requestedEssay = parseInt(numEssay) || 0;

		if (requestedMC === 0 && requestedEssay === 0) {
			throw new AppError("Phải tạo ít nhất một câu hỏi", 400);
		}

		const questions = await generateExamQuestionService.generateQuestions(
			documentContent,
			aiModel,
			requestedMC,
			requestedEssay,
			userId,
			contestId
		);

		res.status(200).json({
			success: true,
			message: "Tạo bộ câu hỏi thành công",
			data: questions,
		});
	} catch (error) {
		throw error;
	}
};

const addQuestionsToContest = async (req, res) => {
	try {
		const {multipleChoiceQuestions, essayQuestions, contestId} = req.body;
		const userId = req.user.id;

		if (!contestId) {
			throw new AppError("Thiếu contestId", 400);
		}

		const contest = await Contest.findById(contestId);
		if (!contest) {
			throw new AppError("Không tìm thấy contest", 404);
		}

		if (contest.createdBy.toString() !== userId) {
			throw new AppError(
				"Bạn không có quyền thêm câu hỏi vào đề thi này",
				403
			);
		}

		const savedMCIds = [];
		const savedEssayIds = [];

		if (multipleChoiceQuestions && multipleChoiceQuestions.length > 0) {
			for (const mcqData of multipleChoiceQuestions) {
				const {_id, createdAt, updatedAt, __v, ...cleanData} = mcqData;

				const question = new MultipleChoiceQuestion(cleanData);
				const saved = await question.save();
				savedMCIds.push(saved._id);
			}
		}

		if (essayQuestions && essayQuestions.length > 0) {
			for (const eqData of essayQuestions) {
				const {_id, createdAt, updatedAt, __v, ...cleanData} = eqData;

				const question = new EssayQuestion(cleanData);
				const saved = await question.save();
				savedEssayIds.push(saved._id);
			}
		}

		if (savedMCIds.length === 0 && savedEssayIds.length === 0) {
			throw new AppError(
				"Phải chọn ít nhất một câu hỏi để thêm vào đề thi",
				400
			);
		}

		if (savedMCIds.length > 0) {
			contest.multipleChoiceQuestions.push(...savedMCIds);
		}

		if (savedEssayIds.length > 0) {
			contest.essayQuestions.push(...savedEssayIds);
		}

		await contest.save();

		res.status(200).json({
			success: true,
			message: "Lưu câu hỏi vào đề thi thành công",
			data: {
				multipleChoiceCount: contest.multipleChoiceQuestions.length,
				essayCount: contest.essayQuestions.length,
			},
		});
	} catch (error) {
		throw error;
	}
};

const getQuestionsByContest = async (req, res) => {
	try {
		const {contestId} = req.params;

		const multipleChoiceQuestions = await MultipleChoiceQuestion.find({
			contest: contestId,
		}).sort({createdAt: 1});

		const essayQuestions = await EssayQuestion.find({
			contest: contestId,
		}).sort({createdAt: 1});

		res.status(200).json({
			success: true,
			data: {
				multipleChoice: multipleChoiceQuestions,
				essay: essayQuestions,
				total: multipleChoiceQuestions.length + essayQuestions.length,
			},
		});
	} catch (error) {
		throw error;
	}
};

const updateMultipleChoiceQuestion = async (req, res) => {
	try {
		const {questionId} = req.params;
		const updateData = req.body;

		const question = await MultipleChoiceQuestion.findByIdAndUpdate(
			questionId,
			updateData,
			{new: true, runValidators: true}
		);

		if (!question) {
			throw new AppError("Không tìm thấy câu hỏi", 404);
		}

		res.status(200).json({
			success: true,
			message: "Cập nhật câu hỏi thành công",
			data: question,
		});
	} catch (error) {
		throw error;
	}
};

const updateEssayQuestion = async (req, res) => {
	try {
		const {questionId} = req.params;
		const updateData = req.body;

		const question = await EssayQuestion.findByIdAndUpdate(
			questionId,
			updateData,
			{new: true, runValidators: true}
		);

		if (!question) {
			throw new AppError("Không tìm thấy câu hỏi", 404);
		}

		res.status(200).json({
			success: true,
			message: "Cập nhật câu hỏi thành công",
			data: question,
		});
	} catch (error) {
		throw error;
	}
};

const deleteMultipleChoiceQuestion = async (req, res) => {
	try {
		const {questionId} = req.params;

		const question = await MultipleChoiceQuestion.findByIdAndDelete(
			questionId
		);

		if (!question) {
			throw new AppError("Không tìm thấy câu hỏi", 404);
		}

		res.status(200).json({
			success: true,
			message: "Xóa câu hỏi thành công",
		});
	} catch (error) {
		throw error;
	}
};

const deleteEssayQuestion = async (req, res) => {
	try {
		const {questionId} = req.params;

		const question = await EssayQuestion.findByIdAndDelete(questionId);

		if (!question) {
			throw new AppError("Không tìm thấy câu hỏi", 404);
		}

		res.status(200).json({
			success: true,
			message: "Xóa câu hỏi thành công",
		});
	} catch (error) {
		throw error;
	}
};

const addManualQuestion = async (req, res) => {
	try {
		const {questionType, questionData, contestId} = req.body;
		const userId = req.user.id;

		let savedQuestion;

		if (questionType === "multipleChoice") {
			const question = new MultipleChoiceQuestion({
				...questionData,
				createdBy: userId,
				contest: contestId,
			});
			savedQuestion = await question.save();
		} else if (questionType === "essay") {
			const question = new EssayQuestion({
				...questionData,
				createdBy: userId,
				contest: contestId,
			});
			savedQuestion = await question.save();
		} else {
			throw new AppError("Loại câu hỏi không hợp lệ", 400);
		}

		res.status(201).json({
			success: true,
			message: "Thêm câu hỏi thành công",
			data: savedQuestion,
		});
	} catch (error) {
		throw error;
	}
};

const createContest = async (req, res) => {
	try {
		const userId = req.user.id;
		console.log("Creating contest with data:", req.body);
		console.log("User ID:", userId);

		const contestData = {
			...req.body,
			createdBy: userId,
			visibility: "private",
			status: "draft",
		};

		const contest = new Contest(contestData);
		await contest.save();

		res.status(201).json({
			success: true,
			message: "Tạo đề thi thành công",
			data: contest,
		});
	} catch (error) {
		console.error("Error creating contest:", error);
		throw error;
	}
};

const updateContest = async (req, res) => {
	try {
		const {contestId} = req.params;
		const userId = req.user.id;
		const updateData = req.body;

		const existingContest = await Contest.findById(contestId);
		if (!existingContest) {
			throw new AppError("Không tìm thấy contest", 404);
		}

		if (existingContest.createdBy.toString() !== userId) {
			throw new AppError("Bạn không có quyền chỉnh sửa đề thi này", 403);
		}

		const contest = await Contest.findByIdAndUpdate(contestId, updateData, {
			new: true,
			runValidators: true,
		});

		res.status(200).json({
			success: true,
			message: "Cập nhật đề thi thành công",
			data: contest,
		});
	} catch (error) {
		throw error;
	}
};

const getContestById = async (req, res) => {
	try {
		const {contestId} = req.params;

		const contest = await Contest.findById(contestId)
			.populate("createdBy", "full_name username")
			.populate("members._id", "full_name username school_email");

		if (!contest) {
			throw new AppError("Không tìm thấy contest", 404);
		}

		res.status(200).json({
			success: true,
			data: contest,
		});
	} catch (error) {
		throw error;
	}
};

const getAllContests = async (req, res) => {
	try {
		const userId = req.user.id;
		const {status, visibility} = req.query;

		const query = {createdBy: userId};
		if (status) query.status = status;
		if (visibility) query.visibility = visibility;

		const contests = await Contest.find(query)
			.populate("createdBy", "full_name username")
			.sort({createdAt: -1});


		res.status(200).json({
			success: true,
			data: contests,
		});
	} catch (error) {
		throw error;
	}
};

const addMemberToContest = async (req, res) => {
	try {
		const {contestId} = req.params;
		const {userId, studentId, fullName, email, class_name} = req.body;

		if (!userId) {
			throw new AppError("userId là bắt buộc", 400);
		}

		const contest = await Contest.findById(contestId);
		if (!contest) {
			throw new AppError("Không tìm thấy contest", 404);
		}

		const user = await User.findById(userId);
		if (!user) {
			throw new AppError("Không tìm thấy người dùng", 404);
		}

		const existingMember = contest.members.find(
			(m) => m._id.toString() === userId
		);

		if (existingMember) {
			throw new AppError("Người dùng đã có trong danh sách", 400);
		}

		contest.members.push({
			_id: userId,
			studentId,
			fullName,
			email,
			class_name: class_name || user.class_name,
		});

		if (!user.contest_joined.includes(contestId)) {
			user.contest_joined.push(contestId);
		}

		await contest.save();
		await user.save();

		res.status(200).json({
			success: true,
			message: "Thêm thành viên thành công",
			data: contest.members,
		});
	} catch (error) {
		throw error;
	}
};

const removeMemberFromContest = async (req, res) => {
	try {
		const {contestId, userId} = req.params;

		const contest = await Contest.findById(contestId);
		if (!contest) {
			throw new AppError("Không tìm thấy contest", 404);
		}

		const user = await User.findById(userId);
		if (!user) {
			throw new AppError("Không tìm thấy người dùng", 404);
		}

		contest.members = contest.members.filter(
			(m) => m._id.toString() !== userId
		);

		user.contest_joined = user.contest_joined.filter(
			(cId) => cId.toString() !== contestId
		);

		await contest.save();
		await user.save();

		res.status(200).json({
			success: true,
			message: "Xóa thành viên thành công",
			data: contest.members,
		});
	} catch (error) {
		throw error;
	}
};

const getContestMembers = async (req, res) => {
	try {
		const {contestId} = req.params;

		const contest = await Contest.findById(contestId);

		if (!contest) {
			throw new AppError("Không tìm thấy contest", 404);
		}

		const members = contest.members.map((member) => ({
			userId: member._id?.toString() || "",
			studentId: member.studentId || "",
			fullName: member.fullName || "",
			email: member.email || "",
			class_name: member.class_name || "",
		}));

		res.status(200).json({
			success: true,
			data: members,
		});
	} catch (error) {
		throw error;
	}
};

const deleteContest = async (req, res) => {
	try {
		const {contestId} = req.params;
		const userId = req.user.id;

		console.log("[deleteContest] ContestId:", contestId);
		console.log("[deleteContest] UserId:", userId);

		const contest = await Contest.findById(contestId);

		if (!contest) {
			throw new AppError("Không tìm thấy contest", 404);
		}

		if (contest.createdBy.toString() !== userId) {
			throw new AppError("Bạn không có quyền xóa đề thi này", 403);
		}

		await MultipleChoiceQuestion.deleteMany({contest: contestId});
		await EssayQuestion.deleteMany({contest: contestId});

		await Contest.findByIdAndDelete(contestId);

		console.log("[deleteContest] Contest deleted successfully");

		res.status(200).json({
			success: true,
			message: "Xóa đề thi thành công",
		});
	} catch (error) {
		console.error("[deleteContest] Error:", error);
		throw error;
	}
};

module.exports = {
	uploadDocument,
	generateQuestionBank,
	addQuestionsToContest,
	getQuestionsByContest,
	updateMultipleChoiceQuestion,
	updateEssayQuestion,
	deleteMultipleChoiceQuestion,
	deleteEssayQuestion,
	addManualQuestion,
	createContest,
	updateContest,
	getContestById,
	getAllContests,
	addMemberToContest,
	removeMemberFromContest,
	getContestMembers,
	deleteContest,
};
