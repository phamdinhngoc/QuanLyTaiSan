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
using E00_API.Maintenance;
using E00_Base;
using E00_Helpers.Helpers;
using E00_Model;
using E00_SafeCacheDataService.Base;
using E00_SafeCacheDataService.Interface;
using QuanLyTaiSan.ViewBasic;

namespace QuanLyTaiSan.Maintenance
{
    public partial class Frm_ScheduleListReport : ReportViewBasic<LichBaoTriLLInfo>
    {

        //------------------------------------

        #region Member
        api_LichBaoTriLL _defaultService;
        public string ItemID { get; set; }
        private api_TaiSan_ChiTietLinhKien _chiTietLinhKienSevice;
        private IAPI<BTBaoTriLLInfo> _maintenanceService;
        private DataRow _rowCurrent;
        private const string DanhMuc = @"DM";
        private LichBaoTriLLInfo _scheduleInfo;
        #endregion

        //------------------------------------

        public Frm_ScheduleListReport():base()
        {
            InitializeComponent();
            expLeft.Expanded = true;
            expLeft.Width = 650;

            _syncContext = SynchronizationContext.Current;
            datDate.Value = DateTime.Now;
            dgvData.AutoGenerateColumns = false;
            btnNext.Click += (send, e) => { datDate.Value = datDate.Value.AddDays(1); };
            btnPrevious.Click += (send, e) => { datDate.Value = datDate.Value.AddDays(-1);};
            btnTodate.Click += (send, e) => { datDate.Value = DateTime.Now;};
            datDate.ValueChanged += (send, e) => Synchronized();
            btnAddSchedule.Click += (send, e) => OpenScheduleReport();
            btnMaintenance.Click += (send, e) => OpenMaintenanceReport();
            colInfo.Click += (send, e) => OnpenInfo();
            Initialize();
        }

        //------------------------------------

        #region Public/Protected Method
        protected override void DataLoading()
        {
            try
            {
            gvgContainer.Initialize("");
            _defaultService = new api_LichBaoTriLL();
            _chiTietLinhKienSevice = new api_TaiSan_ChiTietLinhKien();
            _maintenanceService = new API_Common<BTBaoTriLLInfo>();
                Synchronized();
            }
            catch (Exception ex)
            {
                _log.Error("Frm_ScheduleListReport => DataLoading:" + ex.Message);
            }
        }
        protected override void Synchronized()
        {
            try
            {
            gvgContainer.StartWaiting("");
            pnlTitle.Enabled = false;
            DateTime date = datDate.Value;
            if (date == DateTime.MinValue) return;
            _defaultLazyData = new Lazy<DataTable>(() => _defaultService.GetScheduleList(date));
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
                _log.Error("Frm_ScheduleListReport => Synchronized:" + ex.Message);
            }
        }
        protected override void Initialize()
        {
            base.Initialize();
            this.Load += (send, e) => DataLoading();
        }

        #endregion

        //------------------------------------

        #region Private Method
        private void OpenBOM()
        {
            if (!string.IsNullOrEmpty(ItemID))
            {
                Frm_BillOfMaterial frm_BillOfMaterial = new Frm_BillOfMaterial();
                frm_BillOfMaterial.ShowDialog();
            }
        }

        private void OnpenInfo()
        {
            try
            {
                if (dgvData.CurrentRow == null) return;
                string rowSelect = GetSelectItemCurrent();
                if (!string.IsNullOrEmpty(rowSelect))
                {
                    Frm_PossessionsInfomation frmInfo = new Frm_PossessionsInfomation();
                    frmInfo.ItemID = rowSelect;
                    frmInfo.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                _log.Error("Frm_ScheduleListReport => OnpenInfo:" + ex.Message);
            }

        }
   
        private string GetSelectItemCurrent()
        {
            try
            {
                if (dgvData.CurrentRow == null) return "";
                var rowSelect = dgvData.CurrentRow.DataBoundItem as DataRowView;
                if (rowSelect != null)
                {
                    return "" + rowSelect.Row[cls_TaiSan_SuDungLL.col_MaVach];
                }
                return "";
            }
            catch (Exception ex)
            {
                _log.Error("Frm_ScheduleListReport => OnpenInfo:" + ex.Message);
                return "";
            }
        }
     
        private void OpenScheduleReport()
        {
            try
            {
            Frm_ScheduleJoReports scheduleJoReports = new Frm_ScheduleJoReports();
            scheduleJoReports.WindowState = FormWindowState.Maximized;
            scheduleJoReports.ShowDialog();
            Synchronized();
            }
            catch (Exception ex)
            {
                _log.Error("Frm_ScheduleListReport => OpenScheduleReport:" + ex.Message);
            }
        }
        private void OpenMaintenanceReport()
        {
            try
            {

            Frm_MaintenanceReports maintenanceReports = new Frm_MaintenanceReports();
            maintenanceReports.WindowState = FormWindowState.Maximized;
            maintenanceReports.ShowDialog();
            Synchronized();
            }
            catch (Exception ex)
            {
                _log.Error("Frm_ScheduleListReport => OpenScheduleReport:" + ex.Message);
            }
        }

        //--------------------------------------------------
        #endregion

        //------------------------------------

        #region Event control

        #endregion

        //------------------------------------

    }
}
