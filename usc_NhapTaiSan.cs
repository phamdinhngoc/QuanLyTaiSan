using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
//using Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;
using System.IO;
using DevComponents.DotNetBar.Controls;
using System.Diagnostics;
using E00_Common;
using E00_Control;
using E00_Model;
using E00_System;
using E00_Base;
using System.Threading;

namespace QuanLyTaiSan
{

    public partial class usc_NhapTaiSan : UserControl
    {
        #region Biến toàn cục
        private E00_API.api_TaiSan _apiTaiSan = new E00_API.api_TaiSan();
        private BindingSource _bsDanhMuc = new BindingSource();
        private BindingSource _bsTC = new BindingSource();
        private BindingSource _bsTB = new BindingSource();
        List<string> _lst = new List<string>();
        List<string> _lst2 = new List<string>();
        string _columnName = "";
        public static string _lstColumn = "";
        public static string _lstValue = "";
        public static string _queryInsert = "";
        public static string _ngonNgu = "";
        public bool _isClose = false;
        private SortedList<string, string> _lstType = new SortedList<string, string>();
        private Api_Common _api = new Api_Common();
        private Acc_Oracle _acc = new Acc_Oracle();
        private Dictionary<string, string> _lstCotMap = new Dictionary<string, string>();
        private string _tableName = string.Empty;
        private List<string> _lstBatBuoc = new List<string>();
        private List<string> _lstDuyNhat = new List<string>();
        private List<string> _lstRowError = new List<string>();
        private Dictionary<string, string> _lstCotTheoMau = new Dictionary<string, string>();
        private SortedList<string, string> _lstDongTrung = new SortedList<string, string>();
        private int _count = 0;
        private string _userError = "";
        private string _systemError = "";
        private bool _isCopy = false;

        private string _capNhat = "CapNhat";
        private string _dongTrung = "DongTrung";

        #endregion

        #region Khởi tạo

        public usc_NhapTaiSan()
        {
            InitializeComponent();
            _api.KetNoi();
            dgvMain.AutoGenerateColumns = false;
        }

        #endregion

        #region Phương thức

        #region LoadData (Load dữ liệu khi mở form)
        /// <summary>
        /// Load dữ liệu khi mở form
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/05/16</Date>
        public void LoadData()
        {
            Load_DanhMuc();
        }

        #endregion

        #region Load_DanhMuc (Load cấu trúc bảng danh mục tài sản)
        /// <summary>
        /// Load cấu trúc bảng danh mục tài sản
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/05/22</Date>
        private void Load_DanhMuc()
        {
            try
            {
                System.Data.DataTable dtDanhMuc = new System.Data.DataTable();
                dtDanhMuc = _apiTaiSan.Get_CauTrucImportNhapTaISan();
                _bsDanhMuc.DataSource = dtDanhMuc;
                dgvMain.DataSource = _bsDanhMuc;
            }
            catch
            {
                _bsDanhMuc.DataSource = null;
            }
            _count = _bsDanhMuc.Count;
            lblKetQuaNhap.Text = _count.ToString();
            Set_Enabled_Control(true);
        }

        #endregion

