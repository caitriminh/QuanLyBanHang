using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemPhuCap : XtraForm
    {
        public Form_ThemPhuCap()
        {
            InitializeComponent();
            dteNgayNhap.EditValue = DateTime.Now.Date;
        }

        public void GetNhanVien()
        {
            var lstData = ExecSQL.ExecProcedureDataAsDataTable("sp_NhanVienselectall", new
            {
                option= 1
            });
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
            if (txtPhuCap.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập phụ cấp", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhuCap.Focus();
                return;
            }            
            if (dteNgayNhap.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập ngày nhập mức lương", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dteNgayNhap.Focus();
                return;
            }
            //Chỉnh sửa thông tin
            if (Form_Template.intIDPhuCap > 0)
            {
                var dataEdit = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_PhuCap", new
                {
                    Id = Form_Template.intIDPhuCap,
                    ngaynhap = Convert.ToDateTime(dteNgayNhap.EditValue),
                    phucap = Convert.ToDecimal(txtPhuCap.EditValue),                   
                    ghichu = txtGhiChu.Text,
                    nguoicapnhat = Form_Template.TaiKhoanLogin.ToUpper()
                });               
                if (dataEdit.errcode == 1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_phucap"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(dataEdit.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form_Template.intIDPhuCap = 0;
                    Close();
                }
                else
                {
                    XtraMessageBox.Show(dataEdit.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                var dataEdit = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_PhuCap", new                
                {           
                    manv = Convert.ToInt32(cboNhanVien.EditValue),
                    ngaynhap = Convert.ToDateTime(dteNgayNhap.EditValue),
                    phucap = Convert.ToDecimal(txtPhuCap.EditValue),
                    ghichu = txtGhiChu.Text,
                    nguoicapnhat = Form_Template.TaiKhoanLogin.ToUpper()                   
                });
                if (dataEdit.errcode == 1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_phucap"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(dataEdit.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboNhanVien.EditValue = null;
                    txtPhuCap.EditValue = null;
                    dteNgayNhap.EditValue = null;
                    txtGhiChu.EditValue = null;                   
                    cboNhanVien.Focus();
                }
                else
                {
                    XtraMessageBox.Show(dataEdit.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Form_ThemPhuCap_Load(object sender, EventArgs e)
        {
            GetNhanVien();
            if (Form_Template.Edit)
            {
                DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_PhuCapSelectById", new
                {
                    Id = Form_Template.intIDPhuCap
                });
                if (dt.Rows.Count > 0)
                {
                    cboNhanVien.EditValue = Convert.ToInt32(dt.Rows[0]["MaNV"]);
                    txtPhuCap.EditValue = dt.Rows[0]["PhuCap"];
                    txtGhiChu.EditValue = dt.Rows[0]["GhiChu"];
                    dteNgayNhap.EditValue = dt.Rows[0]["NgayNhap"];
                    cboNhanVien.Properties.ReadOnly = true;
                }
            }
        }

        private void Form_ThemPhuCap_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Template.intIDPhuCap = 0;
            Form_Template.Edit = false;
        }

        private void Form_ThemPhuCap_KeyDown(object sender, KeyEventArgs e)
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