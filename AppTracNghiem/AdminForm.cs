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
using AppTracNghiem.Services;
using AppTracNghiem.Models;

namespace AppTracNghiem
{
    public partial class AdminForm : Form
    {
        private readonly AuthService _authService;
        private readonly ContestService _contestService;
        private ContextMenuStrip _avatarMenu;
        private List<ContestModel> _contests;
        private Dictionary<string, CreateContestForm> _openEditForms;

        public AdminForm()
        {
            InitializeComponent();
            _authService = new AuthService();
            _contestService = new ContestService();
            _contests = new List<ContestModel>();
            _avatarMenu = new ContextMenuStrip();
            _openEditForms = new Dictionary<string, CreateContestForm>();

            LoadAvatar();
            SetupAvatarMenu();
            SetupAvatarClick();
            _ = LoadContests();
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

        private async Task LoadContests()
        {
            try
            {                
                var tokenData = TokenManager.GetTokenData();
                if (tokenData == null)
                {
                    return;
                }
                
                _contests = await _contestService.GetAllContestsAsync(tokenData.AccessToken);
                
                if (_contests != null && _contests.Count > 0)
                {
                    foreach (var c in _contests)
                    {
                        Console.WriteLine($"  - Contest: {c.Id} | {c.Title} | Status: {c.Status}");
                    }
                }
                
                DisplayContests();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách đề thi: {ex.Message}\n\nChi tiết: {ex.InnerException?.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayContests()
        {
            Console.WriteLine($"[AdminForm] DisplayContests - Start with {_contests?.Count ?? 0} contests");
            
            hopeGroupBox3.Controls.Clear();

            if (_contests == null || _contests.Count == 0)
            {
                Console.WriteLine("[AdminForm] DisplayContests - No contests, showing empty message");
                var emptyLabel = new Label
                {
                    Text = "Chưa có đề thi nào\nNhấn 'Tạo Đề Mới' để bắt đầu",
                    Font = new Font("Segoe UI", 12F),
                    ForeColor = Color.Gray,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill
                };
                hopeGroupBox3.Controls.Add(emptyLabel);
                return;
            }
            
            Console.WriteLine($"[AdminForm] DisplayContests - Creating cards for {_contests.Count} contests");

            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(10)
            };

            int yPosition = 10;
            foreach (var contest in _contests)
            {
                var contestCard = CreateContestCard(contest);
                contestCard.Location = new Point(10, yPosition);
                panel.Controls.Add(contestCard);
                yPosition += contestCard.Height + 10;
            }

            hopeGroupBox3.Controls.Add(panel);
        }

        private Panel CreateContestCard(ContestModel contest)
        {
            var card = new Panel
            {
                Width = 1080,
                Height = 80,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
            };

            var titleLabel = new Label
            {
                Text = contest.Title,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                Location = new Point(15, 15),
                Width = 600,
                AutoSize = false
            };
            card.Controls.Add(titleLabel);

            var statusLabel = new Label
            {
                Text = GetStatusText(contest.Status),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = GetStatusColor(contest.Status),
                Location = new Point(15, 45),
                Size = new Size(80, 25),
                TextAlign = ContentAlignment.MiddleCenter
            };
            card.Controls.Add(statusLabel);

            var visibilityLabel = new Label
            {
                Text = contest.Visibility == "public" ? "Công khai" : "Riêng tư",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.White,
                BackColor = contest.Visibility == "public" ? Color.Green : Color.Orange,
                Location = new Point(105, 45),
                Size = new Size(80, 25),
                TextAlign = ContentAlignment.MiddleCenter
            };
            card.Controls.Add(visibilityLabel);

            var membersLabel = new Label
            {
                Text = $"{contest.Members?.Count ?? 0}",
                Image = Image.FromFile(Path.Combine(Application.StartupPath, "Assets", "community.png")),
                ImageAlign = ContentAlignment.MiddleRight,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(63, 81, 181),
                Location = new Point(195, 45),
                Size = new Size(80, 25)
            };
            card.Controls.Add(membersLabel);

            var dateLabel = new Label
            {
                Text = contest.CreatedAt.ToString("dd/MM/yyyy HH:mm"),
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(96, 125, 139),
                Location = new Point(285, 45),
                Size = new Size(130, 25),
                TextAlign = ContentAlignment.MiddleCenter
            };
            card.Controls.Add(dateLabel);

            var questionsLabel = new Label
            {
                Text = $"TN: {contest.CountMultipleChoiceQuestions} | TL: {contest.CountEssayQuestions}",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(0, 150, 136),
                Location = new Point(425, 45),
                Size = new Size(120, 25),
                TextAlign = ContentAlignment.MiddleCenter
            };
            card.Controls.Add(questionsLabel);

            var editBtn = new ReaLTaiizor.Controls.MaterialButton
            {
                Icon = Image.FromFile(Path.Combine(Application.StartupPath, "Assets", "edit-alt.png")),
                IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase,
                Text = "Sửa",
                Location = new Point(810, 20),
                MinimumSize = new Size(120, 36),
                Cursor = Cursors.Hand,
                Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained
            };

            var deleteBtn = new ReaLTaiizor.Controls.MaterialButton
            {
                Icon = Image.FromFile(Path.Combine(Application.StartupPath, "Assets", "trash.png")),
                IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase,
                Text = "Xóa",
                Location = new Point(940, 20),
                MinimumSize = new Size(120, 36),
                Cursor = Cursors.Hand,
                Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained
            };

            editBtn.Click += (s, e) => EditContest(contest, editBtn, deleteBtn);
            deleteBtn.Click += (s, e) => DeleteContest(contest);
            
            card.Controls.Add(editBtn);
            card.Controls.Add(deleteBtn);

            return card;
        }

        private string GetStatusText(string status)
        {
            return status switch
            {
                "draft" => "Nháp",
                "active" => "Hoạt động",
                "completed" => "Hoàn tất",
                "archived" => "Lưu trữ",
                _ => "Không xác định"
            };
        }

        private Color GetStatusColor(string status)
        {
            return status switch
            {
                "draft" => Color.Gray,
                "active" => Color.Green,
                "completed" => Color.Blue,
                "archived" => Color.DarkGray,
                _ => Color.Black
            };
        }

        private async void EditContest(ContestModel contest, ReaLTaiizor.Controls.MaterialButton editBtn, ReaLTaiizor.Controls.MaterialButton deleteBtn)
        {
            try
            {
                if (!string.IsNullOrEmpty(contest.Id) &&
                    _openEditForms.ContainsKey(contest.Id) && 
                    _openEditForms[contest.Id] != null && 
                    !_openEditForms[contest.Id].IsDisposed)
                {
                    _openEditForms[contest.Id].Focus();
                    _openEditForms[contest.Id].BringToFront();
                    return;
                }

                editBtn.Enabled = false;
                editBtn.Text = "Đang mở...";
                editBtn.Icon = null;
                deleteBtn.Enabled = false;

                var tokenData = TokenManager.GetTokenData();
                if (tokenData == null)
                {
                    MessageBox.Show("Vui lòng đăng nhập lại!", 
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    editBtn.Enabled = true;
                    editBtn.Text = "Sửa";
                    deleteBtn.Enabled = true;
                    return;
                }

                if (string.IsNullOrEmpty(contest.Id))
                {
                    MessageBox.Show("ID đề thi không hợp lệ!", 
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    editBtn.Enabled = true;
                    editBtn.Text = "Sửa";
                    deleteBtn.Enabled = true;
                    return;
                }

                var contestData = await _contestService.GetContestByIdAsync(contest.Id, tokenData.AccessToken);
                
                if (contestData == null)
                {
                    MessageBox.Show("Không thể tải dữ liệu đề thi!", 
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    editBtn.Enabled = true;
                    editBtn.Text = "Sửa";
                    deleteBtn.Enabled = true;
                    return;
                }

                var editForm = new CreateContestForm(contest.Id);
                editForm.FormClosed += (s, args) => 
                {
                    _openEditForms.Remove(contest.Id);
                    editBtn.Enabled = true;
                    editBtn.Text = "Sửa";
                    deleteBtn.Enabled = true;
                    _ = LoadContests();
                };
                
                _openEditForms[contest.Id] = editForm;
                editForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải đề thi: {ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                editBtn.Enabled = true;
                editBtn.Text = "Sửa";
                deleteBtn.Enabled = true;
            }
        }

        private async void DeleteContest(ContestModel contest)
        {
            var result = MessageBox.Show(
                $"Bạn có chắc muốn xóa đề thi '{contest.Title}'?\n\nLưu ý: Tất cả câu hỏi trong đề thi cũng sẽ bị xóa!",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    var tokenData = TokenManager.GetTokenData();
                    if (tokenData == null)
                    {
                        MessageBox.Show("Vui lòng đăng nhập lại!", 
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (string.IsNullOrEmpty(contest.Id))
                    {
                        MessageBox.Show("ID đề thi không hợp lệ!", 
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var success = await _contestService.DeleteContestAsync(contest.Id, tokenData.AccessToken);

                    if (success)
                    {
                        await LoadContests();
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa đề thi. Vui lòng thử lại!", 
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa đề thi: {ex.Message}", 
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void BtnTaoDe_Click(object? sender, EventArgs e)
        {
            try
            {
                materialButton1.Enabled = false;
                materialButton1.Text = "Đang tạo...";

                var tokenData = TokenManager.GetTokenData();
                if (tokenData == null)
                {
                    MessageBox.Show("Vui lòng đăng nhập lại!", 
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var request = new CreateContestRequest
                {
                    Title = $"Đề thi mới {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}",
                    Description = "",
                    StartDate = DateTime.Now,
                    Duration = 60,
                    Visibility = "private",
                    IsDurationLimited = false,
                    IsEndDateSet = false,
                    AllowReview = "afterEnd",
                    ShuffleQuestions = false,
                    ShuffleAnswers = false,
                    Members = new List<ContestMemberModel>()
                };

                var response = await _contestService.CreateContestAsync(request, tokenData.AccessToken);

                if (response.Success && response.Data != null)
                {
                    await LoadContests();

                    var editForm = new CreateContestForm(response.Data.Id);
                    editForm.FormClosed += (s, args) => _ = LoadContests();
                    editForm.Show();
                }
                else
                {
                    MessageBox.Show($"Lỗi: {response.Message}", 
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                materialButton1.Enabled = true;
                materialButton1.Text = "Tạo Đề Mới";
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
    }
}
