using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemNganHang : XtraForm
    {
        public Form_ThemNganHang()
        {
            InitializeComponent();                
        }

      
        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtNganHang.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập tên ngân hàng", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNganHang.Focus();
                return;
            }
            //Chỉnh sửa thông tin
            if (Form_Template.intIDNganHang > 0)
            {
                var dataEdit = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_NganHang", new
                {
                    id = Form_Template.intIDNganHang,
                    ten = txtNganHang.Text,                    
                    nguoicapnhat = Form_Template.TaiKhoanLogin.ToUpper()
                });                
                if (dataEdit.errcode == 1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_nganhang"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(dataEdit.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form_Template.intIDNganHang = 0;
                    Close();
                }
                else
                {
                    XtraMessageBox.Show(dataEdit.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            else //Thêm mới chiếc khấu
            {              
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_NganHang", new
                {
                    id = 0,
                    ten = txtNganHang.Text,                  
                    nguoicapnhat = Form_Template.TaiKhoanLogin.ToUpper()
                });               
                if (data.errcode==1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_nganhang"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNganHang.Text = ""; 
                    txtNganHang.Focus();
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
           
        }


        private void Form_ThemNganHang_Load(object sender, EventArgs e)
        {
            if (Form_Template.Edit)
            {
                DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_NganHangSelectByID", new
                {
                    ID = Form_Template.intIDNganHang
                });
                if (dt.Rows.Count > 0)
                {
                    txtNganHang.EditValue = dt.Rows[0]["NganHang"];
                }
            }
        }

        private void Form_ThemNganHang_KeyDown(object sender, KeyEventArgs e)
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

        private void Form_ThemNganHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Template.Edit = false;
            Form_Template.intIDNganHang = 0;
        }
    }
}