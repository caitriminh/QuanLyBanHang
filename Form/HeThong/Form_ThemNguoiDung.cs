using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemNguoiDung : XtraForm
    {
        public Form_ThemNguoiDung()
        {
            InitializeComponent();                
        }

      
        public void GetNhomQuyen()
        {
            var lstNhomQuyen = ExecSQL.ExecProcedureDataAsDataTable("sp_NhomQuyenSelect");
            cboNhomQuyen.Properties.DataSource = lstNhomQuyen;
            cboNhomQuyen.Properties.DisplayMember = "NhomQuyen";
            cboNhomQuyen.Properties.ValueMember = "Id";
        }

        public void GetNhanVien()
        {
            var lstData = ExecSQL.ExecProcedureDataAsDataTable("sp_NhanVienselectall");
            cboNhanVien.Properties.DataSource = lstData;
            cboNhanVien.Properties.DisplayMember = "TenNhanVien";
            cboNhanVien.Properties.ValueMember = "ID";
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtTenDangNhap.EditValue ==null)
            {
                XtraMessageBox.Show("Vui lòng nhập tên đăng nhập", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDangNhap.Focus();
                return;
            }
            if (cboNhanVien.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập chọn tên nhân viên", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNhanVien.Focus();
                return;
            }
            if (cboNhanVien.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập chọn nhóm quyền", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNhomQuyen.Focus();
                return;
            }
            if (txtMatKhau.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập vào mật khẩu", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDangNhap.Focus();
                return;
            }
            if (txtMatKhau.Text!=txtNhapLaiMatKhau.Text)
            {
                XtraMessageBox.Show("Mật khẩu không khớp. Vui lòng thử lại", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNhapLaiMatKhau.Focus();
                return;
            }
            //Chỉnh sửa thông tin
            if (Form_Template.TenDangNhap.Length > 0)
            {
                var dataEdit = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_DangNhapUpdate", new
                {
                    Form_Template.TenDangNhap,
                    MaNV = Convert.ToInt32(cboNhanVien.EditValue),
                    NhomQuyen = Convert.ToInt32(cboNhomQuyen.EditValue),
                    MatKhau = Ultis.GetMd5Hash(txtNhapLaiMatKhau.Text),
                    Login = Convert.ToBoolean(chkTrangThai.EditValue),
                    NguoiSua = Form_Template.TaiKhoanLogin.ToUpper()
                });               
                if (dataEdit.errcode == 1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_nguoidung"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(dataEdit.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form_Template.TenDangNhap = "";
                    Close();
                }
                else
                {
                    XtraMessageBox.Show(dataEdit.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            else //Thêm mới chiếc khấu
            {              
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_DangNhapInsert", new
                {
                    TenDangNhap = txtTenDangNhap.Text,
                    MatKhau = Ultis.GetMd5Hash(txtNhapLaiMatKhau.Text),
                    NhomQuyen = Convert.ToInt32(cboNhomQuyen.EditValue),  
                    MaNV = Convert.ToInt32(cboNhanVien.EditValue),
                    NguoiTao = Form_Template.TaiKhoanLogin.ToUpper()
                });
                if (data == null)
                {
                    XtraMessageBox.Show("Thêm mới sản phẩm không thành công", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (data.errcode==1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_nguoidung"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMatKhau.Text = "";
                    txtNhapLaiMatKhau.Text = "";
                    cboNhomQuyen.EditValue = DBNull.Value;                 
                    txtMatKhau.Focus();
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
           
        }

        private void Form_ThemNguoiDung_Load(object sender, EventArgs e)
        {
            GetNhomQuyen();
            GetNhanVien();
            if (Form_Template.Edit)
            {
                DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_DangNhapSelectID", new
                {
                   Form_Template.TenDangNhap
                });
                if (dt.Rows.Count > 0)
                {
                    txtTenDangNhap.EditValue = dt.Rows[0]["TenDangNhap"];
                    cboNhomQuyen.EditValue = dt.Rows[0]["NhomQuyen"];
                    cboNhanVien.EditValue = dt.Rows[0]["MaNV"];
                    chkTrangThai.EditValue = dt.Rows[0]["Login"];
                    txtTenDangNhap.Properties.ReadOnly = true;
                }
            }
        }
    }
}