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
using E00_API.TaiSan;
using E00_Base;
using E00_Helpers.Helpers;
using E00_Model;
using E00_SafeCacheDataService.Common;

namespace QuanLyTaiSan.Forms
{
    public partial class frm_RemainingReports : frm_Base
    {
        //-----------------------------------------------------------

        #region Member
        private api_TaiSan_ThuHoiLL _defaultDataService;
        private Lazy<DataTable> _defaultData;
        private SynchronizationContext _syncContext;
        private ResultInfo _resultInfo;
        DataRowView _itemRow;
        #endregion

        //-----------------------------------------------------------

        #region Contructor
        public frm_RemainingReports()
        {
            InitializeComponent();
            _syncContext = SynchronizationContext.Current;
            this.Load += Frm_Load;
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            dgvGrid.AutoGenerateColumns = false;
            _defaultDataService = new api_TaiSan_ThuHoiLL();
            dtiToDate.Value = DateTime.Now;
            dtiFromDate.Value = DateTime.Now.AddDays(-30);
            gvgContainer.Initialize("");
            chkAll.CheckedChanged += ChkAll_CheckedChanged;
            btnClose.Click += (send, q) => { this.Close(); };
            btnDelete.Click += (send, q) => DeleteItemSelected();
            btnEdit.Click += (send, q) => OpenEditForm();
            btnAdd.Click += (send, q) => OpenNew();
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
                _defaultData = new Lazy<DataTable>(()=> _defaultDataService.Get_RemainningList(chkAll.Checked, dtiFromDate.Value, dtiToDate.Value));
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

        private void ChkAll_CheckedChanged(object sender, EventArgs e)
        {
            Synchronized();
        }
        private void DeleteItemSelected()
        {
            if (dgvGrid.CurrentRow != null)
            {
                _itemRow = dgvGrid.CurrentRow.DataBoundItem as DataRowView;
                if (_itemRow != null)
                {
                    if (Helper.ConvertSToDec("" + _itemRow["Total"]) == 0 && !Helper.ConvertSToBool("" + _itemRow[cls_TaiSan_ThuHoiLL.col_TrangThai]))
                    {
                        if(TA_MessageBox.MessageBox.Show($"Bạn có chắc chắn muốn xóa phiếu {_itemRow[cls_TaiSan_ThuHoiLL.col_SoChungTu]} không?",TA_MessageBox.MessageIcon.Question) == DialogResult.Yes)
                        {
                            _resultInfo = _defaultDataService.Delete(_itemRow[cls_TaiSan_ThuHoiLL.col_THID]);
                            if (_resultInfo.Status)
                            {
                                Synchronized();
                            }
                            else
                            {
                                TA_MessageBox.MessageBox.Show("Xóa không thành công, vui lòng kiểm tra và thử lại." + _resultInfo.SystemError, TA_MessageBox.MessageIcon.Error);
                            }
                        }
                       
                    }
                    else
                    {
                        TA_MessageBox.MessageBox.Show("Phiếu không thế xóa.", TA_MessageBox.MessageIcon.Error);
                    }
                }
            }
        }
        private void OpenEditForm()
        {
            if (dgvGrid.CurrentRow == null) return;
            _itemRow = dgvGrid.CurrentRow.DataBoundItem as DataRowView;
            if (_itemRow != null)
            {
                frm_Remaining frm = new frm_Remaining();
                frm.SetData("" + _itemRow[cls_TaiSan_ThuHoiLL.col_THID], "" + _itemRow[cls_TaiSan_ThuHoiLL.col_SoChungTu]);
                frm.ShowDialog();
                Synchronized();
            }

        }
        private void OpenNew()
        {
            frm_Remaining frm = new frm_Remaining();
            frm.SetData("", "");
            frm.ShowDialog();
            Synchronized();
        }

        #endregion

        //-----------------------------------------------------------

    }
}
