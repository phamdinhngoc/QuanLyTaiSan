using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using E00_Base;
using E00_Model;
using E00_Common;
using E00_Base;


namespace QuanLyTaiSan
{
    public partial class frm_TS_DanhMuc0 : E00_Base.frm_DanhMuc
    {

        #region Biến toàn cục

        private Api_Common _api = new Api_Common();
        public Acc_Oracle _acc = new Acc_Oracle();
        private int _rowIndex = 0;
        private int _rowSelect = 0;
        private string  _lstID = "";
        private string  _lstMa = "";
        private string _lstTen = "";
        private string _lstSTT = "";
        private string _lstMaNhom = "";
        private string _lstTenNhom = "";
        private bool _isAdd = true;
        private bool _isSearch = false;
        private string _userError = string.Empty;
        private string _systemError = string.Empty;
        DataTable _dtMain = new DataTable();

        #endregion

        #region Khởi tạo

        #region Khởi tạo chung

        public frm_TS_DanhMuc()
        {
            _api.KetNoi();
            InitializeComponent();
        } 

        #endregion

        #region Khởi tạo theo đối tượng tương ứng trong DB

        /// <summary>
        /// Khởi tạo theo đối tượng tương ứng trong DB
        /// </summary>
        /// <param name="tenNhom">Text hiển thị tiêu đề Form</param>
        /// <param name="maNhom">Mã nhóm  xác định dữ liệu trong DB</param>
        public frm_TS_DanhMuc(string maNhom,string tenNhom)
        {
            InitializeComponent();
            _api.KetNoi();
            _lstMaNhom = maNhom;
            _lstTenNhom = tenNhom;
            this.Text = tenNhom;
           
        }  

        #endregion

        #endregion

        #region Phương thức

        #region Kế thừa

        protected override void LoadData()
        {
            base.LoadData();
            txtTen.Enabled = false;
            txtGhiChu.Enabled = false;
            itgSTT.Enabled = false;
            Load_DanhMuc();
            txtTen.Focus();
        }

        #region Phương thức thêm
        /// <summary>
        /// Phương thức thêm
        /// </summary>
		protected override void Them()
        {
            base.Them();
            _isAdd = true;
            txtTen.Enabled = true;
            txtGhiChu.Enabled = true;
            itgSTT.Enabled = true;
            chkTamNgung.Enabled = true;
            //itgSTT.Value = 1;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            if (_isAdd) itgSTT.Text = Get_STT(_dtMain);
            txtTen.Clear();
            txtGhiChu.Clear();
            txtTimKiem.Clear();
            txtTen.Focus();
            
        } 
	#endregion

        #region Phương thức xóa
        /// <summary>
        /// Phương thức xóa - 
        /// </summary>
        protected override void Xoa()
        {
            base.Xoa();
            try
            {
              
                //Xóa
                if (TA_MessageBox.MessageBox.Show("Bạn có chắc muốn xóa dữ liệu có tên sau: \n" + _lstTen, "Xác nhận", "Có~Không~0", TA_MessageBox.MessageButton.YesNo, TA_MessageBox.MessageIcon.Question) == DialogResult.No)
                {
                    this.dgvMain.CellValueChanged -= new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMain_CellValueChanged);
                    for (int j = 0; j < dgvMain.RowCount; j++)
                    {
                        dgvMain.Rows[j].DataGridView["colCheck", j].Value = false;
                    }
                    this.dgvMain.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMain_CellValueChanged);
                    return;
                }
              
                        Dictionary<string, string> dicData = new Dictionary<string, string>();
                        dicData.Add(cls_SYS_DanhMuc.col_TamNgung, "1");
                        Dictionary<string, string> dicWhere = new Dictionary<string, string>();
                        dicWhere.Add(cls_SYS_DanhMuc.col_ID, _lstID);
                        _api.Update(ref _userError, ref _systemError, cls_SYS_DanhMuc.tb_TenBang, dicData, new List<string>(), dicWhere);

