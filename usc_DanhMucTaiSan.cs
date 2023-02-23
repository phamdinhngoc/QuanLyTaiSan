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

namespace QuanLyTaiSan
{

    public partial class usc_DanhMucTaiSan : UserControl
    {
        #region Biến toàn cục

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

        public usc_DanhMucTaiSan()
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
            //Load_LoaiTaiSan();
            //Load_KhoaPhongQuanLy();
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
                //dtDanhMuc = XL_BANG.Doc_cau_truc(cls_TaiSan_DanhMuc.tb_TenBang);
                //dtDanhMuc.Columns.Add(_capNhat);
                //dtDanhMuc.Columns.Add(_dongTrung);

                dtDanhMuc.Columns.Add(cls_TaiSan_DanhMuc.col_Ma);
                dtDanhMuc.Columns.Add(cls_TaiSan_DanhMuc.col_Ten);
                dtDanhMuc.Columns.Add(cls_TaiSan_DanhMuc.col_KyHieu);
                dtDanhMuc.Columns.Add(cls_TaiSan_DanhMuc.col_MaVachSanXuat);
                dtDanhMuc.Columns.Add(cls_TaiSan_DanhMuc.col_MaLoai);
                dtDanhMuc.Columns.Add(cls_TaiSan_DanhMuc.col_TenLoai);
                dtDanhMuc.Columns.Add(cls_TaiSan_DanhMuc.col_MaKPQuanLy);
                dtDanhMuc.Columns.Add(cls_TaiSan_DanhMuc.col_TenKPQuanLy);
                dtDanhMuc.Columns.Add(cls_TaiSan_DanhMuc.col_NuocSanXuat);
                dtDanhMuc.Columns.Add(cls_TaiSan_DanhMuc.col_NamSanXuat);
                dtDanhMuc.Columns.Add(cls_TaiSan_DanhMuc.col_QuyCach);
                dtDanhMuc.Columns.Add(cls_TaiSan_DanhMuc.col_TaiLieu);

                _bsDanhMuc.DataSource = dtDanhMuc;
                dgvMain.DataSource = _bsDanhMuc;

                //foreach (DataRowView view in _bsDanhMuc)
                //{
                //    view[_capNhat] = 0;
                //    view[_dongTrung] = 0;
                //}
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

        #region Load_LoaiTaiSan (Load loại tài sản)
        /// <summary>
        /// Load loại tài sản
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/05/08</Date>
        //private void Load_LoaiTaiSan()
        //{
        //    try
        //    {
        //        System.Data.DataTable dtNhomDanhMuc = new System.Data.DataTable();
        //        Dictionary<string, string> dicE = new Dictionary<string, string>();
        //        List<string> lstCot = new List<string>();
        //        lstCot.Add(cls_SYS_DanhMuc.col_Ma);
        //        lstCot.Add(cls_SYS_DanhMuc.col_Ten);
        //        dicE.Add(cls_SYS_DanhMuc.col_Loai, cls_sys_LoaiDanhMuc.loaiTaiSan_MaLoai);
        //        dtNhomDanhMuc = _api.Search(ref _userError, ref _systemError, cls_SYS_DanhMuc.tb_TenBang, dicEqual: dicE, lst: lstCot
        //                                            , orderByASC1: true, orderByName1: cls_SYS_DanhMuc.col_Ten);
        //        colMaLoai.DataSource = dtNhomDanhMuc;
        //        colMaLoai.DisplayMember = cls_SYS_DanhMuc.col_Ma;
        //        colMaLoai.ValueMember = cls_SYS_DanhMuc.col_Ma;
        //    }
        //    catch
        //    {
        //        colMaLoai.DataSource = null;
        //    }
        //} 

        #endregion

        #region Load_KhoaPhongQuanLy (Load đơn vị quản lý)
        /// <summary>
        /// Load đơn vị quản lý
        /// </summary>
        /// <author>Nguyễn Văn Long (Long Dài)</author>
        /// <Date>2018/06/28</Date>
        //private void Load_KhoaPhongQuanLy()
        //{
        //    try
        //    {
        //        List<string> lstCot = new List<string>();
        //        lstCot.Add(cls_LT_DanhMucBoPhan.col_MA + " AS " + cls_TaiSan_DanhMuc.col_MaKPQuanLy);
        //        lstCot.Add(cls_LT_DanhMucBoPhan.col_TEN + " AS " + cls_TaiSan_DanhMuc.col_TenKPQuanLy);

