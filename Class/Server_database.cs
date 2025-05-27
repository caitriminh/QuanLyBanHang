using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;//khai báo thư viện
using System.Data.SqlClient;//khai báo thư viện
using System.Threading.Tasks;
using DevExpress.XtraGrid.Views.Grid;

namespace VisualStudio2019.Class
{
    class Server_database
    {
        //public SqlConnection scon = new SqlConnection(@"data source =.\SQLEXPRESS; initial catalog =quan_ly_ban_hang_sql2019; integrated security = true;");
        public SqlConnection scon = new SqlConnection(ExecSQL.CONNECTION_STRINGS);
        
        public void Myconnect()
        {
            scon.Open();
        }
        public void Myclose()
        {
            scon.Close();
        }
        //Thao tác cho Bảng đăng nhập
        public DataTable Tabledangnhap()
        {
            Myconnect();
            string sql = "select *from DangNhap";
            SqlDataAdapter sda = new SqlDataAdapter(sql, scon);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            Myclose();
            return dt;
        }
        //ráng dữ liệu cho data gridview
        public void ConfigureGridView(GridView gridView)
        {
            gridView.Columns[0].Caption = "Tên đăng nhập";
            gridView.Columns[1].Caption = "Mật khẩu";
            gridView.Columns[2].Caption = "Email";
        }
        //Insert table DangNhap
        public DataTable Insert_DangNhap(string TenDangNhap, string MatKhau, string Email)
        {
            // Cập nhật dữ liệu vào cơ sở dữ liệu
            Myconnect();
            // Viết các câu truy vấn SQL để insert, update hoặc delete dữ liệu ở đây
            string sql = "INSERT INTO DangNhap (TenDangNhap, MatKhau, Email) VALUES (@TenDangNhap, @MatKhau, @Email)";
            SqlCommand cmd = new SqlCommand(sql, scon);
            cmd.Parameters.AddWithValue("@TenDangNhap", TenDangNhap);
            cmd.Parameters.AddWithValue("@MatKhau", MatKhau);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.ExecuteNonQuery();
            // Truy vấn lại dữ liệu từ cơ sở dữ liệu để lấy DataTable
            sql = "SELECT * FROM DangNhap";
            SqlDataAdapter sda = new SqlDataAdapter(sql, scon);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            Myclose();
            return dt;
        }
        public DataTable Update_DangNhap(string tenDangNhap, string matKhau, string email)
        {
            // Open the database connection
            Myconnect();
            // Create the SQL command using the stored procedure
            SqlCommand cmd = new SqlCommand("Update_DangNhap", scon)
            {
                CommandType = CommandType.StoredProcedure
            };
            // Add the input parameters
            cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
            cmd.Parameters.AddWithValue("@MatKhau", matKhau);
            cmd.Parameters.AddWithValue("@Email", email);
            // Execute the stored procedure
            cmd.ExecuteNonQuery();
            // Retrieve the updated data from the database
            string sql = "SELECT * FROM DangNhap";
            SqlDataAdapter sda = new SqlDataAdapter(sql, scon);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            // Close the database connection
            Myclose();
            return dt;
        }
        //Update table DangNhap
        //public DataTable Update_DangNhap(string TenDangNhap, string MatKhau, string Email)
        //{
        //    // Cập nhật dữ liệu vào cơ sở dữ liệu
        //    Myconnect();
        //    // Viết các câu truy vấn SQL để update dữ liệu
        //    string sql = "UPDATE DangNhap SET MatKhau = @MatKhau, Email = @Email WHERE TenDangNhap = @TenDangNhap";
        //    SqlCommand cmd = new SqlCommand(sql, scon);
        //    cmd.Parameters.AddWithValue("@TenDangNhap", TenDangNhap);
        //    cmd.Parameters.AddWithValue("@MatKhau", MatKhau);
        //    cmd.Parameters.AddWithValue("@Email", Email);
        //    cmd.ExecuteNonQuery();
        //    // Truy vấn lại dữ liệu từ cơ sở dữ liệu để lấy DataTable
        //    sql = "SELECT * FROM DangNhap";
        //    SqlDataAdapter sda = new SqlDataAdapter(sql, scon);
        //    DataTable dt = new DataTable();
        //    sda.Fill(dt);
        //    Myclose();
        //    return dt;
        //}
        ////Delete table DangNhap
        public DataTable Delete_DangNhap(string tenDangNhap)
        {
            // Connect to the database
            Myconnect();
            // Write the SQL query to delete the data
            string sql = "DELETE FROM DangNhap WHERE TenDangNhap = @TenDangNhap";
            SqlCommand cmd = new SqlCommand(sql, scon);
            cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
            cmd.ExecuteNonQuery();
            // Retrieve the updated data from the database
            sql = "SELECT * FROM DangNhap";
            SqlDataAdapter sda = new SqlDataAdapter(sql, scon);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            // Close the database connection
            Myclose();
            return dt;
        }
        //Thao tác cho Bảng khách hàng
        public DataTable DatabaseKhachhang()
        {
            Myconnect();
            string sql = "select KhachHang.ID,KhachHang.TenKhachHang,KhachHang.DiaChi,KhachHang.DienThoai1,KhachHang.DienThoai2,KhachHang.EmailKH,KhachHang.MaSoThue,KhachHang.ChietKhau,Nhomkhachhang.TenNhom,KhachHang.Ghichu from KhachHang,Nhomkhachhang";
            SqlDataAdapter sda = new SqlDataAdapter(sql, scon);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            Myclose();
            return dt;
        }
        //Thao tác cho Bảng NHOM
        public DataTable Databasenhomkhachhang()
        {
            Myconnect();
            string sql = "select *from Nhomkhachhang";
            SqlDataAdapter sda = new SqlDataAdapter(sql, scon);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            Myclose();
            return dt;
        }
        //Thao tác cho bảng sản phẩm
        public DataTable Databasesanpham()
        {
            Myconnect();
            string sql = "select SanPham.ID, SanPham.TenSanPham, SanPham.DonViTinh, SanPham.GiaNhap, SanPham.GiaBan, SanPham.SoLuong, LoaiSanPham.TenLoai, SanPham.Status, SanPham.GhiChu from SanPham, LoaiSanPham ORDER BY SanPham.ID ASC";
            SqlDataAdapter sda = new SqlDataAdapter(sql, scon);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            Myclose();
            return dt;
        }
        //Thao tác cho bảng loại sản phẩm
        public DataTable Databaseloaisanpham()
        {
            Myconnect();
            string sql = "select *from LoaiSanPham";
            SqlDataAdapter sda = new SqlDataAdapter(sql, scon);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            Myclose();
            return dt;
        }
        //Phần cho nhập hàng.
        public DataTable PhieuNhapMaxID()
        {
            Myconnect();
            SqlCommand sqlCommand = new SqlCommand("sp_PhieuNhapSelectID", scon);
            SqlCommand cmd = sqlCommand;
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Myclose();
            return dt;
        }
        public string Autoidhdnhap()
        {
            _ = new DataTable();
            DataTable dt = PhieuNhapMaxID();
            string _x;
            if (dt.Rows.Count == 0)
                _x = "PN000";
            else
                _x = dt.Rows[0]["MaPhieuNhap"].ToString();
            string kq = "";
            string left = _x.Substring(0, 2).ToString();
            string snew = _x.Substring(2, 3).ToString();
            int i = int.Parse(snew);
            i++;
            if (i < 10)
                kq = "00" + i.ToString();
            if (i >= 10 && i < 100)
                kq = "0" + i.ToString();
            if (i >= 100)
                kq = i.ToString();
            return left + kq;
        }
        public DataTable DataNhaCungCap()
        {
            Myconnect();
            SqlCommand sqlCommand = new SqlCommand("sp_NhaCungCapSelect", scon);
            SqlCommand cmd = sqlCommand;
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Myclose();
            return dt;
        }
        public DataTable DataNhaCungCapById(int mancc)
        {
            Myconnect();
            SqlCommand sqlCommand = new SqlCommand("sp_NhaCungCapByID", scon);
            SqlCommand cmd = sqlCommand;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", mancc);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Myclose();
            return dt;
        }
        public DataTable ThemPhieuNhap(string mahd, DateTime ngaylap, int manv, int makh)
        {
            Myconnect();
            SqlCommand sqlCommand = new SqlCommand("sp_PhieuNhapInsert", scon);
            SqlCommand cmd = sqlCommand;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mapn", mahd);
            cmd.Parameters.AddWithValue("@ngaylap", ngaylap);
            cmd.Parameters.AddWithValue("@manv", manv);
            cmd.Parameters.AddWithValue("@mancc", makh);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Myclose();
            return dt;
        }
        public DataTable ThemChiTietPhieuNhap(string mahd, int masp, int soluong)
        {
            Myconnect();
            SqlCommand sqlCommand = new SqlCommand("sp_ChiTietPhieuNhapInsert", scon);
            SqlCommand cmd = sqlCommand;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mapn", mahd);
            cmd.Parameters.AddWithValue("@masp", masp);
            cmd.Parameters.AddWithValue("@soluong", soluong);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Myclose();
            return dt;
        }
        //phần cho hóa đơn xuất hàng
        public DataTable HoaDonMaxID()
        {
            Myconnect();
            SqlCommand sqlCommand = new SqlCommand("sp_HoaDonSelectID", scon);
            SqlCommand cmd = sqlCommand;
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Myclose();
            return dt;
        }
        //mã tự động tăng phiếu nhập hàng.
        public string Autoidphieunhap()
        {
            _ = new DataTable();
            DataTable dt = PhieuNhapMaxID();
            string _x;
            if (dt.Rows.Count == 0)
                _x = "PN000";
            else
                _x = dt.Rows[0]["MaPhieuNhap"].ToString();
            string kq = "";
            string left = _x.Substring(0, 2).ToString();
            string snew = _x.Substring(2, 3).ToString();
            int i = int.Parse(snew);
            i++;
            if (i < 10)
                kq = "00" + i.ToString();
            if (i >= 10 && i < 100)
                kq = "0" + i.ToString();
            if (i >= 100)
                kq = i.ToString();
            return left + kq;
        }
        //mã tự động tăng phiếu xuất hàng.
        public string Autoidhd()
        {
            _ = new DataTable();
            DataTable dt = HoaDonMaxID();
            string _x;
            if (dt.Rows.Count == 0)
                _x = "HD000";
            else
                _x = dt.Rows[0]["MaHoaDon"].ToString();
            string kq = "";
            string left = _x.Substring(0, 2).ToString();
            string snew = _x.Substring(2, 3).ToString();
            int i = int.Parse(snew);
            i++;
            if (i < 10)
                kq = "00" + i.ToString();
            if (i >= 10 && i < 100)
                kq = "0" + i.ToString();
            if (i >= 100)
                kq = i.ToString();
            return left + kq;
        }
        //load khách hàng
        public DataTable Datakhachhang()
        {
            Myconnect();
            SqlCommand sqlCommand = new SqlCommand("sp_KhachHangSelect", scon);
            SqlCommand cmd = sqlCommand;
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Myclose();
            return dt;
        }
        public DataTable KhachHangByID(int makh)
        {
            Myconnect();
            SqlCommand sqlCommand = new SqlCommand("sp_KhachHangByID", scon);
            SqlCommand cmd = sqlCommand;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", makh);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Myclose();
            return dt;
        }
        public DataTable SanPhamByID(int mahang)
        {
            SqlCommand sqlCommand = new SqlCommand("sp_SanPhamByID", scon);
            SqlCommand cmd = sqlCommand;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", mahang);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public DataTable DataNhanVienGet()
        {
            Myconnect();
            SqlCommand sqlCommand = new SqlCommand("[sp_NhanVienSelect]", scon);
            SqlCommand cmd = sqlCommand;
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Myclose();
            return dt;
        }
        public DataTable DataSanPhamGet()
        {
            Myconnect();
            SqlCommand sqlCommand = new SqlCommand("sp_SanPhamSelect", scon);
            SqlCommand cmd = sqlCommand;
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Myclose();
            return dt;
        }
        //Thêm hóa đơn
        public DataTable ThemHoaDon(string mahd, DateTime ngaylap, int manv, int makh)
        {
            Myconnect();
            SqlCommand sqlCommand = new SqlCommand("sp_HoaDonInsert", scon);
            SqlCommand cmd = sqlCommand;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mahd", mahd);
            cmd.Parameters.AddWithValue("@ngaylap", ngaylap);
            cmd.Parameters.AddWithValue("@manv", manv);
            cmd.Parameters.AddWithValue("@makh", makh);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Myclose();
            return dt;
        }
        //Thêm chi tiết hóa đơn
        public DataTable ThemChiTietHoaDon(string mahd, int masp, int soluong)
        {
            Myconnect();
            SqlCommand sqlCommand = new SqlCommand("sp_ChiTietHoaDonInsert", scon);
            SqlCommand cmd = sqlCommand;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mahd", mahd);
            cmd.Parameters.AddWithValue("@masp", masp);
            cmd.Parameters.AddWithValue("@soluong", soluong);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Myclose();
            return dt;
        }
        //Update hóa đơn.sp_HoaDonUpdate
        public DataTable CapnhatHoaDon(string MaHoaDon, DateTime NgayLap, int MaNhanVien, int MaKhachHang)
        {
            Myconnect();
            SqlCommand sqlCommand = new SqlCommand("sp_HoaDonUpdate", scon);
            SqlCommand cmd = sqlCommand;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaHoaDon", MaHoaDon);
            cmd.Parameters.AddWithValue("@NgayLap", NgayLap);
            cmd.Parameters.AddWithValue("@MaNhanVien", MaNhanVien);
            cmd.Parameters.AddWithValue("@MaKhachHang", MaKhachHang);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Myclose();
            return dt;
        }
        public DataTable CapnhatChiTietHoaDon(string MaHoaDon, int MaSanPham, int SoLuong)
        {
            Myconnect();
            SqlCommand sqlCommand = new SqlCommand("sp_ChiTietHoaDonUpdate", scon);
            SqlCommand cmd = sqlCommand;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaHoaDon", MaHoaDon);
            cmd.Parameters.AddWithValue("@MaSanPham", MaSanPham);
            cmd.Parameters.AddWithValue("@SoLuong", SoLuong);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Myclose();
            return dt;
        }
        //thống kê bánh hàng
        //Cho hiển thị thông tin lên form xuất hàng hóa ở datagridview
        public DataTable Gethienthihoadonbanhang()
        {
            string sql = "Select Khachhang.TenKhachhang,ChiTietHoaDon.MaHoaDon,HoaDon.NgayLap,ChiTietHoaDon.MaSanPham,SanPham.TenSanPham,ChiTietHoaDon.SoLuong ,SanPham.GiaBan,(ChiTietHoaDon.SoLuong * SanPham.GiaBan) AS TongTien from SanPham,ChiTietHoaDon,HoaDon,Khachhang where HoaDon.MaHoaDon=ChiTietHoaDon.MaHoaDon ORDER BY MaHoaDon";
            SqlDataAdapter sda = new SqlDataAdapter(sql, scon);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            scon.Close();
            return dt;
        }
        public DataTable Gethienthihoadonbanhangtungaydenngay(DateTime ngay1, DateTime ngay2)
        {
            string sql = "	SELECT ChiTietHoaDon.MaHoaDon,Khachhang.TenKhachhang,HoaDon.NgayLap,ChiTietHoaDon.MaSanPham,SanPham.TenSanPham,ChiTietHoaDon.SoLuong,SanPham.GiaBan,FORMAT((ChiTietHoaDon.SoLuong * SanPham.GiaBan), 'C', 'vi-VN') AS TongTien FROM ChiTietHoaDon INNER JOIN HoaDon ON ChiTietHoaDon.MaHoaDon = HoaDon.MaHoaDon INNER JOIN Khachhang ON HoaDon.MaKhachHang = Khachhang.ID INNER JOIN SanPham ON ChiTietHoaDon.MaSanPham = SanPham.ID WHERE HoaDon.NgayLap BETWEEN @ngay1 AND @ngay2 ORDER BY MaHoaDon";
            SqlCommand cmd = new SqlCommand(sql, scon);
            cmd.Parameters.AddWithValue("@ngay1", ngay1);
            cmd.Parameters.AddWithValue("@ngay2", ngay2.AddDays(1));
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            scon.Close();
            return dt;
            ////string sql = " SELECT Khachhang.TenKhachhang,ChiTietHoaDon.MaHoaDon,ChiTietHoaDon.MaSanPham,SanPham.TenSanPham,ChiTietHoaDon.SoLuong,SanPham.GiaBan,(ChiTietHoaDon.SoLuong * SanPham.GiaBan) AS TongTien FROM ChiTietHoaDon INNER JOIN HoaDon ON ChiTietHoaDon.MaHoaDon = HoaDon.MaHoaDon INNER JOIN Khachhang ON HoaDon.MaKhachHang = Khachhang.ID INNER JOIN SanPham ON ChiTietHoaDon.MaSanPham = SanPham.ID  ORDER BY MaHoaDon;
            //string sql = "SELECT Khachhang.TenKhachhang,ChiTietHoaDon.MaHoaDon,ChiTietHoaDon.MaSanPham,SanPham.TenSanPham,ChiTietHoaDon.SoLuong,SanPham.GiaBan,(ChiTietHoaDon.SoLuong * SanPham.GiaBan) AS TongTien FROM ChiTietHoaDon INNER JOIN HoaDon ON ChiTietHoaDon.MaHoaDon = HoaDon.MaHoaDon INNER JOIN Khachhang ON HoaDon.MaKhachHang = Khachhang.ID INNER JOIN SanPham ON ChiTietHoaDon.MaSanPham = SanPham.ID and HoaDon.NgayLap>='" + ngay1 + "' and HoaDon.NgayLap<'" + ngay2 + "' ORDER BY MaHoaDon";
            //SqlDataAdapter sda = new SqlDataAdapter(sql, scon);
            //DataTable dt = new DataTable();
            //sda.Fill(dt);
            //scon.Close();
            //return dt;
        }
        // doanh thu theo ngày
        public DataTable Doanhthutheongaythang(DateTime ngay1, DateTime ngay2)
        {
            string sql = "	SELECT ChiTietHoaDon.MaHoaDon,Khachhang.TenKhachhang,HoaDon.NgayLap,ChiTietHoaDon.MaSanPham,SanPham.TenSanPham,ChiTietHoaDon.SoLuong,SanPham.GiaBan,FORMAT((ChiTietHoaDon.SoLuong * SanPham.GiaBan), 'C', 'vi-VN') AS TongTien FROM ChiTietHoaDon INNER JOIN HoaDon ON ChiTietHoaDon.MaHoaDon = HoaDon.MaHoaDon INNER JOIN Khachhang ON HoaDon.MaKhachHang = Khachhang.ID INNER JOIN SanPham ON ChiTietHoaDon.MaSanPham = SanPham.ID WHERE HoaDon.NgayLap BETWEEN @ngay1 AND @ngay2 ORDER BY MaHoaDon";
            SqlCommand cmd = new SqlCommand(sql, scon);
            cmd.Parameters.AddWithValue("@ngay1", ngay1);
            cmd.Parameters.AddWithValue("@ngay2", ngay2.AddDays(1));
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            scon.Close();
            return dt;
            ////string sql = " SELECT Khachhang.TenKhachhang,ChiTietHoaDon.MaHoaDon,ChiTietHoaDon.MaSanPham,SanPham.TenSanPham,ChiTietHoaDon.SoLuong,SanPham.GiaBan,(ChiTietHoaDon.SoLuong * SanPham.GiaBan) AS TongTien FROM ChiTietHoaDon INNER JOIN HoaDon ON ChiTietHoaDon.MaHoaDon = HoaDon.MaHoaDon INNER JOIN Khachhang ON HoaDon.MaKhachHang = Khachhang.ID INNER JOIN SanPham ON ChiTietHoaDon.MaSanPham = SanPham.ID  ORDER BY MaHoaDon;
            //string sql = "SELECT Khachhang.TenKhachhang,ChiTietHoaDon.MaHoaDon,ChiTietHoaDon.MaSanPham,SanPham.TenSanPham,ChiTietHoaDon.SoLuong,SanPham.GiaBan,(ChiTietHoaDon.SoLuong * SanPham.GiaBan) AS TongTien FROM ChiTietHoaDon INNER JOIN HoaDon ON ChiTietHoaDon.MaHoaDon = HoaDon.MaHoaDon INNER JOIN Khachhang ON HoaDon.MaKhachHang = Khachhang.ID INNER JOIN SanPham ON ChiTietHoaDon.MaSanPham = SanPham.ID and HoaDon.NgayLap>='" + ngay1 + "' and HoaDon.NgayLap<'" + ngay2 + "' ORDER BY MaHoaDon";
            //SqlDataAdapter sda = new SqlDataAdapter(sql, scon);
            //DataTable dt = new DataTable();
            //sda.Fill(dt);
            //scon.Close();
            //return dt;
        }
        //Tổng phiếu nhập theo ngày ,tháng năm
        public DataTable Gettongphieunhaphangtungaydenngay(DateTime ngay1, DateTime ngay2)
        {
            string sql = "SELECT ChiTietPhieuNhap.MaPhieuNhap,Nhanvien.TenNhanVien,PhieuNhap.NgayLap,ChiTietPhieuNhap.MaSanPham,SanPham.TenSanPham,ChiTietPhieuNhap.SoLuong,SanPham.GiaNhap,FORMAT((ChiTietPhieuNhap.SoLuong * SanPham.GiaNhap), 'C', 'vi-VN') AS TongTien FROM ChiTietPhieuNhap INNER JOIN PhieuNhap ON ChiTietPhieuNhap.MaPhieuNhap = PhieuNhap.MaPhieuNhap INNER JOIN Nhanvien ON PhieuNhap.MaNhanVien = Nhanvien.ID INNER JOIN SanPham ON ChiTietPhieuNhap.MaSanPham = SanPham.ID WHERE PhieuNhap.NgayLap BETWEEN @ngay1 AND @ngay2 ORDER BY MaPhieuNhap";
            SqlCommand cmd = new SqlCommand(sql, scon);
            cmd.Parameters.AddWithValue("@ngay1", ngay1);
            cmd.Parameters.AddWithValue("@ngay2", ngay2.AddDays(1));
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            scon.Close();
            return dt;
            ////string sql = " SELECT Khachhang.TenKhachhang,ChiTietHoaDon.MaHoaDon,ChiTietHoaDon.MaSanPham,SanPham.TenSanPham,ChiTietHoaDon.SoLuong,SanPham.GiaBan,(ChiTietHoaDon.SoLuong * SanPham.GiaBan) AS TongTien FROM ChiTietHoaDon INNER JOIN HoaDon ON ChiTietHoaDon.MaHoaDon = HoaDon.MaHoaDon INNER JOIN Khachhang ON HoaDon.MaKhachHang = Khachhang.ID INNER JOIN SanPham ON ChiTietHoaDon.MaSanPham = SanPham.ID  ORDER BY MaHoaDon;
            //string sql = "SELECT Khachhang.TenKhachhang,ChiTietHoaDon.MaHoaDon,ChiTietHoaDon.MaSanPham,SanPham.TenSanPham,ChiTietHoaDon.SoLuong,SanPham.GiaBan,(ChiTietHoaDon.SoLuong * SanPham.GiaBan) AS TongTien FROM ChiTietHoaDon INNER JOIN HoaDon ON ChiTietHoaDon.MaHoaDon = HoaDon.MaHoaDon INNER JOIN Khachhang ON HoaDon.MaKhachHang = Khachhang.ID INNER JOIN SanPham ON ChiTietHoaDon.MaSanPham = SanPham.ID and HoaDon.NgayLap>='" + ngay1 + "' and HoaDon.NgayLap<'" + ngay2 + "' ORDER BY MaHoaDon";
            //SqlDataAdapter sda = new SqlDataAdapter(sql, scon);
            //DataTable dt = new DataTable();
            //sda.Fill(dt);
            //scon.Close();
            //return dt;
        }
        //báo cao hóa đơn bán hàng theo nhân viên
        public DataTable LoadNhanVien()
        {
            SqlCommand sqlCommand = new SqlCommand("sp_NhanVienselectall", scon);
            SqlCommand cmd = sqlCommand;
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        //in hóa đơn bán hàng.
        public DataTable Printhoadon(string MaHoaDon)
        {
            if (MaHoaDon is null)
            {
                throw new ArgumentNullException(nameof(MaHoaDon));
            }

            Myconnect();
            SqlCommand sqlCommand = new SqlCommand("sp_InHoaDonByMaHD", scon);
            SqlCommand cmd = sqlCommand;
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Myclose();
            return dt;
        }
        public class Doctienthanhchu
        {
            private readonly string[] ChuSo = new string[10] { " không", " một", " hai", " ba", " bốn", " năm", " sáu", " bẩy", " tám", " chín" };
            private readonly string[] Tien = new string[6] { "", " nghìn", " triệu", " tỷ", " nghìn tỷ", " triệu tỷ" };
            // Hàm đọc số thành chữ
            public string DocTienBangChu(long SoTien, string strTail)
            {
                int lan, i;
                long so;
                string KetQua = "";
                int[] ViTri = new int[6];
                if (SoTien < 0) return "Số tiền âm !";
                if (SoTien == 0) return "Không đồng !";
                if (SoTien > 0)
                {
                    so = SoTien;
                }
                else
                {
                    so = -SoTien;
                }
                //Kiểm tra số quá lớn
                if (SoTien > 8999999999999999)
                {
                    return "";
                }
                ViTri[5] = (int)(so / 1000000000000000);
                so -= long.Parse(ViTri[5].ToString()) * 1000000000000000;
                ViTri[4] = (int)(so / 1000000000000);
                so -= long.Parse(ViTri[4].ToString()) * +1000000000000;
                ViTri[3] = (int)(so / 1000000000);
                so -= long.Parse(ViTri[3].ToString()) * 1000000000;
                ViTri[2] = (int)(so / 1000000);
                ViTri[1] = (int)((so % 1000000) / 1000);
                ViTri[0] = (int)(so % 1000);
                if (ViTri[5] > 0)
                {
                    lan = 5;
                }
                else if (ViTri[4] > 0)
                {
                    lan = 4;
                }
                else if (ViTri[3] > 0)
                {
                    lan = 3;
                }
                else if (ViTri[2] > 0)
                {
                    lan = 2;
                }
                else if (ViTri[1] > 0)
                {
                    lan = 1;
                }
                else
                {
                    lan = 0;
                }
                for (i = lan; i >= 0; i--)
                {
                    string tmp = DocSo3ChuSo(ViTri[i]);
                    KetQua += tmp;
                    if (ViTri[i] != 0) KetQua += Tien[i];
                    if ((i > 0) && (!string.IsNullOrEmpty(tmp))) KetQua += ",";//&& (!string.IsNullOrEmpty(tmp))
                }
                if (KetQua.Substring(KetQua.Length - 1, 1) == ",") KetQua = KetQua.Substring(0, KetQua.Length - 1);
                KetQua = KetQua.Trim() + strTail;
                return KetQua.Substring(0, 1).ToUpper() + KetQua.Substring(1);
            }
            // Hàm đọc số có 3 chữ số
            private string DocSo3ChuSo(int baso)
            {
                int tram, chuc, donvi;
                string KetQua = "";
                tram = (int)(baso / 100);
                chuc = (int)((baso % 100) / 10);
                donvi = baso % 10;
                if ((tram == 0) && (chuc == 0) && (donvi == 0)) return "";
                if (tram != 0)
                {
                    KetQua += ChuSo[tram] + " trăm";
                    if ((chuc == 0) && (donvi != 0)) KetQua += " linh";
                }
                if ((chuc != 0) && (chuc != 1))
                {
                    KetQua += ChuSo[chuc] + " mươi";
                    if ((chuc == 0) && (donvi != 0)) KetQua += " linh";
                }
                if (chuc == 1) KetQua += " mười";
                switch (donvi)
                {
                    case 1:
                        if ((chuc != 0) && (chuc != 1))
                        {
                            KetQua += " mốt";
                        }
                        else
                        {
                            KetQua += ChuSo[donvi];
                        }
                        break;
                    case 5:
                        if (chuc == 0)
                        {
                            KetQua += ChuSo[donvi];
                        }
                        else
                        {
                            KetQua += " lăm";
                        }
                        break;
                    default:
                        if (donvi != 0)
                        {
                            KetQua += ChuSo[donvi];
                        }
                        break;
                }
                return KetQua;
            }
        }
        //report in phiếu xuất hàng 1 hay nhiều phiếu vẫn ok .
        public DataTable GetHDRprt(string MaHoaDon)
        {
            string sql = "select *from vw_Inphieuxuat where MaHoaDon='" + MaHoaDon + "'";
            SqlDataAdapter sda = new SqlDataAdapter(sql, scon);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            scon.Close();
            return dt;
        }
        //in chiết khấu của khách hàng khi mua sản phẩm

        // đang tesxy.
        //public DataTable loadsanphamn(string tenSanPham, string donViTinh, decimal giaNhap, decimal giaBan, int soLuong, int loaiSanPham, bool status, string ghiChu)
        //{
        //    Myconnect();
        //    string sql = "select *from Sanpham";
        //    SqlCommand cmd = new SqlCommand(sql,scon);
        //    cmd.Parameters.AddWithValue("@TenSanPham", tenSanPham);
        //    cmd.Parameters.AddWithValue("@DonViTinh", donViTinh);
        //    cmd.Parameters.AddWithValue("@GiaNhap", giaNhap);
        //    cmd.Parameters.AddWithValue("@GiaBan", giaBan);
        //    cmd.Parameters.AddWithValue("@SoLuong", soLuong);
        //    cmd.Parameters.AddWithValue("@LoaiSanPham", loaiSanPham);
        //    cmd.Parameters.AddWithValue("@Status", status);
        //    cmd.Parameters.AddWithValue("@GhiChu", ghiChu);
        //    cmd.ExecuteNonQuery();
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    Myclose();
        //    return dt;

        //    }
        //thao tác bảng loại sản phẩm insert
        public DataTable ThemLoaiSanPham(string TenLoai, bool Status)
        {
            Myconnect();
            string sql = "INSERT INTO [dbo].[LoaiSanPham] (TenLoai, Status) VALUES (@TenLoai, @Status)";
            SqlCommand sqlCommand = new SqlCommand(sql, scon);
            SqlCommand cmd = sqlCommand;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TenLoai", TenLoai);
            cmd.Parameters.AddWithValue("@Status", Status);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Myclose();
            return dt;
        }
        //thao tác bảng loại sản phẩm Update
        public DataTable UpdateLoaiSanPham(int id,string TenLoai, bool Status)
        {
            Myconnect();
            string sql = "UPDATE[dbo].[LoaiSanPham] SET TenLoai = @TenLoai, Status = @Status WHERE ID = @ID";
            SqlCommand sqlCommand = new SqlCommand(sql, scon);
            SqlCommand cmd = sqlCommand;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@TenLoai", TenLoai);
            cmd.Parameters.AddWithValue("@Status", Status);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Myclose();
            return dt;
        }
        //thao tác bảng loại sản phẩm Delete
        public DataTable DeleteLoaiSanPham(int id)
        {
            Myconnect();
            string sql = "DELETE FROM [dbo].[LoaiSanPham] WHERE ID = @ID";
            SqlCommand sqlCommand = new SqlCommand(sql, scon);
            SqlCommand cmd = sqlCommand;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Myclose();
            return dt;
        }
    }
}
