using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using E00_API;
using E00_API.Contract;
using E00_API.Contract.Kho;
using E00_API.Contract.TaiSan;
using E00_API.TaiSan;
using E00_Base;
using E00_ControlAdv.ViewUI;
using E00_Model;
using E00_SafeCacheDataService.Base;
using E00_SafeCacheDataService.Common;
using E00_SafeCacheDataService.Interface;

namespace QuanLyTaiSan.Forms
{
    
    public partial class frm_LocationMovement : frm_Base
    {
        //-------------------------------------------------------------

        #region Member
        private IAPI<ViTriKhoInfo> _locationService;
        private IAPI<TaiSan_LSCDInfo> _historyService;
        private IAPI<TaiSan_NhapCTSPInfo> _receiveProductService;
        private IAPI<TaiSan_ThuHoiCTInfo> _revokeDetailService;
        private api_TaiSanSuDungLL _usingService;
        private api_TaiSan _api_TaiSan;
        private DataTable _tmpData;
        private ViTriKhoInfo _locationInfo;
        private DataRow _rowSelected;
        private TaiSan_LSCDInfo _historyInfo;
        private ResultInfo _resultInfo;

        #endregion

        //-------------------------------------------------------------

        #region Constructor
        public frm_LocationMovement()
        {
            InitializeComponent();
            dgvSource.AutoGenerateColumns = false;
            dgvDes.AutoGenerateColumns = false;
            Load += (send, e) => Loading() ;
            slbLocationSou.HisSelectChange += (send, e) => FindSourceInventory(slbLocationSou.txtTen.Text,gvgSou);
            slbLocationDes.HisSelectChange += (send, e) => FindSourceInventory(slbLocationDes.txtTen.Text,gvgDes);
            slbZoneDes.HisSelectChange += (send, e) => Get_Floor(slbZoneDes.txtMa.Text,CategoryLoadSource.Des);
            slbZoneSou.HisSelectChange += (send, e) => Get_Floor(slbZoneSou.txtMa.Text, CategoryLoadSource.Sou);

            slbFloorDes.HisSelectChange += (send, e) => Get_FuncRom(slbFloorDes.txtMa.Text, CategoryLoadSource.Des);
            slbFloorSou.HisSelectChange += (send, e) => Get_FuncRom(slbFloorSou.txtMa.Text, CategoryLoadSource.Sou);

            btnFindSou.Click += (send, e) => SearchSou();
            btnFindDes.Click += (send, e) => SearchDes();

            btnMoveTo.Click += (send, e) => MovedSToD();
            btnDMovetoS.Click += (send, e) => MovedDToS();
            chkStockOnHand.CheckedChanged += (SendKeys, e) => onCheckChanged();
            btnClose.Click += (send, e) => { Close(); };
        }

        #endregion

        //-------------------------------------------------------------

        #region Public Method

        #endregion

        //-------------------------------------------------------------

        #region Private Method

        private void Loading()
        {
            _locationService = new API_Common<ViTriKhoInfo>();
            _historyService = new API_Common<TaiSan_LSCDInfo>();
            _receiveProductService = new API_Common<TaiSan_NhapCTSPInfo>();
            _revokeDetailService = new API_Common<TaiSan_ThuHoiCTInfo>();
            _usingService = new api_TaiSanSuDungLL();
            _api_TaiSan = new api_TaiSan();
            gvgSou.Initialize();
            gvgDes.Initialize();

            DataTable tmpLocation = _locationService.Get_Data($"{cls_D_ViTriKho.col_HoatDong} = 1");
            if(tmpLocation != null)
            {
                slbLocationDes.DataSource = tmpLocation.Copy();
                slbLocationSou.DataSource = tmpLocation.Copy();
            }

            Get_Zone("-1", CategoryLoadSource.All);
            Get_Floor("-1", CategoryLoadSource.All);
            Get_FuncRom("-1", CategoryLoadSource.All);
        }

