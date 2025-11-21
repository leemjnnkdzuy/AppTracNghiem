using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using AppTracNghiem.Models;
using AppTracNghiem.Services;
using AppTracNghiem.Helpers;

namespace AppTracNghiem
{
    public partial class CreateContestForm : Form
    {
        private ContestService _contestService;
        private string _contestId;
        private int _membersCount;
        private bool _isEditMode;

        public CreateContestForm(string? contestId = null)
        {
            InitializeComponent();
            _contestService = new ContestService();
            _contestId = contestId ?? string.Empty;
            _membersCount = 0;
            _isEditMode = !string.IsNullOrEmpty(contestId);

            dungeonToggleButton1.Click += (s, e) => UpdateEndDateState();
            materialButton4.Click += MaterialButton4_Click;
            materialButton3.Click += MaterialButton3_Click;

            UpdateEndDateState();
            UpdateMemberListButton();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void materialRadioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void UpdateEndDateState()
        {
            if (dungeonToggleButton1.Toggled)
            {
                dtpEnd.Enabled = true;
            }
            else
            {
                dtpEnd.Enabled = false;
            }
        }

        private void UpdateMemberListButton()
        {
            if (_membersCount > 0)
            {
                materialButton1.Text = $"Sửa Danh Sách ({_membersCount})";
                string iconPath = Path.Combine(Application.StartupPath, "Assets", "edit-alt.png");
                if (File.Exists(iconPath))
                {
                    materialButton1.Icon = Image.FromFile(iconPath);
                }
            }
            else
            {
                materialButton1.Text = "Tạo Danh Sách";
                string iconPath = Path.Combine(Application.StartupPath, "Assets", "layers-plus-alt.png");
                if (File.Exists(iconPath))
                {
                    materialButton1.Icon = Image.FromFile(iconPath);
                }
            }
        }

        private void CreateContestForm_Load(object sender, EventArgs e)
        {
            if (_isEditMode)
            {
                _ = LoadContestData();
            }
            else
            {
                materialMaskedTextBox1.Text = $"Đề thi mới {DateTime.Now:dd/MM/yyyy HH:mm}";
                materialMaskedTextBox2.Text = "60";
                dtpStart.Value = DateTime.Now;
                dungeonNumeric1.Value = 1;
                dungeonToggleButton1.Toggled = false;
                materialRadioButton2.Checked = true;
                this.Text = "Tạo đề thi mới";
            }
        }

        private async Task LoadContestData()
        {
            try
            {
                var tokenData = TokenManager.GetTokenData();
                if (tokenData == null) return;

                var contest = await _contestService.GetContestByIdAsync(_contestId, tokenData.AccessToken);
                if (contest != null)
                {
                    materialMaskedTextBox1.Text = contest.Title;
                    materialMaskedTextBox2.Text = contest.Duration.ToString();
                    dtpStart.Value = contest.StartDate;

                    if (contest.EndDate.HasValue)
                    {
                        dungeonToggleButton1.Toggled = true;
                        dtpEnd.Value = contest.EndDate.Value;
                    }
                    else
                    {
                        dungeonToggleButton1.Toggled = false;
                    }

                    if (contest.Visibility == "public")
                    {
                        materialRadioButton1.Checked = true;
                    }
                    else
                    {
                        materialRadioButton2.Checked = true;
                    }

                    materialCheckBox1.Checked = contest.HasMultipleChoice ?? false;
                    materialCheckBox2.Checked = contest.HasEssay ?? false;
                    dungeonToggleButton2.Toggled = contest.ShowScore ?? false;
                    dungeonToggleButton4.Toggled = contest.ShowAnswers ?? false;
                    dungeonToggleButton3.Toggled = contest.LockScreen ?? false;
                    dungeonNumeric1.Value = contest.MaxAttempts ?? 1;

                    _membersCount = contest.Members?.Count ?? 0;
                    UpdateMemberListButton();

                    this.Text = $"Chỉnh sửa: {contest.Title}";
                }
            }
            catch { }
        }



        private void CreateContestForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Nếu đóng form mà chưa hoàn tất, có thể xóa contest draft
            // Hoặc giữ lại để người dùng tiếp tục sau
        }

