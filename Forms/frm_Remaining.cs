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
using System.Threading;
using E00_Helpers.Common.Class;
using E00_API.Contract.Kho;

namespace QuanLyTaiSan
{
    public partial class frm_Remaining : frm_DanhMuc
    {
        private api_TaiSan_ThuHoiCT _detailService;
        private api_TaiSan_ThuHoiLL _masterService;
        private api_TaiSanSuDungLL _usingItemService;
        private TaiSan_ThuHoiLLInfo _masterInfo;
        private TaiSan_ThuHoiCTInfo _detailInfo;
        private IAPI<ViTriKhoInfo> _locationService;
        private bool _isEdit = false;
        private ResultInfo _resultInfo;
        private Api_Common _api = new Api_Common();
        private string _userError = string.Empty;
        private string _systemError = string.Empty;
        private SynchronizationContext _context;

        #region Khởi tạo

        #region Khởi tạo chung

        public frm_Remaining()
        {
            InitializeComponent();
            _context = SynchronizationContext.Current;
            E00_Helpers.Helpers.Helper.ControlEventEnter(new Control[] { txtSoPhieu, dtpNgayNhan, slbBanGiamDoc, txtChucVuBGD, slbDonViQuanLy, slbDonViSuDung,
            slbBanGiao1,txtChucVuBenGiao1,slbBanGiao2,txtChucVuBenGiao2,txtGhichu,
            slbTaiSan});
            btnChecNumber.Click += (send, e) => CheckNumber();
            if (cls_System.sys_UserID == "")
            {
                cls_System.sys_UserID = "1";
            }
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
            _masterService = new api_TaiSan_ThuHoiLL();
            _detailService = new api_TaiSan_ThuHoiCT();
            _usingItemService = new api_TaiSanSuDungLL();
            _locationService = new API_Common<ViTriKhoInfo>();

            cboLoai.DataSource = cls_sys_LoaiDanhMuc.GetLoaiThuHoi().ToList();
            cboLoai.DisplayMember = "Value";
            cboLoai.ValueMember = "Key";

            Load_KhoaPhongQuanLy();
            Load_KhoaPhongSuDung();
            Load_TaiSan();
            Load_NhanVien();
            Load_SoPhieu();


            col_TinhTrang.DisplayMember = "Name";
            col_TinhTrang.ValueMember = "Code";
            col_TinhTrang.DataSource = GlobalMember.GetListSatusPart;

            col_ViTri.DisplayMember = cls_D_ViTriKho.col_TenViTri;
            col_ViTri.ValueMember = cls_D_ViTriKho.col_TenViTri;
            col_ViTri.DataSource = _locationService.Get_Data(cls_D_ViTriKho.col_HoatDong + " = 1");

            base.LoadData();

            pnlControl2.Enabled = true;
            slbSoPhieu.txtTen.MaxLength = 20;
            btnXoa.Enabled = false;
            pnlSearch.Enabled = true;
            EndEditMode();
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
            cboLoai.SelectedIndex = 0;
            dtpNgayNhan.Value = DateTime.Now;
            txtSoPhieu.Text = _masterService.GetNumberMax();
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
            Load_TaiSan();
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
                decimal masterID = Helper.ConvertSToDec("0" + txtMaPhieu.Text);
                if (masterID == 0) return;
                DataTable dt = (DataTable)dgvMain.DataSource;
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
                    _resultInfo = _masterService.Delete((object)masterID);
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
                TA_MessageBox.MessageBox.Show("Vui lòng nhập ngày thu hồi.", TA_MessageBox.MessageIcon.Error);
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
                TA_MessageBox.MessageBox.Show("Vui lòng nhập đại diện người bàn giao tài sản.", TA_MessageBox.MessageIcon.Error);
                slbBanGiao1.txtTen.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(txtChucVuBenGiao1.Text))
            {
                TA_MessageBox.MessageBox.Show("Vui lòng nhập chức vụ người bàn giao tài sản.", TA_MessageBox.MessageIcon.Error);
                txtChucVuBenGiao1.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(""+cboLoai.SelectedValue))
            {
                TA_MessageBox.MessageBox.Show("Vui lòng chọn loại thu hồi.", TA_MessageBox.MessageIcon.Error);
                cboLoai.Focus();
                cboLoai.DroppedDown = true;
                return false;
            }
            if (String.IsNullOrEmpty("" + txtLyDo.Text))
            {
                TA_MessageBox.MessageBox.Show("Vui lòng nhập lý do thu hồi.", TA_MessageBox.MessageIcon.Error);
                txtLyDo.Focus();
                return false;
            }

