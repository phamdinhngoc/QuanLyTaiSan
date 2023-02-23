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
using E00_API.Maintenance;
using E00_Base;
using E00_Model;
using QuanLyTaiSan.ViewBasic;

namespace QuanLyTaiSan.Maintenance
{
    public partial class Frm_DeviceInfomationBk : LViewbasic
    {

        //--------------------------------------------------

        #region Member
        public string ItemID { get; set; }
        private api_TaiSan_ChiTietLinhKien _defaultSevice;
        private DataRow _rowCurrent;
        private const string DanhMuc = @"DM";


        #endregion

        //--------------------------------------------------

        #region Constructor
        public Frm_DeviceInfomationBk() : base()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        //--------------------------------------------------

        #region Private method
        protected override void Initialize()
        {
            base.Initialize();
            this.Load += (send, e) => DataLoading();
            gvgLinhKien.Initialize("");
            gvgChiTiecSD.Initialize("");
        }
        private void DataLoading()
        {
            _defaultSevice = new api_TaiSan_ChiTietLinhKien();
            Synchronized();
        }
        private void Synchronized()
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
        private void GetItemInfo()
        {
            _rowCurrent = _defaultSevice.GetInfo("" + ItemID);
            if (_rowCurrent != null)
            {
                FillControl(_rowCurrent);
            }
        }
        private void FillControl(DataRow _rowCurrent)
        {
            try
            {
                lblMaTS.Text = "" + _rowCurrent[cls_TaiSan_SuDungLL.col_Ma];
                lblMaVach.Text = "" + _rowCurrent[cls_TaiSan_SuDungLL.col_MaVach];
                lblKyHieu.Text = "" + _rowCurrent[DanhMuc + cls_TaiSan_DanhMuc.col_KyHieu];
                lblTen.Text = "" + _rowCurrent[DanhMuc + cls_TaiSan_DanhMuc.col_Ten];
                lblTenLoai.Text = "" + _rowCurrent[DanhMuc + cls_TaiSan_DanhMuc.col_MaLoai] + "-" + _rowCurrent[DanhMuc + cls_TaiSan_DanhMuc.col_TenLoai];
                lblNoiSX.Text = "" + _rowCurrent[DanhMuc + cls_TaiSan_DanhMuc.col_NuocSanXuat];
                lblMaVachSX.Text = "" + _rowCurrent[DanhMuc + cls_TaiSan_DanhMuc.col_MaVachSanXuat];
                lblQuyCach.Text = "" + _rowCurrent[DanhMuc + cls_TaiSan_DanhMuc.col_QuyCach];
                lblTaiLieu.Text = "" + _rowCurrent[DanhMuc + cls_TaiSan_DanhMuc.col_TaiLieu];
                lblTamNgung.Text = "" + _rowCurrent[DanhMuc + cls_TaiSan_DanhMuc.col_TamNgung];
                lblNgayTao.Text = "" + _rowCurrent[DanhMuc + cls_TaiSan_DanhMuc.col_NgayTao];

                lblSoPhieuNhap.Text = "" + _rowCurrent[cls_TaiSan_NhapLL.col_SoPhieu];
                lblNgayNhap.Text = "" + _rowCurrent[cls_TaiSan_NhapLL.col_NgayNhap];
                lblSoPhieuNhap.Text = "" + _rowCurrent[cls_TaiSan_NhapLL.col_NgayNhap];
                lblSoHoaDon.Text = "" + _rowCurrent[cls_TaiSan_NhapLL.col_SoHoaDon];
                lblNgayHoaDon.Text = "" + _rowCurrent[cls_TaiSan_NhapLL.col_NgayHoaDon];
                lblNhaCungCap.Text = "" + _rowCurrent[cls_TaiSan_NhapLL.col_MaNhaCungCap] + "-" + _rowCurrent["TenNCC"];
                lblDVT.Text = "" + _rowCurrent[cls_TaiSan_NhapCT.col_DonViTinh];
                lblDonGia.Text = "" + _rowCurrent[cls_TaiSan_NhapCT.col_DonGia];
                lblHanBaoHanh.Text = "" + _rowCurrent[cls_TaiSan_NhapCT.col_HanBaoHanh];
                lbldotkiemke.Text = "" + _rowCurrent[cls_TaiSan_SuDungLL.col_MaKiemKe];
                lblTenDotKiem.Text = "" + _rowCurrent[cls_TaiSan_SuDungLL.col_TenKiemKe];
                lblNgayKiem.Text = "" + _rowCurrent[cls_TaiSan_SuDungLL.col_NgayKiemKe];

                lblNgaySuDung.Text = "" + _rowCurrent[cls_TaiSan_SuDungLL.col_NgaySuDung];
                lblNguoiSudung.Text = "" + _rowCurrent[cls_TaiSan_SuDungLL.col_MaNguoiSuDung] + " - " + _rowCurrent[cls_TaiSan_SuDungLL.col_TenNguoiSuDung];
                lblKhoaSD.Text = "" + _rowCurrent[cls_TaiSan_SuDungLL.col_MaKPSuDung] + " - " + _rowCurrent[cls_TaiSan_SuDungLL.col_TenKPSuDung];
                lblKhoaQL.Text = "" + _rowCurrent[cls_TaiSan_SuDungLL.col_MaKPQuanLy] + " - " + _rowCurrent[cls_TaiSan_SuDungLL.col_TenKPQuanLy];
                lblKhu.Text = "" + _rowCurrent[cls_TaiSan_SuDungLL.col_MaKhu] + " - " + _rowCurrent[cls_TaiSan_SuDungLL.col_TenKhu];
                lblTang.Text = "" + _rowCurrent[cls_TaiSan_SuDungLL.col_MaTang] + " - " + _rowCurrent[cls_TaiSan_SuDungLL.col_TenTang];
                lblPhongCongNang.Text = "" + _rowCurrent[cls_TaiSan_SuDungLL.col_MaPhongCongNang] + " - " + _rowCurrent[cls_TaiSan_SuDungLL.col_TenPhongCongNang];
                lblTrangThai.Text = "" + _rowCurrent[cls_TaiSan_SuDungLL.col_TenTrangThai];

            }
            catch (Exception ex)
            {
                _log.Error("Frm_DeviceInfomation => FillControl:" + ex.Message);
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
                _log.Error("Frm_DeviceInfomation => SynchronizedPart:" + ex.Message);
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
                _log.Error("Frm_DeviceInfomation => SynchronizedUse:" + ex.Message);
            }
        }
        private void SynchronizedMaintenance()
        {
            try
            {

                var _theardRun = new Thread(new ThreadStart(() => {
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
                _log.Error("DataLoading => SynchronizedMaintenance:" + ex.Message);
            }
        }
        private void SynchronizedSchedule()
        {
            try
            {
                DataTable data = _defaultSevice.GetSheduleInfo(ItemID);
                if(data != null)
                {
                    if(data.Rows.Count > 0)
                    {
                        txtOJ.Text = "" + data.Rows[0][cls_LichBaoTri_LL.col_SoPhieu];
                        txtSchedule_Coun.Text = "" + data.Rows[0][cls_LichBaoTri_LL.col_LapLai];
                        txtSchedule_Date.Text = "" + data.Rows[0][cls_LichBaoTri_LL.col_Ngay];
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("DataLoading => SynchronizedSchedule:" + ex.Message);
            }
        }
        #endregion

        //--------------------------------------------------
    }
}
