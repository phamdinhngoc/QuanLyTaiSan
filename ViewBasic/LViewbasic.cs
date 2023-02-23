using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using E00_Base;
using E00_ControlAdv.Common;
using E00_ControlAdv.Interface.Log;
using E00_ControlAdv.Log;

namespace QuanLyTaiSan.ViewBasic
{
    public partial class LViewbasic : frm_Base
    {
        protected ILog _log;
        protected SynchronizationContext _syncContext;
        public LViewbasic():base()
        {
            InitializeComponent();
           
        }

        protected virtual void Initialize()
        {
            _syncContext = SynchronizationContext.Current;
            _log = Singleton<DummyLog>.Instance;
            _log = ControlHelper.Log;
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // LViewbasic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.DoubleBuffered = true;
            this.Name = "LViewbasic";
            this.Text = "Viewbasic";
            this.ResumeLayout(false);

        }
    }
}
