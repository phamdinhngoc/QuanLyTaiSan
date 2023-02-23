namespace QuanLyTaiSan
{
    partial class frm_TS_DanhMuc
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_TS_DanhMuc));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTen = new E00_Control.his_LabelX(this.components);
            this.txtTen = new E00_Control.his_TextboxX();
            this.lblSTT = new E00_Control.his_LabelX(this.components);
            this.itgSTT = new E00_Control.his_IntegerInput();
            this.chkTamNgung = new E00_Control.his_CheckBoxX();
            this.lblGhiChu = new E00_Control.his_LabelX(this.components);
            this.txtGhiChu = new E00_Control.his_TextboxX();
            this.chkAll = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.dgvMain = new E00_Control.his_DataGridView();
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colSua = new DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn();
            this.colXoa = new DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSTT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTamNgung = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colGhiChu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlButton.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.pnlControl2.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlIn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.itgSTT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCount
            // 
            // 
            // 
            // 
            this.lblCount.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblCount.Size = new System.Drawing.Size(131, 28);
            // 
            // txtTimKiem
            // 
            // 
            // 
            // 
            this.txtTimKiem.Border.Class = "TextBoxBorder";
            this.txtTimKiem.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTimKiem.Location = new System.Drawing.Point(997, 2);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Location = new System.Drawing.Point(1204, 2);
            // 
            // btnThem
            // 
            this.btnThem.Size = new System.Drawing.Size(70, 51);
            // 
            // pnlButton
            // 
            this.pnlButton.Size = new System.Drawing.Size(801, 53);
            // 
            // pnlSearch
            // 
            this.pnlSearch.Location = new System.Drawing.Point(2, 111);
            this.pnlSearch.Size = new System.Drawing.Size(801, 28);
            // 
            // lblKetQua
            // 
            // 
            // 
            // 
            this.lblKetQua.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblKetQua.Location = new System.Drawing.Point(859, 4);
            this.lblKetQua.Size = new System.Drawing.Size(111, 15);
            this.lblKetQua.Visible = false;
            // 
            // btnBoQua
            // 
            this.btnBoQua.Size = new System.Drawing.Size(70, 51);
            // 
            // btnLuu
            // 
            this.btnLuu.Size = new System.Drawing.Size(70, 51);
            // 
            // btnXoa
            // 
            this.btnXoa.Size = new System.Drawing.Size(70, 51);
            // 
            // btnSua
            // 
            this.btnSua.Size = new System.Drawing.Size(70, 51);
            // 
            // pnlControl2
            // 
            this.pnlControl2.Controls.Add(this.txtGhiChu);
            this.pnlControl2.Controls.Add(this.lblGhiChu);
            this.pnlControl2.Controls.Add(this.chkTamNgung);
            this.pnlControl2.Controls.Add(this.itgSTT);
            this.pnlControl2.Controls.Add(this.lblSTT);
            this.pnlControl2.Controls.Add(this.txtTen);
            this.pnlControl2.Controls.Add(this.lblTen);
            this.pnlControl2.Location = new System.Drawing.Point(2, 55);
            this.pnlControl2.Size = new System.Drawing.Size(801, 56);
            this.pnlControl2.Controls.SetChildIndex(this.lblTen, 0);
            this.pnlControl2.Controls.SetChildIndex(this.txtTen, 0);
            this.pnlControl2.Controls.SetChildIndex(this.lblSTT, 0);
            this.pnlControl2.Controls.SetChildIndex(this.itgSTT, 0);
            this.pnlControl2.Controls.SetChildIndex(this.chkTamNgung, 0);
            this.pnlControl2.Controls.SetChildIndex(this.lblGhiChu, 0);
            this.pnlControl2.Controls.SetChildIndex(this.txtGhiChu, 0);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.chkAll);
            this.pnlMain.Controls.Add(this.dgvMain);
            this.pnlMain.Location = new System.Drawing.Point(2, 139);
            this.pnlMain.Size = new System.Drawing.Size(801, 359);
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(433, 1);
            this.btnThoat.Size = new System.Drawing.Size(70, 51);
            // 
            // lblIn
            // 
            this.lblIn.Size = new System.Drawing.Size(1, 51);
            // 
            // btnIn
            // 
            this.btnIn.Size = new System.Drawing.Size(74, 34);
            // 
            // pnlIn
            // 
            this.pnlIn.Size = new System.Drawing.Size(75, 51);
            // 
            // lblTen
            // 
            this.lblTen.AutoSize = true;
            // 
            // 
            // 
            this.lblTen.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblTen.IsNotNull = true;
            this.lblTen.Location = new System.Drawing.Point(50, 7);
            this.lblTen.Name = "lblTen";
            this.lblTen.Size = new System.Drawing.Size(28, 15);
            this.lblTen.TabIndex = 0;
            this.lblTen.Text = "Tên:<font color=\"#ED1C24\">*</font>";
            // 
            // txtTen
            // 
            this.txtTen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTen.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtTen.Border.Class = "TextBoxBorder";
            this.txtTen.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTen.Location = new System.Drawing.Point(90, 5);
            this.txtTen.Name = "txtTen";
            this.txtTen.Size = new System.Drawing.Size(514, 20);
            this.txtTen.TabIndex = 0;
            // 
            // lblSTT
            // 
            this.lblSTT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSTT.AutoSize = true;
            // 
            // 
            // 
            this.lblSTT.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSTT.IsNotNull = false;
            this.lblSTT.Location = new System.Drawing.Point(610, 7);
            this.lblSTT.Name = "lblSTT";
            this.lblSTT.Size = new System.Drawing.Size(24, 15);
            this.lblSTT.TabIndex = 2;
            this.lblSTT.Text = "STT:<font color=\"#ED1C24\"></font>";
            // 
            // itgSTT
            // 
            this.itgSTT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.itgSTT.BackgroundStyle.Class = "DateTimeInputBackground";
            this.itgSTT.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itgSTT.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.itgSTT.Location = new System.Drawing.Point(639, 5);
            this.itgSTT.MinValue = 0;
            this.itgSTT.Name = "itgSTT";
            this.itgSTT.ShowUpDown = true;
            this.itgSTT.Size = new System.Drawing.Size(57, 20);
            this.itgSTT.TabIndex = 1;
            // 
            // chkTamNgung
            // 
            this.chkTamNgung.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTamNgung.AutoSize = true;
            this.chkTamNgung.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.chkTamNgung.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkTamNgung.Location = new System.Drawing.Point(702, 8);
            this.chkTamNgung.Name = "chkTamNgung";
            this.chkTamNgung.Size = new System.Drawing.Size(78, 15);
            this.chkTamNgung.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkTamNgung.TabIndex = 2;
            this.chkTamNgung.Text = "Tạm ngừng";
            this.chkTamNgung.TextColor = System.Drawing.Color.Black;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            // 
            // 
            // 
            this.lblGhiChu.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblGhiChu.IsNotNull = false;
            this.lblGhiChu.Location = new System.Drawing.Point(31, 31);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(42, 15);
            this.lblGhiChu.TabIndex = 5;
            this.lblGhiChu.Text = "Ghi chú:<font color=\"#ED1C24\"></font>";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGhiChu.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtGhiChu.Border.Class = "TextBoxBorder";
            this.txtGhiChu.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtGhiChu.Location = new System.Drawing.Point(90, 29);
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(690, 20);
            this.txtGhiChu.TabIndex = 3;
            // 
            // chkAll
            // 
            this.chkAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.chkAll.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkAll.CheckSignSize = new System.Drawing.Size(16, 14);
            this.chkAll.Location = new System.Drawing.Point(6, 3);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(22, 19);
            this.chkAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkAll.TabIndex = 34;
            this.chkAll.Click += new System.EventHandler(this.chkAll_Click);
            // 
            // dgvMain
            // 
            this.dgvMain.AllowUserToAddRows = false;
            dataGridViewCellStyle19.BackColor = System.Drawing.Color.LightCyan;
            this.dgvMain.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle19;
            this.dgvMain.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvMain.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvMain.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(246)))));
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle20.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle20;
            this.dgvMain.ColumnHeadersHeight = 26;
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.colSua,
            this.colXoa,
            this.colID,
            this.colMa,
            this.colTen,
            this.colSTT,
            this.colTamNgung,
            this.colGhiChu,
            this.Column1});
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle22.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle22.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMain.DefaultCellStyle = dataGridViewCellStyle22;
            this.dgvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMain.EnableHeadersVisualStyles = false;
            this.dgvMain.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.dgvMain.Location = new System.Drawing.Point(0, 0);
            this.dgvMain.MultiSelect = false;
            this.dgvMain.Name = "dgvMain";
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle23.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle23.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle23.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle23.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMain.RowHeadersDefaultCellStyle = dataGridViewCellStyle23;
            this.dgvMain.RowHeadersVisible = false;
            this.dgvMain.RowHeadersWidth = 100;
            dataGridViewCellStyle24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.dgvMain.RowsDefaultCellStyle = dataGridViewCellStyle24;
            this.dgvMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMain.Size = new System.Drawing.Size(801, 359);
            this.dgvMain.TabIndex = 33;
            this.dgvMain.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMain_CellClick);
            this.dgvMain.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMain_CellContentClick);
            this.dgvMain.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMain_CellValueChanged);
            // 
            // colCheck
            // 
            this.colCheck.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colCheck.HeaderText = "";
            this.colCheck.Name = "colCheck";
            this.colCheck.Width = 33;
            // 
            // colSua
            // 
            this.colSua.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colSua.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange;
            this.colSua.HeaderText = "Sửa";
            this.colSua.HoverImage = ((System.Drawing.Image)(resources.GetObject("colSua.HoverImage")));
            this.colSua.Image = ((System.Drawing.Image)(resources.GetObject("colSua.Image")));
            this.colSua.Name = "colSua";
            this.colSua.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colSua.Text = "Sửa";
            this.colSua.ToolTipText = "Sửa";
            this.colSua.Width = 40;
            // 
            // colXoa
            // 
            this.colXoa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colXoa.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange;
            this.colXoa.HeaderText = "Xóa";
            this.colXoa.HoverImage = ((System.Drawing.Image)(resources.GetObject("colXoa.HoverImage")));
            this.colXoa.Image = ((System.Drawing.Image)(resources.GetObject("colXoa.Image")));
            this.colXoa.Name = "colXoa";
            this.colXoa.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colXoa.Text = "Xóa";
            this.colXoa.ToolTipText = "Xóa dòng đang chọn";
            this.colXoa.Width = 40;
            // 
            // colID
            // 
            this.colID.DataPropertyName = "ID";
            this.colID.HeaderText = "Id";
            this.colID.Name = "colID";
            this.colID.Visible = false;
            this.colID.Width = 44;
            // 
            // colMa
            // 
            this.colMa.DataPropertyName = "MA";
            this.colMa.HeaderText = "Mã";
            this.colMa.Name = "colMa";
            this.colMa.ReadOnly = true;
            this.colMa.Visible = false;
            this.colMa.Width = 52;
            // 
            // colTen
            // 
            this.colTen.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colTen.DataPropertyName = "TEN";
            this.colTen.HeaderText = "Tên";
            this.colTen.Name = "colTen";
            this.colTen.ReadOnly = true;
            this.colTen.Width = 51;
            // 
            // colSTT
            // 
            this.colSTT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colSTT.DataPropertyName = "STT";
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colSTT.DefaultCellStyle = dataGridViewCellStyle21;
            this.colSTT.FillWeight = 5F;
            this.colSTT.HeaderText = "STT";
            this.colSTT.Name = "colSTT";
            this.colSTT.ReadOnly = true;
            this.colSTT.Width = 44;
            // 
            // colTamNgung
            // 
            this.colTamNgung.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colTamNgung.DataPropertyName = "TAMNGUNG";
            this.colTamNgung.FillWeight = 10F;
            this.colTamNgung.HeaderText = "Tạm Ngừng";
            this.colTamNgung.Name = "colTamNgung";
            this.colTamNgung.ReadOnly = true;
            this.colTamNgung.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colTamNgung.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colTamNgung.Width = 99;
            // 
            // colGhiChu
            // 
            this.colGhiChu.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colGhiChu.DataPropertyName = "GhiChu";
            this.colGhiChu.HeaderText = "Ghi Chú";
            this.colGhiChu.Name = "colGhiChu";
            this.colGhiChu.ReadOnly = true;
            this.colGhiChu.Width = 70;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // frm_TS_DanhMuc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 500);
            this.Name = "frm_TS_DanhMuc";
            this.Text = "Danh Mục";
            this.pnlButton.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlControl2.ResumeLayout(false);
            this.pnlControl2.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlIn.ResumeLayout(false);
            this.pnlIn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.itgSTT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private E00_Control.his_TextboxX txtTen;
        private E00_Control.his_LabelX lblTen;
        private E00_Control.his_CheckBoxX chkTamNgung;
        private E00_Control.his_IntegerInput itgSTT;
        private E00_Control.his_LabelX lblSTT;
        private E00_Control.his_TextboxX txtGhiChu;
        private E00_Control.his_LabelX lblGhiChu;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkAll;
        private E00_Control.his_DataGridView dgvMain;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn colSua;
        private DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn colXoa;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMa;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSTT;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colTamNgung;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGhiChu;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    }
}