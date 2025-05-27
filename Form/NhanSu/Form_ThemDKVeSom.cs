using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemDKVeSom : XtraForm
    {
        public Form_ThemDKVeSom()
        {
            InitializeComponent();
            dteNgayDangKy.EditValue = DateTime.Now.Date;
            txtTuGio.EditValue = DateTime.Now.Date;           
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

        public void GetLoaiNghiPhep()
        {
            var lstData = ExecSQL.ExecProcedureDataAsDataTable("sp_LoaiNghiPhepSelect");
            cboLoaiNghiPhep.Properties.DataSource = lstData;
            cboLoaiNghiPhep.Properties.DisplayMember = "LoaiNghiPhep";
            cboLoaiNghiPhep.Properties.ValueMember = "Id";
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
            if (dteNgayDangKy.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập ngày đăng ký", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dteNgayDangKy.Focus();
                return;
            }
            if (txtTuGio.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập giờ về", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTuGio.Focus();
                return;
            }           
            if (cboLoaiNghiPhep.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập chọn lý do", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLoaiNghiPhep.Focus();
                return;
            }
            string TuGio = Convert.ToDateTime(dteNgayDangKy.EditValue).ToString("yyyyMMdd") + " " + Convert.ToDateTime(txtTuGio.EditValue).ToString("HH:mm");
            //Chỉnh sửa thông tin
            if (Form_Template.intIDNghiPhep_VS_CT > 0)
            {
                var dataEdit = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_VeSomUpdate", new
                {
                    Id = Form_Template.intIDNghiPhep_VS_CT,                   
                    NgayDangKy = Convert.ToDateTime(dteNgayDangKy.EditValue),
                    TuGio,
                    LyDo = Convert.ToInt32(cboLoaiNghiPhep.EditValue),
                    GhiChu = txtGhiChu.Text,
                    NguoiSua = Form_Template.TaiKhoanLogin.ToUpper()
                });               
                if (dataEdit.errcode == 1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_vesom"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(dataEdit.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form_Template.intIDNghiPhep_VS_CT = 0;
                    Close();
                }
                else
                {
                    XtraMessageBox.Show(dataEdit.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            else //Thêm mới chiếc khấu
            {              
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_VeSomInsert", new
                {                     
                    MaNV = Convert.ToInt32(cboNhanVien.EditValue),
                    NgayDangKy = Convert.ToDateTime(dteNgayDangKy.EditValue),
                    TuGio,                  
                    LyDo = Convert.ToInt32(cboLoaiNghiPhep.EditValue),
                    GhiChu = txtGhiChu.Text,
                    NguoiTao = Form_Template.TaiKhoanLogin.ToUpper()
                });               
                if (data.errcode==1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_vesom"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboNhanVien.EditValue = null;                  
                    cboLoaiNghiPhep.EditValue = null;                   
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

        private void Form_ThemDKNghiPhep_Load(object sender, EventArgs e)
        {
            GetNhanVien();
            GetLoaiNghiPhep();
            if (Form_Template.Edit)
            {
                DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_NghiPhepVeSomCongTacSelectID", new
                {
                    Id = Form_Template.intIDNghiPhep_VS_CT
                });
                if (dt.Rows.Count > 0)
                {
                    cboNhanVien.EditValue = Convert.ToInt32(dt.Rows[0]["MaNV"]);
                    txtGhiChu.EditValue = dt.Rows[0]["GhiChu"];  
                    dteNgayDangKy.EditValue = dt.Rows[0]["NgayDangKy"];
                    cboLoaiNghiPhep.EditValue = Convert.ToInt32(dt.Rows[0]["LyDo"]);
                    txtTuGio.EditValue = dt.Rows[0]["TuGio"];
                    cboNhanVien.Properties.ReadOnly = true;
                }
            }
        }

        private void Form_ThemDKNghiPhep_KeyDown(object sender, KeyEventArgs e)
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

        private void Form_ThemDKNghiPhep_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Template.intIDNghiPhep_VS_CT = 0;
            Form_Template.Edit = false;
        }
    }
}