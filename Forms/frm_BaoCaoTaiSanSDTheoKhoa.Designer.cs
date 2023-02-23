namespace QuanLyTaiSan
{
    partial class frm_BaoCaoTaiSanSDTheoKhoa
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_BaoCaoTaiSanSDTheoKhoa));
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.dgvMain = new E00_ControlNew.App.Widget.DotNetBar.General.DataGridView();
            this.pnlHead = new DevComponents.DotNetBar.PanelEx();
            this.chkByDeparment = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkNotUsed = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkAll = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.btnExport = new DevComponents.DotNetBar.ButtonX();
            this.btnFind = new DevComponents.DotNetBar.ButtonX();
            this.lblDepartment = new DevComponents.DotNetBar.LabelX();
            this.cboDeparment = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.pnlMain = new DevComponents.DotNetBar.PanelEx();
            this.col_MATAISAN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_SERINUMBER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_MAVACH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenTaiSan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_SOLUONNG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_MAKPQUANLY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TENKPQUANLY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_MAKPSUDUNG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TenKPSUDUNG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_MANGUOISUDUNG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TENNGUOISUDUNG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_MAKHUVUC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TENKHUVUC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TENTRANGTHAI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_MAKP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_MANGUOINHAN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_NGAYKETTHUC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_NGAYSUDUNG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_SUDUNG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_GHICHU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_MACHIEID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_MAKIEMKE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TENKIEMKE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_NGAYKIEMKE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.pnlHead.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.dgvMain, 0, 1);
            this.tlpMain.Controls.Add(this.pnlHead, 0, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(886, 507);
            this.tlpMain.TabIndex = 2;
            // 
            // dgvMain
            // 
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
            this.dgvMain.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMain.BackgroundColor = System.Drawing.Color.White;
            this.dgvMain.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_MATAISAN,
            this.col_SERINUMBER,
            this.col_MAVACH,
            this.colTenTaiSan,
            this.col_SOLUONNG,
            this.col_MAKPQUANLY,
            this.col_TENKPQUANLY,
            this.col_MAKPSUDUNG,
            this.col_TenKPSUDUNG,
            this.col_MANGUOISUDUNG,
            this.col_TENNGUOISUDUNG,
            this.col_MAKHUVUC,
            this.col_TENKHUVUC,
            this.col_TENTRANGTHAI,
            this.col_MAKP,
            this.col_MANGUOINHAN,
            this.col_NGAYKETTHUC,
            this.col_NGAYSUDUNG,
            this.col_SUDUNG,
            this.col_GHICHU,
            this.col_MACHIEID,
            this.col_MAKIEMKE,
            this.col_TENKIEMKE,
            this.col_NGAYKIEMKE});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMain.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMain.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvMain.IsEnabled = true;
            this.dgvMain.IsReadonly = true;
            this.dgvMain.IsVisible = true;
            this.dgvMain.Location = new System.Drawing.Point(0, 30);
            this.dgvMain.Margin = new System.Windows.Forms.Padding(0);
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMain.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvMain.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMain.Size = new System.Drawing.Size(886, 477);
            this.dgvMain.TabIndex = 0;
            // 
            // pnlHead
            // 
            this.pnlHead.CanvasColor = System.Drawing.SystemColors.Control;
            this.pnlHead.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pnlHead.Controls.Add(this.chkByDeparment);
            this.pnlHead.Controls.Add(this.chkNotUsed);
            this.pnlHead.Controls.Add(this.chkAll);
            this.pnlHead.Controls.Add(this.btnClose);
            this.pnlHead.Controls.Add(this.btnExport);
            this.pnlHead.Controls.Add(this.btnFind);
            this.pnlHead.Controls.Add(this.lblDepartment);
            this.pnlHead.Controls.Add(this.cboDeparment);
            this.pnlHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHead.Location = new System.Drawing.Point(0, 0);
            this.pnlHead.Margin = new System.Windows.Forms.Padding(0);
            this.pnlHead.Name = "pnlHead";
            this.pnlHead.Size = new System.Drawing.Size(886, 30);
            this.pnlHead.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlHead.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pnlHead.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.pnlHead.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlHead.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pnlHead.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlHead.Style.GradientAngle = 90;
            this.pnlHead.TabIndex = 2;
            // 
            // chkByDeparment
            // 
            // 
            // 
            // 
            this.chkByDeparment.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkByDeparment.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.chkByDeparment.Location = new System.Drawing.Point(202, 3);
            this.chkByDeparment.Name = "chkByDeparment";
            this.chkByDeparment.Size = new System.Drawing.Size(100, 23);
            this.chkByDeparment.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkByDeparment.TabIndex = 2;
            this.chkByDeparment.Text = "Theo bộ phận";
            // 
            // chkNotUsed
            // 
            // 
            // 
            // 
            this.chkNotUsed.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkNotUsed.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.chkNotUsed.Location = new System.Drawing.Point(102, 3);
            this.chkNotUsed.Name = "chkNotUsed";
            this.chkNotUsed.Size = new System.Drawing.Size(100, 23);
            this.chkNotUsed.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkNotUsed.TabIndex = 1;
            this.chkNotUsed.Text = "Chưa sử dụng";
            // 
            // chkAll
            // 
            // 
            // 
            // 
            this.chkAll.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkAll.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.chkAll.Checked = true;
            this.chkAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAll.CheckValue = "Y";
            this.chkAll.Location = new System.Drawing.Point(2, 3);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(100, 23);
            this.chkAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkAll.TabIndex = 0;
            this.chkAll.Text = "Tất cả";
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageFixedSize = new System.Drawing.Size(20, 20);
            this.btnClose.Location = new System.Drawing.Point(781, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 25);
            this.btnClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Đóng";
            this.btnClose.Tooltip = "Đóng giao diện (Esc)";
            // 
            // btnExport
            // 
            this.btnExport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
            this.btnExport.ImageFixedSize = new System.Drawing.Size(22, 22);
            this.btnExport.Location = new System.Drawing.Point(681, 2);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(100, 25);
            this.btnExport.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExport.TabIndex = 6;
            this.btnExport.Text = "Xuất Excel";
            this.btnExport.Tooltip = "Xuất file Excel (F9)";
            // 
            // btnFind
            // 
            this.btnFind.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFind.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnFind.Image = ((System.Drawing.Image)(resources.GetObject("btnFind.Image")));
            this.btnFind.ImageFixedSize = new System.Drawing.Size(22, 22);
            this.btnFind.Location = new System.Drawing.Point(581, 2);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(100, 25);
            this.btnFind.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnFind.TabIndex = 5;
            this.btnFind.Text = "Tìm";
            this.btnFind.Tooltip = "Lấy thông tin dữ liệu (Enter)";
            // 
            // lblDepartment
            // 
            // 
            // 
            // 
            this.lblDepartment.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblDepartment.Location = new System.Drawing.Point(308, 3);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(75, 23);
            this.lblDepartment.TabIndex = 3;
            this.lblDepartment.Text = "Bộ phận:";
            this.lblDepartment.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // cboDeparment
            // 
            this.cboDeparment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDeparment.DisplayMember = "Text";
            this.cboDeparment.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboDeparment.Enabled = false;
            this.cboDeparment.FormattingEnabled = true;
            this.cboDeparment.ItemHeight = 14;
            this.cboDeparment.Location = new System.Drawing.Point(389, 5);
            this.cboDeparment.Name = "cboDeparment";
            this.cboDeparment.Size = new System.Drawing.Size(186, 20);
            this.cboDeparment.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboDeparment.TabIndex = 4;
            this.cboDeparment.WatermarkText = "Nhập thông tin bộ phận";
            // 
            // pnlMain
            // 
            this.pnlMain.CanvasColor = System.Drawing.SystemColors.Control;
            this.pnlMain.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pnlMain.Controls.Add(this.tlpMain);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(886, 507);
            this.pnlMain.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlMain.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pnlMain.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.pnlMain.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlMain.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pnlMain.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlMain.Style.GradientAngle = 90;
            this.pnlMain.TabIndex = 3;
            // 
            // col_MATAISAN
            // 
            this.col_MATAISAN.DataPropertyName = "MATAISAN";
            this.col_MATAISAN.HeaderText = "Mã Tài sản";
            this.col_MATAISAN.Name = "col_MATAISAN";
            this.col_MATAISAN.ReadOnly = true;
            // 
            // col_SERINUMBER
            // 
            this.col_SERINUMBER.DataPropertyName = "SERINUMBER";
            this.col_SERINUMBER.HeaderText = "Số seri";
            this.col_SERINUMBER.Name = "col_SERINUMBER";
            this.col_SERINUMBER.ReadOnly = true;
            // 
            // col_MAVACH
            // 
            this.col_MAVACH.DataPropertyName = "MAVACH";
            this.col_MAVACH.HeaderText = "Mã vạch";
            this.col_MAVACH.MinimumWidth = 150;
            this.col_MAVACH.Name = "col_MAVACH";
            this.col_MAVACH.ReadOnly = true;
            this.col_MAVACH.Width = 150;
            // 
            // colTenTaiSan
            // 
            this.colTenTaiSan.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTenTaiSan.DataPropertyName = "TENTAISAN";
            this.colTenTaiSan.HeaderText = "Tên tài sản";
            this.colTenTaiSan.MinimumWidth = 200;
            this.colTenTaiSan.Name = "colTenTaiSan";
            this.colTenTaiSan.ReadOnly = true;
            // 
            // col_SOLUONNG
            // 
            this.col_SOLUONNG.DataPropertyName = "SOLUONNG";
            this.col_SOLUONNG.HeaderText = "Số lượng";
            this.col_SOLUONNG.MinimumWidth = 80;
            this.col_SOLUONNG.Name = "col_SOLUONNG";
            this.col_SOLUONNG.ReadOnly = true;
            this.col_SOLUONNG.Width = 80;
            // 
            // col_MAKPQUANLY
            // 
            this.col_MAKPQUANLY.DataPropertyName = "MAKPQUANLY";
            this.col_MAKPQUANLY.HeaderText = "Mã KPQL";
            this.col_MAKPQUANLY.MinimumWidth = 80;
            this.col_MAKPQUANLY.Name = "col_MAKPQUANLY";
            this.col_MAKPQUANLY.ReadOnly = true;
            this.col_MAKPQUANLY.Width = 80;
            // 
            // col_TENKPQUANLY
            // 
            this.col_TENKPQUANLY.DataPropertyName = "TENKPQUANLY";
            this.col_TENKPQUANLY.HeaderText = "Tên KP quản lý ";
            this.col_TENKPQUANLY.MinimumWidth = 160;
            this.col_TENKPQUANLY.Name = "col_TENKPQUANLY";
            this.col_TENKPQUANLY.ReadOnly = true;
            this.col_TENKPQUANLY.Width = 160;
            // 
            // col_MAKPSUDUNG
            // 
            this.col_MAKPSUDUNG.DataPropertyName = "MAKPSUDUNG";
            this.col_MAKPSUDUNG.HeaderText = "Mã KPSD";
            this.col_MAKPSUDUNG.MinimumWidth = 80;
            this.col_MAKPSUDUNG.Name = "col_MAKPSUDUNG";
            this.col_MAKPSUDUNG.ReadOnly = true;
            this.col_MAKPSUDUNG.Width = 80;
            // 
            // col_TenKPSUDUNG
            // 
            this.col_TenKPSUDUNG.DataPropertyName = "TENKPSUDUNG";
            this.col_TenKPSUDUNG.HeaderText = "Tên KP sử dụng";
            this.col_TenKPSUDUNG.MinimumWidth = 150;
            this.col_TenKPSUDUNG.Name = "col_TenKPSUDUNG";
            this.col_TenKPSUDUNG.ReadOnly = true;
            this.col_TenKPSUDUNG.Width = 150;
            // 
            // col_MANGUOISUDUNG
            // 
            this.col_MANGUOISUDUNG.DataPropertyName = "MANGUOISUDUNG";
            this.col_MANGUOISUDUNG.HeaderText = "Mã NS.dụng";
            this.col_MANGUOISUDUNG.Name = "col_MANGUOISUDUNG";
            this.col_MANGUOISUDUNG.ReadOnly = true;
            // 
            // col_TENNGUOISUDUNG
            // 
            this.col_TENNGUOISUDUNG.DataPropertyName = "TENNGUOISUDUNG";
            this.col_TENNGUOISUDUNG.HeaderText = "Người sử dụng";
            this.col_TENNGUOISUDUNG.MinimumWidth = 120;
            this.col_TENNGUOISUDUNG.Name = "col_TENNGUOISUDUNG";
            this.col_TENNGUOISUDUNG.ReadOnly = true;
            this.col_TENNGUOISUDUNG.Width = 120;
            // 
            // col_MAKHUVUC
            // 
            this.col_MAKHUVUC.DataPropertyName = "MAKHUVUC";
            this.col_MAKHUVUC.HeaderText = "Mã KV";
            this.col_MAKHUVUC.MinimumWidth = 70;
            this.col_MAKHUVUC.Name = "col_MAKHUVUC";
            this.col_MAKHUVUC.ReadOnly = true;
            this.col_MAKHUVUC.Width = 70;
            // 
            // col_TENKHUVUC
            // 
            this.col_TENKHUVUC.DataPropertyName = "TENKHUVUC";
            this.col_TENKHUVUC.HeaderText = "Khu vực";
            this.col_TENKHUVUC.MinimumWidth = 120;
            this.col_TENKHUVUC.Name = "col_TENKHUVUC";
            this.col_TENKHUVUC.ReadOnly = true;
            this.col_TENKHUVUC.Width = 120;
            // 
            // col_TENTRANGTHAI
            // 
            this.col_TENTRANGTHAI.DataPropertyName = "TENTRANGTHAI";
            this.col_TENTRANGTHAI.HeaderText = "Trạng thái";
            this.col_TENTRANGTHAI.MinimumWidth = 100;
            this.col_TENTRANGTHAI.Name = "col_TENTRANGTHAI";
            this.col_TENTRANGTHAI.ReadOnly = true;
            // 
            // col_MAKP
            // 
            this.col_MAKP.DataPropertyName = "MAKP";
            this.col_MAKP.HeaderText = "Mã KP";
            this.col_MAKP.Name = "col_MAKP";
            this.col_MAKP.ReadOnly = true;
            // 
            // col_MANGUOINHAN
            // 
            this.col_MANGUOINHAN.DataPropertyName = "MANGUOINHAN";
            this.col_MANGUOINHAN.HeaderText = "Mã Người nhận";
            this.col_MANGUOINHAN.Name = "col_MANGUOINHAN";
            this.col_MANGUOINHAN.ReadOnly = true;
            // 
            // col_NGAYKETTHUC
            // 
            this.col_NGAYKETTHUC.DataPropertyName = "NGAYKETTHUC";
            this.col_NGAYKETTHUC.HeaderText = "Ngày kết thúc";
            this.col_NGAYKETTHUC.Name = "col_NGAYKETTHUC";
            this.col_NGAYKETTHUC.ReadOnly = true;
            // 
            // col_NGAYSUDUNG
            // 
            this.col_NGAYSUDUNG.DataPropertyName = "NGAYSUDUNG";
            this.col_NGAYSUDUNG.HeaderText = "Ngày sử dụng";
            this.col_NGAYSUDUNG.Name = "col_NGAYSUDUNG";
            this.col_NGAYSUDUNG.ReadOnly = true;
            // 
            // col_SUDUNG
            // 
            this.col_SUDUNG.DataPropertyName = "SUDUNG";
            this.col_SUDUNG.HeaderText = "Sử dụng";
            this.col_SUDUNG.Name = "col_SUDUNG";
            this.col_SUDUNG.ReadOnly = true;
            // 
            // col_GHICHU
            // 
            this.col_GHICHU.DataPropertyName = "GHICHU";
            this.col_GHICHU.HeaderText = "Ghi chú";
            this.col_GHICHU.Name = "col_GHICHU";
            this.col_GHICHU.ReadOnly = true;
            // 
            // col_MACHIEID
            // 
            this.col_MACHIEID.DataPropertyName = "MACHIEID";
            this.col_MACHIEID.HeaderText = "Mã Máy";
            this.col_MACHIEID.Name = "col_MACHIEID";
            this.col_MACHIEID.ReadOnly = true;
            // 
            // col_MAKIEMKE
            // 
            this.col_MAKIEMKE.DataPropertyName = "MAKIEMKE";
            this.col_MAKIEMKE.HeaderText = "Mã kiểm kê";
            this.col_MAKIEMKE.Name = "col_MAKIEMKE";
            this.col_MAKIEMKE.ReadOnly = true;
            // 
            // col_TENKIEMKE
            // 
            this.col_TENKIEMKE.DataPropertyName = "TENKIEMKE";
            this.col_TENKIEMKE.HeaderText = "Tên kiểm kê";
            this.col_TENKIEMKE.Name = "col_TENKIEMKE";
            this.col_TENKIEMKE.ReadOnly = true;
            // 
            // col_NGAYKIEMKE
            // 
            this.col_NGAYKIEMKE.DataPropertyName = "NGAYKIEMKE";
            this.col_NGAYKIEMKE.HeaderText = "Ngày kiểm kê";
            this.col_NGAYKIEMKE.Name = "col_NGAYKIEMKE";
            this.col_NGAYKIEMKE.ReadOnly = true;
            // 
            // frm_BaoCaoTaiSanSDTheoKhoa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 507);
            this.Controls.Add(this.pnlMain);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "frm_BaoCaoTaiSanSDTheoKhoa";
            this.Text = "Báo cáo tài sản sử dụng";
            this.tlpMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.pnlHead.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private E00_ControlNew.App.Widget.DotNetBar.General.DataGridView dgvMain;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private DevComponents.DotNetBar.PanelEx pnlMain;
        private DevComponents.DotNetBar.PanelEx pnlHead;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboDeparment;
        private DevComponents.DotNetBar.LabelX lblDepartment;
        private DevComponents.DotNetBar.ButtonX btnFind;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkByDeparment;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkAll;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkNotUsed;
        private DevComponents.DotNetBar.ButtonX btnExport;
        private DevComponents.DotNetBar.ButtonX btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_MATAISAN;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_SERINUMBER;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_MAVACH;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenTaiSan;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_SOLUONNG;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_MAKPQUANLY;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TENKPQUANLY;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_MAKPSUDUNG;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TenKPSUDUNG;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_MANGUOISUDUNG;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TENNGUOISUDUNG;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_MAKHUVUC;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TENKHUVUC;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TENTRANGTHAI;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_MAKP;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_MANGUOINHAN;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_NGAYKETTHUC;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_NGAYSUDUNG;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_SUDUNG;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_GHICHU;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_MACHIEID;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_MAKIEMKE;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TENKIEMKE;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_NGAYKIEMKE;
    }
}