using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemPhongBan : XtraForm
    {
        public Form_ThemPhongBan()
        {
            InitializeComponent();                
        }

      
        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtPhongBan.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập tên phòng ban", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhongBan.Focus();
                return;
            }
            //Chỉnh sửa thông tin
            if (Form_Template.intIDPhongBan > 0)
            {
                var dataEdit = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_PhongBan", new
                {
                    id = Form_Template.intIDPhongBan,
                    ten = txtPhongBan.Text,                    
                    nguoicapnhat = Form_Template.TaiKhoanLogin.ToUpper()
                });                
                if (dataEdit.errcode == 1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_phongban"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(dataEdit.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form_Template.intIDPhongBan = 0;
                    Close();
                }
                else
                {
                    XtraMessageBox.Show(dataEdit.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            else //Thêm mới chiếc khấu
            {              
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_PhongBan", new
                {
                    id = 0,
                    ten = txtPhongBan.Text,                  
                    nguoicapnhat = Form_Template.TaiKhoanLogin.ToUpper()
                });               
                if (data.errcode==1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_phongban"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPhongBan.Text = ""; 
                    txtPhongBan.Focus();
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
           
        }

       
        private void Form_ThemPhongBan_KeyDown(object sender, KeyEventArgs e)
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

        private void Form_ThemPhongBan_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Template.Edit = false;
            Form_Template.intIDPhongBan = 0;
        }

        private void Form_ThemPhongBan_Load(object sender, EventArgs e)
        {
            if (Form_Template.Edit)
            {
                DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_PhongBanSelectByID", new
                {
                    ID = Form_Template.intIDPhongBan
                });
                if (dt.Rows.Count > 0)
                {
                    txtPhongBan.EditValue = dt.Rows[0]["PhongBan"];
                }
            }
        }
    }
}