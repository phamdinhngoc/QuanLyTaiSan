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
using E00_Base;
using E00_ControlAdv.Interface.Log;
using E00_ControlAdv.Log;
using E00_SafeCacheDataService.Base;
using E00_SafeCacheDataService.Common;
using E00_SafeCacheDataService.Interface;
using HISNgonNgu.Chung;
using HISNgonNgu.NoiTru;

namespace QuanLyTaiSan.ViewBasic
{
    public partial class ListViewLeftFilterBasic<FilterInfo,MasterInfo, DetailInfo, ReferenceInfo, TViewEntity> : LViewbasic
        where FilterInfo : class, IInfo<FilterInfo>, new()
        where MasterInfo : class, IInfo<MasterInfo>, new()
        where DetailInfo : class, IInfo<DetailInfo>, new()
        where ReferenceInfo : class, IInfo<ReferenceInfo>, new()
        where TViewEntity : class, IViewEntity<MasterInfo>, new()
    {
        //-------------------------------------------------------------

        #region Member
      
        public MasterInfo Info { get; set; }

        protected bool IsUsingReference = false;
        protected string DisplayMemberMaster = "";
        protected MasterInfo _masterInfo;
        protected DetailInfo _detailInfo;
        protected ReferenceInfo _referencetInfo;
        protected readonly TViewEntity EditingEntity;

        protected IAPI<FilterInfo> _leftDataService;
        protected IAPI<ReferenceInfo> _rightDataService;
        protected IAPI<MasterInfo> _masterDataService;
        protected IAPI<DetailInfo> _detailDataService;

        protected Lazy<DataTable> _lazyDataLeft;
        protected Lazy<DataTable> _lazyDataReference;
        protected bool _isEditing;
        protected bool _isDataChanged;
        protected ResultInfo _resultInfo;

        protected IList<DataRow> _rowSelectedtmp;
        protected bool ExpanlLeft = true;
        protected bool ExpanlRight = true;
        protected Keys _closeKey = (System.Windows.Forms.Keys.Escape);
        #endregion

        //-------------------------------------------------------------

        #region Constructor
        public ListViewLeftFilterBasic():base()
        {
            InitializeComponent();
         
            _syncContext = SynchronizationContext.Current;
            EditingEntity = new TViewEntity();
            EditingEntity.PropertyChanged += EditingEntity_PropertyChanged;
            KeyPress += FKeyPress;
            this.KeyPreview = true;
            //Initialize();


        }
        #endregion

        //-------------------------------------------------------------

        #region Public Method

        #endregion

        //-------------------------------------------------------------

        #region Protected Method
        //-------------------------------------------------------------
        protected override void Initialize()
        {
            try
            {
                base.Initialize();
                Load += (send, e) => DataLoading();
                gvgLeft.GridView.SelectionChanged += GridView_SelectionChanged;
                expLeft.Expanded = ExpanlLeft;
                expRight.Expanded = ExpanlRight;
            }
            catch (Exception ex)
            {
                _log.Error("ListViewLeftFilterBasic => Initialize:" + ex.Message);
            }
       
        }
        protected virtual void InitializeService()
        {
            try
            {
                _leftDataService = new API_Common<FilterInfo>();
                _masterDataService = new API_Common<MasterInfo>();
                _detailDataService = new API_Common<DetailInfo>();
                if (IsUsingReference)
                {
                    _rightDataService = new API_Common<ReferenceInfo>();
                }
            }
            catch (Exception ex)
            {
                _log.Error("ListViewLeftFilterBasic => InitializeService:" + ex.Message);
            }
          
        }
        protected virtual void InitializeGrid()
        {
            try
            {
                gvgLeft.Initialize(_log,"");
                gvgDetail.Initialize(_log);

                expRight.Visible = IsUsingReference;
                if (IsUsingReference)
                {
                    gvgRight.Initialize(_log,"");
                }
            }
            catch (Exception ex)
            {
                _log.Error("ListViewLeftFilterBasic => InitializeGrid:" + ex.Message);
            }
        }
        protected virtual void DataLoading()
        {
            try
            {
            InitializeService();
            InitializeGrid();

            SynchronizedLeft();
            SynchronizedReference();
            ExitEditMode();

            }
            catch (Exception ex)
            {
                _log.Error("ListViewLeftFilterBasic => DataLoading:" + ex.Message);
            }
        }

        //-------------------------------------------------------------

        protected virtual void SynchronizedLeft()
        {
            try
            {

            _lazyDataLeft = new Lazy<DataTable>(() => _leftDataService.Get_Data());
            var _theardRun = new Thread(new ThreadStart(() =>
            {
                var data = _lazyDataLeft.Value;
                _syncContext.Send(state =>
                {
                    gvgLeft.DataSource = data;
                }, null);
            }));
            _theardRun.IsBackground = true;
            _theardRun.Start();

            }
            catch (Exception ex)
            {
                _log.Error("ListViewLeftFilterBasic => SynchronizedLeft:" + ex.Message);
            }
        }
        protected virtual void SynchronizedDetail()
        {

        }
        protected virtual void SynchronizedReference()
        {
            try
            {

            _lazyDataReference = new Lazy<DataTable>(() => _rightDataService.Get_Data());
            var _theardRun = new Thread(new ThreadStart(() => {
                var data = _lazyDataReference.Value;
                _syncContext.Send(state =>
                {
                    gvgRight.DataSource = data;
                }, null);
            }));
            _theardRun.IsBackground = true;
            _theardRun.Start();

            }
            catch (Exception ex)
            {
                _log.Error("ListViewLeftFilterBasic => SynchronizedReference:" + ex.Message);
            }
        }

        //-------------------------------------------------------------

        protected virtual bool ValidateMaster()
        {
            return true;
        }
        protected virtual bool ValidateDetail()
        {
            try
            {
                _rowSelectedtmp = GetRecordReferenceSelect();
                if (EditingEntity.KeyIDValue == null)
                {
                    TA_MessageBox.MessageBox.Show(LNoiTru.LLSelectMaster, TA_MessageBox.MessageIcon.Information);
             
                    return false;
                }
                if ((decimal)EditingEntity.KeyIDValue == 0)
                {
                    TA_MessageBox.MessageBox.Show(LNoiTru.LLSelectMaster, TA_MessageBox.MessageIcon.Information);
          
                    return false;
                }
                if (_rowSelectedtmp == null)
                {
                    TA_MessageBox.MessageBox.Show(LNoiTru.LLSelectDetail, TA_MessageBox.MessageIcon.Information);
                    return false;
                }
                if (_rowSelectedtmp.Count == 0)
                {
                    TA_MessageBox.MessageBox.Show(LNoiTru.LLSelectDetail, TA_MessageBox.MessageIcon.Information);
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                _log.Error("ListViewLeftFilterBasic => ValidateDetail:" + ex.Message);
                return false;
            }
        }
        protected virtual bool ValidateReference()
        {
            return true;
        }
        protected virtual IList<DataRow> GetRecordLeftSelect()
        {
            try
            {
            var list = new List<DataRow>();
            if (gvgLeft.GridView.SelectedRows.Count <= 0) return list;
            foreach (DataGridViewRow item in gvgLeft.GridView.SelectedRows)
            {
                list.Add((item.DataBoundItem as DataRowView).Row);
            }
            return list;
            }
            catch (Exception ex)
            {
                _log.Error("ListViewLeftFilterBasic => GetRecordLeftSelect:" + ex.Message);
                return null;
            }
        }
        protected virtual IList<DataRow> GetRecordDetailSelect()
        {
            try
            {

            var list = new List<DataRow>();
            if (gvgDetail.GridView.SelectedRows.Count <= 0) return list;
            foreach (DataGridViewRow item in gvgDetail.GridView.SelectedRows)
            {
                list.Add((item.DataBoundItem as DataRowView).Row);
            }
            return list;

            }
            catch (Exception ex)
            {
                _log.Error("ListViewLeftFilterBasic => GetRecordDetailSelect:" + ex.Message);
                return null;
            }
        }
        protected virtual IList<DataRow> GetRecordReferenceSelect()
        {
            try
            {

            var list = new List<DataRow>();
            if (gvgRight.GridView.SelectedRows.Count <= 0) return list;
            foreach (DataGridViewRow item in gvgRight.GridView.SelectedRows)
            {
                list.Add((item.DataBoundItem as DataRowView).Row);
            }
            return list;

            }
            catch (Exception ex)
            {
                _log.Error("ListViewLeftFilterBasic => GetRecordReferenceSelect:" + ex.Message);
                return null;
            }
        }
        protected virtual bool InsertDetail(DataRow row)
        {

            return true;
        }
        protected virtual void ExitEditMode()
        {
            _isEditing = false;
            _isDataChanged = false;
        }
        protected virtual void EnterEditMode()
        {
            _isEditing = true;
            _isDataChanged = false;
        }
        protected virtual void LeftSelectionChanged(DataRow row)
        {
            try
            {

            _masterInfo = _masterDataService.ConvertRowToInfo(row);
            if(_masterInfo != null)
            {
                if (EditingEntity.KeyIDValue != _masterInfo.IDValue)
                {
                    EditingEntity.Set(_masterInfo);
                    ExitEditMode();
                    SynchronizedDetail();
                }
            }

            }
            catch (Exception ex)
            {
                _log.Error("ListViewLeftFilterBasic => LeftSelectionChanged:" + ex.Message);
            }
        }

        //-------------------------------------------------------------

        protected virtual void RequestUpdateDetail()
        {
            try
            {

            if (ValidateDetail())
            {
                var _theardRun = new Thread(new ThreadStart(() => {
                    _syncContext.Send(state =>
                    {
                        //gvgDetail.LoadingForm(LNNChung.LProccess);
                        bool isRefresh = false;
                        foreach (var item in _rowSelectedtmp)
                        {
                            bool kt = InsertDetail(item);
                            isRefresh = isRefresh || kt;
                        }
                        if (isRefresh)
                            SynchronizedDetail();
                        //gvgDetail.CloseloadingForm();
                    }, null);
                }));
                _theardRun.IsBackground = true;
                _theardRun.Start();
            }
            }
            catch (Exception ex)
            {
                _log.Error("ListViewLeftFilterBasic => RequestUpdateDetail:" + ex.Message);
            }
        }
        protected virtual void RequestUpdateDetailFiterAll()
        {
            try
            {

            if (ValidateDetail())
            {
                var _theardRun = new Thread(new ThreadStart(() => {
                    _syncContext.Send(state =>
                    {
                        //gvgDetail.LoadingForm(LNNChung.LProccess);
                        bool isRefresh = false;
                        var tableSelect = gvgLeft.FilterDataSource;
                        if (tableSelect == null) return;
                        if (tableSelect.ToTable().Rows.Count == 0) return;
                        foreach (DataRow item in tableSelect.ToTable().Rows)
                        {
                            bool kt = InsertDetail(item);
                            isRefresh = isRefresh || kt;
                        }
                        if (isRefresh)
                            SynchronizedDetail();

                        //gvgDetail.CloseloadingForm();
                    }, null);
                }));
                _theardRun.IsBackground = true;
                _theardRun.Start();
            }

            }
            catch (Exception ex)
            {
                _log.Error("ListViewLeftFilterBasic => RequestUpdateDetailFiterAll:" + ex.Message);
            }
        }
        protected virtual bool RequestUpdateReference()
        {
            try
            {
            if (ValidateReference())
            {
                if ((decimal)(_referencetInfo.IDValue) == 0)
                {
                    _referencetInfo.IDValue = _rightDataService.CreateNewID(20);
                    _resultInfo = _rightDataService.Insert(_referencetInfo);
                }
                else
                {
                    _resultInfo = _rightDataService.Update(_referencetInfo);
                }
                if (_resultInfo.Status)
                {
                    SynchronizedReference();
                    return true;
                }
                else
                {
                    TA_MessageBox.MessageBox.Show(LNNChung.EUpdateFailed + ": " + _resultInfo.SystemError, TA_MessageBox.MessageIcon.Error);
                    return false;
                }
            }
            return true;
            }
            catch (Exception ex)
            {
                _log.Error("ListViewLeftFilterBasic => RequestUpdateDetailFiterAll:" + ex.Message);
                return false;
            }
        }

        //-------------------------------------------------------------

        protected virtual void RemoveDetailSelect()
        {
            try
            {

            var _theardRun = new Thread(new ThreadStart(() => {
                _syncContext.Send(state =>
                {
                    //gvgDetail.LoadingForm(LNNChung.LProccess);
                    _rowSelectedtmp = GetRecordDetailSelect();
                    if (_rowSelectedtmp == null) return;
                    if (_rowSelectedtmp.Count == 0) return;
                    _detailInfo = new DetailInfo();
                    if (TA_MessageBox.MessageBox.Show(string.Format(LNNChung.LInfoDelete, "danh sách mục đang chọn"), TA_MessageBox.MessageIcon.Question) == DialogResult.Yes)
                    {
                        foreach (var item in _rowSelectedtmp)
                        {
                            _resultInfo = _detailDataService.Delete((object)item[_detailInfo.PriKeyColumn]);
                            if (!_resultInfo.Status)
                            {
                                TA_MessageBox.MessageBox.Show(LNNChung.EUpdateFailed + ":" + _resultInfo.SystemError, TA_MessageBox.MessageIcon.Error);
                                break;
                            }
                        }
                        SynchronizedDetail();
                    }

                   // gvgDetail.CloseloadingForm();

                }, null);
            }));
            _theardRun.IsBackground = true;
            _theardRun.Start();

            }
            catch (Exception ex)
            {
                _log.Error("ListViewLeftFilterBasic => RemoveDetailSelect:" + ex.Message);
            }

        }
        protected virtual void RemoveDetailFiterAll()
        {
            try
            {

            var _theardRun = new Thread(new ThreadStart(() => {
                _syncContext.Send(state =>
                {
                    //gvgDetail.LoadingForm(LNNChung.LProccess);
                    var tableSelect = gvgDetail.FilterDataSource;
                    if (tableSelect == null) return;
                    if (tableSelect.ToTable().Rows.Count == 0) return;
                    _detailInfo = new DetailInfo();
                    if (TA_MessageBox.MessageBox.Show(string.Format(LNNChung.LInfoDelete, "tất cả danh sách mục đang chọn không?"), TA_MessageBox.MessageIcon.Question) == DialogResult.Yes)
                    {
                        foreach (DataRow item in tableSelect.ToTable().Rows)
                        {
                            _resultInfo = _detailDataService.Delete((object)item[_detailInfo.PriKeyColumn]);
                            if (!_resultInfo.Status)
                            {
                                // TA_MessageBox.MessageBox.Show(LNNChung.EUpdateFailed + ":" + _resultInfo.SystemError, TA_MessageBox.MessageIcon.Error);
                            }
                        }
                        SynchronizedDetail();
                    }

                    //gvgDetail.CloseloadingForm();

                }, null);
            }));
            _theardRun.IsBackground = true;
            _theardRun.Start();

            }
            catch (Exception ex)
            {
                _log.Error("ListViewLeftFilterBasic => RemoveDetailSelect:" + ex.Message);
            }
        }
        #endregion

        //-------------------------------------------------------------

        #region Private Method
        #endregion

        //-------------------------------------------------------------

        #region Event Control
        private void FKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)_closeKey)
            {
                Close();
            }
        }
        protected virtual void GridView_SelectionChanged(object sender, EventArgs e)
        {
            try
            {

            if(gvgLeft.GridView.CurrentRow != null)
            {
                var row = gvgLeft.GridView.CurrentRow.DataBoundItem as DataRowView;
                if(row != null)
                {
                    LeftSelectionChanged(row.Row);
                }
            }
            }
            catch (Exception ex)
            {
                _log.Error("ListViewLeftFilterBasic => GridView_SelectionChanged:" + ex.Message);
            }
        }

        protected virtual void EditingEntity_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _isDataChanged = true;
        }
        protected virtual void BtnAddReference()
        {
            RequestUpdateReference();
        }
        #endregion

        //-------------------------------------------------------------


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTitle = new E00_Control.his_PanelEx();
            this.lblTitle = new E00_Control.his_LabelX(this.components);
            this.expLeft = new E00_Control.his_ExpandablePanel();
            this.pnlMainEdit = new E00_Control.his_PanelEx();
            this.pnlLeft = new E00_Control.his_PanelEx();
            this.gvgLeft = new E00_ControlAdv.ViewUI.GridContainerAdv();
            this.dgvLeft = new E00_ControlAdv.ViewUI.DataGridViewAdv();
            this.expandableSplitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.expRight = new E00_Control.his_ExpandablePanel();
            this.pnlGrifRight = new E00_Control.his_PanelEx();
            this.gvgRight = new E00_ControlAdv.ViewUI.GridContainerAdv();
            this.dgvReference = new E00_Control.his_DataGridView();
            this.pnlEditRight = new E00_Control.his_PanelEx();
            this.expandableSplitter2 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.expMain = new E00_Control.his_ExpandablePanel();
            this.gvgDetail = new E00_ControlAdv.ViewUI.GridContainerAdv();
            this.dgvMain = new E00_Control.his_DataGridView();
            this.pnlEdit = new E00_Control.his_PanelEx();
            this.pnlTitle.SuspendLayout();
            this.expLeft.SuspendLayout();
            this.pnlMainEdit.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeft)).BeginInit();
            this.expRight.SuspendLayout();
            this.pnlGrifRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReference)).BeginInit();
            this.expMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
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
            this.pnlTitle.Size = new System.Drawing.Size(1269, 33);
            this.pnlTitle.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlTitle.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.pnlTitle.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.pnlTitle.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlTitle.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pnlTitle.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlTitle.Style.GradientAngle = 90;
            this.pnlTitle.TabIndex = 3;
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
            this.lblTitle.Size = new System.Drawing.Size(1269, 33);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "View Basic";
            // 
            // expLeft
            // 
            this.expLeft.CanvasColor = System.Drawing.SystemColors.Control;
            this.expLeft.CollapseDirection = DevComponents.DotNetBar.eCollapseDirection.RightToLeft;
            this.expLeft.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.expLeft.Controls.Add(this.pnlMainEdit);
            this.expLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.expLeft.HideControlsWhenCollapsed = true;
            this.expLeft.Location = new System.Drawing.Point(0, 33);
            this.expLeft.Name = "expLeft";
            this.expLeft.Size = new System.Drawing.Size(380, 677);
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
            this.expLeft.TitleText = "DANH SÁCH";
            // 
            // pnlMainEdit
            // 
            this.pnlMainEdit.CanvasColor = System.Drawing.SystemColors.Control;
            this.pnlMainEdit.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pnlMainEdit.Controls.Add(this.pnlLeft);
            this.pnlMainEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainEdit.Location = new System.Drawing.Point(0, 26);
            this.pnlMainEdit.Name = "pnlMainEdit";
            this.pnlMainEdit.Size = new System.Drawing.Size(380, 651);
            this.pnlMainEdit.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlMainEdit.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pnlMainEdit.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.pnlMainEdit.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlMainEdit.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pnlMainEdit.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlMainEdit.Style.GradientAngle = 90;
            this.pnlMainEdit.TabIndex = 3;
            // 
            // pnlLeft
            // 
            this.pnlLeft.CanvasColor = System.Drawing.SystemColors.Control;
            this.pnlLeft.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pnlLeft.Controls.Add(this.gvgLeft);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(380, 651);
            this.pnlLeft.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlLeft.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pnlLeft.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.pnlLeft.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlLeft.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pnlLeft.Style.BorderWidth = 0;
            this.pnlLeft.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlLeft.Style.GradientAngle = 90;
            this.pnlLeft.TabIndex = 0;
            // 
            // gvgLeft
            // 
            this.gvgLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvgLeft.GridView = this.dgvLeft;
            this.gvgLeft.Location = new System.Drawing.Point(0, 0);
            this.gvgLeft.MinimumSize = new System.Drawing.Size(10, 36);
            this.gvgLeft.Name = "gvgLeft";
            this.gvgLeft.Size = new System.Drawing.Size(380, 651);
            this.gvgLeft.TabIndex = 3;
            // 
            // dgvLeft
            // 
            this.dgvLeft.AllowUserToAddRows = false;
            this.dgvLeft.AllowUserToDeleteRows = false;
            this.dgvLeft.AllowUserToOrderColumns = true;
            this.dgvLeft.BackgroundColor = System.Drawing.Color.White;
            this.dgvLeft.BindingSource = null;
            this.dgvLeft.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dgvLeft.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLeft.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLeft.FilterList = null;
            this.dgvLeft.FormContain = this;
            this.dgvLeft.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvLeft.IsEnabled = true;
            this.dgvLeft.IsReadonly = false;
            this.dgvLeft.IsVisible = true;
            this.dgvLeft.Location = new System.Drawing.Point(0, 0);
            this.dgvLeft.Name = "dgvLeft";
            this.dgvLeft.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLeft.Size = new System.Drawing.Size(380, 621);
            this.dgvLeft.TabIndex = 3;
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
            this.expandableSplitter1.Location = new System.Drawing.Point(380, 33);
            this.expandableSplitter1.Name = "expandableSplitter1";
            this.expandableSplitter1.Size = new System.Drawing.Size(3, 677);
            this.expandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.expandableSplitter1.TabIndex = 6;
            this.expandableSplitter1.TabStop = false;
            // 
            // expRight
            // 
            this.expRight.CanvasColor = System.Drawing.SystemColors.Control;
            this.expRight.CollapseDirection = DevComponents.DotNetBar.eCollapseDirection.LeftToRight;
            this.expRight.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.expRight.Controls.Add(this.pnlGrifRight);
            this.expRight.Controls.Add(this.pnlEditRight);
            this.expRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.expRight.HideControlsWhenCollapsed = true;
            this.expRight.Location = new System.Drawing.Point(1025, 33);
            this.expRight.Name = "expRight";
            this.expRight.Size = new System.Drawing.Size(244, 677);
            this.expRight.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expRight.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expRight.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expRight.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expRight.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expRight.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expRight.Style.GradientAngle = 90;
            this.expRight.TabIndex = 8;
            this.expRight.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expRight.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expRight.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expRight.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expRight.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expRight.TitleStyle.GradientAngle = 90;
            this.expRight.TitleStyle.MarginLeft = 10;
            this.expRight.TitleText = "DANH SÁCH";
            // 
            // pnlGrifRight
            // 
            this.pnlGrifRight.CanvasColor = System.Drawing.SystemColors.Control;
            this.pnlGrifRight.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pnlGrifRight.Controls.Add(this.gvgRight);
            this.pnlGrifRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrifRight.Location = new System.Drawing.Point(0, 26);
            this.pnlGrifRight.Name = "pnlGrifRight";
            this.pnlGrifRight.Size = new System.Drawing.Size(244, 651);
            this.pnlGrifRight.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlGrifRight.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pnlGrifRight.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.pnlGrifRight.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlGrifRight.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pnlGrifRight.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlGrifRight.Style.GradientAngle = 90;
            this.pnlGrifRight.TabIndex = 2;
            // 
            // gvgRight
            // 
            this.gvgRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvgRight.GridView = this.dgvReference;
            this.gvgRight.Location = new System.Drawing.Point(0, 0);
            this.gvgRight.MinimumSize = new System.Drawing.Size(10, 36);
            this.gvgRight.Name = "gvgRight";
            this.gvgRight.Size = new System.Drawing.Size(244, 651);
            this.gvgRight.TabIndex = 0;
            // 
            // dgvReference
            // 
            this.dgvReference.AllowUserToAddRows = false;
            this.dgvReference.AllowUserToDeleteRows = false;
            this.dgvReference.AllowUserToOrderColumns = true;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.LightCyan;
            this.dgvReference.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvReference.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvReference.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvReference.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvReference.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgvReference.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReference.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvReference.Location = new System.Drawing.Point(0, 0);
            this.dgvReference.MultiSelect = false;
            this.dgvReference.Name = "dgvReference";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvReference.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.dgvReference.RowsDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvReference.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReference.Size = new System.Drawing.Size(244, 621);
            this.dgvReference.TabIndex = 3;
            // 
            // pnlEditRight
            // 
            this.pnlEditRight.CanvasColor = System.Drawing.SystemColors.Control;
            this.pnlEditRight.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pnlEditRight.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEditRight.Location = new System.Drawing.Point(0, 26);
            this.pnlEditRight.Name = "pnlEditRight";
            this.pnlEditRight.Size = new System.Drawing.Size(244, 0);
            this.pnlEditRight.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlEditRight.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pnlEditRight.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pnlEditRight.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlEditRight.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pnlEditRight.Style.BorderWidth = 0;
            this.pnlEditRight.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlEditRight.Style.GradientAngle = 90;
            this.pnlEditRight.TabIndex = 1;
            this.pnlEditRight.Visible = false;
            // 
            // expandableSplitter2
            // 
            this.expandableSplitter2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter2.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter2.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandableSplitter2.Dock = System.Windows.Forms.DockStyle.Right;
            this.expandableSplitter2.ExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter2.ExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter2.ExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.expandableSplitter2.ExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter2.GripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.expandableSplitter2.GripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter2.GripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.expandableSplitter2.GripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.expandableSplitter2.HotBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(151)))), ((int)(((byte)(61)))));
            this.expandableSplitter2.HotBackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(94)))));
            this.expandableSplitter2.HotBackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2;
            this.expandableSplitter2.HotBackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground;
            this.expandableSplitter2.HotExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter2.HotExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter2.HotExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.expandableSplitter2.HotExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter2.HotGripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter2.HotGripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter2.HotGripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.expandableSplitter2.HotGripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.expandableSplitter2.Location = new System.Drawing.Point(1022, 33);
            this.expandableSplitter2.Name = "expandableSplitter2";
            this.expandableSplitter2.Size = new System.Drawing.Size(3, 677);
            this.expandableSplitter2.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.expandableSplitter2.TabIndex = 10;
            this.expandableSplitter2.TabStop = false;
            // 
            // expMain
            // 
            this.expMain.CanvasColor = System.Drawing.SystemColors.Control;
            this.expMain.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.expMain.Controls.Add(this.gvgDetail);
            this.expMain.Controls.Add(this.pnlEdit);
            this.expMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.expMain.ExpandButtonVisible = false;
            this.expMain.HideControlsWhenCollapsed = true;
            this.expMain.Location = new System.Drawing.Point(383, 33);
            this.expMain.Name = "expMain";
            this.expMain.Size = new System.Drawing.Size(639, 677);
            this.expMain.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expMain.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expMain.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expMain.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expMain.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expMain.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expMain.Style.GradientAngle = 90;
            this.expMain.TabIndex = 11;
            this.expMain.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expMain.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expMain.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expMain.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expMain.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expMain.TitleStyle.GradientAngle = 90;
            this.expMain.TitleStyle.MarginLeft = 10;
            this.expMain.TitleText = "CHI TIẾT";
            // 
            // gvgDetail
            // 
            this.gvgDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvgDetail.GridView = this.dgvMain;
            this.gvgDetail.Location = new System.Drawing.Point(0, 126);
            this.gvgDetail.Name = "gvgDetail";
            this.gvgDetail.Size = new System.Drawing.Size(639, 551);
            this.gvgDetail.TabIndex = 2;
            // 
            // dgvMain
            // 
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.AllowUserToDeleteRows = false;
            this.dgvMain.AllowUserToOrderColumns = true;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightCyan;
            this.dgvMain.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMain.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMain.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMain.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvMain.Location = new System.Drawing.Point(0, 0);
            this.dgvMain.MultiSelect = false;
            this.dgvMain.Name = "dgvMain";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMain.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.dgvMain.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMain.Size = new System.Drawing.Size(639, 521);
            this.dgvMain.TabIndex = 3;
            // 
            // pnlEdit
            // 
            this.pnlEdit.CanvasColor = System.Drawing.SystemColors.Control;
            this.pnlEdit.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pnlEdit.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEdit.Location = new System.Drawing.Point(0, 26);
            this.pnlEdit.Name = "pnlEdit";
            this.pnlEdit.Size = new System.Drawing.Size(639, 100);
            this.pnlEdit.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlEdit.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pnlEdit.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.pnlEdit.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlEdit.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pnlEdit.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlEdit.Style.GradientAngle = 90;
            this.pnlEdit.TabIndex = 1;
            // 
            // ListViewLeftFilterBasic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1269, 710);
            this.Controls.Add(this.expMain);
            this.Controls.Add(this.expandableSplitter2);
            this.Controls.Add(this.expRight);
            this.Controls.Add(this.expandableSplitter1);
            this.Controls.Add(this.expLeft);
            this.Controls.Add(this.pnlTitle);
            this.Name = "ListViewLeftFilterBasic";
            this.Text = "ListViewLeftFilterBasic";
            this.pnlTitle.ResumeLayout(false);
            this.expLeft.ResumeLayout(false);
            this.pnlMainEdit.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeft)).EndInit();
            this.expRight.ResumeLayout(false);
            this.pnlGrifRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReference)).EndInit();
            this.expMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
          protected E00_Control.his_ExpandablePanel expMain;
        protected E00_ControlAdv.ViewUI.GridContainerAdv gvgDetail;
        protected E00_Control.his_PanelEx pnlEdit;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter2;
        protected E00_Control.his_ExpandablePanel expRight;
        protected E00_Control.his_PanelEx pnlGrifRight;
        protected E00_ControlAdv.ViewUI.GridContainerAdv gvgRight;
        protected E00_Control.his_PanelEx pnlEditRight;
        private E00_Control.his_DataGridView dgvMain;
        private E00_Control.his_DataGridView dgvReference;
        protected E00_Control.his_PanelEx pnlTitle;
        protected E00_Control.his_LabelX lblTitle;
        protected E00_Control.his_ExpandablePanel expLeft;
        private E00_Control.his_PanelEx pnlMainEdit;
        private E00_Control.his_PanelEx pnlLeft;
        protected E00_ControlAdv.ViewUI.GridContainerAdv gvgLeft;
        private E00_ControlAdv.ViewUI.DataGridViewAdv dgvLeft;
        private IContainer components;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;
    }
}
