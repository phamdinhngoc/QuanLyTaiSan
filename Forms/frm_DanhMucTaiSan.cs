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
using System.Text.RegularExpressions;
using QuanLyTaiSan;
using E00_Base;
using E00_SafeCacheDataService.Interface;
using E00_API.Contract.Kho;
using E00_SafeCacheDataService.Base;
using E00_Helpers.Helpers;

namespace QuanLyTaiSan
{
    public partial class frm_DanhMucTaiSan : frm_DanhMuc
    {

        #region Biến toàn cục
        private IAPI<ViTriKhoInfo> _locationService;

        private Api_Common _api = new Api_Common();
        public Acc_Oracle _acc = new Acc_Oracle();
        private int _rowIndex = 0;
        private bool _isAdd = true;
        private string _userError = string.Empty;
        private string _systemError = string.Empty;
        BindingSource _bsTaiSan = new BindingSource();
        DataRowView _rowView;
        Dictionary<string, string> _dicTaiSan = new Dictionary<string, string>();
        List<string> _lstKey = new List<string>();
        List<string> _lstUnique = new List<string>();
        Dictionary<string, string> _dicWhere = new Dictionary<string, string>();

        #endregion

        #region Khởi tạo

        #region Khởi tạo chung

        public frm_DanhMucTaiSan()
        {
            InitializeComponent();
            _api.KetNoi();
            dgvMain.Columns["colTen"].Frozen = true;
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
            _locationService = new API_Common<ViTriKhoInfo>();
            Load_LoaiTaiSan();
            Load_TaiSan();
            Load_DonViTinh();
            Load_TinhChat();

            cboKhoMacDinh.DisplayMember = cls_D_ViTriKho.col_TenViTri;
            cboKhoMacDinh.ValueMember = cls_D_ViTriKho.col_TenViTri;
            cboKhoMacDinh.DataSource = _locationService.Get_Data(cls_D_ViTriKho.col_HoatDong + " = 1");
            cboKhoMacDinh.SelectedIndex = -1;

            gvgMain.Initialize();
            base.LoadData();
            pnlControl2.Enabled = false;
            dgvMain.ReadOnly = true;


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
            btnIn.Enabled = false;
            chkXemTruoc.Enabled = false;
            btnIn2.Enabled = false;
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

        #region Them (Mặc định các giá trị khi thêm mới)
        /// <summary>
        /// Mặc định các giá trị khi thêm mới
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <date>2018/04/23</date>
        protected override void Them()
        {
            base.Them();
            _isAdd = true;
            txtTen.Text =
            txtKyHieu.Text =
            txtMaVachSanXuat.Text =
            slbLoaiTaiSan.txtMa.Text =
            slbLoaiTaiSan.txtTen.Text =
            txtNuocSanXuat.Text =
            txtQuyCach.Text =
            txtMaVachSanXuat.Text =
            txtMoTa.Text =
            txtModel.Text =
            txtTaiLieuKyThuat.Text = "";
            chkTamNgung.Value = false;
            txtTen.Focus();

            intGiaGoc.Value = 0;
            douSLTonTT.Value = 0;
            intHanBaoHanh.Value = 0;

            cboTinhChat.SelectedIndex =
            cboDonViTinh.SelectedIndex =
            cboKhoMacDinh.SelectedIndex = -1;
        }

        #endregion

        #region Sua (Mặc định các giá trị khi sửa)
        /// <summary>
        /// Mặc định các giá trị khi sửa
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <date>2018/04/23</date>
        protected override void Sua()
        {
            base.Sua();
            _isAdd = false;
            txtTen.Focus();
        }

        #endregion

        #region Xoa (Thực hiện khi gọi sự kiện Xóa)
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

                DataTable dt = (DataTable)_bsTaiSan.DataSource;
                foreach (DataRow row in dt.Select("Chon = 1"))
                {
                    lstTen += row[cls_TaiSan_DanhMuc.col_Ten].ToString() + "\n";
                    lstID += row[cls_TaiSan_DanhMuc.col_ID].ToString() + ",";
                }
                if (lstTen == "")
                {
                    DataRowView view = (DataRowView)_bsTaiSan.Current;
                    lstTen = view[cls_TaiSan_DanhMuc.col_Ten].ToString();
                    lstID = view[cls_TaiSan_DanhMuc.col_ID].ToString();
                }
                else
                {
                    lstID = lstID.Substring(0, lstID.Length - 1);
                }
                DialogResult rsl = MessageBox.Show("Bạn có chắc chắn muốn xóa dữ liệu:" + lstTen, "Xác nhận"
                                        , MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rsl == System.Windows.Forms.DialogResult.Yes)
                {
                    if (_api.DeleteAll(ref _userError, ref _systemError, cls_TaiSan_DanhMuc.tb_TenBang, lstID))
                    {
                        Load_TaiSan();
                        TimKiem();
                    }
                    else
                    {
                        TA_MessageBox.MessageBox.Show(_userError, TA_MessageBox.MessageIcon.Error);
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
            if (String.IsNullOrEmpty(txtTen.Text))
            {
                TA_MessageBox.MessageBox.Show("Tên không được bỏ trống. Vui lòng nhập lại.", TA_MessageBox.MessageIcon.Error);
                txtTen.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(slbLoaiTaiSan.txtMa.Text))
            {
                TA_MessageBox.MessageBox.Show("Loại tài sản không được bỏ trống. Vui lòng nhập lại.", TA_MessageBox.MessageIcon.Error);
                slbLoaiTaiSan.txtTen.Focus();
                return false;
            }
            if (String.IsNullOrEmpty("" + cboTinhChat.SelectedValue))
            {
                TA_MessageBox.MessageBox.Show("Vui lòng chọn tính chất của tài sản.", TA_MessageBox.MessageIcon.Error);
                cboTinhChat.Focus();
                cboTinhChat.DroppedDown = true;
                return false;
            }
            if (String.IsNullOrEmpty("" + cboDonViTinh.SelectedValue))
            {
                TA_MessageBox.MessageBox.Show("Vui lòng chọn đơn vị tính.", TA_MessageBox.MessageIcon.Error);
                cboDonViTinh.Focus();
                cboDonViTinh.DroppedDown = true;
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
                if (Check_Data())
                {
                    _dicTaiSan.Clear();
                    _lstUnique.Clear();
                    _dicWhere.Clear();
                    _dicTaiSan.Add(cls_TaiSan_DanhMuc.col_Ten, txtTen.Text);
                    _dicTaiSan.Add(cls_TaiSan_DanhMuc.col_KyHieu, txtKyHieu.Text);
                    _dicTaiSan.Add(cls_TaiSan_DanhMuc.col_MaVachSanXuat, txtMaVachSanXuat.Text);
                    _dicTaiSan.Add(cls_TaiSan_DanhMuc.col_MaLoai, slbLoaiTaiSan.txtMa.Text);
                    _dicTaiSan.Add(cls_TaiSan_DanhMuc.col_TenLoai, slbLoaiTaiSan.txtTen.Text);

                    _dicTaiSan.Add(cls_TaiSan_DanhMuc.col_NuocSanXuat, txtNuocSanXuat.Text);
                    _dicTaiSan.Add(cls_TaiSan_DanhMuc.col_QuyCach, txtQuyCach.Text);
                    _dicTaiSan.Add(cls_TaiSan_DanhMuc.col_TaiLieu, txtTaiLieuKyThuat.Text);
                    _dicTaiSan.Add(cls_TaiSan_DanhMuc.col_TamNgung, chkTamNgung.Value ? "1" : "0");
                    _dicTaiSan.Add(cls_TaiSan_DanhMuc.col_UserID, cls_System.sys_UserID);
                    _dicTaiSan.Add(cls_TaiSan_DanhMuc.col_UserUD, cls_System.sys_UserID);
                    _dicTaiSan.Add(cls_TaiSan_DanhMuc.col_NgayTao, _acc.Get_YYYYMMddHHmmss());
                    _dicTaiSan.Add(cls_TaiSan_DanhMuc.col_NgayUD, _acc.Get_YYYYMMddHHmmss());
                    _dicTaiSan.Add(cls_TaiSan_DanhMuc.col_MachineID, cls_Common.Get_MachineID());
                    _dicTaiSan.Add(cls_TaiSan_DanhMuc.col_MoTa, txtMoTa.Text);
                    _dicTaiSan.Add(cls_TaiSan_DanhMuc.col_Model, txtModel.Text);
                    _dicTaiSan.Add(cls_TaiSan_DanhMuc.col_BaoHanh, "" + intHanBaoHanh.Value);
                    _dicTaiSan.Add(cls_TaiSan_DanhMuc.col_KhoMacDinh, "" + cboKhoMacDinh.SelectedValue);
                    _dicTaiSan.Add(cls_TaiSan_DanhMuc.col_DonViTinh, "" + cboDonViTinh.SelectedValue);
                    _dicTaiSan.Add(cls_TaiSan_DanhMuc.col_NguyenGia, "" + intGiaGoc.Value);
                    _dicTaiSan.Add(cls_TaiSan_DanhMuc.col_TinhChat, "" + cboTinhChat.SelectedValue);
                    _dicTaiSan.Add(cls_TaiSan_DanhMuc.col_SoLuongTonKhoToiThieu, "" + douSLTonTT.Value);

                    _lstUnique.Add(cls_TaiSan_DanhMuc.col_Ma);

                    if (_isAdd)
                    {
                        _dicTaiSan.Add(cls_TaiSan_DanhMuc.col_Ma, _acc.Get_MaMoi());

                        if (_api.Insert(ref _userError, ref _systemError, cls_TaiSan_DanhMuc.tb_TenBang, _dicTaiSan, _lstUnique, _lstUnique))
                        {
                            Load_TaiSan();
                            base.Luu();
                        }
                        else
                        {
                            if (_userError != null)
                            {
                                TA_MessageBox.MessageBox.Show(_userError, TA_MessageBox.MessageIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        _rowIndex = _bsTaiSan.Position;
                        _dicTaiSan.Add(cls_TaiSan_DanhMuc.col_Ma, _rowView[cls_TaiSan_DanhMuc.col_Ma].ToString());
                        _dicWhere.Add(cls_TaiSan_DanhMuc.col_Ma, _rowView[cls_TaiSan_DanhMuc.col_Ma].ToString());
                        if (_api.UpdateAll(ref _userError, ref _systemError, cls_TaiSan_DanhMuc.tb_TenBang, _dicTaiSan, _dicWhere))
                        {
                            Load_TaiSan();
                            _bsTaiSan.Position = _rowIndex;
                            base.Luu();
                        }
                        else
                        {
                            if (_userError != null)
                            {
                                TA_MessageBox.MessageBox.Show(_userError, TA_MessageBox.MessageIcon.Error);
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        #endregion

        #region Import_Excel (Mở form sử dụng chức năng copy dữ liệu từ file excel)
        /// <summary>
        /// Mở form sử dụng chức năng copy dữ liệu từ file excel
        /// </summary>
        /// <author>Nguyễn Văn Long (Long dài)</author>
        /// <Date>18/05/2018</Date>
        protected override void Import_Excel()
        {
            var frm = new frm_ImportTaiSan();
            frm.ShowDialog();
            Load_TaiSan();
            base.Import_Excel();
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
                _bsTaiSan.Filter = "";
            }
            else
            {
                _bsTaiSan.Filter = string.Format("{1} like '%{0}%' OR {2} like'%{0}%'  OR {3} like'%{0}%'"
                    , txtTimKiem.Text, cls_TaiSan_DanhMuc.col_Ma, cls_TaiSan_DanhMuc.col_Ten, cls_TaiSan_DanhMuc.col_KyHieu);
            }
            _count = _bsTaiSan.Count;
            lblTong.Text = _count.ToString();
            base.TimKiem();
        }

        #endregion

        #region Show_ChiTiet (Hiển thị dữ liệu chi tiết dòng đang chọn lên Form)
        /// <summary>
        /// Hiển thị dữ liệu chi tiết dòng đang chọn lên Form
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/04/16</Date>
        protected override void Show_ChiTiet()
        {
            try
            {
                if (_bsTaiSan.Current == null) return;
                _rowView = (DataRowView)_bsTaiSan.Current;
                txtTen.Text = _rowView[cls_TaiSan_DanhMuc.col_Ten] + "";
                txtKyHieu.Text = _rowView[cls_TaiSan_DanhMuc.col_KyHieu]+ "";
                txtMaVachSanXuat.Text = _rowView[cls_TaiSan_DanhMuc.col_Ma]+ "";
                slbLoaiTaiSan.txtMa.Text = _rowView[cls_TaiSan_DanhMuc.col_MaLoai]+ "";
                slbLoaiTaiSan.txtTen.Text = _rowView[cls_TaiSan_DanhMuc.col_TenLoai]+ "";
                //slbDonViQuanLy.txtMa.Text = _rowView[cls_TaiSan_DanhMuc.col_MaKPQuanLy]+ "";
                //slbDonViQuanLy.txtTen.Text = _rowView[cls_TaiSan_DanhMuc.col_TenKPQuanLy]+ "";
                //txtNamSanXuat.Text = _rowView[cls_TaiSan_DanhMuc.col_NamSanXuat]+ "";
                txtNuocSanXuat.Text = _rowView[cls_TaiSan_DanhMuc.col_NuocSanXuat]+ "";
                txtQuyCach.Text = _rowView[cls_TaiSan_DanhMuc.col_QuyCach]+ "";
                txtTaiLieuKyThuat.Text = _rowView[cls_TaiSan_DanhMuc.col_TaiLieu]+ "";

                txtMoTa.Text = ""+_rowView[cls_TaiSan_DanhMuc.col_MoTa];
                txtModel.Text =""+_rowView[cls_TaiSan_DanhMuc.col_MoTa];
                
                try
                {
                    cboTinhChat.SelectedValue =""+ _rowView[cls_TaiSan_DanhMuc.col_TinhChat];
                }
                catch (Exception)
                {
                    cboTinhChat.SelectedIndex = -1;
                }
                try
                {
                    cboDonViTinh.SelectedValue = "" + _rowView[cls_TaiSan_DanhMuc.col_DonViTinh];
                }
                catch (Exception)
                {
                    cboDonViTinh.SelectedIndex = -1;
                }
                try
                {
                    cboKhoMacDinh.SelectedValue = "" + _rowView[cls_TaiSan_DanhMuc.col_KhoMacDinh];
                }
                catch (Exception)
                {

                    cboKhoMacDinh.SelectedIndex = -1;
                }

                intGiaGoc.Value = Helper.ConvertSToDob("" + _rowView[cls_TaiSan_DanhMuc.col_NguyenGia]);
                douSLTonTT.Value = Helper.ConvertSToDob("" + _rowView[cls_TaiSan_DanhMuc.col_SoLuongTonKhoToiThieu]);
                intHanBaoHanh.Value = Helper.ConvertSToIn("" + _rowView[cls_TaiSan_DanhMuc.col_BaoHanh]);
                try
                {
                    chkTamNgung.Value = !(_rowView[cls_TaiSan_DanhMuc.col_TamNgung] +"" == "true");
                }
                catch
                {
                    chkTamNgung.Value = false;
                }
                base.Show_ChiTiet();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        #endregion

        #endregion

        #region Phương thức dùng riêng (Private)

        #region Load_TaiSan (Load dữ liệu danh mục tài sản)
        /// <summary>
        /// Load dữ liệu danh mục tài sản
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/05/21</Date>
        private void Load_TaiSan()
        {
            try
            {
                _bsTaiSan.DataSource = _api.Search(ref _userError, ref _systemError, cls_TaiSan_DanhMuc.tb_TenBang
                    , orderByName1: cls_TaiSan_DanhMuc.col_Ten);
                gvgMain.DataSource = _bsTaiSan;
            }
            catch
            {
                _bsTaiSan.DataSource = null;
            }
            _count = _bsTaiSan.Count;
            lblTong.Text = _count.ToString();
            base.TimKiem();
        }

        #endregion

        #region Load_LoaiTaiSan (Load dữ liệu danh mục loại tài sản)
        /// <summary>
        /// Load dữ liệu danh mục loại tài sản
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/05/21</Date>
        private void Load_LoaiTaiSan()
        {
            try
            {
                Dictionary<string, string> dicE = new Dictionary<string, string>();
                dicE.Add(cls_SYS_DanhMuc.col_Loai, cls_sys_LoaiDanhMuc.loaiTaiSan_MaLoai);

                List<string> lstCot = new List<string>();
                lstCot.Add(cls_SYS_DanhMuc.col_Ma);
                lstCot.Add(cls_SYS_DanhMuc.col_Ten);

                slbLoaiTaiSan.DataSource = _api.Search(ref _userError, ref _systemError, cls_SYS_DanhMuc.tb_TenBang, dicEqual: dicE, lst: lstCot
                    , orderByName1: cls_SYS_DanhMuc.col_Ten);
            }
            catch
            {
                slbLoaiTaiSan.DataSource = null;
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
                cboDonViTinh.DataSource = _api.Search(ref _userError, ref _systemError, cls_DanhMucDonViTinh.tb_TenBang, orderByName1: cls_DanhMucDonViTinh.col_Ten);
                cboDonViTinh.DisplayMember = cls_DanhMucDonViTinh.col_Ten;
                cboDonViTinh.ValueMember = cls_DanhMucDonViTinh.col_ID;
                cboDonViTinh.SelectedIndex = -1;
            }
            catch
            {
                cboDonViTinh.DataSource = null;
            }
        }

        #endregion

        #region Load_TinhChat (Load dữ liệu tính chất tài sản)
        /// <summary>
        /// Load dữ liệu tính chất tài sản
        /// </summary>
        /// <author>TinNT</author>
        /// <Date>2018/12/27</Date>
        private void Load_TinhChat()
        {
            try
            {

                cboTinhChat.DataSource = cls_sys_LoaiDanhMuc.GetTinhChat().ToList();
                cboTinhChat.DisplayMember = "Value";
                cboTinhChat.ValueMember = "Key";
                cboTinhChat.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                cboTinhChat.DataSource = null;
            }
        }

        #endregion

        #region CheckALL (Chọn tất cả các dòng)
        /// <summary>
        /// Chọn tất cả các dòng
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/04/23</Date>
        private void CheckALL()
        {
            try
            {
                foreach (DataRowView view in _bsTaiSan)
                {
                    view["Chon"] = chkAll.Checked;
                }
                _bsTaiSan.EndEdit();
            }
            catch { }
        }

        #endregion


        #endregion

        #endregion

        #region Sự kiện

        #region Control_KeyDown (Sự kiện bấm bàn phím)

        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }

        #endregion

        #region chkAll_CheckedChanged (Sự kiện chọn tất cả)

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckALL();
        }

        #endregion

        #region Sự kiện chọn sửa/xóa

        private void dgvMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvMain.RowCount > 0 && e.RowIndex > -1)
            {
                try
                {
                    if (e.ColumnIndex == 0)
                    {
                        DataRowView view = (DataRowView)_bsTaiSan.Current;
                        try
                        {
                            view["Chon"] = (int.Parse(view["Chon"].ToString()) + 1) % 2;
                            _bsTaiSan.EndEdit();
                        }
                        catch
                        {
                        }
                    }
                    if (e.ColumnIndex == 1)
                    {
                        Sua();
                    }
                    if (e.ColumnIndex == 2)
                    {
                        Xoa();
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
            Show_ChiTiet();
        }

        #endregion

        #region Sự kiện bỏ qua dữ liệu null khi load lên lưới

        #endregion

        #region Sự kiện enter khi chuột đang nằm ở txtGhiChu

        private void txtGhiChu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLuu.Focus();
            }
        }

        #endregion

        #region Sự kiện load form danh mục loại tài sản

        private void lblLoaiTaiSan_Click(object sender, EventArgs e)
        {
            var frm = new frm_DanhMucChung(cls_sys_LoaiDanhMuc.loaiTaiSan_MaLoai, cls_sys_LoaiDanhMuc.loaiTaiSan_TenLoai, "", "", "", "");
            frm.ShowDialog();
            Load_LoaiTaiSan();
        }

        #endregion

        #endregion

    }
}
