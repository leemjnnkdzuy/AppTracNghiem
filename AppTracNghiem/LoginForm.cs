using AppTracNghiem.Helpers;
using AppTracNghiem.Services;
using System.Diagnostics;

namespace AppTracNghiem
{
    public partial class LoginForm : Form
    {
        private readonly AuthService _authService;

        public LoginForm()
        {
            InitializeComponent();
            _authService = new AuthService();
            btnLogin.Click += Button1_Click;
            txtPassword.KeyPress += TxtPassword_KeyPress;
            pictureBox1.Click += pictureBox1_Click;
        }

        private async void Button1_Click(object? sender, EventArgs e)
        {
            await PerformLogin();
        }

        private async void TxtPassword_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                await PerformLogin();
            }
        }

        private async Task PerformLogin()
        {
            string username = txtMssv.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Vui lòng nhập MSSV!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMssv.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            btnLogin.Enabled = false;
            txtMssv.Enabled = false;
            txtPassword.Enabled = false;
            btnLogin.Text = "Đang đăng nhập...";

            try
            {
                var response = await _authService.LoginAsync(username, password);
                if (response != null && response.Success && response.Data != null)
                {
                    TokenManager.SaveToken(response.Data.Tokens, response.Data.User);

                    var role = response.Data.User.Role?.Trim();
                    Hide();
                    if (!string.IsNullOrEmpty(role) &&
                        string.Equals(role, "admin", StringComparison.OrdinalIgnoreCase))
                    {
                        var adminForm = new AdminForm();
                        adminForm.FormClosed += (_, _) => Close();
                        adminForm.Show();
                    }
                    else
                    {
                        var mainForm = new MainForm();
                        mainForm.FormClosed += (_, _) => Close();
                        mainForm.Show();
                    }
                }
                else
                {
                    MessageBox.Show(response?.Message ?? "Sai mật khẩu hoặc tên đăng nhập!",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnLogin.Enabled = true;
                txtMssv.Enabled = true;
                txtPassword.Enabled = true;
                btnLogin.Text = "Đăng nhập";
            }
        }

        private void pictureBox1_Click(object? sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://vhu-portal.leemjnnkdzuy.live/home",
                UseShellExecute = true
            });
        }

        private void label1_Click(object? sender, EventArgs e)
        {
        }

        private void label2_Click(object? sender, EventArgs e)
        {
        }

        private void label4_Click(object? sender, EventArgs e)
        {
        }

        private void label3_Click(object? sender, EventArgs e)
        {
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

        }
    }
}
