namespace QuanLyTaiSan.Maintenance
{
    partial class Frm_ScheduleListReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_ScheduleListReport));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnNext = new E00_Control.his_ButtonX2();
            this.btnPrevious = new E00_Control.his_ButtonX2();
            this.btnTodate = new E00_Control.his_ButtonX2();
            this.his_LabelX2 = new E00_Control.his_LabelX(this.components);
            this.datDate = new E00_Control.his_DateTimeInput();
            this.btnMaintenance = new E00_Control.his_ButtonX2();
            this.btnAddSchedule = new E00_Control.his_ButtonX2();
            this.pnlDetail = new E00_Control.his_PanelEx();
            this.dgvData = new E00_ControlAdv.ViewUI.DataGridViewAdv();
            this.superTabControlPanel1 = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.superTabItem1 = new DevComponents.DotNetBar.SuperTabItem();
            this.colInfo = new DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn();
            this.col_MATS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_MaVach = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TenTaiSan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TenTang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_KPQL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TenKhoaSD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_NgayBD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_SoNgay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_NgayBT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_ChuKy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTitle.SuspendLayout();
            this.expLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datDate)).BeginInit();
            this.pnlDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.btnNext);
            this.pnlTitle.Controls.Add(this.btnPrevious);
            this.pnlTitle.Controls.Add(this.btnTodate);
            this.pnlTitle.Controls.Add(this.his_LabelX2);
            this.pnlTitle.Controls.Add(this.btnAddSchedule);
            this.pnlTitle.Controls.Add(this.datDate);
            this.pnlTitle.Controls.Add(this.btnMaintenance);
            this.pnlTitle.Size = new System.Drawing.Size(1639, 33);
            this.pnlTitle.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlTitle.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.pnlTitle.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.pnlTitle.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlTitle.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pnlTitle.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlTitle.Style.GradientAngle = 90;
            this.pnlTitle.Controls.SetChildIndex(this.lblTitle, 0);
            this.pnlTitle.Controls.SetChildIndex(this.btnMaintenance, 0);
            this.pnlTitle.Controls.SetChildIndex(this.datDate, 0);
            this.pnlTitle.Controls.SetChildIndex(this.btnAddSchedule, 0);
            this.pnlTitle.Controls.SetChildIndex(this.his_LabelX2, 0);
            this.pnlTitle.Controls.SetChildIndex(this.btnTodate, 0);
            this.pnlTitle.Controls.SetChildIndex(this.btnPrevious, 0);
            this.pnlTitle.Controls.SetChildIndex(this.btnNext, 0);
            // 
            // lblTitle
            // 
            // 
            // 
            // 
            this.lblTitle.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblTitle.Size = new System.Drawing.Size(1639, 33);
            this.lblTitle.Text = "THÔNG TIN TÀI SẢN";
            // 
            // expLeft
            // 
            this.expLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.expLeft.ExpandButtonVisible = false;
            this.expLeft.Size = new System.Drawing.Size(1639, 742);
            this.expLeft.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expLeft.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expLeft.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expLeft.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expLeft.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expLeft.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expLeft.Style.GradientAngle = 90;
            this.expLeft.TitleHeight = 0;
            this.expLeft.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expLeft.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expLeft.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expLeft.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expLeft.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expLeft.TitleStyle.GradientAngle = 90;
            this.expLeft.TitleStyle.MarginLeft = 5;
            this.expLeft.TitleText = "DANH SÁCH THIẾT BỊ";
            this.expLeft.Controls.SetChildIndex(this.gvgContainer, 0);
            // 
            // gvgContainer
            // 
            this.gvgContainer.GridView = this.dgvData;
            this.gvgContainer.Location = new System.Drawing.Point(0, 0);
            this.gvgContainer.Size = new System.Drawing.Size(1639, 742);
            // 
            // expandableSplitter1
            // 
            this.expandableSplitter1.Location = new System.Drawing.Point(0, 33);
            this.expandableSplitter1.Size = new System.Drawing.Size(3, 742);
            this.expandableSplitter1.Visible = false;
            // 
            // pnlEdit
            // 
            this.pnlEdit.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlEdit.Location = new System.Drawing.Point(0, 0);
            this.pnlEdit.Size = new System.Drawing.Size(0, 0);
            this.pnlEdit.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlEdit.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pnlEdit.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.pnlEdit.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlEdit.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pnlEdit.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlEdit.Style.GradientAngle = 90;
            // 
            // btnNext
            // 
            this.btnNext.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.BackColor = System.Drawing.Color.Transparent;
            this.btnNext.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.Image")));
            this.btnNext.ImageFixedSize = new System.Drawing.Size(18, 18);
            this.btnNext.Location = new System.Drawing.Point(1238, 6);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(50, 23);
            this.btnNext.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnNext.TabIndex = 4;
            this.btnNext.TextColor = System.Drawing.Color.White;
            // 
            // btnPrevious
            // 
            this.btnPrevious.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrevious.BackColor = System.Drawing.Color.Transparent;
            this.btnPrevious.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnPrevious.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrevious.Image = ((System.Drawing.Image)(resources.GetObject("btnPrevious.Image")));
            this.btnPrevious.ImageFixedSize = new System.Drawing.Size(18, 18);
            this.btnPrevious.Location = new System.Drawing.Point(1065, 6);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(50, 23);
            this.btnPrevious.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPrevious.TabIndex = 4;
            this.btnPrevious.TextColor = System.Drawing.Color.White;
            // 
            // btnTodate
            // 
            this.btnTodate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnTodate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTodate.BackColor = System.Drawing.Color.Transparent;
            this.btnTodate.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnTodate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTodate.Location = new System.Drawing.Point(1292, 6);
            this.btnTodate.Name = "btnTodate";
            this.btnTodate.Size = new System.Drawing.Size(75, 23);
            this.btnTodate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnTodate.TabIndex = 4;
            this.btnTodate.Text = "Hiện tại";
            this.btnTodate.TextColor = System.Drawing.Color.White;
            // 
            // his_LabelX2
            // 
            this.his_LabelX2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.his_LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.his_LabelX2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.his_LabelX2.ForeColor = System.Drawing.Color.White;
            this.his_LabelX2.IsNotNull = false;
            this.his_LabelX2.Location = new System.Drawing.Point(990, 6);
            this.his_LabelX2.Name = "his_LabelX2";
            this.his_LabelX2.Size = new System.Drawing.Size(75, 23);
            this.his_LabelX2.TabIndex = 3;
            this.his_LabelX2.Text = "Chọn ngày:";
            // 
            // datDate
            // 
            this.datDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.datDate.BackgroundStyle.Class = "DateTimeInputBackground";
            this.datDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.datDate.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.datDate.ButtonDropDown.Visible = true;
            this.datDate.CustomFormat = "dd/MM/yyyy";
            this.datDate.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.datDate.IsPopupCalendarOpen = false;
            this.datDate.Location = new System.Drawing.Point(1118, 8);
            // 
            // 
            // 
            this.datDate.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.datDate.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.datDate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.datDate.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.datDate.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.datDate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.datDate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.datDate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.datDate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.datDate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.datDate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.datDate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.datDate.MonthCalendar.DisplayMonth = new System.DateTime(2015, 6, 1, 0, 0, 0, 0);
            this.datDate.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.datDate.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.datDate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.datDate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.datDate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.datDate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.datDate.MonthCalendar.TodayButtonVisible = true;
            this.datDate.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.datDate.Name = "datDate";
            this.datDate.Size = new System.Drawing.Size(117, 20);
            this.datDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.datDate.TabIndex = 2;
            // 
            // btnMaintenance
            // 
            this.btnMaintenance.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMaintenance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaintenance.BackColor = System.Drawing.Color.Transparent;
            this.btnMaintenance.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnMaintenance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMaintenance.Image = ((System.Drawing.Image)(resources.GetObject("btnMaintenance.Image")));
            this.btnMaintenance.ImageFixedSize = new System.Drawing.Size(20, 20);
            this.btnMaintenance.Location = new System.Drawing.Point(1388, 6);
            this.btnMaintenance.Name = "btnMaintenance";
            this.btnMaintenance.Size = new System.Drawing.Size(121, 23);
            this.btnMaintenance.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMaintenance.TabIndex = 1;
            this.btnMaintenance.Text = "Nhập bảo trì";
            this.btnMaintenance.TextColor = System.Drawing.Color.White;
            // 
            // btnAddSchedule
            // 
            this.btnAddSchedule.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddSchedule.BackColor = System.Drawing.Color.Transparent;
            this.btnAddSchedule.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnAddSchedule.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddSchedule.Image = ((System.Drawing.Image)(resources.GetObject("btnAddSchedule.Image")));
            this.btnAddSchedule.ImageFixedSize = new System.Drawing.Size(20, 22);
            this.btnAddSchedule.Location = new System.Drawing.Point(1513, 6);
            this.btnAddSchedule.Name = "btnAddSchedule";
            this.btnAddSchedule.Size = new System.Drawing.Size(121, 23);
            this.btnAddSchedule.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAddSchedule.TabIndex = 1;
            this.btnAddSchedule.Text = "Lịch bảo trì";
            this.btnAddSchedule.TextColor = System.Drawing.Color.White;
            // 
            // pnlDetail
            // 
            this.pnlDetail.CanvasColor = System.Drawing.SystemColors.Control;
            this.pnlDetail.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pnlDetail.Controls.Add(this.pnlEdit);
            this.pnlDetail.Location = new System.Drawing.Point(500, 33);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Size = new System.Drawing.Size(0, 0);
            this.pnlDetail.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlDetail.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pnlDetail.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.pnlDetail.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlDetail.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pnlDetail.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlDetail.Style.GradientAngle = 90;
            this.pnlDetail.TabIndex = 1;
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.AllowUserToOrderColumns = true;
            this.dgvData.BackgroundColor = System.Drawing.Color.White;
            this.dgvData.BindingSource = null;
            this.dgvData.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colInfo,
            this.col_MATS,
            this.col_MaVach,
            this.col_TenTaiSan,
            this.col_TenTang,
            this.col_KPQL,
            this.col_TenKhoaSD,
            this.col_NgayBD,
            this.col_SoNgay,
            this.col_NgayBT,
            this.col_ChuKy});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.FilterList = null;
            this.dgvData.FormContain = this;
            this.dgvData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvData.IsEnabled = true;
            this.dgvData.IsReadonly = true;
            this.dgvData.IsVisible = true;
            this.dgvData.Location = new System.Drawing.Point(0, 0);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvData.RowHeadersWidth = 22;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(1639, 712);
            this.dgvData.TabIndex = 1;
            // 
            // superTabControlPanel1
            // 
            this.superTabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControlPanel1.Location = new System.Drawing.Point(0, 0);
            this.superTabControlPanel1.Name = "superTabControlPanel1";
            this.superTabControlPanel1.Size = new System.Drawing.Size(1188, 10);
            this.superTabControlPanel1.TabIndex = 1;
            this.superTabControlPanel1.TabItem = this.superTabItem1;
            // 
            // superTabItem1
            // 
            this.superTabItem1.AttachedControl = this.superTabControlPanel1;
            this.superTabItem1.GlobalItem = false;
            this.superTabItem1.Name = "superTabItem1";
            this.superTabItem1.Text = "superTabItem1";
            // 
            // colInfo
            // 
            this.colInfo.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.colInfo.HeaderText = "";
            this.colInfo.Image = ((System.Drawing.Image)(resources.GetObject("colInfo.Image")));
            this.colInfo.Name = "colInfo";
            this.colInfo.ReadOnly = true;
            this.colInfo.Text = null;
            this.colInfo.Width = 40;
            // 
            // col_MATS
            // 
            this.col_MATS.DataPropertyName = "MATAISAN";
            this.col_MATS.HeaderText = "Mã tài sản";
            this.col_MATS.Name = "col_MATS";
            this.col_MATS.ReadOnly = true;
            this.col_MATS.Visible = false;
            this.col_MATS.Width = 120;
            // 
            // col_MaVach
            // 
            this.col_MaVach.DataPropertyName = "MAVACH";
            this.col_MaVach.HeaderText = "Mã vạch";
            this.col_MaVach.Name = "col_MaVach";
            this.col_MaVach.ReadOnly = true;
            this.col_MaVach.Width = 130;
            // 
            // col_TenTaiSan
            // 
            this.col_TenTaiSan.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_TenTaiSan.DataPropertyName = "TENTAISAN";
            this.col_TenTaiSan.HeaderText = "Tên tài sản";
            this.col_TenTaiSan.MinimumWidth = 200;
            this.col_TenTaiSan.Name = "col_TenTaiSan";
            this.col_TenTaiSan.ReadOnly = true;
            // 
            // col_TenTang
            // 
            this.col_TenTang.DataPropertyName = "TENTANG";
            this.col_TenTang.HeaderText = "Vị trí";
            this.col_TenTang.Name = "col_TenTang";
            this.col_TenTang.ReadOnly = true;
            this.col_TenTang.Width = 250;
            // 
            // col_KPQL
            // 
            this.col_KPQL.DataPropertyName = "TENKPQUANLY";
            this.col_KPQL.HeaderText = "Khoa quản lý";
            this.col_KPQL.Name = "col_KPQL";
            this.col_KPQL.ReadOnly = true;
            this.col_KPQL.Width = 250;
            // 
            // col_TenKhoaSD
            // 
            this.col_TenKhoaSD.DataPropertyName = "TENKPSUDUNG";
            this.col_TenKhoaSD.HeaderText = "Khoa sử dụng";
            this.col_TenKhoaSD.Name = "col_TenKhoaSD";
            this.col_TenKhoaSD.ReadOnly = true;
            this.col_TenKhoaSD.Width = 250;
            // 
            // col_NgayBD
            // 
            this.col_NgayBD.DataPropertyName = "NGAYBD";
            dataGridViewCellStyle2.Format = "dd/MM/yyyy";
            this.col_NgayBD.DefaultCellStyle = dataGridViewCellStyle2;
            this.col_NgayBD.HeaderText = "Ngày bảo trì";
            this.col_NgayBD.Name = "col_NgayBD";
            this.col_NgayBD.ReadOnly = true;
            this.col_NgayBD.Width = 90;
            // 
            // col_SoNgay
            // 
            this.col_SoNgay.DataPropertyName = "DIFDAY";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "#,##0";
            this.col_SoNgay.DefaultCellStyle = dataGridViewCellStyle3;
            this.col_SoNgay.HeaderText = "Số ngày";
            this.col_SoNgay.Name = "col_SoNgay";
            this.col_SoNgay.ReadOnly = true;
            this.col_SoNgay.Width = 110;
            // 
            // col_NgayBT
            // 
            this.col_NgayBT.DataPropertyName = "NGAYBTTT";
            dataGridViewCellStyle4.Format = "dd/MM/yyyy";
            this.col_NgayBT.DefaultCellStyle = dataGridViewCellStyle4;
            this.col_NgayBT.HeaderText = "Ngày dự kiến";
            this.col_NgayBT.Name = "col_NgayBT";
            this.col_NgayBT.ReadOnly = true;
            this.col_NgayBT.Width = 95;
            // 
            // col_ChuKy
            // 
            this.col_ChuKy.DataPropertyName = "CHUKY";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "#,##0";
            this.col_ChuKy.DefaultCellStyle = dataGridViewCellStyle5;
            this.col_ChuKy.HeaderText = "Chu kỳ";
            this.col_ChuKy.Name = "col_ChuKy";
            this.col_ChuKy.ReadOnly = true;
            this.col_ChuKy.Width = 80;
            // 
            // Frm_ScheduleListReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1639, 775);
            this.Controls.Add(this.pnlDetail);
            this.Name = "Frm_ScheduleListReport";
            this.Text = "BÁO CÁO BẢO TRÌ THIẾT BỊ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Controls.SetChildIndex(this.pnlTitle, 0);
            this.Controls.SetChildIndex(this.expLeft, 0);
            this.Controls.SetChildIndex(this.pnlDetail, 0);
            this.Controls.SetChildIndex(this.expandableSplitter1, 0);
            this.pnlTitle.ResumeLayout(false);
            this.expLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.datDate)).EndInit();
            this.pnlDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private E00_Control.his_PanelEx pnlDetail;
        private E00_Control.his_DateTimeInput datDate;
        private E00_Control.his_ButtonX2 btnAddSchedule;
        private E00_Control.his_LabelX his_LabelX2;
        private E00_Control.his_ButtonX2 btnNext;
        private E00_Control.his_ButtonX2 btnPrevious;
        private E00_Control.his_ButtonX2 btnTodate;
        private E00_ControlAdv.ViewUI.DataGridViewAdv dgvData;
        private E00_Control.his_ButtonX2 btnMaintenance;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel1;
        private DevComponents.DotNetBar.SuperTabItem superTabItem1;
        private DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn colInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_MATS;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_MaVach;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TenTaiSan;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TenTang;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_KPQL;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TenKhoaSD;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_NgayBD;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_SoNgay;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_NgayBT;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_ChuKy;
    }
}