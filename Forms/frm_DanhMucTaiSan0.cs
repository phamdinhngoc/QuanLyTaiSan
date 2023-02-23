using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using E00_Base;
using E00_Common;
using E00_Model;

namespace QuanLyTaiSan
{
    public partial class frm_DanhMucTaiSan0 : frm_DanhMuc
    {
        #region Khai báo
        private Acc_Oracle _acc = new Acc_Oracle();
        private Api_Common _api = new Api_Common();
        private List<string> _lst = new List<string>();
        private Dictionary<string, string> _lst2 = new Dictionary<string, string>();
        private Dictionary<string, string> _lst3 = new Dictionary<string, string>();
        private string _userError, _systemError, _id = string.Empty;
        private List<string> _lstCheck = new List<string>();
        private List<string> _lstCheckHoTen = new List<string>();
        private DataTable _tbDanhSachDuong = new DataTable();
        private string _maKhoa = string.Empty;
        private string _idPhong = string.Empty;
        public string _maKhoaLoad, _maPhongLoad, _indexOptionLoad = string.Empty;
        public string _tenKhoaLoad, _tenPhongLoad, _tenOptionLoad = string.Empty;
        private string _maBN;
        private bool _isAdd;
        private string _sql = string.Empty;
        private int _index, _check = 0, _deleteOneRecord = 0, _statusCheckAll = 0;

        #endregion

        #region Khởi tạo
        public frm_DanhMucTaiSan0()
        {
            InitializeComponent();
            _api.KetNoi();
            btnSua.Visible = false;
        }
        #endregion

        #region Phương thức

        #region Phương thức protected

        protected override void LoadData()
        {
            #region Load du lieu tat ca danh muc
            //LoadDuLieu(cls_QuanLyTaiSan._loaiTaiSan, slbLoaiTaiSan);
            //LoadDuLieu(cls_QuanLyTaiSan._lyDoTangGiam, slbMaTangTS);
            //LoadDuLieu(cls_QuanLyTaiSan._kieuKH, slbKieuKH);
            //LoadDuLieu(cls_QuanLyTaiSan._ngoaiTe, slbTyGia);
            //LoadDuLieu(cls_QuanLyTaiSan._boPhanSuDung, slbMaBoPhan);
            //LoadDuLieu(cls_QuanLyTaiSan._danhMucKhoan, slbTKTaiSan);
            //LoadDuLieu(cls_QuanLyTaiSan._danhMucKhoan, slbTKKhauHao);
            //LoadDuLieu(cls_QuanLyTaiSan._danhMucKhoan, slbTKNguon);
            //LoadDuLieu(cls_QuanLyTaiSan._danhMucPhi, slbMaPhi);
            //LoadDuLieu(cls_QuanLyTaiSan._phanNhom, slbNhom1);
            //LoadDuLieu(cls_QuanLyTaiSan._phanNhom, slbNhom2);
            //LoadDuLieu(cls_QuanLyTaiSan._phanNhom, slbNhom3);
            //LoadDuLieu(cls_QuanLyTaiSan._phanNhom, slbNhom4);
            //LoadDuLieu(cls_QuanLyTaiSan._nguonKinhPhi, slbNguonKP);
            //LoadDuLieu(cls_QuanLyTaiSan._chuong, slbChuong);
            //LoadDuLieu(cls_QuanLyTaiSan._mucTieuMuc, slbMuc);
            //LoadDuLieu(cls_QuanLyTaiSan._nghiepVu, slbNghiepVu);
            //LoadDuLieu(cls_QuanLyTaiSan._coCauVon, slbCoCauVon);
            //LoadDuLieu(cls_QuanLyTaiSan._taiKhoanNganHang, slbTKNganHangKho);
            //LoadDuLieu(cls_QuanLyTaiSan._capPhat, slbCapPhat);
            //LoadDuLieu(cls_QuanLyTaiSan._danhMucKhoan, slbKhoan);
            //LoadDuLieu(cls_QuanLyTaiSan._mucTieuMuc, slbTieuMuc);
            //LoadDuLieu(cls_QuanLyTaiSan._hoatDongSuNghiep, slbHoatDongSN);
            //LoadDuLieu(cls_QuanLyTaiSan._maThongKe, slbMaThongKe);
            //LoadDuLieu(cls_QuanLyTaiSan._vuViecCongTrinh, slbCTMucTieuDuAn);
            #endregion
            TimKiem();
            Show_ChiTiet();
            base.LoadData();
        }

        protected override void Them()
        {
            _isAdd = true;
            ClearData();
            //txtSoTheTaiSan.Focus();
            base.Them();
        }

