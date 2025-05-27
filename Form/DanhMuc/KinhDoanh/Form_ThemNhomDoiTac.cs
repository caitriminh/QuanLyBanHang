using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemNhomDoiTac : XtraForm
    {
        public Form_ThemNhomDoiTac()
        {
            InitializeComponent();                
        }

      
        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtNhomKhachHang.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập tên nhóm khách hàng", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNhomKhachHang.Focus();
                return;
            }
            //Chỉnh sửa thông tin
            if (Form_Template.intIDNhomKhachhang > 0)
            {
                var dataEdit = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_NhomKhachHang", new
                {
                    id = Form_Template.intIDNhomKhachhang,
                    ten = txtNhomKhachHang.Text,                   
                    nguoicapnhat = Form_Template.TaiKhoanLogin.ToUpper()
                });
                if (dataEdit== null) 
                {
                    XtraMessageBox.Show("Cập nhật nhóm khách hàng không thành công", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; 
                }
                if (dataEdit.errcode == 1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_nhomkhachhang"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(dataEdit.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form_Template.intIDNhomKhachhang = 0;
                    Close();
                }
                else
                {
                    XtraMessageBox.Show("Cập nhật chiếc khấu không thành công", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            else //Thêm mới chiếc khấu
            {              
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_NhomKhachHang", new
                {
                    id = 0,
                    ten = txtNhomKhachHang.Text,                   
                    nguoicapnhat = Form_Template.TaiKhoanLogin.ToUpper()
                });
                if (data == null)
                {
                    XtraMessageBox.Show("Thêm mới nhóm khách hàng không thành công", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (data.errcode==1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_nhomkhachhang"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNhomKhachHang.Text = "";  
                    txtNhomKhachHang.Focus();
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
           
        }

        private void Form_ThemNhomDoiTac_Load(object sender, EventArgs e)
        {
            if (Form_Template.Edit)
            {
                DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_NhomKhachHangSelectByID", new
                {
                    ID = Form_Template.intIDNhomKhachhang
                });
                if (dt.Rows.Count > 0)
                {
                    txtNhomKhachHang.EditValue = dt.Rows[0]["TenNhom"];                   
                }
            }
        }

        private void Form_ThemNhomDoiTac_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Template.Edit = false;
            Form_Template.intIDNhomKhachhang = 0;
        }

        private void Form_ThemNhomDoiTac_KeyDown(object sender, KeyEventArgs e)
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