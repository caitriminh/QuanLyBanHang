using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemNguoiPhuThuoc : XtraForm
    {
        public Form_ThemNguoiPhuThuoc()
        {
            InitializeComponent();
            dteNgayHetHan.EditValue = DateTime.Now.Date;
            dteNgaySinh.EditValue = DateTime.Now.Date;
        }

        public void GetNhanVien()
        {
            var lstData = ExecSQL.ExecProcedureDataAsDataTable("sp_NhanVienselectall", new
            {
                option= Form_Template.intIDNhanVien > 0 ? 2 : 1
            });
            cboNhanVien.Properties.DataSource = lstData;
            cboNhanVien.Properties.DisplayMember = "TenNhanVien";
            cboNhanVien.Properties.ValueMember = "ID";
        }

        public void GetQuanHe()
        {
            var lstData = ExecSQL.ExecProcedureDataAsDataTable("sp_QuanHeSelect");
            cboQuanHe.Properties.DataSource = lstData;
            cboQuanHe.Properties.DisplayMember = "QuanHe";
            cboQuanHe.Properties.ValueMember = "Id";
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cboNhanVien.EditValue ==null)
            {
                XtraMessageBox.Show("Vui lòng nhập chọn tên nhân viên", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNhanVien.Focus();
                return;
            }
            if (cboQuanHe.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập chọn quan hệ", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboQuanHe.Focus();
                return;
            }
            
            //Chỉnh sửa thông tin
            if (Form_Template.intIDNguoiPhuThuoc > 0)
            {
                var dataEdit = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_NguoiPhuThuocUpdate", new
                {
                    Id = Form_Template.intIDNguoiPhuThuoc,
                    NguoiPhuThuoc = txtNguoiPhuThuoc.Text,
                    GioiTinh = cboGioiTinh.Text,
                    NgaySinh = Convert.ToDateTime(dteNgaySinh.EditValue),
                    NgayHetHan = Convert.ToDateTime(dteNgayHetHan.EditValue) == DateTime.MinValue ? (object)DBNull.Value : dteNgayHetHan.EditValue,
                    QuanHe = Convert.ToInt32(cboQuanHe.EditValue),
                    GhiChu = txtGhiChu.Text,
                    NguoiSua = Form_Template.TaiKhoanLogin.ToUpper()
                });               
                if (dataEdit.errcode == 1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_nguoiphuthuoc"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(dataEdit.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form_Template.intIDNguoiPhuThuoc = 0;
                    Close();
                }
                else
                {
                    XtraMessageBox.Show(dataEdit.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            else //Thêm mới chiếc khấu
            {              
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_NguoiPhuThuocInsert", new
                {                     
                    MaNV = Convert.ToInt32(cboNhanVien.EditValue),
                    NguoiPhuThuoc = txtNguoiPhuThuoc.Text,
                    GioiTinh = cboGioiTinh.Text,
                    NgaySinh = Convert.ToDateTime(dteNgaySinh.EditValue),
                    NgayHetHan = Convert.ToDateTime(dteNgayHetHan.EditValue) == DateTime.MinValue ? (object)DBNull.Value : dteNgayHetHan.EditValue,
                    QuanHe = Convert.ToInt32(cboQuanHe.EditValue),
                    GhiChu = txtGhiChu.Text,
                    NguoiTao = Form_Template.TaiKhoanLogin.ToUpper()
                });                
                if (data.errcode==1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_nguoiphuthuoc"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboNhanVien.EditValue = null;
                    cboQuanHe.EditValue = null;
                    txtGhiChu.EditValue = null;
                    dteNgayHetHan.EditValue = null;
                    dteNgaySinh.EditValue = null;
                    cboNhanVien.Focus();
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }           
        }

        private void Form_ThemNguoiPhuThuoc_KeyDown(object sender, KeyEventArgs e)
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

        private void Form_ThemNguoiPhuThuoc_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Template.intIDNguoiPhuThuoc = 0;
            Form_Template.Edit = false;
        }

        private void Form_ThemNguoiPhuThuoc_Load(object sender, EventArgs e)
        {
            GetNhanVien();
            GetQuanHe();
            if (Form_Template.Edit)
            {
                DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_NguoiPhuThuocSelectID", new
                {
                    Id = Form_Template.intIDNguoiPhuThuoc
                });
                if (dt.Rows.Count > 0)
                {
                    cboNhanVien.EditValue = Convert.ToInt32(dt.Rows[0]["MaNV"]);
                    txtGhiChu.EditValue = dt.Rows[0]["GhiChu"];
                    dteNgaySinh.EditValue = Convert.ToDateTime(dt.Rows[0]["NgaySinh"]);
                    dteNgayHetHan.EditValue = dt.Rows[0]["NgayHetHan"];
                    cboQuanHe.EditValue = Convert.ToInt32(dt.Rows[0]["QuanHe"]);
                    txtNguoiPhuThuoc.EditValue = dt.Rows[0]["NguoiPhuThuoc"];
                    cboGioiTinh.EditValue = dt.Rows[0]["GioiTinh"];
                    cboNhanVien.Properties.ReadOnly = true;
                }
            }
        }
    }
}