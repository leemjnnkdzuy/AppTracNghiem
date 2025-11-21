using ReaLTaiizor.Controls;
using ReaLTaiizor.Helper;

namespace AppTracNghiem
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            pictureBox1 = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            txtMssv = new MaterialTextBoxEdit();
            txtPassword = new MaterialTextBoxEdit();
            label3 = new Label();
            label4 = new Label();
            btnLogin = new MaterialButton();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.Image = Properties.Resources.image_logo;
            pictureBox1.Location = new Point(89, 98);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(243, 247);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(89, 393);
            label1.Name = "label1";
            label1.Size = new Size(243, 15);
            label1.TabIndex = 1;
            label1.Text = "Vui lòng đăng nhập bằng tài khoản sinh viên";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ControlText;
            label2.Location = new Point(122, 348);
            label2.Name = "label2";
            label2.Size = new Size(190, 45);
            label2.TabIndex = 2;
            label2.Text = "Đăng Nhập";
            label2.Click += label2_Click;
            // 
            // txtMssv
            // 
            txtMssv.AnimateReadOnly = false;
            txtMssv.AutoCompleteMode = AutoCompleteMode.None;
            txtMssv.AutoCompleteSource = AutoCompleteSource.None;
            txtMssv.BackgroundImageLayout = ImageLayout.None;
            txtMssv.CharacterCasing = CharacterCasing.Normal;
            txtMssv.Depth = 0;
            txtMssv.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            txtMssv.HideSelection = true;
            txtMssv.Hint = "MSSV";
            txtMssv.LeadingIcon = null;
            txtMssv.Location = new Point(446, 171);
            txtMssv.MaxLength = 50;
            txtMssv.MouseState = MaterialDrawHelper.MaterialMouseState.OUT;
            txtMssv.Name = "txtMssv";
            txtMssv.PasswordChar = '\0';
            txtMssv.PrefixSuffixText = null;
            txtMssv.ReadOnly = false;
            txtMssv.RightToLeft = RightToLeft.No;
            txtMssv.SelectedText = "";
            txtMssv.SelectionLength = 0;
            txtMssv.SelectionStart = 0;
            txtMssv.ShortcutsEnabled = true;
            txtMssv.Size = new Size(365, 48);
            txtMssv.TabIndex = 3;
            txtMssv.TabStop = false;
            txtMssv.TextAlign = HorizontalAlignment.Left;
            txtMssv.TrailingIcon = null;
            txtMssv.UseSystemPasswordChar = false;
            // 
            // txtPassword
            // 
            txtPassword.AnimateReadOnly = false;
            txtPassword.AutoCompleteMode = AutoCompleteMode.None;
            txtPassword.AutoCompleteSource = AutoCompleteSource.None;
            txtPassword.BackgroundImageLayout = ImageLayout.None;
            txtPassword.CharacterCasing = CharacterCasing.Normal;
            txtPassword.Depth = 0;
            txtPassword.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            txtPassword.HideSelection = true;
            txtPassword.Hint = "Mật khẩu";
            txtPassword.LeadingIcon = null;
            txtPassword.Location = new Point(446, 250);
            txtPassword.MaxLength = 50;
            txtPassword.MouseState = MaterialDrawHelper.MaterialMouseState.OUT;
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '●';
            txtPassword.PrefixSuffixText = null;
            txtPassword.ReadOnly = false;
            txtPassword.RightToLeft = RightToLeft.No;
            txtPassword.SelectedText = "";
            txtPassword.SelectionLength = 0;
            txtPassword.SelectionStart = 0;
            txtPassword.ShortcutsEnabled = true;
            txtPassword.Size = new Size(365, 48);
            txtPassword.TabIndex = 4;
            txtPassword.TabStop = false;
            txtPassword.TextAlign = HorizontalAlignment.Left;
            txtPassword.TrailingIcon = null;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(446, 142);
            label3.Name = "label3";
            label3.Size = new Size(65, 25);
            label3.TabIndex = 5;
            label3.Text = "MSSV";
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(446, 222);
            label4.Name = "label4";
            label4.Size = new Size(96, 25);
            label4.TabIndex = 6;
            label4.Text = "Mật khẩu";
            label4.Click += label4_Click;
            // 
            // btnLogin
            // 
            btnLogin.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.Density = MaterialButton.MaterialButtonDensity.Default;
            btnLogin.Depth = 0;
            btnLogin.HighEmphasis = true;
            btnLogin.Icon = null;
            btnLogin.IconType = MaterialButton.MaterialIconType.Rebase;
            btnLogin.Location = new Point(446, 312);
            btnLogin.Margin = new Padding(4, 6, 4, 6);
            btnLogin.MinimumSize = new Size(365, 36);
            btnLogin.MouseState = MaterialDrawHelper.MaterialMouseState.HOVER;
            btnLogin.Name = "btnLogin";
            btnLogin.NoAccentTextColor = Color.Empty;
            btnLogin.Size = new Size(365, 36);
            btnLogin.TabIndex = 7;
            btnLogin.Text = "Đăng nhập";
            btnLogin.Type = MaterialButton.MaterialButtonType.Contained;
            btnLogin.UseAccentColor = false;
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(903, 517);
            Controls.Add(btnLogin);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(txtPassword);
            Controls.Add(txtMssv);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Cursor = Cursors.Hand;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "LoginForm";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label label1;
        private Label label2;
        private MaterialTextBoxEdit txtMssv;
        private MaterialTextBoxEdit txtPassword;
        private Label label3;
        private Label label4;
        private MaterialButton btnLogin;
    }
}
