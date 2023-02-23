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
using E00_Helpers.Helpers;
using E00_Helpers.Format;
using E00_API.Contract.TaiSan;
using E00_SafeCacheDataService.Interface;
using E00_API.Contract;
using E00_SafeCacheDataService.Base;
using E00_SafeCacheDataService.Common;
using QuanLyTaiSan.Forms;

namespace QuanLyTaiSan
{
    public partial class frm_NhapTaiSan : frm_DanhMuc
    {

        #region Biến toàn cục

        private Api_Common _api = new Api_Common();
        public Acc_Oracle _acc = new Acc_Oracle();
        private api_TaiSan _apiTaiSan = new api_TaiSan();
        private string _maNhom = "";
        private string _tenNhom = "";
        private bool _isAdd = true;
        private string _userError = string.Empty;
        private string _systemError = string.Empty;
        BindingSource _bsNhapCT = new BindingSource();

        Dictionary<string, string> _dicNhapLL = new Dictionary<string, string>();
        Dictionary<string, string> _dicNhapCT = new Dictionary<string, string>();
        List<string> _lstKey = new List<string>();
        List<string> _lstUnique = new List<string>();
        Dictionary<string, string> _dicWhere = new Dictionary<string, string>();
        DataTable _dtTaiSan = new DataTable();
        private string _sophieu = "";
        private decimal _soLuongMin = 0;
        private decimal _idMax = 0;
        private List<string> _lstMaTaiSanXoa = new List<string>();
        private TaiSan_NhapLLInfo _masterInfo;
        private TaiSan_NhapCTInfo _detailInfo;
        private string _hanBaoHanh = "";
        private bool _isEdit = false;
        private IAPI<TaiSan_NhapCTSPInfo> _productService;
        private IAPI<TaiSan_NhapCTInfo> _detailService;
        private IAPI<TaiSan_DanhMucInfo> _menuService;

        #endregion

        #region Khởi tạo

        #region Khởi tạo chung

        public frm_NhapTaiSan()
        {
            InitializeComponent();
            _api.KetNoi();
            E00_Helpers.Helpers.Helper.ControlEventEnter(new Control[] { txtSoPhieu, dtpNgayChungTu, cboLoaiNhap, slbNhaCungCap, txtNguoiGiao, txtSoDTNGuoiGiao,
            txtSoXe,txtNoiNhap,txtGhiChu,chkTrangThai,slbTaiSan,cboDVT,txtSoLuong,txtHanBaoHanh,txtDonGia,txtThanhTien} );
        }

        #endregion

        #region Khởi tạo sử dụng dữ liệu theo nhóm
        /// <summary>
        /// Khởi tạo sử dụng dữ liệu theo nhóm
        /// </summary>
        /// <param name="maNhom">Mã nhóm để lấy dữ liệu</param>
        /// <param name="tenNhom">Tên hiển thị trên textForm</param>
        public frm_NhapTaiSan(string maNhom, string tenNhom)
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
            base.LoadData();
            _productService = new API_Common<TaiSan_NhapCTSPInfo>();
            _detailService = new API_Common<TaiSan_NhapCTInfo>();
            _menuService = new API_Common<TaiSan_DanhMucInfo>();
            Load_NhaCungCap();
            Load_DanhMuc();
            Load_TaiSan();
            Load_DonViTinh();

            slbSoPhieu.DataSource = _apiTaiSan.Get_SophieuNhapDangNhap();
            cboLoaiNhap.DataSource = cls_sys_LoaiDanhMuc.GetLoaiNhap().ToList();
            cboLoaiNhap.DisplayMember = "Value";
            cboLoaiNhap.ValueMember = "Key";

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