            _masterInfo = GetInfomationMaster();
            if (_masterInfo.THID == 0)
            {
                var rowCheck = _masterService.Get_Item($"{cls_TaiSan_ThuHoiLL.col_SoChungTu} = '{txtSoPhieu.Text}'");
                if (rowCheck != null)
                {
                    TA_MessageBox.MessageBox.Show("Số phiếu đã tồn tại vui lòng nhập số khác.", TA_MessageBox.MessageIcon.Error);
                    txtSoPhieu.Focus();
                    return false;
                }
            }
            if (chkTrangThai.Value)
            {
                DataTable tmpTable = dgvMain.DataSource as DataTable;
                if(tmpTable == null)
                {
                    TA_MessageBox.MessageBox.Show("Vui lòng thêm chi tiết trước khi hoàn thành phiếu.", TA_MessageBox.MessageIcon.Information);
                    return false;
                }
                if(tmpTable.Rows.Count == 0)
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
                    if(_masterInfo.THID == 0)
                    {
                        _masterInfo.THID = _masterService.CreateNewID(20);
                        _masterInfo.MACHINEID = _masterService.GetMachineID();
                        _resultInfo = _masterService.Insert(_masterInfo);
                        if (!_resultInfo.Status)
                        {
                            TA_MessageBox.MessageBox.Show("Cập nhật không thành công." + _resultInfo.SystemError, TA_MessageBox.MessageIcon.Error);
                            return;
                        }else
                        {
                            UpdateDetails();
                        }
                   
                    }
                    else
                    {
                        _resultInfo = _masterService.Update(_masterInfo);
                        if (!_resultInfo.Status)
                        {
                            TA_MessageBox.MessageBox.Show("Cập nhật không thành công." + _resultInfo.SystemError, TA_MessageBox.MessageIcon.Error);
                            return;
                        }
                        else
                        {
                            UpdateDetails();
                        }

                    }
                    base.Luu();
                    EndEditMode();
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
            

            if (!string.IsNullOrEmpty(txtMaPhieu.Text))
            {
                FillToControl(Helper.ConvertSToDec(txtMaPhieu.Text));
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
            else
            {
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                ClearControl();
            }
            EndEditMode();
        }

        #endregion

        #endregion

        #region Phương thức dùng riêng (Private)

        #region Load_TaiSan Load tài sản đã nhập
        /// <summary>
        /// Load tài sản đã nhập
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/05/03</Date>
        private void Load_TaiSan()
        {
            var theard = new Thread(new ThreadStart(() => {
                _context.Send(state => {
                    DataTable _dtTaiSan = _detailService.Get_TaiSanDangSuDung(slbDonViQuanLy.txtMa.Text,slbDonViSuDung.txtMa.Text);
                slbTaiSan.ColumDataList = new string[] { "MA", "TENTAISAN", "MAVACH", "TENKHU", "TENTANG", "TENPHONGCONGNANG", "TENNGUOISUDUNG" };
                slbTaiSan.ColumWidthList = new int[] { 120, 999, 150, 100, 100, 120, 120 };
                slbTaiSan.ValueMember = "MA";
                slbTaiSan.DisplayMember = "TENTAISAN";
                slbTaiSan.DataSource = _dtTaiSan;
                }, null);
            }));
            theard.IsBackground = true;
            theard.Start();
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
        //private void Load_NhanVien(string maBoPhan)
        //{
        //    try
        //    {
        //        List<string> lstCot = new List<string>();
        //        lstCot.Add(cls_NhanSu_LiLichNhanVien.col_MaNhanVien);
        //        lstCot.Add(cls_NhanSu_LiLichNhanVien.col_HoTen);

        //        Dictionary<string, string> dicE = new Dictionary<string, string>();
        //        dicE.Add(cls_NhanSu_LiLichNhanVien.col_MaBoPhan, maBoPhan);

        //        slbNguoiSuDung.DataSource = _api.Search(ref _userError, ref _systemError, cls_NhanSu_LiLichNhanVien.tb_TenBang, lst: lstCot, dicEqual: dicE);
        //    }
        //    catch
        //    {
        //        slbNguoiSuDung.DataSource = null;
        //    }
        //}

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
                
            }
            catch
            {
                slbBanGiamDoc.DataSource =
                slbBanGiao1.DataSource =
                slbBanGiao2.DataSource = null;
               
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
            string maSuDungLL = slbTaiSan.txtMa.Text;
            if (maxterID == 0)
            {
                TA_MessageBox.MessageBox.Show("Phiếu bàn giao chưa được tại, vui lòng tạo trước khi thêm tài sản.", TA_MessageBox.MessageIcon.Error);
                return;
            }
            TaiSanSuDungLLInfo infoDetail = _usingItemService.Get_Item((object)maSuDungLL);
            
            string locationDefault = cls_sys_LoaiDanhMuc.LocationDefault;
            if (infoDetail != null)
            {
                _detailInfo = new TaiSan_ThuHoiCTInfo();
                _detailInfo.THID = maxterID;
                _detailInfo.MaVach = infoDetail.MAVACH;
                _detailInfo.SDID = decimal.Parse(infoDetail.MA);
                _detailInfo.GhiChu = "";
                _detailInfo.TrangThai = 0;
                _detailInfo.NgayTao = DateTime.Now;
                _detailInfo.NgayUD = DateTime.Now;
               
                _detailInfo.NhapCTID = infoDetail.NhapCTID;
                _detailInfo.TinhTrang = infoDetail.TRANGTHAI?? cls_sys_LoaiDanhMuc.StatusDefault;
                DataRow infoTaiSan = _usingItemService.Get_DefaultLocationNhapCT(infoDetail.NhapCTID);
                if (infoTaiSan != null)
                {
                    if (!string.IsNullOrEmpty(""+infoTaiSan[cls_TaiSan_DanhMuc.col_KhoMacDinh]))
                    {
                        locationDefault = "" + infoTaiSan[cls_TaiSan_DanhMuc.col_KhoMacDinh];
                    }
                }
                _detailInfo.ViTri = locationDefault;
                if (_detailInfo.THCTID == 0)
                {
                    _detailInfo.THCTID = _detailService.CreateNewID(20);
                    _detailInfo.MACHINEID = _detailService.GetMachineID();
                }
                _resultInfo = _detailService.InsertOrUpdate(_detailInfo);
                if (!_resultInfo.Status)
                {
                    TA_MessageBox.MessageBox.Show("", TA_MessageBox.MessageIcon.Error);
                    return;
                }
                Load_TaiSan();
                ClearDetail();
            }else
            {
                TA_MessageBox.MessageBox.Show("Không tìm thấy tài sản, vui lòng kiểm tra và thử lại", TA_MessageBox.MessageIcon.Error);
                return;
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
                slbSoPhieu.DataSource = _masterService.Get_SophieuDangXuat();
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
            //try
            //{
            //    if (!_daLuu)
            //    {
            //        MessageBox.Show("Vui lòng Cập nhật trươc khi in.");
            //        return;
            //    }
            //    DataSet ds = new DataSet();

            //    #region Xuất XML thông tin bàn giao

            //    DataTable dtBanGiao = new DataTable();



            //    dtBanGiao.Columns.Add(cls_sys_LoaiDanhMuc.maBanGiamDoc);
            //    dtBanGiao.Columns.Add(cls_sys_LoaiDanhMuc.tenBanGiamDoc);
            //    dtBanGiao.Columns.Add(cls_sys_LoaiDanhMuc.chucVuBanGiamDoc);

            //    dtBanGiao.Columns.Add(cls_sys_LoaiDanhMuc.maBenGiao1);
            //    dtBanGiao.Columns.Add(cls_sys_LoaiDanhMuc.tenBenGiao1);
            //    dtBanGiao.Columns.Add(cls_sys_LoaiDanhMuc.chucVuBenGiao1);

            //    dtBanGiao.Columns.Add(cls_sys_LoaiDanhMuc.maBenGiao2);
            //    dtBanGiao.Columns.Add(cls_sys_LoaiDanhMuc.tenBenGiao2);
            //    dtBanGiao.Columns.Add(cls_sys_LoaiDanhMuc.chucVuBenGiao2);

            //    dtBanGiao.Columns.Add(cls_sys_LoaiDanhMuc.maBenNhan1);
            //    dtBanGiao.Columns.Add(cls_sys_LoaiDanhMuc.tenBenNhan1);
            //    dtBanGiao.Columns.Add(cls_sys_LoaiDanhMuc.chucVuBenNhan1);

            //    dtBanGiao.Columns.Add(cls_sys_LoaiDanhMuc.maBenNhan2);
            //    dtBanGiao.Columns.Add(cls_sys_LoaiDanhMuc.tenBenNhan2);
            //    dtBanGiao.Columns.Add(cls_sys_LoaiDanhMuc.chucVuBenNhan2);

            //    dtBanGiao.Columns.Add(cls_sys_LoaiDanhMuc.diaDiemBanGiao);

            //    DataRow drBanGiao = dtBanGiao.NewRow();
            //    drBanGiao[cls_sys_LoaiDanhMuc.maBanGiamDoc] = slbBanGiamDoc.txtMa.Text;
            //    drBanGiao[cls_sys_LoaiDanhMuc.tenBanGiamDoc] = slbBanGiamDoc.txtTen.Text;
            //    drBanGiao[cls_sys_LoaiDanhMuc.chucVuBanGiamDoc] = txtChucVuBenGiao1.Text;

            //    drBanGiao[cls_sys_LoaiDanhMuc.maBenGiao1] = slbBanGiao1.txtMa.Text;
            //    drBanGiao[cls_sys_LoaiDanhMuc.tenBenGiao1] = slbBanGiao1.txtTen.Text;
            //    drBanGiao[cls_sys_LoaiDanhMuc.chucVuBenGiao1] = txtChucVuBenNhan1.Text;

            //    drBanGiao[cls_sys_LoaiDanhMuc.maBenGiao2] = slbBanGiao2.txtMa.Text;
            //    drBanGiao[cls_sys_LoaiDanhMuc.tenBenGiao2] = slbBanGiao2.txtTen.Text;
            //    drBanGiao[cls_sys_LoaiDanhMuc.chucVuBenGiao2] = txtChucVuBenGiao2.Text;

            //    drBanGiao[cls_sys_LoaiDanhMuc.maBenNhan1] = slbBenNhan1.txtMa.Text;
            //    drBanGiao[cls_sys_LoaiDanhMuc.tenBenNhan1] = slbBenNhan1.txtTen.Text;
            //    drBanGiao[cls_sys_LoaiDanhMuc.chucVuBenNhan1] = txtChucVuBenNhan2.Text;

            //    drBanGiao[cls_sys_LoaiDanhMuc.maBenNhan2] = slbBenNhan2.txtMa.Text;
            //    drBanGiao[cls_sys_LoaiDanhMuc.tenBenNhan2] = slbBenNhan2.txtTen.Text;
            //    drBanGiao[cls_sys_LoaiDanhMuc.chucVuBenNhan2] = txtChucVuBGD.Text;

            //    drBanGiao[cls_sys_LoaiDanhMuc.diaDiemBanGiao] = txtDiaDiemBanGiao.Text;

            //    dtBanGiao.Rows.Add(drBanGiao);

            //    #endregion

            //    #region Xuất XML thông tin bàn giao chi tiết
            //    //nuoc sx, nam sx, quy cach, don gia, tai lieu ky thuat kem theo
            //    // DataTable dtBanGiaoChiTiet = ((DataTable)_bsNhanTaiSan.DataSource).Select().CopyToDataTable();
            //    DataTable dtBanGiaoChiTiet = _apiTaiSan.Get_SuDungLLTheoSoPhieu(slbSoPhieu.txtTen.Text);
            //    dtBanGiaoChiTiet.Columns.Add(cls_TaiSan_DanhMuc.col_NuocSanXuat);
            //    dtBanGiaoChiTiet.Columns.Add(cls_TaiSan_DanhMuc.col_NamSanXuat);
            //    dtBanGiaoChiTiet.Columns.Add(cls_TaiSan_DanhMuc.col_QuyCach);
            //    dtBanGiaoChiTiet.Columns.Add(cls_TaiSan_DanhMuc.col_TaiLieu);
            //    dtBanGiaoChiTiet.Columns.Add(cls_TaiSan_NhapCT.col_DonGia);
            //    DataRow drTaiSan;
            //    foreach (DataRow item in dtBanGiaoChiTiet.Rows)
            //    {
            //        try
            //        {
            //            drTaiSan = _apiTaiSan.Get_TaiSanTheoMa(item[cls_TaiSan_SuDungLL.col_MaTaiSan].ToString());
            //            item[cls_TaiSan_SuDungLL.col_TenTaiSan] = drTaiSan[cls_TaiSan_DanhMuc.col_Ten];
            //            item[cls_TaiSan_DanhMuc.col_NuocSanXuat] = drTaiSan[cls_TaiSan_DanhMuc.col_NuocSanXuat];
            //            item[cls_TaiSan_DanhMuc.col_NamSanXuat] = drTaiSan[cls_TaiSan_DanhMuc.col_NamSanXuat];
            //            item[cls_TaiSan_DanhMuc.col_QuyCach] = drTaiSan[cls_TaiSan_DanhMuc.col_QuyCach];
            //            item[cls_TaiSan_DanhMuc.col_TaiLieu] = drTaiSan[cls_TaiSan_DanhMuc.col_TaiLieu];
            //            item[cls_TaiSan_NhapCT.col_DonGia] = _apiTaiSan.Get_NhapCTTheoMa(item[cls_TaiSan_SuDungLL.col_MaNhapCT].ToString())[cls_TaiSan_NhapCT.col_DonGia];
            //        }
            //        catch
            //        {
            //        }
            //    }

            //    #endregion

            //    dtBanGiao.TableName = "BanGiao";
            //    dtBanGiaoChiTiet.TableName = "BanGiaoChiTiet";

            //    if (dtBanGiao.Rows.Count > 0)
            //    {
            //        ds.Tables.Add(dtBanGiao);
            //        ds.Tables.Add(dtBanGiaoChiTiet);
            //        if (!System.IO.Directory.Exists("..//xml"))
            //        {
            //            System.IO.Directory.CreateDirectory("..//xml");
            //        }
            //        ds.WriteXml("..//xml//ts_BienBanBanGiao.xml", XmlWriteMode.WriteSchema);
            //        if (!System.IO.File.Exists("..\\..\\..\\Report\\ts_BienBanBanGiao.rpt"))
            //        {
            //            MessageBox.Show("Thiếu file: ts_BienBanBanGiao.rpt trong thư mục Report.");
            //            return;
            //        }
            //        ReportDocument rptDoc = new ReportDocument();
            //        rptDoc.Load("..\\..\\..\\Report\\ts_BienBanBanGiao.rpt", OpenReportMethod.OpenReportByDefault);
            //        rptDoc.SetDataSource(ds);
            //        frm_Report frm = new frm_Report(rptDoc,"");
            //        frm.ShowDialog();
            //    }
            //    else
            //    {
            //        MessageBox.Show("In thất bại. Vui lòng kiểm tra lại kết nối máy in");
            //    }
            //}
            //catch
            //{
            //    MessageBox.Show("In thất bại");
            //}
        }

        #endregion

        #endregion

        #endregion

        #region Sự kiện

        #region frm_SuDung_Load (Sự kiện Load form)

        private void frm_SuDung_Load(object sender, EventArgs e)
        {
            if(Helper.ConvertSToDec(txtMaPhieu.Text) > 0)
            {
                FillToControl(Helper.ConvertSToDec(txtMaPhieu.Text));
            }
            EndEditMode();

            btnSua.Enabled = true;
        }

        #endregion

        #region dgvMain_CellClick (Sự kiện chọn sửa/xóa)

        private void dgvMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvMain.RowCount > 0 && e.RowIndex > -1)
            {
                try
                {


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
                                object id = rowSelect.Row[cls_TaiSan_ThuHoiCT.col_THCTID];
                                _resultInfo = _detailService.Delete(rowSelect.Row[cls_TaiSan_ThuHoiCT.col_THCTID]);
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
            if (_masterInfo.THID == 0)
            {
                ResultInfo result = SaveMaster();
                kt = result.Status;
                if (kt)
                {
                    Load_SoPhieu();
                }
            }
            if (kt)
            {
                FillToControl(_masterInfo.THID);
                btnBoQua.Enabled = false;
                ThemChiTiet();
                Synchronized();
               
            }
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
            txtMaPhieu.Text = maPhieu;
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
            
            txtLyDo.ReadOnly = !status;
            cboLoai.Enabled = status;
            slbBanGiao2.Enabled = status;
            txtChucVuBenGiao2.ReadOnly = !status;
            txtGhichu.ReadOnly = !status;
            chkTrangThai.IsReadOnly = !status;

            slbTaiSan.Enabled = status;
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
            txtLyDo.Text = "";
            cboLoai.SelectedIndex = -1;
            slbBanGiao2.clear();
            txtChucVuBenGiao2.Text = "";
            txtGhichu.Text = "";
            chkTrangThai.Value = false;
            lblTaoboi.Text = "";
            ClearDetail();
        }
        private void FillToControl(decimal ma)
        {
            try
            {
                DataRow row = _masterService.Get_PhieuThuhoiTheoMa(ma);
                if (row != null)
                {
                    txtMaPhieu.Text = "" + ma;
                    txtSoPhieu.Text = "" + row[cls_TaiSan_ThuHoiLL.col_SoChungTu];
                    dtpNgayNhan.Value = Helper.ConvertSToDtime("" + row[cls_TaiSan_ThuHoiLL.col_Ngay]);
                    slbBanGiamDoc.txtMa.Text = "" + row[cls_TaiSan_ThuHoiLL.col_DaiDien];
                    slbBanGiamDoc.txtTen.Text = "" + row[@"TENNGUOIDD"];

                    txtChucVuBGD.Text = "" + row[cls_TaiSan_ThuHoiLL.col_ChuVuDD];
                    slbDonViQuanLy.txtMa.Text = "" + row[cls_TaiSan_ThuHoiLL.col_KhoaQL];
                    slbDonViQuanLy.txtTen.Text = "" + row["TENKPQL"];
                    slbDonViSuDung.txtMa.Text = "" + row[cls_TaiSan_ThuHoiLL.col_KhoaSuDung];
                    slbDonViSuDung.txtTen.Text = "" + row["TENKPSD"];
                    slbBanGiao1.txtMa.Text = "" + row[cls_TaiSan_ThuHoiLL.col_NguoiGiao1];
                    slbBanGiao1.txtTen.Text = "" + row["TENNGUOIGIAO1"];

                    slbBanGiao2.txtMa.Text = "" + row[cls_TaiSan_ThuHoiLL.col_NguoiGiao2];
                    slbBanGiao2.txtTen.Text = "" + row["TENNGUOIGIAO2"];

                    txtLyDo.Text = "" + row[cls_TaiSan_ThuHoiLL.col_LyDo];
                    try
                    {
                        cboLoai.SelectedValue = "" + row[cls_TaiSan_ThuHoiLL.col_LoaiTH];
                    }
                    catch (Exception)
                    {
                        cboLoai.SelectedIndex = -1;
                    }

                    txtChucVuBenGiao1.Text = "" + row[cls_TaiSan_ThuHoiLL.col_ChucVuNG1];
                    txtChucVuBenGiao2.Text = "" + row[cls_TaiSan_ThuHoiLL.col_ChucVuNG2];

                    txtGhichu.Text = "" + row[cls_TaiSan_ThuHoiLL.col_GhiChu];
                    chkTrangThai.Value = Helper.ConvertSToBool("" + row[cls_TaiSan_ThuHoiLL.col_TrangThai]);
                    lblTaoboi.Text = $"Tạo bởi: { row[cls_TaiSan_ThuHoiLL.col_UserID]} lúc: {Helper.ConvertSToDtime("" + row[cls_TaiSan_ThuHoiLL.col_NgayTao]).ToString("dd/MM/yy hh: mm")} ";
                    Synchronized();

                    btnSua.Enabled = true;
                    Load_TaiSan();
                }
            }
            catch
            {
                ClearControl();
            }
        }
        private TaiSan_ThuHoiLLInfo GetInfomationMaster()
        {
            _masterInfo = new TaiSan_ThuHoiLLInfo();
            _masterInfo.THID = Helper.ConvertSToDec("0" + txtMaPhieu.Text);
            _masterInfo.SoChungTu = txtSoPhieu.Text;
            _masterInfo.Ngay = dtpNgayNhan.Value;
            _masterInfo.LoaiTH = "" + cboLoai.SelectedValue;
            _masterInfo.LyDo = txtLyDo.Text;
            _masterInfo.DaiDien = slbBanGiamDoc.txtMa.Text;
            _masterInfo.ChuVuDD = txtChucVuBGD.Text;
            _masterInfo.KhoaQL = slbDonViQuanLy.txtMa.Text;
            _masterInfo.KhoaSuDung = slbDonViSuDung.txtMa.Text;
            _masterInfo.NguoiGiao1 = slbBanGiao1.txtMa.Text;
            _masterInfo.NguoiGiao2 = slbBanGiao2.txtMa.Text;
            _masterInfo.ChucVuNG1 = txtChucVuBenGiao1.Text;
            _masterInfo.ChucVuNG2 = txtChucVuBenGiao2.Text;

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
                    if (_masterInfo.THID == 0)
                    {
                        _masterInfo.THID = _masterService.CreateNewID(20);
                        _masterInfo.MACHINEID = _masterService.GetMachineID();
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
            DataTable dtBanGiao = _detailService.Get_DetailList(Helper.ConvertSToDec(txtMaPhieu.Text));
            dgvMain.DataSource = dtBanGiao;
        }
        private void CheckNumber()
        {
            var rowCheck = _masterService.Get_Item($"{cls_TaiSan_ThuHoiLL.col_SoChungTu} = '{txtSoPhieu.Text}'");
            if (rowCheck != null)
            {
                if(""+rowCheck.THID != txtMaPhieu.Text)
                {
                    TA_MessageBox.MessageBox.Show("Số phiếu đã tồn tại vui lòng nhập số khác.", TA_MessageBox.MessageIcon.Error);
                    txtSoPhieu.Focus();
                    return;
                }
                TA_MessageBox.MessageBox.Show("Số phiếu chưa được sử dụng, bạn có thể sử dụng số phiếu này.", TA_MessageBox.MessageIcon.Error);
            }
        }

        public void UpdateDetails()
        {
            DataTable data = dgvMain.DataSource as DataTable;
            if(data != null)
            {
                if(data.Rows.Count > 0)
                {
                    Dictionary<string, string> dicColumnSet = new Dictionary<string, string>();
                    dicColumnSet.Add(cls_TaiSan_ThuHoiCT.col_ViTri, cls_TaiSan_ThuHoiCT.col_ViTri);
                    dicColumnSet.Add(cls_TaiSan_ThuHoiCT.col_TinhTrang, cls_TaiSan_ThuHoiCT.col_TinhTrang);
                    _resultInfo = _detailService.Update(dicColumnSet, data);
                    if (!_resultInfo.Status)
                    {
                        TA_MessageBox.MessageBox.Show("Cập nhật thông tin chi tiếc không thành công, vui lòng kiểm tra và thử lại.");
                        return;
                    }
                  
                }
            }
        }

    }
}
