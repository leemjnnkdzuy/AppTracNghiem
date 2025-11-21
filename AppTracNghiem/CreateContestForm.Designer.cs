namespace AppTracNghiem
{
    partial class CreateContestForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateContestForm));
            label1 = new Label();
            materialMaskedTextBox1 = new ReaLTaiizor.Controls.MaterialMaskedTextBox();
            label3 = new Label();
            materialMaskedTextBox2 = new ReaLTaiizor.Controls.MaterialMaskedTextBox();
            label4 = new Label();
            dtpStart = new DateTimePicker();
            dtpEnd = new DateTimePicker();
            label2 = new Label();
            hopeGroupBox1 = new ReaLTaiizor.Controls.HopeGroupBox();
            materialCheckBox2 = new ReaLTaiizor.Controls.MaterialCheckBox();
            materialCheckBox1 = new ReaLTaiizor.Controls.MaterialCheckBox();
            materialButton1 = new ReaLTaiizor.Controls.MaterialButton();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            dungeonNumeric1 = new ReaLTaiizor.Controls.DungeonNumeric();
            materialButton2 = new ReaLTaiizor.Controls.MaterialButton();
            materialButton4 = new ReaLTaiizor.Controls.MaterialButton();
            label11 = new Label();
            dungeonToggleButton1 = new ReaLTaiizor.Controls.DungeonToggleButton();
            dungeonToggleButton2 = new ReaLTaiizor.Controls.DungeonToggleButton();
            dungeonToggleButton3 = new ReaLTaiizor.Controls.DungeonToggleButton();
            dungeonToggleButton4 = new ReaLTaiizor.Controls.DungeonToggleButton();
            label5 = new Label();
            hopeGroupBox2 = new ReaLTaiizor.Controls.HopeGroupBox();
            materialRadioButton2 = new ReaLTaiizor.Controls.MaterialRadioButton();
            materialRadioButton1 = new ReaLTaiizor.Controls.MaterialRadioButton();
            hopeGroupBox1.SuspendLayout();
            hopeGroupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(104, 25);
            label1.TabIndex = 1;
            label1.Text = "Tên Đề Thi";
            label1.Click += label1_Click;
            // 
            // materialMaskedTextBox1
            // 
            materialMaskedTextBox1.AllowPromptAsInput = true;
            materialMaskedTextBox1.AnimateReadOnly = false;
            materialMaskedTextBox1.AsciiOnly = false;
            materialMaskedTextBox1.BackgroundImageLayout = ImageLayout.None;
            materialMaskedTextBox1.BeepOnError = false;
            materialMaskedTextBox1.CutCopyMaskFormat = MaskFormat.IncludeLiterals;
            materialMaskedTextBox1.Depth = 0;
            materialMaskedTextBox1.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialMaskedTextBox1.HidePromptOnLeave = false;
            materialMaskedTextBox1.HideSelection = true;
            materialMaskedTextBox1.Hint = "Tên Đề Thi";
            materialMaskedTextBox1.InsertKeyMode = InsertKeyMode.Default;
            materialMaskedTextBox1.LeadingIcon = null;
            materialMaskedTextBox1.Location = new Point(12, 37);
            materialMaskedTextBox1.Mask = "";
            materialMaskedTextBox1.MaxLength = 32767;
            materialMaskedTextBox1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            materialMaskedTextBox1.Name = "materialMaskedTextBox1";
            materialMaskedTextBox1.PasswordChar = '\0';
            materialMaskedTextBox1.PrefixSuffixText = null;
            materialMaskedTextBox1.PromptChar = '_';
            materialMaskedTextBox1.ReadOnly = false;
            materialMaskedTextBox1.RejectInputOnFirstFailure = false;
            materialMaskedTextBox1.ResetOnPrompt = true;
            materialMaskedTextBox1.ResetOnSpace = true;
            materialMaskedTextBox1.RightToLeft = RightToLeft.No;
            materialMaskedTextBox1.SelectedText = "";
            materialMaskedTextBox1.SelectionLength = 0;
            materialMaskedTextBox1.SelectionStart = 0;
            materialMaskedTextBox1.ShortcutsEnabled = true;
            materialMaskedTextBox1.Size = new Size(250, 48);
            materialMaskedTextBox1.SkipLiterals = true;
            materialMaskedTextBox1.TabIndex = 2;
            materialMaskedTextBox1.TabStop = false;
            materialMaskedTextBox1.TextAlign = HorizontalAlignment.Left;
            materialMaskedTextBox1.TextMaskFormat = MaskFormat.IncludeLiterals;
            materialMaskedTextBox1.TrailingIcon = null;
            materialMaskedTextBox1.UseSystemPasswordChar = false;
            materialMaskedTextBox1.ValidatingType = null;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label3.Location = new Point(12, 176);
            label3.Name = "label3";
            label3.Size = new Size(171, 25);
            label3.TabIndex = 8;
            label3.Text = "Thời Gian Làm Bài";
            label3.Click += label3_Click;
            // 
            // materialMaskedTextBox2
            // 
            materialMaskedTextBox2.AllowPromptAsInput = true;
            materialMaskedTextBox2.AnimateReadOnly = false;
            materialMaskedTextBox2.AsciiOnly = false;
            materialMaskedTextBox2.BackgroundImageLayout = ImageLayout.None;
            materialMaskedTextBox2.BeepOnError = false;
            materialMaskedTextBox2.CutCopyMaskFormat = MaskFormat.IncludeLiterals;
            materialMaskedTextBox2.Depth = 0;
            materialMaskedTextBox2.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialMaskedTextBox2.HidePromptOnLeave = false;
            materialMaskedTextBox2.HideSelection = true;
            materialMaskedTextBox2.Hint = "Thời Gian Làm Bài (phút)";
            materialMaskedTextBox2.InsertKeyMode = InsertKeyMode.Default;
            materialMaskedTextBox2.LeadingIcon = null;
            materialMaskedTextBox2.Location = new Point(12, 202);
            materialMaskedTextBox2.Mask = "";
            materialMaskedTextBox2.MaxLength = 32767;
            materialMaskedTextBox2.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            materialMaskedTextBox2.Name = "materialMaskedTextBox2";
            materialMaskedTextBox2.PasswordChar = '\0';
            materialMaskedTextBox2.PrefixSuffixText = null;
            materialMaskedTextBox2.PromptChar = '_';
            materialMaskedTextBox2.ReadOnly = false;
            materialMaskedTextBox2.RejectInputOnFirstFailure = false;
            materialMaskedTextBox2.ResetOnPrompt = true;
            materialMaskedTextBox2.ResetOnSpace = true;
            materialMaskedTextBox2.RightToLeft = RightToLeft.No;
            materialMaskedTextBox2.SelectedText = "";
            materialMaskedTextBox2.SelectionLength = 0;
            materialMaskedTextBox2.SelectionStart = 0;
            materialMaskedTextBox2.ShortcutsEnabled = true;
            materialMaskedTextBox2.Size = new Size(250, 48);
            materialMaskedTextBox2.SkipLiterals = true;
            materialMaskedTextBox2.TabIndex = 9;
            materialMaskedTextBox2.TabStop = false;
            materialMaskedTextBox2.TextAlign = HorizontalAlignment.Left;
            materialMaskedTextBox2.TextMaskFormat = MaskFormat.IncludeLiterals;
            materialMaskedTextBox2.TrailingIcon = null;
            materialMaskedTextBox2.UseSystemPasswordChar = false;
            materialMaskedTextBox2.ValidatingType = null;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label4.Location = new Point(274, 9);
            label4.Name = "label4";
            label4.Size = new Size(172, 25);
            label4.TabIndex = 10;
            label4.Text = "Thời Gian Bắt Đầu";
            // 
            // dtpStart
            // 
            dtpStart.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpStart.Font = new Font("Segoe UI", 14F);
            dtpStart.Format = DateTimePickerFormat.Custom;
            dtpStart.Location = new Point(274, 37);
            dtpStart.Name = "dtpStart";
            dtpStart.ShowUpDown = true;
            dtpStart.Size = new Size(251, 32);
            dtpStart.TabIndex = 15;
            // 
            // dtpEnd
            // 
            dtpEnd.CalendarFont = new Font("Segoe UI", 12F);
            dtpEnd.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpEnd.Enabled = false;
            dtpEnd.Font = new Font("Segoe UI", 14F);
            dtpEnd.Format = DateTimePickerFormat.Custom;
            dtpEnd.Location = new Point(275, 108);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.ShowUpDown = true;
            dtpEnd.Size = new Size(249, 32);
            dtpEnd.TabIndex = 16;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label2.Location = new Point(12, 88);
            label2.Name = "label2";
            label2.Size = new Size(112, 25);
            label2.TabIndex = 4;
            label2.Text = "Kiểu Đề Thi";
            // 
            // hopeGroupBox1
            // 
            hopeGroupBox1.BorderColor = Color.FromArgb(220, 223, 230);
            hopeGroupBox1.Controls.Add(materialCheckBox2);
            hopeGroupBox1.Controls.Add(materialCheckBox1);
            hopeGroupBox1.Font = new Font("Segoe UI", 12F);
            hopeGroupBox1.ForeColor = Color.FromArgb(48, 49, 51);
            hopeGroupBox1.LineColor = Color.FromArgb(220, 223, 230);
            hopeGroupBox1.Location = new Point(12, 117);
            hopeGroupBox1.Name = "hopeGroupBox1";
            hopeGroupBox1.ShowText = false;
            hopeGroupBox1.Size = new Size(250, 56);
            hopeGroupBox1.TabIndex = 19;
            hopeGroupBox1.TabStop = false;
            hopeGroupBox1.Text = "hopeGroupBox1";
            hopeGroupBox1.ThemeColor = Color.Transparent;
            // 
            // materialCheckBox2
            // 
            materialCheckBox2.AutoSize = true;
            materialCheckBox2.Depth = 0;
            materialCheckBox2.Location = new Point(143, 9);
            materialCheckBox2.Margin = new Padding(0);
            materialCheckBox2.MouseLocation = new Point(-1, -1);
            materialCheckBox2.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialCheckBox2.Name = "materialCheckBox2";
            materialCheckBox2.ReadOnly = false;
            materialCheckBox2.Ripple = true;
            materialCheckBox2.Size = new Size(95, 37);
            materialCheckBox2.TabIndex = 11;
            materialCheckBox2.Text = "Tự Luận";
            materialCheckBox2.UseAccentColor = false;
            materialCheckBox2.UseVisualStyleBackColor = true;
            // 
            // materialCheckBox1
            // 
            materialCheckBox1.AutoSize = true;
            materialCheckBox1.Depth = 0;
            materialCheckBox1.Location = new Point(3, 9);
            materialCheckBox1.Margin = new Padding(0);
            materialCheckBox1.MouseLocation = new Point(-1, -1);
            materialCheckBox1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialCheckBox1.Name = "materialCheckBox1";
            materialCheckBox1.ReadOnly = false;
            materialCheckBox1.Ripple = true;
            materialCheckBox1.Size = new Size(126, 37);
            materialCheckBox1.TabIndex = 10;
            materialCheckBox1.Text = "Trắc Nghiệm";
            materialCheckBox1.UseAccentColor = false;
            materialCheckBox1.UseVisualStyleBackColor = true;
            // 
            // materialButton1
            // 
            materialButton1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            materialButton1.Cursor = Cursors.Hand;
            materialButton1.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            materialButton1.Depth = 0;
            materialButton1.HighEmphasis = true;
            materialButton1.Icon = (Image)resources.GetObject("materialButton1.Icon");
            materialButton1.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            materialButton1.Location = new Point(12, 347);
            materialButton1.Margin = new Padding(4, 6, 4, 6);
            materialButton1.MinimumSize = new Size(250, 36);
            materialButton1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialButton1.Name = "materialButton1";
            materialButton1.NoAccentTextColor = Color.Empty;
            materialButton1.Size = new Size(250, 36);
            materialButton1.TabIndex = 22;
            materialButton1.Text = "Tạo Danh Sách";
            materialButton1.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            materialButton1.UseAccentColor = false;
            materialButton1.UseVisualStyleBackColor = true;
            materialButton1.Click += materialButton1_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F);
            label7.Location = new Point(275, 176);
            label7.Name = "label7";
            label7.Size = new Size(133, 21);
            label7.TabIndex = 23;
            label7.Text = "Hiển Thị Điểm Thi";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 12F);
            label8.Location = new Point(275, 209);
            label8.Name = "label8";
            label8.Size = new Size(122, 21);
            label8.TabIndex = 25;
            label8.Text = "Hiển Thị Đáp Án";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.Location = new Point(275, 242);
            label9.Name = "label9";
            label9.Size = new Size(126, 21);
            label9.TabIndex = 27;
            label9.Text = "Khóa Màng Hình";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label10.Location = new Point(274, 282);
            label10.Name = "label10";
            label10.Size = new Size(128, 25);
            label10.TabIndex = 29;
            label10.Text = "Lượt Làm Bài";
            // 
            // dungeonNumeric1
            // 
            dungeonNumeric1.BackColor = Color.Transparent;
            dungeonNumeric1.BackColorA = Color.FromArgb(246, 246, 246);
            dungeonNumeric1.BackColorB = Color.FromArgb(254, 254, 254);
            dungeonNumeric1.BorderColor = Color.FromArgb(180, 180, 180);
            dungeonNumeric1.ButtonForeColorA = Color.FromArgb(75, 75, 75);
            dungeonNumeric1.ButtonForeColorB = Color.FromArgb(75, 75, 75);
            dungeonNumeric1.Font = new Font("Tahoma", 11F);
            dungeonNumeric1.ForeColor = Color.FromArgb(76, 76, 76);
            dungeonNumeric1.Location = new Point(274, 310);
            dungeonNumeric1.Maximum = 1000000L;
            dungeonNumeric1.Minimum = 1L;
            dungeonNumeric1.MinimumSize = new Size(93, 28);
            dungeonNumeric1.Name = "dungeonNumeric1";
            dungeonNumeric1.Size = new Size(250, 28);
            dungeonNumeric1.TabIndex = 31;
            dungeonNumeric1.Text = "dungeonNumeric1";
            dungeonNumeric1.TextAlignment = ReaLTaiizor.Controls.DungeonNumeric._TextAlignment.Near;
            dungeonNumeric1.Value = 1L;
            // 
            // materialButton2
            // 
            materialButton2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            materialButton2.Cursor = Cursors.Hand;
            materialButton2.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            materialButton2.Depth = 0;
            materialButton2.HighEmphasis = true;
            materialButton2.Icon = (Image)resources.GetObject("materialButton2.Icon");
            materialButton2.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            materialButton2.Location = new Point(275, 347);
            materialButton2.Margin = new Padding(4, 6, 4, 6);
            materialButton2.MinimumSize = new Size(250, 36);
            materialButton2.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialButton2.Name = "materialButton2";
            materialButton2.NoAccentTextColor = Color.Empty;
            materialButton2.Size = new Size(250, 36);
            materialButton2.TabIndex = 32;
            materialButton2.Text = "Tạo Bộ Câu Hỏi";
            materialButton2.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            materialButton2.UseAccentColor = false;
            materialButton2.UseVisualStyleBackColor = true;
            materialButton2.Click += materialButton2_Click;
            // 
            // materialButton4
            // 
            materialButton4.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            materialButton4.Cursor = Cursors.Hand;
            materialButton4.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            materialButton4.Depth = 0;
            materialButton4.Font = new Font("Segoe UI", 14F);
            materialButton4.HighEmphasis = true;
            materialButton4.Icon = (Image)resources.GetObject("materialButton4.Icon");
            materialButton4.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            materialButton4.Location = new Point(12, 395);
            materialButton4.Margin = new Padding(4, 6, 4, 6);
            materialButton4.MinimumSize = new Size(513, 72);
            materialButton4.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialButton4.Name = "materialButton4";
            materialButton4.NoAccentTextColor = Color.Empty;
            materialButton4.Size = new Size(513, 72);
            materialButton4.TabIndex = 34;
            materialButton4.Text = "Lưu ";
            materialButton4.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            materialButton4.UseAccentColor = true;
            materialButton4.UseVisualStyleBackColor = true;
            materialButton4.Click += materialButton4_Click_1;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 12F);
            label11.Location = new Point(274, 81);
            label11.Name = "label11";
            label11.Size = new Size(120, 21);
            label11.TabIndex = 18;
            label11.Text = "Không Giới Hạn";
            // 
            // dungeonToggleButton1
            // 
            dungeonToggleButton1.Location = new Point(445, 75);
            dungeonToggleButton1.Name = "dungeonToggleButton1";
            dungeonToggleButton1.Size = new Size(79, 27);
            dungeonToggleButton1.TabIndex = 35;
            dungeonToggleButton1.Text = "dungeonToggleButton1";
            dungeonToggleButton1.Toggled = false;
            dungeonToggleButton1.ToggledBackColorA = Color.FromArgb(253, 253, 253);
            dungeonToggleButton1.ToggledBackColorB = Color.FromArgb(240, 238, 237);
            dungeonToggleButton1.ToggledBorderColorA = Color.FromArgb(185, 89, 55);
            dungeonToggleButton1.ToggledBorderColorB = Color.FromArgb(185, 89, 55);
            dungeonToggleButton1.ToggledBorderColorC = Color.FromArgb(181, 181, 181);
            dungeonToggleButton1.ToggledBorderColorD = Color.FromArgb(181, 181, 181);
            dungeonToggleButton1.ToggledColorA = Color.FromArgb(231, 108, 58);
            dungeonToggleButton1.ToggledColorB = Color.FromArgb(236, 113, 63);
            dungeonToggleButton1.ToggledColorC = Color.FromArgb(208, 208, 208);
            dungeonToggleButton1.ToggledColorD = Color.FromArgb(226, 226, 226);
            dungeonToggleButton1.ToggledIOColorA = Color.WhiteSmoke;
            dungeonToggleButton1.ToggledIOColorB = Color.DimGray;
            dungeonToggleButton1.ToggledOnOffColorA = Color.WhiteSmoke;
            dungeonToggleButton1.ToggledOnOffColorB = Color.DimGray;
            dungeonToggleButton1.ToggledYesNoColorA = Color.WhiteSmoke;
            dungeonToggleButton1.ToggledYesNoColorB = Color.DimGray;
            dungeonToggleButton1.Type = ReaLTaiizor.Controls.DungeonToggleButton._Type.OnOff;
            // 
            // dungeonToggleButton2
            // 
            dungeonToggleButton2.Location = new Point(446, 170);
            dungeonToggleButton2.Name = "dungeonToggleButton2";
            dungeonToggleButton2.Size = new Size(79, 27);
            dungeonToggleButton2.TabIndex = 37;
            dungeonToggleButton2.Text = "dungeonToggleButton2";
            dungeonToggleButton2.Toggled = false;
            dungeonToggleButton2.ToggledBackColorA = Color.FromArgb(253, 253, 253);
            dungeonToggleButton2.ToggledBackColorB = Color.FromArgb(240, 238, 237);
            dungeonToggleButton2.ToggledBorderColorA = Color.FromArgb(185, 89, 55);
            dungeonToggleButton2.ToggledBorderColorB = Color.FromArgb(185, 89, 55);
            dungeonToggleButton2.ToggledBorderColorC = Color.FromArgb(181, 181, 181);
            dungeonToggleButton2.ToggledBorderColorD = Color.FromArgb(181, 181, 181);
            dungeonToggleButton2.ToggledColorA = Color.FromArgb(231, 108, 58);
            dungeonToggleButton2.ToggledColorB = Color.FromArgb(236, 113, 63);
            dungeonToggleButton2.ToggledColorC = Color.FromArgb(208, 208, 208);
            dungeonToggleButton2.ToggledColorD = Color.FromArgb(226, 226, 226);
            dungeonToggleButton2.ToggledIOColorA = Color.WhiteSmoke;
            dungeonToggleButton2.ToggledIOColorB = Color.DimGray;
            dungeonToggleButton2.ToggledOnOffColorA = Color.WhiteSmoke;
            dungeonToggleButton2.ToggledOnOffColorB = Color.DimGray;
            dungeonToggleButton2.ToggledYesNoColorA = Color.WhiteSmoke;
            dungeonToggleButton2.ToggledYesNoColorB = Color.DimGray;
            dungeonToggleButton2.Type = ReaLTaiizor.Controls.DungeonToggleButton._Type.OnOff;
            // 
            // dungeonToggleButton3
            // 
            dungeonToggleButton3.Location = new Point(446, 236);
            dungeonToggleButton3.Name = "dungeonToggleButton3";
            dungeonToggleButton3.Size = new Size(79, 27);
            dungeonToggleButton3.TabIndex = 38;
            dungeonToggleButton3.Text = "dungeonToggleButton3";
            dungeonToggleButton3.Toggled = false;
            dungeonToggleButton3.ToggledBackColorA = Color.FromArgb(253, 253, 253);
            dungeonToggleButton3.ToggledBackColorB = Color.FromArgb(240, 238, 237);
            dungeonToggleButton3.ToggledBorderColorA = Color.FromArgb(185, 89, 55);
            dungeonToggleButton3.ToggledBorderColorB = Color.FromArgb(185, 89, 55);
            dungeonToggleButton3.ToggledBorderColorC = Color.FromArgb(181, 181, 181);
            dungeonToggleButton3.ToggledBorderColorD = Color.FromArgb(181, 181, 181);
            dungeonToggleButton3.ToggledColorA = Color.FromArgb(231, 108, 58);
            dungeonToggleButton3.ToggledColorB = Color.FromArgb(236, 113, 63);
            dungeonToggleButton3.ToggledColorC = Color.FromArgb(208, 208, 208);
            dungeonToggleButton3.ToggledColorD = Color.FromArgb(226, 226, 226);
            dungeonToggleButton3.ToggledIOColorA = Color.WhiteSmoke;
            dungeonToggleButton3.ToggledIOColorB = Color.DimGray;
            dungeonToggleButton3.ToggledOnOffColorA = Color.WhiteSmoke;
            dungeonToggleButton3.ToggledOnOffColorB = Color.DimGray;
            dungeonToggleButton3.ToggledYesNoColorA = Color.WhiteSmoke;
            dungeonToggleButton3.ToggledYesNoColorB = Color.DimGray;
            dungeonToggleButton3.Type = ReaLTaiizor.Controls.DungeonToggleButton._Type.OnOff;
            // 
            // dungeonToggleButton4
            // 
            dungeonToggleButton4.Location = new Point(446, 203);
            dungeonToggleButton4.Name = "dungeonToggleButton4";
            dungeonToggleButton4.Size = new Size(79, 27);
            dungeonToggleButton4.TabIndex = 39;
            dungeonToggleButton4.Text = "dungeonToggleButton4";
            dungeonToggleButton4.Toggled = false;
            dungeonToggleButton4.ToggledBackColorA = Color.FromArgb(253, 253, 253);
            dungeonToggleButton4.ToggledBackColorB = Color.FromArgb(240, 238, 237);
            dungeonToggleButton4.ToggledBorderColorA = Color.FromArgb(185, 89, 55);
            dungeonToggleButton4.ToggledBorderColorB = Color.FromArgb(185, 89, 55);
            dungeonToggleButton4.ToggledBorderColorC = Color.FromArgb(181, 181, 181);
            dungeonToggleButton4.ToggledBorderColorD = Color.FromArgb(181, 181, 181);
            dungeonToggleButton4.ToggledColorA = Color.FromArgb(231, 108, 58);
            dungeonToggleButton4.ToggledColorB = Color.FromArgb(236, 113, 63);
            dungeonToggleButton4.ToggledColorC = Color.FromArgb(208, 208, 208);
            dungeonToggleButton4.ToggledColorD = Color.FromArgb(226, 226, 226);
            dungeonToggleButton4.ToggledIOColorA = Color.WhiteSmoke;
            dungeonToggleButton4.ToggledIOColorB = Color.DimGray;
            dungeonToggleButton4.ToggledOnOffColorA = Color.WhiteSmoke;
            dungeonToggleButton4.ToggledOnOffColorB = Color.DimGray;
            dungeonToggleButton4.ToggledYesNoColorA = Color.WhiteSmoke;
            dungeonToggleButton4.ToggledYesNoColorB = Color.DimGray;
            dungeonToggleButton4.Type = ReaLTaiizor.Controls.DungeonToggleButton._Type.OnOff;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label5.Location = new Point(12, 253);
            label5.Name = "label5";
            label5.Size = new Size(106, 25);
            label5.TabIndex = 40;
            label5.Text = "Trạng Thái";
            // 
            // hopeGroupBox2
            // 
            hopeGroupBox2.BorderColor = Color.FromArgb(220, 223, 230);
            hopeGroupBox2.Controls.Add(materialRadioButton2);
            hopeGroupBox2.Controls.Add(materialRadioButton1);
            hopeGroupBox2.Font = new Font("Segoe UI", 12F);
            hopeGroupBox2.ForeColor = Color.FromArgb(48, 49, 51);
            hopeGroupBox2.LineColor = Color.FromArgb(220, 223, 230);
            hopeGroupBox2.Location = new Point(12, 282);
            hopeGroupBox2.Name = "hopeGroupBox2";
            hopeGroupBox2.ShowText = false;
            hopeGroupBox2.Size = new Size(250, 56);
            hopeGroupBox2.TabIndex = 41;
            hopeGroupBox2.TabStop = false;
            hopeGroupBox2.Text = "hopeGroupBox2";
            hopeGroupBox2.ThemeColor = Color.Transparent;
            // 
            // materialRadioButton2
            // 
            materialRadioButton2.AutoSize = true;
            materialRadioButton2.Depth = 0;
            materialRadioButton2.Location = new Point(3, 10);
            materialRadioButton2.Margin = new Padding(0);
            materialRadioButton2.MouseLocation = new Point(-1, -1);
            materialRadioButton2.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialRadioButton2.Name = "materialRadioButton2";
            materialRadioButton2.Ripple = true;
            materialRadioButton2.Size = new Size(99, 37);
            materialRadioButton2.TabIndex = 13;
            materialRadioButton2.TabStop = true;
            materialRadioButton2.Text = "Riêng Tư";
            materialRadioButton2.UseAccentColor = false;
            materialRadioButton2.UseVisualStyleBackColor = true;
            // 
            // materialRadioButton1
            // 
            materialRadioButton1.AutoSize = true;
            materialRadioButton1.Depth = 0;
            materialRadioButton1.Location = new Point(130, 10);
            materialRadioButton1.Margin = new Padding(0);
            materialRadioButton1.MouseLocation = new Point(-1, -1);
            materialRadioButton1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialRadioButton1.Name = "materialRadioButton1";
            materialRadioButton1.Ripple = true;
            materialRadioButton1.Size = new Size(108, 37);
            materialRadioButton1.TabIndex = 12;
            materialRadioButton1.TabStop = true;
            materialRadioButton1.Text = "Công Khai";
            materialRadioButton1.UseAccentColor = false;
            materialRadioButton1.UseVisualStyleBackColor = true;
            // 
            // CreateContestForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(535, 480);
            Controls.Add(label5);
            Controls.Add(hopeGroupBox2);
            Controls.Add(dungeonToggleButton4);
            Controls.Add(dungeonToggleButton3);
            Controls.Add(dungeonToggleButton2);
            Controls.Add(dungeonToggleButton1);
            Controls.Add(dtpEnd);
            Controls.Add(label11);
            Controls.Add(materialButton4);
            Controls.Add(materialButton2);
            Controls.Add(dungeonNumeric1);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(materialButton1);
            Controls.Add(label2);
            Controls.Add(hopeGroupBox1);
            Controls.Add(dtpStart);
            Controls.Add(label4);
            Controls.Add(materialMaskedTextBox2);
            Controls.Add(label3);
            Controls.Add(materialMaskedTextBox1);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CreateContestForm";
            Text = "Thông Tin Cơ Bản Cho Đề Thi";
            TransparencyKey = SystemColors.ControlLight;
            Load += CreateContestForm_Load;
            hopeGroupBox1.ResumeLayout(false);
            hopeGroupBox1.PerformLayout();
            hopeGroupBox2.ResumeLayout(false);
            hopeGroupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private ReaLTaiizor.Controls.MaterialMaskedTextBox materialMaskedTextBox1;
        private Label label3;
        private ReaLTaiizor.Controls.MaterialMaskedTextBox materialMaskedTextBox2;
        private Label label4;
        private DateTimePicker dtpStart;
        private DateTimePicker dtpEnd;
        private Label label2;
        private ReaLTaiizor.Controls.HopeGroupBox hopeGroupBox1;
        private ReaLTaiizor.Controls.MaterialButton materialButton1;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private ReaLTaiizor.Controls.DungeonNumeric dungeonNumeric1;
        private ReaLTaiizor.Controls.MaterialButton materialButton2;
        private ReaLTaiizor.Controls.MaterialButton materialButton4;
        private Label label11;
        private ReaLTaiizor.Controls.DungeonToggleButton dungeonToggleButton1;
        private ReaLTaiizor.Controls.DungeonToggleButton dungeonToggleButton2;
        private ReaLTaiizor.Controls.DungeonToggleButton dungeonToggleButton3;
        private ReaLTaiizor.Controls.DungeonToggleButton dungeonToggleButton4;
        private ReaLTaiizor.Controls.MaterialCheckBox materialCheckBox1;
        private ReaLTaiizor.Controls.MaterialCheckBox materialCheckBox2;
        private Label label5;
        private ReaLTaiizor.Controls.HopeGroupBox hopeGroupBox2;
        private ReaLTaiizor.Controls.MaterialRadioButton materialRadioButton1;
        private ReaLTaiizor.Controls.MaterialRadioButton materialRadioButton2;
    }
}
