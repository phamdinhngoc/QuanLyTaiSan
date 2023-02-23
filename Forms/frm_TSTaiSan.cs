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
    public partial class frm_TSTaiSan : frm_DanhMuc
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
        public frm_TSTaiSan()
        {
            InitializeComponent();
            _api.KetNoi();
        }
        #endregion

        #region Phương thức

        #region Phương thức protected

        protected override void LoadData()
        {
            Load_DanhMuc(frm_DanhMucChung2._kyHieuMaLoaiTaiSan, slbLoaiTaiSan);
            TimKiem();
            Show_ChiTiet();
            base.LoadData();
        }

        protected override void Them()
        {
            _isAdd = true;
            ClearData();
            txtTenTaiSan.Focus();
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
                _lst2.Add(cls_TS_TaiSan.col_TenTaiSan, txtTenTaiSan.Text);
                _lst2.Add(cls_TS_TaiSan.col_LoaiTaiSan, slbLoaiTaiSan.txtMa.Text);
                _lst2.Add(cls_TS_TaiSan.col_NgayUD, DateTime.Now.ToString());
                _lst2.Add(cls_TS_TaiSan.col_MachineID, cls_Common.Get_MachineID());
                //Thông tin các field ở tab
                _lst2.Add(cls_TS_TaiSan.col_TenKhac, txtTenKhac.Text);
                _lst2.Add(cls_TS_TaiSan.col_ThongSoKT, txtThongSoKT.Text);
                _lst2.Add(cls_TS_TaiSan.col_LyDoDinhChi, txtLyDoDinhChi.Text);
                _lst2.Add(cls_TS_TaiSan.col_GhiChu, txtGhiChu.Text);

                _lst2.Add(cls_TS_TaiSan.col_Nhom1, slbNhom1.txtMa.Text);
                _lst2.Add(cls_TS_TaiSan.col_Nhom2, slbNhom2.txtMa.Text);
                _lst2.Add(cls_TS_TaiSan.col_Nhom3, slbNhom3.txtMa.Text);
                _lst2.Add(cls_TS_TaiSan.col_Nhom4, slbNhom4.txtMa.Text);

                _lst2.Add(cls_TS_TaiSan.col_SoKyKHQuyDinh, txtSoKyKHQD.Text);
                _lst2.Add(cls_TS_TaiSan.col_TyLeKHVuotMuc, txtTyLeKHVuotMuc.Text);
                _lst2.Add(cls_TS_TaiSan.col_HSChoVuotMuc, txtHSChoVuotMuc.Text);
                _lst2.Add(cls_TS_TaiSan.col_SoKyKHConLai, txtSoKyKHConLai.Text);

                _lst2.Add(cls_TS_TaiSan.col_SoHieuTaiSan, txtSoHieuTaiSan.Text);
                _lst2.Add(cls_TS_TaiSan.col_NgaySuDung, dtpNgaySuDung.Value.ToString());
                _lst2.Add(cls_TS_TaiSan.col_NgayDinhChi, dtpNgayDinhChi.Value.ToString());
                _lst2.Add(cls_TS_TaiSan.col_NuocSanXuat, txtNuocSX.Text);
                _lst2.Add(cls_TS_TaiSan.col_NamSanXuat, txtNamSX.Text);

                _lst2.Add(cls_TS_TaiSan.col_GiaTriLamTron, txtGiaTriLamTron.Text);
                _lst2.Add(cls_TS_TaiSan.col_NguyenGia, txtNguyenGia.Text);
                _lst2.Add(cls_TS_TaiSan.col_GisTriDaKH, txtGiaTriDaKH.Text);
                _lst2.Add(cls_TS_TaiSan.col_GiaTriConLai, txtGiaTriConLai.Text);

                _lst2.Add(cls_TS_TaiSan.col_GiaTriKHMotKy, txtGiaTriKHMotKy.Text);
                _lst2.Add(cls_TS_TaiSan.col_NguonKP, slbNguonKP.txtMa.Text);
                _lst2.Add(cls_TS_TaiSan.col_Chuong, slbChuong.txtMa.Text);
                _lst2.Add(cls_TS_TaiSan.col_Muc, slbMuc.txtMa.Text);

                _lst2.Add(cls_TS_TaiSan.col_NghiepVu, slbNghiepVu.txtMa.Text);
                _lst2.Add(cls_TS_TaiSan.col_CoCauVon, slbCoCauVon.txtMa.Text);
                _lst2.Add(cls_TS_TaiSan.col_TKNganHangKho, slbTKNganHangKho.txtMa.Text);

                _lst2.Add(cls_TS_TaiSan.col_DaIn, ckDaIn.Checked ? "1" : "0");
                _lst2.Add(cls_TS_TaiSan.col_CapPhat, slbCapPhat.txtMa.Text);
                _lst2.Add(cls_TS_TaiSan.col_Khoan, slbKhoan.txtMa.Text);
                _lst2.Add(cls_TS_TaiSan.col_TieuMuc, slbTieuMuc.txtMa.Text);

                _lst2.Add(cls_TS_TaiSan.col_HoatDongSN, slbHoatDongSN.txtMa.Text);
                _lst2.Add(cls_TS_TaiSan.col_MaThongKe, slbMaThongKe.txtMa.Text);
                _lst2.Add(cls_TS_TaiSan.col_ChiTietMucTieuDuAn, slbCTMucTieuDuAn.txtMa.Text);
                _lst2.Add(cls_TS_TaiSan.col_DaKiemKe, ckDaKiemKe.Checked ? "1" : "0");
                //
                //_lst.Add(cls_TS_TaiSan.col_Id);
                _lst.Add(cls_TS_TaiSan.col_Ma);
                if (_api.Insert(ref _userError,ref _systemError,cls_TS_TaiSan.tb_TenBang,_lst2,_lst,_lst))
                {
                    LoadData();
                    base.Luu();
                }
            }
            else if (!_isAdd && AcceptData())
            {
                string ma = dgvDanhSachTaiSan.Rows[dgvDanhSachTaiSan.CurrentCell.RowIndex].Cells["col_Ma"].Value.ToString();
                string id = dgvDanhSachTaiSan.Rows[dgvDanhSachTaiSan.CurrentCell.RowIndex].Cells["col_Id"].Value.ToString();
                _lst2.Clear(); _lst.Clear(); _lst3.Clear();
                _lst2.Add(cls_TS_TaiSan.col_TenTaiSan, txtTenTaiSan.Text);
                _lst2.Add(cls_TS_TaiSan.col_LoaiTaiSan, slbLoaiTaiSan.txtMa.Text);
                _lst2.Add(cls_TS_TaiSan.col_NgayUD, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                _lst2.Add(cls_TS_TaiSan.col_MachineID, cls_Common.Get_MachineID());
                //Thông tin các field ở tab
                _lst2.Add(cls_TS_TaiSan.col_TenKhac, txtTenKhac.Text);
                _lst2.Add(cls_TS_TaiSan.col_ThongSoKT, txtThongSoKT.Text);
                _lst2.Add(cls_TS_TaiSan.col_LyDoDinhChi, txtLyDoDinhChi.Text);
                _lst2.Add(cls_TS_TaiSan.col_GhiChu, txtGhiChu.Text);

                _lst2.Add(cls_TS_TaiSan.col_Nhom1, slbNhom1.txtMa.Text);
                _lst2.Add(cls_TS_TaiSan.col_Nhom2, slbNhom2.txtMa.Text);
                _lst2.Add(cls_TS_TaiSan.col_Nhom3, slbNhom3.txtMa.Text);
                _lst2.Add(cls_TS_TaiSan.col_Nhom4, slbNhom4.txtMa.Text);

                _lst2.Add(cls_TS_TaiSan.col_SoKyKHQuyDinh, txtSoKyKHQD.Text);
                _lst2.Add(cls_TS_TaiSan.col_TyLeKHVuotMuc, txtTyLeKHVuotMuc.Text);
                _lst2.Add(cls_TS_TaiSan.col_HSChoVuotMuc, txtHSChoVuotMuc.Text);
                _lst2.Add(cls_TS_TaiSan.col_SoKyKHConLai, txtSoKyKHConLai.Text);

                _lst2.Add(cls_TS_TaiSan.col_SoHieuTaiSan, txtSoHieuTaiSan.Text);
                _lst2.Add(cls_TS_TaiSan.col_NgaySuDung, dtpNgaySuDung.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                _lst2.Add(cls_TS_TaiSan.col_NgayDinhChi, dtpNgayDinhChi.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                _lst2.Add(cls_TS_TaiSan.col_NuocSanXuat, txtNuocSX.Text);
                _lst2.Add(cls_TS_TaiSan.col_NamSanXuat, txtNamSX.Text);

                _lst2.Add(cls_TS_TaiSan.col_GiaTriLamTron, txtGiaTriLamTron.Text);
                _lst2.Add(cls_TS_TaiSan.col_NguyenGia, txtNguyenGia.Text);
                _lst2.Add(cls_TS_TaiSan.col_GisTriDaKH, txtGiaTriDaKH.Text);
                _lst2.Add(cls_TS_TaiSan.col_GiaTriConLai, txtGiaTriConLai.Text);

                _lst2.Add(cls_TS_TaiSan.col_GiaTriKHMotKy, txtGiaTriKHMotKy.Text);
                _lst2.Add(cls_TS_TaiSan.col_NguonKP, slbNguonKP.txtMa.Text);
                _lst2.Add(cls_TS_TaiSan.col_Chuong, slbChuong.txtMa.Text);
                _lst2.Add(cls_TS_TaiSan.col_Muc, slbMuc.txtMa.Text);

                _lst2.Add(cls_TS_TaiSan.col_NghiepVu, slbNghiepVu.txtMa.Text);
                _lst2.Add(cls_TS_TaiSan.col_CoCauVon, slbCoCauVon.txtMa.Text);
                //_lst2.Add(cls_TS_TaiSan.col_Chuong, slbChuong.txtMa.Text);
                _lst2.Add(cls_TS_TaiSan.col_TKNganHangKho, slbTKNganHangKho.txtMa.Text);

                _lst2.Add(cls_TS_TaiSan.col_DaIn, ckDaIn.Checked ? "1" : "0");
                _lst2.Add(cls_TS_TaiSan.col_CapPhat, slbCapPhat.txtMa.Text);
                _lst2.Add(cls_TS_TaiSan.col_Khoan, slbKhoan.txtMa.Text);
                _lst2.Add(cls_TS_TaiSan.col_TieuMuc, slbTieuMuc.txtMa.Text);

                _lst2.Add(cls_TS_TaiSan.col_HoatDongSN, slbHoatDongSN.txtMa.Text);
                _lst2.Add(cls_TS_TaiSan.col_MaThongKe, slbMaThongKe.txtMa.Text);
                _lst2.Add(cls_TS_TaiSan.col_ChiTietMucTieuDuAn, slbCTMucTieuDuAn.txtMa.Text);
                _lst2.Add(cls_TS_TaiSan.col_DaKiemKe, ckDaKiemKe.Checked ? "1" : "0");
                //

                _lst3.Add(cls_TS_TaiSan.col_Id, id);
               // _lst3.Add(cls_TS_TaiSan.col_Ma, ma);

                if (_api.UpdateAll(ref _userError, ref _systemError, cls_TS_TaiSan.tb_TenBang, _lst2, _lst3))
                {
                    LoadData();
                    base.Luu();
                }
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
                col_Id.DataPropertyName = "ID";
                col_Ma.DataPropertyName = "MA";
                col_TenTS.DataPropertyName = "TENTAISAN";
                colLoaiTaiSan.DataPropertyName = "LOAITAISAN";
                base.TimKiem();
            }
            catch
            {

                return;
            }

        }

        protected override void Show_ChiTiet()
        {
            ClearData();
            int _index = -1;
            DataTable tb = new DataTable();
            DataRow row = null;
            try
            {
                _index = dgvDanhSachTaiSan.CurrentRow.Index;
                tb = (DataTable)dgvDanhSachTaiSan.DataSource;
                row = tb.Rows[_index];
            }
            catch
            {
                
                
            }
           
            #region Tên tài sản
            try
            {
                txtTenTaiSan.Text = row[cls_TS_TaiSan.col_TenTaiSan].ToString();
            }
            catch
            {
                txtTenTaiSan.Text = "";

            }
            
            #endregion
            
            
            try
            {
                DataRow drLoaiTS = getRowByID(slbLoaiTaiSan.DataSource, "MA='" + row[cls_TS_TaiSan.col_LoaiTaiSan].ToString() + "'");
                slbLoaiTaiSan.txtMa.Text = drLoaiTS["MA"].ToString();
                slbLoaiTaiSan.txtTen.Text = drLoaiTS["TEN"].ToString();
            }
            catch
            {
            }

           
            try
            {
                txtTenKhac.Text = row[cls_TS_TaiSan.col_TenKhac].ToString();
            }
            catch
            {
                txtTenKhac.Text = "";

            }

            try
            {
                txtThongSoKT.Text = row[cls_TS_TaiSan.col_ThongSoKT].ToString();
            }
            catch
            {
                txtThongSoKT.Text = "";

            }
            try
            {
                txtLyDoDinhChi.Text = row[cls_TS_TaiSan.col_LyDoDinhChi].ToString();
            }
            catch
            {
                txtLyDoDinhChi.Text = "";
            }
            try
            {
                txtGhiChu.Text = row[cls_TS_TaiSan.col_GhiChu].ToString();
            }
            catch
            {
            }
            try
            {
                DataRow drNhom1 = getRowByID(slbNhom1.DataSource, "MA='" + row[cls_TS_TaiSan.col_Nhom1].ToString() + "'");
                slbNhom1.txtMa.Text = drNhom1["MA"].ToString();
                slbNhom1.txtTen.Text = drNhom1["TEN"].ToString();
            }
            catch
            {
            }

            try
            {
                DataRow drNhom2 = getRowByID(slbNhom2.DataSource, "MA='" + row[cls_TS_TaiSan.col_Nhom2].ToString() + "'");
                slbNhom2.txtMa.Text = drNhom2["MA"].ToString();
                slbNhom2.txtTen.Text = drNhom2["TEN"].ToString();
            }
            catch
            {
            }

            try
            {
                DataRow drNhom3 = getRowByID(slbNhom3.DataSource, "MA='" + row[cls_TS_TaiSan.col_Nhom3].ToString() + "'");
                slbNhom3.txtMa.Text = drNhom3["MA"].ToString();
                slbNhom3.txtTen.Text = drNhom3["TEN"].ToString();
            }
            catch
            {
            }

            try
            {
                DataRow drNhom4 = getRowByID(slbNhom4.DataSource, "MA='" + row[cls_TS_TaiSan.col_Nhom4].ToString() + "'");
                slbNhom4.txtMa.Text = drNhom4["MA"].ToString();
                slbNhom4.txtTen.Text = drNhom4["TEN"].ToString();
            }
            catch
            {
            }

            try
            {
                txtSoKyKHQD.Text = row[cls_TS_TaiSan.col_SoKyKHQuyDinh].ToString();
            }
            catch
            {
                txtSoKyKHQD.Text = "";
            }
            try
            {
                txtTyLeKHVuotMuc.Text = row[cls_TS_TaiSan.col_TyLeKHVuotMuc].ToString();
            }
            catch
            {
                txtTyLeKHVuotMuc.Text = "";
            }
            try
            {
                txtHSChoVuotMuc.Text = row[cls_TS_TaiSan.col_HSChoVuotMuc].ToString();
            }
            catch
            {
                txtHSChoVuotMuc.Text = "";
            }
            try
            {
                txtSoKyKHConLai.Text = row[cls_TS_TaiSan.col_SoKyKHConLai].ToString();
            }
            catch
            {
                txtSoKyKHConLai.Text = "";
            }
            try
            {
                txtSoHieuTaiSan.Text = row[cls_TS_TaiSan.col_SoHieuTaiSan].ToString();
            }
            catch
            {
                txtSoHieuTaiSan.Text = "";
            }

            try
            {
                dtpNgaySuDung.Value = DateTime.Parse(row[cls_TS_TaiSan.col_NgaySuDung].ToString());
            }
            catch
            {
            }
            try
            {
                dtpNgayDinhChi.Value = DateTime.Parse(row[cls_TS_TaiSan.col_NgayDinhChi].ToString());
            }
            catch
            {
            }
            try
            {
                txtNuocSX.Text = row[cls_TS_TaiSan.col_NuocSanXuat].ToString();
            }
            catch
            {
                txtNuocSX.Text = "";
            }
            try
            {
                txtNamSX.Text = row[cls_TS_TaiSan.col_NamSanXuat].ToString();
            }
            catch
            {
                txtNamSX.Text = "";
            }
            try
            {
                txtGiaTriLamTron.Text = row[cls_TS_TaiSan.col_GiaTriLamTron].ToString();
            }
            catch
            {
                txtGiaTriLamTron.Text = "";
            }
            try
            {
                txtNguyenGia.Text = row[cls_TS_TaiSan.col_NguyenGia].ToString();
            }
            catch
            {
                txtNguyenGia.Text = "";
            }
            try
            {
                txtGiaTriDaKH.Text = row[cls_TS_TaiSan.col_GisTriDaKH].ToString();
            }
            catch
            {
                txtGiaTriDaKH.Text = "";
            }
            try
            {
                txtGiaTriConLai.Text = row[cls_TS_TaiSan.col_GiaTriConLai].ToString();
            }
            catch
            {
                txtGiaTriConLai.Text = "";
            }
            try
            {
                txtGiaTriKHMotKy.Text = row[cls_TS_TaiSan.col_GiaTriKHMotKy].ToString();
            }
            catch
            {
                txtGiaTriKHMotKy.Text = "";
            }
            try
            {
                DataRow drNguonKP = getRowByID(slbNguonKP.DataSource, "MA='" + row[cls_TS_TaiSan.col_NguonKP].ToString() + "'");
                slbNguonKP.txtMa.Text = drNguonKP["MA"].ToString();
                slbNguonKP.txtTen.Text = drNguonKP["TEN"].ToString();
            }
            catch
            {
            }

            try
            {
                DataRow drChuong = getRowByID(slbChuong.DataSource, "MA='" + row[cls_TS_TaiSan.col_Chuong].ToString() + "'");
                slbChuong.txtMa.Text = drChuong["MA"].ToString();
                slbChuong.txtTen.Text = drChuong["TEN"].ToString();
            }
            catch
            {
            }

            try
            {
                DataRow drMuc = getRowByID(slbMuc.DataSource, "MA='" + row[cls_TS_TaiSan.col_Muc].ToString() + "'");
                slbMuc.txtMa.Text = drMuc["MA"].ToString();
                slbMuc.txtTen.Text = drMuc["TEN"].ToString();
            }
            catch
            {
            }

            try
            {
                DataRow drNghiepVu = getRowByID(slbNghiepVu.DataSource, "MA='" + row[cls_TS_TaiSan.col_NghiepVu].ToString() + "'");
                slbNghiepVu.txtMa.Text = drNghiepVu["MA"].ToString();
                slbNghiepVu.txtTen.Text = drNghiepVu["TEN"].ToString();
            }
            catch
            {
            }
            try
            {
                DataRow drCoCauVon = getRowByID(slbCoCauVon.DataSource, "MA='" + row[cls_TS_TaiSan.col_CoCauVon].ToString() + "'");
                slbCoCauVon.txtMa.Text = drCoCauVon["MA"].ToString();
                slbCoCauVon.txtTen.Text = drCoCauVon["TEN"].ToString();
            }
            catch
            {
            }

            try
            {
                DataRow drNganHangKho = getRowByID(slbTKNganHangKho.DataSource, "MA='" + row[cls_TS_TaiSan.col_TKNganHangKho].ToString() + "'");
                slbTKNganHangKho.txtMa.Text = drNganHangKho["MA"].ToString();
                slbTKNganHangKho.txtTen.Text = drNganHangKho["TEN"].ToString();
            }
            catch
            {
            }
            try
            {
                DataRow drCapPhat = getRowByID(slbCapPhat.DataSource, "MA='" + row[cls_TS_TaiSan.col_CapPhat].ToString() + "'");
                slbCapPhat.txtMa.Text = drCapPhat["MA"].ToString();
                slbCapPhat.txtTen.Text = drCapPhat["TEN"].ToString();
            }
            catch
            {
            }

            try
            {
                DataRow drKhoan = getRowByID(slbKhoan.DataSource, "MA='" + row[cls_TS_TaiSan.col_Khoan].ToString() + "'");
                slbKhoan.txtMa.Text = drKhoan["MA"].ToString();
                slbKhoan.txtTen.Text = drKhoan["TEN"].ToString();
            }
            catch
            {
            }

            try
            {
                DataRow drTieuMuc = getRowByID(slbTieuMuc.DataSource, "MA='" + row[cls_TS_TaiSan.col_TieuMuc].ToString() + "'");
                slbTieuMuc.txtMa.Text = drTieuMuc["MA"].ToString();
                slbTieuMuc.txtTen.Text = drTieuMuc["TEN"].ToString();
            }
            catch
            {
            }

            try
            {
                DataRow drHoatDongSN = getRowByID(slbHoatDongSN.DataSource, "MA='" + row[cls_TS_TaiSan.col_HoatDongSN].ToString() + "'");
                slbHoatDongSN.txtMa.Text = drHoatDongSN["MA"].ToString();
                slbHoatDongSN.txtTen.Text = drHoatDongSN["TEN"].ToString();
            }
            catch
            {
            }

            try
            {
                DataRow drMaThongKe = getRowByID(slbMaThongKe.DataSource, "MA='" + row[cls_TS_TaiSan.col_MaThongKe].ToString() + "'");
                slbMaThongKe.txtMa.Text = drMaThongKe["MA"].ToString();
                slbMaThongKe.txtTen.Text = drMaThongKe["TEN"].ToString();
            }
            catch
            {
            }

            try
            {

                DataRow drChiTietMucTieuDuAn = getRowByID(slbCTMucTieuDuAn.DataSource, "MA='" + row[cls_TS_TaiSan.col_ChiTietMucTieuDuAn].ToString() + "'");
                slbCTMucTieuDuAn.txtMa.Text = drChiTietMucTieuDuAn["MA"].ToString();
                slbCTMucTieuDuAn.txtTen.Text = drChiTietMucTieuDuAn["TEN"].ToString();
            }
            catch
            {
            }

            //ckDaIn.Checked = row[cls_TS_TaiSan.col_DaIn].ToString() == "1";
            //ckDaKiemKe.Checked = row[cls_TS_TaiSan.col_DaKiemKe].ToString() == "1";
            base.Show_ChiTiet();
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
             if (string.IsNullOrEmpty(txtTenTaiSan.Text))
            {
                TA_MessageBox.MessageBox.Show("Tên tài sản không được để trống!", "Thông báo", "OK", TA_MessageBox.MessageButton.OK, TA_MessageBox.MessageIcon.Error);
                txtTenTaiSan.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(slbLoaiTaiSan.txtMa.Text))
            {
                TA_MessageBox.MessageBox.Show("Loại tài sản không được để trống!", "Thông báo", "OK", TA_MessageBox.MessageButton.OK, TA_MessageBox.MessageIcon.Error);
                slbLoaiTaiSan.txtTen.Focus();
                return false;
            }
           
            return true;
        }

        #endregion

        #region Xóa dữ liệu cũ trước khi thêm mới

        private void ClearData()
        {
            txtTenTaiSan.Text = "";
            slbLoaiTaiSan.txtTen.Text = slbLoaiTaiSan.txtMa.Text = "";
            txtTenKhac.Text = txtThongSoKT.Text = txtLyDoDinhChi.Text = txtGhiChu.Text = slbNhom1.txtTen.Text = slbNhom1.txtMa.Text = slbNhom2.txtTen.Text = slbNhom2.txtMa.Text =
            slbNhom3.txtTen.Text = slbNhom3.txtMa.Text = slbNhom4.txtTen.Text = slbNhom4.txtMa.Text = txtSoKyKHQD.Text = txtTyLeKHVuotMuc.Text = txtHSChoVuotMuc.Text =
            txtSoKyKHConLai.Text = txtSoHieuTaiSan.Text = txtNuocSX.Text =
            txtNamSX.Text = txtGiaTriLamTron.Text = txtNguyenGia.Text = txtGiaTriDaKH.Text = txtGiaTriConLai.Text = txtGiaTriKHMotKy.Text = slbNguonKP.txtTen.Text = slbNguonKP.txtMa.Text =
            slbChuong.txtTen.Text = slbChuong.txtMa.Text = slbMuc.txtTen.Text = slbMuc.txtMa.Text = slbNghiepVu.txtTen.Text = slbNghiepVu.txtMa.Text =
            slbCoCauVon.txtTen.Text = slbCoCauVon.txtMa.Text = slbTKNganHangKho.txtTen.Text = slbTKNganHangKho.txtMa.Text = slbCapPhat.txtTen.Text = slbCapPhat.txtMa.Text =
            slbKhoan.txtTen.Text = slbKhoan.txtMa.Text = slbTieuMuc.txtTen.Text = slbTieuMuc.txtMa.Text = slbHoatDongSN.txtTen.Text = slbHoatDongSN.txtMa.Text =
            slbMaThongKe.txtTen.Text = slbMaThongKe.txtMa.Text = slbCTMucTieuDuAn.txtTen.Text = slbCTMucTieuDuAn.txtMa.Text = string.Empty;
            ckDaIn.CheckState = ckDaKiemKe.CheckState = CheckState.Unchecked;
            dtpNgaySuDung.Value = dtpNgayDinhChi.Value = System.DateTime.Now;
        }

        #endregion

        private void HienThi(string maLoai, string tenLoai)
        {
            var frm = new frm_DanhMucChung2(maLoai, tenLoai);
            frm.ShowDialog();
        }

        private void Load_DanhMuc(string maLoai, E00_ControlNew.usc_SelectBox ctl)
        {
            //Load tài sản
            _lst.Clear(); _lst2.Clear();
            _lst.Add(cls_SYS_DanhMuc.col_Ma);
            _lst.Add(cls_SYS_DanhMuc.col_Ten);
            _lst2.Add(cls_SYS_DanhMuc.col_MaNhom, maLoai);
            DataTable dt = _api.Search(ref _userError, ref _systemError, cls_SYS_DanhMuc.tb_TenBang, count: -1, lst: _lst, dicEqual: _lst2);
            ctl.DataSource = dt;

            colLoaiTaiSan.DataSource = dt.Copy();
            colLoaiTaiSan.DisplayMember = cls_SYS_DanhMuc.col_Ten;
            colLoaiTaiSan.ValueMember = cls_SYS_DanhMuc.col_Ma;
        }

        #endregion

        #endregion

        #region Sự kiện

        private void lblLoaiTaiSan_Click(object sender, EventArgs e)
        {
            HienThi(frm_DanhMucChung2._kyHieuMaLoaiTaiSan, frm_DanhMucChung2._kyHieuTenLoaiTaiSan);
            Load_DanhMuc(cls_QuanLyTaiSan._loaiTaiSan, slbLoaiTaiSan);
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

        private void lblNhom1_Click(object sender, EventArgs e)
        {
            HienThi(cls_QuanLyTaiSan._phanNhom, cls_QuanLyTaiSan._tenPhanNhom);
            Load_DanhMuc(cls_QuanLyTaiSan._phanNhom, slbNhom1);
        }

        private void lblNhom2_Click(object sender, EventArgs e)
        {
            HienThi(cls_QuanLyTaiSan._phanNhom, cls_QuanLyTaiSan._tenPhanNhom);
            Load_DanhMuc(cls_QuanLyTaiSan._phanNhom, slbNhom2);

        }

        private void lblNhom3_Click(object sender, EventArgs e)
        {
            HienThi(cls_QuanLyTaiSan._phanNhom, cls_QuanLyTaiSan._tenPhanNhom);
            Load_DanhMuc(cls_QuanLyTaiSan._phanNhom, slbNhom3);
        }

        private void lblNhom4_Click(object sender, EventArgs e)
        {
            HienThi(cls_QuanLyTaiSan._phanNhom, cls_QuanLyTaiSan._tenPhanNhom);
            Load_DanhMuc(cls_QuanLyTaiSan._phanNhom, slbNhom4);

        }

        private void lblNguonKP_Click(object sender, EventArgs e)
        {
            HienThi(cls_QuanLyTaiSan._nguonKinhPhi, cls_QuanLyTaiSan._tenNguonKinhPhi);
            Load_DanhMuc(cls_QuanLyTaiSan._nguonKinhPhi, slbNguonKP);
        }

        private void lblChuong_Click(object sender, EventArgs e)
        {
            HienThi(cls_QuanLyTaiSan._chuong, cls_QuanLyTaiSan._tenChuong);
            Load_DanhMuc(cls_QuanLyTaiSan._chuong, slbChuong);
        }

        private void lblMuc_Click(object sender, EventArgs e)
        {
            HienThi(cls_QuanLyTaiSan._mucTieuMuc, cls_QuanLyTaiSan._tenMucTieuMuc);
            Load_DanhMuc(cls_QuanLyTaiSan._mucTieuMuc, slbMuc);
        }

        private void lblNghiepVu_Click(object sender, EventArgs e)
        {
            HienThi(cls_QuanLyTaiSan._nghiepVu, cls_QuanLyTaiSan._tenNghiepVu);
            Load_DanhMuc(cls_QuanLyTaiSan._nghiepVu, slbNghiepVu);
        }

        private void lblCoCauVon_Click(object sender, EventArgs e)
        {
            HienThi(cls_QuanLyTaiSan._coCauVon, cls_QuanLyTaiSan._tenCoCauVon);
            Load_DanhMuc(cls_QuanLyTaiSan._coCauVon, slbCoCauVon);
        }

        private void lblTKNganHangKho_Click(object sender, EventArgs e)
        {
            HienThi(cls_QuanLyTaiSan._taiKhoanNganHang, cls_QuanLyTaiSan._tenTaiKhoanNganHang);
            Load_DanhMuc(cls_QuanLyTaiSan._taiKhoanNganHang, slbTKNganHangKho);
        }

        private void lblCapPhat_Click(object sender, EventArgs e)
        {
            HienThi(cls_QuanLyTaiSan._capPhat, cls_QuanLyTaiSan._tenCapPhat);
            Load_DanhMuc(cls_QuanLyTaiSan._capPhat, slbCapPhat);
        }

        private void lblKhoan_Click(object sender, EventArgs e)
        {
            HienThi(cls_QuanLyTaiSan._danhMucKhoan, cls_QuanLyTaiSan._tenDanhMucKhoan);
            Load_DanhMuc(cls_QuanLyTaiSan._danhMucKhoan, slbKhoan);
        }

        private void lblTieuMuc_Click(object sender, EventArgs e)
        {
            HienThi(cls_QuanLyTaiSan._mucTieuMuc, cls_QuanLyTaiSan._tenMucTieuMuc);
            Load_DanhMuc(cls_QuanLyTaiSan._mucTieuMuc, slbTieuMuc);
        }

        private void lblHoatDongSN_Click(object sender, EventArgs e)
        {
            HienThi(cls_QuanLyTaiSan._hoatDongSuNghiep, cls_QuanLyTaiSan._tenHoatDongSuNghiep);
            Load_DanhMuc(cls_QuanLyTaiSan._hoatDongSuNghiep, slbHoatDongSN);
        }

        private void lblMaThongKe_Click(object sender, EventArgs e)
        {
            HienThi(cls_QuanLyTaiSan._maThongKe, cls_QuanLyTaiSan._tenMaThongKe);
            Load_DanhMuc(cls_QuanLyTaiSan._maThongKe, slbMaThongKe);
        }

        private void lblCTMucTieuDuAn_Click(object sender, EventArgs e)
        {
            HienThi(cls_QuanLyTaiSan._vuViecCongTrinh, cls_QuanLyTaiSan._tenVuViecCongTrinh);
            Load_DanhMuc(cls_QuanLyTaiSan._vuViecCongTrinh, slbCTMucTieuDuAn);
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
                slbNguonKP.txtTen.Focus();
            }
        }

        private void slbNguonKP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                slbCapPhat.txtTen.Focus();
            }
        }

        #endregion

        private void dgvDanhSachTaiSan_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
    }
}
