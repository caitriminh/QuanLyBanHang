using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemLoaiNghiPhep : XtraForm
    {
        public Form_ThemLoaiNghiPhep()
        {
            InitializeComponent();                
        }

      
        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txt_LoaiNghiPhep.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập tên loại nghỉ phép", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_LoaiNghiPhep.Focus();
                return;
            }
            //Chỉnh sửa thông tin
            if (Form_Template.intIDLoaiNghiPhep > 0)
            {
                var dataEdit = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_LoaiNghiPhep", new
                {
                    id = Form_Template.intIDLoaiNghiPhep,
                    ten = txt_LoaiNghiPhep.Text,                    
                    nguoicapnhat = Form_Template.TaiKhoanLogin.ToUpper()
                });                
                if (dataEdit.errcode == 1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_loainghiphep"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(dataEdit.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form_Template.intIDLoaiNghiPhep = 0;
                    Close();
                }
                else
                {
                    XtraMessageBox.Show(dataEdit.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            else //Thêm mới chiếc khấu
            {              
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_LoaiNghiPhep", new
                {
                    id = 0,
                    ten = txt_LoaiNghiPhep.Text,                  
                    nguoicapnhat = Form_Template.TaiKhoanLogin.ToUpper()
                });               
                if (data.errcode==1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_loainghiphep"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txt_LoaiNghiPhep.Text = ""; 
                    txt_LoaiNghiPhep.Focus();
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
           
        }

        private void Form_ThemLoaiNghiPhep_Load(object sender, EventArgs e)
        {
            if (Form_Template.Edit)
            {
                DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_LoaiNghiPhepSelectByID", new
                {
                    ID = Form_Template.intIDLoaiNghiPhep
                });
                if (dt.Rows.Count > 0)
                {
                    txt_LoaiNghiPhep.EditValue = dt.Rows[0]["LoaiNghiPhep"];
                }
            }
        }

        private void Form_ThemLoaiNghiPhep_KeyDown(object sender, KeyEventArgs e)
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

        private void Form_ThemLoaiNghiPhep_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Template.Edit = false;
            Form_Template.intIDLoaiNghiPhep = 0;
        }
    }
}