using DevExpress.XtraEditors;
using ExcelDataReader;
using QuanLyBanHang.Class;
using SimpleBroker;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_DinhMuc : XtraForm
    {


        public Form_DinhMuc()
        {
            InitializeComponent();
            GetPhanQuyen();
        }

        public void GetPhanQuyen()
        {
            var data = ExecSQL.ExecProcedureDataFistOrDefault<QuyenHan>("sp_PhanQuyenSelectById", new
            {
                NhomQuyen = Form_Template.QuyenHan,
                Menu = 38
            });
            if (data == null) { return; }
            btnIn2.Enabled = data.InAn;
            BtnSua.Enabled = data.Sua;
            BtnExcel.Enabled = data.Excel;
            btnOptionThem.Enabled = data.Luu;
            colCopy.Visible = data.Luu;
            colXoa.Visible = data.Xoa;
        }

        public void GetSanPham()
        {
            var x = gridView2.FocusedRowHandle;
            var y = gridView2.TopRowIndex;
            var data = ExecSQL.ExecProcedureDataAsDataTable("sp_SanPhamOption", new
            {
                option = 1
            });
            grcSanPham.DataSource = data;
            lbl_MaSanPham.DataBindings.Clear();           
            lbl_MaSanPham.DataBindings.Add("text", data, "ID");           

            gridView2.FocusedRowHandle = x;
            gridView2.TopRowIndex = y;
        }

       
        public void GetDinhMucSanPham(int masp)        {
          
            var lstDinhMuc =  ExecSQL.ExecProcedureDataAsDataTable("sp_DinhMucSelectByMaSP", new
            {
                masp
            });
            grcChiTietDinhMuc.DataSource = lstDinhMuc;
            lbl_Id.DataBindings.Clear();
            lbl_Id.DataBindings.Add("text", lstDinhMuc, "Id");
        }

        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetSanPham();
        }

        private void BtnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var i = gridView2.FocusedRowHandle;
            if (i < 0) { return; }
            var dgr = XtraMessageBox.Show("Có muốn xóa bảng báo giá này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr != DialogResult.Yes) { return; }
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_BaoGiaDelete", new
            {
                MaKH = Convert.ToInt32(gridView2.GetRowCellValue(i,"MaKH")),
                NgayBaoGia = Convert.ToDateTime(gridView2.GetRowCellValue(i,"NgayBaoGia"))
            });
            if (data.errcode ==1)
            {
                GetSanPham();
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (lbl_Id.Text.Length > 0)
            {
                Form_Template.intIDDinhMuc = Convert.ToInt32(lbl_Id.Text);
                Form_Template.Edit = true;
                Form_Template.intIDSanPham = Convert.ToInt32(lbl_MaSanPham.Text);
                Form_ThemDinhMuc frm = new Form_ThemDinhMuc();
                frm.ShowDialog();
            }
            else
            {
                XtraMessageBox.Show("Chưa có định mức", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        
        private void BtnExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.SelectedRowsCount <= 0)
            {
                XtraMessageBox.Show("Không có dữ liệu để xuất.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "DinhMucSanPham_" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
            if (xtraSaveFileDialog1.ShowDialog() != DialogResult.OK) { return; }
            grcChiTietDinhMuc.ExportToXlsx(xtraSaveFileDialog1.FileName);
            Process.Start(new ProcessStartInfo(xtraSaveFileDialog1.FileName) { UseShellExecute = true });
        }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var i = gridView2.FocusedRowHandle;
            if (i < 0) { return; }
            GetDinhMucSanPham(Convert.ToInt32(gridView2.GetRowCellValue(i, "ID")));
        }

       
        private void OnNext(MessageBroker value)
        {
            if (value.task == "them_dinhmuc")
            {
                GetDinhMucSanPham(Convert.ToInt32(lbl_MaSanPham.Text));
            }
        }

        private void Form_DinhMuc_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetSanPham();
        }

        private void btnThem_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.Edit = false;
            Form_Template.intIDSanPham = Convert.ToInt32(lbl_MaSanPham.Text);
            Form_ThemDinhMuc frm = new Form_ThemDinhMuc();
            frm.Show();
        }

        private void grcChiTietDinhMuc_DoubleClick(object sender, EventArgs e)
        {
            if (BtnSua.Enabled == false) { return; }
            Form_Template.intIDDinhMuc = Convert.ToInt32(lbl_Id.Text);
            Form_Template.Edit = true;
            Form_Template.intIDSanPham = Convert.ToInt32(lbl_MaSanPham.Text);
            Form_ThemDinhMuc frm = new Form_ThemDinhMuc();
            frm.ShowDialog();
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column == colXoa)
            {
                var dgrResult = XtraMessageBox.Show("Bạn có muốn xóa định mức?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dgrResult != DialogResult.Yes) { return; }
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_DinhMucDeleteId", new
                {
                    id = Convert.ToInt32(lbl_Id.Text)
                });
                if (data.errcode == 1)
                {
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GetDinhMucSanPham(Convert.ToInt32(lbl_MaSanPham.Text));
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void gridView2_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column == colCopy)
            {
                Form_Template.intIDSanPham = Convert.ToInt32(lbl_MaSanPham.Text);
                Form_SaoChepDinhMuc frm = new Form_SaoChepDinhMuc();
                frm.ShowDialog();
            }
        }

        private void btnIn2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {            
            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_DinhMucSelectByMaSP", new
            {
                masp = Convert.ToInt32(lbl_MaSanPham.Text),                
            });
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 20;
            var frm = new Form_Report();
            frm.Show();
        }

        public void ImportExcelDataToSQL(string filePath)
        {
            int dem = 0;
            // Đọc dữ liệu từ file Excel           
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true // Sử dụng dòng đầu tiên làm tiêu đề cột
                        }
                    });
                    DataTable dataTable = result.Tables[0]; // Giả sử dữ liệu nằm ở sheet đầu tiên


                    foreach (DataRow row in dataTable.Rows)
                    {
                        string maSP = row["Mã sản phẩm"].ToString();
                        string manl = row["Mã NVL"].ToString();
                        decimal soluong =Convert.ToDecimal(row["Số lượng"]);
                        string dvt = row["ĐVT"].ToString();  
                        string GhiChu = row["Ghi chú"].ToString();
                        var masp = ExecSQL.ExecProcedureSacalar("sp_SanPhamSelectByCode", new
                        {
                            masp = maSP
                        });
                        var manvl = ExecSQL.ExecProcedureSacalar("sp_SanPhamSelectByCode", new
                        {
                            masp = manl
                        });
                        var MaDVT = ExecSQL.ExecProcedureSacalar("sp_DonViTinhSelectByTenDVT", new
                        {
                            DonViTinh = dvt
                        });
                        var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_DinhMucInsert", new
                        {
                            masp,
                            manvl,
                            soluong,
                            Ghichu = GhiChu,
                            NguoiTao = Form_Template.TaiKhoanLogin.ToUpper(),
                            madvt = Convert.ToInt32(MaDVT)
                        });
                        dem += 1;
                    }
                }
            }
            GetDinhMucSanPham(Convert.ToInt32(lbl_MaSanPham.Text));
            XtraMessageBox.Show("Đã nhập được " + dem.ToString() + " dòng.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnImportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraOpenFileDialog1.Filter = "Excel files |*.xlsx";
            xtraOpenFileDialog1.FileName = "Chọn file Excel";
            if (xtraOpenFileDialog1.ShowDialog() != DialogResult.OK) { return; }
            ImportExcelDataToSQL(xtraOpenFileDialog1.FileName);
        }
    }
}