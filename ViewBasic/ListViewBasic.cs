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
using DevComponents.DotNetBar;
using E00_Base;
using E00_ControlAdv.Interface.Log;
using E00_ControlAdv.Log;
using E00_SafeCacheDataService.Base;
using E00_SafeCacheDataService.Interface;
using HISNgonNgu.Chung;

namespace QuanLyTaiSan.ViewBasic
{
    public partial class ListViewBasic<TInfo> : LViewbasic
        where TInfo : class, IInfo<TInfo>, new()
    {
        //----------------------------------------------

        #region Member
        protected TInfo _info = new TInfo();
        protected Lazy<DataTable> _defaultLazyData;
        protected IAPI<TInfo> _defaultDataService;
        protected bool _isEditing;
        protected E00_SafeCacheDataService.Common.ResultInfo _resultInfo;
        protected Keys _closeKey = (System.Windows.Forms.Keys.Escape);
        #endregion

        //----------------------------------------------

        #region Constructor
        public ListViewBasic():base()
        {
            InitializeComponent();

            
            KeyPress += FKeyPress;
            this.KeyPreview = true;
            btnAdd.Click += (send, e) => OnNew_Clicked();
            btnEdit.Click += (send, e) => OnEdit_Clicked();
            btnDelete.Click += (send, e) => OnDelete_Clicked();
            btnClose.Click += (send, e) => OnClose_Clicked();

            btnAdd.Image = Properties.Resources.Add1;
            btnEdit.Image = Properties.Resources.Edit2;
            btnDelete.Image = Properties.Resources.Delete;
            btnClose.Image = Properties.Resources.Exit;

            ExitEditMode();
        }



        #endregion

        //----------------------------------------------

        #region Public Method

        #endregion

        //----------------------------------------------

        #region Protected Method
        protected override void Initialize()
        {
            try
            {
                base.Initialize();
                this.Load += (send, e) => DataLoading();
                gvgContainer.GridView.SelectionChanged += GridView_SelectionChanged;
                gvgContainer.GridView.MouseDoubleClick += GridView_MouseDoubleClick;
            }
            catch (Exception ex)
            {
                _log.Error("ListViewLeftFilterBasic => GridView_SelectionChanged:" + ex.Message);
            }
        }
        protected void GridView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var info = GetRowSelect();
            if(info != null)
            {
                OnEdit_Clicked();
            }
        }
        protected virtual object GetRowSelect()
        {
            if(gvgContainer.GridView.CurrentRow != null)
            {
                var rowSelect = gvgContainer.GridView.CurrentRow.DataBoundItem as DataRowView;
                if(rowSelect != null)
                {
                    return rowSelect.Row;
                }
                
            }
            return null;
        }
        protected virtual void DataLoading()
        {
            gvgContainer.Initialize("");
            _defaultDataService = new API_Common<TInfo>();
            Synchronized();
        }
        protected virtual void Synchronized()
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
        protected virtual void OnNew_Clicked()
        {
            _info = new TInfo();
            OpenEditForm();
        }
        protected virtual void OnEdit_Clicked()
        {
            var rowSelected = GetRowSelect();
            if(rowSelected != null)
            {
                _info = _defaultDataService.ConvertRowToInfo((DataRow)rowSelected);
                if(_info != null)
                {
                    OpenEditForm();
                }
            }
        }
        protected virtual void OnClose_Clicked()
        {
            this.Close();
        }
        protected virtual void OnDelete_Clicked()
        {
            var rowSelected = GetRowSelect();
            if (rowSelected != null)
            {
               if(TA_MessageBox.MessageBox.Show(string.Format(LNNChung.LInfoDelete,@"Mục đang chọn"),TA_MessageBox.MessageIcon.Question) == DialogResult.Yes)
                {
                    _info = new TInfo();
                    _resultInfo = _defaultDataService.Delete(((DataRow)rowSelected)[_info.PriKeyColumn]);
                    if (_resultInfo.Status)
                    {
                        Synchronized();
                    }
                    else
                    {
                        TA_MessageBox.MessageBox.Show(LNNChung.EUpdateFailed + ": " + _resultInfo.SystemError, TA_MessageBox.MessageIcon.Error);
                    }
                }
            }
        }
        protected virtual void ExitEditMode()
        {
            _isEditing = false;
            btnEdit.Enabled = !_isEditing;
            btnDelete.Enabled = !_isEditing;
        }
        protected virtual void EnterEditMode()
        {
            _isEditing = true;
            btnEdit.Enabled = _isEditing;
            btnDelete.Enabled = _isEditing;
        }
        protected virtual void OpenEditForm()
        {

        }
        protected virtual void GridView_SelectionChanged(object sender, EventArgs e)
        {
            if (GetRowSelect() != null)
            {
                EnterEditMode();
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
            this.btnAdd = new E00_Control.his_ButtonX2();
            this.btnEdit = new E00_Control.his_ButtonX2();
            this.btnDelete = new E00_Control.his_ButtonX2();
            this.btnClose = new E00_Control.his_ButtonX2();
            this.lblTitle = new E00_Control.his_LabelX(this.components);
            this.pnlEdit = new E00_Control.his_PanelEx();
            this.gvgContainer = new E00_ControlAdv.ViewUI.GridContainerAdv();
            this.dgvDefaut = new E00_Control.his_DataGridView();
            this.pnlTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDefaut)).BeginInit();
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
            this.pnlTitle.Size = new System.Drawing.Size(1241, 33);
            this.pnlTitle.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlTitle.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.pnlTitle.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.pnlTitle.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlTitle.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pnlTitle.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlTitle.Style.GradientAngle = 90;
            this.pnlTitle.TabIndex = 0;
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(953, 5);
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
            this.btnEdit.Location = new System.Drawing.Point(1031, 5);
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
            this.btnDelete.Location = new System.Drawing.Point(1098, 5);
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
            this.btnClose.Location = new System.Drawing.Point(1165, 5);
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
            this.lblTitle.Size = new System.Drawing.Size(1241, 33);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "View Basic";
            // 
            // pnlEdit
            // 
            this.pnlEdit.CanvasColor = System.Drawing.SystemColors.Control;
            this.pnlEdit.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pnlEdit.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEdit.Location = new System.Drawing.Point(0, 33);
            this.pnlEdit.Name = "pnlEdit";
            this.pnlEdit.Size = new System.Drawing.Size(1241, 10);
            this.pnlEdit.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlEdit.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pnlEdit.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.pnlEdit.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlEdit.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pnlEdit.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlEdit.Style.GradientAngle = 90;
            this.pnlEdit.TabIndex = 1;
            this.pnlEdit.Visible = false;
            // 
            // gvgContainer
            // 
            this.gvgContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvgContainer.GridView = this.dgvDefaut;
            this.gvgContainer.Location = new System.Drawing.Point(0, 43);
            this.gvgContainer.Name = "gvgContainer";
            this.gvgContainer.Size = new System.Drawing.Size(1241, 620);
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
            this.dgvDefaut.Size = new System.Drawing.Size(1241, 590);
            this.dgvDefaut.TabIndex = 3;
            // 
            // ListViewBasic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1241, 663);
            this.Controls.Add(this.gvgContainer);
            this.Controls.Add(this.pnlEdit);
            this.Controls.Add(this.pnlTitle);
            this.Name = "ListViewBasic";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.pnlTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDefaut)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private IContainer components;
        protected E00_Control.his_DataGridView dgvDefaut;
        protected E00_Control.his_ButtonX2 btnAdd;
        protected E00_Control.his_ButtonX2 btnEdit;
        protected E00_Control.his_ButtonX2 btnDelete;
        protected E00_Control.his_ButtonX2 btnClose;
        protected E00_Control.his_PanelEx pnlTitle;
        protected E00_Control.his_PanelEx pnlEdit;
        protected E00_ControlAdv.ViewUI.GridContainerAdv gvgContainer;
        protected E00_Control.his_LabelX lblTitle;


        //----------------------------------------------

    }
}
