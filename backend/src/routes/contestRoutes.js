const express = require("express");
const multer = require("multer");
const {
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
} = require("../controllers/contestController");
const {authenticate, requireAdmin} = require("../middlewares/authMiddleware");

const router = express.Router();

const storage = multer.memoryStorage();
const upload = multer({
	storage: storage,
	limits: {
		fileSize: 20 * 1024 * 1024,
	},
	fileFilter: (req, file, cb) => {
		const allowedTypes = [
			"application/pdf",
			"application/vnd.openxmlformats-officedocument.wordprocessingml.document",
			"text/plain",
		];
		if (allowedTypes.includes(file.mimetype)) {
			cb(null, true);
		} else {
			cb(new Error("Chỉ chấp nhận file PDF, DOCX, hoặc TXT"));
		}
	},
});

router.post("/", authenticate, requireAdmin, createContest);

router.get("/", authenticate, requireAdmin, getAllContests);

router.put("/:contestId", authenticate, requireAdmin, updateContest);

router.get("/:contestId", authenticate, requireAdmin, getContestById);

router.delete("/:contestId", authenticate, requireAdmin, deleteContest);

router.post(
	"/upload-document",
	authenticate,
	requireAdmin,
	upload.single("document"),
	uploadDocument
);

// Generate question bank (không lưu vào contest)
router.post(
	"/generate-question-bank",
	authenticate,
	requireAdmin,
	generateQuestionBank
);

// Add selected questions to contest
router.post(
	"/add-questions",
	authenticate,
	requireAdmin,
	addQuestionsToContest
);

router.get(
	"/questions/:contestId",
	authenticate,
	requireAdmin,
	getQuestionsByContest
);

router.put(
	"/multiple-choice/:questionId",
	authenticate,
	requireAdmin,
	updateMultipleChoiceQuestion
);

router.put(
	"/essay/:questionId",
	authenticate,
	requireAdmin,
	updateEssayQuestion
);

router.delete(
	"/multiple-choice/:questionId",
	authenticate,
	requireAdmin,
	deleteMultipleChoiceQuestion
);

router.delete(
	"/essay/:questionId",
	authenticate,
	requireAdmin,
	deleteEssayQuestion
);

router.post(
	"/add-manual-question",
	authenticate,
	requireAdmin,
	addManualQuestion
);

router.post(
	"/:contestId/members",
	authenticate,
	requireAdmin,
	addMemberToContest
);

router.delete(
	"/:contestId/members/:userId",
	authenticate,
	requireAdmin,
	removeMemberFromContest
);

router.get(
	"/:contestId/members",
	authenticate,
	requireAdmin,
	getContestMembers
);

module.exports = router;
