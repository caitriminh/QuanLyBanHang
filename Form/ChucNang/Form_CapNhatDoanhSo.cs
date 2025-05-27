using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_CapNhatDoanhSo : XtraForm
    {
        public Form_CapNhatDoanhSo()
        {
            InitializeComponent();
        }

        public void GetNhanVien()
        {
            var lstData = ExecSQL.ExecProcedureDataAsDataTable("sp_NhanVienselectall", new
            {
                option = 1
            }) ;
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
            if (cboNhanVien.EditValue ==null)
            {
                XtraMessageBox.Show("Vui lòng nhập chọn tên nhân viên", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNhanVien.Focus();
                return;
            }  
            
            //Chỉnh sửa thông tin
            if (Form_Template.intIDHoaHongNhanVien > 0)
            {
                var dataEdit = ExecSQL.ExecProcedureDataFistOrDefault<Status>("spHoaHongUpdateDS", new
                {
                    Id = Form_Template.intIDHoaHongNhanVien,                    
                    SoTien = Convert.ToDecimal(txtSoTien.EditValue),
                    PhanTram = 0,
                    GhiChu = txtGhiChu.Text,                 
                    NguoiSua = Form_Template.TaiKhoanLogin.ToUpper()
                });
                if (dataEdit.errcode == 1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_hoahong"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(dataEdit.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form_Template.intIDHoaHongNhanVien = 0;
                    Close();
                }
                else
                {
                    XtraMessageBox.Show(dataEdit.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void Form_CapNhatDoanhSo_Load(object sender, EventArgs e)
        {
            GetNhanVien();
            if (Form_Template.Edit)
            {
                DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("spGetHoaHong", new
                {
                    Option = 2,
                    Id = Form_Template.intIDHoaHongNhanVien
                });
                if (dt.Rows.Count > 0)
                {
                    cboNhanVien.EditValue = Convert.ToInt32(dt.Rows[0]["MaNV"]);
                    txtGhiChu.EditValue = dt.Rows[0]["GhiChu"];
                    txtSoTien.EditValue = dt.Rows[0]["SoTien"];                   
                    cboNhanVien.Properties.ReadOnly = true;
                }
            }
        }

        private void Form_CapNhatDoanhSo_KeyDown(object sender, KeyEventArgs e)
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

        private void Form_CapNhatDoanhSo_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Template.intIDHoaHongNhanVien = 0;
            Form_Template.Edit = false;
        }
    }
}