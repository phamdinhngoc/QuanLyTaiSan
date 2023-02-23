using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using E00_Base;

namespace QuanLyTaiSan
{
    public partial class frm_ImportNhapTaiSan : frm_Base
    {
        #region Khởi tạo

        public frm_ImportNhapTaiSan()
        {
            InitializeComponent();
        } 

        #endregion

        #region Sự kiện

        #region Sự kiện load form

        private void frm_ImportTaiSan_Load(object sender, EventArgs e)
        {
            usc_NhapTaiSan1.LoadData();
        } 

        #endregion

        #region Sự kiện đóng form

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (usc_NhapTaiSan1._isClose)
            {
                this.Close();
            }
        }  

        #endregion

        #endregion
    }
}