        protected override void Xoa()
        {
            try
            {
                _lst2.Clear();
                #region Delete each record
                if (_deleteOneRecord == 1)
                {
                    string id = dgvDanhSachTaiSan.Rows[_index].Cells["col_id"].Value.ToString();
                    _lst2.Add(cls_TS_TaiSan.col_Id.ToUpper(), id.ToUpper());
                    if (TA_MessageBox.MessageBox.Show("Bạn có chắc chắn muốn xóa: " + txtTenTaiSan.Text,
                        "Xác nhận", "Có~Không~Bỏ qua~0",
                        TA_MessageBox.MessageButton.YesNoCancel, TA_MessageBox.MessageIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (!_api.Delete(ref _userError, ref _systemError, cls_TS_TaiSan.tb_TenBang, _lst2, null))
                        {
                            TA_MessageBox.MessageBox.Show(string.Format("Không thể xóa {0}. Lỗi: {1} !!!!",
                                txtTenTaiSan.Text, _userError), "Lỗi", "Đồng ý~0",
                                TA_MessageBox.MessageButton.OK, TA_MessageBox.MessageIcon.Error);
                            dgvDanhSachTaiSan.Focus();
                            return;
                        }
                    }
                }
                #endregion
                else
                {
                    #region Delete all record have checked
                    string str = "", strHoTen = string.Empty;
                    ///append id
                    foreach (var item in _lstCheck)
                    {
                        str += item.ToString() + ",";
                    }
                    StringBuilder str1 = new StringBuilder(str);
                    str1 = str1.Remove(str.Length - 1, 1);

                    ///append name
                    foreach (var item in _lstCheckHoTen)
                    {
                        strHoTen += item.ToString() + ",";
                    }
                    StringBuilder str2 = new StringBuilder(strHoTen);
                    str2 = str2.Remove(str2.Length - 1, 1);


                    if (TA_MessageBox.MessageBox.Show("Bạn có chắc chắn muốn xóa  " + str2 + " không?",
                        "Xác nhận", "Có~Không~Bỏ qua~0",
                            TA_MessageBox.MessageButton.YesNoCancel, TA_MessageBox.MessageIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        _lst2.Add(cls_TS_TaiSan.col_Id.ToUpper(), str1.ToString());
                        if (!_api.DeleteAll(ref _userError, ref _systemError, cls_TS_TaiSan.tb_TenBang, _lst2, null))
                        {
                            TA_MessageBox.MessageBox.Show(string.Format("Không thể xóa {0}. Lỗi: {1} !!!!",
                                txtTenTaiSan.Text, _userError), "Lỗi", "Đồng ý~0",
                                TA_MessageBox.MessageButton.OK, TA_MessageBox.MessageIcon.Error);
                            dgvDanhSachTaiSan.Focus();
                            return;
                        }
                        _lstCheck.Clear(); _lstCheckHoTen.Clear();
                    }
                    #endregion
                }
                LoadData();
                ClearData();
                base.Xoa();
            }
            catch
            {

                return;
            }
        }

        protected override void Sua()
        {
            _isAdd = false;
            base.Sua();
        }

        protected override void Luu()
        {

            if (_isAdd && AcceptData())
            {
                _lst2.Clear(); _lst.Clear();
                //_lst2.Add(cls_TS_TaiSan.col_Ma, _tT.LayMaTaiSanTuDong());
                _lst2.Add(cls_TS_TaiSan.col_Ma, DateTime.Now.ToString("yyyyMMddHHmmss"));
                //_lst2.Add(cls_TS_TaiSan.col_SoTheTaiSan, txtSoTheTaiSan.Text);
                _lst2.Add(cls_TS_TaiSan.col_TenTaiSan, txtTenTaiSan.Text);
                _lst2.Add(cls_TS_TaiSan.col_LoaiTaiSan, slbLoaiTaiSan.txtMa.Text);
                //_lst2.Add(cls_TS_TaiSan.col_MaTangTS, slbMaTangTS.txtMa.Text);
                //_lst2.Add(cls_TS_TaiSan.col_NgayTang, dtpNgayTang.Value.ToString());
                //_lst2.Add(cls_TS_TaiSan.col_SoKyKH, txtSoKyKH.Text);
                //_lst2.Add(cls_TS_TaiSan.col_KieuKH, slbKieuKH.txtMa.Text);
                _lst2.Add(cls_TS_TaiSan.col_MaVach, txtMaVach.Text);
                //_lst2.Add(cls_TS_TaiSan.col_DonViTinh, txtDonViTinh.Text);
                //_lst2.Add(cls_TS_TaiSan.col_MaPhi, slbMaPhi.txtMa.Text);
                //_lst2.Add(cls_TS_TaiSan.col_NgayTinhKH, dtpNgayTinhKH.Value.ToString());
               // _lst2.Add(cls_TS_TaiSan.col_NgayKetThucKH, dtpNgayKetThucKH.Value.ToString());
                //_lst2.Add(cls_TS_TaiSan.col_TongSanLuong, txtTongSanLuong.Text);
               // _lst2.Add(cls_TS_TaiSan.col_SoChungTu, txtSoChungTu.Text);
               // _lst2.Add(cls_TS_TaiSan.col_NgayChungTu, dtpNgayChungTu.Value.ToString());
               // _lst2.Add(cls_TS_TaiSan.col_TyGia, slbTyGia.txtMa.Text);
               // _lst2.Add(cls_TS_TaiSan.col_MaBoPhan, slbMaBoPhan.txtMa.Text);
                //_lst2.Add(cls_TS_TaiSan.col_TKTaiSan, slbTKTaiSan.txtMa.Text);
               // _lst2.Add(cls_TS_TaiSan.col_TKKhauHao, slbTKKhauHao.txtMa.Text);
               // _lst2.Add(cls_TS_TaiSan.col_TKNguon, slbTKNguon.txtMa.Text);
                _lst2.Add(cls_TS_TaiSan.col_NgayUD, DateTime.Now.ToString());
                _lst2.Add(cls_TS_TaiSan.col_MachineID, cls_Common.Get_MachineID());
                //Thông tin các field ở tab
                _lst2.Add(cls_TS_TaiSan.col_TenKhac, txtTenKhac.Text);
                _lst2.Add(cls_TS_TaiSan.col_ThongSoKT, txtThongSo.Text);
              //  _lst2.Add(cls_TS_TaiSan.col_LyDoDinhChi, txtLyDoDinhChi.Text);
                _lst2.Add(cls_TS_TaiSan.col_GhiChu, txtCongSuat.Text);

                //_lst2.Add(cls_TS_TaiSan.col_Nhom1, slbNhom1.txtMa.Text);
                //_lst2.Add(cls_TS_TaiSan.col_Nhom2, slbNhom2.txtMa.Text);
                //_lst2.Add(cls_TS_TaiSan.col_Nhom3, slbNhom3.txtMa.Text);
                //_lst2.Add(cls_TS_TaiSan.col_Nhom4, slbNhom4.txtMa.Text);

                //_lst2.Add(cls_TS_TaiSan.col_SoKyKHQuyDinh, txtSoKyKHQD.Text);
                //_lst2.Add(cls_TS_TaiSan.col_TyLeKHVuotMuc, txtTyLeKHVuotMuc.Text);
                //_lst2.Add(cls_TS_TaiSan.col_HSChoVuotMuc, txtHSChoVuotMuc.Text);
                //_lst2.Add(cls_TS_TaiSan.col_SoKyKHConLai, txtSoKyKHConLai.Text);

                _lst2.Add(cls_TS_TaiSan.col_SoHieuTaiSan, txtKyHieu.Text);
                //_lst2.Add(cls_TS_TaiSan.col_NgaySuDung, dtpNgaySuDung.Value.ToString());
                //_lst2.Add(cls_TS_TaiSan.col_NgayDinhChi, dtpNgayDinhChi.Value.ToString());
                //_lst2.Add(cls_TS_TaiSan.col_NuocSanXuat, txtNuocSX.Text);
                //_lst2.Add(cls_TS_TaiSan.col_NamSanXuat, txtNamSX.Text);

                //_lst2.Add(cls_TS_TaiSan.col_GiaTriLamTron, txtGiaTriLamTron.Text);
                //_lst2.Add(cls_TS_TaiSan.col_NguyenGia, txtNguyenGia.Text);
                //_lst2.Add(cls_TS_TaiSan.col_GisTriDaKH, txtGiaTriDaKH.Text);
                //_lst2.Add(cls_TS_TaiSan.col_GiaTriConLai, txtGiaTriConLai.Text);

                //_lst2.Add(cls_TS_TaiSan.col_GiaTriKHMotKy, txtGiaTriKHMotKy.Text);
                //_lst2.Add(cls_TS_TaiSan.col_NguonKP, slbNguonKP.txtMa.Text);
                //_lst2.Add(cls_TS_TaiSan.col_Chuong, slbChuong.txtMa.Text);
                //_lst2.Add(cls_TS_TaiSan.col_Muc, slbMuc.txtMa.Text);

                //_lst2.Add(cls_TS_TaiSan.col_NghiepVu, slbNghiepVu.txtMa.Text);
                //_lst2.Add(cls_TS_TaiSan.col_CoCauVon, slbCoCauVon.txtMa.Text);
                //_lst2.Add(cls_TS_TaiSan.col_TKNganHangKho, slbTKNganHangKho.txtMa.Text);

                //_lst2.Add(cls_TS_TaiSan.col_DaIn, ckDaIn.Checked ? "1" : "0");
                //_lst2.Add(cls_TS_TaiSan.col_CapPhat, slbCapPhat.txtMa.Text);
                //_lst2.Add(cls_TS_TaiSan.col_Khoan, slbKhoan.txtMa.Text);
                //_lst2.Add(cls_TS_TaiSan.col_TieuMuc, slbTieuMuc.txtMa.Text);

                //_lst2.Add(cls_TS_TaiSan.col_HoatDongSN, slbHoatDongSN.txtMa.Text);
                //_lst2.Add(cls_TS_TaiSan.col_MaThongKe, slbMaThongKe.txtMa.Text);
                //_lst2.Add(cls_TS_TaiSan.col_ChiTietMucTieuDuAn, slbCTMucTieuDuAn.txtMa.Text);
                //_lst2.Add(cls_TS_TaiSan.col_DaKiemKe, ckDaKiemKe.Checked ? "1" : "0");
                //
                //_lst.Add(cls_TS_TaiSan.col_Id);
                _lst.Add(cls_TS_TaiSan.col_Ma);
                //if (!_api.Insert(ref _userError, ref _systemError, cls_TS_TaiSan.tb_TenBang, _lst2, _lst, _lst))
                //{
                //    TA_MessageBox.MessageBox.Show("Bệnh nhân này đã được thêm, không thể thêm!", "Thông báo", "OK", TA_MessageBox.MessageButton.OK, TA_MessageBox.MessageIcon.Error);
                //    txtSoTheTaiSan.Focus();
                //    return;
                //}
                TA_MessageBox.MessageBox.Show("Thêm thành công!", "Thông báo", "OK", TA_MessageBox.MessageButton.OK, TA_MessageBox.MessageIcon.Information);
                LoadData();
                base.Luu();
            }
            else if (!_isAdd && AcceptData())
            {
                string ma = dgvDanhSachTaiSan.Rows[dgvDanhSachTaiSan.CurrentCell.RowIndex].Cells["col_Ma"].Value.ToString();
                string id = dgvDanhSachTaiSan.Rows[dgvDanhSachTaiSan.CurrentCell.RowIndex].Cells["col_Id"].Value.ToString();
                _lst2.Clear(); _lst.Clear(); _lst3.Clear();
                //_lst2.Add(cls_TS_TaiSan.col_SoTheTaiSan, txtSoTheTaiSan.Text);
                _lst2.Add(cls_TS_TaiSan.col_TenTaiSan, txtTenTaiSan.Text);
                _lst2.Add(cls_TS_TaiSan.col_LoaiTaiSan, slbLoaiTaiSan.txtMa.Text);
                //_lst2.Add(cls_TS_TaiSan.col_MaTangTS, slbMaTangTS.txtMa.Text);
                //_lst2.Add(cls_TS_TaiSan.col_NgayTang, dtpNgayTang.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                //_lst2.Add(cls_TS_TaiSan.col_SoKyKH, txtSoKyKH.Text);
                //_lst2.Add(cls_TS_TaiSan.col_KieuKH, slbKieuKH.txtMa.Text);
                _lst2.Add(cls_TS_TaiSan.col_MaVach, txtMaVach.Text);
                //_lst2.Add(cls_TS_TaiSan.col_DonViTinh, txtDonViTinh.Text);
                //_lst2.Add(cls_TS_TaiSan.col_MaPhi, slbMaPhi.txtMa.Text);
                //_lst2.Add(cls_TS_TaiSan.col_NgayTinhKH, dtpNgayTinhKH.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                //_lst2.Add(cls_TS_TaiSan.col_NgayKetThucKH, dtpNgayKetThucKH.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                //_lst2.Add(cls_TS_TaiSan.col_TongSanLuong, txtTongSanLuong.Text);
                //_lst2.Add(cls_TS_TaiSan.col_SoChungTu, txtSoChungTu.Text);
                //_lst2.Add(cls_TS_TaiSan.col_NgayChungTu, dtpNgayChungTu.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                //_lst2.Add(cls_TS_TaiSan.col_TyGia, slbTyGia.txtMa.Text);
                //_lst2.Add(cls_TS_TaiSan.col_MaBoPhan, slbMaBoPhan.txtMa.Text);
                //_lst2.Add(cls_TS_TaiSan.col_TKTaiSan, slbTKTaiSan.txtMa.Text);
                //_lst2.Add(cls_TS_TaiSan.col_TKKhauHao, slbTKKhauHao.txtMa.Text);
                //_lst2.Add(cls_TS_TaiSan.col_TKNguon, slbTKNguon.txtMa.Text);
                _lst2.Add(cls_TS_TaiSan.col_NgayUD, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                _lst2.Add(cls_TS_TaiSan.col_MachineID, cls_Common.Get_MachineID());
                //Thông tin các field ở tab
                _lst2.Add(cls_TS_TaiSan.col_TenKhac, txtTenKhac.Text);
                _lst2.Add(cls_TS_TaiSan.col_ThongSoKT, txtThongSo.Text);
                //_lst2.Add(cls_TS_TaiSan.col_LyDoDinhChi, txtLyDoDinhChi.Text);
                _lst2.Add(cls_TS_TaiSan.col_GhiChu, txtCongSuat.Text);

                //_lst2.Add(cls_TS_TaiSan.col_Nhom1, slbNhom1.txtMa.Text);
                //_lst2.Add(cls_TS_TaiSan.col_Nhom2, slbNhom2.txtMa.Text);
                //_lst2.Add(cls_TS_TaiSan.col_Nhom3, slbNhom3.txtMa.Text);
                //_lst2.Add(cls_TS_TaiSan.col_Nhom4, slbNhom4.txtMa.Text);

                //_lst2.Add(cls_TS_TaiSan.col_SoKyKHQuyDinh, txtSoKyKHQD.Text);
                //_lst2.Add(cls_TS_TaiSan.col_TyLeKHVuotMuc, txtTyLeKHVuotMuc.Text);
                //_lst2.Add(cls_TS_TaiSan.col_HSChoVuotMuc, txtHSChoVuotMuc.Text);
                //_lst2.Add(cls_TS_TaiSan.col_SoKyKHConLai, txtSoKyKHConLai.Text);

                _lst2.Add(cls_TS_TaiSan.col_SoHieuTaiSan, txtKyHieu.Text);
                //_lst2.Add(cls_TS_TaiSan.col_NgaySuDung, dtpNgaySuDung.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                //_lst2.Add(cls_TS_TaiSan.col_NgayDinhChi, dtpNgayDinhChi.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                //_lst2.Add(cls_TS_TaiSan.col_NuocSanXuat, txtNuocSX.Text);
                //_lst2.Add(cls_TS_TaiSan.col_NamSanXuat, txtNamSX.Text);

                //_lst2.Add(cls_TS_TaiSan.col_GiaTriLamTron, txtGiaTriLamTron.Text);
                //_lst2.Add(cls_TS_TaiSan.col_NguyenGia, txtNguyenGia.Text);
                //_lst2.Add(cls_TS_TaiSan.col_GisTriDaKH, txtGiaTriDaKH.Text);
                //_lst2.Add(cls_TS_TaiSan.col_GiaTriConLai, txtGiaTriConLai.Text);

                //_lst2.Add(cls_TS_TaiSan.col_GiaTriKHMotKy, txtGiaTriKHMotKy.Text);
                //_lst2.Add(cls_TS_TaiSan.col_NguonKP, slbNguonKP.txtMa.Text);
                //_lst2.Add(cls_TS_TaiSan.col_Chuong, slbChuong.txtMa.Text);
                //_lst2.Add(cls_TS_TaiSan.col_Muc, slbMuc.txtMa.Text);

                //_lst2.Add(cls_TS_TaiSan.col_NghiepVu, slbNghiepVu.txtMa.Text);
                //_lst2.Add(cls_TS_TaiSan.col_CoCauVon, slbCoCauVon.txtMa.Text);
                ////_lst2.Add(cls_TS_TaiSan.col_Chuong, slbChuong.txtMa.Text);
                //_lst2.Add(cls_TS_TaiSan.col_TKNganHangKho, slbTKNganHangKho.txtMa.Text);

                //_lst2.Add(cls_TS_TaiSan.col_DaIn, ckDaIn.Checked ? "1" : "0");
                //_lst2.Add(cls_TS_TaiSan.col_CapPhat, slbCapPhat.txtMa.Text);
                //_lst2.Add(cls_TS_TaiSan.col_Khoan, slbKhoan.txtMa.Text);
                //_lst2.Add(cls_TS_TaiSan.col_TieuMuc, slbTieuMuc.txtMa.Text);

                //_lst2.Add(cls_TS_TaiSan.col_HoatDongSN, slbHoatDongSN.txtMa.Text);
                //_lst2.Add(cls_TS_TaiSan.col_MaThongKe, slbMaThongKe.txtMa.Text);
                //_lst2.Add(cls_TS_TaiSan.col_ChiTietMucTieuDuAn, slbCTMucTieuDuAn.txtMa.Text);
                //_lst2.Add(cls_TS_TaiSan.col_DaKiemKe, ckDaKiemKe.Checked ? "1" : "0");
                ////
                _lst.Add(cls_TS_TaiSan.col_NgayTang);
                _lst.Add(cls_TS_TaiSan.col_NgayTinhKH);
                _lst.Add(cls_TS_TaiSan.col_NgayKetThucKH);
                _lst.Add(cls_TS_TaiSan.col_NgayChungTu);
                _lst.Add(cls_TS_TaiSan.col_NgayUD);
                _lst.Add(cls_TS_TaiSan.col_NgaySuDung);
                _lst.Add(cls_TS_TaiSan.col_NgayDinhChi);
                _lst3.Add(cls_TS_TaiSan.col_Id, id);
                _lst3.Add(cls_TS_TaiSan.col_Ma, ma);
                //if (!_api.Update(ref _userError, ref _systemError, cls_TS_TaiSan.tb_TenBang, _lst2, _lst, _lst3))
                //{
                //    TA_MessageBox.MessageBox.Show("Không thể cập nhật!", "Thông báo", "OK", TA_MessageBox.MessageButton.OK, TA_MessageBox.MessageIcon.Error);
                //    txtSoTheTaiSan.Focus();
                //    return;
                //}
                TA_MessageBox.MessageBox.Show("Cập nhật thành công!", "Thông báo", "OK", TA_MessageBox.MessageButton.OK, TA_MessageBox.MessageIcon.Information);
                LoadData();
                base.Luu();
            }

        }

        protected override void BoQua()
        {
            Show_ChiTiet();
            base.BoQua();
        }

        protected override void Thoat()
        {
            base.Thoat();
        }

        protected override void TimKiem()
        {
            try
            {
                _lst2.Clear();
                _chuoiTimKiem = txtTimKiem.Text;
                Dictionary<string, string> lst3 = new Dictionary<string, string>();
                lst3.Add(cls_TS_TaiSan.col_TenTaiSan, _chuoiTimKiem);
                //dgvDanhSachTaiSan.DataSource = _api.Search(ref _userError, ref _systemError, cls_TS_TaiSan.tb_TenBang, count: -1, dicLike: lst3, orderByASC1: true, orderByName1: cls_D_DMLoaiGiuong.col_ID);
                dgvDanhSachTaiSan.DataSource = _api.Search(ref _userError, ref _systemError, cls_TS_TaiSan.tb_TenBang, count: -1, dicLike: lst3, orderByASC1: true);

                _count = dgvDanhSachTaiSan.RowCount;
                //col_Id.DataPropertyName = "ID";
                //col_Ma.DataPropertyName = "MA";
                //col_TenTS.DataPropertyName = "TENTAISAN";
                //col_MaVach.DataPropertyName = "MAVACH";
                //col_ChungTu.DataPropertyName = "SOCHUNGTU";
                //col_NgayChungTu.DataPropertyName = "NGAYCHUNGTU";
                //col_LoaiTaiSan.DataPropertyName = "LOAITAISAN";
                //col_SoTheTS.DataPropertyName = "SOTHETAISAN";
                //col_DVT.DataPropertyName = "DONVITINH";
                base.TimKiem();
            }
            catch
            {

                return;
            }

        }

        protected override void Show_ChiTiet()
        {
            //ClearData();
            //int _index = dgvDanhSachTaiSan.CurrentRow.Index;
            //DataTable tb = (DataTable)dgvDanhSachTaiSan.DataSource;
            //DataRow row = tb.Rows[_index];

            //#region Số thẻ tài sản
            //try
            //{
            //    txtSoTheTaiSan.Text = row[cls_TS_TaiSan.col_SoTheTaiSan].ToString();
            //}
            //catch
            //{
            //    txtSoTheTaiSan.Text = "";
            //}
            //#endregion
            //#region Tên tài sản
            //try
            //{
            //    txtTenTaiSan.Text = row[cls_TS_TaiSan.col_TenTaiSan].ToString();
            //}
            //catch
            //{
            //    txtTenTaiSan.Text = "";

            //}
            //try
            //{
            //    txtSoChungTu.Text = row[cls_TS_TaiSan.col_SoChungTu].ToString();
            //}
            //catch
            //{
            //    txtSoChungTu.Text = "";

            //}
            //#endregion
            //#region Số kỳ kh
            //try
            //{
            //    txtSoKyKH.Text = row[cls_TS_TaiSan.col_SoKyKH].ToString();
            //}
            //catch
            //{
            //    txtSoKyKH.Text = "";

            //}
            //#endregion
            //#region Mã vạch
            //try
            //{
            //    txtMaVach.Text = row[cls_TS_TaiSan.col_MaVach].ToString();
            //}
            //catch
            //{
            //    txtMaVach.Text = "";

            //}
            //#endregion
            //#region Đơn vị tính
            //try
            //{
            //    txtDonViTinh.Text = row[cls_TS_TaiSan.col_DonViTinh].ToString();
            //}
            //catch
            //{
            //    txtDonViTinh.Text = "";

            //}
            //#endregion
            //#region Tổng sản lượng
            //try
            //{
            //    txtTongSanLuong.Text = row[cls_TS_TaiSan.col_TongSanLuong].ToString();
            //}
            //catch
            //{
            //    txtTongSanLuong.Text = "";

            //}
            //#endregion

            //#region Ma tang TS
            //try
            //{
            //    DataRow drMaTS = getRowByID(slbMaTangTS.DataSource, "MA='" + row[cls_TS_TaiSan.col_MaTangTS].ToString() + "'");
            //    slbMaTangTS.txtMa.Text = drMaTS["MA"].ToString();
            //    slbMaTangTS.txtTen.Text = drMaTS["TEN"].ToString();
            //}
            //catch
            //{
            //}
            //#endregion

            //try
            //{
            //    DataRow drLoaiTS = getRowByID(slbLoaiTaiSan.DataSource, "MA='" + row[cls_TS_TaiSan.col_LoaiTaiSan].ToString() + "'");
            //    slbLoaiTaiSan.txtMa.Text = drLoaiTS["MA"].ToString();
            //    slbLoaiTaiSan.txtTen.Text = drLoaiTS["TEN"].ToString();
            //}
            //catch
            //{
            //}

            //try
            //{
            //    dtpNgayTang.Value = DateTime.Parse(row[cls_TS_TaiSan.col_NgayTang].ToString(), System.Globalization.CultureInfo.InvariantCulture);
            //}
            //catch
            //{
            //}
            //try
            //{
            //    DataRow drKieuKH = getRowByID(slbKieuKH.DataSource, "MA='" + row[cls_TS_TaiSan.col_KieuKH].ToString() + "'");
            //    slbKieuKH.txtMa.Text = drKieuKH["MA"].ToString();
            //    slbKieuKH.txtTen.Text = drKieuKH["TEN"].ToString();
            //}
            //catch
            //{
            //}
            //try
            //{
            //    DataRow drMaPhi = getRowByID(slbMaPhi.DataSource, "MA='" + row[cls_TS_TaiSan.col_MaPhi].ToString() + "'");
            //    slbMaPhi.txtMa.Text = drMaPhi["MA"].ToString();
            //    slbMaPhi.txtTen.Text = drMaPhi["TEN"].ToString();
            //}
            //catch
            //{
            //}

            //try
            //{
            //    DataRow drTiGia = getRowByID(slbTyGia.DataSource, "MA='" + row[cls_TS_TaiSan.col_TyGia].ToString() + "'");
            //    slbTyGia.txtMa.Text = drTiGia["MA"].ToString();
            //    slbTyGia.txtTen.Text = drTiGia["TEN"].ToString();
            //}
            //catch
            //{
            //}

            //try
            //{
            //    DataRow drMaBoPhan = getRowByID(slbMaBoPhan.DataSource, "MA='" + row[cls_TS_TaiSan.col_MaBoPhan].ToString() + "'");
            //    slbMaBoPhan.txtMa.Text = drMaBoPhan["MA"].ToString();
            //    slbMaBoPhan.txtTen.Text = drMaBoPhan["TEN"].ToString();
            //}
            //catch
            //{
            //}

            //try
            //{
            //    DataRow drTKTaiSan = getRowByID(slbTKTaiSan.DataSource, "MA='" + row[cls_TS_TaiSan.col_TKTaiSan].ToString() + "'");
            //    slbTKTaiSan.txtMa.Text = drTKTaiSan["MA"].ToString();
            //    slbTKTaiSan.txtTen.Text = drTKTaiSan["TEN"].ToString();
            //}
            //catch
            //{
            //}

            //try
            //{
            //    DataRow drTKKhauHao = getRowByID(slbTKKhauHao.DataSource, "MA='" + row[cls_TS_TaiSan.col_TKKhauHao].ToString() + "'");
            //    slbTKKhauHao.txtMa.Text = drTKKhauHao["MA"].ToString();
            //    slbTKKhauHao.txtTen.Text = drTKKhauHao["TEN"].ToString();
            //}
            //catch
            //{
            //}
            //try
            //{
            //    DataRow drTKNguon = getRowByID(slbTKNguon.DataSource, "MA='" + row[cls_TS_TaiSan.col_TKNguon].ToString() + "'");
            //    slbTKNguon.txtMa.Text = drTKNguon["MA"].ToString();
            //    slbTKNguon.txtTen.Text = drTKNguon["TEN"].ToString();
            //}
            //catch
            //{
            //}
            //try
            //{
            //    dtpNgayTinhKH.Value = DateTime.Parse(row[cls_TS_TaiSan.col_NgayTinhKH].ToString());
            //}
            //catch
            //{
            //}
            //try
            //{
            //    dtpNgayKetThucKH.Value = DateTime.Parse(row[cls_TS_TaiSan.col_NgayKetThucKH].ToString());
            //}
            //catch
            //{
            //}
            //try
            //{
            //    dtpNgayChungTu.Value = DateTime.Parse(row[cls_TS_TaiSan.col_NgayChungTu].ToString(), System.Globalization.CultureInfo.InvariantCulture);
            //}
            //catch
            //{
            //}
            //try
            //{
            //    txtTenKhac.Text = row[cls_TS_TaiSan.col_TenKhac].ToString();
            //}
            //catch
            //{
            //    txtTenKhac.Text = "";

            //}

            //try
            //{
            //    txtThongSoKT.Text = row[cls_TS_TaiSan.col_ThongSoKT].ToString();
            //}
            //catch
            //{
            //    txtThongSoKT.Text = "";

            //}
            //try
            //{
            //    txtLyDoDinhChi.Text = row[cls_TS_TaiSan.col_LyDoDinhChi].ToString();
            //}
            //catch
            //{
            //    txtLyDoDinhChi.Text = "";
            //}
            //try
            //{
            //    txtGhiChu.Text = row[cls_TS_TaiSan.col_GhiChu].ToString();
            //}
            //catch
            //{
            //}
            //try
            //{
            //    DataRow drNhom1 = getRowByID(slbNhom1.DataSource, "MA='" + row[cls_TS_TaiSan.col_Nhom1].ToString() + "'");
            //    slbNhom1.txtMa.Text = drNhom1["MA"].ToString();
            //    slbNhom1.txtTen.Text = drNhom1["TEN"].ToString();
            //}
            //catch
            //{
            //}

            //try
            //{
            //    DataRow drNhom2 = getRowByID(slbNhom2.DataSource, "MA='" + row[cls_TS_TaiSan.col_Nhom2].ToString() + "'");
            //    slbNhom2.txtMa.Text = drNhom2["MA"].ToString();
            //    slbNhom2.txtTen.Text = drNhom2["TEN"].ToString();
            //}
            //catch
            //{
            //}

            //try
            //{
            //    DataRow drNhom3 = getRowByID(slbNhom3.DataSource, "MA='" + row[cls_TS_TaiSan.col_Nhom3].ToString() + "'");
            //    slbNhom3.txtMa.Text = drNhom3["MA"].ToString();
            //    slbNhom3.txtTen.Text = drNhom3["TEN"].ToString();
            //}
            //catch
            //{
            //}

            //try
            //{
            //    DataRow drNhom4 = getRowByID(slbNhom4.DataSource, "MA='" + row[cls_TS_TaiSan.col_Nhom4].ToString() + "'");
            //    slbNhom4.txtMa.Text = drNhom4["MA"].ToString();
            //    slbNhom4.txtTen.Text = drNhom4["TEN"].ToString();
            //}
            //catch
            //{
            //}

            //try
            //{
            //    txtSoKyKHQD.Text = row[cls_TS_TaiSan.col_SoKyKHQuyDinh].ToString();
            //}
            //catch
            //{
            //    txtSoKyKHQD.Text = "";
            //}
            //try
            //{
            //    txtTyLeKHVuotMuc.Text = row[cls_TS_TaiSan.col_TyLeKHVuotMuc].ToString();
            //}
            //catch
            //{
            //    txtTyLeKHVuotMuc.Text = "";
            //}
            //try
            //{
            //    txtHSChoVuotMuc.Text = row[cls_TS_TaiSan.col_HSChoVuotMuc].ToString();
            //}
            //catch
            //{
            //    txtHSChoVuotMuc.Text = "";
            //}
            //try
            //{
            //    txtSoKyKHConLai.Text = row[cls_TS_TaiSan.col_SoKyKHConLai].ToString();
            //}
            //catch
            //{
            //    txtSoKyKHConLai.Text = "";
            //}
            //try
            //{
            //    txtKyHieu.Text = row[cls_TS_TaiSan.col_SoHieuTaiSan].ToString();
            //}
            //catch
            //{
            //    txtKyHieu.Text = "";
            //}

            //try
            //{
            //    dtpNgaySuDung.Value = DateTime.Parse(row[cls_TS_TaiSan.col_NgaySuDung].ToString());
            //}
            //catch
            //{
            //}
            //try
            //{
            //    dtpNgayDinhChi.Value = DateTime.Parse(row[cls_TS_TaiSan.col_NgayDinhChi].ToString());
            //}
            //catch
            //{
            //}
            //try
            //{
            //    txtNuocSX.Text = row[cls_TS_TaiSan.col_NuocSanXuat].ToString();
            //}
            //catch
            //{
            //    txtNuocSX.Text = "";
            //}
            //try
            //{
            //    txtNamSX.Text = row[cls_TS_TaiSan.col_NamSanXuat].ToString();
            //}
            //catch
            //{
            //    txtNamSX.Text = "";
            //}
            //try
            //{
            //    txtGiaTriLamTron.Text = row[cls_TS_TaiSan.col_GiaTriLamTron].ToString();
            //}
            //catch
            //{
            //    txtGiaTriLamTron.Text = "";
            //}
            //try
            //{
            //    txtNguyenGia.Text = row[cls_TS_TaiSan.col_NguyenGia].ToString();
            //}
            //catch
            //{
            //    txtNguyenGia.Text = "";
            //}
            //try
            //{
            //    txtGiaTriDaKH.Text = row[cls_TS_TaiSan.col_GisTriDaKH].ToString();
            //}
            //catch
            //{
            //    txtGiaTriDaKH.Text = "";
            //}
            //try
            //{
            //    txtGiaTriConLai.Text = row[cls_TS_TaiSan.col_GiaTriConLai].ToString();
            //}
            //catch
            //{
            //    txtGiaTriConLai.Text = "";
            //}
            //try
            //{
            //    txtGiaTriKHMotKy.Text = row[cls_TS_TaiSan.col_GiaTriKHMotKy].ToString();
            //}
            //catch
            //{
            //    txtGiaTriKHMotKy.Text = "";
            //}
            //try
            //{
            //    DataRow drNguonKP = getRowByID(slbNguonKP.DataSource, "MA='" + row[cls_TS_TaiSan.col_NguonKP].ToString() + "'");
            //    slbNguonKP.txtMa.Text = drNguonKP["MA"].ToString();
            //    slbNguonKP.txtTen.Text = drNguonKP["TEN"].ToString();
            //}
            //catch
            //{
            //}

            //try
            //{
            //    DataRow drChuong = getRowByID(slbChuong.DataSource, "MA='" + row[cls_TS_TaiSan.col_Chuong].ToString() + "'");
            //    slbChuong.txtMa.Text = drChuong["MA"].ToString();
            //    slbChuong.txtTen.Text = drChuong["TEN"].ToString();
            //}
            //catch
            //{
            //}

            //try
            //{
            //    DataRow drMuc = getRowByID(slbMuc.DataSource, "MA='" + row[cls_TS_TaiSan.col_Muc].ToString() + "'");
            //    slbMuc.txtMa.Text = drMuc["MA"].ToString();
            //    slbMuc.txtTen.Text = drMuc["TEN"].ToString();
            //}
            //catch
            //{
            //}

            //try
            //{
            //    DataRow drNghiepVu = getRowByID(slbNghiepVu.DataSource, "MA='" + row[cls_TS_TaiSan.col_NghiepVu].ToString() + "'");
            //    slbNghiepVu.txtMa.Text = drNghiepVu["MA"].ToString();
            //    slbNghiepVu.txtTen.Text = drNghiepVu["TEN"].ToString();
            //}
            //catch
            //{
            //}
            //try
            //{
            //    DataRow drCoCauVon = getRowByID(slbCoCauVon.DataSource, "MA='" + row[cls_TS_TaiSan.col_CoCauVon].ToString() + "'");
            //    slbCoCauVon.txtMa.Text = drCoCauVon["MA"].ToString();
            //    slbCoCauVon.txtTen.Text = drCoCauVon["TEN"].ToString();
            //}
            //catch
            //{
            //}

            //try
            //{
            //    DataRow drNganHangKho = getRowByID(slbTKNganHangKho.DataSource, "MA='" + row[cls_TS_TaiSan.col_TKNganHangKho].ToString() + "'");
            //    slbTKNganHangKho.txtMa.Text = drNganHangKho["MA"].ToString();
            //    slbTKNganHangKho.txtTen.Text = drNganHangKho["TEN"].ToString();
            //}
            //catch
            //{
            //}
            //try
            //{
            //    DataRow drCapPhat = getRowByID(slbCapPhat.DataSource, "MA='" + row[cls_TS_TaiSan.col_CapPhat].ToString() + "'");
            //    slbCapPhat.txtMa.Text = drCapPhat["MA"].ToString();
            //    slbCapPhat.txtTen.Text = drCapPhat["TEN"].ToString();
            //}
            //catch
            //{
            //}

            //try
            //{
            //    DataRow drKhoan = getRowByID(slbKhoan.DataSource, "MA='" + row[cls_TS_TaiSan.col_Khoan].ToString() + "'");
            //    slbKhoan.txtMa.Text = drKhoan["MA"].ToString();
            //    slbKhoan.txtTen.Text = drKhoan["TEN"].ToString();
            //}
            //catch
            //{
            //}

            //try
            //{
            //    DataRow drTieuMuc = getRowByID(slbTieuMuc.DataSource, "MA='" + row[cls_TS_TaiSan.col_TieuMuc].ToString() + "'");
            //    slbTieuMuc.txtMa.Text = drTieuMuc["MA"].ToString();
            //    slbTieuMuc.txtTen.Text = drTieuMuc["TEN"].ToString();
            //}
            //catch
            //{
            //}

            //try
            //{
            //    DataRow drHoatDongSN = getRowByID(slbHoatDongSN.DataSource, "MA='" + row[cls_TS_TaiSan.col_HoatDongSN].ToString() + "'");
            //    slbHoatDongSN.txtMa.Text = drHoatDongSN["MA"].ToString();
            //    slbHoatDongSN.txtTen.Text = drHoatDongSN["TEN"].ToString();
            //}
            //catch
            //{
            //}

            //try
            //{
            //    DataRow drMaThongKe = getRowByID(slbMaThongKe.DataSource, "MA='" + row[cls_TS_TaiSan.col_MaThongKe].ToString() + "'");
            //    slbMaThongKe.txtMa.Text = drMaThongKe["MA"].ToString();
            //    slbMaThongKe.txtTen.Text = drMaThongKe["TEN"].ToString();
            //}
            //catch
            //{
            //}

            //try
            //{

            //    DataRow drChiTietMucTieuDuAn = getRowByID(slbCTMucTieuDuAn.DataSource, "MA='" + row[cls_TS_TaiSan.col_ChiTietMucTieuDuAn].ToString() + "'");
            //    slbCTMucTieuDuAn.txtMa.Text = drChiTietMucTieuDuAn["MA"].ToString();
            //    slbCTMucTieuDuAn.txtTen.Text = drChiTietMucTieuDuAn["TEN"].ToString();
            //}
            //catch
            //{
            //}

            ////ckDaIn.Checked = row[cls_TS_TaiSan.col_DaIn].ToString() == "1";
            ////ckDaKiemKe.Checked = row[cls_TS_TaiSan.col_DaKiemKe].ToString() == "1";
            //base.Show_ChiTiet();
        }

        #endregion

        #region Phương thức private

        #region Lấy dataRow theo điều kiện

        public DataRow getRowByID(DataTable dt, string exp)
        {
            try
            {
                DataRow[] r = dt.Select(exp);
                if (r.Length > 0)
                    return r[0];
                else return null;
            }
            catch { return null; }
        }

        #endregion

        #region Check dữ liệu để nhập vào

        private bool AcceptData()
        {
            //if (string.IsNullOrEmpty(txtSoTheTaiSan.Text))
            //{
            //    TA_MessageBox.MessageBox.Show("Số thẻ tài sản không được để trống!", "Thông báo", "OK", TA_MessageBox.MessageButton.OK, TA_MessageBox.MessageIcon.Error);
            //    txtSoTheTaiSan.Focus();
            //    return false;
            //}
            //else if (string.IsNullOrEmpty(txtTenTaiSan.Text))
            //{
            //    TA_MessageBox.MessageBox.Show("Tên tài sản không được để trống!", "Thông báo", "OK", TA_MessageBox.MessageButton.OK, TA_MessageBox.MessageIcon.Error);
            //    txtTenTaiSan.Focus();
            //    return false;
            //}
            //else if (string.IsNullOrEmpty(slbLoaiTaiSan.txtTen.Text))
            //{
            //    TA_MessageBox.MessageBox.Show("Loại tài sản không được để trống!", "Thông báo", "OK", TA_MessageBox.MessageButton.OK, TA_MessageBox.MessageIcon.Error);
            //    slbLoaiTaiSan.txtTen.Focus();
            //    return false;
            //}
            //else if (string.IsNullOrEmpty(slbMaTangTS.txtTen.Text))
            //{
            //    TA_MessageBox.MessageBox.Show("Mã tăng ts không được để trống!", "Thông báo", "OK", TA_MessageBox.MessageButton.OK, TA_MessageBox.MessageIcon.Error);
            //    slbMaTangTS.txtTen.Focus();
            //    return false;
            //}
            //else if (DateTime.Parse(dtpNgayTang.Value.ToString()) == DateTime.MinValue)
            //{
            //    TA_MessageBox.MessageBox.Show("Ngày tăng không được để trống!", "Thông báo", "OK", TA_MessageBox.MessageButton.OK, TA_MessageBox.MessageIcon.Error);
            //    dtpNgayTang.Focus();
            //    return false;
            //}
            //else if (DateTime.Parse(dtpNgayTinhKH.Value.ToString()) == DateTime.MinValue)
            //{
            //    TA_MessageBox.MessageBox.Show("Ngày tính kh không được để trống!", "Thông báo", "OK", TA_MessageBox.MessageButton.OK, TA_MessageBox.MessageIcon.Error);
            //    slbMaTangTS.txtTen.Focus();
            //    return false;
            //}

            //else if (string.IsNullOrEmpty(txtSoChungTu.Text))
            //{
            //    TA_MessageBox.MessageBox.Show("Số chứng từ không được để trống!", "Thông báo", "OK", TA_MessageBox.MessageButton.OK, TA_MessageBox.MessageIcon.Error);
            //    txtSoChungTu.Focus();
            //    return false;
            //}
            //else if (string.IsNullOrEmpty(slbTyGia.txtTen.Text))
            //{
            //    TA_MessageBox.MessageBox.Show("Tỷ giá không được để trống!", "Thông báo", "OK", TA_MessageBox.MessageButton.OK, TA_MessageBox.MessageIcon.Error);
            //    slbTyGia.txtTen.Focus();
            //    return false;
            //}
            //else if (string.IsNullOrEmpty(slbMaBoPhan.txtTen.Text))
            //{
            //    TA_MessageBox.MessageBox.Show("Mã bộ phận không được để trống!", "Thông báo", "OK", TA_MessageBox.MessageButton.OK, TA_MessageBox.MessageIcon.Error);
            //    slbMaBoPhan.txtTen.Focus();
            //    return false;
            //}
            //else if (string.IsNullOrEmpty(slbTKTaiSan.txtTen.Text))
            //{
            //    TA_MessageBox.MessageBox.Show("TK tài sản không được để trống!", "Thông báo", "OK", TA_MessageBox.MessageButton.OK, TA_MessageBox.MessageIcon.Error);
            //    slbTKTaiSan.txtTen.Focus();
            //    return false;
            //}
            //else if (string.IsNullOrEmpty(slbTKKhauHao.txtTen.Text))
            //{
            //    TA_MessageBox.MessageBox.Show("TK khấu hao không được để trống!", "Thông báo", "OK", TA_MessageBox.MessageButton.OK, TA_MessageBox.MessageIcon.Error);
            //    slbTKKhauHao.txtTen.Focus();
            //    return false;
            //}
            //else if (string.IsNullOrEmpty(slbTKNguon.txtTen.Text))
            //{
            //    TA_MessageBox.MessageBox.Show("TK nguồn không được để trống!", "Thông báo", "OK", TA_MessageBox.MessageButton.OK, TA_MessageBox.MessageIcon.Error);
            //    slbTKNguon.txtTen.Focus();
            //    return false;
            //}
            return true;
        }

        #endregion

        #region Xóa dữ liệu cũ trước khi thêm mới

        private void ClearData()
        {
        //    txtSoTheTaiSan.Text = txtTenTaiSan.Text = txtSoKyKH.Text = txtMaVach.Text = txtDonViTinh.Text = txtTongSanLuong.Text = txtSoChungTu.Text = string.Empty;
        //    slbLoaiTaiSan.txtTen.Text = slbLoaiTaiSan.txtMa.Text = slbMaTangTS.txtTen.Text = slbMaTangTS.txtMa.Text =
        //    slbKieuKH.txtTen.Text = slbKieuKH.txtMa.Text = slbMaPhi.txtTen.Text = slbMaPhi.txtMa.Text = slbTyGia.txtTen.Text = slbTyGia.txtMa.Text
        //    = slbMaBoPhan.txtTen.Text = slbMaBoPhan.txtMa.Text = slbTKTaiSan.txtTen.Text = slbTKTaiSan.txtMa.Text = slbTKKhauHao.txtTen.Text = slbTKKhauHao.txtMa.Text
        //    = slbTKKhauHao.txtTen.Text = slbTKKhauHao.txtMa.Text = string.Empty;
        //    dtpNgayChungTu.Value = dtpNgayDinhChi.Value = dtpNgayKetThucKH.Value = dtpNgaySuDung.Value = dtpNgayTang.Value = dtpNgayTinhKH.Value = System.DateTime.Now;
        //    txtTenKhac.Text = txtThongSoKT.Text = txtLyDoDinhChi.Text = txtGhiChu.Text = slbNhom1.txtTen.Text = slbNhom1.txtMa.Text = slbNhom2.txtTen.Text = slbNhom2.txtMa.Text =
        //    slbNhom3.txtTen.Text = slbNhom3.txtMa.Text = slbNhom4.txtTen.Text = slbNhom4.txtMa.Text = txtSoKyKHQD.Text = txtTyLeKHVuotMuc.Text = txtHSChoVuotMuc.Text =
        //    txtSoKyKHConLai.Text = txtKyHieu.Text = txtNuocSX.Text =
        //    txtNamSX.Text = txtGiaTriLamTron.Text = txtNguyenGia.Text = txtGiaTriDaKH.Text = txtGiaTriConLai.Text = txtGiaTriKHMotKy.Text = slbNguonKP.txtTen.Text = slbNguonKP.txtMa.Text =
        //    slbChuong.txtTen.Text = slbChuong.txtMa.Text = slbMuc.txtTen.Text = slbMuc.txtMa.Text = slbNghiepVu.txtTen.Text = slbNghiepVu.txtMa.Text =
        //    slbCoCauVon.txtTen.Text = slbCoCauVon.txtMa.Text = slbTKNganHangKho.txtTen.Text = slbTKNganHangKho.txtMa.Text = slbCapPhat.txtTen.Text = slbCapPhat.txtMa.Text =
        //    slbKhoan.txtTen.Text = slbKhoan.txtMa.Text = slbTieuMuc.txtTen.Text = slbTieuMuc.txtMa.Text = slbHoatDongSN.txtTen.Text = slbHoatDongSN.txtMa.Text =
        //    slbMaThongKe.txtTen.Text = slbMaThongKe.txtMa.Text = slbCTMucTieuDuAn.txtTen.Text = slbCTMucTieuDuAn.txtMa.Text = string.Empty;
        //    ckDaIn.CheckState = ckDaKiemKe.CheckState = CheckState.Unchecked;
        //    dtpNgaySuDung.Value = dtpNgayDinhChi.Value = System.DateTime.Now;
        }

        #endregion

        private void HienThi(string maLoai, string tenLoai)
        {
            var frm = new frm_DanhMucChung(maLoai, tenLoai);
            frm.ShowDialog();
        }

        private void LoadDuLieu(string maLoai, E00_ControlNew.usc_SelectBox ctl)
        {
            //Load tài sản
            _lst.Clear(); _lst2.Clear();
            _lst.Add(cls_SYS_DanhMuc.col_Ma);
            _lst.Add(cls_SYS_DanhMuc.col_Ten);
            _lst2.Add(cls_SYS_DanhMuc.col_MaNhom, maLoai);
            ctl.DataSource = _api.Search(ref _userError, ref _systemError, cls_SYS_DanhMuc.tb_TenBang, count: -1, lst: _lst, dicEqual: _lst2);
        }

        #endregion

        #endregion

        #region Sự kiện

        private void lblLoaiTaiSan_Click(object sender, EventArgs e)
        {
            HienThi(cls_QuanLyTaiSan._loaiTaiSan, cls_QuanLyTaiSan._tenLoaiTaiSan);
            LoadDuLieu(cls_QuanLyTaiSan._loaiTaiSan, slbLoaiTaiSan);
        }

        private void dgvDanhSachTaiSan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Show_ChiTiet();
            string name = dgvDanhSachTaiSan.Columns[dgvDanhSachTaiSan.CurrentCell.ColumnIndex].Name;
            if (name == "col_Sua")
            {
                Sua();
            }
            else if (name == "col_Xoa")
            {
                _deleteOneRecord = 1;
                Xoa();
            }

        }

        private void lblMaTangTS_Click(object sender, EventArgs e)
        {
            //HienThi(cls_QuanLyTaiSan._lyDoTangGiam, cls_QuanLyTaiSan._tenLyDoTangGiam);
            //LoadDuLieu(cls_QuanLyTaiSan._lyDoTangGiam, slbMaTangTS);
        }

        private void lblKieuKH_Click(object sender, EventArgs e)
        {
            //HienThi(cls_QuanLyTaiSan._kieuKH, cls_QuanLyTaiSan._tenKieuKH);
            //LoadDuLieu(cls_QuanLyTaiSan._kieuKH, slbKieuKH);
        }

        private void lblTyGia_Click(object sender, EventArgs e)
        {
            //HienThi(cls_QuanLyTaiSan._ngoaiTe, cls_QuanLyTaiSan._tenNgoaiTe);
            //LoadDuLieu(cls_QuanLyTaiSan._ngoaiTe, slbTyGia);
        }

        private void lblMaBoPhan_Click(object sender, EventArgs e)
        {
            //HienThi(cls_QuanLyTaiSan._boPhanSuDung, cls_QuanLyTaiSan._tenBoPhanSuDung);
            //LoadDuLieu(cls_QuanLyTaiSan._boPhanSuDung, slbMaBoPhan);
        }

        private void lblTKTaiSan_Click(object sender, EventArgs e)
        {

            //HienThi(cls_QuanLyTaiSan._danhMucKhoan, cls_QuanLyTaiSan._tenDanhMucKhoan);
            //LoadDuLieu(cls_QuanLyTaiSan._danhMucKhoan, slbTKTaiSan);
        }

        private void lblTKKhauHao_Click(object sender, EventArgs e)
        {
            //HienThi(cls_QuanLyTaiSan._danhMucKhoan, cls_QuanLyTaiSan._tenDanhMucKhoan);
            //LoadDuLieu(cls_QuanLyTaiSan._danhMucKhoan, slbTKKhauHao);
        }

        private void lblTKNguon_Click(object sender, EventArgs e)
        {
            //HienThi(cls_QuanLyTaiSan._danhMucKhoan, cls_QuanLyTaiSan._tenDanhMucKhoan);
            //LoadDuLieu(cls_QuanLyTaiSan._danhMucKhoan, slbTKNguon);
        }

        private void lblMaPhi_Click(object sender, EventArgs e)
        {
            //HienThi(cls_QuanLyTaiSan._danhMucPhi, cls_QuanLyTaiSan._tenDanhMucPhi);
            //LoadDuLieu(cls_QuanLyTaiSan._danhMucPhi, slbMaPhi);
        }

        private void lblNhom1_Click(object sender, EventArgs e)
        {
            //HienThi(cls_QuanLyTaiSan._phanNhom, cls_QuanLyTaiSan._tenPhanNhom);
            //LoadDuLieu(cls_QuanLyTaiSan._phanNhom, slbNhom1);
        }

        private void lblNhom2_Click(object sender, EventArgs e)
        {
            //HienThi(cls_QuanLyTaiSan._phanNhom, cls_QuanLyTaiSan._tenPhanNhom);
            //LoadDuLieu(cls_QuanLyTaiSan._phanNhom, slbNhom2);

        }

        private void lblNhom3_Click(object sender, EventArgs e)
        {
            //HienThi(cls_QuanLyTaiSan._phanNhom, cls_QuanLyTaiSan._tenPhanNhom);
            //LoadDuLieu(cls_QuanLyTaiSan._phanNhom, slbNhom3);
        }

        private void lblNhom4_Click(object sender, EventArgs e)
        {
            //HienThi(cls_QuanLyTaiSan._phanNhom, cls_QuanLyTaiSan._tenPhanNhom);
            //LoadDuLieu(cls_QuanLyTaiSan._phanNhom, slbNhom4);

        }

        private void lblNguonKP_Click(object sender, EventArgs e)
        {
            //HienThi(cls_QuanLyTaiSan._nguonKinhPhi, cls_QuanLyTaiSan._tenNguonKinhPhi);
            //LoadDuLieu(cls_QuanLyTaiSan._nguonKinhPhi, slbNguonKP);
        }

        private void lblChuong_Click(object sender, EventArgs e)
        {
            //HienThi(cls_QuanLyTaiSan._chuong, cls_QuanLyTaiSan._tenChuong);
            //LoadDuLieu(cls_QuanLyTaiSan._chuong, slbChuong);
        }

        private void lblMuc_Click(object sender, EventArgs e)
        {
            //HienThi(cls_QuanLyTaiSan._mucTieuMuc, cls_QuanLyTaiSan._tenMucTieuMuc);
            //LoadDuLieu(cls_QuanLyTaiSan._mucTieuMuc, slbMuc);
        }

        private void lblNghiepVu_Click(object sender, EventArgs e)
        {
            //HienThi(cls_QuanLyTaiSan._nghiepVu, cls_QuanLyTaiSan._tenNghiepVu);
            //LoadDuLieu(cls_QuanLyTaiSan._nghiepVu, slbNghiepVu);
        }

        private void lblCoCauVon_Click(object sender, EventArgs e)
        {
            //HienThi(cls_QuanLyTaiSan._coCauVon, cls_QuanLyTaiSan._tenCoCauVon);
            //LoadDuLieu(cls_QuanLyTaiSan._coCauVon, slbCoCauVon);
        }

        private void lblTKNganHangKho_Click(object sender, EventArgs e)
        {
            //HienThi(cls_QuanLyTaiSan._taiKhoanNganHang, cls_QuanLyTaiSan._tenTaiKhoanNganHang);
            //LoadDuLieu(cls_QuanLyTaiSan._taiKhoanNganHang, slbTKNganHangKho);
        }

        private void lblCapPhat_Click(object sender, EventArgs e)
        {
            //HienThi(cls_QuanLyTaiSan._capPhat, cls_QuanLyTaiSan._tenCapPhat);
            //LoadDuLieu(cls_QuanLyTaiSan._capPhat, slbCapPhat);
        }

        private void lblKhoan_Click(object sender, EventArgs e)
        {
            //HienThi(cls_QuanLyTaiSan._danhMucKhoan, cls_QuanLyTaiSan._tenDanhMucKhoan);
            //LoadDuLieu(cls_QuanLyTaiSan._danhMucKhoan, slbKhoan);
        }

        private void lblTieuMuc_Click(object sender, EventArgs e)
        {
            //HienThi(cls_QuanLyTaiSan._mucTieuMuc, cls_QuanLyTaiSan._tenMucTieuMuc);
            //LoadDuLieu(cls_QuanLyTaiSan._mucTieuMuc, slbTieuMuc);
        }

        private void lblHoatDongSN_Click(object sender, EventArgs e)
        {
            //HienThi(cls_QuanLyTaiSan._hoatDongSuNghiep, cls_QuanLyTaiSan._tenHoatDongSuNghiep);
            //LoadDuLieu(cls_QuanLyTaiSan._hoatDongSuNghiep, slbHoatDongSN);
        }

        private void lblMaThongKe_Click(object sender, EventArgs e)
        {
            //HienThi(cls_QuanLyTaiSan._maThongKe, cls_QuanLyTaiSan._tenMaThongKe);
            //LoadDuLieu(cls_QuanLyTaiSan._maThongKe, slbMaThongKe);
        }

        private void lblCTMucTieuDuAn_Click(object sender, EventArgs e)
        {
            //HienThi(cls_QuanLyTaiSan._vuViecCongTrinh, cls_QuanLyTaiSan._tenVuViecCongTrinh);
            //LoadDuLieu(cls_QuanLyTaiSan._vuViecCongTrinh, slbCTMucTieuDuAn);
        }

        private void txtSoTheTaiSan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
        }

        private void txtGiaTriLamTron_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //slbNguonKP.txtTen.Focus();
            }
        }

        private void slbNguonKP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //slbCapPhat.txtTen.Focus();
            }
        }

        #endregion
    }
}
