const express = require("express");
const {
	updateEssayQuestion,
	deleteEssayQuestion,
} = require("../controllers/essayQuestionsController");
const {authenticate, requireAdmin} = require("../middlewares/authMiddleware");

const router = express.Router();

router.put("/:questionId", authenticate, requireAdmin, updateEssayQuestion);

router.delete("/:questionId", authenticate, requireAdmin, deleteEssayQuestion);

module.exports = router;