        private void materialRadioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void materialRadioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private async void materialButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_contestId))
            {
                if (!await SaveContestData("draft"))
                {
                    return;
                }
            }

            var createListMemberForm = new CreateListMemberContest(_contestId);
            if (createListMemberForm.ShowDialog() == DialogResult.OK)
            {
                var tokenData = TokenManager.GetTokenData();
                if (tokenData != null)
                {
                    var members = await _contestService.GetContestMembersAsync(_contestId, tokenData.AccessToken);
                    _membersCount = members.Count;
                    UpdateMemberListButton();
                }
            }
        }

        private async void materialButton2_Click(object sender, EventArgs e)
        {
            // Nút "Tạo Bộ Câu Hỏi" - Mở form CreateExamQuestion
            if (string.IsNullOrEmpty(_contestId))
            {
                if (!await SaveContestData("draft"))
                {
                    return;
                }
            }

            var createExamQuestionForm = new CreateExamQuestion(
                _contestId, 
                materialCheckBox1.Checked,  // hasMultipleChoice
                materialCheckBox2.Checked    // hasEssay
            );
            createExamQuestionForm.FormClosed += (s, args) => this.Close();
            createExamQuestionForm.Show();
            this.Hide();
        }

        private async void MaterialButton4_Click(object? sender, EventArgs e)
        {
            if (await SaveContestData("draft"))
            {
                this.Close();
            }
        }

        private async void MaterialButton3_Click(object? sender, EventArgs e)
        {
            // Nút "Hoàn Tất Tạo Đề" - Logic sẽ viết sau
            MessageBox.Show("Chức năng đang phát triển", 
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async Task<bool> SaveContestData(string status = "draft")
        {
            if (string.IsNullOrWhiteSpace(materialMaskedTextBox1.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đề thi!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                var tokenData = TokenManager.GetTokenData();
                if (tokenData == null)
                {
                    MessageBox.Show("Vui lòng đăng nhập lại!",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (string.IsNullOrEmpty(_contestId))
                {
                    int duration = 60;
                    int.TryParse(materialMaskedTextBox2.Text, out duration);

                    var createRequest = new CreateContestRequest
                    {
                        Title = materialMaskedTextBox1.Text.Trim(),
                        Description = "",
                        StartDate = dtpStart.Value,
                        EndDate = dungeonToggleButton1.Toggled ? (DateTime?)dtpEnd.Value : null,
                        Duration = duration,
                        Visibility = materialRadioButton1.Checked ? "public" : "private",
                        IsDurationLimited = false,
                        IsEndDateSet = dungeonToggleButton1.Toggled,
                        AllowReview = "afterEnd",
                        ShuffleQuestions = false,
                        ShuffleAnswers = false,
                        HasMultipleChoice = materialCheckBox1.Checked,
                        HasEssay = materialCheckBox2.Checked,
                        ShowScore = dungeonToggleButton2.Toggled,
                        ShowAnswers = dungeonToggleButton4.Toggled,
                        LockScreen = dungeonToggleButton3.Toggled,
                        MaxAttempts = (int)dungeonNumeric1.Value,
                        Members = new List<ContestMemberModel>()
                    };

                    var response = await _contestService.CreateContestAsync(createRequest, tokenData.AccessToken);
                    if (response?.Success == true && response.Data != null)
                    {
                        _contestId = response.Data.Id;
                        _isEditMode = true;

                        MessageBox.Show("Tạo đề thi thành công!",
                            "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    else
                    {
                        MessageBox.Show($"Lỗi khi tạo đề thi: {response?.Message ?? "Không có phản hồi từ server"}",
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    int duration = 60;
                    int.TryParse(materialMaskedTextBox2.Text, out duration);

                    var updateRequest = new UpdateContestRequest
                    {
                        Title = materialMaskedTextBox1.Text.Trim(),
                        Description = "",
                        StartDate = dtpStart.Value,
                        EndDate = dungeonToggleButton1.Toggled ? (DateTime?)dtpEnd.Value : null,
                        Duration = duration,
                        Visibility = materialRadioButton1.Checked ? "public" : "private",
                        IsDurationLimited = false,
                        IsEndDateSet = dungeonToggleButton1.Toggled,
                        HasMultipleChoice = materialCheckBox1.Checked,
                        HasEssay = materialCheckBox2.Checked,
                        ShowScore = dungeonToggleButton2.Toggled,
                        ShowAnswers = dungeonToggleButton4.Toggled,
                        LockScreen = dungeonToggleButton3.Toggled,
                        MaxAttempts = (int)dungeonNumeric1.Value,
                        Status = status
                    };

                    var success = await _contestService.UpdateContestAsync(_contestId, updateRequest, tokenData.AccessToken);

                    if (success)
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Lỗi khi cập nhật đề thi!",
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void materialButton4_Click_1(object sender, EventArgs e)
        {

        }
    }
}
