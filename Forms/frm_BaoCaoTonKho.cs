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

namespace QuanLyTaiSan
{
    public partial class frm_BaoCaoTonKho : frm_Base
    {
        #region Biến toàn cục
        private api_TaiSan _apiTaiSan = new api_TaiSan();
        #endregion

        #region Khởi tạo
        public frm_BaoCaoTonKho()
        {
            InitializeComponent();
        } 
        #endregion

        #region Sự kiền

        private void btnXem_Click(object sender, EventArgs e)
        {
            if (dtpTuNgay.Text == "" || dtpDenNgay.Text == "")
            {
                MessageBox.Show("Vui lòng chọn ngày.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                dgvMain.DataSource = _apiTaiSan.Get_BaoCaoNhapXuatTon(DateTime.Parse(dtpTuNgay.Text).ToString("dd/MM/YYYY"), DateTime.Parse(dtpDenNgay.Text).ToString("dd/MM/YYYY"));
            }
        } 
        #endregion

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
                if (txtFilter.Text != "") (dgvMain.DataSource as DataTable).DefaultView.RowFilter = string.Format("{1} LIKE '{0}%'", txtFilter.Text,cls_TaiSan_SuDungLL.col_TenTaiSan);
                else (dgvMain.DataSource as DataTable).DefaultView.RowFilter = "";
            
        }
    }
}
