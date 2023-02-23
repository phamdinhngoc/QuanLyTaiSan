using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using E00_API;
using E00_API.Contract.TaiSan;
using E00_API.TaiSan;
using E00_Base;
using E00_Helpers.Helpers;
using E00_Model;
using E00_SafeCacheDataService.Common;
using QuanLyTaiSan.Maintenance;

namespace QuanLyTaiSan.Forms
{
    public partial class frm_StockMovement : frm_Base
    {
        //-----------------------------------------------------------

        #region Member
        private api_TaiSan _defaultDataService;
        private DataTable _defaultData;
        private SynchronizationContext _syncContext;
        private ResultInfo _resultInfo;
        DataRowView _itemRow;
        #endregion

        //-----------------------------------------------------------

        #region Contructor
        public frm_StockMovement()
        {
            InitializeComponent();
            _syncContext = SynchronizationContext.Current;
            this.Load += Frm_DS_NhapTaiSan_Load;
        }
        private void Frm_DS_NhapTaiSan_Load(object sender, EventArgs e)
        {
            dgvGrid.AutoGenerateColumns = false;
            _defaultDataService = new api_TaiSan();
            dtiToDate.Value = DateTime.Now;
            dtiFromDate.Value = DateTime.Now.AddDays(-30);
            gvgContainer.Initialize("");
            btnClose.Click += (send, q) => { this.Close(); };
            dtiFromDate.ValueChanged += (send, q) => Synchronized();
            dtiToDate.ValueChanged += (send, q) => Synchronized();
            Synchronized();
        }

        #endregion

        //-----------------------------------------------------------

        #region Private Method
        private void Synchronized()
        {
            try
            {
                gvgContainer.StartWaiting("");
                pnlTitle.Enabled = false;

                var _theardRun = new Thread(new ThreadStart(() =>
                {
                    try
                    {
                        _syncContext.Send(state =>
                        {
                            _defaultData = _defaultDataService.Get_StockMovement(dtiFromDate.Value, dtiToDate.Value);
                            gvgContainer.DataSource = _defaultData;
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

    }
}
