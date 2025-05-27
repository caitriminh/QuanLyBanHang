using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemDonMuaHang : XtraForm
    {
        public int intMaSP = 0;
        public int count = 0;
        public Form_ThemDonMuaHang()
        {
            InitializeComponent();
            dteNgayDatHang.EditValue = DateTime.Now.Date;
            dteNgayGiaoHang.EditValue = DateTime.Now.Date;
        }

        private void OnNext(MessageBroker value)
        {
            if (value.task == "them_nhacungcap")
            {
                GetNhaCungCap();
            }
        }

        public void GetNhaCungCap()
        {
            var lstNhaCungCap = ExecSQL.ExecProcedureDataAsDataTable("sp_NhaCungCapSelect");
            cboNhaCungCap.Properties.DataSource = lstNhaCungCap;
            cboNhaCungCap.Properties.DisplayMember = "TenNhaCungCap";
            cboNhaCungCap.Properties.ValueMember = "ID";
        }

        public void GetCTDonDatHang()
        {
            var y = gridView1.FocusedRowHandle;
            var x = gridView1.TopRowIndex;
            var lstCTDonDatHang = ExecSQL.ExecProcedureDataAsDataTable("sp_ChiTietDonMuaHang", new
            {
                madh = txtMaDonHang.Text
            });
            grcChiTietMuaHang.DataSource = lstCTDonDatHang;

            gridView1.FocusedRowHandle = y;
            gridView1.TopRowIndex = x;
        }

        public void GetSanPham()
        {            
            var lstSanPham = ExecSQL.ExecProcedureDataAsDataTable("sp_SanPhamTheoNCC", new
            {
                MaNCC = Convert.ToInt32(cboNhaCungCap.EditValue)
            });
            cboMaSP.DataSource = lstSanPham;
            cboMaSP.DisplayMember = "MaSP";
            cboMaSP.ValueMember = "MaSP";
        }

        private void BtnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtMaDonHang.Focus();
            if (string.IsNullOrEmpty(txtMaDonHang.Text))
            {
                XtraMessageBox.Show("Vui lòng nhập số hóa đơn", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaDonHang.Focus();
                return;
            }
            if (cboNhaCungCap.EditValue == null || txtMaNCC.Text.Length == 0)
            {
                XtraMessageBox.Show("Vui lòng nhập chọn nhà cung cấp", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNhaCungCap.Focus();
                return;
            }
            //Lưu Hoa1 đơn
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_DonMuaHangInsert", new
            {
                madh = txtMaDonHang.Text,
                ngaydathang = Convert.ToDateTime(dteNgayDatHang.EditValue),
                ngaygiaohang = Convert.ToDateTime(dteNgayGiaoHang.EditValue),
                mancc = Convert.ToInt32(cboNhaCungCap.EditValue),
                ghichu = txtDienGiai.Text,
                nguoicapnhat = Form_Template.TaiKhoanLogin.ToUpper()
            });
            if (data.errcode == 1) //Lưu thành công
            {
                for (var i = 0; i <= gridView1.RowCount; i++)
                {
                    var dr = gridView1.GetDataRow(Convert.ToInt32(i));
                    if (ReferenceEquals(dr, null))
                    {
                        break;
                    }
                    switch (dr.RowState)
                    {
                        case DataRowState.Added:
                            ExecSQL.ExecProcedureNonData("sp_ChiTietDonMuaHangInsert", new
                            {
                                madh = txtMaDonHang.Text,
                                masp = dr["MaSP"].ToString(),
                                soluong = dr["SoLuong"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["SoLuong"]),
                                vat = Convert.ToInt32(dr["VAT"]),
                                NguoiTao = Form_Template.TaiKhoanLogin.ToUpper(),
                                dongia = dr["DonGia"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["DonGia"]),
                                thanhtien = dr["ThanhTien"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["ThanhTien"]),
                                tienvat = dr["TienVAT"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["TienVAT"]),
                            });
                            break;
                        case DataRowState.Modified:
                            ExecSQL.ExecProcedureNonData("sp_ChiTietDonMuaHangUpdate", new
                            {
                                madh = txtMaDonHang.Text,
                                masp = dr["MaSP"].ToString(),
                                soluong = dr["SoLuong"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["SoLuong"]),
                                dongia = dr["DonGia"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["DonGia"]),
                                vat = Convert.ToInt32(dr["VAT"]),
                                NguoiSua = Form_Template.TaiKhoanLogin.ToUpper(),
                                thanhtien = dr["ThanhTien"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["ThanhTien"]),
                                tienvat = dr["TienVAT"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["TienVAT"]),
                            });
                            break;
                    }
                    count += 1;
                }
                if (count > 0)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_donmuahang"
                    };
                    msgBroker.Publish();
                    GetCTDonDatHang();
                    XtraMessageBox.Show("Thêm mới đơn mua hàng thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    GetCTDonDatHang();
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_dondathang"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            //Lưu chi tiết hóa đơn

        }

        private void gridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaDonHang.Text))
            {
                XtraMessageBox.Show("Bạn vui lòng nhập vào mã đơn hàng để tiếp tục.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaDonHang.Focus();
                return;
            }
            if (cboNhaCungCap.EditValue == null)
            {
                XtraMessageBox.Show("Bạn vui lòng nhập chọn tên khách hàng.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNhaCungCap.Focus();
                return;
            }
            decimal TienThueVAT, ThanhTien;
            var view = (GridView)sender;
            var i = view.FocusedRowHandle;
            switch (view.FocusedColumn.FieldName)
            {
                case "MaSP":

                    DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_SanPhamTheoNCC", new 
                    { 
                        MaNCC = Convert.ToInt32(cboNhaCungCap.EditValue),
                        MaSP =  e.Value.ToString() 
                    });
                    if (dt.Rows.Count == 0) {

                        e.Valid = false;
                        e.ErrorText = "Lỗi nhập liệu";
                    }
                    view.SetRowCellValue(i, "MaDonHang", txtMaDonHang.Text);
                    view.SetRowCellValue(i, "MaSP", e.Value);
                    view.SetRowCellValue(i, "TenSanPham", dt.Rows[0]["TenSanPham"].ToString());
                    view.SetRowCellValue(i, "DonViTinh", dt.Rows[0]["DonViTinh"].ToString());
                    view.SetRowCellValue(i, "SoLuong", 1);
                    view.SetRowCellValue(i, "DonGia", Convert.ToDecimal(dt.Rows[0]["GiaSauThue"]));
                    view.SetRowCellValue(i, "ThanhTien", Convert.ToDecimal(dt.Rows[0]["DonGia"]));
                    view.SetRowCellValue(i, "VAT", Convert.ToDecimal(dt.Rows[0]["VAT"]));
                    TienThueVAT = Convert.ToDecimal(dt.Rows[0]["GiaSauThue"]) * Convert.ToDecimal(dt.Rows[0]["VAT"]) / 100;
                    view.SetRowCellValue(i, "TienVAT", TienThueVAT); 
                    
                    break;
                case "SoLuong":
                    view.SetRowCellValue(i, "SoLuong", e.Value);
                    ThanhTien = Convert.ToDecimal(e.Value) * Convert.ToDecimal(view.GetRowCellValue(i, "DonGia"));
                    view.SetRowCellValue(i, "ThanhTien", ThanhTien);
                    TienThueVAT = ThanhTien * Convert.ToDecimal(view.GetRowCellValue(i, "VAT"))  / 100;
                    view.SetRowCellValue(i, "TienVAT", TienThueVAT);
                    break;
            }
        }

        private void txtMaHD_Leave(object sender, EventArgs e)
        {
            GetCTDonDatHang();
        }

        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetCTDonDatHang();
        }

        private void BtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void BtnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dgr = XtraMessageBox.Show($"Có muốn xóa chi tiết đơn hàng ({txtMaDonHang.Text}) này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr != DialogResult.Yes) { return; }
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_ChiTietDonDatHangDelete", new
            {
                madh = txtMaDonHang.Text,
                masp = intMaSP
            });
            if (data.errcode == 1)
            {
                GetCTDonDatHang();
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var i = gridView1.FocusedRowHandle;
            if (i < 0) { return; }
            intMaSP = Convert.ToInt32(gridView1.GetRowCellValue(i, "MaSP"));
        }

        private void BtnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_ChiTietDonDatHang", new
            {
                madh = txtMaDonHang.Text
            });
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 23;
            var frm = new Form_Report();
            frm.Show();
        }


        private void BtnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.Edit = false;
            cboNhaCungCap.EditValue = DBNull.Value;
            var data = ExecSQL.ExecProcedureSacalar("spGetMaTuDong", new { option = 8 });
            txtMaDonHang.Text = data.ToString();
            GetCTDonDatHang();
        }

        private void BtnExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "DonMuaHang_" + DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss"); ;
            if (xtraSaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var dt = ExecSQL.ExecProcedureDataAsDataTable("sp_ChiTietDonDatHang", new { madh = txtMaDonHang.Text });
                ExportExcel.ExportExcelFromDataTable(dt, xtraSaveFileDialog1.FileName);
            }
        }

        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            var i = gridView1.FocusedRowHandle;
            if (e.Column == colXoa)
            {
                var dgr = XtraMessageBox.Show($"Có muốn xóa chi tiết đơn hàng ({txtMaDonHang.Text}) này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dgr != DialogResult.Yes) { return; }
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_ChiTietDonMuaHangDelete", new
                {
                    madh = txtMaDonHang.Text,
                    masp = Convert.ToInt32(gridView1.GetRowCellValue(i, "MaSP"))
                }); ;
                if (data.errcode == 1)
                {
                    GetCTDonDatHang();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void Form_ThemDonMuaHang_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetNhaCungCap();          
            if (Form_Template.Edit == true) //
            {
                DataTable lstHD = ExecSQL.ExecProcedureDataAsDataTable("sp_DonMuaHangSelectMaDH", new
                {
                    madh = Form_Template.strDonMuaHang
                });
                if (lstHD.Rows.Count == 0) { return; }
                cboNhaCungCap.EditValue = Convert.ToInt32(lstHD.Rows[0]["MaNCC"]);
                txtMaDonHang.Text = lstHD.Rows[0]["MaDonHang"].ToString();
                dteNgayDatHang.EditValue = Convert.ToDateTime(lstHD.Rows[0]["NgayDatHang"]);
                dteNgayGiaoHang.EditValue = Convert.ToDateTime(lstHD.Rows[0]["NgayGiaoHang"]);
                txtDienGiai.Text = lstHD.Rows[0]["GhiChu"].ToString();
                txtMaDonHang.Properties.ReadOnly = true;
                ///Load chi tiết hòa đơn
                GetCTDonDatHang();
            }
            else
            {
                var data = ExecSQL.ExecProcedureSacalar("spGetMaTuDong", new { option = 8 });
                txtMaDonHang.Text = data.ToString();
                GetCTDonDatHang();
            }
        }

        private void Form_ThemDonMuaHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Template.Edit = false;
            Form_Template.strDonMuaHang = "";
        }

        private void cboKhachHang_Properties_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                Form_Template.intIDMaKH = 0;
                Form_Template.Edit = false;
                Form_ThemKhachhang frm = new Form_ThemKhachhang();
                frm.ShowDialog();
            }
        }

        private void cboNhaCungCap_EditValueChanged(object sender, EventArgs e)
        {
            if (cboNhaCungCap.EditValue == null)
            {
                txtMaNCC.Text = "";
                txtDiaChi.Text = "";
                txtDienGiai.Text = "";
                return;
            }
            GetSanPham();
            DataTable listNCC = ExecSQL.ExecProcedureDataAsDataTable("sp_NhaCungCapByID", new
            {
                ID = Convert.ToInt32(cboNhaCungCap.EditValue)
            });
            if (listNCC.Rows.Count == 0)
            {
                return;
            }
            txtMaNCC.Text = listNCC.Rows[0]["ID"].ToString();
            txtDiaChi.Text = listNCC.Rows[0]["DiaChi"].ToString();
        }
    }
}