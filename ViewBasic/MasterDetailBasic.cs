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
using E00_Helpers.Helpers;
using E00_SafeCacheDataService.Base;
using E00_SafeCacheDataService.Common;
using E00_SafeCacheDataService.Interface;
using HISNgonNgu.Chung;
using HISNgonNgu.NoiTru;
using QuanLyTaiSan.ViewBasic;

namespace QuanLyTaiSan.ViewBasic
{
    public class MasterDetailBasic<LeftInfo,MasterInfo,DetailInfo,RightInfo, TViewEntity> : EViewBasic
        where LeftInfo : class, IInfo<LeftInfo>, new()
        where MasterInfo : class, IInfo<MasterInfo>, new()
        where DetailInfo : class, IInfo<DetailInfo>, new()
        where RightInfo : class, IInfo<RightInfo>, new()
        where TViewEntity : class, IViewEntity<MasterInfo>, new()
    {
        //-------------------------------------------------------------

        #region Member
        public MasterInfo Info { get; set; }

        protected bool IsUsingReference = false;
        protected string DisplayMemberMaster = "";
        protected MasterInfo _masterInfo;
        protected DetailInfo _detailInfo;
        protected RightInfo _referencetInfo;
        protected readonly TViewEntity EditingEntity;

        protected IAPI<LeftInfo> _leftDataService;
        protected IAPI<RightInfo> _rightDataService;
        protected IAPI<MasterInfo> _masterDataService;
        protected IAPI<DetailInfo> _detailDataService;

        protected Lazy<DataTable> _lazyDataLeft;
        protected SynchronizationContext _syncContext;
        protected bool _isEditing;
        protected bool _isDataChanged;
        protected ResultInfo _resultInfo;
        protected E00_ControlAdv.ViewUI.GridContainerAdv gvgLeft;
        protected E00_Control.his_PanelEx pnlGridMid;
        protected E00_ControlAdv.ViewUI.GridContainerAdv gvgDetail;
        protected E00_Control.his_PanelEx pnlGrifRight;
        protected E00_ControlAdv.ViewUI.GridContainerAdv gvgRight;
        private E00_Control.his_DataGridView dgvDetail;
        private E00_Control.his_DataGridView dgvLeft;
        private E00_Control.his_DataGridView dgvReference;
        private E00_Control.his_DataGridView his_DataGridView1;
        protected IList<DataRow> _rowSelectedtmp;
        protected Keys _closeKey = (System.Windows.Forms.Keys.Escape);
        #endregion

        //-------------------------------------------------------------

        #region Constructor
        public MasterDetailBasic():base()
        {
           
            InitializeComponent();
            this.ActiveControl = cboData;
            _syncContext = SynchronizationContext.Current;
            KeyPress += FKeyPress;
            this.KeyPreview = true;
            EditingEntity = new TViewEntity();
            EditingEntity.PropertyChanged += EditingEntity_PropertyChanged;

            this.btnAddAll.Image = Properties.Resources.movetoall;
            this.btnAddSelect.Image = Properties.Resources.moveto;
            this.btnRemove.Image = Properties.Resources.remove;
            this.btnRemoveAll.Image = Properties.Resources.removetoall;

            //Initialize();

            this.btnAddSelect.Click += (send, e) => RequestUpdateDetail();
            this.btnAddAll.Click += (send, e) => RequestUpdateDetailFiterAll();
            this.btnRemove.Click += (send, e) => RemoveDetailSelect();
            this.btnRemoveAll.Click += (send, e) => RemoveDetailFiterAll();
            this.btnAddReference.Click += (send, e) => BtnAddReference();


        }

        #endregion

        //-------------------------------------------------------------

        #region Public Method

        #endregion

        //-------------------------------------------------------------

        #region Protected Method
        //-------------------------------------------------------------

        protected override void BoQua()
        {
            try
            {

            if (_isDataChanged)
            {
                if (TA_MessageBox.MessageBox.Show(LNNChung.LDataChanged, TA_MessageBox.MessageIcon.Question) != DialogResult.Yes)
                {
                    return;
                }
            }
            base.BoQua();
            if (_masterInfo != null)
            {
                EditingEntity.Set(_masterInfo);
            }
            ExitEditMode();
            }
            catch (Exception ex)
            {
                _log.Error("MasterDetailBasic => BoQua:" + ex.Message);
            }

        }
        protected override void Xoa()
        {
            try
            {

            MasterDelected();
            base.Xoa();

            }
            catch (Exception ex)
            {
                _log.Error("MasterDetailBasic => Xoa:" + ex.Message);
            }
        }
        protected override void Them()
        {
            try
            {
                base.Them();
                _masterInfo = new MasterInfo();
                EditingEntity.Set(_masterInfo);
                EnterEditMode();
                SynchronizedDetail();
                SynchronizedReference();
                
            }
            catch (Exception ex)
            {
                _log.Error("MasterDetailBasic => Them:" + ex.Message);
            }
        }

        //-------------------------------------------------------------
        protected override void Initialize()
        {
            base.Initialize();
            Load += (send, e) => DataLoading();
            
        }
        protected virtual void InitializeService()
        {
            try
            {

            _leftDataService = new API_Common<LeftInfo>();
            
            _masterDataService = new API_Common<MasterInfo>();
            _detailDataService = new API_Common<DetailInfo>();
            if (IsUsingReference)
            {
                _rightDataService = new API_Common<RightInfo>();
            }
            }
            catch (Exception ex)
            {
                _log.Error("MasterDetailBasic => InitializeService:" + ex.Message);
            }
        }
        protected virtual void InitializeGrid()
        {
            try
            {
            gvgLeft.Initialize("");
            gvgDetail.Initialize();

            expRight.Visible = IsUsingReference;
            if (IsUsingReference)
            {
                gvgRight.Initialize();
            }
            }
            catch (Exception ex)
            {
                _log.Error("MasterDetailBasic => InitializeGrid:" + ex.Message);
            }
        }
        protected virtual void DataLoading()
        {
            try
            {

            InitializeService();
            InitializeGrid();
            ExitEditMode();
            SynchronizedLeft();

            SynchronizedMaster();
            
              
            }
            catch (Exception ex)
            {
                _log.Error("MasterDetailBasic => DataLoading:" + ex.Message);
            }
        }

        //-------------------------------------------------------------

        protected virtual void SynchronizedLeft()
        {
            try
            {
            _lazyDataLeft = new Lazy<DataTable>(() => _leftDataService.Get_Data());
            var _theardRun = new Thread(new ThreadStart(() => {
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
                _log.Error("MasterDetailBasic => SynchronizedLeft:" + ex.Message);
            }

        }
        protected virtual void SynchronizedMaster()
        {
            try
            {
            _masterInfo = new MasterInfo();
            cboData.DataSource = _masterDataService.Get_DataInfo("");
            cboData.ValueMember = nameof(_masterInfo.IDValue);
            cboData.DisplayMember = DisplayMemberMaster;
            if (Info != null)
            {
                cboData.SelectedValue = Info.IDValue;
                Info = null;
            }
                cboData.SelectedIndexChanged -= (send, e) => MasterSelectedChanged();
                cboData.SelectedIndexChanged += (send, e) => MasterSelectedChanged();
            }
            catch (Exception ex)
            {
                _log.Error("MasterDetailBasic => SynchronizedMaster:" + ex.Message);
            }
        }
        protected virtual void SynchronizedDetail()
        {
            
        }
        protected virtual void SynchronizedReference()
        {
           
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
            _rowSelectedtmp = GetRecordLeftSelect();

                if (EditingEntity.KeyIDValue == null)
                {
                    bool kt = RequestUpdateMaster();
                    if (kt)
                    {
                        btnSua.PerformClick();
                        btnBoQua.Enabled = false;
                        ValidateDetail();
                    }
                    else
                    {
                        //TA_MessageBox.MessageBox.Show(LNoiTru.LLSelectMaster, TA_MessageBox.MessageIcon.Information);
                        //cboData.Focus();
                        //cboData.DroppedDown = true;
                        return false;
                    }

                }
                if ((decimal)EditingEntity.KeyIDValue == 0)
                {
                    bool kt = RequestUpdateMaster();
                    if (kt)
                    {
                        btnSua.PerformClick();
                        btnBoQua.Enabled = false;
                        ValidateDetail();
                    }
                    else
                    {
                        //TA_MessageBox.MessageBox.Show(LNoiTru.LLSelectMaster, TA_MessageBox.MessageIcon.Information);
                        //cboData.Focus();
                        //cboData.DroppedDown = true;
                        return false;
                    }
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
                _log.Error("MasterDetailBasic => ValidateDetail:" + ex.Message);
                return false;
            }
        }
        protected virtual bool ValidateReference()
        {
            try
            {

            _referencetInfo = new RightInfo();
            if (EditingEntity.KeyIDValue == null)
            {
                TA_MessageBox.MessageBox.Show(LNoiTru.LLSelectMaster, TA_MessageBox.MessageIcon.Information);
                cboData.Focus();
                cboData.DroppedDown = true;
                return false;
            }
            if ((decimal)EditingEntity.KeyIDValue == 0)
            {
                TA_MessageBox.MessageBox.Show(LNoiTru.LLSelectMaster, TA_MessageBox.MessageIcon.Information);
                cboData.Focus();
                cboData.DroppedDown = true;
                return false;
            }
            return true;
            }
            catch (Exception ex)
            {
                _log.Error("MasterDetailBasic => ValidateReference:" + ex.Message);
                return false;
            }
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
                _log.Error("MasterDetailBasic => GetRecordLeftSelect:" + ex.Message);
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
                _log.Error("MasterDetailBasic => GetRecordDetailSelect:" + ex.Message);
                return null;
            }
        }
        protected virtual bool InsertDetail(DataRow row)
        {

            return true;
        }
        protected virtual void ExitEditMode()
        {
            try
            {
            _isEditing = false;
            _isDataChanged = false;
            cboData.Enabled = !_isEditing;
            btnAddAll.Enabled = _isEditing;
            btnAddSelect.Enabled = _isEditing;
            btnRemove.Enabled = _isEditing;
            btnRemoveAll.Enabled = _isEditing;
            btnAddReference.Enabled = _isEditing;
            btnSua.Enabled = ((decimal)EditingEntity.KeyIDValue) > 0;
            }
            catch (Exception ex)
            {
                _log.Error("MasterDetailBasic => ExitEditMode:" + ex.Message);
            }
        }
        protected virtual void EnterEditMode()
        {
            try
            {
            _isEditing = true;
            _isDataChanged = false;
            cboData.Enabled = !_isEditing;
            btnAddAll.Enabled = _isEditing;
            btnAddSelect.Enabled = _isEditing;
            btnRemove.Enabled = _isEditing;
            btnRemoveAll.Enabled = _isEditing;
            btnSua.Enabled = !_isEditing;
            btnAddReference.Enabled = _isEditing;
            }
            catch (Exception ex)
            {
                _log.Error("MasterDetailBasic => ExitEditMode:" + ex.Message);
            }
        }

        //-------------------------------------------------------------

        protected virtual bool RequestUpdateMaster()
        {
            try
            {
            if (ValidateMaster())
            {
                if ((decimal)(EditingEntity.KeyIDValue) == 0)
                {
                    EditingEntity.KeyIDValue = _masterDataService.CreateNewID(20);
                    _resultInfo = _masterDataService.Insert(EditingEntity.Info);
                }
                else
                {
                    _resultInfo = _masterDataService.Update(EditingEntity.Info);
                }
                if (_resultInfo.Status)
                {
                    ExitEditMode();
                    Info = new MasterInfo();
                    Info.Copy(EditingEntity.Info);
                    SynchronizedMaster();
                    return true;
                }
                else
                {
                    TA_MessageBox.MessageBox.Show(LNNChung.EUpdateFailed + ": " + _resultInfo.SystemError, TA_MessageBox.MessageIcon.Error);
                    return false;
                }
            }
            return false;
            }
            catch (Exception ex)
            {
                _log.Error("MasterDetailBasic => RequestUpdateMaster:" + ex.Message);
                return false;
            }
        }
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
                _log.Error("MasterDetailBasic => RequestUpdateDetail:" + ex.Message);
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
                _log.Error("MasterDetailBasic => RequestUpdateDetailFiterAll:" + ex.Message);
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
                _log.Error("MasterDetailBasic => RequestUpdateReference:" + ex.Message);
                return false;
            }
        }

        //-------------------------------------------------------------

        protected virtual void RemoveDetailSelect()
        {
            try
            {
                var _theardRun = new Thread(new ThreadStart(() =>
                {
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

                        //gvgDetail.CloseloadingForm();

                    }, null);
                }));
                _theardRun.IsBackground = true;
                _theardRun.Start();
            }
            catch (Exception ex)
            {
                _log.Error("MasterDetailBasic => RemoveDetailSelect:" + ex.Message);
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
                _log.Error("MasterDetailBasic => RemoveDetailFiterAll:" + ex.Message);
            }
        }
        protected virtual void MasterSelectedChanged()
        {
            try
            {
            _masterInfo = cboData.SelectedItem as MasterInfo;
            if (_masterInfo != null)
            {
                if (EditingEntity.KeyIDValue != _masterInfo.IDValue)
                {
                    EditingEntity.Set(_masterInfo);
                    ExitEditMode();
                    SynchronizedDetail();
                    SynchronizedReference();
                    btnSua.Enabled = true;
                    btnXoa.Enabled = true;
                }
            }
            }
            catch (Exception ex)
            {
                _log.Error("MasterDetailBasic => MasterSelectedChanged:" + ex.Message);
            }
        }
        protected virtual void MasterDelected()
        {
            try
            {

            if((decimal)EditingEntity.KeyIDValue > 0)
            {
                if(TA_MessageBox.MessageBox.Show(string.Format(LNNChung.LInfoDelete,@"mục đang chọn"),TA_MessageBox.MessageIcon.Question) == DialogResult.Yes)
                {
                        _resultInfo = _detailDataService.Delete(nameof(_masterInfo.PriKeyColumn) + "=" + EditingEntity.KeyIDValue);
                    _resultInfo = _masterDataService.Delete(EditingEntity.KeyIDValue);
                    if (_resultInfo.Status)
                    {
                        SynchronizedMaster();
                    }else
                    {
                        TA_MessageBox.MessageBox.Show(LNNChung.EUpdateFailed + ": " + _resultInfo.SystemError, TA_MessageBox.MessageIcon.Error);

                    }
                }
            }
            }
            catch (Exception ex)
            {
                _log.Error("MasterDetailBasic => MasterDelected:" + ex.Message);
            }
        }
        #endregion

        //-------------------------------------------------------------

        #region Private Method
        #endregion

        //-------------------------------------------------------------

        #region Event Control
        protected virtual void EditingEntity_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _isDataChanged = true;
        }
        protected virtual void BtnAddReference()
        {
            RequestUpdateReference();
        }
        private void FKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)_closeKey)
            {
                Close();
            }
        }
        #endregion

        //-------------------------------------------------------------

        #region Designer Form
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlEditMain = new E00_Control.his_PanelEx();
            this.pnlGrid = new E00_Control.his_PanelEx();
            this.expandableSplitter3 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.expandableSplitter2 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.expDetail = new E00_Control.his_ExpandablePanel();
            this.pnlGridMid = new E00_Control.his_PanelEx();
            this.gvgDetail = new E00_ControlAdv.ViewUI.GridContainerAdv();
            this.dgvDetail = new E00_Control.his_DataGridView();
            this.pnlDetail = new E00_Control.his_PanelEx();
            this.expRight = new E00_Control.his_ExpandablePanel();
            this.pnlGrifRight = new E00_Control.his_PanelEx();
            this.gvgRight = new E00_ControlAdv.ViewUI.GridContainerAdv();
            this.dgvReference = new E00_Control.his_DataGridView();
            this.pnlEditRight = new E00_Control.his_PanelEx();
            this.btnAddReference = new E00_Control.his_ButtonX2();
            this.expLeft = new E00_Control.his_ExpandablePanel();
            this.pnlMainEdit = new E00_Control.his_PanelEx();
            this.pnlLeft = new E00_Control.his_PanelEx();
            this.gvgLeft = new E00_ControlAdv.ViewUI.GridContainerAdv();
            this.dgvLeft = new E00_Control.his_DataGridView();
            this.pnlControl = new E00_Control.his_PanelEx();
            this.btnRemoveAll = new E00_Control.his_ButtonX2();
            this.btnRemove = new E00_Control.his_ButtonX2();
            this.btnAddSelect = new E00_Control.his_ButtonX2();
            this.btnAddAll = new E00_Control.his_ButtonX2();
            this.pnlEdit = new E00_Control.his_PanelEx();
            this.cboData = new E00_Control.his_ComboboxX();
            this.his_LabelX7 = new E00_Control.his_LabelX(this.components);
            this.his_DataGridView1 = new E00_Control.his_DataGridView();
            this.pnlButton.SuspendLayout();
            this.pnlEditMain.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            this.expDetail.SuspendLayout();
            this.pnlGridMid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).BeginInit();
            this.expRight.SuspendLayout();
            this.pnlGrifRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReference)).BeginInit();
            this.pnlEditRight.SuspendLayout();
            this.expLeft.SuspendLayout();
            this.pnlMainEdit.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeft)).BeginInit();
            this.pnlControl.SuspendLayout();
            this.pnlEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.his_DataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlButton
            // 
            this.pnlButton.Size = new System.Drawing.Size(1173, 45);
            // 
            // pnlEditMain
            // 
            this.pnlEditMain.CanvasColor = System.Drawing.SystemColors.Control;
            this.pnlEditMain.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pnlEditMain.Controls.Add(this.pnlGrid);
            this.pnlEditMain.Controls.Add(this.pnlEdit);
            this.pnlEditMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlEditMain.Location = new System.Drawing.Point(2, 47);
            this.pnlEditMain.Name = "pnlEditMain";
            this.pnlEditMain.Size = new System.Drawing.Size(1173, 626);
            this.pnlEditMain.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlEditMain.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pnlEditMain.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.pnlEditMain.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlEditMain.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pnlEditMain.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlEditMain.Style.GradientAngle = 90;
            this.pnlEditMain.TabIndex = 5;
            // 
            // pnlGrid
            // 
            this.pnlGrid.CanvasColor = System.Drawing.SystemColors.Control;
            this.pnlGrid.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pnlGrid.Controls.Add(this.expandableSplitter3);
            this.pnlGrid.Controls.Add(this.expandableSplitter2);
            this.pnlGrid.Controls.Add(this.expDetail);
            this.pnlGrid.Controls.Add(this.expRight);
            this.pnlGrid.Controls.Add(this.expLeft);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(0, 88);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(1173, 538);
            this.pnlGrid.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlGrid.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pnlGrid.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.pnlGrid.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlGrid.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pnlGrid.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlGrid.Style.GradientAngle = 90;
            this.pnlGrid.TabIndex = 4;
            // 
            // expandableSplitter3
            // 
            this.expandableSplitter3.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter3.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter3.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandableSplitter3.Dock = System.Windows.Forms.DockStyle.Right;
            this.expandableSplitter3.ExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter3.ExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter3.ExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.expandableSplitter3.ExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter3.GripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.expandableSplitter3.GripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter3.GripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.expandableSplitter3.GripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.expandableSplitter3.HotBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(151)))), ((int)(((byte)(61)))));
            this.expandableSplitter3.HotBackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(94)))));
            this.expandableSplitter3.HotBackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2;
            this.expandableSplitter3.HotBackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground;
            this.expandableSplitter3.HotExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter3.HotExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter3.HotExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.expandableSplitter3.HotExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter3.HotGripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter3.HotGripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter3.HotGripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.expandableSplitter3.HotGripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.expandableSplitter3.Location = new System.Drawing.Point(927, 0);
            this.expandableSplitter3.Name = "expandableSplitter3";
            this.expandableSplitter3.Size = new System.Drawing.Size(3, 538);
            this.expandableSplitter3.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.expandableSplitter3.TabIndex = 9;
            this.expandableSplitter3.TabStop = false;
            // 
            // expandableSplitter2
            // 
            this.expandableSplitter2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter2.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter2.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
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
            this.expandableSplitter2.Location = new System.Drawing.Point(417, 0);
            this.expandableSplitter2.Name = "expandableSplitter2";
            this.expandableSplitter2.Size = new System.Drawing.Size(3, 538);
            this.expandableSplitter2.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.expandableSplitter2.TabIndex = 8;
            this.expandableSplitter2.TabStop = false;
            // 
            // expDetail
            // 
            this.expDetail.CanvasColor = System.Drawing.SystemColors.Control;
            this.expDetail.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.expDetail.Controls.Add(this.pnlGridMid);
            this.expDetail.Controls.Add(this.pnlDetail);
            this.expDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.expDetail.ExpandButtonVisible = false;
            this.expDetail.HideControlsWhenCollapsed = true;
            this.expDetail.Location = new System.Drawing.Point(417, 0);
            this.expDetail.Name = "expDetail";
            this.expDetail.Size = new System.Drawing.Size(513, 538);
            this.expDetail.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expDetail.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expDetail.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expDetail.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expDetail.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expDetail.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expDetail.Style.GradientAngle = 90;
            this.expDetail.TabIndex = 6;
            this.expDetail.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expDetail.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expDetail.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expDetail.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expDetail.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expDetail.TitleStyle.GradientAngle = 90;
            this.expDetail.TitleStyle.MarginLeft = 10;
            this.expDetail.TitleText = "CHI TIẾT";
            // 
            // pnlGridMid
            // 
            this.pnlGridMid.CanvasColor = System.Drawing.SystemColors.Control;
            this.pnlGridMid.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pnlGridMid.Controls.Add(this.gvgDetail);
            this.pnlGridMid.Location = new System.Drawing.Point(0, 66);
            this.pnlGridMid.Name = "pnlGridMid";
            this.pnlGridMid.Size = new System.Drawing.Size(513, 472);
            this.pnlGridMid.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlGridMid.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pnlGridMid.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.pnlGridMid.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlGridMid.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pnlGridMid.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlGridMid.Style.GradientAngle = 90;
            this.pnlGridMid.TabIndex = 4;
            // 
            // gvgDetail
            // 
            this.gvgDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvgDetail.GridView = this.dgvDetail;
            this.gvgDetail.Location = new System.Drawing.Point(0, 0);
            this.gvgDetail.MinimumSize = new System.Drawing.Size(10, 36);
            this.gvgDetail.Name = "gvgDetail";
            this.gvgDetail.Size = new System.Drawing.Size(513, 472);
            this.gvgDetail.TabIndex = 0;
            // 
            // dgvDetail
            // 
            this.dgvDetail.AllowUserToAddRows = false;
            this.dgvDetail.AllowUserToDeleteRows = false;
            this.dgvDetail.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
            this.dgvDetail.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDetail.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDetail.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetail.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvDetail.Location = new System.Drawing.Point(0, 0);
            this.dgvDetail.MultiSelect = false;
            this.dgvDetail.Name = "dgvDetail";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetail.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.dgvDetail.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetail.Size = new System.Drawing.Size(513, 442);
            this.dgvDetail.TabIndex = 0;
            this.dgvDetail.Visible = false;
            // 
            // pnlDetail
            // 
            this.pnlDetail.CanvasColor = System.Drawing.SystemColors.Control;
            this.pnlDetail.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pnlDetail.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDetail.Location = new System.Drawing.Point(0, 26);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Size = new System.Drawing.Size(513, 34);
            this.pnlDetail.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlDetail.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pnlDetail.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pnlDetail.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlDetail.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pnlDetail.Style.BorderWidth = 0;
            this.pnlDetail.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlDetail.Style.GradientAngle = 90;
            this.pnlDetail.TabIndex = 3;
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
            this.expRight.Location = new System.Drawing.Point(930, 0);
            this.expRight.Name = "expRight";
            this.expRight.Size = new System.Drawing.Size(243, 538);
            this.expRight.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expRight.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expRight.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expRight.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expRight.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expRight.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expRight.Style.GradientAngle = 90;
            this.expRight.TabIndex = 7;
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
            this.pnlGrifRight.Location = new System.Drawing.Point(0, 60);
            this.pnlGrifRight.Name = "pnlGrifRight";
            this.pnlGrifRight.Size = new System.Drawing.Size(243, 478);
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
            this.gvgRight.Size = new System.Drawing.Size(243, 478);
            this.gvgRight.TabIndex = 0;
            // 
            // dgvReference
            // 
            this.dgvReference.AllowUserToAddRows = false;
            this.dgvReference.AllowUserToDeleteRows = false;
            this.dgvReference.AllowUserToOrderColumns = true;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.LightCyan;
            this.dgvReference.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvReference.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvReference.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvReference.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvReference.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvReference.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReference.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvReference.Location = new System.Drawing.Point(0, 0);
            this.dgvReference.MultiSelect = false;
            this.dgvReference.Name = "dgvReference";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvReference.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.dgvReference.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvReference.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReference.Size = new System.Drawing.Size(243, 448);
            this.dgvReference.TabIndex = 0;
            this.dgvReference.Visible = false;
            // 
            // pnlEditRight
            // 
            this.pnlEditRight.CanvasColor = System.Drawing.SystemColors.Control;
            this.pnlEditRight.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pnlEditRight.Controls.Add(this.btnAddReference);
            this.pnlEditRight.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEditRight.Location = new System.Drawing.Point(0, 26);
            this.pnlEditRight.Name = "pnlEditRight";
            this.pnlEditRight.Size = new System.Drawing.Size(243, 34);
            this.pnlEditRight.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlEditRight.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pnlEditRight.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pnlEditRight.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlEditRight.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pnlEditRight.Style.BorderWidth = 0;
            this.pnlEditRight.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlEditRight.Style.GradientAngle = 90;
            this.pnlEditRight.TabIndex = 1;
            // 
            // btnAddReference
            // 
            this.btnAddReference.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddReference.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnAddReference.Location = new System.Drawing.Point(6, 8);
            this.btnAddReference.Name = "btnAddReference";
            this.btnAddReference.Size = new System.Drawing.Size(75, 23);
            this.btnAddReference.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAddReference.TabIndex = 0;
            this.btnAddReference.Text = "Thêm";
            // 
            // expLeft
            // 
            this.expLeft.CanvasColor = System.Drawing.SystemColors.Control;
            this.expLeft.CollapseDirection = DevComponents.DotNetBar.eCollapseDirection.RightToLeft;
            this.expLeft.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.expLeft.Controls.Add(this.pnlMainEdit);
            this.expLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.expLeft.HideControlsWhenCollapsed = true;
            this.expLeft.Location = new System.Drawing.Point(0, 0);
            this.expLeft.Name = "expLeft";
            this.expLeft.Size = new System.Drawing.Size(417, 538);
            this.expLeft.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expLeft.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expLeft.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expLeft.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expLeft.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expLeft.Style.GradientAngle = 90;
            this.expLeft.TabIndex = 1;
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
            this.pnlMainEdit.Size = new System.Drawing.Size(417, 512);
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
            this.pnlLeft.Controls.Add(this.pnlControl);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(417, 512);
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
            this.gvgLeft.Size = new System.Drawing.Size(375, 512);
            this.gvgLeft.TabIndex = 3;
            // 
            // dgvLeft
            // 
            this.dgvLeft.AllowUserToAddRows = false;
            this.dgvLeft.AllowUserToDeleteRows = false;
            this.dgvLeft.AllowUserToOrderColumns = true;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.LightCyan;
            this.dgvLeft.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvLeft.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLeft.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvLeft.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLeft.DefaultCellStyle = dataGridViewCellStyle13;
            this.dgvLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLeft.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvLeft.Location = new System.Drawing.Point(0, 0);
            this.dgvLeft.MultiSelect = false;
            this.dgvLeft.Name = "dgvLeft";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLeft.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.dgvLeft.RowsDefaultCellStyle = dataGridViewCellStyle15;
            this.dgvLeft.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLeft.Size = new System.Drawing.Size(375, 482);
            this.dgvLeft.TabIndex = 0;
            this.dgvLeft.Visible = false;
            // 
            // pnlControl
            // 
            this.pnlControl.CanvasColor = System.Drawing.SystemColors.Control;
            this.pnlControl.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pnlControl.Controls.Add(this.btnRemoveAll);
            this.pnlControl.Controls.Add(this.btnRemove);
            this.pnlControl.Controls.Add(this.btnAddSelect);
            this.pnlControl.Controls.Add(this.btnAddAll);
            this.pnlControl.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlControl.Location = new System.Drawing.Point(375, 0);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Size = new System.Drawing.Size(42, 512);
            this.pnlControl.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlControl.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pnlControl.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.pnlControl.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlControl.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pnlControl.Style.BorderWidth = 0;
            this.pnlControl.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlControl.Style.GradientAngle = 90;
            this.pnlControl.TabIndex = 2;
            // 
            // btnRemoveAll
            // 
            this.btnRemoveAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRemoveAll.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRemoveAll.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnRemoveAll.ImageFixedSize = new System.Drawing.Size(18, 18);
            this.btnRemoveAll.Location = new System.Drawing.Point(4, 271);
            this.btnRemoveAll.Name = "btnRemoveAll";
            this.btnRemoveAll.Size = new System.Drawing.Size(32, 37);
            this.btnRemoveAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnRemoveAll.TabIndex = 0;
            // 
            // btnRemove
            // 
            this.btnRemove.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRemove.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRemove.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnRemove.ImageFixedSize = new System.Drawing.Size(18, 18);
            this.btnRemove.Location = new System.Drawing.Point(3, 228);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(32, 37);
            this.btnRemove.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnRemove.TabIndex = 0;
            // 
            // btnAddSelect
            // 
            this.btnAddSelect.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddSelect.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAddSelect.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnAddSelect.ImageFixedSize = new System.Drawing.Size(18, 18);
            this.btnAddSelect.Location = new System.Drawing.Point(3, 185);
            this.btnAddSelect.Name = "btnAddSelect";
            this.btnAddSelect.Size = new System.Drawing.Size(32, 37);
            this.btnAddSelect.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAddSelect.TabIndex = 0;
            // 
            // btnAddAll
            // 
            this.btnAddAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddAll.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAddAll.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnAddAll.ImageFixedSize = new System.Drawing.Size(18, 18);
            this.btnAddAll.Location = new System.Drawing.Point(4, 142);
            this.btnAddAll.Name = "btnAddAll";
            this.btnAddAll.Size = new System.Drawing.Size(32, 37);
            this.btnAddAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAddAll.TabIndex = 0;
            // 
            // pnlEdit
            // 
            this.pnlEdit.CanvasColor = System.Drawing.SystemColors.Control;
            this.pnlEdit.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pnlEdit.Controls.Add(this.cboData);
            this.pnlEdit.Controls.Add(this.his_LabelX7);
            this.pnlEdit.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEdit.Location = new System.Drawing.Point(0, 0);
            this.pnlEdit.Name = "pnlEdit";
            this.pnlEdit.Size = new System.Drawing.Size(1173, 88);
            this.pnlEdit.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlEdit.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pnlEdit.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pnlEdit.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlEdit.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pnlEdit.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlEdit.Style.GradientAngle = 90;
            this.pnlEdit.TabIndex = 3;
            // 
            // cboData
            // 
            this.cboData.DisplayMember = "Text";
            this.cboData.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboData.FormattingEnabled = true;
            this.cboData.ItemHeight = 14;
            this.cboData.Location = new System.Drawing.Point(96, 25);
            this.cboData.Name = "cboData";
            this.cboData.Size = new System.Drawing.Size(159, 20);
            this.cboData.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboData.TabIndex = 8;
            // 
            // his_LabelX7
            // 
            // 
            // 
            // 
            this.his_LabelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.his_LabelX7.IsNotNull = false;
            this.his_LabelX7.Location = new System.Drawing.Point(14, 25);
            this.his_LabelX7.Name = "his_LabelX7";
            this.his_LabelX7.PaddingRight = 5;
            this.his_LabelX7.Size = new System.Drawing.Size(76, 20);
            this.his_LabelX7.TabIndex = 7;
            this.his_LabelX7.Text = "Danh sách:";
            this.his_LabelX7.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // his_DataGridView1
            // 
            this.his_DataGridView1.AllowUserToAddRows = false;
            this.his_DataGridView1.AllowUserToDeleteRows = false;
            this.his_DataGridView1.AllowUserToOrderColumns = true;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.LightCyan;
            this.his_DataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle16;
            this.his_DataGridView1.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.his_DataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle17;
            this.his_DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.his_DataGridView1.DefaultCellStyle = dataGridViewCellStyle18;
            this.his_DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.his_DataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.his_DataGridView1.Location = new System.Drawing.Point(0, 0);
            this.his_DataGridView1.MultiSelect = false;
            this.his_DataGridView1.Name = "his_DataGridView1";
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.his_DataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle19;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.his_DataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle20;
            this.his_DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.his_DataGridView1.Size = new System.Drawing.Size(459, 203);
            this.his_DataGridView1.TabIndex = 10;
            // 
            // MasterDetailBasic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1177, 675);
            this.Controls.Add(this.pnlEditMain);
            this.Name = "MasterDetailBasic";
            this.Text = "MasterDetailBasic";
            this.Controls.SetChildIndex(this.pnlButton, 0);
            this.Controls.SetChildIndex(this.pnlEditMain, 0);
            this.pnlButton.ResumeLayout(false);
            this.pnlEditMain.ResumeLayout(false);
            this.pnlGrid.ResumeLayout(false);
            this.expDetail.ResumeLayout(false);
            this.pnlGridMid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).EndInit();
            this.expRight.ResumeLayout(false);
            this.pnlGrifRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReference)).EndInit();
            this.pnlEditRight.ResumeLayout(false);
            this.expLeft.ResumeLayout(false);
            this.pnlMainEdit.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeft)).EndInit();
            this.pnlControl.ResumeLayout(false);
            this.pnlEdit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.his_DataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        protected E00_Control.his_PanelEx pnlEdit;
        protected E00_Control.his_PanelEx pnlGrid;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter3;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter2;
        protected E00_Control.his_ExpandablePanel expDetail;
        protected E00_Control.his_ExpandablePanel expRight;
        protected E00_Control.his_PanelEx pnlEditRight;
        protected E00_Control.his_ExpandablePanel expLeft;
        private E00_Control.his_PanelEx pnlMainEdit;
        private E00_Control.his_PanelEx pnlLeft;
        protected E00_Control.his_PanelEx pnlControl;
        protected E00_Control.his_ButtonX2 btnRemoveAll;
        protected E00_Control.his_ButtonX2 btnRemove;
        protected E00_Control.his_ButtonX2 btnAddSelect;
        protected E00_Control.his_ButtonX2 btnAddAll;
        protected E00_Control.his_PanelEx pnlDetail;
        protected E00_Control.his_ComboboxX cboData;
        protected E00_Control.his_LabelX his_LabelX7;
        private IContainer components;
        protected E00_Control.his_PanelEx pnlEditMain;
        protected E00_Control.his_ButtonX2 btnAddReference;
        #endregion


    }
}
