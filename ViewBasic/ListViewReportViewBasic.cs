using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using E00_SafeCacheDataService.Base;
using E00_SafeCacheDataService.Interface;

namespace QuanLyTaiSan.ViewBasic
{
    public partial class ListViewReportViewBasic<TInfo> : LViewbasic
        where TInfo : class, IInfo<TInfo>, new()
    {
        //----------------------------------------------

        #region Member
        protected Lazy<DataTable> _defaultLazyData;
        protected E00_Control.his_DataGridView dgvDefaut;
        protected IAPI<TInfo> _defaultDataService;
        private IContainer components;
        protected E00_Control.his_ExpandablePanel expLeft;
        protected DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;
        protected Keys _closeKey = (System.Windows.Forms.Keys.Escape);
        #endregion

        //----------------------------------------------

        #region Constructor
        public ListViewReportViewBasic():base()
        {
            InitializeComponent();
            KeyPress += FKeyPress;
            this.KeyPreview = true;
        }

        #endregion

        //----------------------------------------------

        #region Public Method

        #endregion

        //----------------------------------------------

        #region Protected Method
        protected override void Initialize()
        {
            base.Initialize();
            this.Load += (send, e) => DataLoading();
        }
        protected virtual void DataLoading()
        {
            try
            {
                gvgContainer.Initialize(_log, "");
                _defaultDataService = new API_Common<TInfo>();
                Synchronized();
            }
            catch (Exception ex)
            {
                _log.Error("ListViewReportBasic => DataLoading:" + ex.Message);
            }

        }
        protected virtual void Synchronized()
        {
            try
            {

                gvgContainer.StartWaiting("");
                pnlTitle.Enabled = false;
                _defaultLazyData = new Lazy<DataTable>(() => _defaultDataService.Get_Data());
                var _theardRun = new Thread(new ThreadStart(() =>
                {
                    var data = _defaultLazyData.Value;
                    _syncContext.Send(state =>
                    {
                        gvgContainer.DataSource = data;
                        pnlTitle.Enabled = true;
                    }, null);
                }));
                _theardRun.IsBackground = true;
                _theardRun.Start();

            }
            catch (Exception ex)
            {
                _log.Error("ListViewReportBasic => Synchronized:" + ex.Message);
            }
        }

        #endregion

        //----------------------------------------------

        #region Private Method
        private void FKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)_closeKey)
            {
                Close();
            }
        }
        #endregion

        //----------------------------------------------


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTitle = new E00_Control.his_PanelEx();
            this.lblTitle = new E00_Control.his_LabelX(this.components);
            this.pnlEdit = new E00_Control.his_PanelEx();
            this.gvgContainer = new E00_ControlAdv.ViewUI.GridContainerAdv();
            this.dgvDefaut = new E00_Control.his_DataGridView();
            this.expLeft = new E00_Control.his_ExpandablePanel();
            this.expandableSplitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.pnlTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDefaut)).BeginInit();
            this.expLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTitle
            // 
            this.pnlTitle.CanvasColor = System.Drawing.SystemColors.Control;
            this.pnlTitle.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(867, 33);
            this.pnlTitle.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlTitle.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.pnlTitle.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.pnlTitle.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlTitle.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pnlTitle.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlTitle.Style.GradientAngle = 90;
            this.pnlTitle.TabIndex = 0;
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
            this.lblTitle.Size = new System.Drawing.Size(867, 33);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "View Basic";
            // 
            // pnlEdit
            // 
            this.pnlEdit.CanvasColor = System.Drawing.SystemColors.Control;
            this.pnlEdit.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pnlEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlEdit.Location = new System.Drawing.Point(544, 33);
            this.pnlEdit.Name = "pnlEdit";
            this.pnlEdit.Size = new System.Drawing.Size(323, 451);
            this.pnlEdit.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlEdit.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pnlEdit.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.pnlEdit.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlEdit.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pnlEdit.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlEdit.Style.GradientAngle = 90;
            this.pnlEdit.TabIndex = 1;
            // 
            // gvgContainer
            // 
            this.gvgContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvgContainer.GridView = this.dgvDefaut;
            this.gvgContainer.Location = new System.Drawing.Point(0, 26);
            this.gvgContainer.Name = "gvgContainer";
            this.gvgContainer.Size = new System.Drawing.Size(541, 425);
            this.gvgContainer.TabIndex = 2;
            // 
            // dgvDefaut
            // 
            this.dgvDefaut.AllowUserToAddRows = false;
            this.dgvDefaut.AllowUserToDeleteRows = false;
            this.dgvDefaut.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
            this.dgvDefaut.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDefaut.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDefaut.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDefaut.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDefaut.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDefaut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDefaut.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvDefaut.Location = new System.Drawing.Point(0, 0);
            this.dgvDefaut.MultiSelect = false;
            this.dgvDefaut.Name = "dgvDefaut";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDefaut.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.dgvDefaut.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvDefaut.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDefaut.Size = new System.Drawing.Size(541, 395);
            this.dgvDefaut.TabIndex = 3;
            // 
            // expLeft
            // 
            this.expLeft.CanvasColor = System.Drawing.SystemColors.Control;
            this.expLeft.CollapseDirection = DevComponents.DotNetBar.eCollapseDirection.RightToLeft;
            this.expLeft.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.expLeft.Controls.Add(this.gvgContainer);
            this.expLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.expLeft.HideControlsWhenCollapsed = true;
            this.expLeft.Location = new System.Drawing.Point(0, 33);
            this.expLeft.Name = "expLeft";
            this.expLeft.Size = new System.Drawing.Size(541, 451);
            this.expLeft.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expLeft.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expLeft.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expLeft.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expLeft.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expLeft.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expLeft.Style.GradientAngle = 90;
            this.expLeft.TabIndex = 3;
            this.expLeft.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.expLeft.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expLeft.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expLeft.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expLeft.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expLeft.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expLeft.TitleStyle.GradientAngle = 90;
            this.expLeft.TitleText = "Title Bar";
            // 
            // expandableSplitter1
            // 
            this.expandableSplitter1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandableSplitter1.ExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.ExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.ExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.expandableSplitter1.ExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter1.GripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.expandableSplitter1.GripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter1.GripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.expandableSplitter1.GripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.expandableSplitter1.HotBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(151)))), ((int)(((byte)(61)))));
            this.expandableSplitter1.HotBackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(94)))));
            this.expandableSplitter1.HotBackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2;
            this.expandableSplitter1.HotBackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground;
            this.expandableSplitter1.HotExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.HotExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.HotExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.expandableSplitter1.HotExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter1.HotGripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.HotGripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.HotGripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.expandableSplitter1.HotGripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.expandableSplitter1.Location = new System.Drawing.Point(541, 33);
            this.expandableSplitter1.Name = "expandableSplitter1";
            this.expandableSplitter1.Size = new System.Drawing.Size(3, 451);
            this.expandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.expandableSplitter1.TabIndex = 4;
            this.expandableSplitter1.TabStop = false;
            // 
            // ListViewReportViewBasic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(867, 484);
            this.Controls.Add(this.pnlEdit);
            this.Controls.Add(this.expandableSplitter1);
            this.Controls.Add(this.expLeft);
            this.Controls.Add(this.pnlTitle);
            this.Name = "ListViewReportViewBasic";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.pnlTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDefaut)).EndInit();
            this.expLeft.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected E00_Control.his_PanelEx pnlTitle;
        protected E00_Control.his_PanelEx pnlEdit;
        protected E00_ControlAdv.ViewUI.GridContainerAdv gvgContainer;
        protected E00_Control.his_LabelX lblTitle;


        //----------------------------------------------

    }
}
