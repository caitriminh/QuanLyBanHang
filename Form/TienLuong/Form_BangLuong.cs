using DevExpress.XtraEditors;
using System;
using VisualStudio2019.Class;
using QuanLyBanHang.Class;
using System.Windows.Forms;
using System.Diagnostics;
using SimpleBroker;

namespace VisualStudio2019.Form
{
    public partial class Form_BangLuong : XtraForm
    {
       
        public Form_BangLuong()
        {
            InitializeComponent();
            GetPhanQuyen();
        }

        public void GetPhanQuyen()
        {
            var data = ExecSQL.ExecProcedureDataFistOrDefault<QuyenHan>("sp_PhanQuyenSelectById", new
            {
                NhomQuyen = Form_Template.QuyenHan,
                Menu = 70
            });
            if (data == null) { return; }
            btnXoa.Enabled = data.Xoa;
            btnSua.Enabled = data.Sua;
            BtnExcel.Enabled = data.Excel;
            btnOptionIn.Enabled = data.InAn;
            btnOption.Enabled = data.Luu;
        }

        private void GetBangLuong(int Thang, int Nam)
        {
            try
            {
                var x = advBandedGridView1.FocusedRowHandle;
                var y = advBandedGridView1.TopRowIndex;

                var data = ExecSQL.ExecProcedureDataAsDataTable("sp_BangLuong", new
                {
                    Thang,
                    Nam
                });
                grcBangLuong.DataSource = data;
                lbl_Id.DataBindings.Clear();
                lbl_MaNV.DataBindings.Clear();

                lbl_Id.DataBindings.Add("text", data, "Id");
                lbl_MaNV.DataBindings.Add("text", data, "MaNV");

                advBandedGridView1.FocusedRowHandle = x;
                advBandedGridView1.TopRowIndex = y;
            }
            catch (Exception)
            {

                throw;
            }
           
        }
               
        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetBangLuong(Convert.ToInt32(cboThang.EditValue), Convert.ToInt32(cboNam.EditValue));
        }

        private void BtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void BtnExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (advBandedGridView1.SelectedRowsCount <= 0)
            {
                XtraMessageBox.Show("Không có dữ liệu để xuất.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "BangLuong_" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
            if (xtraSaveFileDialog1.ShowDialog() != DialogResult.OK) { return; }
            grcBangLuong.ExportToXlsx(xtraSaveFileDialog1.FileName);
            Process.Start(new ProcessStartInfo(xtraSaveFileDialog1.FileName) { UseShellExecute = true });
        }

      
        public void GetNam()
        {
            var data = ExecSQL.ExecProcedureDataAsDataTable("sp_GetThangNam", new
            {
                option = 1
            });
            cboNam2.DataSource = data;
            cboNam2.DisplayMember = "Nam";
            cboNam2.ValueMember = "Nam";
            cboNam.EditValue = DateTime.Now.Year;
        }

        public void GetThang()
        {
            var data = ExecSQL.ExecProcedureDataAsDataTable("sp_GetThangNam", new
            {
                option = 2,
                Nam = Convert.ToInt32(cboNam.EditValue)
            });
            cboThang3.DataSource = data;
            cboThang3.DisplayMember = "Thang";
            cboThang3.ValueMember = "Thang";
            cboThang.EditValue = DateTime.Now.Month;
        }

        private void btnKyLuong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_KyLuong frm = new Form_KyLuong();
            frm.ShowDialog();
        }

        private void OnNext(MessageBroker value)
        {
            if (value.task == "them_bangluong")
            {
                GetBangLuong(Convert.ToInt32(cboThang.EditValue), Convert.ToInt32(cboNam.EditValue));
            }
        }


        private void Form_BangLuong_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetNam();
        }

        private void cboNam_EditValueChanged(object sender, EventArgs e)
        {
            if (cboNam.EditValue == null) { return; }
            GetThang();
        }

        private void btnCapNhatLuong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_TongHopLuong", new
            {
                Nam = Convert.ToInt32(cboNam.EditValue),
                Thang = Convert.ToInt32(cboThang.EditValue),
            });
            if (data.errcode == 1)
            {
                GetBangLuong(Convert.ToInt32(cboThang.EditValue), Convert.ToInt32(cboNam.EditValue));
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cboThang_EditValueChanged(object sender, EventArgs e)
        {
            if (cboThang.EditValue == null) { return; }
            GetBangLuong(Convert.ToInt32(cboThang.EditValue), Convert.ToInt32(cboNam.EditValue));
        }

        private void grcBangLuong_DoubleClick(object sender, EventArgs e)
        {
            if(btnSua.Enabled ==false ) { return; }
            Form_Template.intIDBangLuong = Convert.ToInt32(lbl_Id.Text);
            Form_Template.Edit = true;
            Form_ThemBangLuong frm = new Form_ThemBangLuong();
            frm.ShowDialog();
        }

        private void btnBangTinhThue_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_BangTinhThueTNCN frm = new Form_BangTinhThueTNCN();
            frm.ShowDialog();
        }

        private void btnPhieuLuong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_BangLuong", new
            {
                Nam = Convert.ToInt32(cboNam.EditValue),
                Thang = Convert.ToInt32(cboThang.EditValue),
                MaNV = lbl_MaNV.Text
            });
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 29;
            var frm = new Form_Report();
            frm.Show();
        }

        private void btnInBangLuong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_BangLuong", new
            {
                Nam = Convert.ToInt32(cboNam.EditValue),
                Thang = Convert.ToInt32(cboThang.EditValue)            
            });
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 30;
            var frm = new Form_Report();
            frm.Show();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {           
            var dgr = XtraMessageBox.Show($"Có muốn xóa bảng lương này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr != DialogResult.Yes) { return; }
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_BangLuongDelete", new
            {
                Nam = Convert.ToInt32(cboNam.EditValue),
                Thang = Convert.ToInt32(cboThang.EditValue)
            });
            if (data.errcode == 1)
            {
                GetBangLuong(Convert.ToInt32(cboThang.EditValue), Convert.ToInt32(cboNam.EditValue));
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.intIDBangLuong = Convert.ToInt32(lbl_Id.Text);
            Form_Template.Edit = true;
            Form_ThemBangLuong frm = new Form_ThemBangLuong();
            frm.ShowDialog();
        }
    }

}
