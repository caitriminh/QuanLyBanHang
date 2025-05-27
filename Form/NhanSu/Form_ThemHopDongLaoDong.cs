using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemHopDongLaoDong : XtraForm
    {
        public Form_ThemHopDongLaoDong()
        {
            InitializeComponent();
            dteNgayHetHan.EditValue = DateTime.Now.Date;
            dteNgayKy.EditValue = DateTime.Now.Date;
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

        public void GetLoaiHopDong()
        {
            var lstData = ExecSQL.ExecProcedureDataAsDataTable("sp_LoaiHopDongSelect");
            cboLoaiHopDong.Properties.DataSource = lstData;
            cboLoaiHopDong.Properties.DisplayMember = "LoaiHopDong";
            cboLoaiHopDong.Properties.ValueMember = "Id";
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaHD.Text))
            {
                XtraMessageBox.Show("Vui lòng nhập mã số hợp đồng", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaHD.Focus();
                return;
            }

            if (cboNhanVien.EditValue ==null)
            {
                XtraMessageBox.Show("Vui lòng nhập chọn tên nhân viên", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNhanVien.Focus();
                return;
            }
            if (cboLoaiHopDong.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập chọn quan hệ", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLoaiHopDong.Focus();
                return;
            }
                      
            //Chỉnh sửa thông tin
            if (Form_Template.intIDHopDongLaoDong > 0)
            {
                var dataEdit = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_HopDongLaoDongUpdate", new
                {
                    Id = Form_Template.intIDHopDongLaoDong,                  
                    NgayKyHD = Convert.ToDateTime(dteNgayKy.EditValue),
                    NgayHetHan = Convert.ToDateTime(dteNgayHetHan.EditValue),                  
                    LoaiHopDong = Convert.ToInt32(cboLoaiHopDong.EditValue),
                    GhiChu = txtGhiChu.Text,
                    NguoiSua = Form_Template.TaiKhoanLogin.ToUpper()
                });               
                if (dataEdit.errcode == 1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_hopdong_laodong"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(dataEdit.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form_Template.intIDHopDongLaoDong = 0;
                    Close();
                }
                else
                {
                    XtraMessageBox.Show(dataEdit.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            else 
            {              
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_HopDongLaoDongInsert", new
                {                     
                    MaNV = Convert.ToInt32(cboNhanVien.EditValue),
                    MaHopDong = txtMaHD.Text,                   
                    NgayKyHD = Convert.ToDateTime(dteNgayKy.EditValue),
                    NgayHetHan = Convert.ToDateTime(dteNgayHetHan.EditValue),
                    LoaiHopDong = Convert.ToInt32(cboLoaiHopDong.EditValue),
                    GhiChu = txtGhiChu.Text,
                    NguoiTao = Form_Template.TaiKhoanLogin.ToUpper()
                });                
                if (data.errcode==1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_hopdong_laodong"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMaHD.EditValue = null;
                    cboNhanVien.EditValue = null;
                    cboLoaiHopDong.EditValue = null;
                    txtGhiChu.EditValue = null;
                    dteNgayHetHan.EditValue = null;
                    dteNgayKy.EditValue = null;
                    txtMaHD.Focus();
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }           
        }

        private void Form_ThemHopDongLaoDong_Load(object sender, EventArgs e)
        {
            GetNhanVien();
            GetLoaiHopDong();
            if (Form_Template.Edit)
            {
                DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_HopDongLaoDongSelectID", new
                {
                    Id = Form_Template.intIDHopDongLaoDong
                });
                if (dt.Rows.Count > 0)
                {
                    txtMaHD.EditValue = dt.Rows[0]["MaHopDong"];
                    cboNhanVien.EditValue = Convert.ToInt32(dt.Rows[0]["MaNV"]);
                    txtGhiChu.EditValue = dt.Rows[0]["GhiChu"];
                    dteNgayKy.EditValue = Convert.ToDateTime(dt.Rows[0]["NgayKyHD"]);
                    dteNgayHetHan.EditValue = dt.Rows[0]["NgayHetHan"];
                    cboLoaiHopDong.EditValue = Convert.ToInt32(dt.Rows[0]["LoaiHopDong"]);
                    cboNhanVien.Properties.ReadOnly = true;
                    txtMaHD.Properties.ReadOnly = true;
                }
            }
        }

        private void Form_ThemHopDongLaoDong_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Template.intIDHopDongLaoDong = 0;
            Form_Template.Edit = false;
        }

        private void Form_ThemHopDongLaoDong_KeyDown(object sender, KeyEventArgs e)
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