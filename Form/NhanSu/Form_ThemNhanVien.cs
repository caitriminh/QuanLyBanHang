using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.Remoting.Contexts;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemNhanVien : XtraForm
    {
        public Form_ThemNhanVien()
        {
            InitializeComponent();
            cboGioiTinh.SelectedItem = 0;
            dteNgaySinh.EditValue = DateTime.Now.Date;
            dteNgayCap.EditValue = DateTime.Now.Date;
            dteNgayChinhThuc.EditValue = DateTime.Now.Date;
            dteNgayNhanViec.EditValue = DateTime.Now.Date;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void XoaText()
        {
            var data = ExecSQL.ExecProcedureSacalar("spGetMaTuDong", new
            {
                option = 9
            });
            txtMaNV.Text = data.ToString();
            txtHoTen.Focus();
            txtHoTen.Text = "";
            txtDiaChi.Text = "";
            Txtdienthoai2.Text = "";
            Txtemail.Text = "";
            Txtghichu.Text = "";
            chkStatus.Checked = true;
            cboGioiTinh.SelectedIndex = 0;
            dteNgaySinh.EditValue = DateTime.Now.Date;
            cboNoiSinh.EditValue = null;
            cboNguyenQuan.EditValue = null;
            cboTinhTrangHonNhan.SelectedIndex = 0;
            txtSoCCCD.EditValue = null;
            dteNgayCap.EditValue = DateTime.Now.Date;
            cboDanToc.EditValue= null;
            cboTonGiao.EditValue = null;
            txtTamTru.EditValue = null;
            cboTrinhDoVanHoa.EditValue = null;
            txtChuyenMon.EditValue = null;
            cboPhongBan.EditValue = null;
            cboChucVu.EditValue = null;
            dteNgayChinhThuc.EditValue = DateTime.Now.Date;
            dteNgayNhanViec.EditValue = DateTime.Now.Date;
            txtSoBHXH.EditValue = null;
            txtMST.EditValue = null;
            txtTaiKhoanNganHang.EditValue = null;
            txtHoTen.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHoTen.Text))
            {
                XtraMessageBox.Show("Vui lòng nhập vào tên nhân viên.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }
            if (string.IsNullOrEmpty(cboGioiTinh.Text))
            {
                XtraMessageBox.Show("Vui lòng chọn giới tính nhân viên.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboGioiTinh.Focus();
                return;
            }
            if (cboNoiSinh.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng chọn nơi sinh nhân viên.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNoiSinh.Focus();
                return;
            }
            if (cboNguyenQuan.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng chọn nguyên quán nhân viên.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNguyenQuan.Focus();
                return;
            }
            if (cboDanToc.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng chọn dân tộc nhân viên.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboDanToc.Focus();
                return;
            }
            if (cboTonGiao.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng chọn tôn giáo nhân viên.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTonGiao.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtSoCCCD.Text))
            {
                XtraMessageBox.Show("Vui lòng nhập vào số căn cước công dân.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoCCCD.Focus();
                return;
            }
            if (cboTrinhDoVanHoa.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng chọn trình độ văn hóa nhân viên.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTrinhDoVanHoa.Focus();
                return;
            }
            if (cboPhongBan.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng chọn phòng ban nhân viên.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboPhongBan.Focus();
                return;
            }
            if (cboChucVu.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng chọn chức vụ nhân viên.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboChucVu.Focus();
                return;
            }
            if (Form_Template.intMaNV > 0) //Update nhân viên
            {
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_NhanVienUpdate", new
                {
                    ID=Form_Template.intMaNV,
                    TenNhanVien = txtHoTen.Text,
                    DiaChi = txtDiaChi.Text,
                    DienThoai = Txtdienthoai2.Text,
                    NgaySinh = Convert.ToDateTime(dteNgaySinh.EditValue),
                    Email = Txtemail.Text,
                    Status = Convert.ToBoolean(chkStatus.EditValue),
                    Ghichu = Txtghichu.Text,
                    GioiTinh = cboGioiTinh.Text,
                    NguoiSua=Form_Template.TaiKhoanLogin.ToUpper(),
                    TinhTrangHonNhan = cboTinhTrangHonNhan.Text,
                    NoiSinh = Convert.ToInt32(cboNoiSinh.EditValue),
                    NguyenQuan = Convert.ToInt32(cboNguyenQuan.EditValue),
                    DanToc = Convert.ToInt32(cboDanToc.EditValue),
                    TonGiao = Convert.ToInt32(cboTonGiao.EditValue),
                    CCCD = txtSoCCCD.Text,
                    NgayCap = Convert.ToDateTime(dteNgayCap.EditValue),
                    NgayNhanViec = Convert.ToDateTime(dteNgayNhanViec.EditValue),
                    NgayChinhThuc = Convert.ToDateTime(dteNgayChinhThuc.EditValue),
                    TrinhDo = Convert.ToInt32(cboTrinhDoVanHoa.EditValue),
                    ChuyenMon = txtChuyenMon.Text,
                    TamTru = txtTamTru.Text,
                    PhongBan = Convert.ToInt32(cboPhongBan.EditValue),
                    ChucVu = Convert.ToInt32(cboChucVu.EditValue),
                    SoBHXH = txtSoBHXH.Text,
                    MST = txtMST.Text,
                    TaiKhoanNH = txtTaiKhoanNganHang.Text,
                    NganHang = cboNganHang.EditValue == null ? 0 : Convert.ToInt32(cboNganHang.EditValue)
                });
                if (data.errcode == 1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_nhanvien"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else //Thêm mới nhân viên
            {
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_NhanVienInsert", new
                {
                    TenNhanVien = txtHoTen.Text,
                    DiaChi = txtDiaChi.Text,
                    DienThoai = Txtdienthoai2.Text,
                    NgaySinh = Convert.ToDateTime(dteNgaySinh.EditValue),
                    Email = Txtemail.Text,
                    Status = Convert.ToBoolean(chkStatus.EditValue),
                    Ghichu = Txtghichu.Text,
                    GioiTinh = cboGioiTinh.Text,
                    NguoiTao= Form_Template.TaiKhoanLogin.ToUpper(),
                    TinhTrangHonNhan = cboTinhTrangHonNhan.Text,
                    NoiSinh = Convert.ToInt32(cboNoiSinh.EditValue),
                    NguyenQuan = Convert.ToInt32(cboNguyenQuan.EditValue),
                    DanToc = Convert.ToInt32(cboDanToc.EditValue),
                    TonGiao = Convert.ToInt32(cboTonGiao.EditValue),
                    CCCD = txtSoCCCD.Text,
                    NgayCap = Convert.ToDateTime(dteNgayCap.EditValue),
                    NgayNhanViec = Convert.ToDateTime(dteNgayNhanViec.EditValue),
                    NgayChinhThuc = Convert.ToDateTime(dteNgayChinhThuc.EditValue),
                    TrinhDo = Convert.ToInt32(cboTrinhDoVanHoa.EditValue),
                    ChuyenMon = txtChuyenMon.Text,
                    TamTru = txtTamTru.Text,
                    PhongBan = Convert.ToInt32(cboPhongBan.EditValue),
                    ChucVu = Convert.ToInt32(cboChucVu.EditValue),
                    SoBHXH = txtSoBHXH.Text,
                    MST = txtMST.Text,
                    TaiKhoanNH = txtTaiKhoanNganHang.Text,
                    NganHang = cboNganHang.EditValue == null ? 0 : Convert.ToInt32(cboNganHang.EditValue)
                });
                if (data.errcode == 1)
                {
                    XoaText();
                     //Gửi dữ liệu
                     var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_nhanvien"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);                 
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            
        }             

        public void GetNoiSinh()
        {
            var data = ExecSQL.ExecProcedureDataAsDataTable("sp_TinhThanhSelect");
            cboNoiSinh.Properties.DataSource = data;
            cboNoiSinh.Properties.DisplayMember = "TinhThanh";
            cboNoiSinh.Properties.ValueMember = "Id";
            cboNoiSinh.EditValue = Form_Template.intIDTinhThanh;
        }

        public void GetNguyenQuan()
        {
            var data = ExecSQL.ExecProcedureDataAsDataTable("sp_TinhThanhSelect");
            cboNguyenQuan.Properties.DataSource = data;
            cboNguyenQuan.Properties.DisplayMember = "TinhThanh";
            cboNguyenQuan.Properties.ValueMember = "Id";
            cboNguyenQuan.EditValue = Form_Template.intIDTinhThanh;
        }

        public void GetTonGiao()
        {
            var data = ExecSQL.ExecProcedureDataAsDataTable("sp_TonGiaoSelect");
            cboTonGiao.Properties.DataSource = data;
            cboTonGiao.Properties.DisplayMember = "TonGiao";
            cboTonGiao.Properties.ValueMember = "Id";
        }

        public void GetDanToc()
        {
            var data = ExecSQL.ExecProcedureDataAsDataTable("sp_DanTocSelect");
            cboDanToc.Properties.DataSource = data;
            cboDanToc.Properties.DisplayMember = "DanToc";
            cboDanToc.Properties.ValueMember = "Id";
        }

        public void GetTrinhDo()
        {
            var data = ExecSQL.ExecProcedureDataAsDataTable("sp_TrinhDoSelect");
            cboTrinhDoVanHoa.Properties.DataSource = data;
            cboTrinhDoVanHoa.Properties.DisplayMember = "TrinhDo";
            cboTrinhDoVanHoa.Properties.ValueMember = "Id";
        }

        public void GetPhongBan()
        {
            var data = ExecSQL.ExecProcedureDataAsDataTable("sp_PhongBanSelect");
            cboPhongBan.Properties.DataSource = data;
            cboPhongBan.Properties.DisplayMember = "PhongBan";
            cboPhongBan.Properties.ValueMember = "Id";
        }

        public void GetChucVu()
        {
            var data = ExecSQL.ExecProcedureDataAsDataTable("sp_ChucVuSelect");
            cboChucVu.Properties.DataSource = data;
            cboChucVu.Properties.DisplayMember = "ChucVu";
            cboChucVu.Properties.ValueMember = "Id";
        }

        public void GetNganHang()
        {
            var data = ExecSQL.ExecProcedureDataAsDataTable("sp_NganHangSelect");
            cboNganHang.Properties.DataSource = data;
            cboNganHang.Properties.DisplayMember = "NganHang";
            cboNganHang.Properties.ValueMember = "Id";
        }

        private void OnNext(MessageBroker value)
        {
            if (value.task == "them_tinhthanh")
            {
                GetNoiSinh();
                GetNguyenQuan();
            }
        }

        private void Form_ThemNhanVien_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetNoiSinh();
            GetNguyenQuan();
            GetTonGiao();
            GetDanToc();
            GetTrinhDo();
            GetPhongBan();
            GetChucVu();
            GetNganHang();
            if (Form_Template.Edit)
            {
                DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_NhanVienSelectByID", new
                {
                    id = Form_Template.intMaNV
                });
                if (dt.Rows.Count > 0)
                {
                    txtMaNV.EditValue = dt.Rows[0]["MaNV"];
                    txtHoTen.EditValue = dt.Rows[0]["TenNhanVien"];
                    cboGioiTinh.EditValue = dt.Rows[0]["GioiTinh"];
                    txtDiaChi.EditValue = dt.Rows[0]["DiaChi"];
                    Txtdienthoai2.EditValue = dt.Rows[0]["DienThoai"];
                    Txtemail.EditValue = dt.Rows[0]["Email"];
                    Txtghichu.EditValue = dt.Rows[0]["Ghichu"];
                    chkStatus.EditValue = Convert.ToBoolean(dt.Rows[0]["Status"]);
                    dteNgaySinh.EditValue = Convert.ToDateTime(dt.Rows[0]["NgaySinh"]);
                    cboTinhTrangHonNhan.EditValue = dt.Rows[0]["TinhTrangHonNhan"];
                    cboNoiSinh.EditValue = dt.Rows[0]["NoiSinh"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["NoiSinh"]);
                    cboNguyenQuan.EditValue = dt.Rows[0]["NguyenQuan"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["NguyenQuan"]);
                    cboDanToc.EditValue = dt.Rows[0]["DanToc"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["DanToc"]);
                    cboTonGiao.EditValue = dt.Rows[0]["TonGiao"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["TonGiao"]);
                    txtSoCCCD.EditValue = dt.Rows[0]["CCCD"];
                    dteNgayCap.EditValue = dt.Rows[0]["NgayCap"] == DBNull.Value ? DateTime.Now.Date : Convert.ToDateTime(dt.Rows[0]["NgayCap"]);
                    dteNgayChinhThuc.EditValue = dt.Rows[0]["NgayChinhThuc"] == DBNull.Value ? DateTime.Now.Date : Convert.ToDateTime(dt.Rows[0]["NgayChinhThuc"]);
                    dteNgayNhanViec.EditValue = dt.Rows[0]["NgayNhanViec"] == DBNull.Value ? DateTime.Now.Date : Convert.ToDateTime(dt.Rows[0]["NgayNhanViec"]);
                    txtTamTru.EditValue = dt.Rows[0]["TamTru"];
                    cboTrinhDoVanHoa.EditValue = dt.Rows[0]["TrinhDo"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["TrinhDo"]);
                    txtSoBHXH.EditValue = dt.Rows[0]["SoBHXH"];
                    txtMST.EditValue = dt.Rows[0]["MST"];
                    txtTaiKhoanNganHang.EditValue = dt.Rows[0]["TaiKhoanNH"];
                    cboPhongBan.EditValue = dt.Rows[0]["PhongBan"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["PhongBan"]);
                    cboChucVu.EditValue = dt.Rows[0]["ChucVu"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["ChucVu"]);
                    txtChuyenMon.EditValue =  dt.Rows[0]["ChuyenMon"];
                    cboNganHang.EditValue = dt.Rows[0]["NganHang"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["NganHang"]);
                    txtMaNV.Properties.ReadOnly = true;
                }
            }
            else
            {
                var data = ExecSQL.ExecProcedureSacalar("spGetMaTuDong", new
                {
                    option = 9
                });
                txtMaNV.Text = data.ToString();
                txtHoTen.Focus();
            }
        }

        private void Form_ThemNhanVien_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Template.intMaNV = 0;
            Form_Template.Edit = false;
        }

        private void cboNganHang_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
            {
                cboNganHang.EditValue = null;
            }
            else if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                Form_Template.intIDNganHang = 0;
                Form_ThemNganHang frm = new Form_ThemNganHang();
                frm.ShowDialog();
            }
        }

        private void cboNoiSinh_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                Form_Template.intIDTinhThanh = 0;
                Form_ThemTinhThanh frm = new Form_ThemTinhThanh();
                frm.ShowDialog();
            }            
        }

        private void txtMaNV_EditValueChanged(object sender, EventArgs e)
        {
            string sourceFilePath = Form_Template.strURLImageNV + @"\" + txtMaNV.Text + ".jpg";
            try
            {
                pictureEdit1.Image = new Bitmap(sourceFilePath);
            }
            catch (Exception)
            {
                pictureEdit1.Image = new Bitmap(Application.StartupPath + @"\logo\no_image.jpg");
            }
        }

        private void pictureEdit1_DoubleClick(object sender, EventArgs e)
        {
            using (XtraOpenFileDialog openFileDialog = new XtraOpenFileDialog())
            {
                //openFileDialog.InitialDirectory = @"C:\"; // Thư mục mặc định
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"; // Bộ lọc file
                openFileDialog.Title = "Chọn ảnh để cập nhật";
                try
                {
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        pictureEdit1.Image?.Dispose();
                        // Lấy đường dẫn của hình ảnh được chọn
                        string sourceFilePath = openFileDialog.FileName;
                        // Hiển thị hình ảnh trong PictureBox (tùy chọn)
                        pictureEdit1.Image = new Bitmap(sourceFilePath);

                        // Tạo thư mục nếu chưa tồn tại
                        if (!Directory.Exists(Form_Template.strURLImageNV))
                        {
                            Directory.CreateDirectory(Form_Template.strURLImageNV);
                        }

                        // Đường dẫn lưu ảnh trong thư mục D:/Image
                        string destinationFilePath = Path.Combine(Form_Template.strURLImageNV, txtMaNV.Text + ".jpg");

                        // Sao chép file từ thư mục gốc sang thư mục đích
                        File.Copy(sourceFilePath, destinationFilePath, true);

                        //Gửi dữ liệu
                        var msgBroker = new MessageBroker
                        {
                            data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                            task = "them_nhanvien"
                        };
                        msgBroker.Publish();
                        XtraMessageBox.Show("Cập nhật ảnh nhân viên thành công.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);                       
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Error: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}