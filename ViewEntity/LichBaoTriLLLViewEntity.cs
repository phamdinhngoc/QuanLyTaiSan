using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E00_API.Contract;
using E00_API.Contract.Maintenance;
using E00_Helpers.Format;

namespace QuanLyTaiSan.ViewEntity
{
    public partial class LichBaoTriLLLViewEntity : E00_SafeCacheDataService.Base.ViewEntityBase<LichBaoTriLLInfo>
    {
        //------------------------------------------------------------- 

        #region Member 

        #endregion

        //-------------------------------------------------------------  

        #region Constructor 

        public LichBaoTriLLLViewEntity() { }

        public LichBaoTriLLLViewEntity(LichBaoTriLLLViewEntity entity) { }

        public LichBaoTriLLLViewEntity(LichBaoTriLLInfo info)
        {
            Set(info);
        }

        public override void Set(LichBaoTriLLInfo info)
        {
            Copy(info);
        }

        public override void Copy(LichBaoTriLLInfo info)
        {
            KeyIDValue = info.LichBTID;
            _info.Copy(info);
            Caculator();
            OnPropertyChanged(@"LICHBTID");
            OnPropertyChanged(@"TenMoTa");
            OnPropertyChanged(@"GhiChu");
            OnPropertyChanged(@"TrangThai");
            OnPropertyChanged(@"SoPhieu");
            OnPropertyChanged(@"Loai");
            OnPropertyChanged(@"Ngay");
            OnPropertyChanged(@"SCreateTime");
            OnPropertyChanged(@"USERID");
            OnPropertyChanged(@"LoaiBT");
            OnPropertyChanged(@"LapLai");
            
        }

        #endregion

        //-------------------------------------------------------------  

        #region Propertise 

        public override object KeyIDValue { get { return _info.LichBTID; } set { _info.LichBTID = (decimal)value; } }
        public decimal ID { get { return _info.LichBTID; } set { _info.LichBTID = value; } }
        public decimal LICHBTID { get { return _info.LichBTID; } set { _info.LichBTID = value; } }
        public decimal LapLai { get { return _info.LapLai; } set { _info.LapLai = value; } }
        public string TenMoTa { get { return _info.TenMoTa; } set { _info.TenMoTa = value; OnPropertyChanged(@"TenMoTa"); } }
        public string GhiChu { get { return _info.GhiChu??(_info.GhiChu = ""); } set { _info.GhiChu = value; OnPropertyChanged(@"GhiChu"); } }
        public decimal TrangThai { get { return _info.TrangThai; } set { _info.TrangThai = value; OnPropertyChanged(@"TrangThai"); } }
        public string Loai { get { return _info.Loai; } set { _info.Loai = value; OnPropertyChanged(@"Loai"); } }
        public string LoaiBT { get { return _info.LoaiBT; } set { _info.LoaiBT = value; OnPropertyChanged(@"LoaiBT"); } }
        public string SoPhieu { get { return _info.SoPhieu; } set { _info.SoPhieu = value; OnPropertyChanged(@"SoPhieu"); } }
        public DateTime Ngay { get { return _info.Ngay; } set { _info.Ngay = value; OnPropertyChanged(@"Ngay"); } }
        public string USERID { get { return ""+_info.USERID; }}
        public bool IsAccept
        {
            get { return _info.TrangThai > 0; }
            set
            {
                switch ((int)_info.TrangThai)
                {
                    case 0:
                        if (value)
                        {
                            _info.TrangThai = 1;
                        }
                        break;
                    case 1:
                        if (!value)
                        {
                            _info.TrangThai = 0;
                        }
                        break;
                    case 2:
                       
                        break;
                }
               
                OnPropertyChanged(@"IsConfirm");
            }
        }
        public bool IsConfirm
        {
            get { return _info.TrangThai > 1; }
            set
            {
                switch ((int)_info.TrangThai)
                {
                    case 0:
                        if (value)
                        {
                            _info.TrangThai = 2;
                        }
                        break;
                    case 1:
                        if (value)
                        {
                            _info.TrangThai = 2;
                        }
                        break;
                    case 2:
                        if (!value)
                        {
                            _info.TrangThai = 1;
                        }
                        break;
                }
                OnPropertyChanged(@"IsAccept");
              
            }
        }
        #endregion

        //-------------------------------------------------------------  

        #region Method 
        public string SCreateTime { get { return _info.NgayTao == null ? "" : _info.NgayTao.Value.ToString(Formats.FDateDMYHMHM); } }
        public void Caculator()
        {
            switch ((int)_info.TrangThai)
            {
                case 0:
                    IsAccept = false;
                    IsConfirm = false;
                    break;
                case 1:
                    IsAccept = true;
                    IsConfirm = false;
                    break;
                case 2:
                    IsAccept = true;
                    IsConfirm = true;
                    break;
            }
            OnPropertyChanged(@"IsAccept");
            OnPropertyChanged(@"IsConfirm");
        }
        #endregion

        //-------------------------------------------------------------  

    }
}
