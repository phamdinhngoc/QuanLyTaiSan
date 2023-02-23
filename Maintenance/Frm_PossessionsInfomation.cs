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
using E00_API.Maintenance;
using E00_Base;
using E00_Helpers.Helpers;
using E00_Model;
using E00_SafeCacheDataService.Common;
using HISNgonNgu.Chung;
using HISNgonNgu.Maintenance;
using HISNgonNgu.NoiTru;
using QuanLyTaiSan.ViewBasic;

namespace QuanLyTaiSan.Maintenance
{
    public partial class Frm_PossessionsInfomation : LViewbasic
    {

        //--------------------------------------------------

        #region Member
        public string ItemID { get; set; }
        private api_TaiSan_ChiTietLinhKien _defaultSevice;
        protected IList<DataRow> _rowSelectedtmp;
        private DataRow _rowCurrent;
        private const string DanhMuc = @"DM";
        private LichBaoTriLLInfo _scheduleInfo;
        private TaiSan_ChiTietLinhKienInfo _detailInfo;
        private ResultInfo _resultInfo;
        #endregion

        //--------------------------------------------------

        #region Constructor
        public Frm_PossessionsInfomation() : base()
        {
            InitializeComponent();
            Initialize();
            btnOpenOJ.Click += (send, e) => OpenOJ();
            btnAddMaintenance.Click += (send, e) => AddMaintenance();
            btnAddScheduleJob.Click += (send, e) => AddScheduleJob();
            btnAddPart.Click += (send, e) => AddLinhkien();
            btnDeletePart.Click += (send, e) => DeletePart();
        }

        #endregion

        //--------------------------------------------------

