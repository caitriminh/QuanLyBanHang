using DevExpress.DataProcessing.InMemoryDataProcessor;
using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemGiaBan : XtraForm
    {
        public Form_ThemGiaBan()
        {
            InitializeComponent();
            dteNgayApDung.EditValue = DateTime.Now.Date;           
        }

        public void GetTenSP()        {
           
            var lstSP = ExecSQL.ExecProcedureDataAsDataTable("sp_SanPhamSelect");
            cboTenSP.Properties.DataSource = lstSP;
            cboTenSP.Properties.DisplayMember = "TenSanPham";
            cboTenSP.Properties.ValueMember = "ID";           
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cboTenSP.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng chọn tên sản phẩm", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTenSP.Focus();
                return;
            }
            //Chỉnh sửa thông tin
            if (Form_Template.intIDGiaBan > 0)
            {
                var dataEdit = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_GiaBanUpdate", new
                {
                    ID = Form_Template.intIDGiaBan,
                    GiaBan = Convert.ToDecimal(txtGiaTruocThue.EditValue),
                    VAT = Convert.ToInt32(txtVAT.EditValue),
                    GiaSauThue = Convert.ToDecimal(txtGiaSauThue.EditValue),
                    Ghichu = txtGhiChu.Text,
                    NguoiSua = Form_Template.TaiKhoanLogin.ToUpper()
                });
                if (dataEdit.errcode==1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_giaban"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(dataEdit.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form_Template.intIDGiaBan = 0;
                    Close();
                }
                else
                {
                    XtraMessageBox.Show("Cập nhật giá bán không thành công", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else //Thêm mới giá bán
            {
                //var result = Convert.ToDateTime(dteNgayHetHan.EditValue) == DateTime.MinValue ? (object)DBNull.Value : dteNgayHetHan.EditValue;

              
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_GiaBanInsert", new
                {
                    MaSP = Convert.ToInt32(cboTenSP.EditValue),
                    GiaBan = Convert.ToDecimal(txtGiaTruocThue.EditValue),
                    NgayApDung = Convert.ToDateTime(dteNgayApDung.EditValue),  
                    VAT = Convert.ToInt32(txtVAT.EditValue),
                    GiaSauThue = Convert.ToDecimal(txtGiaSauThue.EditValue),
                    Ghichu = txtGhiChu.Text,
                    NguoiTao = Form_Template.TaiKhoanLogin.ToUpper()
                });
                if (data.errcode == 1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_giaban"
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

        private void Form_ThemGiaBan_Load(object sender, EventArgs e)
        {
            GetTenSP();
            if (Form_Template.Edit)
            {
                DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_GiaBanSelectByID", new
                {
                    ID = Form_Template.intIDGiaBan
                });
                if (dt.Rows.Count > 0)
                {
                    cboTenSP.EditValue = dt.Rows[0]["MaSP"];
                    txtGiaTruocThue.EditValue = dt.Rows[0]["GiaBan"];
                    dteNgayApDung.EditValue = dt.Rows[0]["NgayApDung"];
                    txtVAT.EditValue = dt.Rows[0]["VAT"];
                    txtGiaSauThue.EditValue = dt.Rows[0]["GiaSauThue"];
                    txtGhiChu.EditValue= dt.Rows[0]["GhiChu"];
                    dteNgayApDung.Properties.ReadOnly = true;                  
                    cboTenSP.Properties.ReadOnly = true;
                }
            }
        }

        private void Form_ThemGiaBan_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Template.Edit = false;
            Form_Template.intIDGiaBan = 0;
        }

        private void txtVAT_EditValueChanged(object sender, EventArgs e)
        {
            decimal vat = (Convert.ToDecimal(txtVAT.EditValue) / 100 + 1);
            decimal GiaSauThue = vat * Convert.ToDecimal(txtGiaTruocThue.EditValue);
            txtGiaSauThue.EditValue= GiaSauThue;
        }

        private void txtGiaTruocThue_EditValueChanged(object sender, EventArgs e)
        {
            decimal vat = (Convert.ToDecimal(txtVAT.EditValue) / 100 + 1);
            decimal GiaSauThue = vat * Convert.ToDecimal(txtGiaTruocThue.EditValue);
            txtGiaSauThue.EditValue = GiaSauThue;
        }
    }
}