            dtpNgayChungTu.Value = DateTime.Now;
            txtSoPhieu.Text = _apiTaiSan.GetNumberMax();
            cboLoaiNhap.SelectedIndex = 0;
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
            base.Sua();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            dgvMain.Enabled = true;
            pnlMain.Enabled = true;
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
                if (_apiTaiSan.CheckDetail(txtMa.Text))
                {
                    TA_MessageBox.MessageBox.Show("Vui lòng xóa chi tiết trước khi xóa phiếu.", TA_MessageBox.MessageIcon.Information);
                }
                else
                {
                   if(!_apiTaiSan.DeletePhieuNhap(txtMa.Text))
                    {
                        TA_MessageBox.MessageBox.Show("Xóa thất bại, vui lòng kiểm tra và thử lại.", TA_MessageBox.MessageIcon.Information);
                    }else
                    {
                        slbSoPhieu.DataSource = _apiTaiSan.Get_SophieuNhapDangNhap();
                        ClearControl();
                        ClearDetail();
                    }
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
            if (String.IsNullOrEmpty(slbNhaCungCap.txtMa.Text))
            {
                MessageBox.Show("Vui lòng nhập nhà cung cấp.", "Thông báo"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                slbNhaCungCap.Focus();
                return false;
            }
            if (dtpNgayChungTu.Value == null)
            {
                MessageBox.Show("Vui lòng nhập ngày chứng từ.", "Thông báo"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                slbNhaCungCap.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(txtSoPhieu.Text))
            {
                MessageBox.Show("Vui lòng nhập số phiếu.", "Thông báo"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                slbSoPhieu.txtTen.Focus();
                return false;
            }
            if (String.IsNullOrEmpty("" + cboLoaiNhap.SelectedValue))
            {
                MessageBox.Show("Vui lòng chọn loại nhập.", "Thông báo"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                slbSoPhieu.txtTen.Focus();
                return false;
            }
            _masterInfo = GetInfomationMaster();
            
            if (_masterInfo.Ma == 0)
            {
                DataRow rowCheck = _apiTaiSan.Get_RowTheoSoPhieu(_masterInfo.SoPhieu);
                if (rowCheck != null)
                {
                    TA_MessageBox.MessageBox.Show("Số phiếu đã tồn tại vui lòng nhập số khác.", TA_MessageBox.MessageIcon.Error);
                    txtSoPhieu.Focus();
                    return false;
                }
            }
            if (chkTrangThai.Value)
            {
                if (!_apiTaiSan.CheckDetail(txtMa.Text))
                {
                    TA_MessageBox.MessageBox.Show("Vui lòng thêm chi tiết trước khi hoàn thành phiếu.", TA_MessageBox.MessageIcon.Information);
                    return false;
                }
               
            }
            return true;
        }

        #endregion

        #region Import_Excel (Import dữ liệu từ file Excel)

        protected override void Import_Excel()
        {
            var frm = new frm_ImportNhapTaiSan();
            frm.ShowDialog();
            Load_TaiSan();
            base.Import_Excel();
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
                    #region Thêm phiếu nhập kho mới

                    if (_masterInfo.Ma == 0)
                    {
                        _masterInfo.Ma = decimal.Parse(_acc.Get_MaMoi());
                        txtMa.Text = "" + _masterInfo.Ma;
                        if (!_apiTaiSan.Insert_NhapLL(_masterInfo))
                        {
                            TA_MessageBox.MessageBox.Show("Cập nhật không thành công.", TA_MessageBox.MessageIcon.Information);
                        }
                        base.Luu();
                        slbSoPhieu.DataSource = _apiTaiSan.Get_SophieuNhapDangNhap();
                        BoQua();
                        btnSua.Enabled = true;
                        dgvMain.Enabled = false;
                    }

                    #endregion

                    #region Sửa phiếu nhập kho

                    else
                    {
                        if (!_apiTaiSan.Update_NhapKho(_masterInfo))
                        {
                            TA_MessageBox.MessageBox.Show("Cập nhật không thành công.", TA_MessageBox.MessageIcon.Information);
                        }
                        base.Luu();
                        BoQua();
                        btnSua.Enabled = true;
                        dgvMain.Enabled = false;
                    }

                    #endregion
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
                _bsNhapCT.Filter = "";
            }
            else
            {
                _bsNhapCT.Filter = string.Format("{0} like '%{2}%' OR {1} like'%{2}%' ", cls_SYS_DanhMuc.col_Ma, cls_SYS_DanhMuc.col_Ten, txtTimKiem.Text);
            }
            _count = _bsNhapCT.Count;
            base.TimKiem();
        }

        #endregion

        #region BoQua (Hủy thao tác hiện tại)
        /// <summary>
        /// Hủy thao tác hiện tại
        /// </summary>
        /// <author>Võ Bảo Toàn</author>
        /// <Date>2018/06/01</Date>
        protected override void BoQua()
        {
            base.BoQua();
            pnlControl2.Enabled = true;
            EndEditMode();
            ClearControl();

            if (!string.IsNullOrEmpty(slbSoPhieu.txtMa.Text))
            {
                FillToControl(slbSoPhieu.txtMa.Text);
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

        #endregion

        #region Phương thức dùng riêng (Private)

        #region SaveMaster (Cập nhật dữ liệu Master Insert/Update)
        /// <summary>
        /// Cập nhật dữ liệu Master Insert/Update
        /// </summary>
        /// <author>TinNT</author>
        /// <date>2018/12/228</date>
        private bool SaveMaster()
        {
            try
            {
                if (Check_Data())
                {
                    if (_masterInfo.Ma == 0)
                    {
                        _masterInfo.Ma = decimal.Parse(_acc.Get_MaMoi());
                        txtMa.Text = ""+_masterInfo.Ma;
                        return _apiTaiSan.Insert_NhapLL(_masterInfo);
                    }
                    else
                    {
                        return _apiTaiSan.Update_NhapKho(_masterInfo);
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Load_DanhMuc (Load cấu trúc bảng NhapCT)
        /// <summary>
        /// Load cấu trúc bảng NhapCT
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/04/23</Date>
        private void Load_DanhMuc()
        {
            try
            {
                DataTable dt = XL_BANG.Doc_cau_truc(_acc.Get_User() + "." + cls_TaiSan_NhapCT.tb_TenBang);
                dt.Columns.Add("Sua");
                dt.Columns.Add("SoLuongMin");
                _bsNhapCT.DataSource = dt;
                dgvMain.DataSource = _bsNhapCT;
            }
            catch
            {
                _bsNhapCT.DataSource = null;
            }
            _count = _bsNhapCT.Count;
            //base.TimKiem();
        }

        #endregion

        #region Load_NhaCungCap (Load danh mục nhà cung cấp)
        /// <summary>
        /// Load danh mục nhà cung cấp
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/04/23</Date>
        private void Load_NhaCungCap()
        {
            try
            {
                Dictionary<string, string> dicE = new Dictionary<string, string>();
                List<string> lstCot = new List<string>();
                lstCot.Add(cls_SYS_DanhMuc.col_Ma);
                lstCot.Add(cls_SYS_DanhMuc.col_Ten);

                dicE.Add(cls_SYS_DanhMuc.col_Loai, cls_sys_LoaiDanhMuc.nhaCungCap_MaLoai);
                slbNhaCungCap.DataSource = _api.Search(ref _userError, ref _systemError, cls_SYS_DanhMuc.tb_TenBang, dicEqual: dicE, lst: lstCot
                                                    , orderByASC1: true, orderByName1: cls_SYS_DanhMuc.col_Ten);
            }
            catch
            {
                slbNhaCungCap.DataSource = null;
            }
        }

        #endregion

        #region Load_TaiSan (Load danh mục tài sản)
        /// <summary>
        /// Load danh mục tài sản
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/04/23</Date>
        private void Load_TaiSan()
        {
            List<string> lstCot = new List<string>();
            lstCot.Add(cls_TaiSan_DanhMuc.col_Ma);
            lstCot.Add(cls_TaiSan_DanhMuc.col_Ten);
            lstCot.Add(cls_TaiSan_DanhMuc.col_DonViTinh);
            lstCot.Add(cls_TaiSan_DanhMuc.col_KyHieu);
            lstCot.Add(cls_TaiSan_DanhMuc.col_QuyCach);

            slbTaiSan.ColumDataList = new string[] { cls_TaiSan_DanhMuc.col_Ma, cls_TaiSan_DanhMuc.col_Ten, cls_TaiSan_DanhMuc.col_KyHieu, cls_TaiSan_DanhMuc.col_QuyCach, cls_TaiSan_DanhMuc.col_DonViTinh };
            slbTaiSan.ColumWidthList = new int[] { 120, 999, 100, 150, 50 };
            slbTaiSan.ValueMember = cls_TaiSan_DanhMuc.col_Ma;
            slbTaiSan.DisplayMember = cls_TaiSan_DanhMuc.col_Ten;
            slbTaiSan.DataSource = _api.Search(ref _userError, ref _systemError, cls_TaiSan_DanhMuc.tb_TenBang, lst: lstCot);

        }

        #endregion

        #region ThemChiTiet (Thêm chi tiết nhập)
        /// <summary>
        /// Thêm chi tiết nhập
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/05/22</Date>
        private void ThemChiTiet()
        {
            try
            {

                DataRowView view;
                decimal soLuong = 0;
                int index = 0;
                decimal detailID = 0;
                #region Kiểm tra dữ liệu hợp lệ

                if (slbTaiSan.txtMa.Text == "" || slbTaiSan.txtTen.Text == "")
                {
                    TA_MessageBox.MessageBox.Show("Vui lòng chọn tài sản", TA_MessageBox.MessageIcon.Error);
                    slbTaiSan.txtTen.Focus();
                    return;
                }
                if (!decimal.TryParse(txtSoLuong.Text, out soLuong))
                {
                    TA_MessageBox.MessageBox.Show("Số lượng không hợp lệ.", TA_MessageBox.MessageIcon.Error);
                    txtSoLuong.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(cboDVT.txtMa.Text))
                {
                    TA_MessageBox.MessageBox.Show("Vui lòng chọn đơn vị tính.", TA_MessageBox.MessageIcon.Error);
                    cboDVT.txtTen.Focus();
                    return;
                }
                if (txtHanBaoHanh.Value != DateTime.MinValue)
                {
                    _hanBaoHanh = txtHanBaoHanh.Value.ToString("yyyy-MM-dd");
                }
                #endregion

                TaiSan_DanhMucInfo infoTaiSan = _menuService.Get_Item($"{cls_TaiSan_DanhMuc.col_Ma} = '{slbTaiSan.txtMa.Text}'");
                if(infoTaiSan == null)
                {
                    TA_MessageBox.MessageBox.Show("Tài sản không được tìm thấy, vui lòng kiểm tra và thử lại.", TA_MessageBox.MessageIcon.Information);
                    return;
                }
                string locationDefault = cls_sys_LoaiDanhMuc.LocationDefault;
                if (!string.IsNullOrEmpty(infoTaiSan.KhoMacDinh))
                {
                    locationDefault = infoTaiSan.KhoMacDinh;
                }

                if (btnThemCT.Tag.ToString() == "Edit")
                {
                    #region Sửa số lượng nhập chi tiết
                    soLuong = decimal.Parse(txtSoLuong.Text);
                    detailID = decimal.Parse(txtDetailID.Text);
                    if( _apiTaiSan.Update_NhapCT(""+ detailID, slbTaiSan.txtMa.Text, decimal.Parse(txtSoLuong.Text), cboDVT.txtMa.Text, decimal.Parse(txtDonGia.Text != "" ? txtDonGia.Text : "0"), _hanBaoHanh))
                    {
                        _productService.Delete($"{cls_TaiSan_NhapCTSP.col_NhapCTID} = {detailID}");
                        InsertProduct(detailID, soLuong, locationDefault);
                    }
                    btnThemCT.Tag = " ";

                    #endregion
                }
                else
                {
                    #region Thêm mới nhập kho chi tiết
                    DataRowView vNhapCT = Check_NhapCT(slbTaiSan.txtMa.Text, ref index);
                    if (vNhapCT != null)
                    {
                        TA_MessageBox.MessageBox.Show(string.Format("Tài sản '{0}' đã tồn tại. \n số lượng {1} ?", slbTaiSan.txtTen.Text, txtSoLuong.Text), TA_MessageBox.MessageIcon.Information);
                        return;
                    }
                    soLuong = decimal.Parse(txtSoLuong.Text);
                    detailID = _productService.CreateNewID(20);
                   if (_apiTaiSan.Insert_NhapCT(""+ detailID, "" + _masterInfo.Ma, slbTaiSan.txtMa.Text, soLuong, decimal.Parse(txtDonGia.Text != "" ? txtDonGia.Text : "0"), "", cboDVT.txtMa.Text, _hanBaoHanh))
                    {
                        InsertProduct(detailID, soLuong, locationDefault);
                    }
                    else
                    {
                        TA_MessageBox.MessageBox.Show("Thêm thất bại, vui lòng kiểm tra và thử lại.", TA_MessageBox.MessageIcon.Information);

                    }

                    #endregion
                }

                ClearDetail();

                //dsdsdsdsd
            }
            catch// (Exception ex)
            {
                //Console.Write(ex);
            }
        }

        #endregion

        #region TinhThanhTien (Tính thành tiền khi nhập số lượng và đơn giá)
        /// <summary>
        /// Tính thành tiền khi nhập số lượng và đơn giá
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/05/15</Date>
        public void TinhThanhTien()
        {
            try
            {
                decimal soluong = decimal.Parse(this.txtSoLuong.Text == string.Empty ? "0" : this.txtSoLuong.Text);
                decimal dongia = decimal.Parse(this.txtDonGia.Text == string.Empty ? "0" : this.txtDonGia.Text);
                this.txtThanhTien.Text = (soluong * dongia).ToString();
            }
            catch
            {
                this.txtThanhTien.Text = "0";
            }
        }

        #endregion

        #region Check_NhapCT (Kiểm tra tài sản đã tồn tại trong nhập chi tiết)

        private DataRowView Check_NhapCT(string maTaiSan, ref int index)
        {
            try
            {
                index = 0;
                foreach (DataRowView item in _bsNhapCT)
                {
                    if (item[cls_TaiSan_NhapCT.col_MaTaiSan].ToString() == maTaiSan)
                    {
                        return item;
                    }
                    index++;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region Load_DonViTinh (Load dữ liệu đơn vị tính)
        /// <summary>
        /// Load dữ liệu đơn vị tính
        /// </summary>
        /// <author>TinNT</author>
        /// <Date>2018/12/27</Date>
        private void Load_DonViTinh()
        {
            try
            {
                Dictionary<string, string> dicCot = new Dictionary<string, string>();
                cboDVT.DataSource = _api.Search(ref _userError, ref _systemError, cls_DanhMucDonViTinh.tb_TenBang, orderByName1: cls_DanhMucDonViTinh.col_Ten);
                cboDVT.DisplayMember = cls_DanhMucDonViTinh.col_Ten;
                cboDVT.ValueMember = cls_DanhMucDonViTinh.col_ID;
            }
            catch
            {
                cboDVT.DataSource = null;
            }
        }

        #endregion


        #endregion

        #endregion

        #region Sự kiện

        #region Sự kiện chọn sửa/xóa

        private void dgvMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!_isEdit) return;
            if (dgvMain.RowCount > 0 && e.RowIndex > -1)
            {
                try
                {
                    
                    if (e.ColumnIndex == col_Sua.Index)//Button Sửa
                    {
                        if (chkTrangThai.Value)
                        {
                            TA_MessageBox.MessageBox.Show("Phiếu đã đóng, không thể sửa chi tiết.", TA_MessageBox.MessageIcon.Information);
                            return;
                        }
                        #region Sửa
                        DataRow row = ((DataRowView)dgvMain.Rows[e.RowIndex].DataBoundItem).Row;
                        cboDVT.SetTenByMa("" + row[cls_TaiSan_NhapCT.col_DonViTinh]);
                        slbTaiSan.txtMa.Text = row[cls_TaiSan_NhapCT.col_MaTaiSan].ToString();
                        slbTaiSan.txtTen.Text = row[cls_TaiSan_NhapCT.col_TenTaiSan].ToString();
                        txtSoLuong.Text = row[cls_TaiSan_NhapCT.col_SoLuong].ToString();
                        txtDonGia.Text = row[cls_TaiSan_NhapCT.col_DonGia].ToString();
                        txtThanhTien.Text = row[cls_TaiSan_NhapCT.col_ThanhTien].ToString();
                        _soLuongMin = decimal.Parse(row["SoLuongMin"].ToString());
                        txtDetailID.Text = "" + row[cls_TaiSan_NhapCT.col_Ma];
                        txtHanBaoHanh.Value = Helper.ConvertSToDtime("" + row["SHANBAOHANH"], "dd/MM/yyyy");

                        btnThemCT.Tag = "Edit";
                        #endregion

                    }
                    if (e.ColumnIndex == colXoa.Index) //Button Xóa
                    {
                        if (chkTrangThai.Value)
                        {
                            TA_MessageBox.MessageBox.Show("Phiếu đã đóng, không thể xóa chi tiết.", TA_MessageBox.MessageIcon.Information);
                            return;
                        }
                        #region Xóa
                        DataRowView view = (DataRowView)_bsNhapCT.Current;
                        if (decimal.Parse(view["SoLuongMin"].ToString()) > 0)
                        {
                            MessageBox.Show(string.Format("Đã sử dụng {0} tài sản. Bạn không được xóa tài sản này.", view["SoLuongMin"]), "Thông báo");
                            return;
                        }
                        DialogResult rsl = MessageBox.Show("Bạn có chắc chắn xóa dòng đang chọn không?", "Xác nhận"
                           , MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (rsl == System.Windows.Forms.DialogResult.Yes)
                        {
                            ResultInfo result = _detailService.Delete($"{cls_TaiSan_NhapCT.col_Ma} = '{ view[cls_TaiSan_NhapCT.col_Ma] }'");
                            if (result.Status)
                            {
                                _productService.Delete($"{cls_TaiSan_NhapCTSP.col_NhapCTID} = {view[cls_TaiSan_NhapCT.col_Ma]}");
                                Synchronized();
                            }
                            else
                            {
                                TA_MessageBox.MessageBox.Show("Xóa không thành công:" + result.SystemError, TA_MessageBox.MessageIcon.Error);
                            }

                            lblTong.Text = _bsNhapCT.Count.ToString();
                        }
                        #endregion
                    }
                    if(e.ColumnIndex == col_Detail.Index)
                    {
                        if (chkTrangThai.Value)
                        {
                            TA_MessageBox.MessageBox.Show("Phiếu đã đóng, không thể sửa chi tiết.", TA_MessageBox.MessageIcon.Information);
                            return;
                        }
                        DataRow row = ((DataRowView)dgvMain.Rows[e.RowIndex].DataBoundItem).Row;
                        if (row != null)
                        {
                            frm_ProductDetails frmDetail = new frm_ProductDetails();
                            frmDetail.SetData(_productService, decimal.Parse("" + row[cls_TaiSan_NhapCT.col_Ma]), "" + row[cls_TaiSan_NhapCT.col_TenTaiSan] + " (SL: " + row[cls_TaiSan_NhapCT.col_SoLuong] +")");
                            frmDetail.ShowDialog();
                        }

                    }
                }
                catch
                {
                }
            }
        }

        #endregion

        #region Sự kiện click nút thêm chi tiết

        private void btnThemCT_Click(object sender, EventArgs e)
        {
            if (!_isEdit) return;
            if (chkTrangThai.Value)
            {
                TA_MessageBox.MessageBox.Show("Phiếu đã đóng, không thể thêm chi tiết.", TA_MessageBox.MessageIcon.Information);
                return;
            }
            bool kt = true;
            _masterInfo = GetInfomationMaster();
            if (_masterInfo.Ma == 0)
            {
                kt = SaveMaster();
                if (kt)
                {
                    slbSoPhieu.DataSource = _apiTaiSan.Get_SophieuNhapDangNhap();
                }
            }
            if (kt)
            {
                FillToControl("" + _masterInfo.Ma);
                btnBoQua.Enabled = false;
                ThemChiTiet();
                Synchronized();
            }

        }

        #endregion

        #region Sự kiện thay đổi số lượng

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            this.TinhThanhTien();
        }

        #endregion

        #region Sự kiện thay đổi đơn giá

        private void txtDonGia_TextChanged(object sender, EventArgs e)
        {
            this.TinhThanhTien();
        }

        #endregion

        #region Sự kiện Load form

        private void frm_NhapTaiSan_Load(object sender, EventArgs e)
        {
            this.BoQua();
        }

        #endregion

        #region Sự kiện thêm nhà cung cấp

        private void lblNhaCungCap_Click(object sender, EventArgs e)
        {
            var frm = new frm_DanhMucChung(cls_sys_LoaiDanhMuc.nhaCungCap_MaLoai, cls_sys_LoaiDanhMuc.nhaCungCap_TenLoai, "", "", "", "");
            frm.ShowDialog();
            Load_NhaCungCap();
        }

        #endregion

        #region Sự kiện tạo số thứ tự dòng

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

        #region Sự kiện Enter tại ô số lượng

        private void txtSoLuong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDonGia.Focus();
            }
        }

        #endregion

        #region Sự kiện Enter tại ô đơn giá

        private void txtDonGia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnThemCT.Focus();
            }
        }

        #endregion

        #region Sự kiện nhập số lượng

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        #endregion

        #region Sự kiện nhập đơn giá

        private void txtDonGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        #endregion

        #region Sự kiện chọn tài sản

        private void slbTaiSan_HisSelectChange(object sender, EventArgs e)
        {
            if (slbTaiSan.txtMa.Text != "" && slbTaiSan.txtTen.Text != "")
            {
                txtSoLuong.Focus();
                DataRow row = _apiTaiSan.Get_TaiSanTheoMa(slbTaiSan.txtMa.Text);
                if (row != null)
                {
                    cboDVT.SetTenByMa("" + row[cls_TaiSan_DanhMuc.col_DonViTinh]);
                    txtDonGia.Text = ""+ Helper.ConvertSToDec("0" + row[cls_TaiSan_DanhMuc.col_NguyenGia]);
                    txtSoLuong.Text = "1";
                }
            }
        }

        #endregion

        #region Sự kiện khi chọn Select Box Số Phiếu

        private void slbSoPhieu_HisSelectChange(object sender, EventArgs e)
        {
            FillToControl(slbSoPhieu.txtMa.Text);
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        #endregion

        #region btnHuy_Click (Sự kiện hủy thông tin tài sản)

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (chkTrangThai.Value)
            {
                TA_MessageBox.MessageBox.Show("Phiếu đã đóng, không thể xóa chi tiết.", TA_MessageBox.MessageIcon.Information);
                return;
            }
            ClearDetail();
        }

        #endregion

        #endregion

        private void FillToControl(string ma)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(ma))
                {
                    DataRow row = _apiTaiSan.Get_RowTheoMa(ma);
                    if (row != null)
                    {
                        slbNhaCungCap.txtMa.Text = "" + row["MaNCC"];
                        slbNhaCungCap.txtTen.Text = "" + row["TenNCC"];

                        dtpNgayChungTu.Value = Helper.ConvertSToDtime("" + row[cls_TaiSan_NhapLL.col_NgayNhap]);
                        lblTaoboi.Text = "" + row[cls_TaiSan_NhapLL.col_UserID] + " lúc :" + Helper.ConvertSToDtime("" + row[cls_TaiSan_NhapLL.col_NgayTao]).ToString(Formats.FDateTimeShowControl);

                        try
                        {
                            cboLoaiNhap.SelectedValue = "" + row[cls_TaiSan_NhapLL.col_LoaiNhap];
                        }
                        catch (Exception)
                        {
                            cboLoaiNhap.SelectedIndex = -1;
                        }
                        txtMa.Text = ma;
                        txtSoPhieu.Text = "" + row[cls_TaiSan_NhapLL.col_SoPhieu];
                        txtNguoiGiao.Text = "" + row[cls_TaiSan_NhapLL.col_NguoiGiao];
                        txtSoDTNGuoiGiao.Text = "" + row[cls_TaiSan_NhapLL.col_SoDTLienHe];
                        txtSoXe.Text = "" + row[cls_TaiSan_NhapLL.col_SoXe];
                        txtNoiNhap.Text = "" + row[cls_TaiSan_NhapLL.col_NoiNhap];
                        txtGhiChu.Text = "" + row[cls_TaiSan_NhapLL.col_GhiChu];
                        chkTrangThai.Value = Helper.ConvertSToBool(row[cls_TaiSan_NhapLL.col_TrangThai]);
                        Synchronized();

                    }
                    else
                    {
                        ClearControl();
                    }

                }
            }
            catch
            {
                ClearControl();
            }
        }
        private void Synchronized()
        {
            var dt = _apiTaiSan.Get_TaiSanTheoPhieuNhap(txtMa.Text);
            _bsNhapCT.DataSource = dt;
            dgvMain.DataSource = _bsNhapCT;
        }
        private TaiSan_NhapLLInfo GetInfomationMaster()
        {

            var _masterInfo = new TaiSan_NhapLLInfo();
            _masterInfo.Ma = Helper.ConvertSToDec(txtMa.Text);
            _masterInfo.SoPhieu = txtSoPhieu.Text;
            _masterInfo.NgayNhap = dtpNgayChungTu.Value;
            _masterInfo.NguoiGiao = txtNguoiGiao.Text;
            _masterInfo.SDTNguoiGiao = txtSoDTNGuoiGiao.Text;
            _masterInfo.SoXe = txtSoXe.Text;
            _masterInfo.NoiNhap = txtNoiNhap.Text;
            _masterInfo.GhiChu = txtGhiChu.Text;
            _masterInfo.TrangThai = chkTrangThai.Value == true ? 1 : 0;
            _masterInfo.MaNCC = slbNhaCungCap.txtMa.Text;
            _masterInfo.LoaiNhap = "" + cboLoaiNhap.SelectedValue;

            return _masterInfo;
        }
        private void ClearDetail()
        {
            _bsNhapCT.EndEdit();
            slbTaiSan.txtMa.Text =
            slbTaiSan.txtTen.Text =
            txtDonGia.Text =
            txtSoLuong.Text =
            txtThanhTien.Text = "";
            slbTaiSan.txtTen.Focus();
            txtDetailID.Text = "";
            lblTong.Text = _bsNhapCT.Count.ToString();
            txtHanBaoHanh.Value = DateTime.MinValue;
            _hanBaoHanh = "";
        }
        private void ClearControl()
        {
            txtMa.Text = "0";
            txtSoPhieu.Text = "";
            dtpNgayChungTu.Value = DateTime.Now;
            txtNguoiGiao.Text = "";
            txtSoDTNGuoiGiao.Text = "";
            txtSoXe.Text = "";
            txtNoiNhap.Text = "";
            txtGhiChu.Text = "";
            chkTrangThai.Value = false;
            slbNhaCungCap.clear();
            lblTaoboi.Text = "";

            ClearDetail();
        }
        private void LockControl(bool status)
        {
            txtSoPhieu.ReadOnly = !status;
            dtpNgayChungTu.Enabled = status;
            cboLoaiNhap.Enabled = status;
            slbNhaCungCap.Enabled = status;
            txtNguoiGiao.ReadOnly = !status;
            txtSoDTNGuoiGiao.ReadOnly = !status;
            txtSoXe.ReadOnly = !status;
            txtNoiNhap.ReadOnly = !status;
            txtGhiChu.ReadOnly = !status;
            chkTrangThai.IsReadOnly = !status;

            slbTaiSan.Enabled = status;
            cboDVT.Enabled = status;
            txtSoLuong.ReadOnly = !status;
            txtDonGia.ReadOnly = !status;
            txtThanhTien.ReadOnly = !status;
            txtHanBaoHanh.Enabled = status;


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
        private void InsertProduct(decimal detailID, decimal soluong, string vitrimacdinh)
        {
            
            for (int i = 0; i < soluong; i++)
            {
                TaiSan_NhapCTSPInfo info = new TaiSan_NhapCTSPInfo();
                info.SPID = _productService.CreateNewID(20);
                info.NhapCTID = detailID;
                info.MaVach = "" + info.SPID;
                info.ViTri = vitrimacdinh;
                info.HanBaoHanh = null;
                info.TinhTrang = "TOT";
                info.MoTa = "";
                info.MACHINEID = _productService.GetMachineID();
                info.USERID = E00_System.cls_System.sys_UserID;
                info.USERUD = E00_System.cls_System.sys_UserID;
                _productService.Insert(info);
            }
        }
        public void SetData(string ma, string soPhieu)
        {
            slbSoPhieu.txtMa.Text = ma;
            slbSoPhieu.txtTen.Text = soPhieu;
        }
    }
}
