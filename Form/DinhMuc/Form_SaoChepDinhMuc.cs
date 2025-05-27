using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_SaoChepDinhMuc : XtraForm
    {
        public Form_SaoChepDinhMuc()
        {
            InitializeComponent();                
        }


        public void GetSanPham()        
        {
           
            var lstSP = ExecSQL.ExecProcedureDataAsDataTable("sp_SanPhamOption", new
            {
                option = 1
            });
            cboSanPham.Properties.DataSource = lstSP;
            cboSanPham.Properties.DisplayMember = "TenSanPham";
            cboSanPham.Properties.ValueMember = "ID";           
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
           
            if (cboSanPham.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng chọn tên sản phẩm", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboSanPham.Focus();
                return;
            }
            var dataEdit = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_DinhMucCopy", new
            {
                maspmoi = Convert.ToInt32(cboSanPham.EditValue),
                maspcu = Form_Template.intIDSanPham,
                nguoitao = Form_Template.TaiKhoanLogin.ToUpper()
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
                Close();
            }
            else
            {
                XtraMessageBox.Show(dataEdit.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form_SaoChepDinhMuc_Load(object sender, EventArgs e)
        {
            GetSanPham();
            //Load thông tin sản phẩm
            DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_SanPhamByID", new
            {
                id = Form_Template.intIDSanPham
            });
            if (dt.Rows.Count > 0)
            {
                txtTenSP.EditValue = dt.Rows[0]["TenSanPham"].ToString().ToUpper();

            }
        }
    }
}