        //        colMAKPQUANLY.DataSource = _api.Search(ref _userError, ref _systemError, cls_LT_DanhMucBoPhan.tb_TenBang, lst: lstCot);
        //        colMAKPQUANLY.DisplayMember = cls_TaiSan_DanhMuc.col_MaKPQuanLy;
        //        colMAKPQUANLY.ValueMember = cls_TaiSan_DanhMuc.col_MaKPQuanLy;
        //    }
        //    catch 
        //    {
        //        colMAKPQUANLY.DataSource = null;
        //    }
        //}

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
                if (o.GetDataPresent(DataFormats.UnicodeText))
                {
                    string[] pastedRows = Regex.Split(o.GetData(DataFormats.UnicodeText).ToString().TrimEnd("\r\n".ToCharArray()), "\r\n");
                    foreach (string pastedRow in pastedRows)
                    {
                        string[] pastedRowCells = pastedRow.Split(new char[] { '\t' });
                        DataGridViewRow myDataGridViewRow = myDataGridView.Rows[rowIndex];
                        int stt = 0;
                        //if (myDataGridViewRow.Cells["colID"].Value == null)
                        //{
                        //    myDataGridViewRow.Cells["colID"].Value = 0 - _bsDanhMuc.Count;
                        //    myDataGridViewRow.Cells["colDongTrung"].Value = 0;
                        //}

                        if (!int.TryParse(pastedRowCells[0].ToString(), out stt) && columnIndex < 4)
                        {
                            columnIndex = 4;
                        }
                        rowIndex++;
                        //if (rowIndex >= _bsDanhMuc.Count)
                        //{
                        //    _bsDanhMuc.AddNew();
                        //    view = (DataRowView)_bsDanhMuc.Current;

                        //    view[cls_TaiSan_DanhMuc.col_ID] = 0 - _bsDanhMuc.Count;
                        //    view[_dongTrung] = 0;

                        //    _bsDanhMuc.EndEdit();
                        //}
                        for (int i = 0; i < pastedRowCells.Length; i++)
                        {
                            try
                            {
                                myDataGridViewRow.Cells[i + columnIndex].Value = pastedRowCells[i];
                            }
                            catch
                            {
                            }
                        }
                        if (rowIndex == _bsDanhMuc.Count)
                        {
                            view = (DataRowView)_bsDanhMuc.Current;
                            //if (view[cls_TaiSan_DanhMuc.col_ID].ToString() == "")
                            //{
                            //    view[cls_TaiSan_DanhMuc.col_ID] = 0 - _bsDanhMuc.Count;
                            //    view[_dongTrung] = 0;
                            //}
                            if (view[cls_TaiSan_DanhMuc.col_Ma].ToString() == "")
                            {
                                view[cls_TaiSan_DanhMuc.col_Ma] = DateTime.Now.ToString("yyyyddMMHHmmss") + new Random().Next(1000, 9999);
                                //view[_dongTrung] = 0;
                            }
                            _bsDanhMuc.EndEdit();
                            _bsDanhMuc.AddNew();
                        }
                    }
                }

