using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppTracNghiem.Helpers;
using AppTracNghiem.Models;

namespace AppTracNghiem
{
    public partial class Profile : Form
    {
        public Profile()
        {
            InitializeComponent();
            LoadUserProfile();
        }

        private void LoadUserProfile()
        {
            var user = TokenManager.GetCurrentUser();

            if (user != null)
            {
                LoadAvatar();

                lblFullName.Text = user.FullName;
                lblUsername.Text = $"@{user.Username}";
                lblEmail.Text = user.SchoolEmail;
                lblRole.Text = GetRoleDisplayName(user.Role);
                lblClass.Text = user.ClassName ?? "Chưa có thông tin";
                lblBirthDate.Text = user.BirthDate ?? "Chưa có thông tin";
                lblGender.Text = GetGenderDisplayName(user.Gender);
                lblPhone.Text = user.Phone ?? "Chưa có thông tin";
                lblAddress.Text = user.Address ?? "Chưa có thông tin";
                lblProgram.Text = user.ProgramName ?? "Chưa có thông tin";
                lblMajor.Text = user.MajorName ?? "Chưa có thông tin";
                lblEnrollmentYear.Text = user.SchoolYear ?? "Chưa có thông tin";
                lblStatus.Text = GetStatusDisplayName(user.StudentStatus);
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin người dùng!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void LoadAvatar()
        {
            try
            {
                var user = TokenManager.GetCurrentUser();
                string avatarPath = string.Empty;

                if (user != null && !string.IsNullOrEmpty(user.Avatar))
                {
                    avatarPath = Path.Combine(Application.StartupPath, user.Avatar);
                }
                else
                {
                    avatarPath = Path.Combine(Application.StartupPath, "Assets", "avatar_default.png");
                }

                if (File.Exists(avatarPath))
                {
                    pictureBox1.Image = Image.FromFile(avatarPath);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else
                {
                    Bitmap defaultAvatar = new Bitmap(150, 150);
                    using (Graphics g = Graphics.FromImage(defaultAvatar))
                    {
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        g.Clear(Color.LightGray);
                        g.FillEllipse(Brushes.Gray, 0, 0, 149, 149);
                    }
                    pictureBox1.Image = defaultAvatar;
                }

                System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
                gp.AddEllipse(0, 0, pictureBox1.Width - 1, pictureBox1.Height - 1);
                pictureBox1.Region = new Region(gp);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải avatar: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string GetRoleDisplayName(string role)
        {
            return role switch
            {
                "student" => "Sinh viên",
                "teacher" => "Giảng viên",
                "admin" => "Quản trị viên",
                _ => role ?? "Chưa có thông tin"
            };
        }

        private string GetGenderDisplayName(string? gender)
        {
            if (string.IsNullOrEmpty(gender))
                return "Chưa có thông tin";

            return gender.ToLower() switch
            {
                "male" => "Nam",
                "female" => "Nữ",
                "other" => "Khác",
                _ => gender
            };
        }

        private string GetStatusDisplayName(string? status)
        {
            if (string.IsNullOrEmpty(status))
                return "Chưa có thông tin";

            return status.ToLower() switch
            {
                "active" => "Đang học",
                "inactive" => "Tạm nghỉ",
                "graduated" => "Đã tốt nghiệp",
                _ => status
            };
        }

        private void lblUsername_Click(object sender, EventArgs e)
        {

        }
    }
}
