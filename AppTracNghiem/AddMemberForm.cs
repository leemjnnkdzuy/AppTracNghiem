using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppTracNghiem.Services;
using AppTracNghiem.Models;
using AppTracNghiem.Helpers;

namespace AppTracNghiem
{
    public partial class AddMemberForm : Form
    {
        private readonly ContestService _contestService;
        private UserModel? _currentSearchedUser;
        private string _contestId;

        public AddMemberForm(string contestId = "")
        {
			InitializeComponent();
            _contestService = new ContestService();
            _contestId = contestId;
        }

        public UserModel? GetSelectedUser()
        {
            return _currentSearchedUser;
        }

        private void AddMemberForm_Load(object sender, EventArgs e)
        {
            ClearUserInfo();
        }

        private void ClearUserInfo()
        {
            _currentSearchedUser = null;
            lblFullName.Text = "";
            lblUsername.Text = "";
            lblEmail.Text = "";
            lblPhone.Text = "";
            lblGender.Text = "";
            lblClass.Text = "";
            lblProgram.Text = "";
            lblBirthDate.Text = "";
            materialTextBoxEdit1.Text = "";
            materialButton2.Enabled = false;
        }

        private void DisplayUserInfo(UserModel user)
        {
            if (user == null)
            {
                ClearUserInfo();
                return;
            }

            lblFullName.Text = $"Họ và Tên: {user.FullName}";
            lblUsername.Text = $"MSSV: {user.Username}";
            lblEmail.Text = $"Email: {user.SchoolEmail}";
            lblPhone.Text = $"Điện Thoại: {user.Phone ?? "Chưa có"}";
            lblGender.Text = $"Giới Tính: {user.Gender ?? "Chưa có"}";
            lblClass.Text = $"Lớp: {user.ClassName ?? "Chưa có"}";
            lblProgram.Text = $"Chương Trình: {user.MajorName ?? "Chưa có"}";
            lblBirthDate.Text = $"Ngày Sinh: {user.BirthDate ?? "Chưa có"}";
        }

        private async void materialButton1_Click(object sender, EventArgs e)
        {
            string mssv = materialTextBoxEdit1.Text.Trim();
            
            if (string.IsNullOrEmpty(mssv))
            {
                MessageBox.Show("Vui lòng nhập MSSV", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var accessToken = TokenManager.GetAccessToken();
                if (string.IsNullOrEmpty(accessToken))
                {
                    MessageBox.Show("Vui lòng đăng nhập lại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _currentSearchedUser = await _contestService.SearchUserByUsernameAsync(mssv, accessToken);

                if (_currentSearchedUser != null)
                {
                    DisplayUserInfo(_currentSearchedUser);
                    materialButton2.Enabled = true;
                }
                else
                {
                    ClearUserInfo();
                    MessageBox.Show("Sinh viên không tồn tại trong hệ thống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ClearUserInfo();
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (_currentSearchedUser == null)
            {
                MessageBox.Show("Vui lòng tìm kiếm sinh viên trước", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
