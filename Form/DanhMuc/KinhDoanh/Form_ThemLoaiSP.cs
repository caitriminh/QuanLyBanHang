using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemLoaiSP : XtraForm
    {
        public Form_ThemLoaiSP()
        {
            InitializeComponent();                
        }

        public void GetNhomSanPham()
        {
            var data = ExecSQL.ExecProcedureDataAsDataTable("sp_NhomSanPhamSelect");
            cboNhomSP.Properties.DataSource= data;
            cboNhomSP.Properties.DisplayMember = "NhomSanPham";
            cboNhomSP.Properties.ValueMember = "Id";
        }
      
        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtLoaiSP.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập tên loại sản phẩm", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLoaiSP.Focus();
                return;
            }
            //Chỉnh sửa thông tin
            if (Form_Template.intIDLoaiSP > 0)
            {
                var dataEdit = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_LoaiSanPham", new
                {
                    id = Form_Template.intIDLoaiSP,
                    ten = txtLoaiSP.Text,
                    nhom = Convert.ToInt32(cboNhomSP.EditValue),
                    nguoicapnhat = Form_Template.TaiKhoanLogin.ToUpper()
                });
                if (dataEdit== null) 
                {
                    XtraMessageBox.Show("Cập nhật loại sản phẩm không thành công", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; 
                }
                if (dataEdit.errcode == 1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_loaisanpham"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(dataEdit.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form_Template.intIDSanPham = 0;
                    Close();
                }
                else
                {
                    XtraMessageBox.Show("Cập nhật chiếc khấu không thành công", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            else //Thêm mới chiếc khấu
            {              
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_LoaiSanPham", new
                {
                    id = 0,
                    ten = txtLoaiSP.Text,           
                    nhom = Convert.ToInt32(cboNhomSP.EditValue),
                    nguoicapnhat = Form_Template.TaiKhoanLogin.ToUpper()
                });
                if (data == null)
                {
                    XtraMessageBox.Show("Thêm mới loại sản phẩm không thành công", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (data.errcode==1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_loaisanpham"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtLoaiSP.Text = "";
                    txtLoaiSP.Focus();
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
           
        }

        private void Form_ThemSanPham_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Template.Edit = false;
            Form_Template.intIDLoaiSP = 0;
        }

        private void Form_ThemLoaiSP_Load(object sender, EventArgs e)
        {
            GetNhomSanPham();
            if (Form_Template.Edit)
            {
                DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_LoaiSanPhamSelectByID", new
                {
                    ID = Form_Template.intIDLoaiSP
                });
                if (dt.Rows.Count > 0)
                {
                    txtLoaiSP.EditValue = dt.Rows[0]["TenLoai"];    
                    cboNhomSP.EditValue = dt.Rows[0]["NhomSanPham"];
                }
            }
        }

        private void Form_ThemLoaiSP_KeyDown(object sender, KeyEventArgs e)
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