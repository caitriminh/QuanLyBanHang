using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemTrinhDo : XtraForm
    {
        public Form_ThemTrinhDo()
        {
            InitializeComponent();                
        }

      
        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtTrinhDo.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập tên trình độ", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTrinhDo.Focus();
                return;
            }
            //Chỉnh sửa thông tin
            if (Form_Template.intIDTrinhDo > 0)
            {
                var dataEdit = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_TrinhDo", new
                {
                    id = Form_Template.intIDTinhThanh,
                    ten = txtTrinhDo.Text,                    
                    nguoicapnhat = Form_Template.TaiKhoanLogin.ToUpper()
                });                
                if (dataEdit.errcode == 1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_trinhdo"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(dataEdit.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form_Template.intIDTrinhDo = 0;
                    Close();
                }
                else
                {
                    XtraMessageBox.Show(dataEdit.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            else //Thêm mới chiếc khấu
            {              
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_TrinhDo", new
                {
                    id = 0,
                    ten = txtTrinhDo.Text,                  
                    nguoicapnhat = Form_Template.TaiKhoanLogin.ToUpper()
                });               
                if (data.errcode==1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_trinhdo"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTrinhDo.Text = ""; 
                    txtTrinhDo.Focus();
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
           
        }

        private void Form_ThemTrinhDo_Load(object sender, EventArgs e)
        {
            if (Form_Template.Edit)
            {
                DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_TrinhDoSelectByID", new
                {
                    ID = Form_Template.intIDTrinhDo
                });
                if (dt.Rows.Count > 0)
                {
                    txtTrinhDo.EditValue = dt.Rows[0]["TrinhDo"];
                }
            }
        }

        private void Form_ThemTrinhDo_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Template.Edit = false;
            Form_Template.intIDTrinhDo = 0;
        }

        private void Form_ThemTrinhDo_KeyDown(object sender, KeyEventArgs e)
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