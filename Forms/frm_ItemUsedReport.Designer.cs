namespace QuanLyTaiSan.Forms
{
    partial class frm_ItemUsedReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_ItemUsedReport));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTitle = new E00_Control.his_PanelEx();
            this.btnClose = new E00_Control.his_ButtonX2();
            this.lblTitle = new E00_Control.his_LabelX(this.components);
            this.gvgContainer = new E00_ControlAdv.ViewUI.GridContainerAdv();
            this.dgvGrid = new E00_ControlAdv.ViewUI.DataGridViewAdv();
            this.colInfo = new DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn();
            this.col_MAVACH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TENTAISAN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_KYHIEU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TENKPQUANLY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TENKPSUDUNG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TENNGUOISUDUNG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TENKHU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TENTANG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TENPHONGCONGNANG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TINHTRANG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTitle
            // 
            this.pnlTitle.CanvasColor = System.Drawing.SystemColors.Control;
            this.pnlTitle.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pnlTitle.Controls.Add(this.btnClose);
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(838, 33);
            this.pnlTitle.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlTitle.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.pnlTitle.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.pnlTitle.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlTitle.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pnlTitle.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlTitle.Style.GradientAngle = 90;
            this.pnlTitle.TabIndex = 9;
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(762, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(70, 25);
            this.btnClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Đóng";
            this.btnClose.TextColor = System.Drawing.Color.White;
            // 
            // lblTitle
            // 
            // 
            // 
            // 
            this.lblTitle.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.IsNotNull = false;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.PaddingLeft = 10;
            this.lblTitle.Size = new System.Drawing.Size(838, 33);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "THÔNG TIN TÀI SẢN ĐANG SỬ DỤNG";
            // 
            // gvgContainer
            // 
            this.gvgContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvgContainer.GridView = this.dgvGrid;
            this.gvgContainer.Location = new System.Drawing.Point(0, 33);
            this.gvgContainer.Name = "gvgContainer";
            this.gvgContainer.Size = new System.Drawing.Size(838, 352);
            this.gvgContainer.TabIndex = 12;
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
            this.colInfo,
            this.col_MAVACH,
            this.col_TENTAISAN,
            this.col_KYHIEU,
            this.col_TENKPQUANLY,
            this.col_TENKPSUDUNG,
            this.col_TENNGUOISUDUNG,
            this.col_TENKHU,
            this.col_TENTANG,
            this.col_TENPHONGCONGNANG,
            this.col_TINHTRANG});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvGrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGrid.FilterList = null;
            this.dgvGrid.FormContain = this;
            this.dgvGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvGrid.IsEnabled = true;
            this.dgvGrid.IsReadonly = true;
            this.dgvGrid.IsVisible = true;
            this.dgvGrid.Location = new System.Drawing.Point(0, 0);
            this.dgvGrid.MultiSelect = false;
            this.dgvGrid.Name = "dgvGrid";
            this.dgvGrid.ReadOnly = true;
            this.dgvGrid.RowHeadersWidth = 22;
            this.dgvGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGrid.Size = new System.Drawing.Size(838, 322);
            this.dgvGrid.TabIndex = 12;
            // 
            // colInfo
            // 
            this.colInfo.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.colInfo.HeaderText = "";
            this.colInfo.Image = ((System.Drawing.Image)(resources.GetObject("colInfo.Image")));
            this.colInfo.Name = "colInfo";
            this.colInfo.ReadOnly = true;
            this.colInfo.Text = null;
            this.colInfo.Width = 50;
            // 
            // col_MAVACH
            // 
            this.col_MAVACH.DataPropertyName = "MAVACH";
            this.col_MAVACH.HeaderText = "Mã vạch";
            this.col_MAVACH.Name = "col_MAVACH";
            this.col_MAVACH.ReadOnly = true;
            this.col_MAVACH.Width = 150;
            // 
            // col_TENTAISAN
            // 
            this.col_TENTAISAN.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_TENTAISAN.DataPropertyName = "TENTAISAN";
            dataGridViewCellStyle1.Format = "dd/MM/yyyy";
            this.col_TENTAISAN.DefaultCellStyle = dataGridViewCellStyle1;
            this.col_TENTAISAN.HeaderText = "Tên tài sản";
            this.col_TENTAISAN.MinimumWidth = 200;
            this.col_TENTAISAN.Name = "col_TENTAISAN";
            this.col_TENTAISAN.ReadOnly = true;
            // 
            // col_KYHIEU
            // 
            this.col_KYHIEU.DataPropertyName = "KYHIEU";
            this.col_KYHIEU.HeaderText = "Ký hiệu";
            this.col_KYHIEU.Name = "col_KYHIEU";
            this.col_KYHIEU.ReadOnly = true;
            this.col_KYHIEU.Width = 180;
            // 
            // col_TENKPQUANLY
            // 
            this.col_TENKPQUANLY.DataPropertyName = "TENKPQUANLY";
            this.col_TENKPQUANLY.HeaderText = "Tên khoa quản lý";
            this.col_TENKPQUANLY.MinimumWidth = 200;
            this.col_TENKPQUANLY.Name = "col_TENKPQUANLY";
            this.col_TENKPQUANLY.ReadOnly = true;
            this.col_TENKPQUANLY.Width = 200;
            // 
            // col_TENKPSUDUNG
            // 
            this.col_TENKPSUDUNG.DataPropertyName = "TENKPSUDUNG";
            this.col_TENKPSUDUNG.HeaderText = "Tên khoa sử dụng";
            this.col_TENKPSUDUNG.MinimumWidth = 200;
            this.col_TENKPSUDUNG.Name = "col_TENKPSUDUNG";
            this.col_TENKPSUDUNG.ReadOnly = true;
            this.col_TENKPSUDUNG.Width = 200;
            // 
            // col_TENNGUOISUDUNG
            // 
            this.col_TENNGUOISUDUNG.DataPropertyName = "TENNGUOISUDUNG";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "#,##0";
            this.col_TENNGUOISUDUNG.DefaultCellStyle = dataGridViewCellStyle2;
            this.col_TENNGUOISUDUNG.HeaderText = "Người sử dụng";
            this.col_TENNGUOISUDUNG.MinimumWidth = 200;
            this.col_TENNGUOISUDUNG.Name = "col_TENNGUOISUDUNG";
            this.col_TENNGUOISUDUNG.ReadOnly = true;
            this.col_TENNGUOISUDUNG.Width = 200;
            // 
            // col_TENKHU
            // 
            this.col_TENKHU.DataPropertyName = "TENKHU";
            this.col_TENKHU.HeaderText = "Khu sử dụng";
            this.col_TENKHU.MinimumWidth = 200;
            this.col_TENKHU.Name = "col_TENKHU";
            this.col_TENKHU.ReadOnly = true;
            this.col_TENKHU.Width = 200;
            // 
            // col_TENTANG
            // 
            this.col_TENTANG.DataPropertyName = "TENTANG";
            this.col_TENTANG.HeaderText = "Tầng sử dụng";
            this.col_TENTANG.MinimumWidth = 200;
            this.col_TENTANG.Name = "col_TENTANG";
            this.col_TENTANG.ReadOnly = true;
            this.col_TENTANG.Width = 200;
            // 
            // col_TENPHONGCONGNANG
            // 
            this.col_TENPHONGCONGNANG.DataPropertyName = "TENPHONGCONGNANG";
            this.col_TENPHONGCONGNANG.HeaderText = "Phòng công năng";
            this.col_TENPHONGCONGNANG.MinimumWidth = 200;
            this.col_TENPHONGCONGNANG.Name = "col_TENPHONGCONGNANG";
            this.col_TENPHONGCONGNANG.ReadOnly = true;
            this.col_TENPHONGCONGNANG.Width = 200;
            // 
            // col_TINHTRANG
            // 
            this.col_TINHTRANG.HeaderText = "Tình trạng";
            this.col_TINHTRANG.Name = "col_TINHTRANG";
            this.col_TINHTRANG.ReadOnly = true;
            this.col_TINHTRANG.Visible = false;
            this.col_TINHTRANG.Width = 80;
            // 
            // frm_ItemUsedReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(838, 385);
            this.Controls.Add(this.gvgContainer);
            this.Controls.Add(this.pnlTitle);
            this.DoubleBuffered = true;
            this.Name = "frm_ItemUsedReport";
            this.Text = "THÔNG TIN TÀI SẢN ĐANG SỬ DỤNG";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.pnlTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        protected E00_Control.his_PanelEx pnlTitle;
        protected E00_Control.his_ButtonX2 btnClose;
        protected E00_Control.his_LabelX lblTitle;
        private E00_ControlAdv.ViewUI.DataGridViewAdv dgvGrid;
        private E00_ControlAdv.ViewUI.GridContainerAdv gvgContainer;
        private DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn colInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_MAVACH;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TENTAISAN;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_KYHIEU;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TENKPQUANLY;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TENKPSUDUNG;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TENNGUOISUDUNG;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TENKHU;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TENTANG;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TENPHONGCONGNANG;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TINHTRANG;
    }
}