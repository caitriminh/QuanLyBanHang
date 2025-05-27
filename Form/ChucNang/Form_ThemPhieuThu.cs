using DevExpress.XtraEditors;
using QuanLyBanHang.Class;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemPhieuThu : XtraForm
    {
        public Form_ThemPhieuThu()
        {
            InitializeComponent();
            dteNgayChungTu.EditValue = DateTime.Now.Date;           
        }

        public void GetLoaiPhieuThu()
        {
            var lstPhieuThu = ExecSQL.ExecProcedureDataAsDataTable("sp_LoaiThuChiByLoaiPhieu", new
            {
                LoaiPhieu = "Phiếu thu"
            });
            cboLoaiPhieuThu.Properties.DataSource = lstPhieuThu;
            cboLoaiPhieuThu.Properties.DisplayMember = "LoaiThuChi";
            cboLoaiPhieuThu.Properties.ValueMember = "Id";
        }

        public void GetKhachHang()        
        {           
            var lstKhachHang = ExecSQL.ExecProcedureDataAsDataTable("sp_KhachHangSelect");
            cboKhachHang.Properties.DataSource = lstKhachHang;
            cboKhachHang.Properties.DisplayMember = "TenKhachHang";
            cboKhachHang.Properties.ValueMember = "ID";
            cboKhachHang.EditValue = Form_Template.intIDMaKH;
        }

        public void GetHoaDon()
        {
            if (dteNgayChungTu.EditValue == DBNull.Value || cboKhachHang.EditValue==DBNull.Value)
            {
                return;
            }
            var listHoaDon = ExecSQL.ExecProcedureDataAsDataTable("sp_HoaDonByKhachHang", new
            {
                MaKhachHang = Convert.ToInt32(cboKhachHang.EditValue),
                Ky = Convert.ToDateTime(dteNgayChungTu.EditValue)
            });
            cboHoaDon.Properties.DataSource = listHoaDon;
            cboHoaDon.Properties.DisplayMember = "MaHoaDon";
            cboHoaDon.Properties.ValueMember = "MaHoaDon";
            cboHoaDon.EditValue = Form_Template.strMaHoaDon;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (dteNgayChungTu.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập vào ngày chứng từ", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dteNgayChungTu.Focus();
                return;
            }

            if (cboKhachHang.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng chọn khách hàng", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLoaiPhieuThu.Focus();
                return;
            }

            if (cboLoaiPhieuThu.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng chọn loại phiếu thu", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLoaiPhieuThu.Focus();
                return;
            }
            //Chỉnh sửa thông tin
            if (Form_Template.strMaPhieuThu.Length > 0)
            {
                var dataEdit = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_PhieuThuUpdate", new
                {
                    MaPhieu = Form_Template.strMaPhieuThu,
                    LoaiThuChi = Convert.ToInt32(cboLoaiPhieuThu.EditValue),
                    SoTien = Convert.ToDecimal(txtSoTien.EditValue),
                    NgayChungTu = Convert.ToDateTime(dteNgayChungTu.EditValue),
                    DienGiai = txtGhiChu.Text,
                    MaKH = Convert.ToInt32(cboKhachHang.EditValue),
                    MaHoaDon = cboHoaDon.Text,
                    NguoiSua = Form_Template.TaiKhoanLogin.ToUpper()
                });
                if (dataEdit.errcode==1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_phieuthu"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(dataEdit.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form_Template.strMaPhieuThu = "";
                    Close();
                }
                else
                {
                    XtraMessageBox.Show(dataEdit.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else //Thêm mới giá bán
            {               
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_PhieuThuInsert", new
                {                    
                    LoaiThuChi = Convert.ToInt32(cboLoaiPhieuThu.EditValue),
                    SoTien = Convert.ToDecimal(txtSoTien.EditValue),
                    NgayChungTu = Convert.ToDateTime(dteNgayChungTu.EditValue),                 
                    DienGiai = txtGhiChu.Text,
                    MaKH = Convert.ToInt32(cboKhachHang.EditValue),
                    MaHoaDon = cboHoaDon.Text,
                    NguoiTao = Form_Template.TaiKhoanLogin.ToUpper()
                }); ;
                if (data.errcode == 1)
                {                 
                    cboLoaiPhieuThu.EditValue = null;
                    cboKhachHang.EditValue = null;
                    cboHoaDon.EditValue = null;
                    txtSoTien.EditValue = 0;
                    txtGhiChu.EditValue = "";
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_phieuthu"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Nhập từ phiếu xuất bán
                    if (Form_Template.intIDMaKH > 0)
                    {
                        Close();
                    }                    
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
           
        }

        private void cboKhachHang_EditValueChanged(object sender, EventArgs e)
        {
            if (cboKhachHang.EditValue != null)
            {
                GetHoaDon();
            }
        }

        private void Form_ThemPhieuThu_Load(object sender, EventArgs e)
        {
            GetKhachHang();
            GetLoaiPhieuThu();
            if (Form_Template.Edit)
            {
                DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_PhieuThuByMaPhieu", new
                {
                    MaPhieu = Form_Template.strMaPhieuThu
                });
                if (dt.Rows.Count > 0)
                {                   
                    cboKhachHang.EditValue = dt.Rows[0]["MaKH"];
                    cboHoaDon.EditValue = dt.Rows[0]["MaHoaDon"];
                    cboLoaiPhieuThu.EditValue = dt.Rows[0]["LoaiThuChi"];
                    txtSoTien.EditValue = dt.Rows[0]["SoTien"];
                    dteNgayChungTu.EditValue = dt.Rows[0]["NgayChungTu"];
                    txtGhiChu.EditValue = dt.Rows[0]["DienGiai"]; 
                }
            }
            else
            {
                if (Form_Template.intIDMaKH > 0)
                {
                    txtGhiChu.Text = $"Thu tiền cho đơn hàng ({Form_Template.strMaHoaDon}).";
                }
            }
        }

        private void Form_ThemPhieuThu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Template.Edit = false;
            Form_Template.strMaPhieuThu = "";
            Form_Template.intIDMaKH = 0;
        }

        private void cboHoaDon_EditValueChanged(object sender, EventArgs e)
        {
            if (cboHoaDon.EditValue == null) { return; }
            var data = ExecSQL.ExecProcedureDataFistOrDefault<HoaDon>("sp_HoaDonSelectMaHD", new
            {
                mahd = cboHoaDon.EditValue
            });
            if (data == null) { return; }
            txtTienPhaiThu.EditValue = data.ThanhToan;
            txtSoTien.EditValue = data.ThanhToan;
        }

        private void dteNgayChungTu_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                GetHoaDon();
            }
            catch (Exception)
            {
              
            }
           
        }
    }
}