using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemChiecKhau : XtraForm
    {
        public Form_ThemChiecKhau()
        {
            InitializeComponent();                
        }

        public void GetTenSP()        {
           
            var lstSP = ExecSQL.ExecProcedureDataAsDataTable("sp_SanPhamSelect");
            cboTenSP.Properties.DataSource = lstSP;
            cboTenSP.Properties.DisplayMember = "TenSanPham";
            cboTenSP.Properties.ValueMember = "ID";           
        }

        public void GetTenKhachHang()
        {
            var lstKH = ExecSQL.ExecProcedureDataAsDataTable("sp_KhachHangSelect");
            cboKhachHang.Properties.DataSource = lstKH;
            cboKhachHang.Properties.DisplayMember = "TenKhachHang";
            cboKhachHang.Properties.ValueMember = "ID";
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cboKhachHang.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng chọn khách hàng", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKhachHang.Focus();
                return;
            }

            if (cboTenSP.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng chọn tên sản phẩm", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTenSP.Focus();
                return;
            }
            //Chỉnh sửa thông tin
            if (Form_Template.intIDChiecKhau > 0)
            {
                var dataEdit = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_ChiecKhauUpdate", new
                {
                    ID = Form_Template.intIDChiecKhau,
                    ChiecKhau = Convert.ToDecimal(txtChiecKhau.EditValue),                  
                    Ghichu = txtGhiChu.Text,
                    NguoiSua = Form_Template.TaiKhoanLogin.ToUpper()
                });
                if (dataEdit== null) 
                {
                    XtraMessageBox.Show("Cập nhật chiếc khấu không thành công", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; 
                }
                if (dataEdit.errcode == 1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_chieckhau"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(dataEdit.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form_Template.intIDChiecKhau = 0;
                    Close();
                }
                else
                {
                    XtraMessageBox.Show("Cập nhật chiếc khấu không thành công", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            else //Thêm mới chiếc khấu
            {              
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_ChiecKhauInsert", new
                {
                    MaSP = Convert.ToInt32(cboTenSP.EditValue),
                    ChiecKhau = Convert.ToDecimal(txtChiecKhau.EditValue),
                    MaKH = Convert.ToInt32(cboKhachHang.EditValue),                   
                    Ghichu = txtGhiChu.Text,
                    NguoiTao = Form_Template.TaiKhoanLogin.ToUpper()
                });
                if (data == null)
                {
                    XtraMessageBox.Show("Thêm mới chiếc khấu không thành công", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (data.errcode==1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_chieckhau"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtChiecKhau.EditValue = 0;
                    cboTenSP.EditValue = DBNull.Value;
                    txtGhiChu.Text = "";
                    cboTenSP.Focus();
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
           
        }

        private void Form_ThemChiecKhau_Load(object sender, EventArgs e)
        {
            GetTenSP();
            GetTenKhachHang();
            if (Form_Template.Edit)
            {
                DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_ChiecKhauSelectByID", new
                {
                    ID = Form_Template.intIDChiecKhau
                });
                if (dt.Rows.Count > 0)
                {
                    cboTenSP.EditValue = dt.Rows[0]["MaSP"];
                    txtChiecKhau.EditValue = dt.Rows[0]["ChiecKhau"];
                    cboKhachHang.EditValue = dt.Rows[0]["MaKH"];                   
                    txtGhiChu.EditValue = dt.Rows[0]["GhiChu"];
                    cboKhachHang.Properties.ReadOnly = true;
                    cboTenSP.Properties.ReadOnly = true;
                }
            }
        }

        private void Form_ThemChiecKhau_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Template.Edit = false;
            Form_Template.intIDChiecKhau = 0;
        }
    }
}