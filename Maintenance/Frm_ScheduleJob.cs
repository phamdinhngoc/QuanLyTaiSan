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
using E00_API.Contract;
using E00_API.Contract.Maintenance;
using E00_API.Contract.TaiSan;
using E00_API.Contract.VienPhi;
using E00_API.Maintenance;
using E00_Base;
using E00_Helpers.Common.Class;
using E00_Helpers.Helpers;
using E00_Model;
using E00_SafeCacheDataService.Base;
using E00_SafeCacheDataService.Common;
using E00_SafeCacheDataService.Interface;
using HISNgonNgu.Chung;
using HISNgonNgu.Maintenance;
using HISNgonNgu.NoiTru;
using QuanLyTaiSan.ViewBasic;
using QuanLyTaiSan.ViewEntity;

namespace QuanLyTaiSan.Maintenance
{
    public partial class Frm_ScheduleJob : MasterDetailBasic<TaiSanSuDungLLInfo, LichBaoTriLLInfo, LichBaoTriTBCTInfo, BTBaoTriNVInfo, LichBaoTriLLLViewEntity>
    {
        //----------------------------------------------

        #region Member
        private api_LichBaoTriLL _masterSevice;
        private api_LichBaoTriCT _detailService;
        private api_BaoTriNV _rightservice;

        #endregion

        //----------------------------------------------

        #region Constructor

        public Frm_ScheduleJob() : base()
        {

            InitializeComponent();
            this.ActiveControl = cboData;
            DisplayMemberMaster = @"SoPhieu";

            cboType.DisplayMember = "Name";
            cboType.ValueMember = "Code";
            cboType.DataSource = GlobalMember.GetListMaintenanceType;

            cboScheduleType.DisplayMember = "Name";
            cboScheduleType.ValueMember = "Code";
            cboScheduleType.DataSource = GlobalMember.GetListScheduleType;


            dat_Date.DataBindings.Clear();
            dat_Date.DataBindings.Add(@"Value", EditingEntity, "Ngay", true, DataSourceUpdateMode.OnPropertyChanged);

            intNumberFrequence.DataBindings.Clear();
            intNumberFrequence.DataBindings.Add(@"Value", EditingEntity, "LapLai", true, DataSourceUpdateMode.OnPropertyChanged);

            cboType.DataBindings.Clear();
            cboType.DataBindings.Add(@"SelectedValue", EditingEntity, "Loai", true, DataSourceUpdateMode.OnPropertyChanged);

            cboScheduleType.DataBindings.Clear();
            cboScheduleType.DataBindings.Add(@"SelectedValue", EditingEntity, "LoaiBT", true, DataSourceUpdateMode.OnPropertyChanged);

            txtCreateDate.DataBindings.Clear();
            txtCreateDate.DataBindings.Add(@"Text", EditingEntity, "SCreateTime", true, DataSourceUpdateMode.OnPropertyChanged);

            txtCreateBy.DataBindings.Clear();
            txtCreateBy.DataBindings.Add(@"Text", EditingEntity, "USERID", true, DataSourceUpdateMode.OnPropertyChanged);

            txtDescription.DataBindings.Clear();
            txtDescription.DataBindings.Add(@"Text", EditingEntity, "TenMoTa", true, DataSourceUpdateMode.OnPropertyChanged);

            txtRemark.DataBindings.Clear();
            txtRemark.DataBindings.Add(@"Text", EditingEntity, "GhiChu", true, DataSourceUpdateMode.OnPropertyChanged);

            txtJobNumber.DataBindings.Clear();
            txtJobNumber.DataBindings.Add(@"Text", EditingEntity, "SoPhieu", true, DataSourceUpdateMode.OnPropertyChanged);
            Initialize();

        }

        #endregion

        //----------------------------------------------

        #region Public/ Protect Method
       
        protected override void Luu()
        {
            try
            {

                if (RequestUpdateMaster())
                {
                    base.Luu();
                    ExitEditMode();
                }
            }
            catch (Exception ex)
            {
                _log.Error("Frm_ScheduleJob => Luu:" + ex.Message);
            }
        }
        protected override void Sua()
        {
            try
            {
                base.Sua();
                EnterEditMode();
            }
            catch (Exception ex)
            {
                _log.Error("Frm_ScheduleJob => sua:" + ex.Message);
            }
        }

