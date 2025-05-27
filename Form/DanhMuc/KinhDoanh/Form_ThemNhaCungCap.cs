using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemNhaCungCap : XtraForm
    {
        public Form_ThemNhaCungCap()
        {
            InitializeComponent();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtTenNCC.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập vào tên nhà cung cấp.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }           
            if (Form_Template.intIDMaNCC > 0) //Update khách hàng
            {
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_NhaCungCapUpdate", new
                {
                    ID = Form_Template.intIDMaNCC,
                    TenNhaCungCap = txtTenNCC.Text,
                    DiaChi = Txtdiachi.Text,
                    DienThoai1 = Txtdienthoai1.Text,
                    DienThoai2 = Txtdienthoai2.Text,
                    Email = Txtemail.Text,
                    MaSoThue = Txtmasothue.Text,                   
                    Ghichu = Txtghichu.Text,
                    NguoiSua = Form_Template.TaiKhoanLogin.ToUpper()
                });
                if (data.errcode == 1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_ncc"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else //Thêm mới nhà cung cấp
            {
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_NhaCungCapInsert", new
                {
                    TenNhaCungCap = txtTenNCC.Text,
                    DiaChi = Txtdiachi.Text,
                    DienThoai1 = Txtdienthoai1.Text,
                    DienThoai2 = Txtdienthoai2.Text,
                    Email = Txtemail.Text,
                    MaSoThue = Txtmasothue.Text,
                    Ghichu = Txtghichu.Text,
                    NguoiTao = Form_Template.TaiKhoanLogin.ToUpper()
                }) ;
                if (data.errcode == 1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_ncc"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);                 
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            
        }

        private void Form_ThemNhaCungCap_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Template.intIDMaNCC = 0;
            Form_Template.Edit = false;
        }

      
        private void Form_ThemNhaCungCap_Load(object sender, EventArgs e)
        {
            if (Form_Template.Edit)
            {
                DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_NhaCungCapByID", new
                {
                    id = Form_Template.intIDMaNCC
                });
                if (dt.Rows.Count > 0)
                {
                    txtTenNCC.EditValue = dt.Rows[0]["TenNhaCungCap"];
                    Txtdiachi.EditValue = dt.Rows[0]["DiaChi"];
                    Txtdienthoai1.EditValue = dt.Rows[0]["DienThoai1"];
                    Txtdienthoai2.EditValue = dt.Rows[0]["DienThoai2"];
                    Txtemail.EditValue = dt.Rows[0]["Email"];
                    Txtghichu.EditValue = dt.Rows[0]["Ghichu"];
                    Txtmasothue.EditValue = dt.Rows[0]["MaSoThue"];
                }
            }
        }

        private void Form_ThemNhaCungCap_KeyDown(object sender, KeyEventArgs e)
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