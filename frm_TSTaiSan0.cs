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
    public partial class frm_TSTaiSan0 : frm_Base
    {
        public frm_TSTaiSan0()
        {
            InitializeComponent();

            usc_DanhMucTaiSan1._isClose = false;
        }

        private void frm_TSTaiSan_Load(object sender, EventArgs e)
        {
            usc_DanhMucTaiSan1.LoadData();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (usc_DanhMucTaiSan1._isClose)
            {
                this.Close();
            }
        }
    }
}
