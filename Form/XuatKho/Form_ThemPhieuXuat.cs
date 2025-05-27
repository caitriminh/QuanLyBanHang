using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraRichEdit.Import.Doc;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemPhieuXuat : XtraForm
    {
        public int intMaSP = 0;
        public int count = 0;
        public Form_ThemPhieuXuat()
        {
            InitializeComponent();
            dteNgayLap.EditValue = DateTime.Now.Date;
            if (Form_Template.strLoaiPhieu == "XB")
            {
                Text = "Cập Nhật Phiếu Xuất Hàng";
            }
            else
            {
                Text = "Cập Nhật Phiếu Xuất Điều Chỉnh";
            }
        }

        private void OnNext(MessageBroker value)
        {
            if (value.task == "them_khachhang")
            {
                GetKhachHang();
            }
        }

        public void GetKho()
        {
            var data = ExecSQL.ExecProcedureDataAsDataTable("sp_KhoSelect");
            cboKho.Properties.DataSource = data;
            cboKho.Properties.DisplayMember = "TenKho";
            cboKho.Properties.ValueMember = "Id";
            cboKho.EditValue = 1;
        }

        public void GetDonDatHang()
        {
            var data = ExecSQL.ExecProcedureDataAsDataTable("sp_DonDatHangByKH", new
            {
                MaKH = Convert.ToInt32(cboKhachHang.EditValue),
                Ky = Convert.ToDateTime(dteNgayLap.EditValue).ToString("yyyyMM")
            });
            cboDonDatHang.Properties.DataSource = data;
            cboDonDatHang.Properties.DisplayMember = "MaDonHang";
            cboDonDatHang.Properties.ValueMember = "MaDonHang";
        }

        private void Form_ThemPhieuXuat_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetKho();
            GetKhachHang();
            if (Form_Template.Edit == true) //
            {
                DataTable lstHD = ExecSQL.ExecProcedureDataAsDataTable("sp_HoaDonSelectMaHD", new
                {
                    mahd = Form_Template.strMaHoaDon
                });
                if (lstHD.Rows.Count == 0) { return; }
                cboKhachHang.EditValue = Convert.ToInt32(lstHD.Rows[0]["MaKhachHang"]);
                cboDonDatHang.EditValue = lstHD.Rows[0]["MaDonHang"];
                txtMaHD.Text = lstHD.Rows[0]["MaHoaDon"].ToString();
                dteNgayLap.EditValue = Convert.ToDateTime(lstHD.Rows[0]["NgayLap"]);
                txtDienGiai.Text = lstHD.Rows[0]["DienGiai"].ToString();
                txtMaHD.Properties.ReadOnly = true;

                chkNhapXuatThang.Checked = lstHD.Rows[0]["Ref"].ToString().Length > 0;


                ///Load chi tiết hòa đơn
                GetCTHoaDon();
            }
            else
            {
                var data = ExecSQL.ExecProcedureSacalar("spGetMaTuDong", new { option = Form_Template.strLoaiPhieu == "XB" ? 1 : 4 });
                txtMaHD.Text = data.ToString();
                GetCTHoaDon();
            }
        }

        public void GetKhachHang()
        {
            var lstKhachHang = ExecSQL.ExecProcedureDataAsDataTable("sp_KhachHangSelect");
            cboKhachHang.Properties.DataSource = lstKhachHang;
            cboKhachHang.Properties.DisplayMember = "TenKhachHang";
            cboKhachHang.Properties.ValueMember = "ID";
        }


        private void cboKhachHang_EditValueChanged(object sender, EventArgs e)
        {
            if (cboKhachHang.EditValue == null)
            {
                txtMaKH.Text = "";
                txtDiaChi.Text = "";
                txtDienThoai.Text = "";
                txtDienGiai.Text = "";
                return;
            }
            DataTable listKH = ExecSQL.ExecProcedureDataAsDataTable("sp_KhachHangByID", new
            {
                ID = Convert.ToInt32(cboKhachHang.EditValue)
            });
            if (listKH.Rows.Count == 0) {
                return;
            }
            txtMaKH.Text = listKH.Rows[0]["ID"].ToString();
            txtDiaChi.Text = listKH.Rows[0]["DiaChi"].ToString();
            txtDienThoai.Text = listKH.Rows[0]["DienThoai1"].ToString();
            //Load dơn đặt hàng
            GetDonDatHang();
        }

        public void GetCTHoaDon()
        {
            var y = gridView1.FocusedRowHandle;
            var x = gridView1.TopRowIndex;
            var lstCTHoaDon = ExecSQL.ExecProcedureDataAsDataTable("sp_ChiTietHoaDonByMaHD", new
            {
                mahd = txtMaHD.Text
            });
            grcChiTietPhieuXuat.DataSource = lstCTHoaDon;

            gridView1.FocusedRowHandle = y;
            gridView1.TopRowIndex = x;

            //Tính lại tổng khi chi tiết hóa đơn thanh đổi
            //object sumThanhTien = lstCTHoaDon.Compute("SUM(ThanhTien)", "");           
            //decimal decThanhTien = sumThanhTien != DBNull.Value ? Convert.ToDecimal(sumThanhTien) : 0;

            //object sumTienChiecKhau = lstCTHoaDon.Compute("SUM(TienChiecKhau)", "");
            //decimal decTienChiecKhau = sumTienChiecKhau != DBNull.Value ? Convert.ToDecimal(sumTienChiecKhau) : 0;

            //object sumThanhToan = lstCTHoaDon.Compute("SUM(ThanhToan)", "");
            //decimal decThanhToan = sumThanhToan != DBNull.Value ? Convert.ToDecimal(sumThanhToan) : 0;

            //txtThanhTien.EditValue = decThanhTien;
            //txtTienGiam.EditValue = decTienChiecKhau;
            //txtThanhToan.EditValue = decThanhToan;           
        }

        public void GetSanPham()
        {
            if (cboDonDatHang.EditValue == null) { return; }
            var lstSanPham = ExecSQL.ExecProcedureDataAsDataTable("sp_SanPhamGiaBanKH", new
            {
                MaDonHang = cboDonDatHang.EditValue
            });
            cboMaSP.DataSource = lstSanPham;
            cboMaSP.DisplayMember = "ID";
            cboMaSP.ValueMember = "ID";
        }

        private void BtnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtMaHD.Focus();
            if (txtMaHD.EditValue == null || string.IsNullOrWhiteSpace(txtMaHD.EditValue.ToString()))
            {
                XtraMessageBox.Show("Vui lòng nhập số hóa đơn", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaHD.Focus();
                return;
            }
            if (cboKhachHang.EditValue == null || txtMaKH.Text.Length == 0)
            {
                XtraMessageBox.Show("Vui lòng nhập chọn khách hàng", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKhachHang.Focus();
                return;
            }
            if (cboKho.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập chọn tên kho", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKhachHang.Focus();
                return;
            }
            if (cboDonDatHang.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập chọn đơn đặt hàng", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboDonDatHang.Focus();
                return;
            }
            //Lưu Hoa1 đơn
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_HoaDonInsert", new
            {
                mahd = txtMaHD.Text,
                ngaylap = Convert.ToDateTime(dteNgayLap.EditValue),
                manv = Form_Template.intMaNV,
                makh = Convert.ToInt32(cboKhachHang.EditValue),
                diengiai = txtDienGiai.Text,                
                loaiphieu=Form_Template.strLoaiPhieu,
                nguoicapnhat = Form_Template.TaiKhoanLogin.ToUpper(),
                makho = Convert.ToInt32(cboKho.EditValue),
                madonhang = cboDonDatHang.EditValue.ToString()
            });
            if (data.errcode==1) //Lưu thành công
            {                
                for (var i = 0; i <= gridView1.RowCount; i++)
                {

                    var dr = gridView1.GetDataRow(Convert.ToInt32(i));
                    if (dr is null)
                    {
                        break;
                    }
                    switch (dr.RowState)
                    {
                        case DataRowState.Added:                            
                            ExecSQL.ExecProcedureNonData("sp_ChiTietHoaDonInsert", new
                            {
                                mahd = txtMaHD.Text,                                
                                masp = dr["MaSanPham"].ToString(),
                                soluong = dr["SoLuong"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["SoLuong"]),
                                dongia= dr["DonGia"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["DonGia"]),
                                thanhtien = dr["ThanhTien"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["ThanhTien"]),
                                chieckhau = dr["ChiecKhau"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["ChiecKhau"]),
                                tienchieckhau= dr["TienChiecKhau"] == DBNull.Value ? 0 :  Convert.ToDecimal(dr["TienChiecKhau"]),
                                thanhtoan = dr["ThanhToan"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["ThanhToan"]),
                                NguoiTao = Form_Template.TaiKhoanLogin.ToUpper(),
                                VAT = Convert.ToInt32(dr["VAT"]),
                                TienVAT = dr["TienVAT"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["TienVAT"]),
                                GiaTruocThue = dr["GiaTruocThue"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["GiaTruocThue"])
                            });
                            break;
                        case DataRowState.Modified:
                            ExecSQL.ExecProcedureNonData("sp_ChiTietHoaDonUpdate", new
                            {
                                mahd = txtMaHD.Text,
                                masp = dr["MaSanPham"].ToString(),
                                soluong = dr["SoLuong"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["SoLuong"]),
                                dongia = dr["DonGia"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["DonGia"]),
                                thanhtien = dr["ThanhTien"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["ThanhTien"]),
                                chieckhau = dr["ChiecKhau"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["ChiecKhau"]),
                                tienchieckhau = dr["TienChiecKhau"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["TienChiecKhau"]),
                                thanhtoan = dr["ThanhToan"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["ThanhToan"]),
                                NguoiSua = Form_Template.TaiKhoanLogin.ToUpper(),
                                VAT = Convert.ToInt32(dr["VAT"]),
                                TienVAT = dr["TienVAT"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["TienVAT"]),
                                GiaTruocThue = dr["GiaTruocThue"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["GiaTruocThue"])                                
                            });
                            break;                            
                    }
                    count += 1;
                }
                if (count > 0)
                {
                    //Update lại thành tiền
                    ExecSQL.ExecProcedureNonData("sp_HoaDonUpdateThanhTien", new
                    {
                        madh=txtMaHD.Text
                    });
                    //Tạo phiếu nhập xuất thẳng
                    if (chkNhapXuatThang.Checked)
                    {
                        ExecSQL.ExecProcedureNonData("sp_PhieuNhapAuto", new
                        {
                            mahd = txtMaHD.Text
                        });
                    }
                   //Gửi dữ liệu
                   var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_phieuxuat"
                    };
                    msgBroker.Publish();                   
                    GetCTHoaDon();
                    XtraMessageBox.Show("Thêm mới hoá đơn thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    GetCTHoaDon();
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_phieuxuat"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            //Lưu chi tiết hóa đơn

        }
      
        private void GridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
            {
            if (txtMaHD.EditValue == null)
            {
                XtraMessageBox.Show("Bạn vui lòng nhập vào mã chứng từ để tiếp tục.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaHD.Focus();
                return;
            }
            if (cboKhachHang.EditValue == null)
            {
                XtraMessageBox.Show("Bạn vui lòng nhập chọn tên khách hàng.","Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKhachHang.Focus();
                return;
            }
            decimal ThanhTien;
            decimal ThanhTienSauThue;
            decimal TienThueVAT;
            decimal ChieckKhau;
            decimal TienChiecKhau;
            decimal GiaTruocThue;
            var view = (GridView)sender;
            var i = view.FocusedRowHandle;        
            switch (view.FocusedColumn.FieldName)
            {
                case "MaSanPham":
                   
                    DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_SanPhamGiaBanKH", new 
                    { 
                        MaDonHang = cboDonDatHang.EditValue.ToString(),                        
                        ID = e.Value.ToString() 
                    });
                    if (dt == null) { return; }                  
                    view.SetRowCellValue(i, "MaHoaDon", txtMaHD.Text);
                    view.SetRowCellValue(i, "MaSanPham", e.Value);
                    view.SetRowCellValue(i, "TenSanPham", dt.Rows[0]["TenSanPham"].ToString());
                    view.SetRowCellValue(i, "TenSanPham2", "");
                    view.SetRowCellValue(i, "DonViTinh", dt.Rows[0]["DonViTinh"].ToString());
                    view.SetRowCellValue(i, "SoLuong", dt.Rows[0]["SoLuong"]);
                    view.SetRowCellValue(i, "VAT", Convert.ToInt32(dt.Rows[0]["VAT"]));
                   
                    //Lấy chiếc khấu từng khách hàng
                    var data = ExecSQL.ExecProcedureSacalar("sp_ChiecKhauSelectByKH", new
                    {
                        MaKH = Convert.ToInt32(cboKhachHang.EditValue),
                        MaSP = e.Value
                    });
                    if (data == null)
                    {
                        ChieckKhau = 0;
                    }
                    else
                    {
                        ChieckKhau = Convert.ToDecimal(data.ToString());
                    }
                   
                   
                    view.SetRowCellValue(i, "ChiecKhau", ChieckKhau);
                    view.SetRowCellValue(i, "DonGia", Convert.ToDecimal(dt.Rows[0]["GiaSauThue"]));
                  
                    view.SetRowCellValue(i, "ThanhTien", Convert.ToDecimal( dt.Rows[0]["GiaSauThue"]) * Convert.ToDecimal(dt.Rows[0]["SoLuong"]));
                    GiaTruocThue = Convert.ToDecimal(dt.Rows[0]["GiaBan"]);

                    view.SetRowCellValue(i, "GiaTruocThue", GiaTruocThue);

                    TienThueVAT = Convert.ToDecimal(dt.Rows[0]["GiaSauThue"]) * Convert.ToDecimal(dt.Rows[0]["SoLuong"]) * Convert.ToDecimal(dt.Rows[0]["VAT"]) / 100;
                    view.SetRowCellValue(i, "TienVAT", TienThueVAT);
                    //Tiền chiếc khấu tính theo giá trước thuế
                    TienChiecKhau = (GiaTruocThue * Convert.ToDecimal(dt.Rows[0]["SoLuong"]) * ChieckKhau) / 100;
                    view.SetRowCellValue(i, "TienChiecKhau", TienChiecKhau);
                    view.SetRowCellValue(i, "ThanhToan", Convert.ToDecimal(dt.Rows[0]["GiaSauThue"]) * Convert.ToDecimal(dt.Rows[0]["SoLuong"]) - TienChiecKhau);
                    break;
                case "SoLuong":
                    try
                    {
                        view.SetRowCellValue(i, "SoLuong", e.Value);
                        ThanhTienSauThue = Convert.ToDecimal(e.Value) * Convert.ToDecimal(view.GetRowCellValue(i, "DonGia"));
                        GiaTruocThue = Convert.ToDecimal(view.GetRowCellValue(i, "GiaTruocThue"));
                        ThanhTien = Convert.ToDecimal(e.Value) * GiaTruocThue; 
                        TienChiecKhau = ThanhTien * Convert.ToDecimal(view.GetRowCellValue(i, "ChiecKhau")) / 100;

                        TienThueVAT = Convert.ToDecimal(view.GetRowCellValue(i, "DonGia")) * Convert.ToDecimal(view.GetRowCellValue(i, "VAT")) * Convert.ToDecimal(e.Value) / 100;
                        view.SetRowCellValue(i, "TienVAT", TienThueVAT);

                        view.SetRowCellValue(i, "ThanhTien", ThanhTienSauThue);
                        view.SetRowCellValue(i, "TienChiecKhau", TienChiecKhau);
                        view.SetRowCellValue(i, "ThanhToan", ThanhTienSauThue - TienChiecKhau);
                    }
                    catch (Exception)
                    {
                        e.Valid = false;
                        e.ErrorText = "Lỗi nhập liệu";
                    }
                   
                    break;               
                   
            }
        }

        private void txtMaHD_Leave(object sender, EventArgs e)
        {
            GetCTHoaDon();
        }

        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetCTHoaDon();
        }

        private void Form_ThemPhieuXuat_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Template.Edit = false;
        }

        private void BtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void BtnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dgr = XtraMessageBox.Show("Có muốn xóa chi tiết hóa đơn (" + txtMaHD.Text + ") này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr != DialogResult.Yes) { return; }
            var data = ExecSQL.ExecProcedureSacalar("sp_ChiTietHoaDonDelete", new
            {
                mahd = txtMaHD.Text,
                masp=intMaSP
            });
            if (data.ToString() == "1")
            {
                GetCTHoaDon();
                XtraMessageBox.Show("Xóa hóa đơn thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show("Xóa hóa đơn không thành công", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_InHoaDonByMaHD", new
            {
                mahd = txtMaHD.Text               
            });
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 1;
            var frm = new Form_Report();
            frm.Show();
        }

        private void cboKhachHang_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                Form_Template.intIDMaKH = 0;
                Form_Template.Edit = false;
                Form_ThemKhachhang frm = new Form_ThemKhachhang();
                frm.ShowDialog();
            }
        }

        private void BtnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.Edit = false;
            cboKhachHang.EditValue = null;
            var data = ExecSQL.ExecProcedureSacalar("spGetMaTuDong", new { option = Form_Template.strLoaiPhieu == "XB" ? 1 : 4 });
            txtMaHD.Text = data.ToString();
            GetCTHoaDon();
        }
                  
        private void BtnExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "PhieuXuatKho_" + DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss"); ;
            if (xtraSaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var dt = ExecSQL.ExecProcedureDataAsDataTable("sp_ChiTietHoaDonByMaHD", new { mahd = txtMaHD.Text });
                ExportExcel.ExportExcelFromDataTable(dt, xtraSaveFileDialog1.FileName);
            }
        }

        private void cboDonDatHang_EditValueChanged(object sender, EventArgs e)
        {
            GetSanPham();
        }

        private void btnCapNhatDonHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtMaKH.Text.Length == 0)
            {
                XtraMessageBox.Show("Chưa chọn khách hàng.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboKhachHang.Focus();
                return;
            }
            if (cboDonDatHang.EditValue==null)
            {
                XtraMessageBox.Show("Chưa chọn đơn hàng.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboDonDatHang.Focus();
                return;
            }
            var dgr = XtraMessageBox.Show($"Có muốn cập nhật sản phẩm của đơn hàng ({cboDonDatHang.EditValue}) này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr != DialogResult.Yes) { return; }
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_ChiTietHoaDonByDonHang", new
            {
                mahd = txtMaHD.Text,
                ngaylap = Convert.ToDateTime(dteNgayLap.EditValue),
                makho = Convert.ToInt32(cboKho.EditValue),
                manv = Form_Template.intMaNV,
                makh = Convert.ToInt32(cboKhachHang.EditValue),
                madonhang = cboDonDatHang.EditValue.ToString(),
                nguoitao = Form_Template.TaiKhoanLogin.ToUpper()
            });
            if (data.errcode == 1)
            {
                GetCTHoaDon();
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if(e.Column == colXoa)
            {
                var dgr = XtraMessageBox.Show("Có muốn xóa chi tiết hóa đơn (" + txtMaHD.Text + ") này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dgr != DialogResult.Yes) { return; }
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_ChiTietHoaDonDelete", new
                {
                    mahd = txtMaHD.Text,
                    masp = intMaSP
                });
                if (data.errcode == 1)
                {
                    GetCTHoaDon();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }            
        }
    }
}