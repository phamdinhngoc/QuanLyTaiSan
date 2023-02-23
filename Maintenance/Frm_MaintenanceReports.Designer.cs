namespace QuanLyTaiSan.Maintenance
{
    partial class Frm_MaintenanceReports
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvGrid = new E00_ControlAdv.ViewUI.DataGridViewAdv();
            this.col_SoPhieu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_CreateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TenMoTa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Ghichu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_UserCreate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(492, 5);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(570, 5);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(637, 5);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(704, 5);
            // 
            // pnlTitle
            // 
            this.pnlTitle.Size = new System.Drawing.Size(781, 33);
            this.pnlTitle.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlTitle.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.pnlTitle.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.pnlTitle.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlTitle.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pnlTitle.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlTitle.Style.GradientAngle = 90;
            // 
            // pnlEdit
            // 
            this.pnlEdit.Size = new System.Drawing.Size(781, 10);
            this.pnlEdit.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlEdit.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pnlEdit.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.pnlEdit.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlEdit.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pnlEdit.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlEdit.Style.GradientAngle = 90;
            // 
            // gvgContainer
            // 
            this.gvgContainer.GridView = this.dgvGrid;
            this.gvgContainer.Size = new System.Drawing.Size(781, 287);
            // 
            // lblTitle
            // 
            // 
            // 
            // 
            this.lblTitle.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblTitle.Size = new System.Drawing.Size(781, 33);
            this.lblTitle.Text = "DANH SÁCH PHIẾU BẢO TRÌ";
            // 
            // dgvGrid
            // 
            this.dgvGrid.AllowUserToAddRows = false;
            this.dgvGrid.AllowUserToDeleteRows = false;
            this.dgvGrid.AllowUserToOrderColumns = true;
            this.dgvGrid.BackgroundColor = System.Drawing.Color.White;
            this.dgvGrid.BindingSource = null;
            this.dgvGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dgvGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_SoPhieu,
            this.col_CreateDate,
            this.col_TenMoTa,
            this.col_Ghichu,
            this.col_TrangThai,
            this.col_UserCreate});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGrid.FilterList = null;
            this.dgvGrid.FormContain = this;
            this.dgvGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvGrid.IsEnabled = true;
            this.dgvGrid.IsReadonly = true;
            this.dgvGrid.IsVisible = true;
            this.dgvGrid.Location = new System.Drawing.Point(0, 0);
            this.dgvGrid.Name = "dgvGrid";
            this.dgvGrid.ReadOnly = true;
            this.dgvGrid.RowHeadersWidth = 22;
            this.dgvGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGrid.Size = new System.Drawing.Size(781, 257);
            this.dgvGrid.TabIndex = 3;
            // 
            // col_SoPhieu
            // 
            this.col_SoPhieu.DataPropertyName = "SOPHIEU";
            this.col_SoPhieu.HeaderText = "Số phiếu";
            this.col_SoPhieu.Name = "col_SoPhieu";
            this.col_SoPhieu.ReadOnly = true;
            // 
            // col_CreateDate
            // 
            this.col_CreateDate.DataPropertyName = "NGAY";
            dataGridViewCellStyle1.Format = "dd/MM/yyyy";
            this.col_CreateDate.DefaultCellStyle = dataGridViewCellStyle1;
            this.col_CreateDate.HeaderText = "Ngày";
            this.col_CreateDate.Name = "col_CreateDate";
            this.col_CreateDate.ReadOnly = true;
            // 
            // col_TenMoTa
            // 
            this.col_TenMoTa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_TenMoTa.DataPropertyName = "TENBT";
            this.col_TenMoTa.HeaderText = "Tên mô tả";
            this.col_TenMoTa.Name = "col_TenMoTa";
            this.col_TenMoTa.ReadOnly = true;
            // 
            // col_Ghichu
            // 
            this.col_Ghichu.DataPropertyName = "GHICHU";
            this.col_Ghichu.HeaderText = "Ghi chú";
            this.col_Ghichu.Name = "col_Ghichu";
            this.col_Ghichu.ReadOnly = true;
            this.col_Ghichu.Width = 300;
            // 
            // col_TrangThai
            // 
            this.col_TrangThai.DataPropertyName = "TRANGTHAI";
            this.col_TrangThai.HeaderText = "Trạng thái";
            this.col_TrangThai.Name = "col_TrangThai";
            this.col_TrangThai.ReadOnly = true;
            // 
            // col_UserCreate
            // 
            this.col_UserCreate.DataPropertyName = "USERUD";
            this.col_UserCreate.HeaderText = "Cập nhật bởi";
            this.col_UserCreate.Name = "col_UserCreate";
            this.col_UserCreate.ReadOnly = true;
            // 
            // Frm_MaintenanceReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(781, 330);
            this.Name = "Frm_MaintenanceReports";
            this.Text = "DANH SÁCH PHIẾU BẢO TRÌ";
            this.pnlTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private E00_ControlAdv.ViewUI.DataGridViewAdv dgvGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_SoPhieu;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_CreateDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TenMoTa;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Ghichu;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TrangThai;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_UserCreate;
    }
}