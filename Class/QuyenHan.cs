namespace QuanLyBanHang.Class
{
    public class QuyenHan
    {
        public int Id { get; set; }
        public decimal STT { get; set; }
        public int Menu { get; set; }
        public string TenMenu { get; set; }
        public string TenNhomQuyen { get; set; }
        public bool Them { get; set; }
        public bool Luu { get; set; }
        public bool Xoa { get; set; }
        public bool Sua { get; set; }
        public bool Excel { get; set; }
        public bool InAn { get; set; }
    }
}
