using DevExpress.XtraEditors;
using QuanLyBanHang.Class;
using System;
using System.Data;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThongTin : XtraForm
    {
        public Form_ThongTin()
        {
            InitializeComponent();
            GetPhanQuyen();
        }

        public void GetPhanQuyen()
        {
            var data = ExecSQL.ExecProcedureDataFistOrDefault<QuyenHan>("sp_PhanQuyenSelectById", new
            {
                NhomQuyen = Form_Template.QuyenHan,
                Menu = 3
            });
            if (data == null) { return; }
            btnLuu.Enabled = data.Luu;           
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtTenDonVi.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập vào tên đơn vị.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDonVi.Focus();
                return;
            }
            if (txtDiaChi.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập vào tên địa chỉ.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return;
            }
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_ThongTinInsert", new
            {
                TenDonVi = txtTenDonVi.Text,
                DiaChi = txtDiaChi.Text,
                Email = txtEmail.Text,
                SoDT = txtSoDT.Text,
                Website = txtWebsite.Text,
                MST = txtMST.Text,
                NguoiDaiDien = txtNguoiDaiDien.Text
            });
            if(data.errcode==1) {
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form_ThongTin_Load(object sender, EventArgs e)
        {
            DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_ThongTinSelect");
            if (dt.Rows.Count == 0) { return; }
            txtTenDonVi.Text = dt.Rows[0]["TenDonVi"].ToString();
            txtDiaChi.Text = dt.Rows[0]["DiaChi"].ToString();
            txtSoDT.Text = dt.Rows[0]["SoDT"].ToString();
            txtEmail.Text = dt.Rows[0]["Email"].ToString();
            txtWebsite.Text = dt.Rows[0]["Website"].ToString();
            txtMST.Text = dt.Rows[0]["MST"].ToString();
            txtNguoiDaiDien.Text = dt.Rows[0]["NguoiDaiDien"].ToString();
        }
    }
}