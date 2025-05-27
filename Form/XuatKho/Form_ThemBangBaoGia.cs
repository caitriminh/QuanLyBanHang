using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemBangBaoGia : XtraForm
    {
        public Form_ThemBangBaoGia()
        {
            InitializeComponent();
            dteNgayBaoGia.EditValue = DateTime.Now.Date;
        }


        public void GetTenKhachHang()
        {
            var lstKH = ExecSQL.ExecProcedureDataAsDataTable("sp_KhachHangSelect");
            cboKhachHang.Properties.DataSource = lstKH;
            cboKhachHang.Properties.DisplayMember = "TenKhachHang";
            cboKhachHang.Properties.ValueMember = "ID";
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cboKhachHang.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng chọn khách hàng", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKhachHang.Focus();
                return;
            }
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_BaoGiaInsert", new
            {
                    IdGiaBan = Form_GiaBan.strIDGiaBan,                  
                    MaKH = Convert.ToInt32(cboKhachHang.EditValue),
                    NgayBaoGia = Convert.ToDateTime(dteNgayBaoGia.EditValue),
                    NguoiTao = Form_Template.TaiKhoanLogin.ToUpper()
             });
             if (data.errcode==1)
             {
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
             else
             {
                XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void Form_ThemBangBaoGia_Load(object sender, EventArgs e)
        {
            GetTenKhachHang();
        }
    }
}