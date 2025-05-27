using DevExpress.XtraEditors;
using QuanLyBanHang.Class;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_BangBaoGia : XtraForm
    {


        public Form_BangBaoGia()
        {
            InitializeComponent();
            GetPhanQuyen();
        }

        public void GetPhanQuyen()
        {
            var data = ExecSQL.ExecProcedureDataFistOrDefault<QuyenHan>("sp_PhanQuyenSelectById", new
            {
                NhomQuyen = Form_Template.QuyenHan,
                Menu = 30
            });
            if (data == null) { return; }  
            BtnXoa.Enabled = data.Xoa;          
            BtnExcel.Enabled = data.Excel;          
            btnOptionIn.Enabled = data.InAn;
        }

        public void GetGiaBanKH()
        {
            var x = gridView2.FocusedRowHandle;
            var y = gridView2.TopRowIndex;
            var data = ExecSQL.ExecProcedureDataAsDataTable("sp_GetBaoGiaTheoKH");
            grcBaoGiaKH.DataSource = data;

            lbl_MaKH.DataBindings.Clear();
            lbl_NgayBaoGia.DataBindings.Clear();
            lbl_MaKH.DataBindings.Add("text", data, "MaKH");
            lbl_NgayBaoGia.DataBindings.Add("text", data, "NgayBaoGia");

            gridView2.FocusedRowHandle = x;
            gridView2.TopRowIndex = y;
        }

       
        public void GetBangBaoGia(int MaKH, DateTime NgayBaoGia)        {
          
            var lstGiaBan =  ExecSQL.ExecProcedureDataAsDataTable("sp_BaoGiaByKH", new
            {
                MaKH,
                NgayBaoGia
            });
            grcBangBaoGia.DataSource = lstGiaBan;   
        }

        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetGiaBanKH();
        }

        private void BtnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.Edit = false;
            Form_ThemGiaBan frm = new Form_ThemGiaBan();
            frm.ShowDialog();
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
                GetGiaBanKH();
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.intIDGiaBan =Convert.ToInt32(lbl_MaKH.Text);
            Form_Template.Edit = true;
            Form_ThemGiaBan frm = new Form_ThemGiaBan();
            frm.ShowDialog();
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
            xtraSaveFileDialog1.FileName = "GiaBan_" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
            if (xtraSaveFileDialog1.ShowDialog() != DialogResult.OK) { return; }
            grcBangBaoGia.ExportToXlsx(xtraSaveFileDialog1.FileName);
            Process.Start(new ProcessStartInfo(xtraSaveFileDialog1.FileName) { UseShellExecute = true });
        }

        private void btnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var i = gridView2.FocusedRowHandle;
            if (i<0) { return; }
            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_BaoGiaByKH", new
            {
                MaKH =Convert.ToInt32(gridView2.GetRowCellValue(i,"MaKH")) ,
                NgayBaoGia = Convert.ToDateTime(gridView2.GetRowCellValue(i,"NgayBaoGia"))
            });
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 16;
            var frm = new Form_Report();
            frm.Show();
        }

        private void Form_BangBaoGia_Load(object sender, EventArgs e)
        {
            GetGiaBanKH();
        }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var i = gridView2.FocusedRowHandle;
            if (i < 0) { return; }

            GetBangBaoGia(Convert.ToInt32(gridView2.GetRowCellValue(i, "MaKH")), Convert.ToDateTime(gridView2.GetRowCellValue(i, "NgayBaoGia")));
        }

        private void btnInBaoGiav2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var i = gridView2.FocusedRowHandle;
            if (i < 0) { return; }
            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_BaoGiaByKH", new
            {
                MaKH = Convert.ToInt32(gridView2.GetRowCellValue(i, "MaKH")),
                NgayBaoGia = Convert.ToDateTime(gridView2.GetRowCellValue(i, "NgayBaoGia"))
            });
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 17;
            var frm = new Form_Report();
            frm.Show();
        }
    }
}