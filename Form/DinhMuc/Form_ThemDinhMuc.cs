using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemDinhMuc : XtraForm
    {
        public Form_ThemDinhMuc()
        {
            InitializeComponent();                
        }

        public void GetDonViTinh()
        {
            var lstDVT = ExecSQL.ExecProcedureDataAsDataTable("sp_DonViTinhSelect");
            cboDVT.Properties.DataSource = lstDVT;
            cboDVT.Properties.DisplayMember = "DonViTinh";
            cboDVT.Properties.ValueMember = "Id";
        }

        public void GetNguyenLieu()        
        {
           
            var lstSP = ExecSQL.ExecProcedureDataAsDataTable("sp_SanPhamOption", new
            {
                option = 2
            });
            cboTenNguyenLieu.Properties.DataSource = lstSP;
            cboTenNguyenLieu.Properties.DisplayMember = "TenSanPham";
            cboTenNguyenLieu.Properties.ValueMember = "ID";           
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
           
            if (cboTenNguyenLieu.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng chọn tên sản phẩm", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTenNguyenLieu.Focus();
                return;
            }
            //Chỉnh sửa thông tin
            if (Form_Template.intIDDinhMuc > 0)
            {
                var dataEdit = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_DinhMucUpdate", new
                {
                    Id = Form_Template.intIDDinhMuc,
                    soluong = Convert.ToDecimal(txtSoLuong.EditValue),   
                    madvt = Convert.ToInt32(cboDVT.EditValue),
                    ghichu = txtGhiChu.Text,                  
                    nguoisua = Form_Template.TaiKhoanLogin.ToUpper()
                });              
                if (dataEdit.errcode == 1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_dinhmuc"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(dataEdit.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form_Template.intIDDinhMuc = 0;
                    Close();
                }
                else
                {
                    XtraMessageBox.Show("Cập nhật chiếc khấu không thành công", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            else //Thêm mới chiếc khấu
            {              
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_DinhMucInsert", new
                {
                    masp = Form_Template.intIDSanPham,
                    manvl = Convert.ToInt32(cboTenNguyenLieu.EditValue),
                    soluong = Convert.ToDecimal(txtSoLuong.EditValue),                   
                    Ghichu = txtGhiChu.Text,
                    NguoiTao = Form_Template.TaiKhoanLogin.ToUpper(),
                    madvt = Convert.ToInt32(cboDVT.EditValue)
                });                
                if (data.errcode==1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_dinhmuc"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSoLuong.EditValue = 0;
                    cboTenNguyenLieu.EditValue = DBNull.Value;
                    txtGhiChu.Text = "";
                    cboTenNguyenLieu.Focus();
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
           
        }

        private void Form_ThemDinhMuc_Load(object sender, EventArgs e)
        {
            GetNguyenLieu();
            GetDonViTinh();
            //Load thông tin sản phẩm
            DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_SanPhamByID", new
            {
                id = Form_Template.intIDSanPham
            });
            if (dt.Rows.Count > 0)
            {
                txtTenSP.EditValue = dt.Rows[0]["TenSanPham"].ToString().ToUpper();

            }

            if (Form_Template.Edit)
            {
                DataTable dtDinhMuc = ExecSQL.ExecProcedureDataAsDataTable("sp_DinhMucSelectID", new
                {
                    id = Form_Template.intIDDinhMuc
                });
                if (dtDinhMuc.Rows.Count > 0)
                {
                    txtSoLuong.EditValue = dtDinhMuc.Rows[0]["SoLuong"];
                    cboDVT.EditValue = dtDinhMuc.Rows[0]["MaDVT"];
                    txtGhiChu.EditValue = dtDinhMuc.Rows[0]["GhiChu"];
                    cboTenNguyenLieu.EditValue = dtDinhMuc.Rows[0]["MaNVL"];
                    cboTenNguyenLieu.Properties.ReadOnly = true;
                }
            }            
        }

        private void Form_ThemDinhMuc_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Template.Edit = false;
            Form_Template.intIDDinhMuc = 0;
        }
    }
}