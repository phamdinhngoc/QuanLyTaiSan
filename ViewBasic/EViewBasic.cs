using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using E00_Base;
using E00_ControlAdv.Common;
using E00_ControlAdv.Interface.Log;
using E00_ControlAdv.Log;

namespace QuanLyTaiSan.ViewBasic
{
    public partial class EViewBasic : frm_NghiepVu
    {

        protected ILog _log;

        public EViewBasic()
        {
            InitializeComponent();

           
            //var log = new DirectLog(Application.StartupPath, null, new StandardConsole());
            //_log = log;
        }
        protected virtual void Initialize()
        {
            _log = Singleton<DummyLog>.Instance;
            _log = ControlHelper.Log;
        }
        private void InitializeComponent()
        {
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Text = "EViewBasic";
        }

    }
}
