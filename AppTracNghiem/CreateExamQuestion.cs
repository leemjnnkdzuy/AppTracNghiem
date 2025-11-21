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

namespace AppTracNghiem
{
    public partial class CreateExamQuestion : Form
    {
        private ContestService _contestService;
        private string _uploadedFilePath;
        private string _documentContent;
        private GeneratedQuestionsData _currentQuestions;
        private string _contestId;
        private bool _hasMultipleChoice;
        private bool _hasEssay;

        public CreateExamQuestion(string contestId, bool hasMultipleChoice = true, bool hasEssay = true)
        {
            InitializeComponent();
            _contestService = new ContestService();
            _contestId = contestId;
            _currentQuestions = new GeneratedQuestionsData();
            _hasMultipleChoice = hasMultipleChoice;
            _hasEssay = hasEssay;
            
            InitializeForm();
        }

        private void InitializeForm()
        {
            hopeGroupBox3.AllowDrop = true;
            hopeGroupBox3.DragEnter += HopeGroupBox3_DragEnter;
            hopeGroupBox3.DragDrop += HopeGroupBox3_DragDrop;
            hopeGroupBox3.Click += HopeGroupBox3_Click;

            materialButton2.Click += MaterialButton2_Click;
            materialButton1.Click += MaterialButton1_Click;
            materialButton3.Click += MaterialButton3_Click;
            
            dungeonNumeric1.TextChanged += DungeonNumeric_ValueChanged;
            dungeonNumeric2.TextChanged += DungeonNumeric_ValueChanged;

            materialRadioButton8.Checked = true;

            dungeonNumeric1.Enabled = _hasMultipleChoice;
            label5.Enabled = _hasMultipleChoice;
            
            dungeonNumeric2.Enabled = _hasEssay;
            label10.Enabled = _hasEssay;
            
            if (!_hasMultipleChoice)
            {
                dungeonNumeric1.Value = 0;
                dungeonNumeric1.Minimum = 0;
            }
            if (!_hasEssay)
            {
                dungeonNumeric2.Value = 0;
                dungeonNumeric2.Minimum = 0;
            }

            UpdateUI();
        }

        private void HopeGroupBox3_Click(object sender, EventArgs e)
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

        private void HopeGroupBox3_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void HopeGroupBox3_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
            {
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
        }

        private void UpdateFileUploadUI()
        {
            if (!string.IsNullOrEmpty(_uploadedFilePath))
            {
                label1.Text = Path.GetFileName(_uploadedFilePath);
                label2.Text = $"Kích thước: {new FileInfo(_uploadedFilePath).Length / 1024} KB";
            }
        }

        private async void MaterialButton2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_uploadedFilePath))
            {
                MessageBox.Show("Vui lòng chọn file tài liệu trước!", 
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dungeonNumeric1.Value == 0 && dungeonNumeric2.Value == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất 1 câu hỏi!", 
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            materialButton2.Enabled = false;
            materialButton2.Text = "Đang tạo...";

            try
            {
                var tokenData = TokenManager.GetTokenData();
                if (tokenData == null)
                {
                    MessageBox.Show("Vui lòng đăng nhập lại!", 
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _documentContent = await _contestService.UploadDocumentAsync(
                    _uploadedFilePath, 
                    tokenData.AccessToken);

                string aiModel = materialRadioButton7.Checked ? "gemini" : "deepseek";

                var request = new GenerateQuestionsRequest
                {
                    DocumentContent = _documentContent,
                    AiModel = aiModel,
                    NumMultipleChoice = (int)dungeonNumeric1.Value,
                    NumEssay = (int)dungeonNumeric2.Value,
                    ContestId = _contestId
                };

                var response = await _contestService.GenerateQuestionsAsync(
                    request, 
                    tokenData.AccessToken);

                if (response.Success)
                {
                    _currentQuestions = response.Data;
                    DisplayQuestions();
                    
                    MessageBox.Show($"Đã tạo thành công {response.Data.MultipleChoice.Count} câu trắc nghiệm và {response.Data.Essay.Count} câu tự luận!", 
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                Height = 200,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            var questionLabel = new Label
            {
                Text = $"Câu {index}: {question.Question}",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(10, 10),
                Width = 800,
                Height = 40,
                AutoSize = false
            };
            panel.Controls.Add(questionLabel);

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
                    Width = 780,
                    AutoSize = false
                };
                panel.Controls.Add(optLabel);
                optionY += 25;
            }

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
                Width = 800,
                Height = 60,
                AutoSize = false
            };
            panel.Controls.Add(questionLabel);

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

        private void MaterialButton1_Click(object sender, EventArgs e)
        {
            // TODO: Implement thêm câu hỏi thủ công
            MessageBox.Show("Chức năng đang phát triển", 
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MaterialButton3_Click(object sender, EventArgs e)
        {
            if (_currentQuestions.MultipleChoice.Count == 0 && _currentQuestions.Essay.Count == 0)
            {
                MessageBox.Show("Chưa có câu hỏi nào để lưu!", 
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"Hoàn tất tạo đề với {_currentQuestions.MultipleChoice.Count + _currentQuestions.Essay.Count} câu hỏi?\n\n" +
                "Đề thi sẽ được lưu ở trạng thái Riêng tư (Private) và bạn có thể công khai sau.",
                "Xác nhận", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                MessageBox.Show(
                    $"Đã hoàn tất tạo đề thi với {_currentQuestions.MultipleChoice.Count + _currentQuestions.Essay.Count} câu hỏi!\n\n" +
                    "Đề thi đã được lưu ở trạng thái Riêng tư.", 
                    "Thành công", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Information);
                
                this.Close();
            }
        }

        private void DungeonNumeric_ValueChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            label8.Text = $"{_currentQuestions.MultipleChoice.Count} Câu";
            label9.Text = $"{_currentQuestions.Essay.Count} Câu";
        }

        private void materialRadioButton7_CheckedChanged(object sender, EventArgs e)
        {
            // Gemini selected
        }
    }
}
