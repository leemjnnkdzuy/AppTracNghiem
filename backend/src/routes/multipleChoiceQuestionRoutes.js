const express = require("express");
const {
	updateMultipleChoiceQuestion,
	deleteMultipleChoiceQuestion,
} = require("../controllers/multipleChoiceQuestionsController");
const {authenticate, requireAdmin} = require("../middlewares/authMiddleware");

const router = express.Router();

router.put(
	"/:questionId",
	authenticate,
	requireAdmin,
	updateMultipleChoiceQuestion
);

router.delete(
	"/:questionId",
	authenticate,
	requireAdmin,
	deleteMultipleChoiceQuestion
);

module.exports = router;
