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
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using DevComponents.DotNetBar.Controls;
using E00_SafeCacheDataService.Interface;
using E00_API.Contract.TaiSan;
using E00_Helpers.Helpers;
using E00_SafeCacheDataService.Common;
using E00_SafeCacheDataService.Base;
using E00_API.TaiSan;

namespace QuanLyTaiSan
{
    public partial class frm_BanGiaoTaiSan : frm_DanhMuc
    {
        private api_TaiSanSuDungLL _detailService;
        private IAPI<TaiSan_BanGiaoLLInfo> _masterService;
        private TaiSan_BanGiaoLLInfo _masterInfo;
        private TaiSanSuDungLLInfo _detailInfo;
        private bool _isEdit = false;
        private ResultInfo _resultInfo;

        #region Biến toàn cục

        public Acc_Oracle _acc = new Acc_Oracle();

        private Api_Common _api = new Api_Common();
        private E00_API.api_TaiSan _apiTaiSan = new E00_API.api_TaiSan();
        private string _maNhom = "";
        private string _tenNhom = "";
        private bool _isAdd = true;
        private string _userError = string.Empty;
        private string _systemError = string.Empty;
        private BindingSource _bsBanGiao = new BindingSource();
        private Dictionary<string, string> _dicNhapLL = new Dictionary<string, string>();
        private Dictionary<string, string> _dicNhapCT = new Dictionary<string, string>();
        private List<string> _lstKey = new List<string>();
        private List<string> _lstUnique = new List<string>();
        private Dictionary<string, string> _dicWhere = new Dictionary<string, string>();
        private DataTable _dtTaiSan = new DataTable();
        private SortedList<string, decimal> _lstSuDung = new SortedList<string, decimal>();
        //private List<string> _lstMaTaiSan = new List<string>();
        private List<string> _lstMaVach = new List<string>();
        private List<string> _lstMaVachCu = new List<string>();
        //private List<string> _lstMaSuDung = new List<string>();
        //private List<string> _lstMaVachDelete = new List<string>();

        private bool _daLuu = false;

        #endregion

        #region Khởi tạo

        #region Khởi tạo chung

        public frm_BanGiaoTaiSan()
        {
            InitializeComponent();
            this.btnTienIch.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.chkLuuThongTinBanGiao,
            this.btnInBienBanBanGiao});
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.pnlMain.Controls.Add(this.chkAll);
            this.pnlMain.Controls.Add(this.lblTong);
            this.pnlMain.Controls.Add(this.dgvMain);
            btnIn.Enabled = true;
            this.dgvMain.Dock = DockStyle.Fill;
            _api.KetNoi();

