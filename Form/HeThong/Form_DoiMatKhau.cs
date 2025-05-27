using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_DoiMatKhau : XtraForm
    {
        public Form_DoiMatKhau()
        {
            InitializeComponent();                
        }

      
        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(txtMatKhauCu.Text))
            {
                XtraMessageBox.Show("Vui lòng nhập vào mật khẩu", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhauCu.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtMatKhauMoi.EditValue.ToString()))
            {
                XtraMessageBox.Show("Vui lòng nhập vào mật khẩu mới.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNhapLaiMatKhau.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtNhapLaiMatKhau.EditValue.ToString()))
            {
                XtraMessageBox.Show("Vui lòng xác nhận lại mật khẩu", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNhapLaiMatKhau.Focus();
                return;
            }
            if (txtMatKhauMoi.Text != txtNhapLaiMatKhau.Text)
            {
                XtraMessageBox.Show("Mật khẩu không khớp. Vui lòng thử lại", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNhapLaiMatKhau.Focus();
                return;
            }
           
            var dataEdit = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_DangNhapChangePassword", new
            {
                tendn = txtTenDangNhap.Text,
                mkmoi = Ultis.GetMd5Hash(txtNhapLaiMatKhau.Text),
                mkcu = Ultis.GetMd5Hash(txtMatKhauCu.Text)
            });
            if (dataEdit.errcode == 1)
            {               
                XtraMessageBox.Show(dataEdit.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);             
                Close();
            }
            else
            {
                XtraMessageBox.Show(dataEdit.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
      
        private void Form_DoiMatKhau_Load(object sender, EventArgs e)
        {
            txtTenDangNhap.Text = Form_Template.TaiKhoanLogin;
        }
    }
}