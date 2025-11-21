const asyncHandler = require("express-async-handler");
const bcrypt = require("bcrypt");
const {User} = require("../models/userModel");
const SyncUserDataService = require("../services/syncUserDataService");
const JWTService = require("../utils/jwtService");
const AppError = require("../utils/errors");

const login = asyncHandler(async (req, res) => {
	const {username, password} = req.body;

	if (!username || !password) {
		throw new AppError(
			"Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu",
			400
		);
	}

	let user = await User.findOne({username: username});

	if (user && user.role === "admin") {
		const isPasswordValid = await bcrypt.compare(password, user.password);
		if (!isPasswordValid) {
			throw new AppError("Sai tên đăng nhập hoặc mật khẩu", 401);
		}
	} else {
		user = await SyncUserDataService.checkAndSync(username, password);
	}

	if (!user) {
		throw new AppError("Đăng nhập thất bại", 401);
	}

	if (!user.is_active) {
		throw new AppError("Tài khoản đã bị khóa", 403);
	}

	const {
		accessToken,
		refreshToken,
		accessTokenExpiresAt,
		refreshTokenExpiresAt,
	} = JWTService.generateTokens(user._id);

	const deviceInfo = JWTService.getDeviceInfo(req);
	const ipAddress = JWTService.getClientIP(req);

	user.refreshTokens.push({
		token: refreshToken,
		expiresAt: refreshTokenExpiresAt,
		deviceInfo: deviceInfo,
		ipAddress: ipAddress,
	});

	await user.save();

	const userResponse = {
		id: user._id,
		full_name: user.full_name,
		username: user.username,
		birth_date: user.birth_date,
		school_email: user.school_email,
		personal_email: user.personal_email,
		role: user.role,
		enrollment_status: user.enrollment_status,
		avatar: user.avatar,
		phone_number: user.phone_number,
		class_id: user.class_id,
		class_name: user.class_name,
		is_active: user.is_active,

		gender: user.gender,
		study_program_name: user.study_program_name,
		academic_advisor: user.academic_advisor,
		advisor_contact: user.advisor_contact,
		study_status: user.study_status,
		training_type: user.training_type,
		school_year: user.school_year,
		address: user.address,
	};

	res.status(200).json({
		success: true,
		message: "Đăng nhập thành công",
		data: {
			user: userResponse,
			tokens: {
				accessToken: accessToken,
				refreshToken: refreshToken,
				expiresAt: accessTokenExpiresAt,
			},
		},
	});
});

const refreshAccessToken = asyncHandler(async (req, res) => {
	const {refreshToken} = req.body;

	if (!refreshToken) {
		throw new AppError("Refresh token is required", 400);
	}

	const user = await User.findOne({
		"refreshTokens.token": refreshToken,
	});

	if (!user) {
		throw new AppError("Invalid refresh token", 401);
	}

	const tokenObj = user.refreshTokens.find((t) => t.token === refreshToken);

	if (!tokenObj) {
		throw new AppError("Invalid refresh token", 401);
	}

	if (JWTService.isRefreshTokenExpired(tokenObj)) {
		user.refreshTokens = user.refreshTokens.filter(
			(t) => t.token !== refreshToken
		);
		await user.save();
		throw new AppError("Refresh token expired", 401);
	}

	const newAccessToken = JWTService.generateAccessToken(user._id);
	const expiresAt = new Date(Date.now() + JWTService.getAccessTokenTTL());

	res.status(200).json({
		success: true,
		data: {
			accessToken: newAccessToken,
			expiresAt: expiresAt,
		},
	});
});

const logout = asyncHandler(async (req, res) => {
	const {refreshToken} = req.body;
	const userId = req.user.id;

	const user = await User.findById(userId);

	if (!user) {
		throw new AppError("User not found", 404);
	}

	if (refreshToken) {
		user.refreshTokens = user.refreshTokens.filter(
			(t) => t.token !== refreshToken
		);
	} else {
		user.refreshTokens = [];
	}

	await user.save();

	res.status(200).json({
		success: true,
		message: "Đăng xuất thành công",
	});
});

const getCurrentUser = asyncHandler(async (req, res) => {
	const userId = req.user.id;

	const user = await User.findById(userId).select("-password -refreshTokens");

	if (!user) {
		throw new AppError("User not found", 404);
	}

	res.status(200).json({
		success: true,
		data: {
			user: user,
		},
	});
});

const searchUserByUsername = asyncHandler(async (req, res) => {
	const {username} = req.params;

	if (!username) {
		throw new AppError("Username is required", 400);
	}

	const user = await User.findOne({username: username}).select(
		"-password -refreshTokens"
	);

	if (!user) {
		throw new AppError("Không tìm thấy người dùng với MSSV này", 404);
	}

	res.status(200).json({
		success: true,
		data: {
			user: user,
		},
	});
});

module.exports = {
	login,
	refreshAccessToken,
	logout,
	getCurrentUser,
	searchUserByUsername,
};