        //----------------------------------------------
        protected override void InitializeGrid()
        {
            try
            {

                base.InitializeGrid();
                dgvPart.MultiSelect = true;
                dgvDetailList.MultiSelect = true;

            }
            catch (Exception ex)
            {
                _log.Error("Frm_ScheduleJob => InitializeGrid:" + ex.Message);
            }
        }
        protected override void DataLoading()
        {
            base.DataLoading();
            try
            {

                sbmEmployee.IsShowButtonClear = false;
                sbmEmployee.DisplayMember = cls_NhanSu_LiLichNhanVien.col_HoTen;
                sbmEmployee.ValueMember = cls_NhanSu_LiLichNhanVien.col_MaNhanVien;
                sbmEmployee.ColumDataList = new string[] { cls_NhanSu_LiLichNhanVien.col_MaNhanVien, cls_NhanSu_LiLichNhanVien.col_HoTen };
                sbmEmployee.ColumWidthList = new float[] { 40, 200 };

                var _theardRun = new Thread(new ThreadStart(() =>
                {
                    var data = _rightservice.GetListEmployeeIsActivity();
                    _syncContext.Send(state =>
                    {
                        sbmEmployee.SetDatasource(data, cls_NhanSu_LiLichNhanVien.col_MaNhanVien, cls_NhanSu_LiLichNhanVien.col_MaNhanVien, cls_NhanSu_LiLichNhanVien.col_HoTen);
                    }, null);
                }));
                _theardRun.IsBackground = true;
                _theardRun.Start();
            }
            catch (Exception ex)
            {
                _log.Error("Frm_ScheduleJob => DataLoading:" + ex.Message);
            }
        }
        protected override void InitializeService()
        {
            try
            {

                _masterSevice = new api_LichBaoTriLL();
                _detailService = new api_LichBaoTriCT();
                _rightservice = new api_BaoTriNV();
                base.InitializeService();
            }
            catch (Exception ex)
            {
                _log.Error("Frm_ScheduleJob => InitializeService:" + ex.Message);
            }
        }
        protected override bool ValidateMaster()
        {
            try
            {
                if (EditingEntity.Ngay == DateTime.MinValue)
                {
                    TA_MessageBox.MessageBox.Show(LNNChung.LDateinvalid, TA_MessageBox.MessageIcon.Error);
                    dat_Date.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(EditingEntity.TenMoTa))
                {
                    TA_MessageBox.MessageBox.Show(LMaintenance.LScheduleName, TA_MessageBox.MessageIcon.Error);
                    txtDescription.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(EditingEntity.Loai))
                {
                    TA_MessageBox.MessageBox.Show(LMaintenance.LScheduleType, TA_MessageBox.MessageIcon.Error);
                    cboType.Focus();
                    cboType.DroppedDown = true;
                    return false;
                }
                if (string.IsNullOrEmpty(EditingEntity.LoaiBT))
                {
                    TA_MessageBox.MessageBox.Show(LMaintenance.LScheduleType, TA_MessageBox.MessageIcon.Error);
                    cboScheduleType.Focus();
                    cboScheduleType.DroppedDown = true;
                    return false;
                }
                if (EditingEntity.LapLai == 0)
                {
                    TA_MessageBox.MessageBox.Show(LMaintenance.LScheduleFrequence, TA_MessageBox.MessageIcon.Error);
                    intNumberFrequence.Focus();
                    return false;
                }
                if (EditingEntity.LICHBTID == 0)
                {
                    EditingEntity.SoPhieu = _masterSevice.GetNumberMax();
                }
                if (string.IsNullOrEmpty(EditingEntity.SoPhieu))
                {
                    EditingEntity.SoPhieu = _masterSevice.GetNumberMax();
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Frm_ScheduleJob => ValidateMaster:" + ex.Message);
                return false;
            }
        }
        protected override bool ValidateReference()
        {
            try
            {
                bool kt = base.ValidateReference();
                if (kt)
                {
                    _referencetInfo.BTID = EditingEntity.LICHBTID;
                    //_referencetInfo.GhiChu = "";
                    _referencetInfo.TrangThai = 0;
                    _referencetInfo.MaNV = "" + sbmEmployee.SelectedValue;

                    if (string.IsNullOrEmpty(_referencetInfo.MaNV))
                    {
                        TA_MessageBox.MessageBox.Show(LMaintenance.LSelectEmployee, TA_MessageBox.MessageIcon.Information);
                        sbmEmployee.SetDoDragDrop(true);
                        return false;
                    }
                    var chechInfo = _rightDataService.Get_Item(cls_BaoTriNV.col_BTID + " = " + EditingEntity.LICHBTID + " and " + cls_BaoTriNV.col_MaNV + " = '" + _referencetInfo.MaNV + "'");
                    if (chechInfo != null)
                    {
                        TA_MessageBox.MessageBox.Show(LMaintenance.LEmployeeExsist, TA_MessageBox.MessageIcon.Information);
                        sbmEmployee.SetDoDragDrop(true);
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Frm_ScheduleJob => ValidateReference:" + ex.Message);
                return false;
            }
        }
        protected override void SynchronizedMaster()
        {
            try
            {
                //cboData.DataSource = _masterDataService.Get_DataInfo(cls_LichBaoTri_LL.col_TrangThai + " = 0");
                IList<LichBaoTriLLInfo> list = _masterDataService.Get_DataInfo(cls_LichBaoTri_LL.col_TrangThai + " = 0").OrderByDescending(t => t.LichBTID).ToList();
                cboData.DataSource = list;
                cboData.DisplayMember = cls_LichBaoTri_LL.col_SoPhieu;
                cboData.ValueMember = cls_LichBaoTri_LL.col_LichBTID;
                cboData.SelectedIndex = -1;
                if (Info != null)
                {
                    if(Info.LichBTID == 0)
                    {
                        btnThem.PerformClick();
                    }else
                    {
                        cboData.SelectedValue = Info.LichBTID;
                        if (cboData.SelectedValue == null)
                        {
                            _masterInfo = _masterDataService.Get_Item(cls_LichBaoTri_LL.col_LichBTID + " = " + Info.LichBTID);
                            if (_masterInfo != null)
                            {
                                if (list == null) list = new List<LichBaoTriLLInfo>();
                                list.Add(_masterInfo);
                                cboData.DataSource = list;
                                cboData.SelectedValue = _masterInfo.LichBTID;
                            }

                        }
                        MasterSelectedChanged();
                    }
                    Info = null;
                }

                cboData.SelectedIndexChanged -= (send, e) => MasterSelectedChanged();
                cboData.SelectedIndexChanged += (send, e) => MasterSelectedChanged();
            }
            catch (Exception ex)
            {
                _log.Error("Frm_ScheduleJob => SynchronizedMaster:" + ex.Message);
            }
        }
        protected override void SynchronizedDetail()
        {
            try
            {
                var _theardRun = new Thread(new ThreadStart(() =>
                {
                    var data = _detailService.GetListPartByMaster(EditingEntity.LICHBTID);
                    _syncContext.Send(state =>
                    {
                        try
                        {
                            gvgDetail.DataSource = data;
                        }
                        catch (Exception ex)
                        {
                            _log.Error("Frm_ScheduleJob => SynchronizedDetail:" + ex.Message);
                        }
                     
                    }, null);
                }));
                _theardRun.IsBackground = true;
                _theardRun.Start();
            }
            catch (Exception ex)
            {
                _log.Error("Frm_ScheduleJob => SynchronizedDetail:" + ex.Message);
            }
        }
        protected override void SynchronizedReference()
        {
            try
            {
                var _theardRun = new Thread(new ThreadStart(() =>
                {
                    var data = _rightservice.GetListEmployeeByMaster(EditingEntity.LICHBTID);
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
                _log.Error("Frm_ScheduleJob => SynchronizedReference:" + ex.Message);
            }
        }
        protected override void SynchronizedLeft()
        {
            try
            {
                _lazyDataLeft = new Lazy<DataTable>(() => _leftDataService.Get_Data(cls_TaiSan_SuDungLL.col_SuDung + " = 1"));
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
                _log.Error("Frm_ScheduleJob => SynchronizedLeft:" + ex.Message);
            }
        }
        protected override void EnterEditMode()
        {
            base.EnterEditMode();
            try
            {

                dat_Date.Enabled = _isEditing;
                cboType.Enabled = _isEditing;
                txtDescription.ReadOnly = !_isEditing;
                txtRemark.ReadOnly = !_isEditing;
                cboScheduleType.Enabled = !_isEditing;
                intNumberFrequence.Enabled = !_isEditing;

                btnAddAll.Enabled = _isEditing;
                btnAddSelect.Enabled = _isEditing;
                btnRemove.Enabled = _isEditing;
                btnRemoveAll.Enabled = _isEditing;
                btnAddReference.Enabled = _isEditing;

            }
            catch (Exception ex)
            {
                _log.Error("Frm_ScheduleJob => EnterEditMode:" + ex.Message);
            }
        }
        protected override void ExitEditMode()
        {
            try
            {

                base.ExitEditMode();
                dat_Date.Enabled = _isEditing;
                cboType.Enabled = _isEditing;
                txtDescription.ReadOnly = !_isEditing;
                txtRemark.ReadOnly = !_isEditing;
                cboScheduleType.Enabled = !_isEditing;
                intNumberFrequence.Enabled = !_isEditing;

                btnAddAll.Enabled = _isEditing;
                btnAddSelect.Enabled = _isEditing;
                btnRemove.Enabled = _isEditing;
                btnRemoveAll.Enabled = _isEditing;
                btnAddReference.Enabled = _isEditing;

                btnSua.Enabled = true;

            }
            catch (Exception ex)
            {
                _log.Error("Frm_ScheduleJob => ExitEditMode:" + ex.Message);
            }
        }
        protected override bool InsertDetail(DataRow row)
        {
            try
            {

                // var infoCheck = _detailDataService.Get_Item(cls_LichBaoTriTBCT.col_LichBTID + " = " + EditingEntity.LICHBTID + " and " + cls_LichBaoTriTBCT.col_MaTaiSan + " = '" + "" + row[cls_TaiSan_SuDungLL.col_Ma] + "'");
                var infoCheck = _detailDataService.Get_Item(cls_LichBaoTriTBCT.col_MaTaiSan + " = '" + "" + row[cls_TaiSan_SuDungLL.col_Ma] + "'");
                if (infoCheck == null)
                {
                    _detailInfo = new LichBaoTriTBCTInfo();
                    _detailInfo.LichBTCTID = _detailDataService.CreateNewID(20);
                    _detailInfo.LichBTID = EditingEntity.LICHBTID;
                    _detailInfo.MaTaiSan = Helper.ConvertSToDec(row[cls_TaiSan_SuDungLL.col_Ma]);
                    _detailInfo.USERID = Helper.ConvertSToDec(E00_System.cls_System.sys_UserID);
                    _detailInfo.USERUD = _detailInfo.USERID;
                    _detailInfo.NgayUD = DateTime.Now;
                    _detailInfo.MACHINEID = _detailDataService.GetMachineID();
                    _resultInfo = _detailDataService.Insert(_detailInfo);
                    if (!_resultInfo.Status)
                    {
                        TA_MessageBox.MessageBox.Show(LNNChung.EUpdateFailed + ": " + _resultInfo.SystemError, TA_MessageBox.MessageIcon.Error);
                        return false;
                    }

                }
                //else
                //{
                //    TA_MessageBox.MessageBox.Show(string.Format(LNNChung.LLCheckedExist, "" + row[cls_TaiSan_SuDungLL.col_Ma]), TA_MessageBox.MessageIcon.Error);
                //}
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Frm_ScheduleJob => InsertDetail:" + ex.Message);
                return false;
            }
        }
        protected override void MasterSelectedChanged()
        {
            base.MasterSelectedChanged();
            ExitEditMode();
        }
        protected override void MasterDelected()
        {
            try
            {

                if ((decimal)EditingEntity.KeyIDValue > 0)
                {
                    if (TA_MessageBox.MessageBox.Show(string.Format(LNNChung.LInfoDelete, @"phiếu bảo trì đang chọn"), TA_MessageBox.MessageIcon.Question) == DialogResult.Yes)
                    {
                        _resultInfo = _detailDataService.Delete(cls_LichBaoTriTBCT.col_LichBTID + " = " + EditingEntity.KeyIDValue);
                        _resultInfo = _masterDataService.Delete(EditingEntity.KeyIDValue);
                        if (_resultInfo.Status)
                        {
                            SynchronizedMaster();
                            gvgDetail.DataSource = null;
                            EditingEntity.Set(new LichBaoTriLLInfo());
                        }
                        else
                        {
                            TA_MessageBox.MessageBox.Show(LNNChung.EUpdateFailed + ": " + _resultInfo.SystemError, TA_MessageBox.MessageIcon.Error);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Frm_ScheduleJob => MasterDelected:" + ex.Message);
            }
        }
        #endregion

        //----------------------------------------------

        #region Private Method

        #endregion

        //----------------------------------------------

        #region Event Control

        #endregion

        //----------------------------------------------

    }
}
