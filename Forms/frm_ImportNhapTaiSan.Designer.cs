namespace QuanLyTaiSan
{
    partial class frm_ImportNhapTaiSan
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.usc_NhapTaiSan1 = new QuanLyTaiSan.usc_NhapTaiSan();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // usc_NhapTaiSan1
            // 
            this.usc_NhapTaiSan1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.usc_NhapTaiSan1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.usc_NhapTaiSan1.Location = new System.Drawing.Point(-7, -1);
            this.usc_NhapTaiSan1.Name = "usc_NhapTaiSan1";
            this.usc_NhapTaiSan1.Size = new System.Drawing.Size(947, 463);
            this.usc_NhapTaiSan1.TabIndex = 0;
            // 
            // frm_ImportNhapTaiSan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 460);
            this.Controls.Add(this.usc_NhapTaiSan1);
            this.DoubleBuffered = true;
            this.Name = "frm_ImportNhapTaiSan";
            this.Text = "Nhập Tài Sản";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_ImportTaiSan_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private usc_NhapTaiSan usc_NhapTaiSan1;
    }
}