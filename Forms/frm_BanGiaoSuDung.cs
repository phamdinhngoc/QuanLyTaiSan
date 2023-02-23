using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using E00_Common;
using E00_Model;
using E00_System;
using E00_Base;
using E00_API;
using DevComponents.DotNetBar.Controls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace QuanLyTaiSan
{
    public partial class frm_BanGiao : frm_DanhMuc
    {

        #region Biến toàn cục

        private Api_Common _api = new Api_Common();
        public Acc_Oracle _acc = new Acc_Oracle();
        private api_TaiSan _apiTaiSan = new api_TaiSan();
        private int _rowIndex = 0;
        private string _maNhom = "";
        private string _tenNhom = "";
        private bool _isAdd = true;
        private string _userError = string.Empty;
        private string _systemError = string.Empty;
        BindingSource _bsBanGiao = new BindingSource();
        DataRowView _rowView;
        Dictionary<string, string> _dicNhapLL = new Dictionary<string, string>();
        Dictionary<string, string> _dicNhapCT = new Dictionary<string, string>();
        List<string> _lstKey = new List<string>();
        List<string> _lstUnique = new List<string>();
        Dictionary<string, string> _dicWhere = new Dictionary<string, string>();
        DataTable _dtTaiSan = new DataTable();
        SortedList<string, decimal> _lstBanGiao = new SortedList<string, decimal>();
        List<string> _lstBanGiao2 = new List<string>();
        List<string> _lstMaVachDelete = new List<string>();
        private bool _daLuu = false;

        #endregion

        #region Khởi tạo

        #region Khởi tạo chung

        public frm_BanGiao()
        {
            InitializeComponent();
            _api.KetNoi();
        }

        #endregion

        #region Khởi tạo sử dụng dữ liệu theo nhóm
        /// <summary>
        /// Khởi tạo sử dụng dữ liệu theo nhóm
        /// </summary>
        /// <param name="maNhom">Mã nhóm để lấy dữ liệu</param>
        /// <param name="tenNhom">Tên hiển thị trên textForm</param>
        public frm_BanGiao(string maNhom, string tenNhom)
        {
            InitializeComponent();
            _api.KetNoi();

            _maNhom = maNhom;
            _tenNhom = tenNhom;
            Text = _tenNhom;
        }

        #endregion

        #endregion

        #region Phương thức

        #region Phương thức kế thừa (Protected)

        #region LoadData (Load dữ liệu khi mở Form)
        /// <summary>
        /// Load dữ liệu khi mở Form
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <date>2018/04/23</date>
        protected override void LoadData()
        {
            slbKhu.DataSource = Load_DanhMuc(cls_sys_LoaiDanhMuc.khu_MaLoai, "-1");

            Load_DanhMuc();
            Load_KhoaPhongQuanLy();
            Load_KhoaPhongSuDung();
            Load_TaiSan();
            base.LoadData();
            pnlControl2.Enabled = false;
            btnSua.Enabled = true;
            slbSoPhieu.txtTen.MaxLength = 20;
            pnlSearch.Enabled = false;
        }

        #endregion

        protected override void BoQua()
        {
            base.BoQua();
            Load_DanhMuc();
            btnSua.Enabled = true;
            _lstBanGiao.Clear();
            slbDonViQuanLy.txtMa.Text = "";
            slbDonViQuanLy.txtTen.Text = "";
            slbDonViSuDung.txtMa.Text = "";
            slbDonViSuDung.txtTen.Text = "";
            slbNguoiSuDung.txtMa.Text = "";
            slbNguoiSuDung.txtTen.Text = "";
            slbKhu.txtMa.Text = "";
            slbKhu.txtTen.Text = "";
            slbTang.txtMa.Text = "";
            slbTang.txtTen.Text = "";
            slbPhongCongNang.txtMa.Text = "";
            slbPhongCongNang.txtTen.Text = "";
            txtGhiChu.Text = "";
            slbTaiSan.txtMa.Text =
                   slbTaiSan.txtTen.Text =
                   txtQuanLy.Text =
                   txtSuDung.Text =
                   slbSoPhieu.txtMa.Text =
                   slbSoPhieu.txtTen.Text =
                   txtSoLuong.Text = "";
            pnlSearch.Enabled = false;
            btnSua.Enabled = false;
        }

        #region Load_Quyen (Load quyền sử dụng của tài khoản đã đăng nhập)
        /// <summary>
        /// Kiểm tra quyền sử dụng của tài khoản đã đăng nhập
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/04/23</Date>
        protected override void Load_Quyen()
        {
            base.Load_Quyen();
        }

        #endregion

        #region Load_CauHinh (Load cấu hình theo tài khoản đăng nhập)
        /// <summary>
        /// Load cấu hình theo tài khoản đăng nhập
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/04/23</Date>
        protected override void Load_CauHinh()
        {
            base.Load_CauHinh();
        }

        #endregion

        #region Thêm (Mặc định các giá trị khi thêm mới)
        /// <summary>
        /// Mặc định các giá trị khi thêm mới
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <date>2018/04/23</date>
        protected override void Them()
        {
            base.Them();
            pnlMain.Enabled = true;
            slbDonViQuanLy.txtMa.Text = "";
            slbDonViQuanLy.txtTen.Text = "";
            slbDonViSuDung.txtMa.Text = "";
            slbDonViSuDung.txtTen.Text = "";
            slbNguoiSuDung.txtMa.Text = "";
            slbNguoiSuDung.txtTen.Text = "";
            slbKhu.txtMa.Text = "";
            slbKhu.txtTen.Text = "";
            slbTang.txtMa.Text = "";
            slbTang.txtTen.Text = "";
            slbPhongCongNang.txtMa.Text = "";
            slbPhongCongNang.txtTen.Text = "";
            txtGhiChu.Text = "";
            slbTaiSan.txtMa.Text =
                   slbTaiSan.txtTen.Text =
                   txtQuanLy.Text =
                   txtSuDung.Text =
                   slbSoPhieu.txtMa.Text =
                   slbSoPhieu.txtTen.Text =
                   txtSoLuong.Text = "";
            btnSua.Enabled = false;
            txtConLai.Text = "";
            lblTong.Text = "0";
            _daLuu = false;
            Load_TaiSan();
            try
            {
                Load_DanhMuc();
                lblTong.Text = "0";
            }
            catch
            {
                lblTong.Text = "0";
            }
            _isAdd = true;
            slbSoPhieu.Enabled = true;
            pnlSearch.Enabled = true;
            slbSoPhieu.txtTen.Text = DateTime.Now.ToString("yyMMdd") + new Random().Next(0001, 9999);
            _lstBanGiao.Clear();
            _lstBanGiao2.Clear();
        }

        #endregion

        #region Sửa (Mặc định các giá trị khi sửa)
        /// <summary>
        /// Mặc định các giá trị khi sửa
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <date>2018/04/23</date>
        protected override void Sua()
        {
            base.Sua();
            _isAdd = false;
            _daLuu = false;
            btnSua.Enabled = false;
            pnlMain.Enabled = true;
            pnlSearch.Enabled = true;
            btnXoa.Enabled = false;
            dgvMain.Enabled = true;
            slbSoPhieu.Enabled = true;
        }

        #endregion

        #region Xóa (Thực hiện khi gọi sự kiện Xóa)
        /// <summary>
        /// Thực hiện khi gọi sự kiện xóa
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/04/16</Date>
        protected override void Xoa()
        {
            try
            {
                string lstID = "";
                string lstTen = "";

                DataTable dt = (DataTable)_bsBanGiao.DataSource;
                foreach (DataRow row in dt.Select("Chon = 1"))
                {
                    //sTen += "\n" + row[cls_SYS_DanhMuc.col_Ten].ToString();
                    //lstID += 
                }
                //DialogResult rsl = TA_MessageBox.MessageBox.Show("Bạn có chắc chắn muốn xóa dữ liệu:" + sTen, "Xác nhận", "Có~Không"
                //                        , TA_MessageBox.MessageButton.YesNo, TA_MessageBox.MessageIcon.Question);
                //if (true)
                //{

                //}
            }
            catch
            {
            }
            base.Xoa();
        }

        #endregion

        #region Check_Data (Kiểm tra dữ liệu trước khi cập nhật)
        /// <summary>
        /// Kiểm tra dữ liệu trước khi cập nhật
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <date>2018/04/23</date>
        protected override bool Check_Data()
        {
            if (String.IsNullOrEmpty(slbDonViQuanLy.txtMa.Text))
            {
                TA_MessageBox.MessageBox.Show("Vui lòng nhập đơn vị quản lý.", "Lỗi", "Đồng ý"
                    , TA_MessageBox.MessageButton.OK, TA_MessageBox.MessageIcon.Error);
                slbDonViQuanLy.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(slbDonViSuDung.txtMa.Text))
            {
                TA_MessageBox.MessageBox.Show("Vui lòng nhập đơn vị sử dụng.", "Lỗi", "Đồng ý"
                    , TA_MessageBox.MessageButton.OK, TA_MessageBox.MessageIcon.Error);
                slbDonViSuDung.Focus();
                return false;
            }
            if (dtpNgaySuDung.Value == null)
            {
                TA_MessageBox.MessageBox.Show("Vui lòng nhập ngày sử dụng.", "Lỗi", "Đồng ý"
                    , TA_MessageBox.MessageButton.OK, TA_MessageBox.MessageIcon.Error);
                dtpNgaySuDung.Focus();
                return false;
            }
            return true;
        }

        #endregion

        #region Luu (Cập nhật dữ liệu Insert/Update)
        /// <summary>
        /// Cập nhật dữ liệu Insert/Update
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <date>2018/04/23</date>
        protected override void Luu()
        {
            try
            {
                int count = 0;
                string loiCapNhatSuDung = "";
                if (slbSoPhieu.txtTen.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập số phiếu");
                    slbSoPhieu.txtMa.Select();
                    return;
                }

                if (Check_Data())
                {
                    _lstBanGiao2.Clear();
                    foreach (DataRowView view in _bsBanGiao)
                    {
                        view[cls_TaiSan_SuDungCT.col_MaKPQuanLy] = slbDonViQuanLy.txtMa.Text;
                        view[cls_TaiSan_SuDungCT.col_TenKPQuanLy] = slbDonViQuanLy.txtTen.Text;

                        view[cls_TaiSan_SuDungCT.col_MaKPSuDung] = slbDonViSuDung.txtMa.Text;
                        view[cls_TaiSan_SuDungCT.col_TenKPSuDung] = slbDonViSuDung.txtTen.Text;

                        view[cls_TaiSan_SuDungCT.col_MaNguoiSuDung] = slbNguoiSuDung.txtMa.Text;
                        view[cls_TaiSan_SuDungCT.col_TenNguoiSuDung] = slbNguoiSuDung.txtTen.Text;

                        view[cls_TaiSan_SuDungCT.col_MaKhu] = slbKhu.txtMa.Text;
                        view[cls_TaiSan_SuDungCT.col_TenKhu] = slbKhu.txtTen.Text;

                        view[cls_TaiSan_SuDungCT.col_MaTang] = slbTang.txtMa.Text;
                        view[cls_TaiSan_SuDungCT.col_TenTang] = slbTang.txtTen.Text;

                        view[cls_TaiSan_SuDungCT.col_MaPhongCongNang] = slbPhongCongNang.txtMa.Text;
                        view[cls_TaiSan_SuDungCT.col_TenPhongCongNang] = slbPhongCongNang.txtTen.Text;
                        view[cls_TaiSan_SuDungCT.col_SoPhieu] = slbSoPhieu.txtTen.Text;
                        _lstBanGiao2.Add(view[cls_TaiSan_SuDungCT.col_MaVach].ToString());
                        slbTaiSan.txtMa.Text = view[cls_TaiSan_SuDungCT.col_MaTaiSan].ToString();
                    }

                    DataRowView view2 = (DataRowView)_bsBanGiao.Current;
                    //public static string col_MaTrangThai = "MATRANGTHAI";
                    //public static string col_TenTrangThai = "TENTRANGTHAI";

                    if (_isAdd)
                    {

                        for (int i = 0; i < _lstBanGiao.Values.Count; i++)
                        {
                            if (_apiTaiSan.Insert_BanGiao(ref loiCapNhatSuDung, slbNguoiSuDung.txtMa.Text, dtpNgaySuDung.Value.ToString()
                                , _lstBanGiao.Keys[i], _lstBanGiao.Values[i], view2))
                            {
                                count++;
                            }
                        }
                        if (count > 0)
                        {

                            _daLuu = true;
                            base.Luu();
                            btnBoQua.Enabled = false;
                            btnSua.Enabled = true;
                            dgvMain.Enabled = false;
                            btnXoa.Enabled = false;
                        }
                    }
                    else
                    {
                        if (_apiTaiSan.Delete_SuDungCT(slbSoPhieu.txtTen.Text))
                        {
                            _apiTaiSan.Insert_BanGiao(ref loiCapNhatSuDung, slbNguoiSuDung.txtMa.Text, dtpNgaySuDung.Value.ToString()
                                , slbTaiSan.txtMa.Text, _lstBanGiao2.Count, view2);
                            _daLuu = true;
                            base.Luu();
                            btnBoQua.Enabled = false;
                            btnSua.Enabled = true;
                            dgvMain.Enabled = false;
                            btnXoa.Enabled = false;
                        }
                    }
                }
            }
            catch
            {
            }
        }

        #endregion

        #region TimKiem (Tìm kiếm dữ liệu)
        /// <summary>
        /// Tìm kiếm dữ liệu
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/04/24</Date>
        protected override void TimKiem()
        {
            if (string.IsNullOrEmpty(txtTimKiem.Text))
            {
                _bsBanGiao.Filter = "";
            }
            else
            {
                _bsBanGiao.Filter = string.Format("{0} like '%{2}%' OR {1} like'%{2}%' ", cls_SYS_DanhMuc.col_Ma, cls_SYS_DanhMuc.col_Ten, txtTimKiem.Text);
            }
            _count = _bsBanGiao.Count;
            base.TimKiem();
        }

        #endregion

        #region In mã vạch
        /// <summary>
        /// In mã vạch
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/05/09</Date>
        protected override void In()
        {
            try
            {
                if (!_daLuu)
                {
                    MessageBox.Show("Vui lòng bấm 'Cập nhật' trươc khi in.");
                    return;
                }
                DataSet ds = new DataSet();
                DataTable dtIn = ((DataTable)_bsBanGiao.DataSource).Select("Chon = true").CopyToDataTable();
                if (dtIn.Rows.Count > 0)
                {
                    ds.Tables.Add(dtIn);

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
                    ReportDocument rptDoc = new ReportDocument();
                    rptDoc.Load("..\\..\\..\\Report\\ts_mavach.rpt", OpenReportMethod.OpenReportByDefault);
                    rptDoc.SetDataSource(ds);

                    if (chkXemTruoc.Checked)
                    {
                        frm_Report frm = new frm_Report(rptDoc);
                        frm.ShowDialog();
                    }
                    else
                    {
                        rptDoc.PrintToPrinter(1, false, 1, dtIn.Rows.Count);
                        rptDoc.Dispose();
                        ds.Dispose();
                        rptDoc.Close();
                    }
                }
                else
                {
                    MessageBox.Show("In thất bại. Vui lòng kiểm tra lại kết nối máy in");
                }
            }
            catch
            {
                MessageBox.Show("In thất bại");
            }
            base.In();
        }

        #endregion

        #endregion

        #region Phương thức dùng riêng (Private)

        #region Load_DanhMuc (Load dữ liệu danh mục)
        /// <summary>
        /// Load dữ liệu danh mục
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/04/23</Date>
        private void Load_DanhMuc()
        {
            try
            {
                DataTable dtBanGiao = XL_BANG.Doc_cau_truc(_acc.Get_User() + "." + cls_TaiSan_SuDungCT.tb_TenBang);
                dtBanGiao.Columns.Add("Chon");
                _bsBanGiao.DataSource = dtBanGiao;
                dgvMain.DataSource = _bsBanGiao;
            }
            catch
            {
                _bsBanGiao.DataSource = null;
            }
            _count = _bsBanGiao.Count;
            base.TimKiem();
        }

        #endregion

        #region Load_DanhMuc (Load dữ liệu danh mục theo mã loại)
        /// <summary>
        /// Load dữ liệu danh mục theo mã loại
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/05/22</Date>
        /// <param name="maLoai">Mã loại dùng để load dữ liệu</param>
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

        #region Load_TaiSan (Load danh tài sản theo đơn vị quản lý)
        /// <summary>
        /// Load danh tài sản theo đơn vị quản lý
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/05/23</Date>
        private void Load_TaiSan()
        {
            _dtTaiSan = _apiTaiSan.Get_TaiSanSuDung(slbDonViQuanLy.txtMa.Text);
            slbTaiSan.DataSource = _dtTaiSan;
        }

        #endregion

        #region Load_KhoaPhongQuanLy (Load đơn vị quản lý)
        /// <summary>
        /// Load đơn vị quản lý
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/05/23</Date>
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
        /// <Date>2018/05/18</Date>
        private void Load_KhoaPhongSuDung()
        {
            List<string> lstCot = new List<string>();
            lstCot.Add(cls_LT_DanhMucBoPhan.col_MA);
            lstCot.Add(cls_LT_DanhMucBoPhan.col_TEN);

            slbDonViSuDung.DataSource = _api.Search(ref _userError, ref _systemError, cls_LT_DanhMucBoPhan.tb_TenBang, lst: lstCot);
        }

        #endregion

        #region Load_NhanVien (Load nhân viên theo đơn vị sử dụng)
        /// <summary>
        /// Load nhân viên theo đơn vị sử dụng
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/05/18</Date>
        /// <param name="maBoPhan">Mã đơn vị sử dụng</param>
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

        #endregion

        #region ThemChiTiet (Thêm chi tiết tài sản)
        /// <summary>
        /// Thêm chi tiết tài sản
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/05/18</Date>
        private void ThemChiTiet()
        {
            try
            {
                DataRowView view;
                int soLuong = int.Parse(txtSoLuong.Text);
                decimal soLuongCo = 0;
                decimal soLuongChon = 0;
                DataTable dtSuDungLL = null;
                try
                {
                    soLuongCo = (decimal)_dtTaiSan.Select(string.Format("{0} = '{1}'", cls_TaiSan_NhapCT.col_MaTaiSan
                                                              , slbTaiSan.txtMa.Text)).CopyToDataTable().Rows[0]["SoLuong"];
                    dtSuDungLL = XL_BANG.Doc(string.Format("Select * From {0}.{1} Where {2} = '{3}' AND {4} = 0 AND Rownum <= {5} ORDER BY {6} "
                   , _acc.Get_User(), cls_TaiSan_SuDungLL.tb_TenBang, cls_TaiSan_SuDungLL.col_MaTaiSan, slbTaiSan.txtMa.Text, cls_TaiSan_SuDungLL.col_SuDung, soLuong, cls_TaiSan_SuDungLL.col_ID));
                }
                catch
                {
                    soLuongCo = 0;
                }
                if (_lstBanGiao.Keys.Contains(slbTaiSan.txtMa.Text))
                {
                    soLuongChon = _lstBanGiao[slbTaiSan.txtMa.Text] + soLuong;
                }
                else
                {
                    soLuongChon = soLuong;
                }
                if (soLuongCo < soLuongChon)
                {
                    MessageBox.Show(string.Format("Số lượng tối đa {0},bạn đã nhập {1}, số lượng nhập thêm tối đa: {2} "
                        , soLuongCo, _lstBanGiao[slbTaiSan.txtMa.Text], soLuongCo - _lstBanGiao[slbTaiSan.txtMa.Text]));
                    return;
                }
                if (!_lstBanGiao.Keys.Contains(slbTaiSan.txtMa.Text))
                {
                    _lstBanGiao.Add(slbTaiSan.txtMa.Text, soLuong);
                }
                else
                {
                    _lstBanGiao[slbTaiSan.txtMa.Text] += soLuong;
                }

                for (int i = 0; i < soLuong; i++)
                {
                    _bsBanGiao.AddNew();
                    view = (DataRowView)_bsBanGiao.Current;
                    view = (DataRowView)_bsBanGiao.Current;
                    view[cls_TaiSan_SuDungCT.col_Ma] = _acc.Get_Ma() + i.ToString().PadLeft(2, '0');
                    view[cls_TaiSan_SuDungCT.col_MaVach] = dtSuDungLL.Rows[i][cls_TaiSan_SuDungLL.col_MaVach];
                    view[cls_TaiSan_SuDungCT.col_MaTaiSan] = slbTaiSan.txtMa.Text;
                    view[cls_TaiSan_SuDungCT.col_TenTaiSan] = slbTaiSan.txtTen.Text;
                    view[cls_TaiSan_SuDungCT.col_MaSuDungLL] = slbTaiSan.txtMa.Text;
                    view[cls_TaiSan_SuDungCT.col_KyHieu] = dtSuDungLL.Rows[i][cls_TaiSan_SuDungLL.col_KyHieu]; ;
                    view["Chon"] = true;
                    _bsBanGiao.EndEdit();
                }
                DataRow rowTaiSan = _dtTaiSan.Select(string.Format("{0} = '{1}'", cls_TaiSan_SuDungLL.col_MaTaiSan, slbTaiSan.txtMa.Text)).CopyToDataTable().Rows[0];
                rowTaiSan["SoLuong"] = decimal.Parse(rowTaiSan["SoLuong"].ToString()) - decimal.Parse(txtSoLuong.Text);

                slbTaiSan.DataSource = _dtTaiSan;
                // _bsDanhMuc.Sort = string.Format("{0} DESC", cls_TaiSan_SuDungLL.col_Ma);
                _bsBanGiao.EndEdit();
                _bsBanGiao.Position = 0;

                slbTaiSan.txtMa.Text =
                    slbTaiSan.txtTen.Text =
                    txtQuanLy.Text =
                     txtSuDung.Text =
                    txtConLai.Text =
                    txtSoLuong.Text = "";
                slbTaiSan.txtTen.Focus();
                lblTong.Text = _bsBanGiao.Count.ToString();
            }
            catch //(Exception ex)
            {
                //Console.Write(ex);
            }
        }

        #endregion

        #region Load tài sản theo đơn vị quản lý
        private void Load_TaiSanTheoSoPhieu()
        {
            try
            {
                dgvMain.DataSource = null;
                DataTable dt = _apiTaiSan.Get_TaiSanTheoSoPhieuCT(slbSoPhieu.txtTen.Text);
                dt.Columns.Add("Chon");

                if (dt.Rows[0]["makpquanly"].ToString() == slbDonViQuanLy.txtMa.Text)
                {
                    _bsBanGiao.DataSource = dt;
                    dgvMain.DataSource = _bsBanGiao;
                }
            }
            catch { }
        }
        #endregion

        #endregion

        #endregion

        #region Sự kiện

        #region Sự kiện load form

        private void frm_BanGiao_Load(object sender, EventArgs e)
        {
            slbKhu.Enabled = false;
            slbTang.Enabled = false;
            slbPhongCongNang.Enabled = false;
            btnSua.Enabled = false;
        }

        #endregion

        #region Sự kiện chọn tất cả

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataRowView view in _bsBanGiao)
            {
                view["Chon"] = chkAll.Checked;
            }
            _bsBanGiao.EndEdit();
        }

        #endregion

        #region Sự kiện chọn sửa/xóa

        private void dgvMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvMain.RowCount > 0 && e.RowIndex > -1)
            {
                try
                {
                    if (e.ColumnIndex == 6)
                    {
                        DialogResult rsl = TA_MessageBox.MessageBox.Show("Bạn có chắc chắn xóa dòng đang chọn không?", "Xác nhận", "Có~Không~0"
                          , TA_MessageBox.MessageButton.YesNo, TA_MessageBox.MessageIcon.Question);
                        if (rsl == System.Windows.Forms.DialogResult.Yes)
                        {
                            //_lstBanGiao[((DataRowView)_bsBanGiao.Current)[cls_TaiSan_SuDungCT.col_MaVach].ToString()] -= 1;
                            _lstMaVachDelete.Add(((DataRowView)_bsBanGiao.Current)[cls_TaiSan_SuDungCT.col_MaVach].ToString());
                            _bsBanGiao.Remove(_bsBanGiao.Current);
                            lblTong.Text = _bsBanGiao.Count.ToString();
                        }

                    }
                }
                catch
                {
                }
            }
        }

        #endregion

        #region Sự kiện chọn dòng

        private void dgvMain_SelectionChanged(object sender, EventArgs e)
        {
            //Show_ChiTiet();
        }

        #endregion

        #region Sự kiện bỏ qua dữ liệu lỗi khi load form

        private void dgvMain_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        #endregion

        #region Sự kiện thêm chi tiết

        private void btnThemCT_Click(object sender, EventArgs e)
        {
            ThemChiTiet();
        }

        #endregion

        #region Sự kiện xóa danh sách

        private void btnXoaDanhSach_Click(object sender, EventArgs e)
        {
            _bsBanGiao.Clear();
        }

        #endregion

        #region Sự kiện Enter tại ô số phiếu

        private void txtSoPhieu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectNextControl(this, true, true, true, true);
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

        #region Sự kiện mở danh mục khu

        private void lblKhu_Click(object sender, EventArgs e)
        {
            var frm = new frm_DanhMucChung(cls_sys_LoaiDanhMuc.khu_MaLoai, cls_sys_LoaiDanhMuc.khu_TenLoai, "", "", "", "");

            frm.ShowDialog();
            slbKhu.DataSource = Load_DanhMuc(cls_sys_LoaiDanhMuc.khu_MaLoai, "-1");
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
            var frm = new frm_DanhMucChung(cls_sys_LoaiDanhMuc.phong_MaLoai, cls_sys_LoaiDanhMuc.phong_TenLoai, cls_sys_LoaiDanhMuc.tang_MaLoai
                , cls_sys_LoaiDanhMuc.tang_TenLoai, slbTang.txtMa.Text, slbTang.txtTen.Text);
            frm.ShowDialog();
            slbPhongCongNang.DataSource = Load_DanhMuc(cls_sys_LoaiDanhMuc.phong_MaLoai, slbTang.txtMa.Text);
        }

        #endregion

        #region Sự kiện chọn người sử dụng

        private void slbNguoiSuDung_HisValueChange(object sender, EventArgs e)
        {
        }

        #endregion

        #region Sự kiện chọn đơn vị sử dụng

        private void slbDonViSuDung_HisSelectChange(object sender, EventArgs e)
        {
            Load_NhanVien(slbDonViSuDung.txtMa.Text);
            if (!string.IsNullOrEmpty(slbDonViSuDung.txtMa.Text))
            {
                slbKhu.Enabled = true;
                slbTang.Enabled = true;
                slbPhongCongNang.Enabled = true;
            }
            else
            {
                slbKhu.Enabled = false;
                slbTang.Enabled = false;
                slbPhongCongNang.Enabled = false;
            }
        }

        #endregion

        #region Sự kiện chọn đơn vị quản lý

        private void slbDonViQuanLy_HisSelectChange(object sender, EventArgs e)
        {
            Load_TaiSan();
            if (slbDonViQuanLy.txtMa.Text != "" && slbTaiSan.txtMa.Text != "")
            {
                txtQuanLy.Text = _apiTaiSan.Get_SoLuongQuanLy(slbDonViQuanLy.txtMa.Text, slbTaiSan.txtMa.Text);
                txtSuDung.Text = _apiTaiSan.Get_SoLuongSuDung(slbDonViSuDung.txtMa.Text, slbTaiSan.txtMa.Text);
                try
                {
                    txtConLai.Text = _dtTaiSan.Select(string.Format("{0} = '{1}'", cls_TaiSan_SuDungLL.col_MaTaiSan, slbTaiSan.txtMa.Text)).CopyToDataTable().Rows[0]["SoLuong"].ToString();
                    txtSoLuong.Focus();
                }
                catch
                {
                    txtConLai.Text = "0";
                }
            }
            if (dgvMain.Rows.Count > 0 && !_daLuu)
            {
                //    DialogResult dialog = MessageBox.Show("Bạn có muốn bỏ qua danh sách tài sản hiện tại không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                //    if (dialog == DialogResult.OK)
                //    {
                Them();
                //    }
                //    else
                //    {

                //    }
                //}
                //else
                //{
                //    Load_TaiSan();
                //    if (_isAdd == false)
                //    {
                //        slbSoPhieu.DataSource = _apiTaiSan.Get_SoPhieuSuDung(slbDonViQuanLy.txtMa.Text);
                //    }
                //    slbTaiSan.txtMa.Text =
                //    slbTaiSan.txtTen.Text =
                //    txtQuanLy.Text =
                //    txtSuDung.Text =
                //    slbSoPhieu.txtMa.Text =
                //    slbSoPhieu.txtTen.Text =
                //    txtConLai.Text =
                //    txtSoLuong.Text = "";

                //    Load_DanhMuc();
            }
        }

        #endregion

        #region Sự kiện chọn khu

        private void slbKhu_HisValueChange(object sender, EventArgs e)
        {
            slbTang.DataSource = Load_DanhMuc(cls_sys_LoaiDanhMuc.tang_MaLoai, slbKhu.txtMa.Text);
        }

        #endregion

        #region Sự kiện chọn tầng

        private void slbTang_HisValueChange(object sender, EventArgs e)
        {
            slbPhongCongNang.DataSource = Load_DanhMuc(cls_sys_LoaiDanhMuc.phong_MaLoai, slbTang.txtMa.Text);
        }

        #endregion

        #region Sự kiện chọn tài sản

        private void slbTaiSan_HisSelectChange(object sender, EventArgs e)
        {
            if (slbDonViQuanLy.txtMa.Text == "")
            {
                try
                {
                    slbDonViQuanLy.SetTenByMa(_acc.Get_Data(string.Format("Select {0} from {1}.{2} where {3}='{4}'"
                        , cls_TaiSan_DanhMuc.col_MaKPQuanLy, _acc.Get_User(), cls_TaiSan_DanhMuc.tb_TenBang, cls_TaiSan_DanhMuc.col_Ma
                        , slbTaiSan.txtMa.Text)).Rows[0][0] + "");
                }
                catch (Exception)
                {
                }
            }
            txtQuanLy.Text = _apiTaiSan.Get_SoLuongQuanLy(slbDonViQuanLy.txtMa.Text, slbTaiSan.txtMa.Text);
            txtSuDung.Text = _apiTaiSan.Get_SoLuongSuDung(slbDonViSuDung.txtMa.Text, slbTaiSan.txtMa.Text);
            try
            {
                txtConLai.Text = _dtTaiSan.Select(string.Format("{0} = '{1}'", cls_TaiSan_SuDungLL.col_MaTaiSan, slbTaiSan.txtMa.Text)).CopyToDataTable().Rows[0]["SoLuong"].ToString();
                txtSoLuong.Focus();
            }
            catch
            {
                txtConLai.Text = "0";
            }
        }

        #endregion

        #region Sự kiện nhập số lương (chỉ cho phép nhập số)

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        #endregion

        #region Sự kiện Enter tại ô số lượng

        private void txtSoLuong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnThemCT.Focus();
            }
        }

        #endregion

        private void slbSoPhieu_HisSelectChange(object sender, EventArgs e)
        {
            if (slbSoPhieu.txtTen.Text != "") { Load_TaiSanTheoSoPhieu(); } else { Load_KhoaPhongQuanLy(); }
            slbSoPhieu.Enabled = false;
        }

        private void slbSoPhieu_HisTenKeyDown(object sender, KeyEventArgs e)
        {
            if (slbSoPhieu.txtTen.TextLength == 20)
            {
                TA_MessageBox.MessageBox.Show("Số phiếu tối đa 20 ký tự.", "Thông báo", "Đồng ý"
                    , TA_MessageBox.MessageButton.OK, TA_MessageBox.MessageIcon.Error);
            }
        }

        private void btnInBanGiao_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_daLuu)
                {
                    MessageBox.Show("Vui lòng bấm 'Cập nhật' trươc khi in.");
                    return;
                }
                DataSet ds = new DataSet();
                DataTable dtIn = new DataTable();
                dtIn.Columns.Add(cls_TaiSan_SuDungLL.col_ID);
                dtIn.Columns.Add(cls_TaiSan_SuDungLL.col_TenKPQuanLy);
                dtIn.Columns.Add(cls_TaiSan_SuDungLL.col_TenKPSuDung);
                dtIn.Columns.Add(cls_TaiSan_SuDungLL.col_TenNguoiSuDung);
                dtIn.Columns.Add(cls_TaiSan_SuDungLL.col_TenPhongCongNang);
                dtIn.Columns.Add(cls_TaiSan_SuDungLL.col_TenKhu);
                dtIn.Columns.Add(cls_TaiSan_SuDungLL.col_TenTang);
                dtIn.Columns.Add(cls_TaiSan_SuDungLL.col_SoPhieu);
                DataRow drN = dtIn.NewRow();
                drN[cls_TaiSan_SuDungLL.col_ID] = "1";
                drN[cls_TaiSan_SuDungLL.col_TenKPQuanLy] = slbDonViQuanLy.txtTen.Text;
                drN[cls_TaiSan_SuDungLL.col_TenKPSuDung] = slbDonViSuDung.txtTen.Text;
                drN[cls_TaiSan_SuDungLL.col_TenNguoiSuDung] = slbNguoiSuDung.txtTen.Text;
                drN[cls_TaiSan_SuDungLL.col_TenPhongCongNang] = slbPhongCongNang.txtTen.Text;
                drN[cls_TaiSan_SuDungLL.col_TenTang] = slbTang.txtTen.Text;
                drN[cls_TaiSan_SuDungLL.col_TenKhu] = slbKhu.txtTen.Text;
                drN[cls_TaiSan_SuDungLL.col_SoPhieu] = slbSoPhieu.txtTen.Text;
                dtIn.Rows.Add(drN);

                DataTable dtIn2 = ((DataTable)_bsBanGiao.DataSource).Select().CopyToDataTable();
                dtIn2.Columns.Remove(cls_TaiSan_SuDungLL.col_TenKPQuanLy);
                dtIn2.Columns.Remove(cls_TaiSan_SuDungLL.col_TenKPSuDung);
                dtIn2.Columns.Remove(cls_TaiSan_SuDungLL.col_TenNguoiSuDung);
                dtIn2.Columns.Remove(cls_TaiSan_SuDungLL.col_TenPhongCongNang);
                dtIn2.Columns.Remove(cls_TaiSan_SuDungLL.col_TenTang);
                dtIn2.Columns.Remove(cls_TaiSan_SuDungLL.col_TenKhu);
                dtIn2.Columns.Remove(cls_TaiSan_SuDungLL.col_SoPhieu);
                dtIn.TableName = "DonViQuanLy";
                dtIn2.TableName = "TaiSan";
                if (dtIn.Rows.Count > 0)
                {
                    ds.Tables.Add(dtIn);
                    ds.Tables.Add(dtIn2);
                    if (!System.IO.Directory.Exists("..//xml"))
                    {
                        System.IO.Directory.CreateDirectory("..//xml");
                    }
                    ds.WriteXml("..//xml//InBanGiao.xml", XmlWriteMode.WriteSchema);
                    if (!System.IO.File.Exists("..\\..\\..\\Report\\InBanGiao.rpt"))
                    {
                        MessageBox.Show("Thiếu file: InBanGiao.rpt trong thư mục Report.");
                        return;
                    }
                    ReportDocument rptDoc = new ReportDocument();
                    rptDoc.Load("..\\..\\..\\Report\\InBanGiao.rpt", OpenReportMethod.OpenReportByDefault);
                    rptDoc.SetDataSource(ds);
                    rptDoc.PrintToPrinter(1, false, 1, dtIn.Rows.Count);
                    rptDoc.Dispose();
                    ds.Dispose();
                    rptDoc.Close();
                }
                else
                {
                    MessageBox.Show("In thất bại. Vui lòng kiểm tra lại kết nối máy in");
                }
            }
            catch
            {
                MessageBox.Show("In thất bại");
            }

        }

        #endregion

    }
}
