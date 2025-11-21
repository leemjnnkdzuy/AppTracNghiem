const express = require("express");
const userRoutes = require("../routes/userRoutes");
const contestRoutes = require("../routes/contestRoutes");
const essayQuestionRoutes = require("../routes/essayQuestionRoutes");
const multipleChoiceQuestionRoutes = require("../routes/multipleChoiceQuestionRoutes");

const router = express.Router();

router.use("/user", userRoutes);
router.use("/contest", contestRoutes);
router.use("/essay-question", essayQuestionRoutes);
router.use("/multiple-choice-question", multipleChoiceQuestionRoutes);

module.exports = router;
