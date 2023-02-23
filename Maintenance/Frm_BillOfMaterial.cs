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
using E00_API.Contract;
using E00_API.Contract.TaiSan;
using E00_API.Maintenance;
using E00_Base;
using E00_Helpers.Helpers;
using E00_Model;
using HISNgonNgu.Chung;
using HISNgonNgu.Maintenance;
using HISNgonNgu.NoiTru;
using QuanLyTaiSan.ViewBasic;
using QuanLyTaiSan.ViewEntity;

namespace QuanLyTaiSan.Maintenance
{
    public partial class Frm_BillOfMaterial : ListViewLeftFilterBasic<TaiSanSuDungLLInfo, TaiSanSuDungLLInfo, TaiSan_ChiTietLinhKienInfo, TaiSanSuDungLLInfo, TaiSanSuDungViewEntity>
    {
        //--------------------------------------------------------

        #region Member

        private api_TaiSan_ChiTietLinhKien _detailService;

        #endregion

        //--------------------------------------------------------

        #region Constructor

        public Frm_BillOfMaterial(): base()
        {
            InitializeComponent();
            dgvchitiet.AutoGenerateColumns = false;
            dgvPart.AutoGenerateColumns = false;
            dgvTaisan.AutoGenerateColumns = false;

            IsUsingReference = false;
            //txtMaNhap.DataBindings.Clear();
            //txtMaNhap.DataBindings.Add("Text", EditingEntity, "TNHAPCTID", true, DataSourceUpdateMode.OnPropertyChanged);

            //txtMaTaiSan.DataBindings.Clear();
            //txtMaTaiSan.DataBindings.Add("Text", EditingEntity, "TMA", true, DataSourceUpdateMode.OnPropertyChanged);

            //txtMaVach.DataBindings.Clear();
            //txtMaVach.DataBindings.Add("Text", EditingEntity, "TMAVACH", true, DataSourceUpdateMode.OnPropertyChanged);

            //txtTenTaiSan.DataBindings.Clear();
            //txtTenTaiSan.DataBindings.Add("Text", EditingEntity, "TTENTAISAN", true, DataSourceUpdateMode.OnPropertyChanged);

            //txtTenTheLoai.DataBindings.Clear();
            //txtTenTheLoai.DataBindings.Add("Text", EditingEntity, "TTENLOAI", true, DataSourceUpdateMode.OnPropertyChanged);

            //txtQuyCach.DataBindings.Clear();
            //txtQuyCach.DataBindings.Add("Text", EditingEntity, "TQUYCACH", true, DataSourceUpdateMode.OnPropertyChanged);

            //txtbaohanh.DataBindings.Clear();
            //txtbaohanh.DataBindings.Add("Text", EditingEntity, "TBAOHANH", true, DataSourceUpdateMode.OnPropertyChanged);

            //txtNguyenGia.DataBindings.Clear();
            //txtNguyenGia.DataBindings.Add("Text", EditingEntity, "TNGUYENGIA", true, DataSourceUpdateMode.OnPropertyChanged);

            //txtKyhieu.DataBindings.Clear();
            //txtKyhieu.DataBindings.Add("Text", EditingEntity, "TKYHIEU", true, DataSourceUpdateMode.OnPropertyChanged);

            //txtModel.DataBindings.Clear();
            //txtModel.DataBindings.Add("Text", EditingEntity, "TMODEL", true, DataSourceUpdateMode.OnPropertyChanged);

            //lblCreateBy.DataBindings.Clear();
            //lblCreateBy.DataBindings.Add("Text", EditingEntity, "CreateBy", true, DataSourceUpdateMode.OnPropertyChanged);

            this.btnAdd.Click += (send, e) => AddLinhkien();
            col_Delete.Click += (send, e) => DelectItem();
            btnInfo.Click += (send, e) => OpenInfo();
            Initialize();
            btnDetails.Click += (send, e) => OpenInfo();
        }

        private void OpenInfo()
        {
            if (!string.IsNullOrEmpty(""+EditingEntity.MAVACH))
            {
                Frm_PossessionsInfomation frmInfo = new Frm_PossessionsInfomation();
                frmInfo.ItemID = ""+EditingEntity.MAVACH;
                frmInfo.ShowDialog();
            }
        }
        #endregion

        //--------------------------------------------------------


        #region Public Method

        #endregion

        //--------------------------------------------------------


        #region Protected Method

