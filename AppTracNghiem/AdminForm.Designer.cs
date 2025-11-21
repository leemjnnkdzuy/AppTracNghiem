namespace AppTracNghiem
{
    partial class AdminForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.PictureBox pictureBox1;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminForm));
            pictureBox1 = new PictureBox();
            materialButton1 = new ReaLTaiizor.Controls.MaterialButton();
            hopeGroupBox3 = new ReaLTaiizor.Controls.HopeGroupBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Cursor = Cursors.Hand;
            pictureBox1.Location = new Point(1072, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(60, 59);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
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
            materialButton1.Location = new Point(13, 35);
            materialButton1.Margin = new Padding(4, 6, 4, 6);
            materialButton1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialButton1.Name = "materialButton1";
            materialButton1.NoAccentTextColor = Color.Empty;
            materialButton1.Size = new Size(131, 36);
            materialButton1.TabIndex = 2;
            materialButton1.Text = "Tạo Đề Mới";
            materialButton1.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            materialButton1.UseAccentColor = false;
            materialButton1.UseVisualStyleBackColor = true;
            materialButton1.Click += BtnTaoDe_Click;
            // 
            // hopeGroupBox3
            // 
            hopeGroupBox3.BorderColor = Color.FromArgb(220, 223, 230);
            hopeGroupBox3.Font = new Font("Segoe UI", 12F);
            hopeGroupBox3.ForeColor = Color.FromArgb(48, 49, 51);
            hopeGroupBox3.LineColor = Color.FromArgb(220, 223, 230);
            hopeGroupBox3.Location = new Point(13, 80);
            hopeGroupBox3.Name = "hopeGroupBox3";
            hopeGroupBox3.ShowText = false;
            hopeGroupBox3.Size = new Size(1119, 549);
            hopeGroupBox3.TabIndex = 25;
            hopeGroupBox3.TabStop = false;
            hopeGroupBox3.Text = "hopeGroupBox3";
            hopeGroupBox3.ThemeColor = Color.Transparent;
            // 
            // AdminForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1144, 641);
            Controls.Add(hopeGroupBox3);
            Controls.Add(materialButton1);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AdminForm";
            Text = "Quản Lý Đề";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ReaLTaiizor.Controls.MaterialButton materialButton1;
        private ReaLTaiizor.Controls.HopeGroupBox hopeGroupBox3;
    }
}