            E00_Helpers.Helpers.Helper.ControlEventEnter(new Control[] { txtSoPhieu, dtpNgayNhan, slbBanGiamDoc, txtChucVuBGD, slbDonViQuanLy, slbDonViSuDung,
            slbBanGiao1,slbBenNhan1,txtChucVuBenGiao1,txtChucVuBenNhan1,slbBanGiao2,slbBenNhan2,txtChucVuBenGiao2,txtChucVuBenNhan2,txtGhichu,txtDiaDiemBanGiao,
            slbTaiSan,slbKhu,slbTang,slbPhongCongNang,slbNguoiSuDung});
            btnChecNumber.Click += (send, e) => CheckNumber();
            if (cls_System.sys_UserID == "")
            {
                cls_System.sys_UserID = "1";
            }
        }

        #endregion

        #region Khởi tạo sử dụng dữ liệu theo nhóm
        /// <summary>
        /// Khởi tạo sử dụng dữ liệu theo nhóm
        /// </summary>
        /// <param name="maNhom">Mã nhóm để lấy dữ liệu</param>
        /// <param name="tenNhom">Tên hiển thị trên textForm</param>
        public frm_BanGiaoTaiSan(string maNhom, string tenNhom)
        {
            InitializeComponent();
            this.btnTienIch.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.chkLuuThongTinBanGiao,
            this.btnInBienBanBanGiao});
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.pnlMain.Controls.Add(this.chkAll);
            this.pnlMain.Controls.Add(this.lblTong);
            this.pnlMain.Controls.Add(this.dgvMain);
            this.dgvMain.Dock = DockStyle.Fill;
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
            _masterService = new API_Common<TaiSan_BanGiaoLLInfo>();
            _detailService = new api_TaiSanSuDungLL();

            //Load_DanhMuc();
            Load_KhoaPhongQuanLy();
            Load_KhoaPhongSuDung();
            Load_TaiSan();
            Load_NhanVien();
            Load_SoPhieu();
            base.LoadData();

            pnlControl2.Enabled = true;
            slbSoPhieu.txtTen.MaxLength = 20;
            btnXoa.Enabled = false;
            pnlSearch.Enabled = true;
            EndEditMode();
        }

        #endregion

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
            ClearControl();
            EnterEditMode();

            dtpNgayNhan.Value = DateTime.Now;
            txtSoPhieu.Text = _apiTaiSan.GetHandOverNumberMax();
            dgvMain.DataSource = null;

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
            if (string.IsNullOrEmpty(txtMaPhieu.Text))
            {
                TA_MessageBox.MessageBox.Show("Vui lòng chọn phiếu cần sửa.", TA_MessageBox.MessageIcon.Information);
                return;
            }
            base.Sua();
            _isAdd = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            pnlMain.Enabled = true;
            //btnLuu.Enabled = false;
            EnterEditMode();
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
                DataTable dt = (DataTable)_bsBanGiao.DataSource;
                if(dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        TA_MessageBox.MessageBox.Show("Vui lòng xóa chi tiết trước khi xóa phiếu.");
                        return;
                    }
                }
                if (TA_MessageBox.MessageBox.Show("Bạn có chắc chắn muốn xóa phiếu bàn giao: " + txtSoPhieu.Text, TA_MessageBox.MessageIcon.Question) == DialogResult.Yes)
                {
                    _resultInfo = _masterService.Delete(txtMaPhieu.Text);
                    if (!_resultInfo.Status)
                    {
                        TA_MessageBox.MessageBox.Show("Xóa không thành công:" + _resultInfo.SystemError, TA_MessageBox.MessageIcon.Error);
                        return;
                    }
                    Load_SoPhieu();
                    ClearControl();
                    ClearDetail();
                }
                
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
            if (String.IsNullOrEmpty(txtSoPhieu.Text))
            {
                TA_MessageBox.MessageBox.Show("Vui lòng nhập số phiếu.", TA_MessageBox.MessageIcon.Error);
                txtSoPhieu.Focus();
                return false;
            }

            if (dtpNgayNhan.Value == DateTime.MinValue)
            {
                TA_MessageBox.MessageBox.Show("Vui lòng nhập ngày bàn giao.", TA_MessageBox.MessageIcon.Error);
                dtpNgayNhan.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(slbDonViQuanLy.txtMa.Text))
            {
                TA_MessageBox.MessageBox.Show("Vui lòng nhập đơn vị quản lý.",  TA_MessageBox.MessageIcon.Error);
                slbDonViQuanLy.txtTen.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(slbDonViSuDung.txtMa.Text))
            {
                TA_MessageBox.MessageBox.Show("Vui lòng nhập đơn vị sử dụng.", TA_MessageBox.MessageIcon.Error);
                slbDonViSuDung.txtTen.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(slbBanGiao1.txtMa.Text))
            {
                TA_MessageBox.MessageBox.Show("Vui lòng nhập đại diện người giao tài sản.", TA_MessageBox.MessageIcon.Error);
                slbBanGiao1.txtTen.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(txtChucVuBenGiao1.Text))
            {
                TA_MessageBox.MessageBox.Show("Vui lòng nhập chức vụ người giao tài sản.", TA_MessageBox.MessageIcon.Error);
                txtChucVuBenGiao1.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(slbBenNhan1.txtMa.Text))
            {
                TA_MessageBox.MessageBox.Show("Vui lòng nhập đại diện người nhận tài sản.", TA_MessageBox.MessageIcon.Error);
                slbBenNhan1.txtTen.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(txtChucVuBenNhan1.Text))
            {
                TA_MessageBox.MessageBox.Show("Vui lòng nhập chức vụ người nhận tài sản.", TA_MessageBox.MessageIcon.Error);
                txtChucVuBenNhan1.Focus();
                return false;
            }
            _masterInfo = GetInfomationMaster();
            
            if (_masterInfo.BGID == 0)
            {
                var rowCheck = _masterService.Get_Item($"{cls_TaiSan_BanGiaoLL.col_SoChungTu} = '{txtSoPhieu.Text}'");
                if (rowCheck != null)
                {
                    TA_MessageBox.MessageBox.Show("Số phiếu đã tồn tại vui lòng nhập số khác.", TA_MessageBox.MessageIcon.Error);
                    txtSoPhieu.Focus();
                    return false;
                }
            }
            if (chkTrangThai.Value)
            {
                DataTable tmpTable = _detailService.Get_Data($"{cls_TaiSan_SuDungLL.col_BanGiaoID} = {_masterInfo.BGID}");
                if (tmpTable == null)
                {
                    TA_MessageBox.MessageBox.Show("Vui lòng thêm chi tiết trước khi hoàn thành phiếu.", TA_MessageBox.MessageIcon.Information);
                    return false;
                }
                if (tmpTable.Rows.Count == 0)
                {
                    TA_MessageBox.MessageBox.Show("Vui lòng thêm chi tiết trước khi hoàn thành phiếu.", TA_MessageBox.MessageIcon.Information);
                    return false;
                }
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
                if (Check_Data())
                {
                    if(_masterInfo.BGID == 0)
                    {
                        _masterInfo.BGID = _masterService.CreateNewID(20);
                        _masterInfo.MACHINEID = _masterService.GetMachineID();
                        _resultInfo = _masterService.Insert(_masterInfo);
                        txtMaPhieu.Text = ""+_masterInfo.BGID;
                        if (!_resultInfo.Status)
                        {
                            TA_MessageBox.MessageBox.Show("Cập nhật không thành công." + _resultInfo.SystemError, TA_MessageBox.MessageIcon.Error);
                        }
                   
                    }
                    else
                    {
                        _resultInfo = _masterService.Update(_masterInfo);
                        if (!_resultInfo.Status)
                        {
                            TA_MessageBox.MessageBox.Show("Cập nhật không thành công." + _resultInfo.SystemError, TA_MessageBox.MessageIcon.Error);
                        }

                    }
                    base.Luu();
                    BoQua();
                    Load_SoPhieu();
                    btnSua.Enabled = true;
                }
            }
            catch
            {
            }
        }

        #endregion

        #region BoQua (Bỏ qua các dữ liệu đã nhập)
        /// <summary>
        /// Bỏ qua các dữ liệu đã nhập
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/05/24</Date>
        protected override void BoQua()
        {
            base.BoQua();
            pnlControl2.Enabled = true;
            EndEditMode();
            ClearControl();

            if (!string.IsNullOrEmpty(slbSoPhieu.txtMa.Text))
            {
                FillToControl(Helper.ConvertSToDec(slbSoPhieu.txtMa.Text));
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
            else
            {
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
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
        /// <Date>2018/05/04</Date>
        protected override void In()
        {
            try
            {
                //if (!_daLuu)
                //{
                //    MessageBox.Show("Vui lòng bấm 'Cập nhật' trươc khi in.");
                //    return;
                //}
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
            catch(Exception ex)
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
                //_bsDanhMuc.DataSource = XL_BANG.Doc_cau_truc(_acc.Get_UserMMYY() + "." + cls_TaiSan_SuDungLL.tb_TenBang);
                DataTable dt = XL_BANG.Doc_cau_truc(_acc.Get_User() + "." + cls_TaiSan_SuDungLL.tb_TenBang);
                dt.Columns.Add("Chon");
                _bsBanGiao.DataSource = dt;
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

        #region Load_TaiSan Load tài sản đã nhập
        /// <summary>
        /// Load tài sản đã nhập
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/05/03</Date>
        private void Load_TaiSan()
        {
            _dtTaiSan = _apiTaiSan.Get_StockOnhand();

            slbTaiSan.ColumDataList = new string[] { "SPID", "TENTAISAN", "MAVACH", "VITRI", "TINHTRANG", "HANBAOHANH" };
            slbTaiSan.ColumWidthList = new int[] { 120, 150, 150, 100, 100, 120 };
            slbTaiSan.ValueMember = "SPID";
            slbTaiSan.DisplayMember = "TENTAISAN";
            slbTaiSan.DataSource = _dtTaiSan;
        }

        #endregion

        #region Load_DanhMuc (Load dữ liệu danh mục theo mã loại và mã nhóm loại)
        /// <summary>
        /// Load dữ liệu danh mục theo mã loại và mã nhóm loại
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/05/24</Date>
        /// <param name="maLoai">Mã loại load dữ liệu</param>
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
        /// <Date>2018/05/18</Date>
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
        /// <Date>2018/05/22</Date>
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
        /// <Date>2018/05/22</Date>
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

        private void Load_NhanVien()
        {
            try
            {
                List<string> lstCot = new List<string>();
                lstCot.Add(cls_NhanSu_LiLichNhanVien.col_MaNhanVien);
                lstCot.Add(cls_NhanSu_LiLichNhanVien.col_HoTen);

                DataTable dtNhanVien = _api.Search(ref _userError, ref _systemError, cls_NhanSu_LiLichNhanVien.tb_TenBang, lst: lstCot);
                slbBanGiamDoc.DataSource = dtNhanVien.Copy();
                slbBanGiao1.DataSource = dtNhanVien.Copy();
                slbBanGiao2.DataSource = dtNhanVien.Copy();
                slbBenNhan1.DataSource = dtNhanVien.Copy();
                slbBenNhan2.DataSource = dtNhanVien.Copy();
            }
            catch
            {
                slbBanGiamDoc.DataSource =
                slbBanGiao1.DataSource =
                slbBanGiao2.DataSource =
                slbBenNhan1.DataSource =
                slbBenNhan2.DataSource = null;
            }
        }

        #endregion

        #region ThemChiTiet Thêm chi tiết tài sản tạo mã vạch
        /// <summary>
        /// Thêm chi tiết tài sản tạo mã vạch
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/05/03</Date>
        private void ThemChiTiet()
        {
            #region Check

            decimal maxterID = Helper.ConvertSToDec("0" + txtMaPhieu.Text);
            decimal maTaiSan = Helper.ConvertSToDec("0" + slbTaiSan.txtMa.Text);
            if (maxterID == 0)
            {
                TA_MessageBox.MessageBox.Show("Phiếu bàn giao chưa được tại, vui lòng tạo trước khi thêm tài sản.", TA_MessageBox.MessageIcon.Error);
                return;
            }
            if(maTaiSan == 0)
            {
                TA_MessageBox.MessageBox.Show("Vui lòng chọn tài sản.", TA_MessageBox.MessageIcon.Error);
                return;
            }
            DataRow row = _apiTaiSan.Get_TaiSanTheoMa(Helper.ConvertSToDec("0"+ slbTaiSan.txtMa.Text));
            #endregion
            if(row != null)
            {
                _detailInfo = new TaiSanSuDungLLInfo();
                _detailInfo.MANHAPCT =""+ maTaiSan;
                _detailInfo.MANGUOISUDUNG = slbNguoiSuDung.txtMa.Text;
                _detailInfo.TENNGUOISUDUNG = slbNguoiSuDung.txtTen.Text;
                _detailInfo.MAKHU = slbKhu.txtMa.Text;
                _detailInfo.TENKHU = slbKhu.txtTen.Text;
                _detailInfo.MATANG = slbTang.txtMa.Text;
                _detailInfo.TENTANG = slbTang.txtTen.Text;
                _detailInfo.TENKHU = slbTang.txtTen.Text;
                _detailInfo.NGAYSUDUNG = dtpNgayNhan.Value;
                _detailInfo.SUDUNG = 1;
                _detailInfo.MAVACH = "" + row[cls_TaiSan_NhapCTSP.col_MaVach];
                _detailInfo.MATAISAN =""+ row["MATAISAN"];
                _detailInfo.TENTAISAN = "" + row["TENTAISAN"];
                _detailInfo.MAKPQUANLY = slbDonViQuanLy.txtMa.Text;
                _detailInfo.TENKPQUANLY = slbDonViQuanLy.txtTen.Text;
                _detailInfo.MAKPSUDUNG = slbDonViSuDung.txtMa.Text;
                _detailInfo.TENKPSUDUNG = slbDonViSuDung.txtTen.Text;
                _detailInfo.MAPHONGCONGNANG = slbPhongCongNang.txtMa.Text;
                _detailInfo.TENPHONGCONGNANG = slbPhongCongNang.txtTen.Text;
                _detailInfo.SOPHIEU = txtSoPhieu.Text;
                _detailInfo.BanGiaoID = maxterID;
                _detailInfo.NhapCTID =  Helper.ConvertSToDec(row[cls_TaiSan_NhapCTSP.col_NhapCTID]);
                _detailInfo.TRANGTHAI = "" + row[cls_TaiSan_NhapCTSP.col_TinhTrang];
                if (Helper.ConvertSToDec(_detailInfo.MA) == 0)
                {
                    _detailInfo.MA = ""+_detailService.CreateNewID(20);
                    _detailInfo.MACHINEID = _detailService.GetMachineID();
                    _detailInfo.ID = _detailService.GetMaxID();
                }
                _resultInfo = _detailService.InsertOrUpdate(_detailInfo);
                if (!_resultInfo.Status)
                {
                    TA_MessageBox.MessageBox.Show("", TA_MessageBox.MessageIcon.Error);
                    return;
                }
                Load_TaiSan();
                ClearDetail();
            }
        }

        #endregion

        #endregion

        #region Load_SoPhieu(load dữ liệu số phiếu)
        /// <summary>
        /// Load dữ liệu selectbox số phiếu
        /// </summary>
        /// <author>Long94</author>
        /// <Date>2018/06/01</Date>
        /// <returns></returns>
        private void Load_SoPhieu()
        {
                slbSoPhieu.DataSource = _apiTaiSan.Get_DSBanGiaoChuaXong();
        }
        #endregion

        /// <summary>
        /// Load dữ liệu dgv tài sản theo đơn vị quản lý và đơn vị sử dụng
        /// </summary>
        /// <author>Long94</author>
        /// <Date>2018/06/04</Date>

        #region InBienBanBanGiao (In biên bản kiểm kê)

        private void InBienBanBanGiao()
        {
            try
            {
                //if (!_daLuu)
                //{
                //    MessageBox.Show("Vui lòng Cập nhật trươc khi in.");
                //    return;
                //}
                DataSet ds = new DataSet();

                #region Xuất XML thông tin bàn giao

                DataTable dtBanGiao = new DataTable();



                dtBanGiao.Columns.Add(cls_sys_LoaiDanhMuc.maBanGiamDoc);
                dtBanGiao.Columns.Add(cls_sys_LoaiDanhMuc.tenBanGiamDoc);
                dtBanGiao.Columns.Add(cls_sys_LoaiDanhMuc.chucVuBanGiamDoc);

                dtBanGiao.Columns.Add(cls_sys_LoaiDanhMuc.maBenGiao1);
                dtBanGiao.Columns.Add(cls_sys_LoaiDanhMuc.tenBenGiao1);
                dtBanGiao.Columns.Add(cls_sys_LoaiDanhMuc.chucVuBenGiao1);

                dtBanGiao.Columns.Add(cls_sys_LoaiDanhMuc.maBenGiao2);
                dtBanGiao.Columns.Add(cls_sys_LoaiDanhMuc.tenBenGiao2);
                dtBanGiao.Columns.Add(cls_sys_LoaiDanhMuc.chucVuBenGiao2);

                dtBanGiao.Columns.Add(cls_sys_LoaiDanhMuc.maBenNhan1);
                dtBanGiao.Columns.Add(cls_sys_LoaiDanhMuc.tenBenNhan1);
                dtBanGiao.Columns.Add(cls_sys_LoaiDanhMuc.chucVuBenNhan1);

                dtBanGiao.Columns.Add(cls_sys_LoaiDanhMuc.maBenNhan2);
                dtBanGiao.Columns.Add(cls_sys_LoaiDanhMuc.tenBenNhan2);
                dtBanGiao.Columns.Add(cls_sys_LoaiDanhMuc.chucVuBenNhan2);

                dtBanGiao.Columns.Add(cls_sys_LoaiDanhMuc.diaDiemBanGiao);

                DataRow drBanGiao = dtBanGiao.NewRow();
                drBanGiao[cls_sys_LoaiDanhMuc.maBanGiamDoc] = slbBanGiamDoc.txtMa.Text;
                drBanGiao[cls_sys_LoaiDanhMuc.tenBanGiamDoc] = slbBanGiamDoc.txtTen.Text;
                drBanGiao[cls_sys_LoaiDanhMuc.chucVuBanGiamDoc] = txtChucVuBenGiao1.Text;

                drBanGiao[cls_sys_LoaiDanhMuc.maBenGiao1] = slbBanGiao1.txtMa.Text;
                drBanGiao[cls_sys_LoaiDanhMuc.tenBenGiao1] = slbBanGiao1.txtTen.Text;
                drBanGiao[cls_sys_LoaiDanhMuc.chucVuBenGiao1] = txtChucVuBenNhan1.Text;

                drBanGiao[cls_sys_LoaiDanhMuc.maBenGiao2] = slbBanGiao2.txtMa.Text;
                drBanGiao[cls_sys_LoaiDanhMuc.tenBenGiao2] = slbBanGiao2.txtTen.Text;
                drBanGiao[cls_sys_LoaiDanhMuc.chucVuBenGiao2] = txtChucVuBenGiao2.Text;

                drBanGiao[cls_sys_LoaiDanhMuc.maBenNhan1] = slbBenNhan1.txtMa.Text;
                drBanGiao[cls_sys_LoaiDanhMuc.tenBenNhan1] = slbBenNhan1.txtTen.Text;
                drBanGiao[cls_sys_LoaiDanhMuc.chucVuBenNhan1] = txtChucVuBenNhan2.Text;

                drBanGiao[cls_sys_LoaiDanhMuc.maBenNhan2] = slbBenNhan2.txtMa.Text;
                drBanGiao[cls_sys_LoaiDanhMuc.tenBenNhan2] = slbBenNhan2.txtTen.Text;
                drBanGiao[cls_sys_LoaiDanhMuc.chucVuBenNhan2] = txtChucVuBGD.Text;

                drBanGiao[cls_sys_LoaiDanhMuc.diaDiemBanGiao] = txtDiaDiemBanGiao.Text;

                dtBanGiao.Rows.Add(drBanGiao);

                #endregion

                #region Xuất XML thông tin bàn giao chi tiết
                //nuoc sx, nam sx, quy cach, don gia, tai lieu ky thuat kem theo
                // DataTable dtBanGiaoChiTiet = ((DataTable)_bsNhanTaiSan.DataSource).Select().CopyToDataTable();
                DataTable dtBanGiaoChiTiet = _apiTaiSan.Get_SuDungLLTheoSoPhieu(slbSoPhieu.txtTen.Text);
                dtBanGiaoChiTiet.Columns.Add(cls_TaiSan_DanhMuc.col_NuocSanXuat);
                dtBanGiaoChiTiet.Columns.Add(cls_TaiSan_DanhMuc.col_NamSanXuat);
                dtBanGiaoChiTiet.Columns.Add(cls_TaiSan_DanhMuc.col_QuyCach);
                dtBanGiaoChiTiet.Columns.Add(cls_TaiSan_DanhMuc.col_TaiLieu);
                dtBanGiaoChiTiet.Columns.Add(cls_TaiSan_NhapCT.col_DonGia);
                DataRow drTaiSan;
                foreach (DataRow item in dtBanGiaoChiTiet.Rows)
                {
                    try
                    {
                        drTaiSan = _apiTaiSan.Get_TaiSanTheoMa(item[cls_TaiSan_SuDungLL.col_MaTaiSan].ToString());
                        item[cls_TaiSan_SuDungLL.col_TenTaiSan] = drTaiSan[cls_TaiSan_DanhMuc.col_Ten];
                        item[cls_TaiSan_DanhMuc.col_NuocSanXuat] = drTaiSan[cls_TaiSan_DanhMuc.col_NuocSanXuat];
                        item[cls_TaiSan_DanhMuc.col_NamSanXuat] = drTaiSan[cls_TaiSan_DanhMuc.col_NamSanXuat];
                        item[cls_TaiSan_DanhMuc.col_QuyCach] = drTaiSan[cls_TaiSan_DanhMuc.col_QuyCach];
                        item[cls_TaiSan_DanhMuc.col_TaiLieu] = drTaiSan[cls_TaiSan_DanhMuc.col_TaiLieu];
                        item[cls_TaiSan_NhapCT.col_DonGia] = _apiTaiSan.Get_NhapCTTheoMa(item[cls_TaiSan_SuDungLL.col_MaNhapCT].ToString())[cls_TaiSan_NhapCT.col_DonGia];
                    }
                    catch
                    {
                    }
                }

                #endregion

                dtBanGiao.TableName = "BanGiao";
                dtBanGiaoChiTiet.TableName = "BanGiaoChiTiet";

                if (dtBanGiao.Rows.Count > 0)
                {
                    ds.Tables.Add(dtBanGiao);
                    ds.Tables.Add(dtBanGiaoChiTiet);
                    if (!System.IO.Directory.Exists("..//xml"))
                    {
                        System.IO.Directory.CreateDirectory("..//xml");
                    }
                    ds.WriteXml("..//xml//ts_BienBanBanGiao.xml", XmlWriteMode.WriteSchema);
                    if (!System.IO.File.Exists("..\\..\\..\\Report\\ts_BienBanBanGiao.rpt"))
                    {
                        MessageBox.Show("Thiếu file: ts_BienBanBanGiao.rpt trong thư mục Report.");
                        return;
                    }
                    ReportDocument rptDoc = new ReportDocument();
                    rptDoc.Load("..\\..\\..\\Report\\ts_BienBanBanGiao.rpt", OpenReportMethod.OpenReportByDefault);
                    rptDoc.SetDataSource(ds);
                    frm_Report frm = new frm_Report(rptDoc);
                    frm.ShowDialog();
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

        #endregion

        #region Sự kiện

        #region frm_SuDung_Load (Sự kiện Load form)

        private void frm_SuDung_Load(object sender, EventArgs e)
        {
            EndEditMode();
            btnSua.Enabled = true;
        }

        #endregion

        #region chkAll_CheckedChanged (Sự kiện chọn tất cả)

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRowView view in _bsBanGiao)
                {
                    view["Chon"] = chkAll.Checked;
                }
                _bsBanGiao.EndEdit();
            }
            catch { }

        }

        #endregion

        #region dgvMain_CellClick (Sự kiện chọn sửa/xóa)

        private void dgvMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvMain.RowCount > 0 && e.RowIndex > -1)
            {
                try
                {
                    DataRowView view = (DataRowView)_bsBanGiao.Current;
                    if (e.ColumnIndex == 0)
                    {
                        try
                        {
                            view["Chon"] = view["Chon"].ToString() == "True" || view["Chon"].ToString() == "1" ? false : true;
                        }
                        catch
                        {
                            view["Chon"] = true;
                        }
                        _bsBanGiao.EndEdit();
                    }

                    if (e.ColumnIndex == colXoa.Index)
                    {
                        if (_isEdit)
                        {
                            if (chkTrangThai.Value)
                            {
                                TA_MessageBox.MessageBox.Show("Phiếu đã đóng, không thể xóa chi tiết.", TA_MessageBox.MessageIcon.Information);
                                return;
                            }

                            DialogResult rsl = MessageBox.Show("Bạn có chắc chắn xóa dòng đang chọn không?", "Xác nhận"
                             , MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (rsl == System.Windows.Forms.DialogResult.Yes)
                            {
                                DataRowView rowSelect = dgvMain.CurrentRow.DataBoundItem as DataRowView;
                                object id = rowSelect.Row[cls_TaiSan_SuDungLL.col_Ma];
                                _resultInfo = _detailService.Delete(rowSelect.Row[cls_TaiSan_SuDungLL.col_Ma]);
                                if (_resultInfo.Status)
                                {
                                    Synchronized();
                                    Load_TaiSan();
                                }else
                                {
                                    TA_MessageBox.MessageBox.Show("", TA_MessageBox.MessageIcon.Error);
                                }
                            }
                        }
                        else
                        {

                        }

                    }
                }
                catch
                {
                }
            }
        }

        #endregion

        #region dgvMain_DataError (Sự kiện DataError của datagridview)

        private void dgvMain_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        #endregion

        #region btnThemCT_Click (Sự kiện thêm 1 dòng chi tiết)

        private void btnThemCT_Click(object sender, EventArgs e)
        {
            if (chkTrangThai.Value)
            {
                TA_MessageBox.MessageBox.Show("Phiếu đã đóng, không thể thêm chi tiết.", TA_MessageBox.MessageIcon.Information);
                return;
            }
            bool kt = true;
            _masterInfo = GetInfomationMaster();
            if (_masterInfo.BGID == 0)
            {
                _resultInfo = SaveMaster();
                kt = _resultInfo.Status;
                if (kt)
                {
                    Load_SoPhieu();
                }
               
            }
            if (kt)
            {
                FillToControl(_masterInfo.BGID);
                btnBoQua.Enabled = false;
                ThemChiTiet();
                Synchronized();
               
            }
        }

        #endregion

        #region btnXoaDanhSach_Click (Sự kiện xóa danh sách chi tiết)

        private void btnXoaDanhSach_Click(object sender, EventArgs e)
        {
            _bsBanGiao.Clear();
        }

        #endregion

        #region txtSoPhieu_KeyDown (Sự kiện Enter số phiếu)

        private void txtSoPhieu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectNextControl(this, true, true, true, true);
            }
        }

        #endregion

        #region dgvMain_RowPostPaint (Sự kiện tạo số thứ tự)

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

        #region slbDonViQuanLy_HisSelectChange (Sự kiện chọn lại đơn vị quản lý)

        private void slbDonViQuanLy_HisSelectChange(object sender, EventArgs e)
        {
            Load_TaiSan();
        }

        #endregion

        #region slbDonViSuDung_HisSelectChange (Sự kiện chọn đơn vị sử dụng)

        private void slbDonViSuDung_HisSelectChange(object sender, EventArgs e)
        {
            Load_NhanVien(slbDonViSuDung.txtMa.Text);
        }

        #endregion

        #region lblKhu_Click (Sự kiện mở danh mục khu sử dụng)

        private void lblKhu_Click(object sender, EventArgs e)
        {
            var frm = new frm_DanhMucChung(cls_sys_LoaiDanhMuc.khu_MaLoai, cls_sys_LoaiDanhMuc.khu_TenLoai, "", "", "", "");

            frm.ShowDialog();
            slbKhu.DataSource = Load_DanhMuc(cls_sys_LoaiDanhMuc.khu_MaLoai, "-1");
        }

        #endregion

        #region lblTang_Click (Sự kiện mở danh mục tầng sử dụng)

        private void lblTang_Click(object sender, EventArgs e)
        {
            var frm = new frm_DanhMucChung(cls_sys_LoaiDanhMuc.tang_MaLoai, cls_sys_LoaiDanhMuc.tang_TenLoai, cls_sys_LoaiDanhMuc.khu_MaLoai, cls_sys_LoaiDanhMuc.khu_TenLoai
                , slbKhu.txtMa.Text, slbKhu.txtTen.Text);
            frm.ShowDialog();
            slbTang.DataSource = Load_DanhMuc(cls_sys_LoaiDanhMuc.tang_MaLoai, slbKhu.txtMa.Text);
        }

        #endregion

        #region lblPhong_Click (Sự kiện mở danh mục phòng công năng)

        private void lblPhong_Click(object sender, EventArgs e)
        {
            var frm = new frm_DanhMucChung(cls_sys_LoaiDanhMuc.phong_MaLoai, cls_sys_LoaiDanhMuc.phong_TenLoai, cls_sys_LoaiDanhMuc.tang_MaLoai, cls_sys_LoaiDanhMuc.tang_TenLoai
                , slbTang.txtMa.Text, slbTang.txtTen.Text);
            frm.ShowDialog();
            slbPhongCongNang.DataSource = Load_DanhMuc(cls_sys_LoaiDanhMuc.phong_MaLoai, slbTang.txtMa.Text);
        }

        #endregion

        #region slbTaiSan_HisSelectChange (Sự kiện chọn tài sản)

        private void slbTaiSan_HisSelectChange(object sender, EventArgs e)
        {
            try
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
            }
            catch
            {
            }
        }

        #endregion

        #region slbKhu_HisValueChange (Sự kiện chọn khu sử dụng)

        private void slbKhu_HisValueChange(object sender, EventArgs e)
        {
            slbTang.DataSource = Load_DanhMuc(cls_sys_LoaiDanhMuc.tang_MaLoai, slbKhu.txtMa.Text);
        }

        #endregion

        #region slbTang_HisValueChange (Sự kiện chọn tầng sử dụng)

        private void slbTang_HisValueChange(object sender, EventArgs e)
        {
            slbPhongCongNang.DataSource = Load_DanhMuc(cls_sys_LoaiDanhMuc.phong_MaLoai, slbTang.txtMa.Text);
        }

        #endregion

        #region slbTaiSan_HisTenKeyDown (Sự kiện Enter ô tài sản)

        private void slbTaiSan_HisTenKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && slbTaiSan.txtMa.Text != "" && slbTaiSan.txtTen.Text != "")
            {
                //txtSoLuong.Select();
            }
        }

        #endregion

        #region slbSoPhieu_HisSelectChange (Sự kiện chọn lại số phiếu)

        private void slbSoPhieu_HisSelectChange(object sender, EventArgs e)
        {
            if (slbSoPhieu.txtMa.Text != "")
            {
                FillToControl(Helper.ConvertSToDec(slbSoPhieu.txtMa.Text));
             //   SuaPhieuBanGiao();
            }
            else
            {
                // Load_KhoaPhongQuanLy(); 
            }
        }

        #endregion

        #region slbSoPhieu_HisTenKeyDown (Sự kiện thay đổi số phiếu)

        private void slbSoPhieu_HisTenKeyDown(object sender, KeyEventArgs e)
        {
            if (slbSoPhieu.txtTen.TextLength > 20)
            {
                TA_MessageBox.MessageBox.Show("Số phiếu tối đa 20 ký tự.", TA_MessageBox.MessageIcon.Error);
            }
        }

        #endregion


        #region btnInBienBanBanGiao_Click (Sự kiện in biên bản bàn giao)

        private void btnInBienBanBanGiao_Click(object sender, EventArgs e)
        {
            InBienBanBanGiao();
        }

        #endregion

        #endregion

        public void SetData(string maPhieu,string soPhieu)
        {
            slbSoPhieu.txtMa.Text = maPhieu;
            slbSoPhieu.txtTen.Text = soPhieu;
        }
        private void LockControl(bool status)
        {
            txtSoPhieu.ReadOnly = !status;
            dtpNgayNhan.Enabled = status;
            slbBanGiamDoc.Enabled = status;
            txtChucVuBenGiao1.ReadOnly = !status;
            txtChucVuBGD.ReadOnly = !status;
            slbDonViQuanLy.Enabled = status;
            slbDonViSuDung.Enabled = status;
            slbBanGiao1.Enabled = status;
            slbBenNhan1.Enabled = status;
            txtChucVuBenNhan1.ReadOnly = !status;

            slbBanGiao2.Enabled = status;
            slbBenNhan2.Enabled = status;
            txtChucVuBenGiao2.ReadOnly = !status;
            txtChucVuBenNhan2.ReadOnly = !status;
            txtDiaDiemBanGiao.ReadOnly = !status;
            txtGhichu.ReadOnly = !status;
            chkTrangThai.IsReadOnly = !status;

            slbTaiSan.Enabled = status;
            slbNguoiSuDung.Enabled = status;
            slbKhu.Enabled = status;
            slbTang.Enabled = status;
            slbPhongCongNang.Enabled = status;
            btnThemCT.Enabled = status;
            dgvMain.ReadOnly = !status;

            slbSoPhieu.Enabled = !status;

            btnBoQua.Enabled = status;
        }
        private void EnterEditMode()
        {
            _isEdit = true;
            LockControl(_isEdit);
        }
        private void EndEditMode()
        {
            _isEdit = false;
            LockControl(_isEdit);
        }
        private void ClearDetail()
        {
            slbTaiSan.clear();
            slbNguoiSuDung.clear();
            slbKhu.clear();
            slbTang.clear();
            slbPhongCongNang.clear();
        }
        private void ClearControl()
        {
            txtMaPhieu.Text = "";
            txtSoPhieu.Text = "";
            dtpNgayNhan.Value = DateTime.Now;
            slbBanGiamDoc.clear();
            txtChucVuBenGiao1.Text = "";
            txtChucVuBGD.Text = "";
            slbDonViQuanLy.clear();
            slbDonViSuDung.clear();
            slbBanGiao1.clear();
            slbBenNhan1.clear();
            txtChucVuBenNhan1.Text = "";

            slbBanGiao2.clear();
            slbBenNhan2.clear();
            txtChucVuBenGiao2.Text = "";
            txtChucVuBenNhan2.Text = "";
            txtDiaDiemBanGiao.Text = "";
            txtGhichu.Text = "";
            chkTrangThai.Value = false;
            lblTaoboi.Text = "";
            ClearDetail();
        }
        private void FillToControl(decimal ma)
        {
            try
            {
                DataRow row = _apiTaiSan.Get_DSBanGiaoTheoMa(ma);
                if (row != null)
                {
                    txtMaPhieu.Text = "" + ma;
                    txtSoPhieu.Text = "" + row[cls_TaiSan_BanGiaoLL.col_SoChungTu];
                    dtpNgayNhan.Value = Helper.ConvertSToDtime("" + row[cls_TaiSan_BanGiaoLL.col_Ngay]);
                    slbBanGiamDoc.txtMa.Text = "" + row[cls_TaiSan_BanGiaoLL.col_DaiDien];
                    slbBanGiamDoc.txtTen.Text = "" + row[@"TENNGUOIDD"];

                    txtChucVuBGD.Text = "" + row[cls_TaiSan_BanGiaoLL.col_ChucVuDaiDien];
                    slbDonViQuanLy.txtMa.Text = "" + row[cls_TaiSan_BanGiaoLL.col_KhoaQuanLy];
                    slbDonViQuanLy.txtTen.Text = "" + row["TENKPQL"];
                    slbDonViSuDung.txtMa.Text = "" + row[cls_TaiSan_BanGiaoLL.col_KhoaSuDung];
                    slbDonViSuDung.txtTen.Text = "" + row["TENKPSD"];
                    slbBanGiao1.txtMa.Text = "" + row[cls_TaiSan_BanGiaoLL.col_NguoiGiao1];
                    slbBanGiao1.txtTen.Text = "" + row["TENNGUOIGIAO1"];

                    slbBanGiao2.txtMa.Text = "" + row[cls_TaiSan_BanGiaoLL.col_NguoiGiao2];
                    slbBanGiao2.txtTen.Text = "" + row["TENNGUOIGIAO2"];

                    slbBenNhan1.txtMa.Text = "" + row[cls_TaiSan_BanGiaoLL.col_NguoiNhan1];
                    slbBenNhan1.txtTen.Text = "" + row["TENNGUOINHAN1"];

                    slbBenNhan2.txtMa.Text = "" + row[cls_TaiSan_BanGiaoLL.col_NguoiNhan2];
                    slbBenNhan2.txtTen.Text = "" + row["TENNGUOINHAN2"];
                    txtChucVuBenNhan1.Text = "" + row[cls_TaiSan_BanGiaoLL.col_ChucVuNN1];
                    txtChucVuBenNhan2.Text = "" + row[cls_TaiSan_BanGiaoLL.col_ChucVuNN2];

                    txtChucVuBenGiao1.Text = "" + row[cls_TaiSan_BanGiaoLL.col_ChucVuNG1];
                    txtChucVuBenGiao2.Text = "" + row[cls_TaiSan_BanGiaoLL.col_ChucVuNG2];

                    txtDiaDiemBanGiao.Text = "" + row[cls_TaiSan_BanGiaoLL.col_NoiBanGiao];
                    txtGhichu.Text = "" + row[cls_TaiSan_BanGiaoLL.col_GhiChu];
                    chkTrangThai.Value = Helper.ConvertSToBool("" + row[cls_TaiSan_BanGiaoLL.col_TrangThai]);
                    lblTaoboi.Text = $"{ new E00_API.Base.clsBUS().GetTenNguoiDungByIDorUserid(row[cls_TaiSan_BanGiaoLL.col_USERID].ToString())} lúc: {Helper.ConvertSToDtime("" + row[cls_TaiSan_BanGiaoLL.col_NgayTao]).ToString("dd/MM/yy hh: mm")} ";
                    Synchronized();

                    btnSua.Enabled = true;
                    Load_TaiSan();
                    Load_NhanVien(slbDonViSuDung.txtMa.Text);
                }
            }
            catch
            {
                ClearControl();
            }
        }
        private TaiSan_BanGiaoLLInfo GetInfomationMaster()
        {
            _masterInfo = new TaiSan_BanGiaoLLInfo();
            _masterInfo.BGID = Helper.ConvertSToDec("0" + txtMaPhieu.Text);
            _masterInfo.SoChungTu = txtSoPhieu.Text;
            _masterInfo.Ngay = dtpNgayNhan.Value;
            _masterInfo.NDaiDien = slbBanGiamDoc.txtMa.Text;
            _masterInfo.CVNDaiDien = txtChucVuBGD.Text;
            _masterInfo.KhoaQuanLy = slbDonViQuanLy.txtMa.Text;
            _masterInfo.KhoaSuDung = slbDonViSuDung.txtMa.Text;
            _masterInfo.NguoiGiao1 = slbBanGiao1.txtMa.Text;
            _masterInfo.NguoiGiao2 = slbBanGiao2.txtMa.Text;
            _masterInfo.NguoiNhan1 = slbBenNhan1.txtMa.Text;
            _masterInfo.NguoiNhan2 = slbBenNhan2.txtMa.Text;
            _masterInfo.ChucVuNG1 = txtChucVuBenGiao1.Text;
            _masterInfo.ChucVuNG2 = txtChucVuBenGiao2.Text;
            _masterInfo.ChucVuNN1 = txtChucVuBenNhan1.Text;
            _masterInfo.ChucVuNN2 = txtChucVuBenNhan2.Text;

            _masterInfo.NoiBanGiao = txtDiaDiemBanGiao.Text;
            _masterInfo.GhiChu = txtGhichu.Text;
            _masterInfo.TrangThai = chkTrangThai.Value == true ? 1 : 0;

            return _masterInfo;
        }
        private ResultInfo SaveMaster()
        {
            try
            {
                if (Check_Data())
                {
                    if (_masterInfo.BGID == 0)
                    {
                        _masterInfo.BGID = _masterService.CreateNewID(20);
                        _masterInfo.MACHINEID = _masterService.GetMachineID();
                        txtMaPhieu.Text = "" + _masterInfo.BGID;
                        return _masterService.Insert(_masterInfo);
                    }
                    else
                    {
                        return _masterService.Update(_masterInfo);
                    }
                    return new ResultInfo();
                }
                return new ResultInfo();
            }
            catch
            {
                return new ResultInfo();
            }
        }
        private void Synchronized()
        {
            _bsBanGiao.DataSource = _apiTaiSan.Get_BanGiaoChiTiet(Helper.ConvertSToDec(txtMaPhieu.Text));
            dgvMain.DataSource = _bsBanGiao;
            _count = dgvMain.RowCount;
        }
        private void CheckNumber()
        {
            var rowCheck = _masterService.Get_Item($"{cls_TaiSan_BanGiaoLL.col_SoChungTu} = '{txtSoPhieu.Text}'");
            if (rowCheck != null)
            {
                TA_MessageBox.MessageBox.Show("Số phiếu đã tồn tại vui lòng nhập số khác.", TA_MessageBox.MessageIcon.Error);
                txtSoPhieu.Focus();
            }
        }
    }
}
