using System;

namespace QuanLyBanHang.Class
{
    public class NhanVien
    {
         public int ID { get; set; }
        public string MaNV { get; set; }
        public string TenNhanVien { get; set; }
        public DateTime NgaySinh { get; set; }
        public string NoiSinhText { get; set; }
        public string PhongBanText { get; set; }
        public string ChucVuText { get; set; }
        public DateTime NgayNhanViec { get; set; }
        public string Email { get; set; }
        public string DienThoai { get; set; }
    }
}