        private void FindSourceInventory(string location, GridContainerAdv grid)
        {
            if (chkStockOnHand.Checked)
            {
                _tmpData = _api_TaiSan.Get_StockOnhandDetailByLocation(location);
                grid.DataSource = _tmpData;
            }
        }
        private void MovedSToD()
        {
            _rowSelected = GetSelectObjectSou();
            if (chkStockOnHand.Checked)
            {
                onMoveToLocation(slbLocationSou.txtTen.Text, slbLocationDes.txtTen.Text);
            }else {
                onMoveToUsing(slbZoneSou.txtMa.Text, slbFloorSou.txtMa.Text, slbFuncRomSou.txtMa.Text, slbZoneDes.txtMa.Text, slbFloorDes.txtMa.Text, slbFuncRomDes.txtMa.Text);
            }
        }
        private void MovedDToS()
        {
            _rowSelected = GetSelectObjectDec();
            if (chkStockOnHand.Checked)
            {
                onMoveToLocation(slbLocationDes.txtTen.Text, slbLocationSou.txtTen.Text);
            }
            else
            {
                onMoveToUsing(slbZoneDes.txtMa.Text,slbFloorDes.txtMa.Text,slbFuncRomDes.txtMa.Text, slbZoneSou.txtMa.Text, slbFloorSou.txtMa.Text, slbFuncRomSou.txtMa.Text);
            }
        }
        private void onMoveToLocation(string from, string to)
        {
            if (string.IsNullOrEmpty(from)) { TA_MessageBox.MessageBox.Show("Vui lòng chọn vị trí nguồn."); return; }
            if (string.IsNullOrEmpty(to)) { TA_MessageBox.MessageBox.Show("Vui lòng chọn vị trí đích."); return; }
            _locationInfo = _locationService.Get_Item($"{cls_D_ViTriKho.col_TenViTri} = '{to}'");
            if (_locationInfo != null && _rowSelected != null)
            {
                if(from == to)
                {
                    TA_MessageBox.MessageBox.Show("Vị trí nguồn và vị trí đích trùng, không thể chuyển.");
                    return;
                }
                if(TA_MessageBox.MessageBox.Show($"Bạn có chắc chắn muốn chuyển {_rowSelected["TENTAISAN"]} từ vị trí {from} tới vị trí {to}",TA_MessageBox.MessageIcon.Question) == DialogResult.Yes)
                {
                    onUpdateInventoty(_rowSelected, _locationInfo, from);
                }
            }
        }
        private void onMoveToUsing(string zoneS, string floorS,string funcRomS, string zoneD, string floorD, string funcRomD)
        {
            if (_rowSelected != null)
            {
                if (TA_MessageBox.MessageBox.Show($"Bạn có chắc chắn muốn chuyển {_rowSelected["TENTAISAN"]} từ vị trí khu {zoneS},tầng {floorS}, phòng công năng {funcRomS} tới vị trí khu {zoneD},tầng {floorD}, phòng công năng {funcRomD} không?", TA_MessageBox.MessageIcon.Question) == DialogResult.Yes)
                {
                    onUpdateUsing(_rowSelected, zoneS, floorS, funcRomS, zoneD, floorD,funcRomD);
                }
            }
        }
        private DataRow GetSelectObjectSou()
        {
            try
            {
                return (dgvSource.CurrentRow.DataBoundItem as DataRowView).Row;
            }
            catch (Exception)
            {
                return null;
            }
        }
        private DataRow GetSelectObjectDec()
        {
            try
            {
                return (dgvDes.CurrentRow.DataBoundItem as DataRowView).Row;
            }
            catch (Exception)
            {
                return null;
            }
        }
        private void onUpdateInventoty(DataRow tmpRow, ViTriKhoInfo locationDes, string vitringuon)
        {
            switch (""+tmpRow["CATEGORY"])
            {
                case "NEW":
                    _resultInfo = onRequestUpdateHistoryInv(tmpRow, locationDes, vitringuon);
                    if (_resultInfo.Status)
                    {
                        _resultInfo = onUpdateLocationReceive(tmpRow, locationDes);
                        if (_resultInfo.Status)
                        {
                            FindSourceInventory(slbLocationSou.txtTen.Text, gvgSou);
                            FindSourceInventory(slbLocationDes.txtTen.Text, gvgDes);
                        }else
                        {
                            _resultInfo = _historyService.Delete(_historyInfo.LSID);
                            TA_MessageBox.MessageBox.Show("Cập nhập vị trí không thành công." + _resultInfo.SystemError);
                        }
                    }else
                    {
                        TA_MessageBox.MessageBox.Show("Cập nhập lịch sử không thành công." + _resultInfo.SystemError);
                    }
                    break;
                case "REVOKE":
                    _resultInfo = onRequestUpdateHistoryInv(tmpRow, locationDes, vitringuon);
                    if (_resultInfo.Status)
                    {
                        _resultInfo = onUpdateLocationRevoke(tmpRow, locationDes);
                        if (_resultInfo.Status)
                        {
                            FindSourceInventory(slbLocationSou.txtTen.Text, gvgSou);
                            FindSourceInventory(slbLocationDes.txtTen.Text, gvgDes);
                        }
                        else
                        {
                            _resultInfo = _historyService.Delete(_historyInfo.LSID);
                            TA_MessageBox.MessageBox.Show("Cập nhập vị trí không thành công." + _resultInfo.SystemError);
                        }
                    }
                    else
                    {
                        TA_MessageBox.MessageBox.Show("Cập nhập lịch sử không thành công." + _resultInfo.SystemError);
                    }
                    break;
               
            }
        }
        private void onUpdateUsing(DataRow tmpRow, string zoneS, string floorS, string funcRomS, string zoneD, string floorD, string funcRomD)
        {
            _resultInfo = onRequestUpdateHistoryUss(tmpRow, zoneS, floorS, funcRomS, zoneD, floorD, funcRomD);
            if (_resultInfo.Status)
            {
                _resultInfo = onUpdateLocationUsing(tmpRow, zoneD, floorD, funcRomD);
                if (_resultInfo.Status)
                {
                    SearchSou();
                    SearchDes();
                }
                else
                {
                    TA_MessageBox.MessageBox.Show("Cập nhập vị trí không thành công." + _resultInfo.SystemError);
                }
            }
            else
            {
                TA_MessageBox.MessageBox.Show("Cập nhập lịch sử không thành công." + _resultInfo.SystemError);
            }
        }
        private ResultInfo onRequestUpdateHistoryInv(DataRow tmpRow, ViTriKhoInfo locationDes,string vitringuon)
        {
            _historyInfo = new TaiSan_LSCDInfo();
            _historyInfo.LSID = _historyService.CreateNewID(20);
            _historyInfo.SPID = E00_Helpers.Helpers.Helper.ConvertSToDec(tmpRow[cls_TaiSan_NhapCTSP.col_SPID]);
            _historyInfo.MaVach =""+ tmpRow[cls_TaiSan_NhapCTSP.col_MaVach];
            _historyInfo.GhiChu = txtGhiChu.Text;
            _historyInfo.TrangThai = 1;
            _historyInfo.MACHINEID = _historyService.GetMachineID();
            _historyInfo.ViTriNguon = vitringuon;
            _historyInfo.ViTriDich = locationDes.TenViTri;
            _historyInfo.Loai = cls_sys_LoaiDanhMuc.SCategoryMoveInventory;
            _historyInfo.HinhThuc = "Move";
            _resultInfo = _historyService.Insert(_historyInfo);
            return _resultInfo;
        }
        private ResultInfo onRequestUpdateHistoryUss(DataRow tmpRow, string zoneS, string floorS, string funcRomS, string zoneD, string floorD, string funcRomD)
        {
            _historyInfo = new TaiSan_LSCDInfo();
            _historyInfo.LSID = _historyService.CreateNewID(20);
            _historyInfo.SPID = E00_Helpers.Helpers.Helper.ConvertSToDec(tmpRow[cls_TaiSan_SuDungLL.col_NhapCTID]);
            _historyInfo.MaVach = "" + tmpRow[cls_TaiSan_SuDungLL.col_MaVach];
            _historyInfo.GhiChu = txtGhiChu.Text;
            _historyInfo.TrangThai = 1;
            _historyInfo.MACHINEID = _historyService.GetMachineID();
            _historyInfo.Loai = cls_sys_LoaiDanhMuc.SCategoryMoveUsing;
            _historyInfo.HinhThuc = "Move";

            _historyInfo.KhuNguon = zoneS;
            _historyInfo.KhuDich = zoneD;
            _historyInfo.TangNguon = floorS;
            _historyInfo.TangDich = floorD;
            _historyInfo.PCNNguon = funcRomS;
            _historyInfo.PCNDich = funcRomD;

            _resultInfo = _historyService.Insert(_historyInfo);
            return _resultInfo;
        }
        private ResultInfo onUpdateLocationReceive(DataRow tmpRow, ViTriKhoInfo locationDes)
        {
            _resultInfo = _receiveProductService.Update($"{cls_TaiSan_NhapCTSP.col_ViTri} = '{locationDes.TenViTri}'",$"{cls_TaiSan_NhapCTSP.col_SPID} = {tmpRow[cls_TaiSan_NhapCTSP.col_SPID]}");
            return _resultInfo;
        }
        private ResultInfo onUpdateLocationRevoke(DataRow tmpRow, ViTriKhoInfo locationDes)
        {
            _resultInfo = _revokeDetailService.Update($"{cls_TaiSan_ThuHoiCT.col_ViTri} = '{locationDes.TenViTri}'", $"{cls_TaiSan_ThuHoiCT.col_THCTID} = {tmpRow[cls_TaiSan_NhapCTSP.col_SPID]}");
            return _resultInfo;
        }
        private ResultInfo onUpdateLocationUsing(DataRow tmpRow, string zoneD, string floorD, string funcRomD)
        {
            _resultInfo = _usingService.Update($"{cls_TaiSan_SuDungLL.col_MaKhu} = '{zoneD}',{cls_TaiSan_SuDungLL.col_MaTang} = '{floorD}',{cls_TaiSan_SuDungLL.col_MaPhongCongNang}='{funcRomD}'", $"{cls_TaiSan_SuDungLL.col_Ma} = {tmpRow[cls_TaiSan_SuDungLL.col_Ma]}");
            return _resultInfo;
        }