                view = (DataRowView)_bsDanhMuc.Current;
                //view[cls_TaiSan_DanhMuc.col_ID] = 0 - _bsDanhMuc.Count;
                //view[_dongTrung] = 0;
                _bsDanhMuc.EndEdit();
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
        public void Luu()
        {
            Dictionary<string, string> dicData = new Dictionary<string, string>();
            Dictionary<string, string> lstData = new Dictionary<string, string>();
            List<string> lstUnique = new List<string>();
            lstUnique.Add(cls_TaiSan_DanhMuc.col_Ma);
            int soDongTrung = 0;

            int soDongThemThanhCong = 0;
            int soDongKhongHopLe = 0;
            string thongBao = "";

            string sql = "";
            bool dongTrung = false;
            decimal idTaiSan = _api.GetMax_ID(ref _userError, ref _systemError, cls_TaiSan_DanhMuc.tb_TenBang) + 1;
            DataTable dtKiemTra = new DataTable();
            try
            {
                for (int i = 0; i < _bsDanhMuc.Count; i++)
                {
                    DataRowView item = (DataRowView)_bsDanhMuc[i];
                    item[cls_TaiSan_DanhMuc.col_Ma] = item[cls_TaiSan_DanhMuc.col_Ma].ToString() == "" 
                        ? DateTime.Now.ToString("yyyyddMMHHmmss") + new Random().Next(1000, 9999) : item[cls_TaiSan_DanhMuc.col_Ma].ToString();
                    idTaiSan += i;
                    if (item[cls_TaiSan_DanhMuc.col_KyHieu].ToString() != ""
                         && item[cls_TaiSan_DanhMuc.col_Ten].ToString() != ""
                        && item[cls_TaiSan_DanhMuc.col_MaKPQuanLy].ToString() != "")
                    {
                        sql = string.Format(@"Select {0} From {1} where {2} = '{3}' OR {4} = '{5}' OR {6} = '{7}'"
                            , cls_TaiSan_DanhMuc.col_ID, _acc.Get_User() + "." + cls_TaiSan_DanhMuc.tb_TenBang
                            , cls_TaiSan_DanhMuc.col_KyHieu, item[cls_TaiSan_DanhMuc.col_KyHieu].ToString().Trim()
                            , cls_TaiSan_DanhMuc.col_Ma, item[cls_TaiSan_DanhMuc.col_Ma].ToString().Trim()
                             , cls_TaiSan_DanhMuc.col_Ten, item[cls_TaiSan_DanhMuc.col_Ten].ToString().Trim());
                        dtKiemTra = _acc.Get_Data(sql);
                        if (dtKiemTra.Rows.Count > 0)
                        {
                            soDongTrung++;
                            dgvMain.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                            File.WriteAllText(Application.StartupPath + "\\Log.txt", "\n" + _systemError);
                            //File.WriteAllText(Application.StartupPath + "Log.txt", "\n" + _systemError);
                            continue;
                        }
                        sql = string.Format(@"Insert Into {0}({1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16}) 
                                              Values ('{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{31}')"
                                , _acc.Get_User() + "." + cls_TaiSan_DanhMuc.tb_TenBang
                                , cls_TaiSan_DanhMuc.col_ID
                                , cls_TaiSan_DanhMuc.col_Ma
                                , cls_TaiSan_DanhMuc.col_Ten
                                , cls_TaiSan_DanhMuc.col_KyHieu
                                , cls_TaiSan_DanhMuc.col_MaLoai
                                , cls_TaiSan_DanhMuc.col_TenLoai
                                , cls_TaiSan_DanhMuc.col_MaKPQuanLy
                                 , cls_TaiSan_DanhMuc.col_TenKPQuanLy
                                , cls_TaiSan_DanhMuc.col_NuocSanXuat
                                , cls_TaiSan_DanhMuc.col_NamSanXuat //10
                                , cls_TaiSan_DanhMuc.col_MaVachSanXuat
                                , cls_TaiSan_DanhMuc.col_QuyCach
                                , cls_TaiSan_DanhMuc.col_TaiLieu
                                 , cls_TaiSan_DanhMuc.col_TamNgung
                                 , cls_TaiSan_DanhMuc.col_UserID
                                 , cls_TaiSan_DanhMuc.col_UserUD


                                , idTaiSan
                                , item[cls_TaiSan_DanhMuc.col_Ma].ToString() == "" ? DateTime.Now.ToString("yyyyddMMHHmmss") + new Random().Next(1000, 9999) : item[cls_TaiSan_DanhMuc.col_Ma].ToString()
                                 , item[cls_TaiSan_DanhMuc.col_Ten].ToString().Replace("'", "''")
                                  , item[cls_TaiSan_DanhMuc.col_KyHieu].ToString().Trim()
                                  , item[cls_TaiSan_DanhMuc.col_MaLoai].ToString().Trim()
                                  , item[cls_TaiSan_DanhMuc.col_TenLoai].ToString().Trim()
                                  , item[cls_TaiSan_DanhMuc.col_MaKPQuanLy].ToString().Trim()
                                   , item[cls_TaiSan_DanhMuc.col_TenKPQuanLy].ToString().Trim()
                                   , item[cls_TaiSan_DanhMuc.col_NuocSanXuat].ToString().Trim()
                                    , item[cls_TaiSan_DanhMuc.col_NamSanXuat].ToString().Trim()
                                     , item[cls_TaiSan_DanhMuc.col_MaVachSanXuat].ToString().Trim()
                                      , item[cls_TaiSan_DanhMuc.col_QuyCach].ToString().Replace("'", "''")
                                       , item[cls_TaiSan_DanhMuc.col_TaiLieu].ToString().Replace("'", "''")
                                   , "0"
                                   , cls_System.sys_UserID
                                   );
                        if (_acc.Execute_Data(ref _userError, ref _systemError, sql))
                        {
                            soDongThemThanhCong++;
                            //item[cls_TaiSan_DanhMuc.col_ID] = i;
                            // dgvMain.Rows[i].DefaultCellStyle.BackColor = Color.Blue; 

                        }
                        else
                        {
                            dgvMain.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                            File.WriteAllText(Application.StartupPath + "\\Log.txt", "\n" + _systemError);
                        }
                    }

                    else
                    {
                        soDongKhongHopLe++;
                        dgvMain.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                        File.WriteAllText(Application.StartupPath + "\\Log.txt", "\n else: " + _systemError);
                    }
                }
            }
            catch
            {
            }
            if (soDongThemThanhCong > 0)
            {
                thongBao = string.Format("Thêm mới thành công {0} dòng.", soDongThemThanhCong);
            }
            if (soDongTrung > 0)
            {
                thongBao += string.Format("\nCó {0} dòng trùng.", soDongTrung);
            }
            if (soDongKhongHopLe > 0)
            {
                thongBao += string.Format("\nCó {0} dòng dữ liệu không hợp lệ.", soDongKhongHopLe);
            }
            if (thongBao != "")
            {
                MessageBox.Show(thongBao);
            }
            if (soDongThemThanhCong + soDongKhongHopLe + soDongTrung > 0)
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
            colMa.ReadOnly =
            colTen.ReadOnly =
            colKyHieu.ReadOnly =
                //colMaLoai.ReadOnly =
            colTenLoai.ReadOnly =
            colNuocSanXuat.ReadOnly =
            colNamSanXuat.ReadOnly =
            colMaVachSanXuat.ReadOnly =
            colQuyCach.ReadOnly =
            colTaiLieu.ReadOnly = enabled;

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
            //try
            //{
            //if (e.ColumnIndex >= 3 && e.ColumnIndex <= 14)
            //{
            //DataRowView view = (DataRowView)_bsDanhMuc.Current;
            //view[_capNhat] = 1;
            //if (view[cls_TaiSan_DanhMuc.col_ID] == null)
            //{
            //    view[cls_TaiSan_DanhMuc.col_ID] = 0 - _bsDanhMuc.Count;
            //    view[_dongTrung] = 0;
            //}
            //_bsDanhMuc.EndEdit();

            //if (e.RowIndex == _bsDanhMuc.Count - 1 && !_isCopy)
            //{
            //_bsDanhMuc.AddNew();
            //try
            //{
            //    view = (DataRowView)_bsDanhMuc.Current;
            //    view[cls_TaiSan_DanhMuc.col_ID] = 0 - _bsDanhMuc.Count;
            //    view[_dongTrung] = 0;
            //    _bsDanhMuc.EndEdit();
            //}
            //catch
            //{
            //}
            //}
            //}
            //}
            //catch
            //{
            //}
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
                if (e.ColumnIndex == 1)
                {
                    DataRowView view = (DataRowView)_bsDanhMuc.Current;
                    DialogResult rsl = MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa: {0} không?", view[cls_TaiSan_DanhMuc.col_Ten]), "Xác nhận"
                         , MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rsl == DialogResult.Yes)
                    {
                        decimal idDanhMuc = decimal.Parse(view[cls_TaiSan_DanhMuc.col_ID].ToString());
                        if (idDanhMuc < 0)
                        {
                            _bsDanhMuc.Remove(view);
                        }
                        else
                        {
                            Dictionary<string, string> dicWhere = new Dictionary<string, string>();
                            dicWhere.Add(cls_TaiSan_DanhMuc.col_ID, idDanhMuc.ToString());
                            if (_api.Delete(ref _userError, ref _systemError, cls_TaiSan_DanhMuc.tb_TenBang, dicWhere))
                            {
                                _bsDanhMuc.Remove(view);
                            }
                            //dgvMain.Rows[_bsDanhMuc.Position].DefaultCellStyle.BackColor = Color.Yellow;
                            //dgvMain.Rows[_bsDanhMuc.Position].DefaultCellStyle.ForeColor = Color.Red;
                        }
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
