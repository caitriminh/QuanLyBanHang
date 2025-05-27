using DevExpress.Charts.Native;
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
    public partial class Form_UpdateThanhToan : XtraForm
    {
        public int intMaSP = 0;
        public int count = 0;
        public Form_UpdateThanhToan()
        {
            InitializeComponent();
            dteNgayLap.EditValue = DateTime.Now.Date;           
        }

        public void GetCTHoaDon()
        {
            var y = gridView1.FocusedRowHandle;
            var x = gridView1.TopRowIndex;         
           var lstHoaDon = ExecSQL.ExecProcedureDataAsDataTable("sp_HoaDonSelectAll", new
           {
               option=1,
               tungay= Form_Template.tungay,
               denngay = Form_Template.denngay,
               loaiphieu = Form_Template.strLoaiPhieu,
               MaHoaDon = Form_Template.strMaHoaDon
           });
            grcPhieuXuat.DataSource = lstHoaDon;

            gridView1.FocusedRowHandle = y;
            gridView1.TopRowIndex = x;  
        }


        private void BtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void BtnAccept_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int dem = 0;

            // Duyệt tất cả các dòng trong gridView1
            for (int rowHandle = 0; rowHandle < gridView1.RowCount; rowHandle++)
            {
                if (gridView1.IsValidRowHandle(rowHandle)) // Kiểm tra nếu là dòng hợp lệ
                {
                    var dr = gridView1.GetDataRow(rowHandle);
                    if (dr is null) continue; // Bỏ qua nếu null

                    // Thực thi stored procedure
                    ExecSQL.ExecProcedureNonData("sp_PhieuThuInsert", new
                    {
                        LoaiThuChi = 1,
                        SoTien = Convert.ToDecimal(dr["ThanhToan"]),
                        NgayChungTu = Convert.ToDateTime(dteNgayLap.EditValue),
                        DienGiai = txtDienGiai.Text,
                        MaKH = Convert.ToInt32(dr["MaKhachHang"]),
                        MaHoaDon = dr["MaHoaDon"],
                        NguoiTao = Form_Template.TaiKhoanLogin.ToUpper()
                    });
                    dem++;
                }
            }

            // Thông báo kết quả
            if (dem > 0)
            {
                // Gửi dữ liệu
                var msgBroker = new MessageBroker
                {
                    data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                    task = "them_phieuxuat"
                };
                msgBroker.Publish();

                XtraMessageBox.Show("Cập nhật thành công.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            else
            {
                XtraMessageBox.Show("Không có dữ liệu để cập nhật.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void Form_UpdateThanhToan_Load(object sender, EventArgs e)
        {
            GetCTHoaDon();
        }
    }
}