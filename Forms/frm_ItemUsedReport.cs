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
using E00_API;
using E00_API.Contract;
using E00_API.Contract.TaiSan;
using E00_API.TaiSan;
using E00_Base;
using E00_Model;
using E00_SafeCacheDataService.Base;
using E00_SafeCacheDataService.Common;
using E00_SafeCacheDataService.Interface;
using QuanLyTaiSan.Maintenance;

namespace QuanLyTaiSan.Forms
{
    public partial class frm_ItemUsedReport : frm_Base
    {
        //-----------------------------------------------------------

        #region Member
        private api_TaiSanSuDungLL _defaultDataService;
        private Lazy<DataTable> _defaultData;
        private SynchronizationContext _syncContext;
        protected API_Common<TaiSanSuDungLLInfo> _sudungllDataService;
        #endregion

        //-----------------------------------------------------------

        #region Contructor
        public frm_ItemUsedReport()
        {

            InitializeComponent();
            
            _syncContext = SynchronizationContext.Current;
            this.Load += Frm_Load;
            colInfo.Click += (send, e) => OpenInfo();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            dgvGrid.AutoGenerateColumns = false;
            _defaultDataService = new api_TaiSanSuDungLL();
            gvgContainer.Initialize("");
            btnClose.Click += (send, q) => { this.Close(); };
            Synchronized();
        }

        #endregion

        //-----------------------------------------------------------

        #region Private Method
        private void OpenInfo()
        {
            if (dgvGrid.CurrentRow == null) return;
            var rowSelected = dgvGrid.CurrentRow.DataBoundItem as DataRowView;
            if (!string.IsNullOrEmpty("" + rowSelected.Row[cls_TaiSan_SuDungLL.col_MaVach]))
            {
                Frm_PossessionsInfomation frmInfo = new Frm_PossessionsInfomation();
                frmInfo.ItemID = "" + rowSelected.Row[cls_TaiSan_SuDungLL.col_MaVach];
                frmInfo.ShowDialog();
            }
        }
        private void Synchronized()
        {
            try
            {
                gvgContainer.StartWaiting("");
                pnlTitle.Enabled = false;
                _defaultData = new Lazy<DataTable>(() => _defaultDataService.Get_TaiSanSuDung());
                var _theardRun = new Thread(new ThreadStart(() =>
                {
                    try
                    {
                        var data = _defaultData.Value;
                        _syncContext.Send(state =>
                        {
                            gvgContainer.DataSource = data;
                            pnlTitle.Enabled = true;
                        }, null);
                    }
                    catch (Exception ex)
                    {
                        _syncContext.Send(state =>
                        {
                            pnlTitle.Enabled = true;
                        }, null);
                    }

                }));
                _theardRun.IsBackground = true;
                _theardRun.Start();
            }
            catch (Exception ex)
            {
                pnlTitle.Enabled = true;
            }

        }
        #endregion

        //-----------------------------------------------------------

        #region Event Control

        #endregion

        //-----------------------------------------------------------

    }
}
