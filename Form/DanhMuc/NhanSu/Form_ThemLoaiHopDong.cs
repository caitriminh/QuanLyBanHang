using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemLoaiHopDong : XtraForm
    {
        public Form_ThemLoaiHopDong()
        {
            InitializeComponent();                
        }

      
        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtLoaiHopDong.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập loại hợp đồng", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLoaiHopDong.Focus();
                return;
            }
            //Chỉnh sửa thông tin
            if (Form_Template.intIDLoaiHopDong > 0)
            {
                var dataEdit = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_LoaiHopDong", new
                {
                    id = Form_Template.intIDLoaiHopDong,
                    ten = txtLoaiHopDong.Text,                    
                    nguoicapnhat = Form_Template.TaiKhoanLogin.ToUpper()
                });                
                if (dataEdit.errcode == 1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_loaihopdong"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(dataEdit.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form_Template.intIDLoaiHopDong = 0;
                    Close();
                }
                else
                {
                    XtraMessageBox.Show(dataEdit.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            else //Thêm mới chiếc khấu
            {              
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_LoaiHopDong", new
                {
                    id = 0,
                    ten = txtLoaiHopDong.Text,                  
                    nguoicapnhat = Form_Template.TaiKhoanLogin.ToUpper()
                });               
                if (data.errcode==1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_loaihopdong"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtLoaiHopDong.Text = ""; 
                    txtLoaiHopDong.Focus();
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
           
        }

        private void Form_ThemLoaiHopDong_Load(object sender, EventArgs e)
        {
            if (Form_Template.Edit)
            {
                DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_LoaiHopDongSelectByID", new
                {
                    ID = Form_Template.intIDLoaiHopDong
                });
                if (dt.Rows.Count > 0)
                {
                    txtLoaiHopDong.EditValue = dt.Rows[0]["LoaiHopDong"];
                }
            }
        }

        private void Form_ThemLoaiHopDong_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Template.Edit = false;
            Form_Template.intIDLoaiHopDong = 0;
        }

        private void Form_ThemLoaiHopDong_KeyDown(object sender, KeyEventArgs e)
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