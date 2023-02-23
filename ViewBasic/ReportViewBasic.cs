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
    public partial class ReportViewBasic<TInfo> : LViewbasic
        where TInfo : class, IInfo<TInfo>, new()
    {
        //----------------------------------------------

        #region Member
        protected Lazy<DataTable> _defaultLazyData;
        protected E00_Control.his_DataGridView dgvDefaut;
        protected IAPI<TInfo> _defaultDataService;
        protected Keys _closeKey = (System.Windows.Forms.Keys.Escape);
        #endregion

        //----------------------------------------------

        #region Constructor
        public ReportViewBasic():base()
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
            this.pnlEdit = new E00_Control.his_PanelEx();
            this.expandableSplitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.expLeft = new E00_Control.his_ExpandablePanel();
            this.gvgContainer = new E00_ControlAdv.ViewUI.GridContainerAdv();
            this.pnlTitle = new E00_Control.his_PanelEx();
            this.lblTitle = new E00_Control.his_LabelX(this.components);
            this.expLeft.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlEdit
            // 
            this.pnlEdit.CanvasColor = System.Drawing.SystemColors.Control;
            this.pnlEdit.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pnlEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlEdit.Location = new System.Drawing.Point(544, 33);
            this.pnlEdit.Name = "pnlEdit";
            this.pnlEdit.Size = new System.Drawing.Size(480, 500);
            this.pnlEdit.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlEdit.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pnlEdit.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.pnlEdit.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlEdit.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pnlEdit.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlEdit.Style.GradientAngle = 90;
            this.pnlEdit.TabIndex = 6;
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
            this.expandableSplitter1.Size = new System.Drawing.Size(3, 500);
            this.expandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.expandableSplitter1.TabIndex = 5;
            this.expandableSplitter1.TabStop = false;
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
            this.expLeft.Size = new System.Drawing.Size(541, 500);
            this.expLeft.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expLeft.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expLeft.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expLeft.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expLeft.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expLeft.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expLeft.Style.GradientAngle = 90;
            this.expLeft.TabIndex = 4;
            this.expLeft.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expLeft.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expLeft.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expLeft.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expLeft.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expLeft.TitleStyle.GradientAngle = 90;
            this.expLeft.TitleStyle.MarginLeft = 10;
            this.expLeft.TitleText = "Title Bar";
            // 
            // gvgContainer
            // 
            this.gvgContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvgContainer.GridView = null;
            this.gvgContainer.Location = new System.Drawing.Point(0, 26);
            this.gvgContainer.Name = "gvgContainer";
            this.gvgContainer.Size = new System.Drawing.Size(541, 474);
            this.gvgContainer.TabIndex = 2;
            // 
            // pnlTitle
            // 
            this.pnlTitle.CanvasColor = System.Drawing.SystemColors.Control;
            this.pnlTitle.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(1024, 33);
            this.pnlTitle.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlTitle.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.pnlTitle.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.pnlTitle.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlTitle.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pnlTitle.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlTitle.Style.GradientAngle = 90;
            this.pnlTitle.TabIndex = 1;
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
            this.lblTitle.Size = new System.Drawing.Size(1024, 33);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "View Basic";
            // 
            // ReportViewBasic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1024, 533);
            this.Controls.Add(this.pnlEdit);
            this.Controls.Add(this.expandableSplitter1);
            this.Controls.Add(this.expLeft);
            this.Controls.Add(this.pnlTitle);
            this.Name = "ReportViewBasic";
            this.Text = "ReportViewBasic";
            this.expLeft.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        protected E00_Control.his_PanelEx pnlTitle;
        protected E00_Control.his_LabelX lblTitle;
        protected E00_Control.his_ExpandablePanel expLeft;
        protected E00_ControlAdv.ViewUI.GridContainerAdv gvgContainer;
        protected DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;
        protected E00_Control.his_PanelEx pnlEdit;
        private System.ComponentModel.IContainer components = null;
        #endregion
    }
}
