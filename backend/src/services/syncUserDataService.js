const bcrypt = require("bcrypt");
const {User} = require("../models/userModel");
const AppError = require("../utils/errors");

const VHU_API_BASE = "https://portal_api.vhu.edu.vn/api";
const VHU_API_KEY = "pscRBF0zT2Mqo6vMw69YMOH43IrB2RtXBS0EHit2kzvL2auxaFJBvw==";
const VHU_CLIENT_ID = "vhu";

class SyncUserDataService {
	static async authenticateVHU(username, password) {
		try {
			const response = await fetch(
				`${VHU_API_BASE}/authenticate/authpsc`,
				{
					method: "POST",
					headers: {
						accept: "application/json, text/plain, */*",
						apikey: VHU_API_KEY,
						clientid: VHU_CLIENT_ID,
						"content-type": "application/json",
					},
					body: JSON.stringify({
						username: username,
						password: password,
						type: 0,
					}),
				}
			);

			const data = await response.json();

			if (!response.ok) {
				const errorMessage =
					data.message || "Sai tên đăng nhập hoặc mật khẩu";
				throw new AppError(errorMessage, 401);
			}

			if (!data.Token) {
				throw new AppError("Không thể xác thực với VHU Portal", 401);
			}

			return {
				token: data.Token,
				fullName: data.FullName,
				username: data.Id,
				role: data.Role,
			};
		} catch (error) {
			if (error instanceof AppError) {
				throw error;
			}
			throw new AppError("Lỗi kết nối đến VHU Portal", 500);
		}
	}

	static async getStudentInfo(token) {
		try {
			const response = await fetch(`${VHU_API_BASE}/student/info`, {
				method: "GET",
				headers: {
					accept: "application/json, text/plain, */*",
					apikey: VHU_API_KEY,
					authorization: `Bearer ${token}`,
					clientid: VHU_CLIENT_ID,
				},
			});

			if (!response.ok) {
				throw new AppError("Không thể lấy thông tin sinh viên", 500);
			}

			const data = await response.json();
			return data.sinhVien;
		} catch (error) {
			if (error instanceof AppError) {
				throw error;
			}
			throw new AppError("Lỗi khi lấy thông tin sinh viên", 500);
		}
	}

	static async getStudyProgram(token) {
		try {
			const response = await fetch(
				`${VHU_API_BASE}/student/getstudyprogram`,
				{
					method: "GET",
					headers: {
						accept: "application/json, text/plain, */*",
						apikey: VHU_API_KEY,
						authorization: `Bearer ${token}`,
						clientid: VHU_CLIENT_ID,
					},
				}
			);

			if (!response.ok) {
				throw new AppError("Không thể lấy chương trình học", 500);
			}

			const data = await response.json();
			return data.length > 0 ? data[0] : null;
		} catch (error) {
			if (error instanceof AppError) {
				throw error;
			}
			throw new AppError("Lỗi khi lấy chương trình học", 500);
		}
	}

	static mapVHUDataToUser(vhuAuth, studentInfo, studyProgram, password) {
		const [day, month, year] = studentInfo.NgaySinh.split("/");
		const birthDate = new Date(year, month - 1, day);

		let unionJoinDate = null;
		if (studentInfo.NgayVaoDoan) {
			const [d, m, y] = studentInfo.NgayVaoDoan.split("/");
			unionJoinDate = new Date(y, m - 1, d);
		}

		let partyJoinDate = null;
		if (studentInfo.NgayVaoDang) {
			const [d, m, y] = studentInfo.NgayVaoDang.split("/");
			partyJoinDate = new Date(y, m - 1, d);
		}

		return {
			full_name: studentInfo.HoTen,
			username: studentInfo.MaSinhVien,
			birth_date: birthDate,
			school_email:
				studentInfo.EmailCaNhan ||
				`${studentInfo.MaSinhVien}@st.vhu.edu.vn`,
			personal_email: studentInfo.EmailTruong || null,
			password: password,
			role: vhuAuth.role === "SV" ? "student" : "admin",

			gender: studentInfo.GioiTinh,
			id_card: studentInfo.CMND,
			ethnicity: studentInfo.DanToc,
			religion: studentInfo.TonGiao,
			academic_year: studentInfo.KhoaHoc,
			position: studentInfo.ChucVu,
			student_type: studentInfo.DoiTuong,
			high_school_class_12: studentInfo.THPTLop12,
			union_member: studentInfo.DoanVien,
			union_join_date: unionJoinDate,
			party_member: studentInfo.DangVien,
			party_join_date: partyJoinDate,
			training_type: studentInfo.LoaiHinhDaoTao,
			academic_advisor: studentInfo.CoVanHocTap,
			advisor_contact: studentInfo.LienHeCoVHT,
			class_name: studentInfo.LopSinhVien,
			study_status: studentInfo.TinhTrangHoc,
			school_year: studentInfo.NienKhoa,
			country: studentInfo.QuocGia,
			province: studentInfo.TinhThanh,
			district: studentInfo.QuanHuyen,
			ward: studentInfo.PhuongXa,
			mobile_phone: studentInfo.DiDong,
			home_phone: studentInfo.DienThoaiBan,
			phone_number: studentInfo.DiDong,
			address: studentInfo.DiaChi,
			contact_person_name: studentInfo.HoTenNguoiLienHe,
			contact_person_address: studentInfo.DiaChiNguoiLienHe,
			contact_person_phone: studentInfo.DienThoaiNguoiLienHe,
			expiry_year: studentInfo.NamHetHan,
			bank_account: studentInfo.STK,
			bank_name: studentInfo.TenNganHang,

			study_program_id: studyProgram?.StudyProgramID,
			study_program_name: studyProgram?.StudyProgramName,
			study_program_type: studyProgram?.Type,

			enrollment_status: this.mapStudyStatus(studentInfo.TinhTrangHoc),
		};
	}

	static mapStudyStatus(vhuStatus) {
		const statusMap = {
			"Còn học": "studying",
			"Bảo lưu": "on_leave",
			"Đã tốt nghiệp": "completed_program",
			"Thôi học": "withdrawn",
		};

		return statusMap[vhuStatus] || "studying";
	}

	static async syncUserData(username, password) {
		try {
			const vhuAuth = await this.authenticateVHU(username, password);

			const studentInfo = await this.getStudentInfo(vhuAuth.token);

			const studyProgram = await this.getStudyProgram(vhuAuth.token);

			const hashedPassword = await bcrypt.hash(password, 12);

			const userData = this.mapVHUDataToUser(
				vhuAuth,
				studentInfo,
				studyProgram,
				hashedPassword
			);

			let user = await User.findOne({username: username});

			if (user) {
				Object.assign(user, userData);
				await user.save();
			} else {
				user = new User(userData);
				await user.save();
			}

			return user;
		} catch (error) {
			throw error;
		}
	}

	static async checkAndSync(username, password) {
		try {
			let user = await User.findOne({username: username});

			user = await this.syncUserData(username, password);

			return user;
		} catch (error) {
			throw error;
		}
	}
}

module.exports = SyncUserDataService;
