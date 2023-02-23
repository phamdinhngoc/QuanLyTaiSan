using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E00_API.Contract;
using E00_API.Contract.TaiSan;
using E00_Helpers.Helpers;
using E00_Model;
using E00_SafeCacheDataService.Base;

namespace QuanLyTaiSan.ViewEntity
{
    public class TaiSanSuDungViewEntity : ViewEntityBase<TaiSanSuDungLLInfo>
    {
        //------------------------------------------------------------- 

        #region Member 

        #endregion

        //-------------------------------------------------------------  

        #region Constructor 

        public TaiSanSuDungViewEntity() { }

        public TaiSanSuDungViewEntity(TaiSanSuDungViewEntity entity) { }

        public TaiSanSuDungViewEntity(TaiSanSuDungLLInfo info)
        {
            Set(info);
        }

        public override void Set(TaiSanSuDungLLInfo info)
        {
            Copy(info);
        }

        public override void Copy(TaiSanSuDungLLInfo info)
        {
            KeyIDValue = Helper.ConvertSToDec(info.MA);
            _info.Copy(info);
            OnPropertyChanged(@"MA");
            OnPropertyChanged(@"MAVACH");
            OnPropertyChanged(@"MAKP");
            OnPropertyChanged(@"MANGUOINHAN");
            OnPropertyChanged(@"MATAISAN");
            OnPropertyChanged(@"TENTAISAN");
            OnPropertyChanged(@"GHICHU");
            OnPropertyChanged(@"MAKPQUANLY");
            OnPropertyChanged(@"USERID");
            OnPropertyChanged(@"TENKPQUANLY");
            OnPropertyChanged(@"MAKPSUDUNG");
            OnPropertyChanged(@"TENKPSUDUNG");
            OnPropertyChanged(@"MATANG");
            OnPropertyChanged(@"TENTANG");
            OnPropertyChanged(@"MANGUOISUDUNG");
            OnPropertyChanged(@"TENNGUOISUDUNG");
            OnPropertyChanged(@"CreateBy");
        }

        #endregion

        //-------------------------------------------------------------  

        #region Propertise 

        //public override object KeyIDValue { get { return _info.MA; } set { _info.MA = ""+value; } }
        public Decimal ID { get { return decimal.Parse(_info.MA); } set { _info.MA = ""+value; } }
        public Decimal MA { get { return decimal.Parse(_info.MA); } set { _info.MA = ""+value; } }
        public string MAVACH { get { return _info.MAVACH; } set { _info.MAVACH = value; } }
        public string MAKP { get { return _info.MAKP ?? (_info.MAKP = ""); } set { _info.MAKP = value; } }
        public string MANGUOINHAN { get { return _info.MANGUOINHAN; } set { _info.MANGUOINHAN = value; } }
        public string MATAISAN { get { return _info.MATAISAN; } set { _info.MATAISAN = value; } }
        public string TENTAISAN { get { return _info.TENTAISAN; } set { _info.TENTAISAN = value;} }
        public string GHICHU { get { return _info.GHICHU; } set { _info.GHICHU = value; } }
        public string MAKPQUANLY { get { return _info.MAKPQUANLY; } set { _info.MAKPQUANLY = value; } }
        public string TENKPQUANLY { get { return _info.TENKPQUANLY; } set { _info.TENKPQUANLY = value; } }
        public string MAKPSUDUNG { get { return _info.MAKPSUDUNG; } set { _info.MAKPSUDUNG = value; } }
        public string TENKPSUDUNG { get { return _info.TENKPSUDUNG; } set { _info.TENKPSUDUNG = value; } }
        public string MATANG { get { return _info.MATANG; } set { _info.MATANG = value; } }
        public string TENTANG { get { return _info.TENTANG; } set { _info.TENTANG = value; } }
        public string TENNGUOISUDUNG { get { return _info.TENNGUOISUDUNG; } set { _info.TENNGUOISUDUNG = value; } }
        public string MANGUOISUDUNG { get { return _info.MANGUOISUDUNG; } set { _info.MANGUOISUDUNG = value; } }

        #endregion

        //-------------------------------------------------------------  

        #region Method 
        public string CreateBy { get { return "Tạo bởi: " + _info.USERID + " lúc: " + (_info.NGAYTAO == null ? "" : _info.NGAYTAO.Value.ToString("dd/MM/yy hh:mm")); } }
        #endregion

        //-------------------------------------------------------------  

    }
}
