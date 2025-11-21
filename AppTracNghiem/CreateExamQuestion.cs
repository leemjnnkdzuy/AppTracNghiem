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
using AppTracNghiem.Services;
using AppTracNghiem.Models;
using AppTracNghiem.Helpers;
using System.Diagnostics;

namespace AppTracNghiem
{
    public partial class CreateExamQuestion : Form
    {
        private ContestService _contestService;
        private string? _uploadedFilePath;
        private string? _documentContent;
        private GeneratedQuestionsData _currentQuestions;
        private string _contestId;
        private bool _hasMultipleChoice;
        private bool _hasEssay;
        private ContestModel? _currentContest;

        public CreateExamQuestion(string contestId, bool hasMultipleChoice = true, bool hasEssay = true)
        {
            InitializeComponent();
            _contestService = new ContestService();
            _contestId = contestId;
            _currentQuestions = new GeneratedQuestionsData();
            _hasMultipleChoice = hasMultipleChoice;
            _hasEssay = hasEssay;

            InitializeForm();
            this.Load += CreateExamQuestion_Load;
            this.FormClosing += CreateExamQuestion_FormClosing;
        }

        private async void CreateExamQuestion_Load(object? sender, EventArgs e)
        {
            await LoadContestData();
            await LoadExistingQuestions();
        }

