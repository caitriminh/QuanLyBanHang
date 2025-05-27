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
    public partial class Form_ThemPhieuNhap : XtraForm
    {
        public int intMaSP = 0;
        public int count = 0;
        public Form_ThemPhieuNhap()
        {
            InitializeComponent();
            dteNgayLap.EditValue = DateTime.Now.Date;
            
            if (Form_Template.strLoaiPhieu == "PN")
            {
                Text = "Cập Nhật Phiếu Nhập Hàng";
            }
            else
            {
                Text = "Cập Nhật Phiếu Nhập Điều Chỉnh";
            }
        }

        private void OnNext(MessageBroker value)
        {
            if (value.task == "them_ncc")
            {
                GetNCC();
            }
        }           

        public void GetNCC()
        {
            var lstNCC = ExecSQL.ExecProcedureDataAsDataTable("sp_NhaCungCapSelect");
            cboNCC.Properties.DataSource= lstNCC; 
            cboNCC.Properties.DisplayMember = "TenNhaCungCap";
            cboNCC.Properties.ValueMember = "ID";
        }

        private void cboKhachHang_EditValueChanged(object sender, EventArgs e)
        {
            if(cboNCC.EditValue == DBNull.Value)
            {
                txtMaKH.Text = "";
                txtDiaChi.Text = "";
                txtDienThoai.Text = "";
                txtDienGiai.Text = "";
                return;
            }
            DataTable listNCC = ExecSQL.ExecProcedureDataAsDataTable("sp_NhaCungCapByID", new
            {
                ID = Convert.ToInt32(cboNCC.EditValue)
            });
            if (listNCC.Rows.Count == 0) {
                return;
            }
            txtMaKH.Text = listNCC.Rows[0]["ID"].ToString();
            txtDiaChi.Text = listNCC.Rows[0]["DiaChi"].ToString();
            txtDienThoai.Text = listNCC.Rows[0]["Dienthoai1"].ToString();          
        }

        public void GetCTPhieuNhap()
        {
            var y = gridView1.FocusedRowHandle;
            var x = gridView1.TopRowIndex;
            var lstCTPhieuNhap= ExecSQL.ExecProcedureDataAsDataTable("sp_ChiTietPhieuNhapByMaHD", new
            {
                mapn=txtMaPhieu.Text
            });
            grcChiTietPhieuNhap.DataSource = lstCTPhieuNhap;

            gridView1.FocusedRowHandle = y;
            gridView1.TopRowIndex = x;
        }

        public void GetSanPham()
        {
            var lstSanPham = ExecSQL.ExecProcedureDataAsDataTable("sp_SanPhamSelect", new {option = 4});
            cboMaSP.DataSource = lstSanPham;
            cboMaSP.DisplayMember = "ID";
            cboMaSP.ValueMember = "ID";
        }

        private void BtnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtMaPhieu.Focus();
            if (txtMaPhieu.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập mã phiếu", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaPhieu.Focus();
                return;
            }
            if (cboNCC.EditValue == null || txtMaKH.Text.Length == 0)
            {
                XtraMessageBox.Show("Vui lòng nhập chọn nhà cung cấp", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNCC.Focus();
                return;
            }
            if (cboKho.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập chọn tên kho", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKho.Focus();
                return;
            }
            //Lưu phiếu nhập
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_PhieuNhapInsert", new
            {
                mapn = txtMaPhieu.Text,
                ngaylap = Convert.ToDateTime(dteNgayLap.EditValue),
                mancc = Convert.ToInt32(cboNCC.EditValue),
                ghichu = txtDienGiai.Text,
                manv = Form_Template.intMaNV,
                loaiphieu = Form_Template.strLoaiPhieu,
                nguoicapnhat = Form_Template.TaiKhoanLogin.ToUpper(),
                makho = Convert.ToInt32(cboKho.EditValue),
                madh = ""
            }) ;
            if (data.errcode==1) //Lưu thành công
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
                            ExecSQL.ExecProcedureNonData("sp_ChiTietPhieuNhapInsert", new
                            {
                                mapn = txtMaPhieu.Text,                                
                                masp = dr["MaSanPham"].ToString(),
                                soluong = dr["SoLuong"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["SoLuong"]),
                                dongia= dr["DonGia"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["DonGia"]),
                                thanhtien = dr["ThanhTien"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["ThanhTien"]),
                                chieckhau = dr["ChiecKhau"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["ChiecKhau"]),
                                tienchieckhau = dr["TienChiecKhau"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["TienChiecKhau"]),
                                thanhtoan = dr["ThanhToan"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["ThanhToan"]),
                                nguoitao = Form_Template.TaiKhoanLogin.ToUpper()
                            });
                            break;
                        case DataRowState.Modified:
                            ExecSQL.ExecProcedureNonData("sp_ChiTietPhieuNhapUpdate", new
                            {
                                mapn = txtMaPhieu.Text,
                                masp = dr["MaSanPham"].ToString(),
                                soluong = dr["SoLuong"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["SoLuong"]),
                                dongia = dr["DonGia"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["DonGia"]),
                                thanhtien = dr["ThanhTien"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["ThanhTien"]),
                                chieckhau = dr["ChiecKhau"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["ChiecKhau"]),
                                tienchieckhau = dr["TienChiecKhau"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["TienChiecKhau"]),
                                thanhtoan = dr["ThanhToan"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["ThanhToan"]),
                                nguoisua = Form_Template.TaiKhoanLogin.ToUpper()
                            });
                            break;                            
                    }
                    count += 1;
                }
                if (count > 0)
                {
                    //Update lại thành tiền
                    ExecSQL.ExecProcedureNonData("sp_PhieuNhapUpdateThanhTien", new
                    {
                        mapn=txtMaPhieu.Text
                    });
                   //Gửi dữ liệu
                   var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_phieunhap"
                    };
                    msgBroker.Publish();
                    ////Cập nhật thông tin thanh toán
                    //DataTable lstHD = ExecSQL.ExecProcedureDataAsDataTable("sp_HoaDonSelectMaHD", new
                    //{
                    //    mahd = Form_Template.strMaHoaDon
                    //});
                    //if (lstHD.Rows.Count == 0) { return; }
                    GetCTPhieuNhap();
                    XtraMessageBox.Show("Thêm mới phiếu nhập thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    GetCTPhieuNhap();
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_phieunhap"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            //Lưu chi tiết hóa đơn

        }
      
        private void gridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
            {
            if (string.IsNullOrEmpty(txtMaPhieu.Text))
            {
                XtraMessageBox.Show("Bạn vui lòng nhập vào mã chứng từ để tiếp tục.");
                return;
            }
            decimal ThanhTien = 0;
            decimal TienChiecKhau = 0;
            decimal ChieckKhau = 0;
            var view = (GridView)sender;
            var i = view.FocusedRowHandle;        
            switch (view.FocusedColumn.FieldName)
            {
                case "MaSanPham":
                    DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_SanPhamByID", new { id = e.Value.ToString() });
                    if (dt.Rows.Count == 0) {
                        e.Valid = false;                       
                        return; 
                    }  
                    view.SetRowCellValue(i, "MaPhieuNhap", txtMaPhieu.Text);
                    view.SetRowCellValue(i, "MaSanPham", e.Value);
                    view.SetRowCellValue(i, "TenSanPham", dt.Rows[0]["TenSanPham"]);
                    view.SetRowCellValue(i, "DonViTinh", dt.Rows[0]["DonViTinh"]);
                    view.SetRowCellValue(i, "SoLuong", 1);                   
                    view.SetRowCellValue(i, "ChiecKhau", ChieckKhau);
                    view.SetRowCellValue(i, "DonGia", 0);
                    view.SetRowCellValue(i, "ThanhTien", 0);                   
                    view.SetRowCellValue(i, "TienChiecKhau", 0);
                    view.SetRowCellValue(i, "ThanhToan", 0);
                    break;
                case "DonGia":
                    try
                    {
                        if (view.GetRowCellValue(i, "MaSanPham") == null) { return; }
                        view.SetRowCellValue(i, "DonGia", e.Value);
                        ThanhTien = Convert.ToDecimal(e.Value) * Convert.ToDecimal(view.GetRowCellValue(i, "SoLuong"));
                        TienChiecKhau = ThanhTien * Convert.ToDecimal(view.GetRowCellValue(i, "ChiecKhau")) / 100;
                        view.SetRowCellValue(i, "ThanhTien", ThanhTien);
                        view.SetRowCellValue(i, "TienChiecKhau", TienChiecKhau);
                        view.SetRowCellValue(i, "ThanhToan", ThanhTien - TienChiecKhau);
                    }
                    catch (Exception)
                    {
                        e.Valid = false;
                        e.ErrorText = "Lỗi nhập liệu.";
                    }
                   
                    break;
                case "SoLuong":
                    try
                    {
                        view.SetRowCellValue(i, "SoLuong", e.Value);
                        ThanhTien = Convert.ToDecimal(e.Value) * Convert.ToDecimal(view.GetRowCellValue(i, "DonGia"));
                        TienChiecKhau = ThanhTien * Convert.ToDecimal(view.GetRowCellValue(i, "ChiecKhau")) / 100;
                        view.SetRowCellValue(i, "ThanhTien", ThanhTien);
                        view.SetRowCellValue(i, "TienChiecKhau", TienChiecKhau);
                        view.SetRowCellValue(i, "ThanhToan", ThanhTien - TienChiecKhau);
                    }
                    catch (Exception)
                    {
                        e.Valid = false;
                        e.ErrorText = "Lỗi nhập liệu.";
                    }                   
                    break;
                case "ChiecKhau":
                    try
                    {
                        ThanhTien = Convert.ToDecimal(view.GetRowCellValue(i, "SoLuong")) * Convert.ToDecimal(view.GetRowCellValue(i, "DonGia"));
                        TienChiecKhau = ThanhTien * Convert.ToDecimal(e.Value) / 100;
                        view.SetRowCellValue(i, "TienChiecKhau", TienChiecKhau);
                        view.SetRowCellValue(i, "ThanhToan", ThanhTien - TienChiecKhau);
                    }
                    catch (Exception)
                    {
                        e.Valid = false;
                        e.ErrorText = "Lỗi nhập liệu.";
                    }                   
                    break;
            }
        }

        private void txtMaHD_Leave(object sender, EventArgs e)
        {
            GetCTPhieuNhap();
        }

        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetCTPhieuNhap();
        }
       
        private void BtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void BtnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dgr = XtraMessageBox.Show("Có muốn xóa chi tiết phiếu nhập (" + txtMaPhieu.Text + ") này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr != DialogResult.Yes) { return; }
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_ChiTietPhieuNhapDelete", new
            {
                mapn = txtMaPhieu.Text,
                masp=intMaSP
            });
            if (data.errcode==1)
            {
                GetCTPhieuNhap();
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Lấy mã sản phẫm khi click vào chi tiết hóa đơn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var i = gridView1.FocusedRowHandle;
            if (i < 0) { return; }
            intMaSP = Convert.ToInt32(gridView1.GetRowCellValue(i, "MaSanPham"));
        }

        private void BtnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_InPhieuNhapByMaHD", new
            {
                mapn = txtMaPhieu.Text               
            });
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 5;
            var frm = new Form_Report();
            frm.Show();
          

        }

        private void cboKhachHang_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                Form_Template.intIDMaNCC = 0;
                Form_Template.Edit = false;
                Form_ThemNhaCungCap frm = new Form_ThemNhaCungCap();
                frm.ShowDialog();
            }
        }

        private void BtnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.Edit = false;
            cboNCC.EditValue = DBNull.Value;
            var data = ExecSQL.ExecProcedureSacalar("spGetMaTuDong", new { option = Form_Template.strLoaiPhieu == "PN" ? 2 : 3 });
            txtMaPhieu.Text = data.ToString();
            GetCTPhieuNhap();
        }

        private void BtnExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "PhieuNhapKho_" + DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss"); ;
            if (xtraSaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var dt = ExecSQL.ExecProcedureDataAsDataTable("sp_ChiTietHoaDonByMaHD", new { mahd = txtMaPhieu.Text });
                ExportExcel.ExportExcelFromDataTable(dt, xtraSaveFileDialog1.FileName);
            }
        }

        private void Form_ThemPhieuNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Template.Edit = false;
        }

        public void GetKho()
        {
            var data = ExecSQL.ExecProcedureDataAsDataTable("sp_KhoSelect");
            cboKho.Properties.DataSource = data;
            cboKho.Properties.DisplayMember = "TenKho";
            cboKho.Properties.ValueMember = "Id";
            cboKho.EditValue = 1;
        }

        public void GetNhanVien()
        {
            DataTable lstNV = ExecSQL.ExecProcedureDataAsDataTable("sp_NhanVienselectall");
            cboNhanVien.Properties.DataSource = lstNV;
            cboNhanVien.Properties.DisplayMember = "TenNhanVien";
            cboNhanVien.Properties.ValueMember = "ID";
            cboNhanVien.EditValue = Form_Template.intMaNV;
        }

        private void Form_ThemPhieuNhap_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetKho();
            GetNCC();
            GetSanPham();
            GetNhanVien();
            if (Form_Template.Edit == true) //
            {
                DataTable lstHD = ExecSQL.ExecProcedureDataAsDataTable("sp_PhieuNhapSelectID", new
                {
                    mapn = Form_Template.strMaPhieuNhap
                });
                if (lstHD.Rows.Count == 0) { return; }
                cboNCC.EditValue = Convert.ToInt32(lstHD.Rows[0]["MaNhaCungCap"]);
                txtMaPhieu.Text = lstHD.Rows[0]["MaPhieuNhap"].ToString();
                dteNgayLap.EditValue = Convert.ToDateTime(lstHD.Rows[0]["NgayLap"]);
                txtDienGiai.Text = lstHD.Rows[0]["GhiChu"].ToString();               
                txtMaPhieu.Properties.ReadOnly = true;

                ///Load chi tiết hòa đơn
                GetCTPhieuNhap();
            }
            else
            {
                var data = ExecSQL.ExecProcedureSacalar("spGetMaTuDong", new { option = Form_Template.strLoaiPhieu == "PN" ? 2 : 3 });
                txtMaPhieu.Text = data.ToString();
                GetCTPhieuNhap();
            }
        }
    }
}