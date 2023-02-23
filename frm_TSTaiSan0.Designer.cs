namespace QuanLyTaiSan
{
    partial class frm_TSTaiSan0
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
            this.usc_DanhMucTaiSan1 = new E00_Base.usc_DanhMucTaiSan();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // usc_DanhMucTaiSan1
            // 
            this.usc_DanhMucTaiSan1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.usc_DanhMucTaiSan1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usc_DanhMucTaiSan1.Location = new System.Drawing.Point(0, 0);
            this.usc_DanhMucTaiSan1.Name = "usc_DanhMucTaiSan1";
            this.usc_DanhMucTaiSan1.Size = new System.Drawing.Size(1002, 385);
            this.usc_DanhMucTaiSan1.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frm_TSTaiSan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 385);
            this.Controls.Add(this.usc_DanhMucTaiSan1);
            this.DoubleBuffered = true;
            this.Name = "frm_TSTaiSan";
            this.Text = "Danh Mục Tài Sản";
            this.Load += new System.EventHandler(this.frm_TSTaiSan_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private E00_Base.usc_DanhMucTaiSan usc_DanhMucTaiSan1;
        private System.Windows.Forms.Timer timer1;
    }
}