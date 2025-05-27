using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemNVNghiViec : XtraForm
    {
        public Form_ThemNVNghiViec()
        {
            InitializeComponent();
            dteNgayNghi.EditValue = DateTime.Now.Date;
            dteNgayNopDon.EditValue = DateTime.Now.Date;
        }

        public void GetNhanVien()
        {
            var lstData = ExecSQL.ExecProcedureDataAsDataTable("sp_NhanVienselectall", new
            {
                option= Form_Template.intIDNhanVien > 0 ? 2 : 1
            });
            cboNhanVien.Properties.DataSource = lstData;
            cboNhanVien.Properties.DisplayMember = "TenNhanVien";
            cboNhanVien.Properties.ValueMember = "ID";
        }

        public void GetLoaiNghiViec()
        {
            var lstData = ExecSQL.ExecProcedureDataAsDataTable("sp_LoaiNghiViecSelect");
            cboLyDo.Properties.DataSource = lstData;
            cboLyDo.Properties.DisplayMember = "LoaiNghiViec";
            cboLyDo.Properties.ValueMember = "Id";
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
            if (cboLyDo.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập chọn lý do nghỉ việc", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLyDo.Focus();
                return;
            }
                      
            //Chỉnh sửa thông tin
            if (Form_Template.intIDNhanVien > 0)
            {
                var dataEdit = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_NhanVienNghiViecUpdate", new
                {
                    MaNV = Form_Template.intIDNhanVien,
                    NgayNopDon = Convert.ToDateTime(dteNgayNopDon.EditValue),
                    NgayNghi = Convert.ToDateTime(dteNgayNghi.EditValue),
                    LyDo = Convert.ToInt32(cboLyDo.EditValue),
                    GhiChu = txtGhiChu.Text,
                    NguoiSua = Form_Template.TaiKhoanLogin.ToUpper()
                });               
                if (dataEdit.errcode == 1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_nhanvien_nghiviec"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(dataEdit.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form_Template.intIDNhanVien = 0;
                    Close();
                }
                else
                {
                    XtraMessageBox.Show(dataEdit.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            else //Thêm mới chiếc khấu
            {              
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_NhanVienNghiViecInsert", new
                {                     
                    MaNV = Convert.ToInt32(cboNhanVien.EditValue),
                    NgayNopDon = Convert.ToDateTime(dteNgayNopDon.EditValue),
                    NgayNghi = Convert.ToDateTime(dteNgayNghi.EditValue),
                    LyDo = Convert.ToInt32(cboLyDo.EditValue),
                    GhiChu = txtGhiChu.Text,
                    NguoiTao = Form_Template.TaiKhoanLogin.ToUpper()
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
                        task = "them_nhanvien_nghiviec"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboNhanVien.EditValue = null;
                    cboLyDo.EditValue = null;
                    txtGhiChu.EditValue = null;
                    dteNgayNghi.EditValue = null;
                    dteNgayNopDon.EditValue = null;
                    cboNhanVien.Focus();
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
           
        }

        private void Form_ThemNVNghiViec_Load(object sender, EventArgs e)
        {
            GetNhanVien();
            GetLoaiNghiViec();
            if (Form_Template.Edit)
            {
                DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_NhanVienNghiViecSelectID", new
                {
                    MaNV = Form_Template.intIDNhanVien
                }) ;
                if (dt.Rows.Count > 0)
                {
                    cboNhanVien.EditValue = Convert.ToInt32(dt.Rows[0]["MaNV"]);
                    txtGhiChu.EditValue = dt.Rows[0]["GhiChu"];
                    dteNgayNopDon.EditValue = Convert.ToDateTime(dt.Rows[0]["NgayNopDon"]);
                    dteNgayNghi.EditValue = Convert.ToDateTime(dt.Rows[0]["NgayNghi"]);
                    cboLyDo.EditValue = Convert.ToInt32(dt.Rows[0]["LyDo"]);
                    cboNhanVien.Properties.ReadOnly = true;
                }
            }
        }

        private void Form_ThemNVNghiViec_KeyDown(object sender, KeyEventArgs e)
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

        private void Form_ThemNVNghiViec_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Template.intIDNhanVien = 0;
            Form_Template.Edit = false;
        }
    }
}