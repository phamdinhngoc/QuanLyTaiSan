using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using E00_API.Contract;
using E00_API.Contract.Kho;
using E00_API.Contract.TaiSan;
using E00_Base;
using E00_Helpers.Common.Class;
using E00_Model;
using E00_SafeCacheDataService.Base;
using E00_SafeCacheDataService.Common;
using E00_SafeCacheDataService.Interface;

namespace QuanLyTaiSan.Forms
{
    public partial class frm_ProductDetails : frm_Base
    {
        private IAPI<TaiSan_NhapCTSPInfo> _defaultService;
        private IAPI<ViTriKhoInfo> _locationService;

        
        DataTable _tmpTable;
        public decimal NHAPCTID { get; set; }
        public string SProductName { get; set; }
        public frm_ProductDetails()
        {
            InitializeComponent();

            this.Load += Frm_ProductDetails_Load;
            btnSave.Click += (send, e) => RequestUpdate();
            btnDismiss.Click += (send, e) => Synchronized();
            btnClose.Click += (send, e) => { this.Close(); };
            btnAllLocation.Click += BtnAllLocation_Click;
        }
        private void BtnAllLocation_Click(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow == null) return;
            DataRowView row = dgvData.CurrentRow.DataBoundItem as DataRowView;
            _tmpTable = dgvData.DataSource as DataTable;
            if (row != null && _tmpTable != null)
            {
                if(TA_MessageBox.MessageBox.Show($"Bạn có chắc muốn bỏ tất cả vào vị trí {row.Row[cls_TaiSan_NhapCTSP.col_ViTri]} không?",TA_MessageBox.MessageIcon.Question) == DialogResult.Yes)
                {
                    foreach (DataRow rowItem in _tmpTable.Rows)
                    {
                        if (("" + rowItem[cls_TaiSan_NhapCTSP.col_SPID]) != ("" + row.Row[cls_TaiSan_NhapCTSP.col_SPID]))
                        {
                            rowItem[cls_TaiSan_NhapCTSP.col_ViTri] = row.Row[cls_TaiSan_NhapCTSP.col_ViTri];
                        }
                    }
                }
                
            }
        }
        private void RequestUpdate()
        {
            _tmpTable = dgvData.DataSource as DataTable;
            if(_tmpTable != null)
            {
                ResultInfo info =  _defaultService.Update(_tmpTable);
                if (info.Status)
                {
                    TA_MessageBox.MessageBox.Show("Cập nhật thành công.", TA_MessageBox.MessageIcon.Information);
                }else
                {
                    TA_MessageBox.MessageBox.Show("Cập nhật thất bại, vui lòng kiểm tra và thử lại:" + info.SystemError, TA_MessageBox.MessageIcon.Information);
                }
            }
        }
        public void SetData(IAPI<TaiSan_NhapCTSPInfo> defaultService,decimal nhapCTID,string productName )
        {
            _defaultService = defaultService;
            NHAPCTID = nhapCTID;
            SProductName = productName;
        }
        private void Frm_ProductDetails_Load(object sender, EventArgs e)
        {
            lblTitle.Text = SProductName.ToUpper();
            _locationService = new API_Common<ViTriKhoInfo>();

            Synchronized();
            col_TinhTrang.DisplayMember = "Name";
            col_TinhTrang.ValueMember = "Code";
            col_TinhTrang.DataSource = GlobalMember.GetListSatusPart;

            col_ViTri.DisplayMember = cls_D_ViTriKho.col_TenViTri;
            col_ViTri.ValueMember = cls_D_ViTriKho.col_TenViTri;
            col_ViTri.DataSource = _locationService.Get_Data(cls_D_ViTriKho.col_HoatDong + " = 1");
            
        }
        private void Synchronized()
        {
            _tmpTable = _defaultService.Get_Data($"{cls_TaiSan_NhapCTSP.col_NhapCTID} = {NHAPCTID}");
            dgvData.DataSource = _tmpTable;
        }
    }
}
