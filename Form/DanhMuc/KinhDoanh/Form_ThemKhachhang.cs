using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemKhachhang : XtraForm
    {
        public Form_ThemKhachhang()
        {
            InitializeComponent();
            txtEmail.Properties.Mask.EditMask = "[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,4}";
        }

        private void Form_Khachhang_Load(object sender, EventArgs e)
        {                
            GetNhomDoiTac();
            GetNhanVienBanHang();
            if (Form_Template.Edit)
            {
                Text = "Chỉnh Sửa Khách Hàng";
                DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_KhachHangByID", new
                {
                    id = Form_Template.intIDMaKH
                });
                if (dt.Rows.Count > 0)
                {
                    txtTenKH.EditValue = dt.Rows[0]["TenKhachHang"];
                    txtDiaChi.EditValue = dt.Rows[0]["DiaChi"];
                    txtDiaChi2.EditValue = dt.Rows[0]["DiaChi2"];
                    txtDT1.EditValue = dt.Rows[0]["DienThoai1"];
                    txtDT2.EditValue = dt.Rows[0]["DienThoai2"];
                    txtEmail.EditValue = dt.Rows[0]["EmailKH"];
                    txtGhiChu.EditValue = dt.Rows[0]["Ghichu"];
                    txtMST.EditValue = dt.Rows[0]["MaSoThue"];
                    cboNhomDoiTac.EditValue = dt.Rows[0]["Nhomkhachhang"];
                    TxtNguoiDaiDien.EditValue = dt.Rows[0]["NguoiDaiDien"];
                    TxtSoDTNuoiDaiDien.EditValue = dt.Rows[0]["SoDTNguoiDaiDien"];
                    TxtChucVu.EditValue = dt.Rows[0]["ChucVu"];
                    TxtNguoiLienHe.EditValue = dt.Rows[0]["NguoiLienHe"];
                    TxtSoDTNguoiLienHe.EditValue = dt.Rows[0]["SoDTNguoiLienHe"];
                }
            }
            else
            {
                Text = "Thêm Mới Khách Hàng";
            }
        }

        public  void GetNhomDoiTac()        {
           
            var lstDoiTac = ExecSQL.ExecProcedureDataAsDataTable("sp_NhomKhachHangSelect");
            cboNhomDoiTac.Properties.DataSource = lstDoiTac;
            cboNhomDoiTac.Properties.DisplayMember = "TenNhom";
            cboNhomDoiTac.Properties.ValueMember = "ID";           
        }

        public void GetNhanVienBanHang()
        {
            var lstNhanVien = ExecSQL.ExecProcedureDataAsDataTable("sp_NhanVienSelect");
            cboNhanVien.Properties.DataSource = lstNhanVien;
            cboNhanVien.Properties.DisplayMember = "TenNhanVien";
            cboNhanVien.Properties.ValueMember = "ID";
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtTenKH.EditValue ==null)
            {
                XtraMessageBox.Show("Vui lòng nhập vào tên khách hàng.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKH.Focus();
                return;
            }
            if (cboNhomDoiTac.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng chọn nhóm đối tác.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNhomDoiTac.Focus();
                return;
            }
            if (cboNhanVien.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng chọn nhân viên bán hàng phụ trách.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNhanVien.Focus();
                return;
            }
            if (Form_Template.intIDMaKH > 0) //Update khách hàng
            {
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_UpdateKhachHang", new
                {
                    ID = Form_Template.intIDMaKH,
                    TenKhachHang = txtTenKH.Text,
                    DiaChi = txtDiaChi.Text,
                    DiaChi2 = txtDiaChi2.Text,
                    DienThoai1 = txtDT1.Text,
                    DienThoai2 = txtDT2.Text,
                    EmailKH = txtEmail.Text,
                    MaSoThue = txtMST.Text,
                    Ghichu = txtGhiChu.Text,
                    Nhomkhachhang = Convert.ToInt16(cboNhomDoiTac.EditValue),
                    NguoiSua = Form_Template.TaiKhoanLogin.ToUpper(),
                    MaNVSale = Convert.ToInt32(cboNhanVien.EditValue),
                    NguoiDaiDien=TxtNguoiDaiDien.Text,
                    SoDTNguoiDaiDien=TxtSoDTNuoiDaiDien.Text,
                    ChucVu=TxtChucVu.Text,
                    NguoiLienHe=TxtNguoiLienHe.Text,
                    SoDTNguoiLienHe=TxtSoDTNguoiLienHe.Text
                }); ;
                if (data.errcode == 1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_khachhang"
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
            else //Thêm mới khách hàng
            {
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_CreateKhachHang", new
                {
                    TenKhachHang = txtTenKH.Text,
                    DiaChi = txtDiaChi.Text,
                    DiaChi2 = txtDiaChi2.Text,
                    DienThoai1 = txtDT1.Text,
                    DienThoai2 = txtDT2.Text,
                    EmailKH = txtEmail.Text,
                    MaSoThue = txtMST.Text,
                    Ghichu = txtGhiChu.Text,
                    Nhomkhachhang = Convert.ToInt16(cboNhomDoiTac.EditValue),
                    NguoiTao = Form_Template.TaiKhoanLogin.ToUpper(),
                    MaNVSale = Convert.ToInt32(cboNhanVien.EditValue),
                    NguoiDaiDien = TxtNguoiDaiDien.Text,
                    SoDTNguoiDaiDien = TxtSoDTNuoiDaiDien.Text,
                    ChucVu = TxtChucVu.Text,
                    NguoiLienHe = TxtNguoiLienHe.Text,
                    SoDTNguoiLienHe = TxtSoDTNguoiLienHe.Text
                });
                if (data.errcode == 1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_khachhang"
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

        private void Form_ThemKhachhang_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Template.intIDMaKH = 0;
            Form_Template.Edit = false;
        }

        private void Form_ThemKhachhang_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    btnLuu_Click(sender, e);
                    break;
                case Keys.Escape:
                    Close();
                    break;
            }
        }
    }
}