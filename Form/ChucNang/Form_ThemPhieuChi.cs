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
    public partial class Form_ThemPhieuChi : XtraForm
    {
        public Form_ThemPhieuChi()
        {
            InitializeComponent();
            dteNgayChungTu.EditValue = DateTime.Now.Date;           
        }

        public void GetLoaiPhieuChi()
        {
            var lstPhieuThu = ExecSQL.ExecProcedureDataAsDataTable("sp_LoaiThuChiByLoaiPhieu", new
            {
                LoaiPhieu = "Phiếu chi"
            });
            cboLoaiPhieuThu.Properties.DataSource = lstPhieuThu;
            cboLoaiPhieuThu.Properties.DisplayMember = "LoaiThuChi";
            cboLoaiPhieuThu.Properties.ValueMember = "Id";
        }

        public void GetNhaCungCap()        
        {           
            var lstNCC = ExecSQL.ExecProcedureDataAsDataTable("sp_NhaCungCapSelect");
            cboNCC.Properties.DataSource = lstNCC;
            cboNCC.Properties.DisplayMember = "TenNhaCungCap";
            cboNCC.Properties.ValueMember = "ID";
            cboNCC.EditValue = Form_Template.intIDMaNCC;
        }

        public void GetMaPhieu()
        {
            var lstMaPhieu = ExecSQL.ExecProcedureDataAsDataTable("sp_PhieuNhapByNhaCungCap", new
            {
                MaNhaCungCap = Convert.ToInt32(cboNCC.EditValue)
            });
            cboMaPhieu.Properties.DataSource = lstMaPhieu;
            cboMaPhieu.Properties.DisplayMember = "MaPhieuNhap";
            cboMaPhieu.Properties.ValueMember = "MaPhieuNhap";
            cboMaPhieu.EditValue = Form_Template.strMaPhieuNhap;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (dteNgayChungTu.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập vào ngày chứng từ.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dteNgayChungTu.Focus();
                return;
            }
            if (cboNCC.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng chọn nhà cung cấp.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLoaiPhieuThu.Focus();
                return;
            }

            if (cboLoaiPhieuThu.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng chọn loại phiếu chi", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    MaKH = Convert.ToInt32(cboNCC.EditValue),
                    MaHoaDon = cboMaPhieu.Text,
                    NguoiSua = Form_Template.TaiKhoanLogin.ToUpper()
                });
                if (dataEdit.errcode==1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_phieuchi"
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
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_PhieuChiInsert", new
                {                    
                    LoaiThuChi = Convert.ToInt32(cboLoaiPhieuThu.EditValue),
                    SoTien = Convert.ToDecimal(txtSoTien.EditValue),
                    NgayChungTu = Convert.ToDateTime(dteNgayChungTu.EditValue),                 
                    DienGiai = txtGhiChu.Text,
                    MaKH = Convert.ToInt32(cboNCC.EditValue),
                    MaHoaDon = cboMaPhieu.Text,
                    NguoiTao = Form_Template.TaiKhoanLogin.ToUpper()
                }); ;
                if (data.errcode == 1)
                {
                    dteNgayChungTu.EditValue = 
                    cboLoaiPhieuThu.EditValue = null;
                    cboNCC.EditValue = null;
                    cboMaPhieu.EditValue = null;
                    txtSoTien.EditValue = 0;
                    txtGhiChu.EditValue = "";
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_phieuchi"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Nhập từ phiếu nhập mua hàng
                    if (Form_Template.intIDMaNCC > 0)
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

        private void cboNCC_EditValueChanged(object sender, EventArgs e)
        {
            if (cboNCC.EditValue != null)
            {
                GetMaPhieu();
            }
        }

        private void cboMaPhieu_EditValueChanged(object sender, EventArgs e)
        {
            if(cboMaPhieu.EditValue ==  null) { return; }
            var data = ExecSQL.ExecProcedureDataFistOrDefault<PhieuNhap>("sp_PhieuNhapSelectID", new
            {
                mapn = cboMaPhieu.EditValue
            });
            if(data == null) { return; }
            txtThanhToan.EditValue = data.ThanhToan;
            txtSoTien.EditValue = data.ConLai;
        }

        private void Form_ThemPhieuChi_Load(object sender, EventArgs e)
        {
            GetNhaCungCap();
            GetLoaiPhieuChi();
            if (Form_Template.Edit)
            {
                DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_PhieuChiByMaPhieu", new
                {
                    MaPhieu = Form_Template.strMaPhieuThu
                });
                if (dt.Rows.Count > 0)
                {
                    cboNCC.EditValue = dt.Rows[0]["MaKH"];
                    cboMaPhieu.EditValue = dt.Rows[0]["MaHoaDon"];
                    cboLoaiPhieuThu.EditValue = dt.Rows[0]["LoaiThuChi"];
                    txtSoTien.EditValue = dt.Rows[0]["SoTien"];
                    dteNgayChungTu.EditValue = dt.Rows[0]["NgayChungTu"];
                    txtGhiChu.EditValue = dt.Rows[0]["DienGiai"];
                }
            }
        }

        private void Form_ThemPhieuChi_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Template.Edit = false;
            Form_Template.strMaPhieuThu = "";
            Form_Template.intIDMaNCC = 0;
        }
    }
}