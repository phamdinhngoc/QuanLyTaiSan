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
using E00_API.Maintenance;
using E00_API.TaiSan;
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
    public partial class Frm_Maintenance : MasterDetailBasic<TaiSanSuDungLLInfo, BTBaoTriLLInfo, BTBaoTriCTInfo, BTBaoTriNVInfo, BaoTriLLLViewEntity>
    {
        //----------------------------------------------

        #region Member
        private api_BaoTriLL _masterSevice;
        private api_BaoTriCT _detailService;
        private api_BaoTriNV _rightservice;
        private api_TaiSanSuDungLL _usingDataService;
        DataTable _tmpDetail;
        #endregion

        //----------------------------------------------

        #region Constructor

        public Frm_Maintenance() : base()
        {
            InitializeComponent();
            this.ActiveControl = cboData;
            IsUsingReference = true;
            try
            {
                DisplayMemberMaster = @"SoPhieu";
                cboType.DisplayMember = "Name";
                cboType.ValueMember = "Code";
                cboType.DataSource = GlobalMember.GetListMaintenanceType;

                dat_Date.DataBindings.Clear();
                dat_Date.DataBindings.Add(@"Value", EditingEntity, "Ngay", true, DataSourceUpdateMode.OnPropertyChanged);

                cboType.DataBindings.Clear();
                cboType.DataBindings.Add(@"SelectedValue", EditingEntity, "Loai", true, DataSourceUpdateMode.OnPropertyChanged);

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

                this.btnUpdateEmpoyee.Click += (send, e) => UpdatePerformanceEmployee();
                this.btnDelectEmployee.Click += (send, e) => DelectEmployee();

                col_DT_TinhTrang.DisplayMember = "Name";
                col_DT_TinhTrang.ValueMember = "Code";
                col_DT_TinhTrang.DataSource = GlobalMember.GetListSatusPart;
                Initialize();
            }
            catch (Exception ex)
            {
                _log.Error("Frm_Maintenance => Frm_Maintenance:" + ex.Message);
            }
        }

        #endregion

        //----------------------------------------------

        #region Public/ Protect Method
        protected override void InitializeGrid()
        {
            try
            {
                base.InitializeGrid();
                dgvPart.MultiSelect = true;
                dgvDetailList.MultiSelect = true;
                expRight.Expanded = false;
                col_DelectRef.Click += (send, e) => DelectEmployee();
            }
            catch (Exception ex)
            {
                _log.Error("Frm_Maintenance => InitializeGrid:" + ex.Message);
            }
        }
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
                _log.Error("Frm_Maintenance => Luu:" + ex.Message);
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
                _log.Error("Frm_Maintenance => Sua:" + ex.Message);
            }
        }
        protected override void DataLoading()
        {
            try
            {

                base.DataLoading();

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
                _log.Error("Frm_Maintenance => DataLoading:" + ex.Message);
            }
        }
        protected override void InitializeService()
        {
            try
            {

                _masterSevice = new api_BaoTriLL();
                _detailService = new api_BaoTriCT();
                _rightservice = new api_BaoTriNV();
                _usingDataService = new api_TaiSanSuDungLL();
                base.InitializeService();
            }
            catch (Exception ex)
            {
                _log.Error("DataLoading => InitializeService:" + ex.Message);
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

                if (EditingEntity.BTID == 0)
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
                _log.Error("DataLoading => ValidateMaster:" + ex.Message);
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
                    _referencetInfo.BTID = EditingEntity.BTID;
                    //_referencetInfo.GhiChu = "";
                    _referencetInfo.TrangThai = 0;
                    _referencetInfo.MaNV = "" + sbmEmployee.SelectedValue;
                    _referencetInfo.NangXuat = (decimal)intPerformance.Value;
                    if (string.IsNullOrEmpty(_referencetInfo.MaNV))
                    {
                        TA_MessageBox.MessageBox.Show(LMaintenance.LSelectEmployee, TA_MessageBox.MessageIcon.Information);
                        sbmEmployee.SetDoDragDrop(true);
                        return false;
                    }

                    var chechInfo = _rightDataService.Get_Item(cls_BaoTriNV.col_BTID + " = " + EditingEntity.BTID + " and " + cls_BaoTriNV.col_MaNV + " = '" + _referencetInfo.MaNV + "'");
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
                _log.Error("DataLoading => ValidateReference:" + ex.Message);
                return false;
            }
        }
        protected override void SynchronizedMaster()
        {
            try
            {
                //cboData.DataSource = _masterDataService.Get_DataInfo(cls_LichBaoTri_LL.col_TrangThai + " = 0");
                IList<BTBaoTriLLInfo> list = _masterDataService.Get_DataInfo(cls_BTBaoTriLL.col_TrangThai + " = 0").OrderByDescending(t => t.BTID).ToList();
                cboData.DataSource = list;
                cboData.DisplayMember = cls_BTBaoTriLL.col_SoPhieu;
                cboData.ValueMember = cls_BTBaoTriLL.col_BTID;
                cboData.SelectedIndex = -1;
                if (Info != null)
                {
                    if (Info.BTID == 0)
                    {
                        btnThem.PerformClick();
                    }
                    else
                    {
                        cboData.SelectedValue = Info.BTID;
                        if (cboData.SelectedValue == null)
                        {
                            _masterInfo = _masterDataService.Get_Item(cls_BTBaoTriLL.col_BTID + " = " + Info.BTID);
                            if (_masterInfo != null)
                            {
                                if (list == null) list = new List<BTBaoTriLLInfo>();
                                list.Add(_masterInfo);
                                cboData.DataSource = list;
                                cboData.SelectedValue = _masterInfo.BTID;
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
                _log.Error("DataLoading => SynchronizedMaster:" + ex.Message);
            }
        }
        protected override void SynchronizedDetail()
        {
            try
            {

                var _theardRun = new Thread(new ThreadStart(() =>
                {
                    _tmpDetail = _detailService.GetListPartByMaster(EditingEntity.BTID);
                    _syncContext.Send(state =>
                    {
                        gvgDetail.DataSource = _tmpDetail;
                    }, null);
                }));
                _theardRun.IsBackground = true;
                _theardRun.Start();
            }
            catch (Exception ex)
            {
                _log.Error("DataLoading => SynchronizedDetail:" + ex.Message);
            }
        }
        protected override void SynchronizedReference()
        {
            try
            {

                var _theardRun = new Thread(new ThreadStart(() =>
                {
                    var data = _rightservice.GetListEmployeeByMaster(EditingEntity.BTID);
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
                _log.Error("DataLoading => SynchronizedReference:" + ex.Message);
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
                _log.Error("DataLoading => SynchronizedLeft:" + ex.Message);
            }
        }
        protected override void EnterEditMode()
        {
            try
            {
                base.EnterEditMode();

                btnAddAll.Enabled = _isEditing;
                btnAddSelect.Enabled = _isEditing;
                btnRemove.Enabled = _isEditing;
                btnRemoveAll.Enabled = _isEditing;
                btnAddReference.Enabled = _isEditing;

                dat_Date.Enabled = _isEditing;
                txtDescription.ReadOnly = !_isEditing;
                txtRemark.ReadOnly = !_isEditing;
                cboType.Enabled = _isEditing;

                pnlEditRight.Enabled = _isEditing;
                dgvEmployee.ReadOnly = !_isEditing;

                dgvEmployee.ReadOnly = !_isEditing;
                dgvDetailList.ReadOnly = !_isEditing;
            }
            catch (Exception ex)
            {
                _log.Error("DataLoading => EnterEditMode:" + ex.Message);
            }
        }
        protected override void ExitEditMode()
        {
            try
            {
                base.ExitEditMode();
                btnSua.Enabled = true;

                btnAddAll.Enabled = _isEditing;
                btnAddSelect.Enabled = _isEditing;
                btnRemove.Enabled = _isEditing;
                btnRemoveAll.Enabled = _isEditing;
                btnAddReference.Enabled = _isEditing;

                dat_Date.Enabled = _isEditing;
                txtDescription.ReadOnly = !_isEditing;
                txtRemark.ReadOnly = !_isEditing;
                cboType.Enabled = _isEditing;

                pnlEditRight.Enabled = _isEditing;
                dgvEmployee.ReadOnly = !_isEditing;

                dgvEmployee.ReadOnly = !_isEditing;
                dgvDetailList.ReadOnly = !_isEditing;
            }
            catch (Exception ex)
            {
                _log.Error("DataLoading => ExitEditMode:" + ex.Message);
            }
        }
        protected override bool InsertDetail(DataRow row)
        {
            try
            {
                var infoCheck = _detailDataService.Get_Item(cls_BTBaoTriCT.col_BTID + " = " + EditingEntity.BTID + " and " + cls_BTBaoTriCT.col_MaTaiSan + " = '" + "" + row[cls_TaiSan_SuDungLL.col_Ma] + "'");
                if (infoCheck == null)
                {
                    _detailInfo = new BTBaoTriCTInfo();
                    _detailInfo.BTCTID = _detailDataService.CreateNewID(20);
                    _detailInfo.BTID = EditingEntity.BTID;
                    _detailInfo.MaTaiSan = Helper.ConvertSToDec(row[cls_TaiSan_SuDungLL.col_Ma]);
                    _detailInfo.USERID = Helper.ConvertSToDec(E00_System.cls_System.sys_UserID);
                    _detailInfo.USERUD = _detailInfo.USERID;
                    _detailInfo.NgayUD = DateTime.Now;
                    _detailInfo.TinhTrang =""+ row[cls_TaiSan_SuDungLL.col_TrangThai];
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
                // TA_MessageBox.MessageBox.Show(string.Format(LNNChung.LLCheckedExist, "" + row[cls_TaiSan_SuDungLL.col_Ma]), TA_MessageBox.MessageIcon.Error);
                //}
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("DataLoading => InsertDetail:" + ex.Message);
                return false;
            }
        }
        protected override void MasterSelectedChanged()
        {
            try
            {

                base.MasterSelectedChanged();
                ExitEditMode();
            }
            catch (Exception ex)
            {
                _log.Error("DataLoading => MasterSelectedChanged:" + ex.Message);
            }
        }
        protected override void BtnAddReference()
        {
            try
            {
                base.BtnAddReference();
                sbmEmployee.Clear();
                sbmEmployee.Focus();
                sbmEmployee.SetDoDragDrop(true);
            }
            catch (Exception ex)
            {
                _log.Error("DataLoading => BtnAddReference:" + ex.Message);
            }
        }
        protected override void MasterDelected()
        {
            try
            {
                if ((decimal)EditingEntity.KeyIDValue > 0)
                {
                    if (TA_MessageBox.MessageBox.Show(string.Format(LNNChung.LInfoDelete, @"phiếu bảo trì đang chọn"), TA_MessageBox.MessageIcon.Question) == DialogResult.Yes)
                    {
                        _resultInfo = _detailDataService.Delete(cls_BTBaoTriCT.col_BTID + " = " + EditingEntity.KeyIDValue);
                        _resultInfo = _rightDataService.Delete(cls_BaoTriNV.col_BTID + " = " + EditingEntity.KeyIDValue);
                        _resultInfo = _masterDataService.Delete(EditingEntity.KeyIDValue);
                        if (_resultInfo.Status)
                        {
                            SynchronizedMaster();
                            gvgDetail.DataSource = null;
                            EditingEntity.Set(new BTBaoTriLLInfo());
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
                _log.Error("DataLoading => MasterDelected:" + ex.Message);
            }
        }
        protected override bool RequestUpdateMaster()
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
                        DataTable detailTmp = dgvDetailList.DataSource as DataTable;
                        if (detailTmp != null)
                        {
                            foreach (DataRow rowItem in detailTmp.Rows)
                            {
                                _resultInfo = _detailDataService.Update(cls_BTBaoTriCT.col_TinhTrang + " = '" + rowItem[cls_BTBaoTriCT.col_TinhTrang] + "'," + cls_BTBaoTriCT.col_GhiChu + " = '" + rowItem[cls_BTBaoTriCT.col_GhiChu] + "'", cls_BTBaoTriCT.col_BTCTID + "=" + rowItem[cls_BTBaoTriCT.col_BTCTID]);
                                if (!_resultInfo.Status)
                                {
                                    TA_MessageBox.MessageBox.Show(LNNChung.EUpdateFailed + ": " + _resultInfo.SystemError, TA_MessageBox.MessageIcon.Error);
                                    return false;
                                }
                                else
                                {
                                    _resultInfo = _usingDataService.Update($"{cls_TaiSan_SuDungLL.col_TrangThai} = '{rowItem[cls_BTBaoTriCT.col_TinhTrang]}'",$"{cls_TaiSan_SuDungLL.col_Ma} = '{ rowItem[cls_BTBaoTriCT.col_MaTaiSan]}'");
                                    if (!_resultInfo.Status)
                                    {
                                        TA_MessageBox.MessageBox.Show(LNNChung.EUpdateFailed + ": " + _resultInfo.SystemError, TA_MessageBox.MessageIcon.Error);
                                        return false;
                                    }
                                }
                            }
                        }
                        _masterInfo = new BTBaoTriLLInfo();
                        Info = new BTBaoTriLLInfo();
                        Info.Copy(EditingEntity.Info);
                        ExitEditMode();
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
                _log.Error("DataLoading => RequestUpdateMaster:" + ex.Message);
                return false;
            }
        }
        #endregion

        //----------------------------------------------

        #region Private Method
        private void UpdatePerformanceEmployee()
        {
            try
            {
                var tmp = dgvEmployee.DataSource as DataTable;
                if (tmp != null)
                {
                    if (tmp.Rows.Count > 0)
                    {
                        foreach (DataRow item in tmp.Rows)
                        {
                            _resultInfo = _rightDataService.Update(cls_BaoTriNV.col_NangXuat + " = " + Helper.ConvertSToDob("" + item[cls_BaoTriNV.col_NangXuat]), cls_BaoTriNV.col_ID + " = " + item[cls_BaoTriNV.col_ID]);
                            if (!_resultInfo.Status)
                            {
                                TA_MessageBox.MessageBox.Show(LNNChung.EUpdateFailed + " :" + _resultInfo.SystemError);
                                SynchronizedReference();
                                return;
                            }
                        }
                        SynchronizedReference();
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("DataLoading => UpdatePerformanceEmployee:" + ex.Message);
            }
        }
        #endregion

        //----------------------------------------------

        #region Event Control
        private void DelectEmployee()
        {
            try
            {

                var rowSelect = dgvEmployee.CurrentRow.DataBoundItem as DataRowView;
                if (rowSelect != null)
                {
                    if (TA_MessageBox.MessageBox.Show(string.Format(LNNChung.LInfoDelete, "" + rowSelect.Row[cls_BacSi.col_HoTen]), TA_MessageBox.MessageIcon.Question) == DialogResult.Yes)
                    {
                        _resultInfo = _rightservice.Delete((object)rowSelect.Row[cls_BaoTriNV.col_ID]);
                        if (_resultInfo.Status)
                        {
                            SynchronizedReference();
                        }
                        else
                        {
                            TA_MessageBox.MessageBox.Show(LNNChung.EUpdateFailed + ":" + _resultInfo.SystemError, TA_MessageBox.MessageIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("DataLoading => DelectEmployee:" + ex.Message);
            }
        }
        #endregion

        //----------------------------------------------

    }
}
