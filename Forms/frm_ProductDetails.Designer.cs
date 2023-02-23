namespace QuanLyTaiSan.Forms
{
    partial class frm_ProductDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_ProductDetails));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.his_PanelEx1 = new E00_Control.his_PanelEx();
            this.lblTitle = new E00_Control.his_LabelX(this.components);
            this.his_PanelEx2 = new E00_Control.his_PanelEx();
            this.btnClose = new E00_Control.his_ButtonX2();
            this.btnDismiss = new E00_Control.his_ButtonX2();
            this.btnSave = new E00_Control.his_ButtonX2();
            this.dgvData = new E00_Control.his_DataGridView();
            this.col_MaVach = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_ViTri = new DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn();
            this.btnAllLocation = new DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn();
            this.col_TinhTrang = new DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn();
            this.col_KyHieu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_MoTa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.his_PanelEx1.SuspendLayout();
            this.his_PanelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // his_PanelEx1
            // 
            this.his_PanelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.his_PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.his_PanelEx1.Controls.Add(this.lblTitle);
            this.his_PanelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.his_PanelEx1.Location = new System.Drawing.Point(0, 0);
            this.his_PanelEx1.Name = "his_PanelEx1";
            this.his_PanelEx1.Size = new System.Drawing.Size(941, 28);
            this.his_PanelEx1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.his_PanelEx1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.his_PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.his_PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.his_PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.his_PanelEx1.Style.GradientAngle = 90;
            this.his_PanelEx1.TabIndex = 0;
            // 
            // lblTitle
            // 
            // 
            // 
            // 
            this.lblTitle.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.IsNotNull = false;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.PaddingLeft = 10;
            this.lblTitle.Size = new System.Drawing.Size(941, 28);
            this.lblTitle.TabIndex = 0;
            // 
            // his_PanelEx2
            // 
            this.his_PanelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.his_PanelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.his_PanelEx2.Controls.Add(this.btnClose);
            this.his_PanelEx2.Controls.Add(this.btnDismiss);
            this.his_PanelEx2.Controls.Add(this.btnSave);
            this.his_PanelEx2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.his_PanelEx2.Location = new System.Drawing.Point(0, 487);
            this.his_PanelEx2.Name = "his_PanelEx2";
            this.his_PanelEx2.Size = new System.Drawing.Size(941, 32);
            this.his_PanelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.his_PanelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.his_PanelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.his_PanelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.his_PanelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.his_PanelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.his_PanelEx2.Style.GradientAngle = 90;
            this.his_PanelEx2.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(537, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 26);
            this.btnClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Thoát";
            this.btnClose.TextColor = System.Drawing.Color.Navy;
            // 
            // btnDismiss
            // 
            this.btnDismiss.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDismiss.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDismiss.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnDismiss.Image = ((System.Drawing.Image)(resources.GetObject("btnDismiss.Image")));
            this.btnDismiss.Location = new System.Drawing.Point(451, 3);
            this.btnDismiss.Name = "btnDismiss";
            this.btnDismiss.Size = new System.Drawing.Size(80, 26);
            this.btnDismiss.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDismiss.TabIndex = 0;
            this.btnDismiss.Text = "Bỏ qua";
            this.btnDismiss.TextColor = System.Drawing.Color.Navy;
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(365, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 26);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Lưu";
            this.btnSave.TextColor = System.Drawing.Color.Navy;
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.LightCyan;
            this.dgvData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvData.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_MaVach,
            this.col_ViTri,
            this.btnAllLocation,
            this.col_TinhTrang,
            this.col_KyHieu,
            this.col_MoTa});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvData.GridColor = System.Drawing.Color.Thistle;
            this.dgvData.Location = new System.Drawing.Point(0, 28);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvData.RowHeadersWidth = 22;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.dgvData.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvData.Size = new System.Drawing.Size(941, 459);
            this.dgvData.TabIndex = 3;
            // 
            // col_MaVach
            // 
            this.col_MaVach.DataPropertyName = "MAVACH";
            this.col_MaVach.HeaderText = "Mã vạch";
            this.col_MaVach.Name = "col_MaVach";
            this.col_MaVach.ReadOnly = true;
            this.col_MaVach.Width = 200;
            // 
            // col_ViTri
            // 
            this.col_ViTri.DataPropertyName = "VITRI";
            this.col_ViTri.DropDownHeight = 106;
            this.col_ViTri.DropDownWidth = 121;
            this.col_ViTri.FillWeight = 150F;
            this.col_ViTri.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.col_ViTri.HeaderText = "Vị trí";
            this.col_ViTri.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.col_ViTri.IntegralHeight = false;
            this.col_ViTri.ItemHeight = 15;
            this.col_ViTri.Name = "col_ViTri";
            this.col_ViTri.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.col_ViTri.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.col_ViTri.Width = 150;
            // 
            // btnAllLocation
            // 
            this.btnAllLocation.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnAllLocation.HeaderText = "";
            this.btnAllLocation.Image = ((System.Drawing.Image)(resources.GetObject("btnAllLocation.Image")));
            this.btnAllLocation.Name = "btnAllLocation";
            this.btnAllLocation.Text = null;
            this.btnAllLocation.Width = 40;
            // 
            // col_TinhTrang
            // 
            this.col_TinhTrang.DataPropertyName = "TINHTRANG";
            this.col_TinhTrang.DropDownHeight = 106;
            this.col_TinhTrang.DropDownWidth = 121;
            this.col_TinhTrang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.col_TinhTrang.HeaderText = "Tình trạng";
            this.col_TinhTrang.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.col_TinhTrang.IntegralHeight = false;
            this.col_TinhTrang.ItemHeight = 15;
            this.col_TinhTrang.Name = "col_TinhTrang";
            this.col_TinhTrang.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.col_TinhTrang.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.col_TinhTrang.Width = 120;
            // 
            // col_KyHieu
            // 
            this.col_KyHieu.DataPropertyName = "KYHIEU";
            this.col_KyHieu.HeaderText = "Đặc trưng";
            this.col_KyHieu.Name = "col_KyHieu";
            this.col_KyHieu.Width = 150;
            // 
            // col_MoTa
            // 
            this.col_MoTa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_MoTa.DataPropertyName = "MOTA";
            this.col_MoTa.HeaderText = "Ghi chú";
            this.col_MoTa.Name = "col_MoTa";
            // 
            // frm_ProductDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(941, 519);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.his_PanelEx2);
            this.Controls.Add(this.his_PanelEx1);
            this.DoubleBuffered = true;
            this.Name = "frm_ProductDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CHI TIẾT TÀI SẢN";
            this.his_PanelEx1.ResumeLayout(false);
            this.his_PanelEx2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private E00_Control.his_PanelEx his_PanelEx1;
        private E00_Control.his_PanelEx his_PanelEx2;
        private E00_Control.his_DataGridView dgvData;
        private E00_Control.his_LabelX lblTitle;
        private E00_Control.his_ButtonX2 btnClose;
        private E00_Control.his_ButtonX2 btnSave;
        private E00_Control.his_ButtonX2 btnDismiss;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_MaVach;
        private DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn col_ViTri;
        private DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn btnAllLocation;
        private DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn col_TinhTrang;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_KyHieu;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_MoTa;
    }
}