        #region PasteClipboard (Paste dữ liệu copy từ Excel)
        /// <summary>
        /// Paste dữ liệu copy từ Excel
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/05/22</Date>
        /// <param name="myDataGridView">Lưới hiển thị dữ liệu trên form</param>
        /// <param name="columnIndex">Vị trí bắt đầu cột hiển thị</param>
        /// <param name="rowIndex">Vị trí bắt đầu dòng hiển thị</param>
        private void PasteClipboard(DataGridView myDataGridView, int columnIndex, int rowIndex)
        {
            try
            {
                _isCopy = true;
                if (dgvMain.Columns["colTen"].ReadOnly)
                {
                    MessageBox.Show("Bạn vui lòng bấm 'Thêm' để nhập thêm dữ liệu hoặc 'Sửa' để sửa lại dữ liệu");
                    return;
                }
                if (columnIndex < 3)
                {
                    columnIndex = 3;
                }
                DataObject o = (DataObject)Clipboard.GetDataObject();
                DataRowView view;
                DataGridViewRow myDataGridViewRow;
                if (o.GetDataPresent(DataFormats.UnicodeText))
                {
                    string[] pastedRows = Regex.Split(o.GetData(DataFormats.UnicodeText).ToString().TrimEnd("\r\n".ToCharArray()), "\r\n");
                    foreach (string pastedRow in pastedRows)
                    {
                        string[] pastedRowCells = pastedRow.Split(new char[] { '\t' });
                        myDataGridViewRow = myDataGridView.Rows[rowIndex];
                        int stt = 0;
                        rowIndex++;
                        for (int i = 0; i < pastedRowCells.Length - 2; i++)
                        {
                            try
                            {
                                myDataGridViewRow.Cells[i + columnIndex].Value = pastedRowCells[i];
                            }
                            catch
                            {
                            }
                        }
                        myDataGridViewRow.Cells[pastedRowCells.Length - 2 + columnIndex].Value = (decimal.Parse(pastedRowCells[pastedRowCells.Length - 2].ToString())).ToString("N0");
                        myDataGridViewRow.Cells[pastedRowCells.Length - 1 + columnIndex].Value = (decimal.Parse(pastedRowCells[pastedRowCells.Length - 1].ToString())).ToString("N0");
                        myDataGridViewRow.Cells[pastedRowCells.Length + columnIndex].Value = (decimal.Parse(pastedRowCells[pastedRowCells.Length - 1].ToString())
                                                                                            * decimal.Parse(pastedRowCells[pastedRowCells.Length - 2].ToString())).ToString("N0");
                        _bsDanhMuc.AddNew();

                        if (rowIndex == _bsDanhMuc.Count)
                        {

                        }
                    }
                }

                _isCopy = false;
            }
            catch //(Exception ex)
            {
                // MessageBox.Show(ex.ToString());
            }
            lblKetQuaNhap.Text = _bsDanhMuc.Count.ToString();
        }

        #endregion

        #region Luu (Lưu thông tin trên lưới vào DB danh mục tài sản)
        /// <summary>
        /// Lưu thông tin trên lưới vào DB danh mục tài sản
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/05/24</Date>
        /// 
        public void Luu()
        {
            Dictionary<string, string> dicData = new Dictionary<string, string>();
            Dictionary<string, string> lstData = new Dictionary<string, string>();
            List<string> lstUnique = new List<string>();
            lstUnique.Add(cls_TaiSan_NhapLL.col_Ma);
            int soDongTrung = 0;

            int soDongThemThanhCong = 0;
            int soDongSuaThanhCong = 0;
            string thongBao = "";

            SortedList<string, string> lst = new SortedList<string, string>();
            Dictionary<string, string> dicSuDungCT = new Dictionary<string, string>();
            DataTable dtNhapCT = new DataTable();
            string key = "";
            string value = "";

            foreach (DataRowView view in _bsDanhMuc)
            {
                if (!lst.Keys.Contains(view[cls_TaiSan_NhapLL.col_MaNhaCungCap].ToString().Trim()) && view[cls_TaiSan_NhapLL.col_MaNhaCungCap].ToString().Trim() != "")
                {
                    lst.Add(view[cls_TaiSan_NhapLL.col_MaNhaCungCap].ToString().Trim(), "PN" + DateTime.Now.ToString("yyMMddHHmmss") + lst.Values.Count.ToString().PadLeft(3, '0'));
                }
            }
            for (int i = 0; i < lst.Values.Count; i++)
            {
                key = lst.Keys[i];
                value = lst.Values[i];
                if (_apiTaiSan.Insert_NhapLL(value, _acc.Get_MaMoi(), _acc.Get_YYYYMMddHHmmss(), "", "", "", ""
                                    , key, cls_System.sys_UserID, ""))
                {
                    dtNhapCT = ((DataTable)_bsDanhMuc.DataSource).Select(string.Format("{0} = '{1}'", cls_TaiSan_NhapLL.col_MaNhaCungCap, key)).CopyToDataTable();
                    foreach (DataRow view in dtNhapCT.Rows)
                    {
                        string maNhapCT = _acc.Get_MaMoi();
                        if (_apiTaiSan.Insert_NhapCT(maNhapCT, value, view[cls_TaiSan_NhapCT.col_MaTaiSan].ToString().Trim()
                                                        , decimal.Parse(view[cls_TaiSan_NhapCT.col_SoLuong].ToString())
                                                        , decimal.Parse(view[cls_TaiSan_NhapCT.col_DonGia].ToString()), ""))
                        {
                            soDongThemThanhCong++;
                        }
                        else
                        {
                        }
                    }
                }
            }
            if (soDongThemThanhCong > 0)
            {
                thongBao = string.Format("Thêm mới thành công {0} dòng.", soDongThemThanhCong);
            }
            if (soDongTrung > 0)
            {
                thongBao += string.Format("Có {0} dòng trùng.", soDongTrung);
            }
            if (soDongSuaThanhCong > 0)
            {
                thongBao += string.Format("Sửa thành công {0} dòng.", soDongSuaThanhCong);
            }
            if (thongBao != "")
            {
                MessageBox.Show(thongBao);
            }
            if (soDongThemThanhCong + soDongSuaThanhCong + soDongTrung > 0)
            {
                _count = _bsDanhMuc.Count;
                Set_Enabled_Control(true);
            }
        }

