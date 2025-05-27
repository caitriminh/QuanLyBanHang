using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemSanPham : XtraForm
    {
        public Form_ThemSanPham()
        {
            InitializeComponent();                
        }

      
        public void GetLoaiSP()
        {
            var lstLoaiSP = ExecSQL.ExecProcedureDataAsDataTable("sp_LoaiSPSelect");
            cboLoaiSanPham.Properties.DataSource = lstLoaiSP;
            cboLoaiSanPham.Properties.DisplayMember = "TenLoai";
            cboLoaiSanPham.Properties.ValueMember = "ID";
            cboLoaiSanPham.EditValue = 1;
        }

        public void GetDonViTinh()
        {
            var lstDVT = ExecSQL.ExecProcedureDataAsDataTable("sp_DonViTinhSelect");
            cboDVT.Properties.DataSource = lstDVT;
            cboDVT.Properties.DisplayMember = "DonViTinh";
            cboDVT.Properties.ValueMember = "Id";
        }

        public void GetLoaiHang()
        {
            var lstLoaiHang = ExecSQL.ExecQueryDataAsDataTable("SELECT ID, LoaiHang FROM dbo.LoaiHang");
            cboLoaiHang.Properties.DataSource = lstLoaiHang;
            cboLoaiHang.Properties.DisplayMember = "LoaiHang";
            cboLoaiHang.Properties.ValueMember = "ID";
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {            
            if (txtTenSP.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng chọn tên sản phẩm", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenSP.Focus();
                return;
            }
            if (cboLoaiSanPham.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng chọn loại sản phẩm", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLoaiSanPham.Focus();
                return;
            }
            if (cboDVT.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng chọn tên đơn vị tính", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenSP.Focus();
                return;
            }
            if (cboLoaiHang.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng chọn loại hàng", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLoaiHang.Focus();
                return;
            }
            //Chỉnh sửa thông tin
            if (Form_Template.intIDSanPham > 0)
            {
                var dataEdit = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_SanPhamUpdate", new
                {
                    ID = Form_Template.intIDSanPham,
                    TenSanPham = txtTenSP.Text,
                    LoaiSanPham = Convert.ToInt32(cboLoaiSanPham.EditValue),
                    MaDVT = Convert.ToInt32(cboDVT.EditValue),
                    GhiChu = txtGhiChu.Text,
                    NguoiSua = Form_Template.TaiKhoanLogin.ToUpper(),
                    QuyCach = txtQuyCach.Text,
                    Status = Convert.ToBoolean(chkStatus.EditValue),
                    IDLoaiHang = Convert.ToInt32(cboLoaiHang.EditValue)
                }) ;                
                if (dataEdit.errcode == 1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_sanpham"
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
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_SanPhamInsert", new
                {
                    MaSP = txtMaSP.Text,
                    TenSanPham = txtTenSP.Text,
                    LoaiSanPham = Convert.ToInt32(cboLoaiSanPham.EditValue),  
                    MaDVT = Convert.ToInt32(cboDVT.EditValue),
                    GhiChu = txtGhiChu.Text,
                    NguoiTao=Form_Template.TaiKhoanLogin.ToUpper(),
                    QuyCach = txtQuyCach.Text,
                    IDLoaiHang = Convert.ToInt32(cboLoaiHang.EditValue)
                });
                if (data == null)
                {
                    XtraMessageBox.Show("Thêm mới sản phẩm không thành công", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (data.errcode==1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_sanpham"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTenSP.Text = "";
                    cboDVT.EditValue = null;
                    cboLoaiSanPham.EditValue = null;
                    txtGhiChu.Text = "";
                    cboLoaiHang.EditValue = null;
                    txtTenSP.Focus();
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
           
        }

        private void Form_ThemSanPham_Load(object sender, EventArgs e)
        {
            GetDonViTinh();
            GetLoaiSP();
            GetLoaiHang();
            if (Form_Template.Edit)
            {
                DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_SanPhamByID", new
                {
                    ID = Form_Template.intIDSanPham
                });
                if (dt.Rows.Count > 0)
                {
                    txtMaSP.EditValue = dt.Rows[0]["MaSP"];
                    txtTenSP.EditValue = dt.Rows[0]["TenSanPham"];                   
                    cboLoaiSanPham.EditValue =Convert.ToInt32(dt.Rows[0]["LoaiSanPham"]);
                    txtGhiChu.EditValue = dt.Rows[0]["GhiChu"];  
                    cboDVT.EditValue =Convert.ToInt32(dt.Rows[0]["MaDVT"]);
                    txtQuyCach.EditValue = dt.Rows[0]["QuyCach"];
                    chkStatus.EditValue = Convert.ToBoolean(dt.Rows[0]["Status"]);
                    cboLoaiHang.EditValue = Convert.ToInt32(dt.Rows[0]["IDLoaiHang"]);
                    txtMaSP.Properties.ReadOnly = true;
                }
            }
            else
            {
               
            }
        }

        private void Form_ThemSanPham_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Template.Edit = false;
            Form_Template.intIDSanPham = 0;
        }

        private void cboLoaiSanPham_EditValueChanged(object sender, EventArgs e)
        {
            if (Form_Template.Edit) { return; }
            if (cboLoaiSanPham.EditValue == null) { return; }
            var data = ExecSQL.ExecProcedureSacalar("spGetMaTuDong", new 
            { 
                option = 5, 
                LoaiSanPham = Convert.ToInt32(cboLoaiSanPham.EditValue)
            });
            txtMaSP.Text = data.ToString();
        }

        private void Form_ThemSanPham_KeyDown(object sender, KeyEventArgs e)
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