        protected override void InitializeService()
        {
            try
            {

            base.InitializeService();
            _detailService = new api_TaiSan_ChiTietLinhKien();
                
            }
            catch (Exception ex)
            {
                _log.Error("Frm_BillOfMaterial => InitializeService:" + ex.Message);
            }
        }
        protected override void SynchronizedLeft()
        {
            try
            {
                gvgLeft.StartWaiting("");
                var _theardRun = new Thread(new ThreadStart(() => {
                    var data = _detailService.GetListLinhKien();
                    _syncContext.Send(state =>
                    {
                        try
                        {
                            gvgLeft.DataSource = data;

                            slbLinhKien.ColumDataList = new string[] { "MAVACH", "TEN", "MAVACH", "VITRI", "KYHIEU", "MODEL" };
                            slbLinhKien.ColumWidthList = new int[] { 120, 150, 150, 100, 100, 120 };
                            slbLinhKien.ValueMember = "MAVACH";
                            slbLinhKien.DisplayMember = "TEN";
                            slbLinhKien.DataSource = data.Copy();
                        }
                        catch (Exception ex)
                        { 
                        }
                      
                    }, null);
                }));
                _theardRun.IsBackground = true;
                _theardRun.Start();
            }
            catch (Exception ex)
            {
                _log.Error("Frm_BillOfMaterial => SynchronizedReference:" + ex.Message);
            }
        }
        protected override void SynchronizedDetail()
        {
            try
            {
            var _theardRun = new Thread(new ThreadStart(() => {
                var data = _detailService.GetListByMaster(""+EditingEntity.MAVACH);
                _syncContext.Send(state =>
                {
                    gvgDetail.DataSource = data;
                }, null);
            }));
            _theardRun.IsBackground = true;
            _theardRun.Start();
            }
            catch (Exception ex)
            {
                _log.Error("Frm_BillOfMaterial => SynchronizedDetail:" + ex.Message);
            }
        }
        protected override void SynchronizedReference()
        {
            //try
            //{
            //gvgRight.StartWaiting("");
            //var _theardRun = new Thread(new ThreadStart(() => {
            //    var data = _detailService.GetListLinhKien();
            //    _syncContext.Send(state =>
            //    {
            //        slbLinhKien.DataSource = data;
            //    }, null);
            //}));
            //_theardRun.IsBackground = true;
            //_theardRun.Start();
            //}
            //catch (Exception ex)
            //{
            //    _log.Error("Frm_BillOfMaterial => SynchronizedReference:" + ex.Message);
            //}
        }
        protected override bool ValidateDetail()
        {
            try
            {
                if (string.IsNullOrEmpty(slbLinhKien.txtMa.Text))
                {
                    TA_MessageBox.MessageBox.Show(LMaintenance.LLKSelectNull, TA_MessageBox.MessageIcon.Information);
                    return false;
                }
                _rowSelectedtmp = _detailService.GetListLinhKienTheoMa(slbLinhKien.txtMa.Text);
              
                if (_rowSelectedtmp == null)
                {
                    TA_MessageBox.MessageBox.Show(LNoiTru.LLSelectDetail, TA_MessageBox.MessageIcon.Information);
                    return false;
                }
                if (_rowSelectedtmp.Count == 0)
                {
                    TA_MessageBox.MessageBox.Show(LNoiTru.LLSelectDetail, TA_MessageBox.MessageIcon.Information);
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                _log.Error("ListViewLeftFilterBasic => ValidateDetail:" + ex.Message);
                return false;
            }
        }
        protected override void LeftSelectionChanged(DataRow row)
        {
            try
            {
                _masterInfo = _masterDataService.ConvertRowToInfo(row);
                if (_masterInfo != null)
                {
                    if (EditingEntity.KeyIDValue != _masterInfo.IDValue)
                    {
                        txtMaNhap.Text = "" + row[cls_TaiSan_NhapCTSP.col_NhapCTID];
                        txtMaTaiSan.Text = "" + row[cls_TaiSan_DanhMuc.col_Ma];
                        txtMaVach.Text = "" + row[cls_TaiSan_NhapCTSP.col_MaVach];
                        txtTenTaiSan.Text = "" + row[cls_TaiSan_DanhMuc.col_Ten];
                        txtTenTheLoai.Text = "" + row[cls_TaiSan_DanhMuc.col_TenLoai];
                        txtQuyCach.Text = "" + row[cls_TaiSan_DanhMuc.col_QuyCach];
                        txtbaohanh.Text = "" + row[cls_TaiSan_DanhMuc.col_BaoHanh];
                        txtNguyenGia.Text = "" + row[cls_TaiSan_DanhMuc.col_NguyenGia]; ;
                        txtKyhieu.Text = "" + row[cls_TaiSan_DanhMuc.col_KyHieu];
                        txtModel.Text = "" + row[cls_TaiSan_DanhMuc.col_Model];

                        EditingEntity.Set(_masterInfo);
                        ExitEditMode();
                        SynchronizedDetail();
                    }
                }

            }
            catch (Exception ex)
            {
                _log.Error("ListViewLeftFilterBasic => LeftSelectionChanged:" + ex.Message);
            }
        }
        #endregion

        //--------------------------------------------------------

        #region Private Method

        private void AddLinhkien()
        {
            try
            {
                
                    if (ValidateDetail())
                    {
                        bool isRefresh = false;
                        foreach (var item in _rowSelectedtmp)
                        {

                            var infoCheck = _detailDataService.Get_Item("(("+ cls_TaiSan_ChiTietLinhKien.col_MaLinhKien + "=" + item[cls_TaiSan_NhapCTSP.col_MaVach] + ") and ("+ cls_TaiSan_ChiTietLinhKien.col_TrangThai + "=0)) or ((" 
                                + cls_TaiSan_ChiTietLinhKien.col_MaTaiSan + " = " + item[cls_TaiSan_NhapCTSP.col_MaVach] + ") and ("+ cls_TaiSan_ChiTietLinhKien.col_MaLinhKien + "= "+ EditingEntity.MAVACH +")) ");
                            if (infoCheck == null)
                            {
                                _detailInfo = new TaiSan_ChiTietLinhKienInfo();
                                _detailInfo.CTLKID = _detailDataService.CreateNewID(20);
                                _detailInfo.MaTaiSan = EditingEntity.MAVACH;
                                _detailInfo.MaLinhKien =""+ item[cls_TaiSan_NhapCTSP.col_MaVach];
                                _detailInfo.USERID = Helper.ConvertSToDec(E00_System.cls_System.sys_UserID);
                                _detailInfo.USERUD = _detailInfo.USERID;
                                _detailInfo.NgayTao = DateTime.Now;
                                _detailInfo.NgayUD = DateTime.Now;
                                _detailInfo.MACHINEID = _detailDataService.GetMachineID();
                                _resultInfo = _detailDataService.Insert(_detailInfo);
                                if (_resultInfo.Status)
                                {
                                    isRefresh = true;
                                }
                                else
                                {
                                    TA_MessageBox.MessageBox.Show(LNNChung.EUpdateFailed, TA_MessageBox.MessageIcon.Information);
                                }
                            }else
                            {
                                TA_MessageBox.MessageBox.Show(LMaintenance.LLKDaTonTai, TA_MessageBox.MessageIcon.Information);
                            }
                        }
                        if (isRefresh) {
                            SynchronizedDetail();
                        slbLinhKien.clear();
                        slbLinhKien.txtTen.Focus();
                           // SynchronizedReference();
                        }
             
            }
            }
            catch (Exception ex)
            {
                _log.Error("Frm_BillOfMaterial => AddLinhkien:" + ex.Message);
            }
        }
        private void DelectItem()
        {
            try
            {

            var rowSelect = gvgDetail.GridView.CurrentRow.DataBoundItem as DataRowView;
            if(rowSelect != null)
            {
                if(TA_MessageBox.MessageBox.Show(string.Format(LNNChung.LInfoDelete,rowSelect[cls_TaiSan_DanhMuc.col_Ten]),TA_MessageBox.MessageIcon.Question) == DialogResult.Yes)
                {
                    _resultInfo = _detailService.Delete((object)(rowSelect[cls_TaiSan_ChiTietLinhKien.col_CTLKID]));
                    if (_resultInfo.Status)
                    {
                        SynchronizedDetail();
                    }else
                    {
                        TA_MessageBox.MessageBox.Show(LNNChung.LInfoDeleteFail + " " + _resultInfo.SystemError,TA_MessageBox.MessageIcon.Error);
                    }
                }
            }
            }
            catch (Exception ex)
            {
                _log.Error("Frm_BillOfMaterial => DelectItem:" + ex.Message);
            }
        }
        #endregion

        //--------------------------------------------------------

        #region Event control

        #endregion

        //--------------------------------------------------------


    }
}
