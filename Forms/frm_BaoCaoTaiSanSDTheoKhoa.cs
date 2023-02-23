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
using System.Threading;
using QuanLyTaiSan.Common;
using E02_BenhNhan;
using System.IO;
using ClosedXML.Excel;
using E00_ControlNew.Common;
using DevComponents.DotNetBar;

namespace QuanLyTaiSan
{
    public partial class frm_BaoCaoTaiSanSDTheoKhoa : frm_Base
    {
        //**-------------------------------------------------------------------------------

        #region Member

        private api_TaiSan _apiTaiSan;
        private Lazy<DataTable> _dataCboDepartment;
        private string _sqlWhere = "", _deparment = "";
        private SynchronizationContext _syncontext;
        private FrmLoading _frmLoading;
        private XMLReader _xMLReader;
        private DataTable _dtaExport;
        #endregion

        //**-------------------------------------------------------------------------------

        #region Constructor
        public frm_BaoCaoTaiSanSDTheoKhoa()
        {
            _syncontext = SynchronizationContext.Current;
            InitializeComponent();
            this.ActiveControl = chkAll;
            dgvMain.AutoGenerateColumns = false;
            this.btnFind.Click += (send, e) => Synchronized();
            this.btnExport.Click += (send, e) => Export();
            this.btnClose.Click += (send, e) => { this.Close(); };
            chkByDeparment.CheckedChanged += (send, e) => CheckDeparmentChanged();
            KeyDown += Frm_BaoCaoTaiSanSDTheoKhoa_KeyDown;
        }

        #endregion

        //**-------------------------------------------------------------------------------

        #region Public Method
        public void SetParamater()
        {
            _apiTaiSan = new api_TaiSan();
            _xMLReader = new XMLReader();
            DataLoading();
        }

        #endregion

        //**-------------------------------------------------------------------------------

        #region Private Method
        private void Synchronized()
        {
            if (_syncontext == null) return;
            _frmLoading = new FrmLoading(GridBinding);
            _frmLoading.ShowDialog();
        }
        private void GridBinding()
        {
        _syncontext.Send(state => {
            _sqlWhere = "where (0=0) ";
            if (chkNotUsed.Checked)
                _sqlWhere += " And (SUDUNG = 0)";
            if (chkByDeparment.Checked)
            {
                _deparment = "" + cboDeparment.SelectedValue;
                if (string.IsNullOrEmpty(_deparment))
                {
                    _sqlWhere += "And (MAKPSUDUNG like '" + cboDeparment.Text + "') Or (TENKPSUDUNG like '" + cboDeparment.Text + "')";
                }
                else
                {
                    _sqlWhere += " And (MAKPSUDUNG = '" + _deparment + "')";
                }
            }

            dgvMain.DataSource = null;
            dgvMain.DataSource = _apiTaiSan.Get_TaiSanSDTheoBP(_sqlWhere);
        }, null);
        }
        private void DataLoading()
        {
            _dataCboDepartment = new Lazy<DataTable>(() => _apiTaiSan.Get_DanhMucBoPhan());
        }
        private void SetDataCombo()
        {
            cboDeparment.DataSource = _dataCboDepartment.Value;
            cboDeparment.DisplayMember = "TEN";
            cboDeparment.ValueMember = "MA";
        }
        private void Export()
        {
            //Exporting to Excel
            _dtaExport = dgvMain.DataSource as DataTable;
            if(_dtaExport != null)
            {
                DialogInfo infoDialog = _xMLReader.SaveAsPathExcel();
                if(infoDialog != null)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(_dtaExport, Languages.LSheetExportTaiSan);
                        wb.SaveAs(infoDialog.FullPath);
                    }
                    if (MessageBox.Show(Languages.LMessageOpentFile, Languages.LMessageTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        _xMLReader.OpenFilePath(infoDialog.FullPath);
                    }
                }
                
            }
           
        }
        #endregion

        //**-------------------------------------------------------------------------------

        #region EventControl
        private void CheckDeparmentChanged()
        {
            cboDeparment.Enabled = chkByDeparment.Checked;
            if (chkByDeparment.Checked && cboDeparment.DataSource == null)
            {
                SetDataCombo();
            }
        }
        private void Frm_BaoCaoTaiSanSDTheoKhoa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Helper.FindKey)
            {
                btnFind.PerformClick();
                return;
            }
            if (e.KeyCode == Helper.CloseFormKey)
            {
                btnClose.PerformClick();
                return;
            }
            if (e.KeyCode == Helper.ExportExcel)
            {
                btnExport.PerformClick();
                return;
            }
            
        }
        #endregion

        //**-------------------------------------------------------------------------------

    }
}
