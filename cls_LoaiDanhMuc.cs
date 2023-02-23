using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuanLyTaiSan
{
    public partial class cls_sys_LoaiDanhMuc
    {
        public static string khu_MaNhomLoai = "";
        public static string khu_MaLoai = "Khu";
        public static string khu_TenLoai = "Danh Mục Khu";
        public static string khu_TenCotMa = "Mã Khu";
        public static string khu_TenCotTen = "Tên Khu";

        public static string tang_MaNhomLoai = khu_MaLoai;
        public static string tang_MaLoai = "Tang";
        public static string tang_TenLoai = "Danh Mục Tầng";
        public static string tang_TenCotMa = "Mã Tầng";
        public static string tang_TenCotTen = "Tên Tầng";
        public static string tang_TenCotMaLoai = khu_TenCotMa;
        public static string tang_TenCotTenLoai = khu_TenCotTen;

        public static string phong_MaNhomLoai = tang_MaLoai;
        public static string phong_MaLoai = "PhongCongNang";
        public static string phong_TenLoai = "Danh Mục Phòng Công Năng";
        public static string phong_TenCotMa = "Mã Phòng Công Năng";
        public static string phong_TenCotTen = "Tên Phòng Công Năng";
        public static string phong_TenCotMaLoai = tang_TenCotMa;
        public static string phong_TenCotTenLoai = tang_TenCotTen;

        public static string loaiTaiSan_MaNhomLoai = "";
        public static string loaiTaiSan_MaLoai = "LoaiTaiSan";
        public static string loaiTaiSan_TenLoai = "Danh Mục Loại Tài Sản";
        public static string loaiTaiSan_TenCotMa = "Mã Loại";
        public static string loaiTaiSan_TenCotTen = "Loại Tài Sản";

        public static string nhaCungCap_MaNhomLoai = "";
        public static string nhaCungCap_MaLoai = "NhaCungCap";
        public static string nhaCungCap_TenLoai = "Danh Mục Nhà Cung Cấp";
        public static string nhaCungCap_TenCotMa = "Mã Nhà Cung Cấp";
        public static string nhaCungCap_TenCotTen = "Tên Nhà Cung Cấp";

        public static string trangThaiTaiSan_MaNhomLoai = "";
        public static string trangThaiTaiSan_MaLoai = "TrangThaiTaiSan";
        public static string trangThaiTaiSan_TenLoai = "Danh Mục Trạng Thái Tài Sản";
        public static string trangThaiTaiSan_TenCotMa = "Mã Trạng Thái Tài Sản";
        public static string trangThaiTaiSan_TenCotTen = "Tên Trạng Thái Tài Sản";

        public static string banGiaoTaiSan_MaLoai = "BanGiaoTaiSan";

        public static string kiemKeTaiSan_MaLoai = "KiemKeTaiSan";

        public static string maBanGiamDoc = "MaBanGiamDoc";
        public static string tenBanGiamDoc = "TenBanGiamDoc";
        public static string chucVuBanGiamDoc = "ChucVuBanGiamDoc";

        public static string maBenGiao1 = "MaBenGiao1";
        public static string tenBenGiao1 = "TenBenGiao1";
        public static string chucVuBenGiao1 = "ChucVuBenGiao1";

        public static string maBenGiao2 = "MaBenGiao2";
        public static string tenBenGiao2 = "TenBenGiao2";
        public static string chucVuBenGiao2 = "ChucVuBenGiao2";

        public static string maBenNhan1 = "MaBenNhan1";
        public static string tenBenNhan1 = "TenBenNhan1";
        public static string chucVuBenNhan1 = "ChucVuBenNhan1";

        public static string maBenNhan2 = "MaBenNhan2";
        public static string tenBenNhan2 = "TenBenNhan2";
        public static string chucVuBenNhan2 = "ChucVuBenNhan2";

        public static string diaDiemBanGiao = "DiaDiemBanGiao";
        public static string diaDiemKiemKe = "DiaDiemKiemKe";

        public static string nhanVienKiemKe = "NhanVienKiemKe";

        public static string trangThaiGiuong = "TrangThaiGiuong";
        public static string baoDongKhoaPhong = "BaoDongKhoaPhong";

        public static string nhomKiemKeDuoc = "NhomKiemKeDuoc";

        //public static string trangThai_ChuaBanGiao = "0";
        //public static string trangThai_DaBanGiao = "0";
        //public static string trangThai_ThuHoi = "0";
        //public static string trangThai_ThanhLy = "0";

        //public static string phong_MaLoai = "";
        //public static string _kyHieuTenPhongCongNang = "";

        //public static string _kyHieuMaLoaiTaiSan = "LoaiTaiSan";
        //public static string _kyHieuTenLoaiTaiSan = "Loại Tài Sản";

        //public static string _kyHieuMaNhaCungCap = "NhaCungCap";
        //public static string _kyHieuTenNhaCungCap = "Nhà Cung Cấp";

        private static Dictionary<string, string> _dicTinhChat ;
        public static Dictionary<string, string> GetTinhChat()
        {
            if(_dicTinhChat == null)
            {
                _dicTinhChat = new Dictionary<string, string>();
                _dicTinhChat.Add(@"VT", @"Vật tư, hàng hóa");
                _dicTinhChat.Add(@"LR", @"Lắp ráp, bộ");
                _dicTinhChat.Add(@"DV", @"Dịch vụ");
                _dicTinhChat.Add(@"TP", @"Thành phần, linh kiện");
                _dicTinhChat.Add(@"KH", @"Khác");
            }

            return _dicTinhChat;
        }
        private static Dictionary<string, string> _dicLoaiNhap;
        public static Dictionary<string, string> GetLoaiNhap()
        {
            if (_dicLoaiNhap == null)
            {
                _dicLoaiNhap = new Dictionary<string, string>();
                _dicLoaiNhap.Add(@"NRO", @"Nhập mới");
                _dicLoaiNhap.Add(@"NCK", @"Nhập chuyển kho");
                _dicLoaiNhap.Add(@"NTV", @"Nhập  trả về");
                _dicLoaiNhap.Add(@"NCD", @"Nhập cân đối tồn kho");
                _dicLoaiNhap.Add(@"NTH", @"Thu hồi tài sản");
                _dicLoaiNhap.Add(@"NKH", @"Nhập khác");
            }

            return _dicLoaiNhap;
        }

        private static Dictionary<string, string> _dicLoaiXuat;
        public static Dictionary<string, string> GetLoaiXuat()
        {
            if (_dicLoaiXuat == null)
            {
                _dicLoaiXuat = new Dictionary<string, string>();
                _dicLoaiXuat.Add(@"XHU", @"Xuất hủy");
                _dicLoaiXuat.Add(@"XCK", @"Xuất chuyển kho");
                _dicLoaiXuat.Add(@"XTV", @"Xuất trả về");
                _dicLoaiXuat.Add(@"XCD", @"Xuất cân đối tồn kho");
                _dicLoaiXuat.Add(@"XKH", @"Xuất khác");
            }

            return _dicLoaiXuat;
        }

        private static Dictionary<string, string> _dicLoaiThuHoi;
        public static Dictionary<string, string> GetLoaiThuHoi()
        {
            if (_dicLoaiThuHoi == null)
            {
                _dicLoaiThuHoi = new Dictionary<string, string>();
                _dicLoaiThuHoi.Add(@"THC", @"Thu hồi chuyển giao");
                _dicLoaiThuHoi.Add(@"THS", @"Thu hồi sửa chữa");
                _dicLoaiThuHoi.Add(@"THH", @"Thu hồi hủy");
                _dicLoaiThuHoi.Add(@"THK", @"khác");
            }

            return _dicLoaiThuHoi;
        }

        public const string LocationDefault = @"A-00-00-0-1";
        public const string StatusDefault = @"TOT";
        public const string SCategoryMoveInventory = @"MoveInventory";
        public const string SCategoryMoveUsing = @"MoveUsing";

    }
}
