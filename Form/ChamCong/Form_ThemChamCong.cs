using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemChamCong : XtraForm
    {
        public Form_ThemChamCong()
        {
            InitializeComponent();
            dteNgayChamCong.EditValue = DateTime.Now.Date;
        }

        public void GetNhanVien()
        {
            var lstData = ExecSQL.ExecProcedureDataAsDataTable("sp_NhanVienselectall", new
            {
                option = 1
            }) ;
            cboNhanVien.Properties.DataSource = lstData;
            cboNhanVien.Properties.DisplayMember = "TenNhanVien";
            cboNhanVien.Properties.ValueMember = "ID";
        }
      
        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cboNhanVien.EditValue ==null)
            {
                XtraMessageBox.Show("Vui lòng nhập chọn tên nhân viên", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNhanVien.Focus();
                return;
            }
            if (dteNgayChamCong.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập ngày chấm công", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dteNgayChamCong.Focus();
                return;
            }
           
            //Chỉnh sửa thông tin
            if (Form_Template.intIDChamCong > 0)
            {
                var dataEdit = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_ChamCongCaUpdate", new
                {
                    Id = Form_Template.intIDChamCong,
                    NgayChamCong = Convert.ToDateTime(dteNgayChamCong.EditValue).ToString("yyyyMMdd HH:mm"),
                    GhiChu = txtGhiChu.Text,
                    NguoiSua = Form_Template.TaiKhoanLogin.ToUpper()
                });               
                if (dataEdit.errcode == 1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_chamcong"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(dataEdit.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form_Template.intIDChamCong = 0;
                    Close();
                }
                else
                {
                    XtraMessageBox.Show(dataEdit.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            else //Thêm mới chiếc khấu
            {              
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_ChamCongCaInsert", new
                {                     
                    MaNV = Convert.ToInt32(cboNhanVien.EditValue),
                    NgayChamCong = Convert.ToDateTime(dteNgayChamCong.EditValue).ToString("yyyyMMdd HH:mm"),                    
                    GhiChu = txtGhiChu.Text,
                    NguoiTao = Form_Template.TaiKhoanLogin.ToUpper()
                });               
                if (data.errcode==1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_chamcong"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboNhanVien.EditValue = null;
                    txtGhiChu.EditValue = null;  
                    cboNhanVien.Focus();
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
           
        }

        private void Form_ThemChamCong_Load(object sender, EventArgs e)
        {
            GetNhanVien();
            if (Form_Template.Edit)
            {
                DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_ChamCongSelectID", new
                {
                    Id = Form_Template.intIDChamCong
                });
                if (dt.Rows.Count > 0)
                {
                    cboNhanVien.EditValue = Convert.ToInt32(dt.Rows[0]["MaNV"]);
                    txtGhiChu.EditValue = dt.Rows[0]["GhiChu"];
                    dteNgayChamCong.EditValue = dt.Rows[0]["NgayChamCong"];
                    cboNhanVien.Properties.ReadOnly = true;
                }
            }
        }

        private void Form_ThemChamCong_KeyDown(object sender, KeyEventArgs e)
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

        private void Form_ThemChamCong_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Template.intIDChamCong = 0;
            Form_Template.Edit = false;
        }
    }
}