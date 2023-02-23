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
using E00_API.Contract.TaiSan;
using E00_SafeCacheDataService.Base;
using E00_SafeCacheDataService.Common;
using QuanLyTaiSan.Forms;
using E00_API.TaiSan;

namespace QuanLyTaiSan
{
    public partial class frm_DispatchingOrders : frm_DanhMuc
    {

        #region Biến toàn cục
        private TaiSan_XuatLLInfo _masterInfo;
        private TaiSan_XuatCTInfo _detailInfo;
        private bool _isEdit = false;
        private IAPI<TaiSan_NhapCTSPInfo> _productService;
        private api_TaiSan_XuatCT _detailService;
        private api_TaiSan_XuatLL _masterService;
        private ResultInfo _resultInfo;
        private api_TaiSan _apiTaiSan;
        #endregion

        #region Khởi tạo

        #region Khởi tạo chung

        public frm_DispatchingOrders()
        {
            InitializeComponent();
            E00_Helpers.Helpers.Helper.ControlEventEnter(new Control[] { txtSoPhieu, dtpNgayChungTu, cboLoai
            ,txtGhiChu,chkTrangThai,slbTaiSan} );
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
            _detailService = new api_TaiSan_XuatCT();
            _masterService = new api_TaiSan_XuatLL();
            _apiTaiSan = new api_TaiSan();

            Load_TaiSan();

            slbSoPhieu.DataSource = _masterService.Get_SophieuDangXuat();

            cboLoai.DataSource = cls_sys_LoaiDanhMuc.GetLoaiXuat().ToList();
            cboLoai.DisplayMember = "Value";
            cboLoai.ValueMember = "Key";

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
            txtSoPhieu.Text = _masterService.GetNumberMax();
            cboLoai.SelectedIndex = 0;
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
                if (chkTrangThai.Value)
                {
                    TA_MessageBox.MessageBox.Show("Phiếu đã đóng, không thể xóa chi tiết.", TA_MessageBox.MessageIcon.Information);
                    return;
                }
                if (_detailService.CheckExist(Helper.ConvertSToDec("0"+txtMa.Text)))
                {
                    TA_MessageBox.MessageBox.Show("Vui lòng xóa chi tiết trước khi xóa phiếu.", TA_MessageBox.MessageIcon.Information);
                }
                else
                {
                    _resultInfo = _masterService.Delete((object)txtMa.Text);
                   if (!_resultInfo.Status)
                    {
                        TA_MessageBox.MessageBox.Show("Xóa thất bại, vui lòng kiểm tra và thử lại."+ _resultInfo.SystemError, TA_MessageBox.MessageIcon.Information);
                    }else
                    {
                        slbSoPhieu.DataSource = _masterService.Get_SophieuDangXuat();
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
            if (String.IsNullOrEmpty(txtSoPhieu.Text))
            {
                TA_MessageBox.MessageBox.Show("Vui lòng nhập số phiếu.", TA_MessageBox.MessageIcon.Error);
                slbSoPhieu.txtTen.Focus();
                return false;
            }
            if (String.IsNullOrEmpty("" + cboLoai.SelectedValue))
            {
                TA_MessageBox.MessageBox.Show("Vui lòng chọn loại xuất.", TA_MessageBox.MessageIcon.Error);
                slbSoPhieu.txtTen.Focus();
                return false;
            }
            _masterInfo = GetInfomationMaster();

            if (_masterInfo.XuatID == 0)
            {
                var rowCheck = _masterService.Get_Item(_masterInfo.SoChungTu);
                if (rowCheck != null)
                {
                    TA_MessageBox.MessageBox.Show("Số phiếu đã tồn tại vui lòng nhập số khác.", TA_MessageBox.MessageIcon.Error);
                    txtSoPhieu.Focus();
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
                    #region Thêm phiếu nhập kho mới

                    if (_masterInfo.XuatID == 0)
                    {
                        _masterInfo.XuatID = _masterService.CreateNewID(20);
                        _masterInfo.MACHINEID = _masterService.GetMachineID();
                        _resultInfo = _masterService.Insert(_masterInfo);
                        if (!_resultInfo.Status)
                        {
                            TA_MessageBox.MessageBox.Show("Cập nhật không thành công."+ _resultInfo.SystemError, TA_MessageBox.MessageIcon.Information);
                        }
                        base.Luu();
                        EndEditMode();
                        btnSua.Enabled = true;
                        dgvMain.Enabled = false;
                        slbSoPhieu.DataSource = _masterService.Get_SophieuDangXuat();
                    }

                    #endregion

                    #region Sửa phiếu nhập kho

                    else
                    {
                        _resultInfo = _masterService.Update(_masterInfo);
                        if (!_resultInfo.Status)
                        {
                            TA_MessageBox.MessageBox.Show("Cập nhật không thành công."+ _resultInfo.SystemError, TA_MessageBox.MessageIcon.Information);
                        }
                        base.Luu();
                        EndEditMode();
                        btnSua.Enabled = true;
                        dgvMain.Enabled = false;
                        slbSoPhieu.DataSource = _masterService.Get_SophieuDangXuat();
                    }

                    #endregion
                }
            }
            catch
            {
            }
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
                    if (_masterInfo.XuatID == 0)
                    {
                        _masterInfo.XuatID = _masterService.CreateNewID(20);
                        _masterInfo.MACHINEID = _masterService.GetMachineID();
                        return _masterService.Insert(_masterInfo).Status;
                    }
                    else
                    {
                        return _masterService.Update(_masterInfo).Status;
                    }
                    return false;
                }
                return false;
            }
            catch
            {
                return false;
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
            DataTable _dtTaiSan = _apiTaiSan.Get_StockOnhand();

            slbTaiSan.ColumDataList = new string[] { "SPID", "TENTAISAN", "MAVACH", "VITRI", "TINHTRANG", "HANBAOHANH" };
            slbTaiSan.ColumWidthList = new int[] { 120, 150,150, 100, 100, 120 };
            slbTaiSan.ValueMember = "SPID";
            slbTaiSan.DisplayMember = "TENTAISAN";
            slbTaiSan.DataSource = _dtTaiSan;

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
                decimal spid = Helper.ConvertSToDec("0" + slbTaiSan.txtMa.Text);
                decimal xuatID = Helper.ConvertSToDec("0" + txtMa.Text);
                DataRow row = _apiTaiSan.Get_StockOnhandBySPID(spid);
                if (row != null)
                {
                if (spid > 0 && xuatID > 0)
                {
                    _detailInfo = new TaiSan_XuatCTInfo();
                    _detailInfo.XuatCTID = _detailService.CreateNewID(20);
                    _detailInfo.XuatID = xuatID;
                    _detailInfo.SPID = spid;
                    _detailInfo.MaVach =""+ row[cls_TaiSan_NhapCTSP.col_MaVach];
                    _resultInfo = _detailService.Insert(_detailInfo);
                    if (!_resultInfo.Status)
                    {
                        TA_MessageBox.MessageBox.Show("Thêm không thành công, vui lòng kiểm tra và thử lại." + _resultInfo.SystemError, TA_MessageBox.MessageIcon.Information);
                    }
                    else
                    {
                        Synchronized();
                        Load_TaiSan();
                        ClearDetail();
                        slbTaiSan.Focus();
                    }
                }
                }
                else
                {
                    TA_MessageBox.MessageBox.Show("Tài sản không tồn tại, vui lòng kiểm tra và thử lại.", TA_MessageBox.MessageIcon.Error);
                }

            }
            catch (Exception ex)
            {
                //Console.Write(ex);
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
                    DataRowView rowSelect = dgvMain.CurrentRow.DataBoundItem as DataRowView;
                    if (chkTrangThai.Value)
                    {
                        TA_MessageBox.MessageBox.Show("Phiếu đã đóng, không thể xóa chi tiết.", TA_MessageBox.MessageIcon.Information);
                        return;
                    }
                    if (e.ColumnIndex == colXoa.Index) //Button Xóa
                    {
                        #region Xóa

                        if (TA_MessageBox.MessageBox.Show("Bạn có chắc chắn xóa dòng đang chọn không?", TA_MessageBox.MessageIcon.Question) == DialogResult.Yes)
                        {
                            ResultInfo result = _detailService.Delete($"{cls_TaiSan_XuatCT.col_XuatCTID} = { rowSelect.Row[cls_TaiSan_XuatCT.col_XuatCTID] }");
                            if (result.Status)
                            {
                                Synchronized();
                                Load_TaiSan();
                                slbTaiSan.Focus();
                            }
                            else
                            {
                                TA_MessageBox.MessageBox.Show("Xóa không thành công:" + result.SystemError, TA_MessageBox.MessageIcon.Error);
                            }
                        }
                        #endregion
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
            if (chkTrangThai.Value)
            {
                TA_MessageBox.MessageBox.Show("Phiếu đã đóng, không thể thêm chi tiết.", TA_MessageBox.MessageIcon.Information);
                return;
            }
            bool kt = true;
            _masterInfo = GetInfomationMaster();
            if (_masterInfo.XuatID == 0)
            {
                kt = SaveMaster();
                slbSoPhieu.DataSource = _masterService.Get_SophieuDangXuat();
            }
            if (kt)
            {
                FillToControl("" + _masterInfo.XuatID);
                btnBoQua.Enabled = false;
                ThemChiTiet();
                Synchronized();
            }

        }

        #endregion

        #region Sự kiện Load form

        private void frm_NhapTaiSan_Load(object sender, EventArgs e)
        {
            this.BoQua();
           
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

        #region Sự kiện Enter tại ô đơn giá

        private void txtDonGia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnThemCT.Focus();
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
                    _masterInfo = _masterService.Get_Item((object)ma);
                    if (_masterInfo != null)
                    {

                        dtpNgayChungTu.Value = _masterInfo.NgayXuat;
                        lblTaoboi.Text = "" + _masterInfo.USERID + " lúc :" + _masterInfo.NgayTao == null?"": _masterInfo.NgayTao.Value.ToString(Formats.FDateTimeShowControl);

                        try
                        {
                            cboLoai.SelectedValue = _masterInfo.LoaiXuat;
                        }
                        catch (Exception)
                        {
                            cboLoai.SelectedIndex = -1;
                        }
                        txtMa.Text = ma;
                        txtSoPhieu.Text = _masterInfo.SoChungTu;
                        txtNoiXuat.Text = _masterInfo.NoiXuat;
                        txtGhiChu.Text = _masterInfo.GhiChu;
                        chkTrangThai.Value = Helper.ConvertSToBool(_masterInfo.TrangThai);
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
            dgvMain.DataSource = _detailService.Get_Detail(Helper.ConvertSToDec(txtMa.Text));
        }
        private TaiSan_XuatLLInfo GetInfomationMaster()
        {

            var _masterInfo = new TaiSan_XuatLLInfo();
            _masterInfo.XuatID = Helper.ConvertSToDec(txtMa.Text);
            _masterInfo.SoChungTu = txtSoPhieu.Text;
            _masterInfo.NgayXuat = dtpNgayChungTu.Value;
            _masterInfo.GhiChu = txtGhiChu.Text;
            _masterInfo.TrangThai = chkTrangThai.Value == true ? 1 : 0;
            _masterInfo.LoaiXuat = "" + cboLoai.SelectedValue;
            _masterInfo.NoiXuat = txtNoiXuat.Text;
            return _masterInfo;
        }
        private void ClearDetail()
        {
            slbTaiSan.txtMa.Text =
            slbTaiSan.txtTen.Text = "";
            slbTaiSan.txtTen.Focus();
            txtDetailID.Text = "";
        }
        private void ClearControl()
        {
            txtMa.Text = "0";
            txtSoPhieu.Text = "";
            dtpNgayChungTu.Value = DateTime.Now;
            txtGhiChu.Text = "";
            chkTrangThai.Value = false;
            lblTaoboi.Text = "";
            txtNoiXuat.Text = "";
            cboLoai.SelectedIndex = -1;
            ClearDetail();
        }
        private void LockControl(bool status)
        {
            txtSoPhieu.ReadOnly = !status;
            dtpNgayChungTu.Enabled = status;
            cboLoai.Enabled = status;
       
            txtGhiChu.ReadOnly = !status;
            txtNoiXuat.ReadOnly = !status;
            chkTrangThai.IsReadOnly = !status;
            slbTaiSan.Enabled = status;

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

        public void SetData(string ma, string soPhieu)
        {
            slbSoPhieu.txtMa.Text = ma;
            slbSoPhieu.txtTen.Text = soPhieu;
        }
    }
}
