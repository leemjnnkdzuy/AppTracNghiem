const mongoose = require("mongoose");
const bcrypt = require("bcrypt");
const dotenv = require("dotenv");
const {User} = require("../models/userModel");

dotenv.config();

const seedAdmin = async () => {
	try {
		const username = encodeURIComponent(process.env.MONGODB_USER);
		const password = encodeURIComponent(process.env.MONGODB_PASSWORD);
		const cluster = encodeURIComponent(process.env.MONGODB_CLUSTER);
		const database = encodeURIComponent(process.env.MONGODB_DATABASE);

		const uri = `mongodb+srv://${username}:${password}@${cluster}/${database}?retryWrites=true&w=majority`;

		await mongoose.connect(uri, {
			serverSelectionTimeoutMS: 5000,
			socketTimeoutMS: 45000,
		});

		console.log("Kết nối MongoDB thành công");

		const adminExists = await User.findOne({username: "admin@vhu.edu.com"});

		if (adminExists) {
			console.log("User admin đã tồn tại!");
			process.exit(0);
		}

		const hashedPassword = await bcrypt.hash("admin", 10);

		const adminUser = new User({
			full_name: "Administrator",
			username: "admin@vhu.edu.com",
			birth_date: new Date("1990-01-01"),
			school_email: "admin@vhu.edu.com",
			password: hashedPassword,
			role: "admin",
			is_active: true,
			isVerified: true,
			enrollment_status: "studying",
		});

		await adminUser.save();

		console.log("Username: admin@vhu.edu.com");
		console.log("Password: admin");

		process.exit(0);
	} catch (error) {
		console.error("Lỗi khi tạo admin:", error);
		process.exit(1);
	}
};

seedAdmin();