        #endregion

        #region Set_Enabled_Control (Ẩn/Hiện control)
        /// <summary>
        /// Ẩn/Hiện control
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/05/16</Date>
        /// <param name="enabled">Ẩn/Hiện control</param>
        private void Set_Enabled_Control(bool enabled)
        {
            btnThem.Enabled =
                btnSua.Enabled =
                btnXoa.Enabled = enabled;

            if (_count > 0)
            {
                btnSua.Enabled =
                    btnXoa.Enabled = enabled;
            }
            else
            {
                btnSua.Enabled =
                    btnXoa.Enabled = !enabled;
            }

            btnLuu.Enabled =
                btnBoQua.Enabled = !enabled;
            colMaTaiSan.ReadOnly =
            colTen.ReadOnly =
            colKyHieu.ReadOnly =
            enabled;

        }

        #endregion

        #endregion

        #region Sự kiện

        #region Sự kiện load form

        private void usc_DanhMucChung_Load(object sender, EventArgs e)
        {
            Set_Enabled_Control(true);
            System.Data.DataTable data = new System.Data.DataTable();
            _bsDanhMuc.DataSource = data;
            dgvMain.DataSource = null;
            dgvMain.DataSource = _bsDanhMuc;
        }

        #endregion

        #region Sự kiện paste dữ liệu copy từ Excel