        #region Private method
        private void SynchronizedListPart()
        {
            try
            {
                var _theardRun = new Thread(new ThreadStart(() => {
                    var data = _defaultSevice.GetListLinhKien();
                    _syncContext.Send(state =>
                    {
                        try
                        {
                            slbLinhKien.ColumDataList = new string[] { "MAVACH", "TEN", "MAVACH", "VITRI", "KYHIEU", "MODEL" };
                            slbLinhKien.ColumWidthList = new int[] { 120, 150, 150, 100, 100, 120 };
                            slbLinhKien.ValueMember = "MAVACH";
                            slbLinhKien.DisplayMember = "TEN";
                            slbLinhKien.DataSource = data;
                        }
                        catch (Exception ex)
                        {
                        }

                    }, null);
                }));
                _theardRun.IsBackground = true;
                _theardRun.Start();
            }
            catch (Exception ex)
            {
                _log.Error("Frm_BillOfMaterial => SynchronizedReference:" + ex.Message);
            }
        }
        protected override void Initialize()
        {
            base.Initialize();
            this.Load += (send, e) => DataLoading();
            gvgLinhKien.Initialize("");
            gvgChiTiecSD.Initialize("");
        }
        private void DataLoading()
        {
            try
            {
            _defaultSevice = new api_TaiSan_ChiTietLinhKien();
            Synchronized();
            SynchronizedListPart();
            }
            catch (Exception ex)
            {
                _log.Error("Frm_PossessionsInfomation => DataLoading:" + ex.Message);
            }
        }
        private void Synchronized()
        {
            try
            {
            if (ItemID != null)
            {
                GetItemInfo();
                SynchronizedPart();
                SynchronizedUse();
                SynchronizedMaintenance();
                SynchronizedSchedule();
            }
            }
            catch (Exception ex)
            {
                _log.Error("Frm_PossessionsInfomation => Synchronized:" + ex.Message);
            }
        }
        private void GetItemInfo()
        {
            try
            {

                _rowCurrent = _defaultSevice.GetInfo("" + ItemID);
                if (_rowCurrent != null)
                {
                    FillControl(_rowCurrent);
                }
            }
            catch (Exception ex)
            {
                _log.Error("Frm_PossessionsInfomation => GetItemInfo:" + ex.Message);
            }
        }
        private void FillControl(DataRow _rowCurrent)
        {
            try
            {
                lblMaTS.Text = "" + _rowCurrent["DMMA"];
                lblMaVach.Text = "" + _rowCurrent[cls_TaiSan_SuDungLL.col_MaVach];
                lblKyHieu.Text = "" + _rowCurrent[DanhMuc + cls_TaiSan_DanhMuc.col_KyHieu];
                lblTen.Text = "" + _rowCurrent[DanhMuc + cls_TaiSan_DanhMuc.col_Ten];
                lblTenLoai.Text = "" + _rowCurrent[DanhMuc + cls_TaiSan_DanhMuc.col_MaLoai] + "-" + _rowCurrent[DanhMuc + cls_TaiSan_DanhMuc.col_TenLoai];
               // lblNoiSX.Text = "" + _rowCurrent[DanhMuc + cls_TaiSan_DanhMuc.col_NuocSanXuat];
                //lblMaVachSX.Text = "" + _rowCurrent[DanhMuc + cls_TaiSan_DanhMuc.col_MaVachSanXuat];
                lblQuyCach.Text = "" + _rowCurrent[DanhMuc + cls_TaiSan_DanhMuc.col_QuyCach];
                lblTaiLieu.Text = "" + _rowCurrent[DanhMuc + cls_TaiSan_DanhMuc.col_TaiLieu];
                lblTamNgung.Text = "" + _rowCurrent[DanhMuc + cls_TaiSan_DanhMuc.col_TamNgung];

                lblNgayTao.Text = "N/a";
                lblNgayNhap.Text = "N/a";
                lblNgayHoaDon.Text = "N/a";
                lblNgayKiem.Text = "N/a";
                lblNgaySuDung.Text = "N/a";

                DateTime tmpDate = Helper.ConvertSToDtime("" + _rowCurrent[DanhMuc + cls_TaiSan_DanhMuc.col_NgayTao]);
                if (tmpDate != DateTime.MinValue)
                {
                    lblNgayTao.Text = tmpDate.ToString(E00_Helpers.Format.Formats.FDateDMYHM);
                }
                tmpDate = Helper.ConvertSToDtime("" + _rowCurrent[cls_TaiSan_NhapLL.col_NgayNhap]);
                if (tmpDate != DateTime.MinValue)
                {
                    lblNgayNhap.Text = tmpDate.ToString(E00_Helpers.Format.Formats.FDateDMYHM);
                }
                //tmpDate = Helper.ConvertSToDtime("" + _rowCurrent[cls_TaiSan_NhapLL.col_NgayHoaDon]);
                //if (tmpDate != DateTime.MinValue)
                //{
                //    lblNgayHoaDon.Text = tmpDate.ToString(E00_Helpers.Format.Formats.FDateDMYHM);
                //}
                //tmpDate = Helper.ConvertSToDtime("" + _rowCurrent[cls_TaiSan_SuDungLL.col_NgayKiemKe]);
                //if (tmpDate != DateTime.MinValue)
                //{
                //    lblNgayKiem.Text = tmpDate.ToString(E00_Helpers.Format.Formats.FDateDMYHM);
                //}
                tmpDate = Helper.ConvertSToDtime("" + _rowCurrent[cls_TaiSan_SuDungLL.col_NgaySuDung]);
                if (tmpDate != DateTime.MinValue)
                {
                    lblNgaySuDung.Text = tmpDate.ToString(E00_Helpers.Format.Formats.FDateDMYHM);
                }

                lblSoPhieuNhap.Text = "" + _rowCurrent[cls_TaiSan_NhapLL.col_SoPhieu];

                //lblSoHoaDon.Text = "" + _rowCurrent[cls_TaiSan_NhapLL.col_SoHoaDon];
                lblNhaCungCap.Text = "" + _rowCurrent[cls_TaiSan_NhapLL.col_MaNhaCungCap] + "-" + _rowCurrent["TenNCC"];
                lblDVT.Text = "" + _rowCurrent["TENDVT"];
                lblDonGia.Text = "" + _rowCurrent[cls_TaiSan_NhapCT.col_DonGia];
                lblHanBaoHanh.Text = "" + _rowCurrent[cls_TaiSan_NhapCT.col_HanBaoHanh];
                lbldotkiemke.Text = "" + _rowCurrent[cls_TaiSan_SuDungLL.col_MaKiemKe];
                lblTenDotKiem.Text = "" + _rowCurrent[cls_TaiSan_SuDungLL.col_TenKiemKe];

                lblNguoiSudung.Text = "" + _rowCurrent[cls_TaiSan_SuDungLL.col_MaNguoiSuDung] + " - " + _rowCurrent[cls_TaiSan_SuDungLL.col_TenNguoiSuDung];
                lblKhoaSD.Text = "" + _rowCurrent[cls_TaiSan_SuDungLL.col_MaKPSuDung] + " - " + _rowCurrent[cls_TaiSan_SuDungLL.col_TenKPSuDung];
                lblKhoaQL.Text = "" + _rowCurrent[cls_TaiSan_SuDungLL.col_MaKPQuanLy] + " - " + _rowCurrent[cls_TaiSan_SuDungLL.col_TenKPQuanLy];
                lblKhu.Text = "" + _rowCurrent[cls_TaiSan_SuDungLL.col_MaKhu] + " - " + _rowCurrent[cls_TaiSan_SuDungLL.col_TenKhu];
                lblTang.Text = "" + _rowCurrent[cls_TaiSan_SuDungLL.col_MaTang] + " - " + _rowCurrent[cls_TaiSan_SuDungLL.col_TenTang];
                lblPhongCongNang.Text = "" + _rowCurrent[cls_TaiSan_SuDungLL.col_MaPhongCongNang] + " - " + _rowCurrent[cls_TaiSan_SuDungLL.col_TenPhongCongNang];
                lblTrangThai.Text = "" + _rowCurrent[cls_TaiSan_SuDungLL.col_TenTrangThai];
                if ("" + _rowCurrent[cls_TaiSan_SuDungLL.col_SuDung] == "1")
                {
                    lblSuDung.Text = @"Đang sử dụng";
                }
                else
                {
                    lblSuDung.Text = @"N/a";
                }

            }
            catch (Exception ex)
            {
                _log.Error("Frm_PossessionsInfomation => FillControl:" + ex.Message);
            }
        }
        private void SynchronizedPart()
        {
            try
            {
                var _theardRun = new Thread(new ThreadStart(() =>
                {
                    var data = _defaultSevice.GetListByMaster(ItemID);
                    _syncContext.Send(state =>
                    {
                        gvgLinhKien.DataSource = data;
                    }, null);
                }));
                _theardRun.IsBackground = true;
                _theardRun.Start();
            }
            catch (Exception ex)
            {
                _log.Error("Frm_PossessionsInfomation => SynchronizedPart:" + ex.Message);
            }
        }
        private void SynchronizedUse()
        {
            try
            {
                var _theardRun = new Thread(new ThreadStart(() =>
                {
                    var data = _defaultSevice.GetListChiTietSD(ItemID);
                    _syncContext.Send(state =>
                    {
                        gvgChiTiecSD.DataSource = data;
                    }, null);
                }));
                _theardRun.IsBackground = true;
                _theardRun.Start();
            }
            catch (Exception ex)
            {
                _log.Error("Frm_PossessionsInfomation => SynchronizedUse:" + ex.Message);
            }
        }
        private void SynchronizedMaintenance()
        {
            try
            {

                var _theardRun = new Thread(new ThreadStart(() =>
                {
                    var data = _defaultSevice.GetListMaintenance(ItemID);
                    _syncContext.Send(state =>
                    {
                        gvgMaintenance.DataSource = data;
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
        //private void SynchronizedSchedule()
        //{
        //    try
        //    {
        //        txtOJ.Text = "";
        //        txtSchedule_Coun.Text = "";
        //        txtSchedule_Date.Text = "";
        //        DataTable data = _defaultSevice.GetSheduleInfo(ItemID);
        //        if (data != null)
        //        {
        //            if (data.Rows.Count > 0)
        //            {
        //                txtOJ.Text = "" + data.Rows[0][cls_LichBaoTri_LL.col_SoPhieu];
        //                txtSchedule_Coun.Text = "" + data.Rows[0][cls_LichBaoTri_LL.col_LapLai] + " - " + data.Rows[0][cls_LichBaoTri_LL.col_LoaiBT];
        //                txtSchedule_Date.Text = Helper.ConvertSToDtime("" + data.Rows[0][cls_LichBaoTri_LL.col_Ngay]).ToString(E00_Helpers.Format.Formats.FDateDMYHM);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Error("DataLoading => SynchronizedSchedule:" + ex.Message);
        //    }
        //}
        private void SynchronizedSchedule()
        {
            try
            {
                txtOJ.Text = "";
                txtSchedule_Coun.Text = "";
                txtSchedule_Date.Text = "";
                _scheduleInfo = null;
                DataTable data = _defaultSevice.GetSheduleInfo(ItemID);
                if (data != null)
                {
                    if (data.Rows.Count > 0)
                    {
                        _scheduleInfo = _defaultSevice.ConvertRowToInfo<LichBaoTriLLInfo>(data.Rows[0]);
                        txtOJ.Text = "" + data.Rows[0][cls_LichBaoTri_LL.col_SoPhieu];
                        txtSchedule_Coun.Text = "" + data.Rows[0][cls_LichBaoTri_LL.col_LapLai] + " - " + data.Rows[0][cls_LichBaoTri_LL.col_LoaiBT];
                        txtSchedule_Date.Text = Helper.ConvertSToDtime("" + data.Rows[0][cls_LichBaoTri_LL.col_Ngay]).ToString(E00_Helpers.Format.Formats.FDateDMYHM);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("DataLoading => SynchronizedSchedule:" + ex.Message);
            }
        }
        private void OpenOJ()
        {
            try
            {
                if (_scheduleInfo != null)
                {
                    Frm_ScheduleJob frm_ScheduleJob = new Frm_ScheduleJob();
                    frm_ScheduleJob.Info = _scheduleInfo;
                    frm_ScheduleJob.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                _log.Error("Frm_ScheduleListReport => OpenOJ:" + ex.Message);
            }

        }
        private void AddScheduleJob()
        {
            try
            {
                Frm_ScheduleJob frmInfo = new Frm_ScheduleJob();
                frmInfo.Info = new LichBaoTriLLInfo();
                frmInfo.ShowDialog();
            }
            catch (Exception ex)
            {
                _log.Error("Frm_ScheduleListReport => AddScheduleJob:" + ex.Message);
            }
        }
        private void AddMaintenance()
        {
            try
            {
                Frm_Maintenance frmInfo = new Frm_Maintenance();
                frmInfo.Info = new BTBaoTriLLInfo();
                frmInfo.ShowDialog();
            }
            catch (Exception ex)
            {
                _log.Error("Frm_ScheduleListReport => OpenMaintainece:" + ex.Message);
            }
        }
        private bool ValidateDetail()
        {
            try
            {
                if (string.IsNullOrEmpty(slbLinhKien.txtMa.Text))
                {
                    TA_MessageBox.MessageBox.Show(LMaintenance.LLKSelectNull, TA_MessageBox.MessageIcon.Information);
                    return false;
                }
                _rowSelectedtmp = _defaultSevice.GetListLinhKienTheoMa(slbLinhKien.txtMa.Text);

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
        private void AddLinhkien()
        {
            try
            {

                if (ValidateDetail())
                {
                    bool isRefresh = false;
                    foreach (var item in _rowSelectedtmp)
                    {

                        var infoCheck = _defaultSevice.Get_Item("((" + cls_TaiSan_ChiTietLinhKien.col_MaLinhKien + "=" + item[cls_TaiSan_NhapCTSP.col_MaVach] + ") and (" + cls_TaiSan_ChiTietLinhKien.col_TrangThai + "=0)) or (("
                            + cls_TaiSan_ChiTietLinhKien.col_MaTaiSan + " = " + item[cls_TaiSan_NhapCTSP.col_MaVach] + ") and (" + cls_TaiSan_ChiTietLinhKien.col_MaLinhKien + "= " + lblMaVach.Text + ")) ");
                        if (infoCheck == null)
                        {
                            _detailInfo = new TaiSan_ChiTietLinhKienInfo();
                            _detailInfo.CTLKID = _defaultSevice.CreateNewID(20);
                            _detailInfo.MaTaiSan = lblMaVach.Text;
                            _detailInfo.MaLinhKien = "" + item[cls_TaiSan_NhapCTSP.col_MaVach];
                            _detailInfo.USERID = Helper.ConvertSToDec(E00_System.cls_System.sys_UserID);
                            _detailInfo.USERUD = _detailInfo.USERID;
                            _detailInfo.NgayTao = DateTime.Now;
                            _detailInfo.NgayUD = DateTime.Now;
                            _detailInfo.MACHINEID = _defaultSevice.GetMachineID();
                            _resultInfo = _defaultSevice.Insert(_detailInfo);
                            if (_resultInfo.Status)
                            {
                                isRefresh = true;
                            }
                            else
                            {
                                TA_MessageBox.MessageBox.Show(LNNChung.EUpdateFailed, TA_MessageBox.MessageIcon.Information);
                            }
                        }
                        else
                        {
                            TA_MessageBox.MessageBox.Show(LMaintenance.LLKDaTonTai, TA_MessageBox.MessageIcon.Information);
                        }
                    }
                    if (isRefresh)
                    {
                        SynchronizedPart();
                        slbLinhKien.clear();
                        slbLinhKien.txtTen.Focus();
                        // SynchronizedReference();
                    }

                }
            }
            catch (Exception ex)
            {
                _log.Error("Frm_BillOfMaterial => AddLinhkien:" + ex.Message);
            }
        }
        private void DeletePart()
        {
            
            try
            {

                if (dgvchitiet.CurrentRow == null) return;
                var rowSelected = dgvchitiet.CurrentRow.DataBoundItem as DataRowView;
                if (rowSelected != null)
                {
                    if (TA_MessageBox.MessageBox.Show(string.Format(LNNChung.LInfoDelete, rowSelected[cls_TaiSan_DanhMuc.col_Ten]), TA_MessageBox.MessageIcon.Question) == DialogResult.Yes)
                    {
                        _resultInfo = _defaultSevice.Delete((object)(rowSelected[cls_TaiSan_ChiTietLinhKien.col_CTLKID]));
                        if (_resultInfo.Status)
                        {
                            SynchronizedPart();
                        }
                        else
                        {
                            TA_MessageBox.MessageBox.Show(LNNChung.LInfoDeleteFail + " " + _resultInfo.SystemError, TA_MessageBox.MessageIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Frm_BillOfMaterial => DelectItem:" + ex.Message);
            }
        }
        #endregion

        //--------------------------------------------------
    }
}
