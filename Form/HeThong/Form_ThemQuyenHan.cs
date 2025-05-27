using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemQuyenHan : XtraForm
    {
        public Form_ThemQuyenHan()
        {
            InitializeComponent();                
        }

      
        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtQuyenHan.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập tên nhóm quyền hạn", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuyenHan.Focus();
                return;
            }
           
            //Chỉnh sửa thông tin
            if (Form_Template.intIDNhomQuyen > 0)
            {
                var dataEdit = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_NhomQuyenUpdate", new
                {
                    Id = Form_Template.intIDNhomQuyen,
                    NhomQuyen = txtQuyenHan.Text,
                    NguoiSua = Form_Template.TaiKhoanLogin.ToUpper()
                });               
                if (dataEdit.errcode == 1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_nhomquyen"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(dataEdit.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form_Template.intIDNhomQuyen = 0;
                    Close();
                }
                else
                {
                    XtraMessageBox.Show(dataEdit.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }                
            }
            else
            {
                var dataEdit = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_NhomQuyenInsert", new
                {
                    NhomQuyen = txtQuyenHan.Text,                   
                    NguoiTao = Form_Template.TaiKhoanLogin.ToUpper()
                });
                if (dataEdit.errcode == 1)
                {
                    txtQuyenHan.Text = "";
                    txtQuyenHan.Focus();
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_nhomquyen"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(dataEdit.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);                  
                    Close();
                }
                else
                {
                    XtraMessageBox.Show(dataEdit.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Form_ThemQuyenHan_Load(object sender, EventArgs e)
        {
            if (Form_Template.Edit)
            {
                DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_NhomQuyenSelectById", new
                {
                    Id = Form_Template.intIDNhomQuyen
                });
                if (dt.Rows.Count > 0)
                {
                    txtQuyenHan.EditValue = dt.Rows[0]["NhomQuyen"];
                }
            }
        }
    }
}