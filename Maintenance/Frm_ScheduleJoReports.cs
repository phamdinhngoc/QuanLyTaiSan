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
using E00_API.Contract.Maintenance;
using QuanLyTaiSan.ViewBasic;

namespace QuanLyTaiSan.Maintenance
{
    public partial class Frm_ScheduleJoReports : ListViewBasic<LichBaoTriLLInfo>
    {
        public Frm_ScheduleJoReports():base()
        {
            InitializeComponent();
            dgvGrid.AutoGenerateColumns = false;
            Initialize();
            
        }

        protected override void Synchronized()
        {
            try
            {
            gvgContainer.StartWaiting("");
            pnlTitle.Enabled = false;
            _defaultLazyData = new Lazy<DataTable>(() => _defaultDataService.Get_Data());
            var _theardRun = new Thread(new ThreadStart(() =>
            {
                var data = _defaultLazyData.Value;
                _syncContext.Send(state =>
                {
                    gvgContainer.DataSource = data;
                    pnlTitle.Enabled = true;
                }, null);
            }));
            _theardRun.IsBackground = true;
            _theardRun.Start();
            }
            catch (Exception ex)
            {
                _log.Error("Frm_ScheduleJoReports => Synchronized:" + ex.Message);
            }
        }
        protected override void OpenEditForm()
        {
            try
            {

            Frm_ScheduleJob formView = new Frm_ScheduleJob();
            formView.Info = _info;
            formView.ShowDialog();
            Synchronized();
            }
            catch (Exception ex)
            {
                _log.Error("Frm_ScheduleJoReports => OpenEditForm:" + ex.Message);
            }
        }

    }
}