        private void onCheckChanged()
        {
            Clear();
            bool status = chkStockOnHand.Checked;
            slbLocationSou.Enabled = status;
            slbLocationDes.Enabled = status;

            slbZoneSou.Enabled = !status;
            slbZoneDes.Enabled = !status;
            slbFloorSou.Enabled = !status;
            slbFloorDes.Enabled = !status;
            slbFuncRomDes.Enabled = !status;
            slbFuncRomSou.Enabled = !status;
            txtCode.Enabled = !status;

            col_ViTri.Visible = status;
            col_Loai.Visible = status;
            col_HanBaoHanh.Visible = status;
            col_TinhTrang.Visible = status;

            colD_ViTri.Visible = status;
            colD_Loai.Visible = status;
            colD_HanBaohanh.Visible = status;
            colD_TinhTrang.Visible = status;

            col_NgaySuDung.Visible = !status;
            col_TenKPQL.Visible = !status;
            col_TenKPSD.Visible = !status;

            colD_NgaySuDung.Visible = !status;
            colD_TenKPQL.Visible = !status;
            colD_TenKPSD.Visible = !status;

            if (status)
            {
                col_ViTri.DataPropertyName = "VITRI";
                col_Loai.DataPropertyName = "TYPEINPUT";
                col_HanBaoHanh.DataPropertyName = "HANBAOHANH";
                col_TinhTrang.DataPropertyName = "TINHTRANG";

                colD_ViTri.DataPropertyName = "VITRI";
                colD_Loai.DataPropertyName = "TYPEINPUT";
                colD_HanBaohanh.DataPropertyName = "HANBAOHANH";
                colD_TinhTrang.DataPropertyName = "TINHTRANG";

                col_NgaySuDung.DataPropertyName = null;
                col_TenKPQL.DataPropertyName = null;
                col_TenKPSD.DataPropertyName = null;
                colD_NgaySuDung.DataPropertyName = null;
                colD_TenKPQL.DataPropertyName = null;
                colD_TenKPSD.DataPropertyName = null;
            }
            else
            {
                col_ViTri.DataPropertyName = null;
                col_Loai.DataPropertyName = null;
                col_HanBaoHanh.DataPropertyName = null;
                col_TinhTrang.DataPropertyName = null;

                colD_ViTri.DataPropertyName = null;
                colD_Loai.DataPropertyName = null;
                colD_HanBaohanh.DataPropertyName = null;
                colD_TinhTrang.DataPropertyName = null;

                col_NgaySuDung.DataPropertyName = "NGAYSUDUNG";
                col_TenKPQL.DataPropertyName = "TENKPQUANLY";
                col_TenKPSD.DataPropertyName = "TENKPSUDUNG";

                colD_NgaySuDung.DataPropertyName = "NGAYSUDUNG";
                colD_TenKPQL.DataPropertyName = "TENKPQUANLY";
                colD_TenKPSD.DataPropertyName = "TENKPSUDUNG";
            }
        }
        private void Get_FuncRom(string group, CategoryLoadSource category)
        {
          
            _tmpData = _api_TaiSan.Get_DanhMuc(cls_sys_LoaiDanhMuc.phong_MaLoai, group);
            if (_tmpData != null)
            {
                switch (category)
                {
                    case CategoryLoadSource.All:
                        slbFuncRomSou.clear();
                        slbFuncRomDes.clear();
                        slbFuncRomSou.DataSource = _tmpData.Copy();
                        slbFuncRomDes.DataSource = _tmpData.Copy();
                        break;
                    case CategoryLoadSource.Sou:
                        slbFuncRomSou.clear();
                        slbFuncRomSou.DataSource = _tmpData.Copy();
                        break;
                    case CategoryLoadSource.Des:
                        slbFuncRomDes.clear();
                        slbFuncRomDes.DataSource = _tmpData.Copy();
                        break;
                    default:
                        slbFuncRomSou.clear();
                        slbFuncRomDes.clear();
                        slbFuncRomSou.DataSource = _tmpData.Copy();
                        slbFuncRomDes.DataSource = _tmpData.Copy();
                        break;
                }
               
            }
        }
        private void Get_Floor(string group, CategoryLoadSource category)
        {
            
            _tmpData = _api_TaiSan.Get_DanhMuc(cls_sys_LoaiDanhMuc.tang_MaLoai, group);
            if (_tmpData != null)
            {
                switch (category)
                {
                    case CategoryLoadSource.All:
                        slbFloorSou.clear();
                        slbFloorDes.clear();
                        slbFloorSou.DataSource = _tmpData.Copy();
                        slbFloorDes.DataSource = _tmpData.Copy();
                        break;
                    case CategoryLoadSource.Sou:
                        slbFloorSou.clear();
                        slbFloorSou.DataSource = _tmpData.Copy();
                        break;
                    case CategoryLoadSource.Des:
                        slbFloorDes.clear();
                        slbFloorDes.DataSource = _tmpData.Copy();
                        break;
                    default:
                        slbFloorSou.clear();
                        slbFloorDes.clear();
                        slbFloorSou.DataSource = _tmpData.Copy();
                        slbFloorDes.DataSource = _tmpData.Copy();
                        break;
                }
               
            }

        }
        private void Get_Zone(string group, CategoryLoadSource category)
        {
            
            _tmpData = _api_TaiSan.Get_DanhMuc(cls_sys_LoaiDanhMuc.khu_MaLoai, group);
            if (_tmpData != null)
            {
                if (_tmpData != null)
                {
                    switch (category)
                    {
                        case CategoryLoadSource.All:
                            slbZoneSou.clear();
                            slbZoneDes.clear();
                            slbZoneSou.DataSource = _tmpData.Copy();
                            slbZoneDes.DataSource = _tmpData.Copy();
                            break;
                        case CategoryLoadSource.Sou:
                            slbZoneSou.clear();
                            slbZoneSou.DataSource = _tmpData.Copy();
                            break;
                        case CategoryLoadSource.Des:
                            slbZoneDes.clear();
                            slbZoneDes.DataSource = _tmpData.Copy();
                            break;
                        default:
                            slbZoneSou.clear();
                            slbZoneDes.clear();
                            slbZoneSou.DataSource = _tmpData.Copy();
                            slbZoneDes.DataSource = _tmpData.Copy();
                            break;
                    }
                   
                }
            }

        }
        private void SearchSou()
        {
            _tmpData = _usingService.Get_TaiSanSuDung(slbZoneSou.txtMa.Text, slbFloorSou.txtMa.Text, slbFuncRomSou.txtMa.Text);
            gvgSou.DataSource = _tmpData;
        }
        private void SearchDes()
        {
            _tmpData = _usingService.Get_TaiSanSuDung(slbZoneDes.txtMa.Text, slbFloorDes.txtMa.Text, slbFuncRomDes.txtMa.Text);
            gvgDes.DataSource = _tmpData;
        }

        private void Clear()
        {
            slbLocationSou.clear();
            slbLocationDes.clear();

            slbZoneDes.clear();
            slbZoneDes.clear();

            slbFloorDes.clear();
            slbFloorSou.clear();

            slbFuncRomSou.clear();
            slbFuncRomDes.clear();

            txtGhiChu.Text = "";
            txtCode.Text = "";
        }
        #endregion

        //-------------------------------------------------------------

        #region Event Control

        #endregion

        //-------------------------------------------------------------
    }

    public enum CategoryLoadSource { All,Sou,Des}
}
