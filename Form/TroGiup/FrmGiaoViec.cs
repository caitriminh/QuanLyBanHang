using DevExpress.XtraEditors;
using QuanLyBanHang.Class;
using SimpleBroker;
using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using VisualStudio2019;
using VisualStudio2019.Class;
using VisualStudio2019.Form;

namespace QuanLyBanHang.Form.TroGiup
{
    public partial class FrmGiaoViec : DevExpress.XtraEditors.XtraForm
    {
        public FrmGiaoViec()
        {
            InitializeComponent();
        }

        public void GetKy()
        {
            cboKy2.DataSource = DateTime.Now.GetDateOfWeek();
            cboKy2.DisplayMember = "name";
            cboKy2.ValueMember = "id";
            cboKy.EditValue = 5;
        }

        public void GetGiaoViec(DateTime tuNgay, DateTime denNgay)
        {
            var x = gridView1.FocusedRowHandle;
            var y = gridView1.TopRowIndex;
            DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("spGetGiaoViec", new
            {
                Option = 1,
                TuNgay=tuNgay,
                DenNgay=denNgay
            });
            grcBaoCaoCongViec.DataSource = dt;
            lbl_Id.DataBindings.Clear();
            lbl_Id.DataBindings.Add("text", dt, "Id");
            gridView1.FocusedRowHandle = x;
            gridView1.TopRowIndex = y;
        }


        private void OnNext(MessageBroker value)
        {
            if (value.task == "them_giaoviec")
            {
                GetGiaoViec(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
            }
        }

        private void FrmGiaoViec_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetKy();
        }

        private void cboKy_EditValueChanged(object sender, EventArgs e)
        {
            DataRowView rowSelected = (DataRowView)cboKy2.GetRowByKeyValue(cboKy.EditValue);
            dteTuNgay.EditValue = Convert.ToDateTime(rowSelected.Row["from"]);
            dteDenNgay.EditValue = Convert.ToDateTime(rowSelected.Row["to"]);
            GetGiaoViec(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
        }

        private void BtnExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.SelectedRowsCount <= 0)
            {
                XtraMessageBox.Show("Không có dữ liệu để xuất.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "BaoCaoCongViec_" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
            if (xtraSaveFileDialog1.ShowDialog() != DialogResult.OK) { return; }
            grcBaoCaoCongViec.ExportToXlsx(xtraSaveFileDialog1.FileName);           
            Process.Start(new ProcessStartInfo(xtraSaveFileDialog1.FileName) { UseShellExecute = true });
        }

        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetGiaoViec(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
        }

        private void BtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void btnTim_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetGiaoViec(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
        }

        private void BtnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dgr = XtraMessageBox.Show("Có muốn xóa chi tiết giao việc này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr != DialogResult.Yes) { return; }
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("spDeleteGiaoViec", new
            {
                Id = Convert.ToInt32(lbl_Id.Text)
            });
            if (data.errcode == 1)
            {
                GetGiaoViec(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.intIDGiaoViec = Convert.ToInt32(lbl_Id.Text);
            Form_Template.Edit = true;
            FrmThemGiaoViec frm = new FrmThemGiaoViec();
            frm.ShowDialog();
        }

        private void BtnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.intIDGiaoViec = 0;
            Form_Template.Edit = false;
            FrmThemGiaoViec frm = new FrmThemGiaoViec();
            frm.ShowDialog();
        }

        private void BtnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("spGetGiaoViec", new
            {
                Option = 1,
                TuNgay = Convert.ToDateTime(dteTuNgay.EditValue),
                DenNgay = Convert.ToDateTime(dteDenNgay.EditValue)
            });
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 44;
            Form_Template.tungay = Convert.ToDateTime(dteTuNgay.EditValue);
            Form_Template.denngay = Convert.ToDateTime(dteDenNgay.EditValue);
            var frm = new Form_Report();
            frm.Show();
        }
    }
}