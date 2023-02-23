namespace QuanLyTaiSan.Forms
{
    partial class frm_DS_NhapTaiSan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_DS_NhapTaiSan));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTitle = new E00_Control.his_PanelEx();
            this.btnAdd = new E00_Control.his_ButtonX2();
            this.btnEdit = new E00_Control.his_ButtonX2();
            this.btnDelete = new E00_Control.his_ButtonX2();
            this.btnClose = new E00_Control.his_ButtonX2();
            this.lblTitle = new E00_Control.his_LabelX(this.components);
            this.gvgContainer = new E00_ControlAdv.ViewUI.GridContainerAdv();
            this.dgvGrid = new E00_ControlAdv.ViewUI.DataGridViewAdv();
            this.col_SOPHIEU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_NGAYNHAP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TotalReceived = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TENNCC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_NGUOIGIAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_SDTNGUOIGIAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_SOXE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_NOINHAP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.his_PanelEx1 = new E00_Control.his_PanelEx();
            this.chkAll = new E00_Control.his_CheckBoxX();
            this.chkMe = new E00_Control.his_CheckBoxX();
            this.dtiToDate = new E00_Control.his_DateTimeInput();
            this.dtiFromDate = new E00_Control.his_DateTimeInput();
            this.his_LabelX2 = new E00_Control.his_LabelX(this.components);
            this.his_LabelX1 = new E00_Control.his_LabelX(this.components);
            this.pnlTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrid)).BeginInit();
            this.his_PanelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtiToDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtiFromDate)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTitle
            // 
            this.pnlTitle.CanvasColor = System.Drawing.SystemColors.Control;
            this.pnlTitle.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pnlTitle.Controls.Add(this.btnAdd);
            this.pnlTitle.Controls.Add(this.btnEdit);
            this.pnlTitle.Controls.Add(this.btnDelete);
            this.pnlTitle.Controls.Add(this.btnClose);
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(833, 33);
            this.pnlTitle.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlTitle.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.pnlTitle.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.pnlTitle.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlTitle.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pnlTitle.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlTitle.Style.GradientAngle = 90;
            this.pnlTitle.TabIndex = 2;
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(545, 5);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(70, 25);
            this.btnAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.TextColor = System.Drawing.Color.White;
            // 
            // btnEdit
            // 
            this.btnEdit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.Location = new System.Drawing.Point(623, 5);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(70, 25);
            this.btnEdit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnEdit.TabIndex = 4;
            this.btnEdit.Text = "Sửa";
            this.btnEdit.TextColor = System.Drawing.Color.White;
            // 
            // btnDelete
            // 
            this.btnDelete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(690, 5);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(70, 25);
            this.btnDelete.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.TextColor = System.Drawing.Color.White;
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(757, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(70, 25);
            this.btnClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClose.TabIndex = 2;
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
            this.lblTitle.Size = new System.Drawing.Size(833, 33);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "DANH SÁCH PHIẾU NHẬP";
            // 
            // gvgContainer
            // 
            this.gvgContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvgContainer.GridView = this.dgvGrid;
            this.gvgContainer.Location = new System.Drawing.Point(0, 64);
            this.gvgContainer.Name = "gvgContainer";
            this.gvgContainer.Size = new System.Drawing.Size(833, 403);
            this.gvgContainer.TabIndex = 3;
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
            this.col_SOPHIEU,
            this.col_NGAYNHAP,
            this.col_TotalReceived,
            this.col_TENNCC,
            this.col_NGUOIGIAO,
            this.col_SDTNGUOIGIAO,
            this.col_SOXE,
            this.col_NOINHAP,
            this.col_TrangThai});
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
            this.dgvGrid.Size = new System.Drawing.Size(833, 373);
            this.dgvGrid.TabIndex = 4;
            // 
            // col_SOPHIEU
            // 
            this.col_SOPHIEU.DataPropertyName = "SOPHIEU";
            this.col_SOPHIEU.HeaderText = "Số phiếu";
            this.col_SOPHIEU.Name = "col_SOPHIEU";
            this.col_SOPHIEU.ReadOnly = true;
            // 
            // col_NGAYNHAP
            // 
            this.col_NGAYNHAP.DataPropertyName = "NGAYNHAP";
            dataGridViewCellStyle1.Format = "dd/MM/yyyy";
            this.col_NGAYNHAP.DefaultCellStyle = dataGridViewCellStyle1;
            this.col_NGAYNHAP.HeaderText = "Ngày nhập";
            this.col_NGAYNHAP.Name = "col_NGAYNHAP";
            this.col_NGAYNHAP.ReadOnly = true;
            // 
            // col_TotalReceived
            // 
            this.col_TotalReceived.DataPropertyName = "TotalReceived";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "#,##0";
            this.col_TotalReceived.DefaultCellStyle = dataGridViewCellStyle2;
            this.col_TotalReceived.HeaderText = "Tổng nhập";
            this.col_TotalReceived.Name = "col_TotalReceived";
            this.col_TotalReceived.ReadOnly = true;
            this.col_TotalReceived.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // col_TENNCC
            // 
            this.col_TENNCC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_TENNCC.DataPropertyName = "TENNCC";
            this.col_TENNCC.HeaderText = "Nhà cung cấp";
            this.col_TENNCC.MinimumWidth = 150;
            this.col_TENNCC.Name = "col_TENNCC";
            this.col_TENNCC.ReadOnly = true;
            // 
            // col_NGUOIGIAO
            // 
            this.col_NGUOIGIAO.DataPropertyName = "NGUOIGIAO";
            this.col_NGUOIGIAO.HeaderText = "Người giao";
            this.col_NGUOIGIAO.Name = "col_NGUOIGIAO";
            this.col_NGUOIGIAO.ReadOnly = true;
            this.col_NGUOIGIAO.Width = 200;
            // 
            // col_SDTNGUOIGIAO
            // 
            this.col_SDTNGUOIGIAO.DataPropertyName = "SDTNGUOIGIAO";
            this.col_SDTNGUOIGIAO.HeaderText = "SĐT";
            this.col_SDTNGUOIGIAO.Name = "col_SDTNGUOIGIAO";
            this.col_SDTNGUOIGIAO.ReadOnly = true;
            // 
            // col_SOXE
            // 
            this.col_SOXE.DataPropertyName = "SOXE";
            this.col_SOXE.HeaderText = "Số xe";
            this.col_SOXE.Name = "col_SOXE";
            this.col_SOXE.ReadOnly = true;
            // 
            // col_NOINHAP
            // 
            this.col_NOINHAP.DataPropertyName = "NOINHAP";
            this.col_NOINHAP.HeaderText = "Nơi nhập";
            this.col_NOINHAP.Name = "col_NOINHAP";
            this.col_NOINHAP.ReadOnly = true;
            this.col_NOINHAP.Width = 150;
            // 
            // col_TrangThai
            // 
            this.col_TrangThai.DataPropertyName = "TENTRANGTHAI";
            this.col_TrangThai.HeaderText = "Trạng thái";
            this.col_TrangThai.Name = "col_TrangThai";
            this.col_TrangThai.ReadOnly = true;
            this.col_TrangThai.Width = 110;
            // 
            // his_PanelEx1
            // 
            this.his_PanelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.his_PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.his_PanelEx1.Controls.Add(this.chkAll);
            this.his_PanelEx1.Controls.Add(this.chkMe);
            this.his_PanelEx1.Controls.Add(this.dtiToDate);
            this.his_PanelEx1.Controls.Add(this.dtiFromDate);
            this.his_PanelEx1.Controls.Add(this.his_LabelX2);
            this.his_PanelEx1.Controls.Add(this.his_LabelX1);
            this.his_PanelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.his_PanelEx1.Location = new System.Drawing.Point(0, 33);
            this.his_PanelEx1.Name = "his_PanelEx1";
            this.his_PanelEx1.Size = new System.Drawing.Size(833, 31);
            this.his_PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.his_PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.his_PanelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.his_PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.his_PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.his_PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.his_PanelEx1.Style.GradientAngle = 90;
            this.his_PanelEx1.TabIndex = 4;
            // 
            // chkAll
            // 
            this.chkAll.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.chkAll.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkAll.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.chkAll.Location = new System.Drawing.Point(109, 5);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(69, 20);
            this.chkAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkAll.TabIndex = 2;
            this.chkAll.Text = "Tất cả";
            this.chkAll.TextColor = System.Drawing.Color.Black;
            // 
            // chkMe
            // 
            this.chkMe.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.chkMe.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkMe.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.chkMe.Checked = true;
            this.chkMe.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMe.CheckValue = "Y";
            this.chkMe.ForeColor = System.Drawing.Color.Red;
            this.chkMe.Location = new System.Drawing.Point(9, 5);
            this.chkMe.Name = "chkMe";
            this.chkMe.Size = new System.Drawing.Size(100, 20);
            this.chkMe.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkMe.TabIndex = 2;
            this.chkMe.Text = "Phiếu của tôi";
            this.chkMe.TextColor = System.Drawing.Color.Black;
            // 
            // dtiToDate
            // 
            // 
            // 
            // 
            this.dtiToDate.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtiToDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiToDate.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtiToDate.ButtonDropDown.Visible = true;
            this.dtiToDate.CustomFormat = "dd/MM/yyyy";
            this.dtiToDate.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.dtiToDate.IsPopupCalendarOpen = false;
            this.dtiToDate.Location = new System.Drawing.Point(522, 5);
            // 
            // 
            // 
            this.dtiToDate.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtiToDate.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.dtiToDate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiToDate.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dtiToDate.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtiToDate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtiToDate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtiToDate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtiToDate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtiToDate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtiToDate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtiToDate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiToDate.MonthCalendar.DisplayMonth = new System.DateTime(2015, 6, 1, 0, 0, 0, 0);
            this.dtiToDate.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtiToDate.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtiToDate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtiToDate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtiToDate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtiToDate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiToDate.MonthCalendar.TodayButtonVisible = true;
            this.dtiToDate.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtiToDate.Name = "dtiToDate";
            this.dtiToDate.Size = new System.Drawing.Size(171, 20);
            this.dtiToDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtiToDate.TabIndex = 1;
            // 
            // dtiFromDate
            // 
            // 
            // 
            // 
            this.dtiFromDate.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtiFromDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiFromDate.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtiFromDate.ButtonDropDown.Visible = true;
            this.dtiFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtiFromDate.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.dtiFromDate.IsPopupCalendarOpen = false;
            this.dtiFromDate.Location = new System.Drawing.Point(253, 5);
            // 
            // 
            // 
            this.dtiFromDate.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtiFromDate.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.dtiFromDate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiFromDate.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dtiFromDate.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtiFromDate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtiFromDate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtiFromDate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtiFromDate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtiFromDate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtiFromDate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtiFromDate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiFromDate.MonthCalendar.DisplayMonth = new System.DateTime(2015, 6, 1, 0, 0, 0, 0);
            this.dtiFromDate.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtiFromDate.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtiFromDate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtiFromDate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtiFromDate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtiFromDate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiFromDate.MonthCalendar.TodayButtonVisible = true;
            this.dtiFromDate.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtiFromDate.Name = "dtiFromDate";
            this.dtiFromDate.Size = new System.Drawing.Size(161, 20);
            this.dtiFromDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtiFromDate.TabIndex = 1;
            // 
            // his_LabelX2
            // 
            // 
            // 
            // 
            this.his_LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.his_LabelX2.IsNotNull = false;
            this.his_LabelX2.Location = new System.Drawing.Point(420, 5);
            this.his_LabelX2.Name = "his_LabelX2";
            this.his_LabelX2.PaddingRight = 2;
            this.his_LabelX2.Size = new System.Drawing.Size(102, 20);
            this.his_LabelX2.TabIndex = 0;
            this.his_LabelX2.Text = "Đến ngày:";
            this.his_LabelX2.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // his_LabelX1
            // 
            // 
            // 
            // 
            this.his_LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.his_LabelX1.IsNotNull = false;
            this.his_LabelX1.Location = new System.Drawing.Point(178, 5);
            this.his_LabelX1.Name = "his_LabelX1";
            this.his_LabelX1.PaddingRight = 2;
            this.his_LabelX1.Size = new System.Drawing.Size(75, 20);
            this.his_LabelX1.TabIndex = 0;
            this.his_LabelX1.Text = "Từ ngày:";
            this.his_LabelX1.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // frm_DS_NhapTaiSan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(833, 467);
            this.Controls.Add(this.gvgContainer);
            this.Controls.Add(this.his_PanelEx1);
            this.Controls.Add(this.pnlTitle);
            this.DoubleBuffered = true;
            this.Name = "frm_DS_NhapTaiSan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DANH SÁCH PHIẾU NHẬP";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.pnlTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrid)).EndInit();
            this.his_PanelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtiToDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtiFromDate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        protected E00_Control.his_PanelEx pnlTitle;
        protected E00_Control.his_ButtonX2 btnAdd;
        protected E00_Control.his_ButtonX2 btnEdit;
        protected E00_Control.his_ButtonX2 btnDelete;
        protected E00_Control.his_ButtonX2 btnClose;
        protected E00_Control.his_LabelX lblTitle;
        private E00_ControlAdv.ViewUI.GridContainerAdv gvgContainer;
        private E00_ControlAdv.ViewUI.DataGridViewAdv dgvGrid;
        private E00_Control.his_PanelEx his_PanelEx1;
        private E00_Control.his_CheckBoxX chkAll;
        private E00_Control.his_CheckBoxX chkMe;
        private E00_Control.his_DateTimeInput dtiToDate;
        private E00_Control.his_DateTimeInput dtiFromDate;
        private E00_Control.his_LabelX his_LabelX2;
        private E00_Control.his_LabelX his_LabelX1;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_SOPHIEU;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_NGAYNHAP;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TotalReceived;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TENNCC;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_NGUOIGIAO;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_SDTNGUOIGIAO;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_SOXE;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_NOINHAP;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TrangThai;
    }
}