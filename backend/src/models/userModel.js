const e = require("express");
const mongoose = require("mongoose");

const UserSchema = new mongoose.Schema(
	{
		full_name: {
			type: String,
			required: true,
			maxlength: 100,
		},
		username: {
			type: String,
			required: true,
			unique: true,
			maxlength: 50,
		},
		birth_date: {
			type: Date,
			required: true,
		},
		school_email: {
			type: String,
			required: true,
			unique: true,
			maxlength: 100,
		},
		personal_email: {
			type: String,
			default: null,
			maxlength: 100,
		},
		password: {
			type: String,
			required: true,
		},
		is_active: {
			type: Boolean,
			default: true,
		},
		role: {
			type: String,
			enum: ["student", "admin"],
			default: "student",
		},
		enrollment_status: {
			type: String,
			enum: ["studying", "on_leave", "completed_program", "withdrawn"],
			default: "studying",
		},
		avatar: {
			type: String,
			default: null,
		},
		phone_number: {
			type: String,
			unique: true,
			sparse: true,
			default: null,
		},
		class_id: {
			type: String,
			default: null,
		},
		contest_joined: {
			type: [mongoose.Schema.Types.ObjectId],
			default: [],
			ref: "Contest",
		},
		gender: {
			type: String,
			enum: ["Nam", "Nữ", "Khác"],
			default: null,
		},
		id_card: {
			type: String,
			default: null,
		},
		ethnicity: {
			type: String,
			default: null,
		},
		religion: {
			type: String,
			default: null,
		},
		academic_year: {
			type: String,
			default: null,
		},
		position: {
			type: String,
			default: null,
		},
		student_type: {
			type: String,
			default: null,
		},
		high_school_class_12: {
			type: String,
			default: null,
		},
		union_member: {
			type: String,
			default: null,
		},
		union_join_date: {
			type: Date,
			default: null,
		},
		party_member: {
			type: String,
			default: null,
		},
		party_join_date: {
			type: Date,
			default: null,
		},
		training_type: {
			type: String,
			default: null,
		},
		academic_advisor: {
			type: String,
			default: null,
		},
		advisor_contact: {
			type: String,
			default: null,
		},
		class_name: {
			type: String,
			default: null,
		},
		study_status: {
			type: String,
			default: null,
		},
		school_year: {
			type: String,
			default: null,
		},
		country: {
			type: String,
			default: null,
		},
		province: {
			type: String,
			default: null,
		},
		district: {
			type: String,
			default: null,
		},
		ward: {
			type: String,
			default: null,
		},
		mobile_phone: {
			type: String,
			default: null,
		},
		home_phone: {
			type: String,
			default: null,
		},
		address: {
			type: String,
			default: null,
		},
		contact_person_name: {
			type: String,
			default: null,
		},
		contact_person_address: {
			type: String,
			default: null,
		},
		contact_person_phone: {
			type: String,
			default: null,
		},
		expiry_year: {
			type: String,
			default: null,
		},
		bank_account: {
			type: String,
			default: null,
		},
		bank_name: {
			type: String,
			default: null,
		},
		study_program_id: {
			type: String,
			default: null,
		},
		study_program_name: {
			type: String,
			default: null,
		},
		study_program_type: {
			type: Number,
			default: null,
		},
		isVerified: {
			type: Boolean,
			default: false,
		},
		verificationCode: {
			type: String,
		},
		verificationCodeExpires: {
			type: Date,
		},
		resetPasswordToken: {
			type: String,
		},
		resetPasswordExpires: {
			type: Date,
		},
		resetPin: {
			type: String,
		},
		resetPinExpires: {
			type: Date,
		},
		tempAuthHashCode: {
			type: String,
			default: null,
		},
		tempAuthHashCodeExpires: {
			type: Date,
			default: null,
		},
		changeEmailPin: {
			type: String,
		},
		changeEmailPinExpires: {
			type: Date,
		},
		changeMailAuthHashCode: {
			type: String,
		},
		changeMailAuthHashCodeExpires: {
			type: Date,
		},
		newEmail: {
			type: String,
		},
		newEmailPin: {
			type: String,
		},
		newEmailPinExpires: {
			type: Date,
		},
		refreshTokens: [
			{
				token: {
					type: String,
					required: true,
				},
				createdAt: {
					type: Date,
					default: Date.now,
				},
				expiresAt: {
					type: Date,
					required: true,
				},
				deviceInfo: {
					type: String,
					default: null,
				},
				ipAddress: {
					type: String,
					default: null,
				},
			},
		],
	},
	{
		createdAt: "created_at",
		updatedAt: "updated_at",
	}
);

exports.User = mongoose.model("User", UserSchema);
