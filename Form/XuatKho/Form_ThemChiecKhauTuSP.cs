using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemChiecKhauTuSP : XtraForm
    {
        public Form_ThemChiecKhauTuSP()
        {
            InitializeComponent();                
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
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_ChiecKhauInsertFromSP", new
            {
                    MaSP = Form_Sanpham.strMaSP,
                    ChiecKhau = Convert.ToDecimal(txtChiecKhau.EditValue),
                    MaKH = Convert.ToInt32(cboKhachHang.EditValue),                   
                    Ghichu = txtGhiChu.Text,
                    NguoiTao = Form_Template.TaiKhoanLogin.ToUpper()
             });
                if (data == null)
                {
                    XtraMessageBox.Show("Thêm mới chiếc khấu không thành công", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (data.errcode==1)
                {                   
                 
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtChiecKhau.EditValue = 0;                   
                    txtGhiChu.Text = "";                   
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
           
           
        }

        private void Form_ThemChiecKhauTuSP_Load(object sender, EventArgs e)
        {
            GetTenKhachHang();
        }
    }
}