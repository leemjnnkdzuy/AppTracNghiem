namespace AppTracNghiem
{
    partial class AddMemberForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddMemberForm));
            materialTextBoxEdit1 = new ReaLTaiizor.Controls.MaterialTextBoxEdit();
            materialButton1 = new ReaLTaiizor.Controls.MaterialButton();
            materialButton2 = new ReaLTaiizor.Controls.MaterialButton();
            lblFullName = new ReaLTaiizor.Controls.MaterialLabel();
            lblUsername = new ReaLTaiizor.Controls.MaterialLabel();
            lblEmail = new ReaLTaiizor.Controls.MaterialLabel();
            lblPhone = new ReaLTaiizor.Controls.MaterialLabel();
            lblGender = new ReaLTaiizor.Controls.MaterialLabel();
            lblClass = new ReaLTaiizor.Controls.MaterialLabel();
            lblProgram = new ReaLTaiizor.Controls.MaterialLabel();
            lblBirthDate = new ReaLTaiizor.Controls.MaterialLabel();
            SuspendLayout();
            // 
            // materialTextBoxEdit1
            // 
            materialTextBoxEdit1.AnimateReadOnly = false;
            materialTextBoxEdit1.AutoCompleteMode = AutoCompleteMode.None;
            materialTextBoxEdit1.AutoCompleteSource = AutoCompleteSource.None;
            materialTextBoxEdit1.BackgroundImageLayout = ImageLayout.None;
            materialTextBoxEdit1.CharacterCasing = CharacterCasing.Normal;
            materialTextBoxEdit1.Depth = 0;
            materialTextBoxEdit1.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialTextBoxEdit1.HideSelection = true;
            materialTextBoxEdit1.Hint = "Nhập MSSV";
            materialTextBoxEdit1.LeadingIcon = null;
            materialTextBoxEdit1.Location = new Point(12, 12);
            materialTextBoxEdit1.MaxLength = 32767;
            materialTextBoxEdit1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            materialTextBoxEdit1.Name = "materialTextBoxEdit1";
            materialTextBoxEdit1.PasswordChar = '\0';
            materialTextBoxEdit1.PrefixSuffixText = null;
            materialTextBoxEdit1.ReadOnly = false;
            materialTextBoxEdit1.RightToLeft = RightToLeft.No;
            materialTextBoxEdit1.SelectedText = "";
            materialTextBoxEdit1.SelectionLength = 0;
            materialTextBoxEdit1.SelectionStart = 0;
            materialTextBoxEdit1.ShortcutsEnabled = true;
            materialTextBoxEdit1.Size = new Size(306, 48);
            materialTextBoxEdit1.TabIndex = 0;
            materialTextBoxEdit1.TabStop = false;
            materialTextBoxEdit1.TextAlign = HorizontalAlignment.Left;
            materialTextBoxEdit1.TrailingIcon = null;
            materialTextBoxEdit1.UseSystemPasswordChar = false;
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
            materialButton1.Location = new Point(325, 12);
            materialButton1.Margin = new Padding(4, 6, 4, 6);
            materialButton1.MinimumSize = new Size(0, 48);
            materialButton1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialButton1.Name = "materialButton1";
            materialButton1.NoAccentTextColor = Color.Empty;
            materialButton1.Size = new Size(114, 48);
            materialButton1.TabIndex = 2;
            materialButton1.Text = "Tìm Kiếm";
            materialButton1.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            materialButton1.UseAccentColor = false;
            materialButton1.UseVisualStyleBackColor = true;
            materialButton1.Click += materialButton1_Click;
            // 
            // materialButton2
            // 
            materialButton2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            materialButton2.Cursor = Cursors.Hand;
            materialButton2.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            materialButton2.Depth = 0;
            materialButton2.Enabled = false;
            materialButton2.HighEmphasis = true;
            materialButton2.Icon = (Image)resources.GetObject("materialButton2.Icon");
            materialButton2.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            materialButton2.Location = new Point(447, 12);
            materialButton2.Margin = new Padding(4, 6, 4, 6);
            materialButton2.MinimumSize = new Size(0, 48);
            materialButton2.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialButton2.Name = "materialButton2";
            materialButton2.NoAccentTextColor = Color.Empty;
            materialButton2.Size = new Size(90, 48);
            materialButton2.TabIndex = 3;
            materialButton2.Text = "Thêm";
            materialButton2.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            materialButton2.UseAccentColor = false;
            materialButton2.UseVisualStyleBackColor = true;
            materialButton2.Click += btnAdd_Click;
            // 
            // lblFullName
            // 
            lblFullName.AutoSize = true;
            lblFullName.Depth = 0;
            lblFullName.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblFullName.Location = new Point(12, 70);
            lblFullName.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(1, 0);
            lblFullName.TabIndex = 4;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Depth = 0;
            lblUsername.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblUsername.Location = new Point(12, 95);
            lblUsername.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(1, 0);
            lblUsername.TabIndex = 5;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Depth = 0;
            lblEmail.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblEmail.Location = new Point(12, 120);
            lblEmail.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(1, 0);
            lblEmail.TabIndex = 6;
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.Depth = 0;
            lblPhone.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblPhone.Location = new Point(12, 145);
            lblPhone.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(1, 0);
            lblPhone.TabIndex = 7;
            // 
            // lblGender
            // 
            lblGender.AutoSize = true;
            lblGender.Depth = 0;
            lblGender.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblGender.Location = new Point(12, 170);
            lblGender.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            lblGender.Name = "lblGender";
            lblGender.Size = new Size(1, 0);
            lblGender.TabIndex = 8;
            // 
            // lblClass
            // 
            lblClass.AutoSize = true;
            lblClass.Depth = 0;
            lblClass.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblClass.Location = new Point(12, 195);
            lblClass.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            lblClass.Name = "lblClass";
            lblClass.Size = new Size(1, 0);
            lblClass.TabIndex = 9;
            // 
            // lblProgram
            // 
            lblProgram.AutoSize = true;
            lblProgram.Depth = 0;
            lblProgram.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblProgram.Location = new Point(12, 220);
            lblProgram.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            lblProgram.Name = "lblProgram";
            lblProgram.Size = new Size(1, 0);
            lblProgram.TabIndex = 10;
            // 
            // lblBirthDate
            // 
            lblBirthDate.AutoSize = true;
            lblBirthDate.Depth = 0;
            lblBirthDate.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblBirthDate.Location = new Point(12, 245);
            lblBirthDate.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            lblBirthDate.Name = "lblBirthDate";
            lblBirthDate.Size = new Size(1, 0);
            lblBirthDate.TabIndex = 11;
            // 
            // AddMemberForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(550, 287);
            Controls.Add(lblBirthDate);
            Controls.Add(lblProgram);
            Controls.Add(lblClass);
            Controls.Add(lblGender);
            Controls.Add(lblPhone);
            Controls.Add(lblEmail);
            Controls.Add(lblUsername);
            Controls.Add(lblFullName);
            Controls.Add(materialButton2);
            Controls.Add(materialButton1);
            Controls.Add(materialTextBoxEdit1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddMemberForm";
            Text = "Thêm Thành Viên";
            Load += AddMemberForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ReaLTaiizor.Controls.MaterialTextBoxEdit materialTextBoxEdit1;
        private ReaLTaiizor.Controls.MaterialButton materialButton1;
        private ReaLTaiizor.Controls.MaterialButton materialButton2;
        private ReaLTaiizor.Controls.MaterialLabel lblFullName;
        private ReaLTaiizor.Controls.MaterialLabel lblUsername;
        private ReaLTaiizor.Controls.MaterialLabel lblEmail;
        private ReaLTaiizor.Controls.MaterialLabel lblPhone;
        private ReaLTaiizor.Controls.MaterialLabel lblGender;
        private ReaLTaiizor.Controls.MaterialLabel lblClass;
        private ReaLTaiizor.Controls.MaterialLabel lblProgram;
        private ReaLTaiizor.Controls.MaterialLabel lblBirthDate;
    }
}