        private void dgvMain_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.V))
            {
                PasteClipboard(dgvMain, dgvMain.SelectedCells[0].ColumnIndex, dgvMain.SelectedCells[0].RowIndex);
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

            chkAll.Location = new System.Drawing.Point(dg.RowHeadersWidth + 6, chkAll.Location.Y);
        }

        #endregion

        #region Sự kiện bỏ qua lỗi dữ liệu khi load lên lưới

        private void dgvMain_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        #endregion

        #region Sự kiện thay đổi giá trị trên lưới

        private void dgvMain_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 8 || e.ColumnIndex == 9)
                {
                    _bsDanhMuc.EndEdit();
                    DataRowView view = (DataRowView)_bsDanhMuc.Current;
                    view[cls_TaiSan_NhapCT.col_ThanhTien] = decimal.Parse(view[cls_TaiSan_NhapCT.col_DonGia].ToString()) * decimal.Parse(view[cls_TaiSan_NhapCT.col_SoLuong].ToString());
                    _bsDanhMuc.EndEdit();
                }
            }
            catch
            {
            }
        }

        #endregion

        #region Sự kiện chọn tất cả

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dgvMain.RowCount - 1; i++)
                {
                    dgvMain.Rows[i].Cells["colChk"].Value = chkAll.Checked;
                }
            }
            catch
            {
            }
        }

        #endregion

        #region Sự kiện thêm dòng mới

        private void btnThem_Click(object sender, EventArgs e)
        {
            Set_Enabled_Control(false);
            _bsDanhMuc.AddNew();
            try
            {
                DataRowView view = (DataRowView)_bsDanhMuc.Current;
                view[cls_TaiSan_DanhMuc.col_ID] = 0 - _bsDanhMuc.Count;
                view[_dongTrung] = 0;
                _bsDanhMuc.EndEdit();
            }
            catch
            {
            }
        }

        #endregion

        #region Sự kiện sử dòng

        private void btnSua_Click(object sender, EventArgs e)
        {
            Set_Enabled_Control(false);
        }

        #endregion

        #region Sự kiện xóa nhiều dòng đang chọn trên lưới

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult rsl = MessageBox.Show("Bạn có chắc chắn muốn xóa các dòng đang chọn không?", "Xác nhận"
                        , MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rsl == DialogResult.Yes)
            {
                DataTable dtXoa = ((DataTable)_bsDanhMuc.DataSource).Select("Chon = true").CopyToDataTable();
                decimal idDanhMuc = -1;

                foreach (DataRow row in dtXoa.Rows)
                {
                    idDanhMuc = decimal.Parse(row[cls_TaiSan_DanhMuc.col_ID].ToString());
                    if (idDanhMuc < 0)
                    {
                        _bsDanhMuc.Remove(row);
                    }
                    else
                    {
                        Dictionary<string, string> dicWhere = new Dictionary<string, string>();
                        dicWhere.Add(cls_TaiSan_DanhMuc.col_ID, idDanhMuc.ToString());
                        if (_api.Delete(ref _userError, ref _systemError, cls_TaiSan_DanhMuc.tb_TenBang, dicWhere))
                        {
                            _bsDanhMuc.Remove(row);
                        }
                    }
                }
            }
            if (_bsDanhMuc.Count == 0)
            {
                Set_Enabled_Control(true);
                btnSua.Enabled =
                    btnXoa.Enabled = false;
            }
        }

        #endregion

        #region Sự kiện bỏ qua

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            Set_Enabled_Control(true);
            Load_DanhMuc();
        }

        #endregion

        #region Sự kiện lưu

        private void btnLuu_Click(object sender, EventArgs e)
        {
            Luu();
        }

        #endregion

        #region Sự kiện thoát

        private void btnThoat_Click(object sender, EventArgs e)
        {
            _isClose = true;
        }

        #endregion

        #region Sự kiện tìm kiếm

        private void txtTimKiemNhap_TextChanged(object sender, EventArgs e)
        {
            try
            {
                _bsDanhMuc.Filter = string.Format("{1} like '%{0}%' OR {2} like'%{0}%'  OR {3} like'%{0}%'"
                    , txtTimKiemNhap.Text, cls_TaiSan_DanhMuc.col_Ma, cls_TaiSan_DanhMuc.col_Ten, cls_TaiSan_DanhMuc.col_KyHieu);

                lblKetQuaNhap.Text = dgvMain.RowCount.ToString();
            }
            catch
            {
                dgvMain.DataSource = null;
                lblKetQuaNhap.Text = "0";
            }
        }

        #endregion

        #region Sự kiện xóa dòng trên lưới

        private void dgvMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1 && e.RowIndex < _bsDanhMuc.Count)
                {
                    DataRowView view = (DataRowView)_bsDanhMuc.Current;
                    DialogResult rsl = MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa: {0} không?", view[cls_TaiSan_NhapCT.col_TenTaiSan]), "Xác nhận"
                         , MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rsl == DialogResult.Yes)
                    {
                        _bsDanhMuc.Remove(view);
                    }
                }
            }
            catch
            {
            }
        }

        #endregion

        #endregion

    }
}
