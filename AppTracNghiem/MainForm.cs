using AppTracNghiem.Helpers;
using AppTracNghiem.Services;

namespace AppTracNghiem
{
    public partial class MainForm : Form
    {
        private readonly AuthService _authService;
        private ContextMenuStrip _avatarMenu;

        public MainForm()
        {
            InitializeComponent();
            _authService = new AuthService();
            _avatarMenu = new ContextMenuStrip();

            LoadUserInfo();
            LoadAvatar();
            SetupAvatarMenu();
            SetupAvatarClick();
        }

        private void LoadUserInfo()
        {
            var user = TokenManager.GetCurrentUser();

            if (user != null)
            {
                labelWelcome.Text = $"Xin chào, {user.FullName}!";
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
                    Bitmap defaultAvatar = new Bitmap(60, 60);
                    using (Graphics g = Graphics.FromImage(defaultAvatar))
                    {
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        g.Clear(Color.LightGray);
                        g.FillEllipse(Brushes.Gray, 0, 0, 59, 59);
                    }
                    pictureBox1.Image = defaultAvatar;
                }

                System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
                gp.AddEllipse(0, 0, pictureBox1.Width - 1, pictureBox1.Height - 1);
                pictureBox1.Region = new Region(gp);

                pictureBox1.Cursor = Cursors.Hand;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải avatar: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SetupAvatarClick()
        {
            pictureBox1.Click += PictureBox1_Click;
        }

        private void SetupAvatarMenu()
        {
            ToolStripMenuItem profileItem = new ToolStripMenuItem("Hồ sơ");
            profileItem.Click += (s, e) => { var profileForm = new Profile(); profileForm.ShowDialog(); };

            ToolStripMenuItem logoutItem = new ToolStripMenuItem("Đăng xuất");
            logoutItem.Click += ButtonLogout_Click;

            _avatarMenu.Items.AddRange(new ToolStripItem[] { profileItem, logoutItem });
            pictureBox1.ContextMenuStrip = _avatarMenu;
        }

        private void PictureBox1_Click(object? sender, EventArgs e)
        {
            _avatarMenu.Show(pictureBox1, new Point(0, pictureBox1.Height));
        }

        private string GetRoleDisplayName(string role)
        {
            return role switch
            {
                "student" => "Sinh viên",
                "teacher" => "Giảng viên",
                "admin" => "Quản trị viên",
                _ => role
            };
        }

        private async void ButtonLogout_Click(object? sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc muốn đăng xuất?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    var token = TokenManager.GetAccessToken();
                    if (!string.IsNullOrEmpty(token))
                    {
                        await _authService.LogoutAsync(token);
                    }
                }
                catch
                {
                    // Ignore logout errors
                }
                finally
                {
                    TokenManager.ClearToken();

                    this.Hide();
                    var loginForm = new LoginForm();
                    loginForm.FormClosed += (s, args) => Application.Exit();
                    loginForm.Show();
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