        private async Task LoadContestData()
        {
            try
            {
                var tokenData = TokenManager.GetTokenData();
                if (tokenData == null)
                {
                    return;
                }

                _currentContest = await _contestService.GetContestByIdAsync(_contestId, tokenData.AccessToken);
                
                if (_currentContest != null)
                {
                    if (_hasMultipleChoice)
                    {
                        dungeonNumeric1.Value = _currentContest.CountMultipleChoiceQuestions;
                    }
                    
                    if (_hasEssay)
                    {
                        dungeonNumeric2.Value = _currentContest.CountEssayQuestions;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin đề thi: {ex.Message}\n\nChi tiết: {ex.InnerException?.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async Task LoadExistingQuestions()
        {
            try
            {

                var tokenData = TokenManager.GetTokenData();
                if (tokenData == null)
                {
                    return;
                }

                var response = await _contestService.GetQuestionsByContestAsync(_contestId, tokenData.AccessToken);

                if (response != null && response.Success && response.Data != null)
                {
                    if (response.Data.MultipleChoice != null && response.Data.MultipleChoice.Count > 0)
                    {
                        _currentQuestions.MultipleChoice.AddRange(response.Data.MultipleChoice);
                    }

                    if (response.Data.Essay != null && response.Data.Essay.Count > 0)
                    {
                        _currentQuestions.Essay.AddRange(response.Data.Essay);
                    }

                    if (_currentQuestions.MultipleChoice.Count > 0 || _currentQuestions.Essay.Count > 0)
                    {
                        UpdateUI();
                        DisplayQuestions();
                        int total = _currentQuestions.MultipleChoice.Count + _currentQuestions.Essay.Count;

                    }
                }

                UpdateDungeonNumericLimits();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải câu hỏi: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateDungeonNumericLimits()
        {
            if (_hasMultipleChoice)
            {
                dungeonNumeric1.Minimum = 0;
                dungeonNumeric1.Maximum = _currentQuestions.MultipleChoice.Count;
                
                if (dungeonNumeric1.Value > dungeonNumeric1.Maximum)
                {
                    dungeonNumeric1.Value = dungeonNumeric1.Maximum;
                }
            }

            if (_hasEssay)
            {
                dungeonNumeric2.Minimum = 0;
                dungeonNumeric2.Maximum = _currentQuestions.Essay.Count;
                
                if (dungeonNumeric2.Value > dungeonNumeric2.Maximum)
                {
                    dungeonNumeric2.Value = dungeonNumeric2.Maximum;
                }
            }
        }

        private void InitializeForm()
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            hopeGroupBox3.AllowDrop = true;
            hopeGroupBox3.DragEnter += HopeGroupBox3_DragEnter;
            hopeGroupBox3.DragDrop += HopeGroupBox3_DragDrop;
            hopeGroupBox3.Click += HopeGroupBox3_Click;

            foreach (Control ctrl in hopeGroupBox3.Controls)
            {
                ctrl.Click += HopeGroupBox3_Click;
                if (ctrl is PictureBox || ctrl is Label)
                {
                    ctrl.Cursor = Cursors.Hand;
                }
            }

            materialButton2.Click += MaterialButton2_Click;
            materialButton1.Click += MaterialButton1_Click;

            dungeonNumeric1.TextChanged += DungeonNumeric_ValueChanged;
            dungeonNumeric2.TextChanged += DungeonNumeric_ValueChanged;

            materialRadioButton8.Checked = true;

            dungeonNumeric1.Enabled = _hasMultipleChoice;
            label5.Enabled = _hasMultipleChoice;
            materialTextBoxEdit2.Enabled = _hasMultipleChoice;
            label12.Enabled = _hasMultipleChoice;

            dungeonNumeric2.Enabled = _hasEssay;
            label10.Enabled = _hasEssay;
            materialTextBoxEdit1.Enabled = _hasEssay;
            label11.Enabled = _hasEssay;

            if (!_hasMultipleChoice)
            {
                dungeonNumeric1.Value = 0;
                dungeonNumeric1.Minimum = 0;
                dungeonNumeric1.Maximum = 0;
                materialTextBoxEdit2.Text = "";
            }
            else
            {
                dungeonNumeric1.Value = 0;
                dungeonNumeric1.Minimum = 0;
                dungeonNumeric1.Maximum = 100;
            }

            if (!_hasEssay)
            {
                dungeonNumeric2.Value = 0;
                dungeonNumeric2.Minimum = 0;
                dungeonNumeric2.Maximum = 0;
                materialTextBoxEdit1.Text = "";
            }
            else
            {
                dungeonNumeric2.Value = 0;
                dungeonNumeric2.Minimum = 0;
                dungeonNumeric2.Maximum = 100;
            }

            this.Text = "Tạo Bộ Câu Hỏi - ";
            if (_hasMultipleChoice && _hasEssay)
            {
                this.Text += "Trắc Nghiệm & Tự Luận";
            }
            else if (_hasMultipleChoice)
            {
                this.Text += "Chỉ Trắc Nghiệm";
            }
            else if (_hasEssay)
            {
                this.Text += "Chỉ Tự Luận";
            }

            UpdateUI();
        }

        private void HopeGroupBox3_Click(object? sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Tài liệu|*.pdf;*.docx;*.txt|All files (*.*)|*.*";
                openFileDialog.Title = "Chọn tệp ngân hàng đề";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _uploadedFilePath = openFileDialog.FileName;
                    UpdateFileUploadUI();
                }
            }
        }

        private void HopeGroupBox3_DragEnter(object? sender, DragEventArgs e)
        {
            if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void HopeGroupBox3_DragDrop(object? sender, DragEventArgs e)
        {
            if (e.Data == null)
                return;

            var data = e.Data.GetData(DataFormats.FileDrop);
            if (data is not string[] files || files.Length == 0)
                return;

            string file = files[0];
            string ext = Path.GetExtension(file).ToLower();

            if (ext == ".pdf" || ext == ".docx" || ext == ".txt")
            {
                _uploadedFilePath = file;
                UpdateFileUploadUI();
            }
            else
            {
                MessageBox.Show("Chỉ hỗ trợ file PDF, DOCX hoặc TXT!",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateFileUploadUI()
        {
            if (!string.IsNullOrEmpty(_uploadedFilePath))
            {
                label1.Text = Path.GetFileName(_uploadedFilePath);
                label2.Text = $"Kích thước: {new FileInfo(_uploadedFilePath).Length / 1024} KB";
            }
        }

        private async void MaterialButton2_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_uploadedFilePath))
            {
                MessageBox.Show("Vui lòng chọn file tài liệu trước!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            materialButton2.Enabled = false;
            materialButton2.Text = "Đang tạo bộ đề...";

            try
            {
                var tokenData = TokenManager.GetTokenData();
                if (tokenData == null)
                {
                    MessageBox.Show("Vui lòng đăng nhập lại!",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    materialButton2.Enabled = true;
                    materialButton2.Text = "Bắt Đầu Tạo Đề Thi";
                    return;
                }

                _documentContent = await _contestService.UploadDocumentAsync(
                    _uploadedFilePath,
                    tokenData.AccessToken);

                string aiModel = materialRadioButton7.Checked ? "gemini" : "deepseek";

                int numMultipleChoice = 0;
                int numEssay = 0;

                if (_hasMultipleChoice && !string.IsNullOrWhiteSpace(materialTextBoxEdit2.Text))
                {
                    int.TryParse(materialTextBoxEdit2.Text, out numMultipleChoice);
                }

                if (_hasEssay && !string.IsNullOrWhiteSpace(materialTextBoxEdit1.Text))
                {
                    int.TryParse(materialTextBoxEdit1.Text, out numEssay);
                }

                if (numMultipleChoice == 0 && numEssay == 0)
                {
                    MessageBox.Show("Vui lòng nhập số lượng câu hỏi cần tạo!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    materialButton2.Enabled = true;
                    materialButton2.Text = "Bắt Đầu Tạo Đề Thi";
                    return;
                }

                if (numMultipleChoice > 0 && !_hasMultipleChoice)
                {
                    MessageBox.Show("Lỗi: Đề thi này không hỗ trợ câu hỏi trắc nghiệm!",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    materialButton2.Enabled = true;
                    materialButton2.Text = "Bắt Đầu Tạo Đề Thi";
                    return;
                }

                if (numEssay > 0 && !_hasEssay)
                {
                    MessageBox.Show("Lỗi: Đề thi này không hỗ trợ câu hỏi tự luận!",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    materialButton2.Enabled = true;
                    materialButton2.Text = "Bắt Đầu Tạo Đề Thi";
                    return;
                }

                var request = new GenerateQuestionsRequest
                {
                    DocumentContent = _documentContent,
                    AiModel = aiModel,
                    NumMultipleChoice = numMultipleChoice,
                    NumEssay = numEssay,
                    ContestId = _contestId
                };

                var response = await _contestService.GenerateQuestionsAsync(
                    request,
                    tokenData.AccessToken);

                if (response.Success && response.Data != null)
                {
                    _currentQuestions = response.Data;
                    DisplayQuestions();
                    UpdateUI();
                    UpdateDungeonNumericLimits();

                    bool saveSuccess = await _contestService.AddQuestionsToContestAsync(
                        _contestId,
                        _currentQuestions.MultipleChoice.ToArray(),
                        _currentQuestions.Essay.ToArray(),
                        tokenData.AccessToken);
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
                materialButton2.Enabled = true;
                materialButton2.Text = "Bắt Đầu Tạo Đề Thi";
            }
        }

        private void DisplayQuestions()
        {
            hopeGroupBox2.Controls.Clear();

            if (_currentQuestions.MultipleChoice.Count == 0 && _currentQuestions.Essay.Count == 0)
            {
                var emptyLabel = new Label
                {
                    Text = "Ngân hàng đề thi đang trống",
                    Font = new Font("Segoe UI", 11F),
                    Location = new Point(300, 250),
                    AutoSize = true
                };
                hopeGroupBox2.Controls.Add(emptyLabel);
                return;
            }

            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(10)
            };

            int yPosition = 10;

            if (_currentQuestions.MultipleChoice.Count > 0)
            {
                var mcLabel = new Label
                {
                    Text = "CÂU HỎI TRẮC NGHIỆM",
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                    Location = new Point(10, yPosition),
                    AutoSize = true
                };
                panel.Controls.Add(mcLabel);
                yPosition += 40;

                for (int i = 0; i < _currentQuestions.MultipleChoice.Count; i++)
                {
                    var q = _currentQuestions.MultipleChoice[i];
                    var questionPanel = CreateMultipleChoiceQuestionPanel(q, i + 1);
                    questionPanel.Location = new Point(10, yPosition);
                    panel.Controls.Add(questionPanel);
                    yPosition += questionPanel.Height + 10;
                }
            }

            if (_currentQuestions.Essay.Count > 0)
            {
                var essayLabel = new Label
                {
                    Text = "CÂU HỎI TỰ LUẬN",
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                    Location = new Point(10, yPosition),
                    AutoSize = true
                };
                panel.Controls.Add(essayLabel);
                yPosition += 40;

                for (int i = 0; i < _currentQuestions.Essay.Count; i++)
                {
                    var q = _currentQuestions.Essay[i];
                    var questionPanel = CreateEssayQuestionPanel(q, i + 1);
                    questionPanel.Location = new Point(10, yPosition);
                    panel.Controls.Add(questionPanel);
                    yPosition += questionPanel.Height + 10;
                }
            }

            hopeGroupBox2.Controls.Add(panel);
        }

        private Panel CreateMultipleChoiceQuestionPanel(MultipleChoiceQuestionModel question, int index)
        {
            var panel = new Panel
            {
                Width = 820,
                Height = 240,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Tag = new { Question = question, Index = index, IsEditing = false }
            };

            var questionLabel = new Label
            {
                Text = $"Câu {index}: {question.Question}",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(10, 10),
                Width = 700,
                Height = 40,
                AutoSize = false,
                Name = "questionLabel"
            };
            panel.Controls.Add(questionLabel);

            var difficultyLabel = new Label
            {
                Text = GetDifficultyText(question.Difficulty),
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                Location = new Point(720, 12),
                Width = 80,
                Height = 22,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = GetDifficultyColor(question.Difficulty),
                ForeColor = Color.White,
                Name = "difficultyLabel"
            };
            panel.Controls.Add(difficultyLabel);

            int optionY = 55;
            for (int i = 0; i < question.Options.Count; i++)
            {
                var opt = question.Options[i];
                var optLabel = new Label
                {
                    Text = $"{(char)('A' + i)}. {opt.Text} {(opt.IsCorrect ? "✓" : "")}",
                    Font = new Font("Segoe UI", 9F),
                    ForeColor = opt.IsCorrect ? Color.Green : Color.Black,
                    Location = new Point(30, optionY),
                    Width = 680,
                    AutoSize = false,
                    Name = $"option{i}"
                };
                panel.Controls.Add(optLabel);
                optionY += 25;
            }

            var editButton = new ReaLTaiizor.Controls.MaterialButton
            {
                Icon = Image.FromFile(Path.Combine(Application.StartupPath, "Assets", "edit-alt.png")),
                Text = "Sửa",
                Location = new Point(540, 180),
                MinimumSize = new Size(120, 36),
                Cursor = Cursors.Hand,
                Name = "editButton",
                Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained,
                UseAccentColor = false,
                Depth = 0,
                HighEmphasis = true
            };
            editButton.Click += (s, e) => EditMultipleChoiceQuestion_Click(panel, question, index);
            panel.Controls.Add(editButton);

            var deleteButton = new ReaLTaiizor.Controls.MaterialButton
            {

                Icon = Image.FromFile(Path.Combine(Application.StartupPath, "Assets", "trash.png")),
                Text = "Xóa",
                Location = new Point(670, 180),
                MinimumSize = new Size(120, 36),
                Cursor = Cursors.Hand,
                Name = "deleteButton",
                Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Outlined,
                UseAccentColor = false,
                Depth = 0,
                HighEmphasis = true
            };
            deleteButton.Click += (s, e) => DeleteMultipleChoiceQuestion_Click(question, index);
            panel.Controls.Add(deleteButton);

            return panel;
        }

        private Panel CreateEssayQuestionPanel(EssayQuestionModel question, int index)
        {
            var panel = new Panel
            {
                Width = 820,
                Height = 150,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            var questionLabel = new Label
            {
                Text = $"Câu {index}: {question.Question}",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(10, 10),
                Width = 740,
                Height = 60,
                AutoSize = false
            };
            panel.Controls.Add(questionLabel);

            var difficultyLabel = new Label
            {
                Text = GetDifficultyText(question.Difficulty),
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                Location = new Point(720, 12),
                Width = 80,
                Height = 22,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = GetDifficultyColor(question.Difficulty),
                ForeColor = Color.White,
                Name = "difficultyLabel"
            };
            panel.Controls.Add(difficultyLabel);

            var criteriaLabel = new Label
            {
                Text = $"Tiêu chí: {question.GradingCriteria}",
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                Location = new Point(10, 75),
                Width = 800,
                Height = 40,
                AutoSize = false
            };
            panel.Controls.Add(criteriaLabel);

            return panel;
        }

        private void MaterialButton1_Click(object? sender, EventArgs e)
        {
            // TODO: Implement thêm câu hỏi thủ công
            MessageBox.Show("Chức năng đang phát triển",
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DungeonNumeric_ValueChanged(object? sender, EventArgs e)
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (_currentQuestions != null)
            {
                label8.Text = $"{_currentQuestions.MultipleChoice.Count} Câu";
                label9.Text = $"{_currentQuestions.Essay.Count} Câu";
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private string GetDifficultyText(string? difficulty)
        {
            if (string.IsNullOrEmpty(difficulty))
                return "Chưa rõ";

            return difficulty.ToLower() switch
            {
                "easy" => "Dễ",
                "medium" => "Trung bình",
                "hard" => "Khó",
                _ => "Chưa rõ"
            };
        }

        private Color GetDifficultyColor(string? difficulty)
        {
            if (string.IsNullOrEmpty(difficulty))
                return Color.Gray;

            return difficulty.ToLower() switch
            {
                "easy" => Color.FromArgb(76, 175, 80),
                "medium" => Color.FromArgb(255, 152, 0),
                "hard" => Color.FromArgb(244, 67, 54),
                _ => Color.Gray
            };
        }

        private async void EditMultipleChoiceQuestion_Click(Panel panel, MultipleChoiceQuestionModel question, int index)
        {
            var editButton = panel.Controls["editButton"] as ReaLTaiizor.Controls.MaterialButton;
            if (editButton == null) return;

            var tag = panel.Tag as dynamic;
            if (tag == null) return;

            bool isEditing = tag.IsEditing;

            if (!isEditing)
            {
                var questionLabel = panel.Controls["questionLabel"] as Label;
                if (questionLabel == null) return;

                var questionText = new TextBox
                {
                    Text = question.Question,
                    Location = questionLabel.Location,
                    Width = questionLabel.Width,
                    Height = 40,
                    Multiline = true,
                    Font = new Font("Segoe UI", 10F),
                    Name = "questionText"
                };
                panel.Controls.Remove(questionLabel);
                panel.Controls.Add(questionText);

                for (int i = 0; i < question.Options.Count; i++)
                {
                    var optLabel = panel.Controls[$"option{i}"] as Label;
                    if (optLabel != null)
                    {
                        var optionPanel = new Panel
                        {
                            Location = optLabel.Location,
                            Width = optLabel.Width,
                            Height = 25,
                            Name = $"optionPanel{i}"
                        };

                        var checkbox = new CheckBox
                        {
                            Checked = question.Options[i].IsCorrect,
                            Location = new Point(0, 2),
                            Width = 20,
                            Name = $"checkbox{i}"
                        };
                        optionPanel.Controls.Add(checkbox);

                        var optText = new TextBox
                        {
                            Text = question.Options[i].Text,
                            Location = new Point(25, 0),
                            Width = optLabel.Width - 30,
                            Font = new Font("Segoe UI", 9F),
                            Name = $"optionText{i}"
                        };
                        optionPanel.Controls.Add(optText);

                        panel.Controls.Remove(optLabel);
                        panel.Controls.Add(optionPanel);
                    }
                }

                editButton.Text = "Lưu";
                editButton.Icon = null;
                editButton.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
                panel.Tag = new { Question = question, Index = index, IsEditing = true };
            }
            else
            {
                var questionText = panel.Controls["questionText"] as TextBox;
                if (questionText != null)
                {
                    question.Question = questionText.Text;

                    for (int i = 0; i < question.Options.Count; i++)
                    {
                        var optionPanel = panel.Controls[$"optionPanel{i}"] as Panel;
                        if (optionPanel != null)
                        {
                            var checkbox = optionPanel.Controls[$"checkbox{i}"] as CheckBox;
                            var optText = optionPanel.Controls[$"optionText{i}"] as TextBox;

                            if (checkbox != null && optText != null)
                            {
                                question.Options[i].Text = optText.Text;
                                question.Options[i].IsCorrect = checkbox.Checked;
                            }
                        }
                    }

                    if (!question.Options.Any(o => o.IsCorrect))
                    {
                        MessageBox.Show("Phải có ít nhất một đáp án đúng!",
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    editButton.Enabled = false;
                    editButton.Text = "Đang lưu...";

                    try
                    {
                        var tokenData = TokenManager.GetTokenData();
                        if (tokenData == null)
                        {
                            MessageBox.Show("Vui lòng đăng nhập lại!",
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (string.IsNullOrEmpty(question.Id))
                        {
                            MessageBox.Show("Lỗi: Không tìm thấy ID câu hỏi!",
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        bool success = await _contestService.UpdateMultipleChoiceQuestionAsync(
                            question.Id,
                            question,
                            tokenData.AccessToken);

                        if (success)
                        {
                            DisplayQuestions();
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật thất bại! Vui lòng thử lại.",
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
                        editButton.Enabled = true;
                        editButton.Text = "Sửa";
                    }
                }
            }
        }

        private async void DeleteMultipleChoiceQuestion_Click(MultipleChoiceQuestionModel question, int index)
        {
            var confirmResult = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa câu hỏi {index}?\n\n{question.Question}",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.No)
                return;

            try
            {
                var tokenData = TokenManager.GetTokenData();
                if (tokenData == null)
                {
                    MessageBox.Show("Vui lòng đăng nhập lại!",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrEmpty(question.Id))
                {
                    MessageBox.Show("Lỗi: Không tìm thấy ID câu hỏi!",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool success = await _contestService.DeleteMultipleChoiceQuestionAsync(
                    question.Id,
                    tokenData.AccessToken);

                if (success)
                {
                    _currentQuestions.MultipleChoice.Remove(question);
                    DisplayQuestions();
                    UpdateUI();
                    UpdateDungeonNumericLimits();

                    MessageBox.Show("Đã xóa câu hỏi thành công!",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Xóa thất bại! Vui lòng thử lại.",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dungeonNumeric1_Click(object sender, EventArgs e)
        {

        }

        private void dungeonNumeric2_Click(object sender, EventArgs e)
        {

        }

        private async void CreateExamQuestion_FormClosing(object? sender, FormClosingEventArgs e)
        {
            try
            {
                var tokenData = TokenManager.GetTokenData();
                if (tokenData == null)
                {
                    return;
                }

                var updateRequest = new UpdateContestRequest();
                
                var requestData = new
                {
                    countMultipleChoiceQuestions = (int)dungeonNumeric1.Value,
                    countEssayQuestions = (int)dungeonNumeric2.Value
                };

                var success = await _contestService.UpdateContestCountQuestionsAsync(
                    _contestId,
                    (int)dungeonNumeric1.Value,
                    (int)dungeonNumeric2.Value,
                    tokenData.AccessToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lưu số lượng câu hỏi: {ex.Message}");
            }
        }
    }
}
