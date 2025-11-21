const express = require("express");
const {
	login,
	refreshAccessToken,
	logout,
	getCurrentUser,
	searchUserByUsername,
} = require("../controllers/userController");
const {verifyToken} = require("../middlewares/authMiddleware");

const router = express.Router();

router.post("/login", login);
router.post("/refresh", refreshAccessToken);

router.post("/logout", verifyToken, logout);
router.get("/me", verifyToken, getCurrentUser);
router.get("/search/:username", verifyToken, searchUserByUsername);

module.exports = router;
