namespace QuanLyTaiSan.Forms
{
    partial class frm_InventoryBalance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_InventoryBalance));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnClose = new E00_Control.his_ButtonX2();
            this.lblTitle = new E00_Control.his_LabelX(this.components);
            this.pnlTitle = new E00_Control.his_PanelEx();
            this.gvgContainer = new E00_ControlAdv.ViewUI.GridContainerAdv();
            this.dgvGrid = new E00_ControlAdv.ViewUI.DataGridViewAdv();
            this.col_MA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TEN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_KYHIEU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TOTAL_RE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TOTAL_DES = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TOTAL_BANGIAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TOTAL_END = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(772, 5);
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
            this.lblTitle.Size = new System.Drawing.Size(848, 33);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "CÂN BẰNG TÀI SẢN TỒN KHO";
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
            this.pnlTitle.Size = new System.Drawing.Size(848, 33);
            this.pnlTitle.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlTitle.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.pnlTitle.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.pnlTitle.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlTitle.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pnlTitle.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlTitle.Style.GradientAngle = 90;
            this.pnlTitle.TabIndex = 5;
            // 
            // gvgContainer
            // 
            this.gvgContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvgContainer.GridView = this.dgvGrid;
            this.gvgContainer.Location = new System.Drawing.Point(0, 33);
            this.gvgContainer.Name = "gvgContainer";
            this.gvgContainer.Size = new System.Drawing.Size(848, 430);
            this.gvgContainer.TabIndex = 6;
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
            this.col_Total,
            this.col_TOTAL_RE,
            this.col_TOTAL_DES,
            this.col_TOTAL_BANGIAO,
            this.col_TOTAL_END});
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
            this.dgvGrid.Name = "dgvGrid";
            this.dgvGrid.ReadOnly = true;
            this.dgvGrid.RowHeadersWidth = 22;
            this.dgvGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGrid.Size = new System.Drawing.Size(848, 400);
            this.dgvGrid.TabIndex = 8;
            // 
            // col_MA
            // 
            this.col_MA.DataPropertyName = "MA";
            this.col_MA.HeaderText = "Mã tài sản";
            this.col_MA.MinimumWidth = 180;
            this.col_MA.Name = "col_MA";
            this.col_MA.ReadOnly = true;
            this.col_MA.Width = 180;
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
            this.col_KYHIEU.HeaderText = "Ký hiệu";
            this.col_KYHIEU.MinimumWidth = 150;
            this.col_KYHIEU.Name = "col_KYHIEU";
            this.col_KYHIEU.ReadOnly = true;
            this.col_KYHIEU.Width = 150;
            // 
            // col_Total
            // 
            this.col_Total.DataPropertyName = "Total";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "#,##0";
            this.col_Total.DefaultCellStyle = dataGridViewCellStyle2;
            this.col_Total.HeaderText = "Tổng tồn";
            this.col_Total.Name = "col_Total";
            this.col_Total.ReadOnly = true;
            this.col_Total.Width = 80;
            // 
            // col_TOTAL_RE
            // 
            this.col_TOTAL_RE.DataPropertyName = "TOTAL_RE";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "#,##0";
            this.col_TOTAL_RE.DefaultCellStyle = dataGridViewCellStyle3;
            this.col_TOTAL_RE.HeaderText = "Đang nhập";
            this.col_TOTAL_RE.MinimumWidth = 150;
            this.col_TOTAL_RE.Name = "col_TOTAL_RE";
            this.col_TOTAL_RE.ReadOnly = true;
            this.col_TOTAL_RE.Width = 150;
            // 
            // col_TOTAL_DES
            // 
            this.col_TOTAL_DES.DataPropertyName = "TOTAL_DES";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "#,##0";
            this.col_TOTAL_DES.DefaultCellStyle = dataGridViewCellStyle4;
            this.col_TOTAL_DES.HeaderText = "Đang xuất";
            this.col_TOTAL_DES.MinimumWidth = 150;
            this.col_TOTAL_DES.Name = "col_TOTAL_DES";
            this.col_TOTAL_DES.ReadOnly = true;
            this.col_TOTAL_DES.Width = 150;
            // 
            // col_TOTAL_BANGIAO
            // 
            this.col_TOTAL_BANGIAO.DataPropertyName = "TOTAL_BANGIAO";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "#,##0";
            this.col_TOTAL_BANGIAO.DefaultCellStyle = dataGridViewCellStyle5;
            this.col_TOTAL_BANGIAO.HeaderText = "Đang bàn giao";
            this.col_TOTAL_BANGIAO.Name = "col_TOTAL_BANGIAO";
            this.col_TOTAL_BANGIAO.ReadOnly = true;
            this.col_TOTAL_BANGIAO.Width = 150;
            // 
            // col_TOTAL_END
            // 
            this.col_TOTAL_END.DataPropertyName = "TOTAL_END";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "#,##0";
            this.col_TOTAL_END.DefaultCellStyle = dataGridViewCellStyle6;
            this.col_TOTAL_END.HeaderText = "Tồn dự kiến";
            this.col_TOTAL_END.Name = "col_TOTAL_END";
            this.col_TOTAL_END.ReadOnly = true;
            this.col_TOTAL_END.Width = 150;
            // 
            // frm_InventoryBalance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(848, 463);
            this.Controls.Add(this.gvgContainer);
            this.Controls.Add(this.pnlTitle);
            this.DoubleBuffered = true;
            this.Name = "frm_InventoryBalance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CÂN BẰNG TỒN KHO TÀI SẢN";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.pnlTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private E00_ControlAdv.ViewUI.GridContainerAdv gvgContainer;
        private E00_ControlAdv.ViewUI.DataGridViewAdv dgvGrid;
        protected E00_Control.his_PanelEx pnlTitle;
        protected E00_Control.his_ButtonX2 btnClose;
        protected E00_Control.his_LabelX lblTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_MA;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TEN;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_KYHIEU;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Total;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TOTAL_RE;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TOTAL_DES;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TOTAL_BANGIAO;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TOTAL_END;
    }
}