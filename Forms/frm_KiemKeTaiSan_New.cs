using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using E00_Base;
using E00_API;
using E00_Model;
using E00_Common;
using DevComponents.DotNetBar.Controls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace QuanLyTaiSan
{
    public partial class frm_KiemKeTaiSan_New : frm_DanhMuc
    {
        #region Biến toàn cục

        private Api_Common _api = new Api_Common();
        private Acc_Oracle _apiOracle = new Acc_Oracle();
        private api_TaiSan _apiTaiSan = new api_TaiSan();
        private DataTable _dtTaiSan = new DataTable();
        private DataTable _dtKiemKe = new DataTable();
        private DataTable _dtNhanVien = new DataTable();
        private string _userError = "";
        private string _systemError = "";
        private BindingSource _bsTaiSan = new BindingSource();
        private BindingSource _bsKiemKe = new BindingSource();
        private bool _suaTaiSan = false;
        private string _maSuDungLL = "";
        private SortedList<string, decimal> _lstSoLuongBanGiao = new SortedList<string, decimal>();
        private SortedList<string, decimal> _lstDonGia = new SortedList<string, decimal>();
        DataRow _rowKiemKe;

        private string _maKiemKeLL = "";

        #endregion

        #region frm_KiemKeTaiSan_New (Khởi tạo)

        public frm_KiemKeTaiSan_New()
        {
            InitializeComponent();

            _api.KetNoi();

            pnlRight.Anchor = AnchorStyles.Left ^ AnchorStyles.Right ^ AnchorStyles.Top;
        }

        #endregion

        #region Phương thức

        #region Phương thức kế thừa

        #region LoadData (Load dữ liệu khi mở form)
        /// <summary>
        /// Load dữ liệu khi mở form
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/07/18</Date>
        protected override void LoadData()
        {
            slbKhu.DataSource = Load_DanhMuc(cls_sys_LoaiDanhMuc.khu_MaLoai, "-1");
            slbLoaiTaiSan.DataSource = Load_DanhMuc(cls_sys_LoaiDanhMuc.loaiTaiSan_MaLoai, "-1");

            Load_KhoaPhongQuanLy();
            Load_KhoaPhongSuDung();
            Load_TaiSan();
            slbDotKiemKe.DataSource = Load_DanhMuc("DotKiemKe", "-1");
            Load_TrangThaiTaiSan();
            Load_NhanVienKiemKe();
            base.LoadData();
        }

        #endregion

        #region Load_CauHinh (Load cấu hình khi mở form)
        /// <summary>
        /// Load cấu hình khi mở form
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/07/15</Date>
        protected override void Load_CauHinh()
        {
            base.Load_CauHinh();
            pnlTimKiem.Enabled = false;
            dgvMain.Enabled = false;
            dgvKiemKe.Enabled = false;
        }

        #endregion

        #region Them (Thêm kiểm kê)
        /// <summary>
        /// Thêm kiểm kê
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/07/10</Date>
        protected override void Them()
        {
            base.Them();
            pnlTimKiem.Enabled = true;
            pnlMain.Enabled = true;
            dgvMain.Enabled = true;
            dgvKiemKe.Enabled = true;
            btnThem.Enabled = true;
            slbSoPhieu.txtTen.Text = _apiOracle.Get_MaMoi();
            slbSoPhieu.his_AddNew = true;
        }

        #endregion

        protected override void Sua()
        {
            base.Sua();
            btnSua.Enabled = true;
            btnThem.Enabled = true;
            slbSoPhieu.his_AddNew = false;
        }

        #region Luu (Cập nhật thông tin tài sản)
        /// <summary>
        /// Cập nhật thông tin tài sản
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/07/10</Date>
        protected override void Luu()
        {
            if (_apiTaiSan.Update_TaiSan(txtMaTaiSan.Text, txtTaiSan.Text, txtKyHieu.Text, slbLoaiTaiSan.txtMa.Text, slbLoaiTaiSan.txtTen.Text, txtQuyCach.Text) > 0
                && _apiTaiSan.Update_TaiSan(txtSerinumber.Text, txtMaVach.Text) > 0)
            {
                MessageBox.Show("Cập nhật tài sản thành công");
                // ShowTaiSan();
            }
        }

        #endregion

        #region BoQua (Bỏ qua mã vạch đang chọn kiểm kê)
        /// <summary>
        /// Bỏ qua mã vạch đang chọn kiểm kê
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/07/10</Date>
        protected override void BoQua()
        {
            txtMaVach.Text = "";
            txtMaVach.Focus();
        }

        #endregion

        #region In (In mã vạch của những tài sản đang chọn)
        /// <summary>
        /// In (In mã vạch của những tài sản đang chọn)
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/07/10</Date>
        protected override void In()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dtInTaiSan = new DataTable();
                DataTable dtInKiemKe = new DataTable();
                DataTable dtIn = new DataTable();
                _bsKiemKe.EndEdit();
                _bsTaiSan.EndEdit();
                try
                {
                    dtIn = _dtTaiSan.Select("Chon = 'True'").CopyToDataTable();
                }
                catch
                {
                }
                try
                {
                    if (dtIn.Rows.Count > 0)
                    {
                        dtInKiemKe = _dtKiemKe.Select("Chon = 'True'").CopyToDataTable();
                        if (dtInKiemKe.Rows.Count > 0)
                        {
                            DataRow row;
                            foreach (DataRow item in dtInKiemKe.Rows)
                            {
                                row = dtIn.NewRow();
                                foreach (var col in dtIn.Columns)
                                {
                                    try
                                    {
                                        row[col.ToString()] = item[col.ToString()];
                                    }
                                    catch
                                    {
                                    }
                                }
                                dtIn.Rows.Add(row);
                            }
                        }
                    }
                    else
                    {
                        dtIn = _dtKiemKe.Select("Chon = 'True'").CopyToDataTable();
                    }
                }
                catch
                {
                }

                ReportDocument rptDoc;

                if (dtIn.Rows.Count > 0)
                {
                    ds.Tables.Add(dtIn);
                    rptDoc = new ReportDocument();

                    if (!System.IO.Directory.Exists("..//xml"))
                    {
                        System.IO.Directory.CreateDirectory("..//xml");
                    }
                    ds.WriteXml("..//xml//ts_mavach.xml", XmlWriteMode.WriteSchema);
                    if (!System.IO.File.Exists("..\\..\\..\\Report\\ts_mavach.rpt"))
                    {
                        MessageBox.Show("Thiếu file: ts_mavach.rpt trong thư mục Report.");
                        return;
                    }
                    rptDoc.Load("..\\..\\..\\Report\\ts_mavach.rpt", OpenReportMethod.OpenReportByDefault);
                    rptDoc.SetDataSource(ds);

                    if (chkXemTruoc.Checked)
                    {
                        frm_Report frm = new frm_Report(rptDoc);
                        frm.ShowDialog();
                    }
                    else
                    {
                        rptDoc.PrintToPrinter(1, false, 1, dtInTaiSan.Rows.Count);
                        rptDoc.Dispose();
                        ds.Dispose();
                        rptDoc.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng check chọn tài sản cần in mã vạch", "Thông báo");
                }
            }
            catch
            {
                MessageBox.Show("In thất bại. Vui lòng kiểm tra lại kết nối máy in", "Thông báo");
            }
        }

        #endregion

        #endregion

        #region Phương thức dùng riêng

        #region Load_DanhMuc (Load dữ liệu trong bảng danh mục chung theo loại)
        /// <summary>
        /// Load dữ liệu trong bảng danh mục chung theo loại
        /// </summary>
        /// <param name="maLoai">Mã loại</param>
        /// <param name="maNhomLoai">Mã nhóm loại</param>
        /// <returns></returns>
        private DataTable Load_DanhMuc(string maLoai, string maNhomLoai)
        {
            try
            {
                System.Data.DataTable dtDanhMuc = new System.Data.DataTable();
                List<string> lstCot = new List<string>();
                lstCot.Add(cls_SYS_DanhMuc.col_Ma);
                lstCot.Add(cls_SYS_DanhMuc.col_Ten);
                Dictionary<string, string> dicE = new Dictionary<string, string>();
                dicE.Add(cls_SYS_DanhMuc.col_Loai, maLoai);
                if (maNhomLoai != "-1")
                {
                    dicE.Add(cls_SYS_DanhMuc.col_MaNhom, maNhomLoai);
                }
                dtDanhMuc = _api.Search(ref _userError, ref _systemError, cls_SYS_DanhMuc.tb_TenBang, dicEqual: dicE, lst: lstCot
                                        , orderByASC1: true, orderByName1: cls_SYS_DanhMuc.col_STT, orderByASC2: true, orderByName2: cls_SYS_DanhMuc.col_Ten);
                return dtDanhMuc;
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region Load_KhoaPhongQuanLy (Load đơn vị quản lý)
        /// <summary>
        /// Load đơn vị quản lý
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <date>2018/07/20</date>
        private void Load_KhoaPhongQuanLy()
        {
            List<string> lstCot = new List<string>();
            lstCot.Add(cls_LT_DanhMucBoPhan.col_MA);
            lstCot.Add(cls_LT_DanhMucBoPhan.col_TEN);

            slbDonViQuanLy.DataSource = _api.Search(ref _userError, ref _systemError, cls_LT_DanhMucBoPhan.tb_TenBang, lst: lstCot);
        }

        #endregion

        #region Load_KhoaPhongSuDung (Load đơn vị sử dụng)
        /// <summary>
        /// Load đơn vị sử dụng
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/07/20</Date>
        private void Load_KhoaPhongSuDung()
        {
            List<string> lstCot = new List<string>();
            lstCot.Add(cls_LT_DanhMucBoPhan.col_MA);
            lstCot.Add(cls_LT_DanhMucBoPhan.col_TEN);

            slbDonViSuDung.DataSource = _api.Search(ref _userError, ref _systemError, cls_LT_DanhMucBoPhan.tb_TenBang, lst: lstCot);
        }

        #endregion

        #region Load_NguoiSuDung (Load người sử dụng theo bộ phận)
        /// <summary>
        /// Load nhân viên theo bộ phận
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/07/20</Date>
        /// <param name="maBoPhan">Mã bộ phận</param>
        private void Load_NguoiSuDung(string maBoPhan)
        {
            try
            {
                List<string> lstCot = new List<string>();
                lstCot.Add(cls_NhanSu_LiLichNhanVien.col_MaNhanVien);
                lstCot.Add(cls_NhanSu_LiLichNhanVien.col_HoTen);

                Dictionary<string, string> dicE = new Dictionary<string, string>();
                dicE.Add(cls_NhanSu_LiLichNhanVien.col_MaBoPhan, maBoPhan);

                slbNguoiSuDung.DataSource = _api.Search(ref _userError, ref _systemError, cls_NhanSu_LiLichNhanVien.tb_TenBang, lst: lstCot, dicEqual: dicE);
            }
            catch
            {
                slbNguoiSuDung.DataSource = null;
            }
        }

        #endregion

        #region Load_NhanVienKiemKe (Load danh sách nhân viên chọn kiểm kê)
        /// <summary>
        /// Load danh sách nhân viên chọn kiểm kê
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/07/20</Date>
        private void Load_NhanVienKiemKe()
        {
            try
            {
                List<string> lstCot = new List<string>();
                lstCot.Add(cls_NhanSu_LiLichNhanVien.col_MaNhanVien);
                lstCot.Add(cls_NhanSu_LiLichNhanVien.col_HoTen);

                slbNhanVien.DataSource = _api.Search(ref _userError, ref _systemError, cls_NhanSu_LiLichNhanVien.tb_TenBang, lst: lstCot);
            }
            catch
            {
                slbNhanVien.DataSource = null;
            }
        }

        #endregion

        #region Load_TaiSan (Load danh mục tài sản)
        /// <summary>
        /// Load danh mục tài sản
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/07/20</Date>
        private void Load_TaiSan()
        {
            List<string> lstCot = new List<string>();
            lstCot.Add(cls_TaiSan_DanhMuc.col_Ma);
            lstCot.Add(cls_TaiSan_DanhMuc.col_Ten);

            slbTaiSan.DataSource = _api.Search(ref _userError, ref _systemError, cls_TaiSan_DanhMuc.tb_TenBang, lst: lstCot);
        }

        #endregion

        #region ShowChiTiet (Hiển thị thông tin chi tiết tài sản đang chọn)
        /// <summary>
        /// Hiển thị thông tin chi tiết tài sản đang chọn
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/07/20</Date>
        /// <param name="row">tài sản đang chọn</param>
        private void ShowChiTiet(DataRow row)
        {
            try
            {
                txtID.Text = row[cls_TaiSan_SuDungLL.col_ID].ToString();
                txtMaTaiSan.Text = row[cls_TaiSan_SuDungLL.col_MaTaiSan].ToString();
                txtSerinumber.Text = row[cls_TaiSan_SuDungLL.col_Serinumber].ToString();
                txtTaiSan.Text = row[cls_TaiSan_SuDungLL.col_TenTaiSan].ToString();
                txtKyHieu.Text = row[cls_TaiSan_SuDungLL.col_KyHieu].ToString();
                txtQuyCach.Text = row[cls_TaiSan_DanhMuc.col_QuyCach].ToString();

                txtKhu.Text = row[cls_TaiSan_SuDungLL.col_TenKhu].ToString();
                txtTang.Text = row[cls_TaiSan_SuDungLL.col_TenTang].ToString();
                txtPhongCongNang.Text = row[cls_TaiSan_SuDungLL.col_TenPhongCongNang].ToString();

                txtDonViQuanLy.Text = row[cls_TaiSan_SuDungLL.col_TenKPQuanLy].ToString();
                txtDonViSuDung.Text = row[cls_TaiSan_SuDungLL.col_TenKPSuDung].ToString();
                txtNguoiSuDung.Text = row[cls_TaiSan_SuDungLL.col_TenNguoiSuDung].ToString();

                slbLoaiTaiSan.txtMa.Text = row[cls_TaiSan_DanhMuc.col_MaLoai].ToString();
                slbLoaiTaiSan.txtTen.Text = row[cls_TaiSan_DanhMuc.col_TenLoai].ToString();

                txtTrangThai.Text = row[cls_TaiSan_SuDungLL.col_TenTrangThai].ToString();
                slbTrangThaiTaiSan.txtMa.Text = row[cls_TaiSan_SuDungLL.col_MaTrangThai].ToString();
                slbTrangThaiTaiSan.txtTen.Text = row[cls_TaiSan_SuDungLL.col_TenTrangThai].ToString();

                _maSuDungLL = row[cls_TaiSan_SuDungLL.col_Ma].ToString();

                Load_TrangThaiTaiSan();
            }
            catch
            {
                txtSerinumber.Text = "";
                txtMaTaiSan.Text = "";
                txtTaiSan.Text = "";
                txtKyHieu.Text = "";
                txtQuyCach.Text = "";

                txtKhu.Text = "";
                txtTang.Text = "";
                txtPhongCongNang.Text = "";

                txtDonViQuanLy.Text = "";
                txtDonViSuDung.Text = "";
                txtNguoiSuDung.Text = "";

                slbLoaiTaiSan.txtMa.Text = "";
                slbLoaiTaiSan.txtTen.Text = "";

                slbTrangThaiTaiSan.txtMa.Text = "";
                slbTrangThaiTaiSan.txtTen.Text = "";

                txtTrangThai.Text = "";

            }
            lblTongTaiSan.Text = dgvMain.RowCount.ToString();
            lblTongKiemKe.Text = dgvKiemKe.RowCount.ToString();
            txtKiemKe.Text = dgvKiemKe.RowCount.ToString() + "/" + (dgvKiemKe.RowCount + dgvMain.RowCount).ToString();
        }

        #endregion

        #region ShowTaiSanChuaKiemKe (Hiện thị tài sản sử dụng chưa kiểm kê)
        /// <summary>
        /// Hiện thị tài sản sử dụng chưa kiểm kê
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/07/20</Date>
        private void ShowTaiSanChuaKiemKe()
        {
            try
            {
                _dtTaiSan = _apiTaiSan.Get_TaiSanChuaKiemKe(slbDotKiemKe.txtMa.Text, slbDonViQuanLy.txtMa.Text, slbTaiSan.txtMa.Text, txtKyHieu2.Text
                                        , slbDonViSuDung.txtMa.Text, slbNguoiSuDung.txtMa.Text, slbKhu.txtMa.Text, slbTang.txtMa.Text, slbPhongCongNang.txtMa.Text);
                _bsTaiSan.DataSource = _dtTaiSan;
                dgvMain.DataSource = _bsTaiSan;
                lblTongTaiSan.Text = dgvMain.RowCount.ToString();
            }
            catch
            {
                lblTongTaiSan.Text = "0";
            }
        }

        #endregion

        private void ShowTaiSanDaKiemKe()
        {
            try
            {
                _dtKiemKe = _apiTaiSan.Get_TaiSanKiemKe(slbDotKiemKe.txtMa.Text, slbSoPhieu.txtTen.Text, slbDonViQuanLy.txtMa.Text, slbTaiSan.txtMa.Text, txtKyHieu2.Text
                     , slbDonViSuDung.txtMa.Text, slbNguoiSuDung.txtMa.Text, slbKhu.txtMa.Text, slbTang.txtMa.Text, slbPhongCongNang.txtMa.Text);
                _dtKiemKe.Columns.Add("Chon");

                _bsKiemKe.DataSource = _dtKiemKe;
                dgvKiemKe.DataSource = _bsKiemKe;
                lblTongKiemKe.Text = dgvKiemKe.RowCount.ToString();
            }
            catch
            {
                lblTongKiemKe.Text = "0";
            }
            txtKiemKe.Text = dgvKiemKe.RowCount.ToString() + "/" + (dgvKiemKe.RowCount + dgvMain.RowCount).ToString();
        }

        private void ShowTaiSan()
        {
            ShowTaiSanChuaKiemKe();
            ShowTaiSanDaKiemKe();
        }

        #region Load_TrangThaiTaiSan (Load trạng thái tài sản)
        /// <summary>
        /// Load trạng thái tài sản
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <date>2018/07/20</date>
        private void Load_TrangThaiTaiSan()
        {
            try
            {
                DataTable dtTrangThai = Load_DanhMuc(cls_sys_LoaiDanhMuc.trangThaiTaiSan_MaLoai, "-1");
                slbTrangThaiTaiSan.DataSource = dtTrangThai;
                if (dtTrangThai.Rows.Count > 0)
                {
                    slbTrangThaiTaiSan.txtMa.Text = dtTrangThai.Rows[0][cls_SYS_DanhMuc.col_Ma].ToString();
                    slbTrangThaiTaiSan.txtTen.Text = dtTrangThai.Rows[0][cls_SYS_DanhMuc.col_Ten].ToString();
                }
            }
            catch
            {
            }
        }

        #endregion

        private void Load_SoPhieu()
        {
            try
            {
                slbSoPhieu.DataSource = _apiTaiSan.Get_SoPhieuKiemKe(slbDotKiemKe.txtMa.Text);
            }
            catch 
            {
                slbSoPhieu.DataSource = null;
            }
        }

        private void Show_NhanVienKiemKe()
        {
            try
            {
                colMaNhanVien.DataPropertyName = cls_SYS_DanhMuc.col_MaNhom;
                colTenNhanVien.DataPropertyName = cls_SYS_DanhMuc.col_TenNhom;
                colChucDanh.DataPropertyName = cls_SYS_DanhMuc.col_GhiChu;
                _dtNhanVien = _apiTaiSan.Get_NhanVien(cls_sys_LoaiDanhMuc.kiemKeTaiSan_MaLoai, cls_sys_LoaiDanhMuc.nhanVienKiemKe, slbSoPhieu.txtTen.Text);
                dgvNhanVienKiemKe.DataSource = _dtNhanVien;
            }
            catch
            {
                dgvNhanVienKiemKe.DataSource = "";
            }
        }

        private void InBienBanKiemKe()
        {
            try
            {
                _lstSoLuongBanGiao.Clear();
                _lstDonGia.Clear();
                decimal tongTien = 0;
                DataSet ds = new DataSet();

                #region Xuất XML thông tin kiểm kê LL

                DataTable dtKiemKeLL = new DataTable();

                dtKiemKeLL.Columns.Add("MaDotKiemKe");
                dtKiemKeLL.Columns.Add("TenDotKiemKe");
                dtKiemKeLL.Columns.Add("SoPhieu");
                dtKiemKeLL.Columns.Add("DiaDiemKiemKe");
                dtKiemKeLL.Columns.Add("TongTienBanGiao");

                dtKiemKeLL.PrimaryKey = new DataColumn[] { dtKiemKeLL.Columns["SoPhieu"] };
                DataRow drKiemKeLL = dtKiemKeLL.NewRow();
                drKiemKeLL["MaDotKiemKe"] = slbDotKiemKe.txtMa.Text;
                drKiemKeLL["TenDotKiemKe"] = slbDotKiemKe.txtTen.Text;
                drKiemKeLL["SoPhieu"] = slbSoPhieu.txtTen.Text;
                drKiemKeLL["DiaDiemKiemKe"] = txtDiaDiem.Text;

                #endregion

                #region Xuất XML thông tin nhân viên kiểm kê

                DataTable dtNhanVienKiemKe = new DataTable();
                dtNhanVienKiemKe.Columns.Add("ID");
                dtNhanVienKiemKe.Columns.Add("MaNhanVien");
                dtNhanVienKiemKe.Columns.Add("TenNhanVien");
                dtNhanVienKiemKe.Columns.Add("ChucDanh");
                dtNhanVienKiemKe.PrimaryKey = new DataColumn[] { dtNhanVienKiemKe.Columns[cls_SYS_DanhMuc.col_ID] };
                foreach (DataRow item in _dtNhanVien.Rows)
                {
                    DataRow drNhanVienKiemKe = dtNhanVienKiemKe.NewRow();
                    drNhanVienKiemKe["ID"] = item[cls_SYS_DanhMuc.col_ID];
                    drNhanVienKiemKe["MaNhanVien"] = item[cls_SYS_DanhMuc.col_MaNhom];
                    drNhanVienKiemKe["TenNhanVien"] = item[cls_SYS_DanhMuc.col_TenNhom];
                    drNhanVienKiemKe["ChucDanh"] = item[cls_SYS_DanhMuc.col_GhiChu];
                    dtNhanVienKiemKe.Rows.Add(drNhanVienKiemKe);
                }

                #endregion

                #region Xuất XML thông tin kiểm kê CT

                DataTable dtKiemKeCT = ((DataTable)_bsKiemKe.DataSource).Select().CopyToDataTable();
                dtKiemKeCT.Columns.Add(cls_TaiSan_NhapCT.col_DonGia);
                dtKiemKeCT.Columns.Add("SoLuongBanGiao");
                DataTable dtBanGiao = _apiTaiSan.Get_ThongTinTaiSan(slbDotKiemKe.txtMa.Text, -1, slbKhu.txtMa.Text, slbTang.txtMa.Text, slbPhongCongNang.txtMa.Text
                                , slbDonViQuanLy.txtMa.Text, slbDonViSuDung.txtMa.Text, slbNguoiSuDung.txtMa.Text, slbTaiSan.txtMa.Text, txtKyHieu2.Text);
                foreach (DataRow item in dtKiemKeCT.Rows)
                {
                    try
                    {
                        if (!_lstSoLuongBanGiao.Keys.Contains(item[cls_TaiSan_SuDungLL.col_MaTaiSan].ToString()))
                        {

                            item["SoLuongBanGiao"] = dtBanGiao.Select(string.Format("{0} = '{1}' AND ({2} = '{3}' OR '{3}' = '') AND ({4} = '{5}' OR '{5}' = '') AND ({6} = '{7}' OR '{7}' = '')"
                                                        , cls_TaiSan_SuDungLL.col_MaTaiSan, item[cls_TaiSan_SuDungLL.col_MaTaiSan]
                                                        , cls_TaiSan_SuDungLL.col_MaKhu, slbKhu.txtMa.Text
                                                        , cls_TaiSan_SuDungLL.col_MaTang, slbTang.txtMa.Text
                                                        , cls_TaiSan_SuDungLL.col_MaPhongCongNang, slbPhongCongNang.txtMa.Text)).Count();

                            _lstSoLuongBanGiao.Add(item[cls_TaiSan_SuDungLL.col_MaTaiSan].ToString(), decimal.Parse(item["SoLuongBanGiao"].ToString()));

                            try
                            {
                                item[cls_TaiSan_NhapCT.col_DonGia] = _apiTaiSan.Get_NhapCTTheoMa(item[cls_TaiSan_SuDungLL.col_MaNhapCT].ToString())[cls_TaiSan_NhapCT.col_DonGia];
                                _lstDonGia.Add(item[cls_TaiSan_SuDungLL.col_MaTaiSan].ToString(), decimal.Parse(item[cls_TaiSan_NhapCT.col_DonGia].ToString()));
                            }
                            catch
                            {
                                _lstDonGia.Add(item[cls_TaiSan_SuDungLL.col_MaTaiSan].ToString(), 0);
                            }
                        }
                        else
                        {
                            item["SoLuongBanGiao"] = _lstSoLuongBanGiao[item[cls_TaiSan_SuDungLL.col_MaTaiSan].ToString()];
                            item[cls_TaiSan_NhapCT.col_DonGia] = _lstDonGia[item[cls_TaiSan_SuDungLL.col_MaTaiSan].ToString()];
                        }
                    }
                    catch
                    {
                        item["SoLuongBanGiao"] = 0;
                        item[cls_TaiSan_NhapCT.col_DonGia] = 0;
                    }
                }
                foreach (string maTaiSan in _lstSoLuongBanGiao.Keys)
                {
                    tongTien += decimal.Parse(_lstDonGia[maTaiSan].ToString()) * decimal.Parse(_lstSoLuongBanGiao[maTaiSan].ToString());
                }

                #endregion

                drKiemKeLL["TongTienBanGiao"] = tongTien;

                dtKiemKeLL.Rows.Add(drKiemKeLL);

                dtKiemKeLL.TableName = "KiemKeLL";
                dtNhanVienKiemKe.TableName = "NhanVienKiemKe";
                dtKiemKeCT.TableName = "KiemKeCT";


                if (dtKiemKeLL.Rows.Count > 0)
                {
                    ds.Tables.Add(dtKiemKeLL);
                    ds.Tables.Add(dtNhanVienKiemKe);
                    ds.Tables.Add(dtKiemKeCT);
                    if (!System.IO.Directory.Exists("..//xml"))
                    {
                        System.IO.Directory.CreateDirectory("..//xml");
                    }
                    ds.WriteXml("..//xml//ts_BienBanKiemKe.xml", XmlWriteMode.WriteSchema);

                    if (!System.IO.File.Exists("..\\..\\..\\Report\\ts_BienBanKiemKe.rpt"))
                    {
                        MessageBox.Show("Thiếu file: ts_BienBanKiemKe.rpt trong thư mục Report.");
                        return;
                    }
                    ReportDocument rptDoc = new ReportDocument();
                    rptDoc.Load("..\\..\\..\\Report\\ts_BienBanKiemKe.rpt", OpenReportMethod.OpenReportByDefault);
                    rptDoc.SetDataSource(ds);
                    frm_Report frm = new frm_Report(rptDoc);
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("In thất bại. Vui lòng kiểm tra lại kết nối máy in");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("In thất bại: " + ex.Message);
            }
        }

        #endregion

        #endregion

        #region Sự kiện

        #region frm_KiemKeTaiSan_Load (Sự kiện load form)

        private void frm_KiemKeTaiSan_Load(object sender, EventArgs e)
        {
            LoadData();
            //dtpNgayKiemKe.Value = DateTime.Now;
            btnSua.Enabled = true;
            slbNhanVien.txtTen.WatermarkText = "Họ và tên";
        }

        #endregion

        #region chkAllMain_CheckedChanged (Sự kiện chọn tất cả các tài sản chưa kiểm kê)

        private void chkAllMain_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRowView view in _bsTaiSan)
                {
                    view["Chon"] = chkAllMain.Checked;
                }
                _bsTaiSan.EndEdit();
            }
            catch
            {
            }
        }

        #endregion

        #region chkAllKiemKe_CheckedChanged (Chọn tất cả các dòng đã kiểm kê)

        private void chkAllKiemKe_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRowView view in _bsKiemKe)
                {
                    view["Chon"] = chkAllKiemKe.Checked;
                }
                _bsKiemKe.EndEdit();
            }
            catch
            {
            }
        }

        #endregion

        #region txtTimKiem_TextChanged (Sự kiện tìm kiếm tài sản)

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtTaiSan = _dtTaiSan.Select(string.Format("{0} like '%{1}%'", cls_TaiSan_SuDungLL.col_TenTaiSan, txtTimKiem.Text)).CopyToDataTable();
                dgvMain.DataSource = dtTaiSan;
            }
            catch
            {
                dgvMain.DataSource = null;
            }
        }

        #endregion

        #region txtMaVach_TextChanged (Sự kiện nhập/quét mã vạch)

        private void txtMaVach_TextChanged(object sender, EventArgs e)
        {
            string sTimKiem = "";
            if (txtMaVach.Text.Trim().Length >= 17)
            {
                sTimKiem = string.Format("{0} = '{1}'", cls_TaiSan_SuDungLL.col_MaVach, txtMaVach.Text.Trim());
                try
                {
                    ShowChiTiet(_rowKiemKe);
                    _bsTaiSan.Filter = sTimKiem;
                    _bsKiemKe.Filter = sTimKiem;
                    if (dgvKiemKe.RowCount > 0)
                    {
                        MessageBox.Show(string.Format("Tài sản '{0} :{1}' đã kiểm kê. \nNếu bạn muốn thay đổi thông tin vui lòng bấm 'Cập nhật' để lưu lại."
                    , txtTaiSan.Text, txtSerinumber.Text));
                        txtMaVach.Focus();

                        btnKiemKe.Enabled = false;
                    }
                    else
                    {
                        btnKiemKe.Enabled = true;
                    }
                }
                catch
                {
                    try
                    {
                        ShowChiTiet(_rowKiemKe);
                        _bsTaiSan.Filter = sTimKiem;
                        _bsKiemKe.Filter = sTimKiem;
                        txtSerinumber.Focus();
                        btnKiemKe.Enabled = true;
                    }
                    catch
                    {
                    }
                }
            }
            else if (txtMaVach.Text == "")
            {
                _bsTaiSan.Filter = "1=1";
                _bsKiemKe.Filter = "1=1";
                ShowChiTiet(_rowKiemKe);
            }
            else
            {
                _bsTaiSan.Filter = "0=1";
                _bsKiemKe.Filter = "0=1";
                ShowChiTiet(_rowKiemKe);
            }
            lblTongTaiSan.Text = dgvMain.RowCount.ToString();
            lblTongKiemKe.Text = dgvKiemKe.RowCount.ToString();
            txtKiemKe.Text = dgvKiemKe.RowCount.ToString() + "/" + (dgvKiemKe.RowCount + dgvMain.RowCount).ToString();
        }

        #endregion

        #region slbDonViSuDung_HisSelectChange (Sự kiện thay đổi đơn vị sử dụng)

        private void slbDonViSuDung_HisSelectChange(object sender, EventArgs e)
        {
            Load_NguoiSuDung(slbDonViSuDung.txtMa.Text);
            ShowTaiSan();
        }

        #endregion

        #region btnKiemKe_Click (Sự kiện kiểm kê)

        private void btnKiemKe_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Bạn chưa chọn tài sản để kiểm kê");
                return;
            }
            if (string.IsNullOrWhiteSpace(slbSoPhieu.txtTen.Text))
            {
                MessageBox.Show("Bạn chưa nhập số phiếu");
                slbSoPhieu.Focus();
                return;
            }
            if (_maKiemKeLL == "")
            {
                _maKiemKeLL = _apiTaiSan.Insert_KiemKeLL(slbSoPhieu.txtTen.Text, slbDotKiemKe.txtMa.Text, slbDotKiemKe.txtTen.Text);

                if (_maKiemKeLL == "")
                {
                    return;
                }
            }
            //_apiTaiSan.Update_TaiSan(txtSerinumber.Text, txtMaVach.Text);
            if (_apiTaiSan.Insert_KiemKeCT(_maKiemKeLL, slbSoPhieu.txtTen.Text, txtMaVach.Text, txtMaTaiSan.Text, slbTrangThaiTaiSan.txtMa.Text, slbTrangThaiTaiSan.txtTen.Text))
            {
                ShowTaiSanDaKiemKe();
                _rowKiemKe = null;
                _bsTaiSan.Remove(_bsTaiSan.Current);
                txtMaVach.Text = "";
                txtMaVach.Focus();
                //MessageBox.Show("Ok");
            }
        }

        #endregion

        #region lblDotKiemKe_Click (Sự kiện mở danh mục đợt kiểm kê)

        private void lblDotKiemKe_Click(object sender, EventArgs e)
        {
            var frm = new frm_DanhMucChung("DotKiemKe", "Đợt Kiểm Kê", "", "", "", "");
            frm.ShowDialog();
            slbDotKiemKe.DataSource = Load_DanhMuc("DotKiemKe", "-1");
        }

        #endregion

        #region slbDotKiemKe_HisSelectChange (Sự kiện chọn đợt kiểm kê)

        private void slbDotKiemKe_HisSelectChange(object sender, EventArgs e)
        {
            Load_SoPhieu();
            ShowTaiSanChuaKiemKe();
        }

        #endregion

        #region txtSerinumber_KeyDown (Sự kiện Enter ô serialNumber)

        private void txtSerinumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMaVach.Focus();
            }
        }

        #endregion

        #region dgvKiemKe_CellClick (Sự kiện xóa dòng tài sản đã kiểm kê)

        private void dgvKiemKe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    string maVach = dgvKiemKe.SelectedRows[0].Cells["colMaVachKiemKe"].Value.ToString();
                    string sTimKiem = string.Format("{0} = '{1}'", cls_TaiSan_SuDungLL.col_MaVach, maVach);
                    _rowKiemKe = ((DataTable)_bsKiemKe.DataSource).Select(sTimKiem).First();
                    txtMaVach.Text = maVach;
                }
                if (e.ColumnIndex == 2)
                {
                    string idKiemKe = dgvKiemKe.SelectedRows[0].Cells["colIDKiemKe"].Value.ToString();

                    DialogResult rsl = MessageBox.Show(string.Format("Bạn có muốn xóa kiểm kê tài sản '{0}:{1}' không?"
                         , dgvKiemKe.SelectedRows[0].Cells["colTenTaiSanKiemKe"].Value.ToString(), dgvKiemKe.SelectedRows[0].Cells["colMaVachKiemKe"].Value.ToString())
                         , "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rsl == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (_apiTaiSan.Delete_KiemKeCT(dgvKiemKe.SelectedRows[0].Cells["colIDKiemKe"].Value.ToString()))
                        {
                            ShowTaiSan();
                        }
                    }
                }
            }
            catch
            {
            }
        }

        #endregion

        #region dgvMain_RowPostPaint (Sự kiện tạo số thứ tự danh sách tài sản chưa kiểm kê)

        private void dgvMain_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewX dg = (DataGridViewX)sender;
            string rowNumber = (e.RowIndex + 1).ToString();
            while (rowNumber.Length < dg.RowCount.ToString().Length)
            {
                rowNumber = "0" + rowNumber;
            }
            SizeF size = e.Graphics.MeasureString(rowNumber, dg.Font);
            if (dg.RowHeadersWidth < (int)(size.Width + 20))
            {
                dg.RowHeadersWidth = (int)(size.Width + 20);
            }
            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(rowNumber, dg.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));
        }

        #endregion

        #region dgvKiemKe_RowPostPaint (Sự kiện tạo số thứ tự danh sách tài sản đã kiểm kê)

        private void dgvKiemKe_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewX dg = (DataGridViewX)sender;
            string rowNumber = (e.RowIndex + 1).ToString();
            while (rowNumber.Length < dg.RowCount.ToString().Length)
            {
                rowNumber = "0" + rowNumber;
            }
            SizeF size = e.Graphics.MeasureString(rowNumber, dg.Font);
            if (dg.RowHeadersWidth < (int)(size.Width + 20))
            {
                dg.RowHeadersWidth = (int)(size.Width + 20);
            }
            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(rowNumber, dg.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));
        }

        #endregion

        #region btnIn_Click (Sự kiện in mã vạch tài sản đang chọn)

        private void btnIn_Click(object sender, EventArgs e)
        {
            In();
        }

        #endregion

        #region slbKhu_HisSelectChange (Sự kiện thay đổi khu sử dụng)

        private void slbKhu_HisSelectChange(object sender, EventArgs e)
        {
            ShowTaiSan();
            slbTang.DataSource = Load_DanhMuc(cls_sys_LoaiDanhMuc.tang_MaLoai, slbKhu.txtMa.Text);
        }

        #endregion

        #region slbTang_HisSelectChange (Sự kiện thay đổi tầng sử dụng)

        private void slbTang_HisSelectChange(object sender, EventArgs e)
        {
            ShowTaiSan();
            slbPhongCongNang.DataSource = Load_DanhMuc(cls_sys_LoaiDanhMuc.phong_MaLoai, slbTang.txtMa.Text);
        }

        #endregion

        #region slbPhongCongNang_HisSelectChange (Sự kiện thay đổi phòng công năng)

        private void slbPhongCongNang_HisSelectChange(object sender, EventArgs e)
        {
            ShowTaiSan();
        }

        #endregion

        #region slbDonViQuanLy_HisSelectChange (Sự kiện thay đổi đơn vị quản lý)

        private void slbDonViQuanLy_HisSelectChange(object sender, EventArgs e)
        {
            ShowTaiSan();
        }

        #endregion

        #region slbNguoiSuDung_HisSelectChange (Sự kiện thay đổi người sử dụng)

        private void slbNguoiSuDung_HisSelectChange(object sender, EventArgs e)
        {
            ShowTaiSan();
        }

        #endregion

        #region lblTongKiemKe_TextChanged (Sự kiện tổng kiểm kê thay đổi)

        private void lblTongKiemKe_TextChanged(object sender, EventArgs e)
        {
            txtKiemKe.Text = lblTongKiemKe.Text + "/" + (dgvMain.RowCount + dgvKiemKe.RowCount).ToString();
        }

        #endregion

        #region dgvMain_CellClick (Sự kiện chọn tài sản để sửa/kiểm kê)

        private void dgvMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    DataRowView view = (DataRowView)_bsTaiSan.Current;
                    string maVach = view[cls_TaiSan_SuDungLL.col_MaVach].ToString();
                    string sTimKiem = string.Format("{0} = '{1}'", cls_TaiSan_SuDungLL.col_MaVach, maVach);

                    _rowKiemKe = _dtTaiSan.Select(sTimKiem).First();
                    txtMaVach.Text = maVach;
                }
            }
            catch
            {
            }
        }

        #endregion

        #region dgvKiemKe_Resize (Sự kiện thay đổi kích thước lưới danh sách tài sản đã kiểm kê)

        private void dgvKiemKe_Resize(object sender, EventArgs e)
        {
            lblTongKiemKe.Location = new Point(dgvKiemKe.Location.X + 3, lblTongKiemKe.Location.Y);
            chkAllKiemKe.Location = new Point(dgvKiemKe.Location.X + 61, lblTongKiemKe.Location.Y + 2);
        }

        #endregion

        #region lblTongTaiSan_TextChanged (Sự kiện thay đổi tổng tài sản)

        private void lblTongTaiSan_TextChanged(object sender, EventArgs e)
        {
            lblTongKiemKe_TextChanged(null, null);
        }

        #endregion

        #region slbTaiSan_HisSelectChange (Sự kiện thay đổi tài sản lọc)

        private void slbTaiSan_HisSelectChange(object sender, EventArgs e)
        {
            ShowTaiSan();
        }

        #endregion

        #region txtKyHieu2_TextChanged (Sự kiện thay đổi ký hiệu lọc)

        private void txtKyHieu2_TextChanged(object sender, EventArgs e)
        {
            ShowTaiSan();
        }

        #endregion

        #region lblTrangThai_Click (Sự kiện mở form danh mục trạng thái tài sản)

        private void lblTrangThai_Click(object sender, EventArgs e)
        {
            var frm = new frm_DanhMucChung(cls_sys_LoaiDanhMuc.trangThaiTaiSan_MaLoai, cls_sys_LoaiDanhMuc.trangThaiTaiSan_TenLoai, "", "", "", "");
            frm.ShowDialog();
            slbTrangThaiTaiSan.DataSource = Load_DanhMuc(cls_sys_LoaiDanhMuc.trangThaiTaiSan_MaLoai, "-1");
        }

        #endregion

        #region btnInBienBanKiemKe_Click (Sự kiện in biên bản kiểm kê)

        private void btnInBienBanKiemKe_Click(object sender, EventArgs e)
        {
            InBienBanKiemKe();
        }

        #endregion

        private void slbSoPhieu_HisSelectChange(object sender, EventArgs e)
        {
            ShowTaiSanDaKiemKe();
            Show_NhanVienKiemKe();
            txtDiaDiem.Text = _apiTaiSan.Get_DiaDiemKiemKe(cls_sys_LoaiDanhMuc.kiemKeTaiSan_MaLoai, cls_sys_LoaiDanhMuc.diaDiemKiemKe, slbSoPhieu.txtTen.Text);
            if (_maKiemKeLL == "")
            {
                _maKiemKeLL = _apiTaiSan.Get_MaTheoSoPhieu(cls_TaiSan_KiemKeLL.tb_TenBang, cls_TaiSan_KiemKeLL.col_Ma
                                                            , cls_TaiSan_KiemKeLL.col_SoPhieu, slbSoPhieu.txtTen.Text);
            }
        }

        private void lblSoPhieu_Click(object sender, EventArgs e)
        {
            slbSoPhieu.txtTen.Text = _apiOracle.Get_MaMoi();
        }

        #endregion

        //private void lblDiaDiem_Click(object sender, EventArgs e)
        //{
        //    if (!_apiTaiSan.Luu_NhanVienBanGiaoKiemKe(cls_sys_LoaiDanhMuc.kiemKeTaiSan_MaLoai, slbSoPhieu.txtTen.Text
        //            , cls_sys_LoaiDanhMuc.diaDiemKiemKe, "", "", txtDiaDiem.Text))
        //    {

        //    }
        //    for (int i = 0; i < dgvNhanVienKiemKe.RowCount - 1; i++)
        //    {
                
        //    }
        //}

        private void dgvNhanVienKiemKe_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void lblNhanVien_Click(object sender, EventArgs e)
        {

        }

        #region btnNhanVien_Click (Thêm nhân viên kiểm kê)

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            try
            {
                if (_apiTaiSan.Luu_NhanVienBanGiaoKiemKe(cls_sys_LoaiDanhMuc.kiemKeTaiSan_MaLoai, slbSoPhieu.txtTen.Text, cls_sys_LoaiDanhMuc.nhanVienKiemKe
                    , slbNhanVien.txtMa.Text, slbNhanVien.txtTen.Text, txtChucDanh.Text))
                {
                    slbNhanVien.txtMa.Text = "";
                    slbNhanVien.txtTen.Text = "";
                    txtChucDanh.Text = "";
                    Show_NhanVienKiemKe();
                    slbNhanVien.txtTen.Focus();
                }
            }
            catch
            {
            }
        } 

        #endregion

        #region dgvNhanVienKiemKe_CellClick (Sửa nhân viên kiểm kê)

        private void dgvNhanVienKiemKe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    slbNhanVien.txtMa.Text = dgvNhanVienKiemKe.SelectedRows[0].Cells["colMaNhanVien"].Value.ToString();
                    slbNhanVien.txtTen.Text = dgvNhanVienKiemKe.SelectedRows[0].Cells["colTenNhanVien"].Value.ToString();
                    txtChucDanh.Text = dgvNhanVienKiemKe.SelectedRows[0].Cells["colChucDanh"].Value.ToString();
                    return;
                }
                if (e.ColumnIndex == 1)
                {
                    if (_apiTaiSan.Delete_NhanVienBanGiaoKiemKe(cls_sys_LoaiDanhMuc.kiemKeTaiSan_MaLoai, slbSoPhieu.txtTen.Text, cls_sys_LoaiDanhMuc.nhanVienKiemKe
                        , dgvNhanVienKiemKe.SelectedRows[0].Cells["colMaNhanVien"].Value.ToString()))
                    {
                        Show_NhanVienKiemKe();
                    }
                }
            }
            catch
            {

            }
        } 

        #endregion

    }
}