                        if (_lstID.Length > 0 && _api.DeleteAll(ref _userError, ref _systemError, cls_SYS_DanhMuc.tb_TenBang, _lstID.Trim(',')))
                {
                    TimKiem();

                    if (_rowIndex <= (dgvMain.Rows.Count - 1))
                    {
                        dgvMain.Rows[_rowIndex].Selected = true;
                    }
                    else if (dgvMain.Rows.Count > 0)
                    {
                        dgvMain.Rows[dgvMain.Rows.Count - 1].Selected = true;
                    }
                    _lstID = _lstMa = _lstTen;
                }
                ResetText();
            }
            catch { }
        } 
        #endregion

        #region Phương thức Sửa
        /// <summary>
        /// Phương thức Sửa
        /// </summary>
        protected override void Sua()
        {
            _isAdd = false;
            base.Sua();
            txtTen.Focus();
            txtTen.Enabled = true;
            txtGhiChu.Enabled = true;
            itgSTT.Enabled = true;
            chkTamNgung.Enabled = true;
            Show_ChiTiet();
        } 
        #endregion

        #region Phương thức thoát
        /// <summary>
        /// Phương thức thoát
        /// </summary>
        protected override void Thoat()
        {
            base.Thoat();
        } 
        #endregion

        #region Phương thức bỏ qua
        /// <summary>
        /// Phương thức bỏ qua
        /// </summary>
        protected override void BoQua()
        {
            base.BoQua();
            txtTen.Enabled = false;
            txtGhiChu.Enabled = false;
            itgSTT.Enabled = false;
            chkTamNgung.Enabled = false;
            ResetText();
        } 
        #endregion

        #region Phương thức lưu
        /// <summary>
        /// Phương thức lưu
        /// </summary>
        protected override void Luu()
        {
            base.Luu();

            #region Kiểm tra trước khi lưu
             if (String.IsNullOrEmpty(txtTen.Text))
                {
                    TA_MessageBox.MessageBox.Show("Vui nhập tên", "Lỗi", "Xác nhận", TA_MessageBox.MessageButton.OK, TA_MessageBox.MessageIcon.Error);
                    return;
                }
             
             if (String.IsNullOrEmpty(itgSTT.Text))
             {
                 TA_MessageBox.MessageBox.Show("Vui nhập số thứ tự", "Lỗi", "Xác nhận", TA_MessageBox.MessageButton.OK, TA_MessageBox.MessageIcon.Error);
                 return;
             }
            #endregion

            #region Lưu dữ liệu
            try
            {
                Dictionary<string, string> dicData = new Dictionary<string, string>();
                dicData.Add(cls_SYS_DanhMuc.col_Ten,txtTen.Text);
                dicData.Add(cls_SYS_DanhMuc.col_STT, itgSTT.Text);
                dicData.Add(cls_SYS_DanhMuc.col_GhiChu,txtGhiChu.Text);
                dicData.Add(cls_SYS_DanhMuc.col_TamNgung,chkTamNgung.Checked==true ? "1":"0" );
                dicData.Add(cls_SYS_DanhMuc.col_MaNhom, _lstMaNhom);
                dicData.Add(cls_SYS_DanhMuc.col_TenNhom, _lstTenNhom);
                List<string> lst = new List<string>();
                lst.Add(cls_SYS_DanhMuc.col_Ma);
                lst.Add(cls_SYS_DanhMuc.col_Ten);

                List<string> lst2 = new List<string>();
                lst2.Add(cls_SYS_DanhMuc.col_Ten);
                if (_isAdd)
                {
                    dicData.Add(cls_SYS_DanhMuc.col_Ma, DateTime.Now.ToString("yyyyMMddHHmmss"));
                    dicData.Add(cls_SYS_DanhMuc.col_NgayTao, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    if (_api.Insert(ref _userError, ref _systemError, cls_SYS_DanhMuc.tb_TenBang, dicData, lst, lst2))
                    {
                        base.Luu();
                        btnThem.Focus();
                        Load_DanhMuc();
                        TimKiem();
                        dgvMain.Rows[dgvMain.RowCount - 1].Selected = true;
                        dgvMain.FirstDisplayedScrollingRowIndex = dgvMain.RowCount - 1;
                    }
                    else
                    {
                        TA_MessageBox.MessageBox.Show("Thêm mới thất bại", "Xác nhận");
                    }
                }
                else
                {
                    
                    Dictionary<string, string> lst3 = new Dictionary<string, string>();
                    lst3.Add(cls_SYS_DanhMuc.col_ID, dgvMain["colID",_rowIndex].Value.ToString());
                    if (_api.Update(ref _userError
                                    , ref _systemError
                                    , cls_SYS_DanhMuc.tb_TenBang
                                    , dicData, new List<string>()
                                    , lst3))
                    {
                        //Clear_Control();
                        _isAdd = true;
                        base.Luu();
                        btnThem.Focus();
                        Load_DanhMuc();
                        TimKiem();
                        dgvMain.Rows[_rowSelect].Selected = true;
                        dgvMain.FirstDisplayedScrollingRowIndex = _rowSelect;
                    }
                    else
                    {
                        TA_MessageBox.MessageBox.Show("Sửa thất bại", "Xác nhận");
                    }
                }

              
            }
            catch (Exception)
            {
            }

            txtTen.Enabled = false;
            txtGhiChu.Enabled = false;
            itgSTT.Enabled = false;
            chkTamNgung.Enabled = false;
        } 
        #endregion


        #endregion
        
        #region Phương thức tìm kiếm
        /// <summary>
        /// Phương thức tìm kiếm
        /// </summary>
        protected override void TimKiem()
        {
            try
            {
                List<string> lst = new List<string>();

                lst.Add(cls_SYS_DanhMuc.col_ID);
                lst.Add(cls_SYS_DanhMuc.col_Ma);
                lst.Add(cls_SYS_DanhMuc.col_Ten);
                lst.Add(cls_SYS_DanhMuc.col_STT);
                lst.Add(cls_SYS_DanhMuc.col_GhiChu);
                lst.Add(cls_SYS_DanhMuc.col_TamNgung);
                Dictionary<string, string> lst3 = new Dictionary<string, string>();
                lst3.Add(cls_SYS_DanhMuc.col_Ma, _chuoiTimKiem);
                lst3.Add(cls_SYS_DanhMuc.col_Ten, _chuoiTimKiem);

                Dictionary<string, string> lst2 = new Dictionary<string, string>();
                lst2.Add(cls_SYS_DanhMuc.col_MaNhom, _lstMaNhom);
                string sOrderBy = cls_SYS_DanhMuc.col_STT + "," + cls_SYS_DanhMuc.col_Ten;
                _dtMain = _api.Search(ref _userError, ref _systemError, cls_SYS_DanhMuc.tb_TenBang, count: -1, lst: lst, andEqual: true, dicEqual: lst2, andLike: false, dicLike: lst3
                        , orderByASC1: true, orderByName1: sOrderBy);
                    dgvMain.DataSource = _dtMain;
                _count = dgvMain.RowCount;
                if (_isAdd) itgSTT.Text = Get_STT(_dtMain);
            }
            catch (Exception ex)
            {
            }
            base.TimKiem();
        } 
        #endregion

        #region Hiển thị dữ liệu đang chọn
        /// <summary>
        /// Hiển thị dữ liệu đang chọn
        /// </summary>
        protected override void Show_ChiTiet()
        {
            base.Show_ChiTiet();
            if (dgvMain.DataSource == null) return;

            _lstID = dgvMain.SelectedRows[0].Cells["colID"].Value == null ? "" : dgvMain.SelectedRows[0].Cells["colID"].Value.ToString();
            _lstMa = dgvMain.SelectedRows[0].Cells["colMa"].Value == null ? "" : dgvMain.SelectedRows[0].Cells["colMa"].Value.ToString();
            _lstTen = dgvMain.SelectedRows[0].Cells["colTen"].Value == null ? "" : dgvMain.SelectedRows[0].Cells["colTen"].Value.ToString();
            _lstSTT = dgvMain.SelectedRows[0].Cells["ColSTT"].Value == null ? "" : dgvMain.SelectedRows[0].Cells["ColSTT"].Value.ToString();
            txtTen.Text = dgvMain.SelectedRows[0].Cells["colTen"].Value == null ? "" : dgvMain.SelectedRows[0].Cells["colTen"].Value.ToString();
            itgSTT.Text = dgvMain.SelectedRows[0].Cells["colSTT"].Value == null ? "" : dgvMain.SelectedRows[0].Cells["colSTT"].Value.ToString();
            txtGhiChu.Text = dgvMain.SelectedRows[0].Cells["colGhiChu"].Value == null ? "" : dgvMain.SelectedRows[0].Cells["colGhiChu"].Value.ToString();
            chkTamNgung.Checked = dgvMain.SelectedRows[0].Cells["colTamNgung"].Value.ToString() == "1" ? true : false;
        }

        #endregion

        #region 
        public override void ResetText()
        {
            base.ResetText();
           
        } 
        #endregion

        #endregion

        #region Phương thức private

        #region Load dữ liệu theo đối tượng tương ứng trong DB
        private void Load_DanhMuc()
        {
            List<string> lst = new List<string>();
            lst.Add(cls_SYS_DanhMuc.col_ID);
            lst.Add(cls_SYS_DanhMuc.col_Ma);
            lst.Add(cls_SYS_DanhMuc.col_Ten);
            lst.Add(cls_SYS_DanhMuc.col_STT);
            lst.Add(cls_SYS_DanhMuc.col_TamNgung);
            lst.Add(cls_SYS_DanhMuc.col_GhiChu);
            Dictionary<string, string> lstWhere = new Dictionary<string, string>();
            lstWhere.Add(cls_SYS_DanhMuc.col_MaNhom, _lstMaNhom);
            _dtMain = _api.Search(ref _userError, ref _systemError, cls_SYS_DanhMuc.tb_TenBang, _acc.Get_User(), -1, lst, true, lstWhere, orderByASC1: true);
            dgvMain.DataSource = _dtMain;
        }
        #region Lấy số thứ tự theo nhóm
        //Lấy số thứ tự theo nhóm
        private string Get_STT(DataTable dataTable)
        {
            try
            {
                //int kq = 1;
                List<int> lst = new List<int>();
                if (dataTable.Rows.Count == 0) return "1";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    lst.Add(Convert.ToInt32(dataTable.Rows[i].ItemArray[3].ToString()));
                }
                return (lst.Max() + 1).ToString();
            }
            catch { return null; }
        }
        #endregion
        #endregion

      
        #endregion

        #endregion

        #region Sự kiện

        #region Chọn/bỏ chọn tất cả các dòng của lưới
        /// <summary>
        /// Chọn/bỏ chọn tất cả các dòng của lưới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAll_Click(object sender, EventArgs e)
        {
            try
            {
                this.dgvMain.CellValueChanged -= new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMain_CellValueChanged);
                for (int i = 0; i < dgvMain.RowCount; i++)
                {
                    dgvMain.Rows[i].DataGridView["colCheck", i].Value = !chkAll.Checked;
                }

                //them du lieu vao list xóa
                _lstID = _lstTen = _lstMa = "";
                if (!chkAll.Checked)
                {
                    foreach (DataGridViewRow row in dgvMain.Rows)
                    {
                        _lstID += string.Format("{0},", row.Cells["colID"].Value.ToString());
                        _lstMa += string.Format("{0},", row.Cells["colMa"].Value.ToString());
                        _lstTen += string.Format("{0}\n", row.Cells["colTen"].Value.ToString());
                    }
                }
                else
                {
                    _lstID = _lstTen  = _lstMa = "";
                }

            }
            catch { }
        }
        
        private void dgvMain_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dgvMain.Columns["colCheck"].Index)
                {
                    if (dgvMain.SelectedRows[0].Cells["colCheck"].Value.ToString() == "False")
                    {
                        if (_lstID.Contains(dgvMain.SelectedRows[0].Cells["colID"].Value.ToString()))
                        {
                            _lstID = _lstID.Replace(string.Format("{0},", dgvMain.SelectedRows[0].Cells["colID"].Value.ToString()), "");
                            _lstMa = _lstMa.Replace(string.Format("{0},", dgvMain.SelectedRows[0].Cells["colMa"].Value.ToString()), "");
                            _lstTen = _lstTen.Replace(string.Format("{0}\n", dgvMain.SelectedRows[0].Cells["colTen"].Value.ToString()), "");
                        }
                    }
                    else
                    {
                        if (!_lstID.Contains(dgvMain.SelectedRows[0].Cells["colID"].Value.ToString()))
                        {
                            _lstID += string.Format("{0},", dgvMain.SelectedRows[0].Cells["colID"].Value.ToString());
                            _lstMa += string.Format("{0},", dgvMain.SelectedRows[0].Cells["colMa"].Value.ToString());
                            _lstTen += string.Format("{0}\n", dgvMain.SelectedRows[0].Cells["colTen"].Value.ToString());
                        }

                    }
                }
            }
            catch
            {

            }
        }

        #endregion

        #region Hiển thị dữ liệu đang chọn
        private void dgvMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _rowSelect = e.RowIndex;
            if (dgvMain.RowCount > 0 && e.RowIndex > -1)
            {
                try
                {
                    //Sửa
                    if (e.ColumnIndex == 1)
                    {
                        Sua();
                    }
                    //xóa
                    if (e.ColumnIndex == 2)
                    {
                        if (!_lstID.Contains(dgvMain.SelectedRows[0].Cells["colID"].Value.ToString()))
                        {
                            _lstID += "," + dgvMain.SelectedRows[0].Cells["colID"].Value.ToString();
                        }
                        Xoa();
                    }

                    _lstMa = dgvMain.SelectedRows[0].Cells["colMa"].Value.ToString();
                    _lstSTT = dgvMain.SelectedRows[0].Cells["colSTT"].Value.ToString();
                    // bỏ checkall
                    if (dgvMain.Rows[e.RowIndex].Cells["colCheck"].Value != null)
                    {
                        if (!(dgvMain.Rows[e.RowIndex].Cells["colCheck"].Value.ToString() == "False"))
                        {
                            chkAll.Checked = false;
                        }
                    }
                }
                catch
                {

                }
            }
        }
        private void dgvMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Show_ChiTiet();
            _isAdd = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        #endregion

        #endregion

    }
}
