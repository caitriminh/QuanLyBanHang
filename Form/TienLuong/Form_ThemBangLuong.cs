using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemBangLuong : XtraForm
    {
        public Form_ThemBangLuong()
        {
            InitializeComponent();           
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
            if (txtCongLeTet.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập mức lương nhân viên", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCongLeTet.Focus();
                return;
            }
           
            //Chỉnh sửa thông tin
            if (Form_Template.intIDBangLuong > 0)
            {
                var dataEdit = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_BangLuongUpdate", new
                {
                    Id = Form_Template.intIDBangLuong,                    
                    CongLeTet = Convert.ToDecimal(txtCongLeTet.EditValue)                
                   
                });               
                if (dataEdit.errcode == 1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_bangluong"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(dataEdit.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form_Template.intIDBangLuong = 0;
                    Close();
                }
                else
                {
                    XtraMessageBox.Show(dataEdit.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }            
        }
       
        private void Form_ThemMucLuong_Load(object sender, EventArgs e)
        {
            GetNhanVien();
            if (Form_Template.Edit)
            {
                DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_BangLuongSelectById", new
                {
                    Id = Form_Template.intIDBangLuong
                });
                if (dt.Rows.Count > 0)
                {
                    cboNhanVien.EditValue = Convert.ToInt32(dt.Rows[0]["MaNV"]);
                    txtCongLeTet.EditValue = dt.Rows[0]["CongLeTet"];                   
                    cboNhanVien.Properties.ReadOnly = true;
                }
            }
        }

        private void Form_ThemBangLuong_KeyDown(object sender, KeyEventArgs e)
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

        private void Form_ThemBangLuong_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Template.intIDBangLuong = 0;
            Form_Template.Edit = false;
        }
    }
}