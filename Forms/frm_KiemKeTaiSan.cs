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
    public partial class frm_KiemKeTaiSan : frm_DanhMuc
    {
        #region Biến toàn cục

        private Api_Common _api = new Api_Common();
        api_TaiSan _apiTaiSan = new api_TaiSan();
        DataTable _dtTaiSan = new DataTable();
        DataTable _dtKiemKe = new DataTable();
        private string _userError = "";
        private string _systemError = "";
        private BindingSource _bsTaiSan = new BindingSource();
        private BindingSource _bsKiemKe = new BindingSource();
        private bool _suaTaiSan = false;
        private string _maSuDungLL = "";
        private SortedList<string, decimal> _lstSoLuongBanGiao = new SortedList<string, decimal>();
        private SortedList<string, decimal> _lstDonGia = new SortedList<string, decimal>();

        #endregion

        #region Khởi tạo

        public frm_KiemKeTaiSan()
        {
            InitializeComponent();

            _api.KetNoi();
        }

        #endregion

        #region Phương thức

        #region Phương thức kế thừa

        #region LoadData (Load dữ liệu khi mở form)
        /// <summary>
        /// Load dữ liệu khi mở form
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/05/18</Date>
        protected override void LoadData()
        {
            slbKhu.DataSource = Load_DanhMuc(cls_sys_LoaiDanhMuc.khu_MaLoai, "-1");
            slbLoaiTaiSan.DataSource = Load_DanhMuc(cls_sys_LoaiDanhMuc.loaiTaiSan_MaLoai, "-1");

            Load_KhoaPhongQuanLy();
            Load_KhoaPhongSuDung();
            Load_TaiSan();
            slbDotKiemKe.DataSource = Load_DanhMuc("DotKiemKe", "-1");
            Load_TrangThaiTaiSan();

            base.LoadData();
        }

        #endregion

        #region Load_CauHinh (Load cấu hình khi mở form)
        /// <summary>
        /// Load cấu hình khi mở form
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/05/22</Date>
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
        /// <Date>2018/05/16</Date>
        protected override void Them()
        {
            base.Them();
            pnlTimKiem.Enabled = true;
            pnlMain.Enabled = true;
            dgvMain.Enabled = true;
            dgvKiemKe.Enabled = true;
        }

        #endregion

        protected override void Luu()
        {
            if (_apiTaiSan.Update_TaiSan(txtMaTaiSan.Text, txtTaiSan.Text, txtKyHieu.Text, slbLoaiTaiSan.txtMa.Text, slbLoaiTaiSan.txtTen.Text, txtQuyCach.Text) > 0
                && _apiTaiSan.Update_TaiSan(txtSerinumber.Text, txtMaVach.Text) > 0)
            {
                MessageBox.Show("Cập nhật tài sản thành công");
                ShowTaiSan();
            }
        }
        
        protected override void BoQua()
        {
            txtMaVach.Text = "";
            txtMaVach.Focus();
            //base.BoQua();
        }

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

        #region Phương thức dùng riêng

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

        private void Load_KhoaPhongQuanLy()
        {
            List<string> lstCot = new List<string>();
            lstCot.Add(cls_LT_DanhMucBoPhan.col_MA);
            lstCot.Add(cls_LT_DanhMucBoPhan.col_TEN);

            slbDonViQuanLy.DataSource = _api.Search(ref _userError, ref _systemError, cls_LT_DanhMucBoPhan.tb_TenBang, lst: lstCot);
        }

        private void Load_KhoaPhongSuDung()
        {
            List<string> lstCot = new List<string>();
            lstCot.Add(cls_LT_DanhMucBoPhan.col_MA);
            lstCot.Add(cls_LT_DanhMucBoPhan.col_TEN);

            slbDonViSuDung.DataSource = _api.Search(ref _userError, ref _systemError, cls_LT_DanhMucBoPhan.tb_TenBang, lst: lstCot);
        }

        private void Load_NhanVien(string maBoPhan)
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

        private void Load_TaiSan()
        {
            List<string> lstCot = new List<string>();
            lstCot.Add(cls_TaiSan_DanhMuc.col_Ma);
            lstCot.Add(cls_TaiSan_DanhMuc.col_Ten);

            slbTaiSan.DataSource = _api.Search(ref _userError, ref _systemError, cls_TaiSan_DanhMuc.tb_TenBang, lst: lstCot);
        }

        private void ShowChiTiet(DataRow row)
        {
            try
            {
                txtID.Text = row[cls_TaiSan_SuDungLL.col_ID].ToString();
                txtMaTaiSan.Text = row[cls_TaiSan_SuDungLL.col_MaTaiSan].ToString();
                txtSerinumber.Text = row[cls_TaiSan_SuDungLL.col_Serinumber].ToString();
                txtTaiSan.Text = row[cls_TaiSan_SuDungLL.col_TenTaiSan].ToString();
                txtKyHieu.Text = row[cls_TaiSan_SuDungLL.col_KyHieu].ToString();
                lblNgayCapNhat.Text = row["NgayCapNhat"].ToString();
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
                lblNgayCapNhat.Text = "";
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
            lblKiemKe.Text = dgvKiemKe.RowCount.ToString() + "/" + (dgvKiemKe.RowCount + dgvMain.RowCount).ToString();
        }

        private void ShowTaiSan()
        {
            try
            {
                _dtTaiSan = _apiTaiSan.Get_ThongTinTaiSan(slbDotKiemKe.txtMa.Text, 0, slbKhu.txtMa.Text, slbTang.txtMa.Text, slbPhongCongNang.txtMa.Text
                                , slbDonViQuanLy.txtMa.Text, slbDonViSuDung.txtMa.Text, slbNguoiSuDung.txtMa.Text, slbTaiSan.txtMa.Text, txtKyHieu2.Text);
                _dtTaiSan.Columns.Add("Chon");
                _bsTaiSan.DataSource = _dtTaiSan;
                dgvMain.DataSource = _bsTaiSan;

                lblTongTaiSan.Text = dgvMain.RowCount.ToString();
            }
            catch
            {
                lblTongTaiSan.Text = "0";
            }
            try
            {
                _dtKiemKe = _apiTaiSan.Get_ThongTinTaiSan(slbDotKiemKe.txtMa.Text, 1, slbKhu.txtMa.Text, slbTang.txtMa.Text, slbPhongCongNang.txtMa.Text
                                , slbDonViQuanLy.txtMa.Text, slbDonViSuDung.txtMa.Text, slbNguoiSuDung.txtMa.Text, slbTaiSan.txtMa.Text, txtKyHieu2.Text);
                _dtKiemKe.Columns.Add("Chon");
                _dtKiemKe.PrimaryKey = new DataColumn[] { _dtKiemKe.Columns[cls_TaiSan_SuDungLL.col_ID] };
                _bsKiemKe.DataSource = _dtKiemKe;
                dgvKiemKe.DataSource = _bsKiemKe;
                lblTongKiemKe.Text = dgvKiemKe.RowCount.ToString();
            }
            catch
            {
                lblTongKiemKe.Text = "0";
            }
            lblKiemKe.Text = dgvKiemKe.RowCount.ToString() + "/" + (dgvKiemKe.RowCount + dgvMain.RowCount).ToString();
        }

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

        private void InBienBanKiemKe()
        {
            try
            {
                //if (slbDonViQuanLy.txtMa.Text == "")
                //{
                //    MessageBox.Show("Bạn chưa chọn đơn vị quản lý");
                //    slbDonViQuanLy.txtTen.Focus();
                //    return;
                //}
                //if (slbDonViSuDung.txtMa.Text == "")
                //{
                //    MessageBox.Show("Bạn chưa chọn đơn vị sử dụng");
                //    slbDonViSuDung.txtTen.Focus();
                //    return;
                //}
                _lstSoLuongBanGiao.Clear();
                _lstDonGia.Clear();
                decimal tongTien = 0;
                DataSet ds = new DataSet();

                #region Xuất XML thông tin kiểm kê LL

                DataTable dtKiemKeLL = new DataTable();

                dtKiemKeLL.Columns.Add("MaDotKiemKe");
                dtKiemKeLL.Columns.Add("TenDotKiemKe");
                dtKiemKeLL.Columns.Add("TongTienBanGiao");

                DataRow drKiemKeLL = dtKiemKeLL.NewRow();
                drKiemKeLL["MaDotKiemKe"] = slbDotKiemKe.txtMa.Text;
                drKiemKeLL["TenDotKiemKe"] = slbDotKiemKe.txtTen.Text;
                

                #endregion

                #region Xuất XML thông tin kiểm kê CT

                DataTable dtKiemKeCT = ((DataTable)_bsKiemKe.DataSource).Select().CopyToDataTable();
                dtKiemKeCT.Columns.Add(cls_TaiSan_NhapCT.col_DonGia);
                 dtKiemKeCT.Columns.Add("SoLuongBanGiao");
                DataRow drTaiSan;
                DataTable dtBanGiao = _apiTaiSan.Get_ThongTinTaiSan(slbDotKiemKe.txtMa.Text, -1, slbKhu.txtMa.Text, slbTang.txtMa.Text, slbPhongCongNang.txtMa.Text
                                , slbDonViQuanLy.txtMa.Text, slbDonViSuDung.txtMa.Text, slbNguoiSuDung.txtMa.Text, slbTaiSan.txtMa.Text, txtKyHieu2.Text);
                foreach (DataRow item in dtKiemKeCT.Rows)
                {
                    try
                    {
                        //drTaiSan = _apiTaiSan.Get_TaiSanTheoMa(item[cls_TaiSan_SuDungLL.col_MaTaiSan].ToString());
                        //item[cls_TaiSan_DanhMuc.col_NuocSanXuat] = drTaiSan[cls_TaiSan_DanhMuc.col_NuocSanXuat];
                        //item[cls_TaiSan_DanhMuc.col_NamSanXuat] = drTaiSan[cls_TaiSan_DanhMuc.col_NamSanXuat];
                        //item[cls_TaiSan_DanhMuc.col_QuyCach] = drTaiSan[cls_TaiSan_DanhMuc.col_QuyCach];
                        //item[cls_TaiSan_DanhMuc.col_TaiLieu] = drTaiSan[cls_TaiSan_DanhMuc.col_TaiLieu];
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
                dtKiemKeCT.TableName = "KiemKeCT";

                if (dtKiemKeLL.Rows.Count > 0)
                {
                    ds.Tables.Add(dtKiemKeLL);
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
            dtpNgayKiemKe.Value = DateTime.Now;
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

        #region Chọn tất cả các dòng đã kiểm kê

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

        #region Sự kiện tìm kiếm

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

        #region Sự kiện nhập mã vạch

        private void txtMaVach_TextChanged(object sender, EventArgs e)
        {
            DataRow row = null;

            string sTimKiem = "";

            if (txtMaVach.Text.Trim().Length >= 17)
            {
                try
                {
                    sTimKiem = string.Format("{0} = '{1}'", cls_TaiSan_SuDungLL.col_MaVach, txtMaVach.Text.Trim());

                    row = _dtKiemKe.Select(sTimKiem).CopyToDataTable().Rows[0];
                    ShowChiTiet(row);
                    _bsTaiSan.Filter = sTimKiem;
                    _bsKiemKe.Filter = sTimKiem;
                    if (btnKiemKe.Enabled)
                    {
                        MessageBox.Show(string.Format("Tài sản '{0} :{1}' đã kiểm kê. \nNếu bạn muốn thay đổi thông tin vui lòng bấm 'Cập nhật' để lưu lại."
                    , txtTaiSan.Text, txtSerinumber.Text));
                        txtMaVach.Focus();

                        btnKiemKe.Enabled = false;
                    }
                }
                catch
                {
                    try
                    {
                        row = _dtTaiSan.Select(string.Format("{0} = '{1}'", cls_TaiSan_SuDungLL.col_MaVach, txtMaVach.Text.Trim())).CopyToDataTable().Rows[0];
                        ShowChiTiet(row);
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
                ShowChiTiet(row);
            }
            else
            {
                _bsTaiSan.Filter = "0=1";
                _bsKiemKe.Filter = "0=1";
                ShowChiTiet(row);
            }
            lblTongTaiSan.Text = dgvMain.RowCount.ToString();
            lblTongKiemKe.Text = dgvKiemKe.RowCount.ToString();
            lblKiemKe.Text = dgvKiemKe.RowCount.ToString() + "/" + (dgvKiemKe.RowCount + dgvMain.RowCount).ToString();
        }

        #endregion

        #region Sự kiện chọn đơn vị sử dụng

        private void slbDonViSuDung_HisSelectChange(object sender, EventArgs e)
        {
            Load_NhanVien(slbDonViSuDung.txtMa.Text);
            ShowTaiSan();
        }

        #endregion

        #region Sự kiện kiểm kê

        private void btnKiemKe_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Bạn chưa chọn tài sản để kiểm kê");
                return;
            }
            else if (_apiTaiSan.UpDate_KiemKe(txtID.Text, slbDotKiemKe.txtMa.Text, slbDotKiemKe.txtTen.Text, slbTrangThaiTaiSan.txtMa.Text, slbTrangThaiTaiSan.txtTen.Text))
            {
                ShowTaiSan();

                txtMaVach.Text = "";
                txtMaVach.Focus();
            }
        }

        #endregion

        #region Sự kiện mở danh mục đợt kiểm kê

        private void lblDotKiemKe_Click(object sender, EventArgs e)
        {
            var frm = new frm_DanhMucChung("DotKiemKe", "Đợt Kiểm Kê", "", "", "", "");
            frm.ShowDialog();
            slbDotKiemKe.DataSource = Load_DanhMuc("DotKiemKe", "-1");
        }

        #endregion

        #region Sự kiện mở danh mục khu

        private void lblKhu_Click(object sender, EventArgs e)
        {
            var frm = new frm_DanhMucChung(cls_sys_LoaiDanhMuc.khu_MaLoai, cls_sys_LoaiDanhMuc.khu_TenLoai, "", "", "", "");

            frm.ShowDialog();
            slbKhu.DataSource = Load_DanhMuc(cls_sys_LoaiDanhMuc.khu_MaLoai, "");
        }

        #endregion

        #region Sự kiện mở danh mục tầng

        private void lblTang_Click(object sender, EventArgs e)
        {
            var frm = new frm_DanhMucChung(cls_sys_LoaiDanhMuc.tang_MaLoai, cls_sys_LoaiDanhMuc.tang_TenLoai, cls_sys_LoaiDanhMuc.khu_MaLoai, cls_sys_LoaiDanhMuc.khu_TenLoai
                , slbKhu.txtMa.Text, slbKhu.txtTen.Text);
            frm.ShowDialog();
            slbTang.DataSource = Load_DanhMuc(cls_sys_LoaiDanhMuc.tang_MaLoai, slbKhu.txtMa.Text);
        }

        #endregion

        #region Sự kiện mở danh mục phòng công năng

        private void lblPhong_Click(object sender, EventArgs e)
        {
            var frm = new frm_DanhMucChung(cls_sys_LoaiDanhMuc.phong_MaLoai, cls_sys_LoaiDanhMuc.phong_TenLoai, cls_sys_LoaiDanhMuc.tang_MaLoai, cls_sys_LoaiDanhMuc.tang_TenLoai
                                            , slbTang.txtMa.Text, slbTang.txtTen.Text);
            frm.ShowDialog();
            slbPhongCongNang.DataSource = Load_DanhMuc(cls_sys_LoaiDanhMuc.phong_MaLoai, slbTang.txtMa.Text);
        }

        #endregion

        #region Sự kiện chọn đợt kiểm kê

        private void slbDotKiemKe_HisSelectChange(object sender, EventArgs e)
        {
            ShowTaiSan();
        }

        #endregion

        #region Sự kiện Enter ô serialNumber

        private void txtSerinumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMaVach.Focus();
            }
        }

        #endregion

        #region Sự kiện xóa dòng tài sản đã kiểm kê

        private void dgvKiemKe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    txtMaVach.Text = dgvKiemKe.SelectedRows[0].Cells["colMaVachKiemKe"].Value.ToString();
                }
                if (e.ColumnIndex == 2)
                {
                    string idKiemKe = dgvKiemKe.SelectedRows[0].Cells["colIDKiemKe"].Value.ToString();

                    DialogResult rsl = MessageBox.Show(string.Format("Bạn có muốn xóa kiểm kê tài sản '{0}:{1}' không?"
                         , dgvKiemKe.SelectedRows[0].Cells["colTenTaiSanKiemKe"].Value.ToString(), dgvKiemKe.SelectedRows[0].Cells["colMaVachKiemKe"].Value.ToString())
                         , "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rsl == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (_apiTaiSan.UpDate_KiemKe(idKiemKe, "", "", slbTrangThaiTaiSan.txtMa.Text, slbTrangThaiTaiSan.txtTen.Text))
                        {
                            ShowTaiSan();
                            ShowChiTiet(null);
                        }
                    }
                }
            }
            catch
            {
            }
        }

        #endregion

        #region Sự kiện tạo số thứ tự

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

        private void btnIn_Click(object sender, EventArgs e)
        {
            In();
        }

        private void slbKhu_HisSelectChange(object sender, EventArgs e)
        {
            ShowTaiSan();
            slbTang.DataSource = Load_DanhMuc(cls_sys_LoaiDanhMuc.tang_MaLoai, slbKhu.txtMa.Text);
        }

        private void slbTang_HisSelectChange(object sender, EventArgs e)
        {
            ShowTaiSan();
            slbPhongCongNang.DataSource = Load_DanhMuc(cls_sys_LoaiDanhMuc.phong_MaLoai, slbTang.txtMa.Text);
        }

        private void slbPhongCongNang_HisSelectChange(object sender, EventArgs e)
        {
            ShowTaiSan();
        }

        private void slbDonViQuanLy_HisSelectChange(object sender, EventArgs e)
        {
            ShowTaiSan();
        }

        private void slbNguoiSuDung_HisSelectChange(object sender, EventArgs e)
        {
            ShowTaiSan();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {

        }

        private void lblTongKiemKe_TextChanged(object sender, EventArgs e)
        {
            lblKiemKe.Text = lblTongKiemKe.Text + "/" + (dgvMain.RowCount + dgvKiemKe.RowCount).ToString();
        }

        private void dgvMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    DataRowView view = (DataRowView)_bsTaiSan.Current;
                    txtMaVach.Text = view[cls_TaiSan_SuDungLL.col_MaVach].ToString();
                }
            }
            catch
            {
            }
        }

        private void dgvKiemKe_Resize(object sender, EventArgs e)
        {
            lblTongKiemKe.Location = new Point(dgvKiemKe.Location.X + 3, lblTongKiemKe.Location.Y);
            chkAllKiemKe.Location = new Point(dgvKiemKe.Location.X + 61, lblTongKiemKe.Location.Y + 2);
        }

        private void lblTongTaiSan_TextChanged(object sender, EventArgs e)
        {
            lblTongKiemKe_TextChanged(null, null);
        }

        private void pnlRight_Paint(object sender, PaintEventArgs e)
        {

        }

        private void slbTaiSan_HisSelectChange(object sender, EventArgs e)
        {
            ShowTaiSan();
        }

        private void txtKyHieu2_TextChanged(object sender, EventArgs e)
        {
            ShowTaiSan();
        }

        private void pnlRight_Resize(object sender, EventArgs e)
        {
            txtDonViQuanLy.Width = txtDonViSuDung.Width = txtNguoiSuDung.Width = pnlRight.Width - 330;
        }

        private void dgvMain_DataSourceChanged(object sender, EventArgs e)
        {
            //lblTongTaiSan.Text = dgvMain.RowCount.ToString();
            //lblKiemKe.Text = dgvKiemKe.RowCount.ToString() + "/" + (dgvKiemKe.RowCount + dgvMain.RowCount).ToString();
        }

        private void dgvKiemKe_DataSourceChanged(object sender, EventArgs e)
        {
            //lblTongKiemKe.Text = dgvKiemKe.RowCount.ToString();
            //lblTongTaiSan.Text = dgvMain.RowCount.ToString();
            //lblKiemKe.Text = dgvKiemKe.RowCount.ToString() + "/" + (dgvKiemKe.RowCount + dgvMain.RowCount).ToString();

        }

        private void lblTrangThai_Click(object sender, EventArgs e)
        {
            var frm = new frm_DanhMucChung(cls_sys_LoaiDanhMuc.trangThaiTaiSan_MaLoai, cls_sys_LoaiDanhMuc.trangThaiTaiSan_TenLoai, "", "", "", "");
            frm.ShowDialog();
            slbTrangThaiTaiSan.DataSource = Load_DanhMuc(cls_sys_LoaiDanhMuc.trangThaiTaiSan_MaLoai, "-1");
        }

        private void btnInBienBanKiemKe_Click(object sender, EventArgs e)
        {
            InBienBanKiemKe();
        }

        #endregion

    }
}
