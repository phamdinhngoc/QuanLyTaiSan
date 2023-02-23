namespace QuanLyTaiSan.Forms
{
    partial class frm_StockMovement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_StockMovement));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTitle = new E00_Control.his_PanelEx();
            this.btnClose = new E00_Control.his_ButtonX2();
            this.lblTitle = new E00_Control.his_LabelX(this.components);
            this.gvgContainer = new E00_ControlAdv.ViewUI.GridContainerAdv();
            this.dgvGrid = new E00_ControlAdv.ViewUI.DataGridViewAdv();
            this.his_LabelX1 = new E00_Control.his_LabelX(this.components);
            this.his_LabelX2 = new E00_Control.his_LabelX(this.components);
            this.dtiFromDate = new E00_Control.his_DateTimeInput();
            this.dtiToDate = new E00_Control.his_DateTimeInput();
            this.his_PanelEx1 = new E00_Control.his_PanelEx();
            this.col_MA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TEN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_KYHIEU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TotalBegin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TotalIn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TotalOut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TotalClose = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtiFromDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtiToDate)).BeginInit();
            this.his_PanelEx1.SuspendLayout();
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
            this.lblTitle.Size = new System.Drawing.Size(833, 33);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "BÁO CÁO XUẤT NHẬP TỒN";
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
            this.col_MA,
            this.col_TEN,
            this.col_KYHIEU,
            this.col_TotalBegin,
            this.col_TotalIn,
            this.col_TotalOut,
            this.col_TotalClose});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvGrid.DefaultCellStyle = dataGridViewCellStyle7;
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
            this.dgvGrid.TabIndex = 0;
            // 
            // his_LabelX1
            // 
            // 
            // 
            // 
            this.his_LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.his_LabelX1.IsNotNull = false;
            this.his_LabelX1.Location = new System.Drawing.Point(4, 6);
            this.his_LabelX1.Name = "his_LabelX1";
            this.his_LabelX1.PaddingRight = 2;
            this.his_LabelX1.Size = new System.Drawing.Size(75, 20);
            this.his_LabelX1.TabIndex = 1;
            this.his_LabelX1.Text = "Từ ngày:";
            this.his_LabelX1.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // his_LabelX2
            // 
            // 
            // 
            // 
            this.his_LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.his_LabelX2.IsNotNull = false;
            this.his_LabelX2.Location = new System.Drawing.Point(246, 6);
            this.his_LabelX2.Name = "his_LabelX2";
            this.his_LabelX2.PaddingRight = 2;
            this.his_LabelX2.Size = new System.Drawing.Size(102, 20);
            this.his_LabelX2.TabIndex = 0;
            this.his_LabelX2.Text = "Đến ngày:";
            this.his_LabelX2.TextAlignment = System.Drawing.StringAlignment.Far;
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
            this.dtiFromDate.Location = new System.Drawing.Point(79, 6);
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
            this.dtiFromDate.TabIndex = 2;
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
            this.dtiToDate.Location = new System.Drawing.Point(348, 6);
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
            this.dtiToDate.TabIndex = 3;
            // 
            // his_PanelEx1
            // 
            this.his_PanelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.his_PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
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
            // col_MA
            // 
            this.col_MA.DataPropertyName = "MA";
            this.col_MA.HeaderText = "Ma tài sản";
            this.col_MA.Name = "col_MA";
            this.col_MA.ReadOnly = true;
            this.col_MA.Width = 150;
            // 
            // col_TEN
            // 
            this.col_TEN.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_TEN.DataPropertyName = "TEN";
            dataGridViewCellStyle1.Format = "dd/MM/yyyy";
            this.col_TEN.DefaultCellStyle = dataGridViewCellStyle1;
            this.col_TEN.HeaderText = "Tên tài sản";
            this.col_TEN.Name = "col_TEN";
            this.col_TEN.ReadOnly = true;
            // 
            // col_KYHIEU
            // 
            this.col_KYHIEU.DataPropertyName = "KYHIEU";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "#,##0";
            this.col_KYHIEU.DefaultCellStyle = dataGridViewCellStyle2;
            this.col_KYHIEU.HeaderText = "Ký hiệu";
            this.col_KYHIEU.Name = "col_KYHIEU";
            this.col_KYHIEU.ReadOnly = true;
            this.col_KYHIEU.Width = 150;
            // 
            // col_TotalBegin
            // 
            this.col_TotalBegin.DataPropertyName = "TotalBegin";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "#,##0";
            this.col_TotalBegin.DefaultCellStyle = dataGridViewCellStyle3;
            this.col_TotalBegin.HeaderText = "Tồn đầu";
            this.col_TotalBegin.MinimumWidth = 120;
            this.col_TotalBegin.Name = "col_TotalBegin";
            this.col_TotalBegin.ReadOnly = true;
            this.col_TotalBegin.Width = 120;
            // 
            // col_TotalIn
            // 
            this.col_TotalIn.DataPropertyName = "TotalIn";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "#,##0";
            this.col_TotalIn.DefaultCellStyle = dataGridViewCellStyle4;
            this.col_TotalIn.HeaderText = "Nhập trong kỳ";
            this.col_TotalIn.Name = "col_TotalIn";
            this.col_TotalIn.ReadOnly = true;
            this.col_TotalIn.Width = 120;
            // 
            // col_TotalOut
            // 
            this.col_TotalOut.DataPropertyName = "TotalOut";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "#,##0";
            this.col_TotalOut.DefaultCellStyle = dataGridViewCellStyle5;
            this.col_TotalOut.HeaderText = "Xuất trong kỳ";
            this.col_TotalOut.Name = "col_TotalOut";
            this.col_TotalOut.ReadOnly = true;
            this.col_TotalOut.Width = 120;
            // 
            // col_TotalClose
            // 
            this.col_TotalClose.DataPropertyName = "TotalClose";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "#,##0";
            this.col_TotalClose.DefaultCellStyle = dataGridViewCellStyle6;
            this.col_TotalClose.HeaderText = "Tồn cuối kỳ";
            this.col_TotalClose.Name = "col_TotalClose";
            this.col_TotalClose.ReadOnly = true;
            this.col_TotalClose.Width = 120;
            // 
            // frm_StockMovement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(833, 467);
            this.Controls.Add(this.gvgContainer);
            this.Controls.Add(this.his_PanelEx1);
            this.Controls.Add(this.pnlTitle);
            this.DoubleBuffered = true;
            this.Name = "frm_StockMovement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BÁO CÁO XUẤT NHẬP TỒN";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.pnlTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtiFromDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtiToDate)).EndInit();
            this.his_PanelEx1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        protected E00_Control.his_PanelEx pnlTitle;
        protected E00_Control.his_ButtonX2 btnClose;
        protected E00_Control.his_LabelX lblTitle;
        private E00_ControlAdv.ViewUI.GridContainerAdv gvgContainer;
        private E00_ControlAdv.ViewUI.DataGridViewAdv dgvGrid;
        private E00_Control.his_PanelEx his_PanelEx1;
        private E00_Control.his_DateTimeInput dtiToDate;
        private E00_Control.his_DateTimeInput dtiFromDate;
        private E00_Control.his_LabelX his_LabelX2;
        private E00_Control.his_LabelX his_LabelX1;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_MA;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TEN;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_KYHIEU;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TotalBegin;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TotalIn;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TotalOut;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TotalClose;
    }
}