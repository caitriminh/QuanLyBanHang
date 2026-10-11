using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Threading.Tasks;
using VisualStudio2019.Class;

namespace QuanLyBanHang.Report.BanLamViec
{
    public partial class user3 : XtraUserControl
    {
        public user3()
        {
            InitializeComponent();
        }

        private void TileItem2_ItemClick(object sender, TileItemEventArgs e)
        {

        }

        private void User3_Load(object sender, EventArgs e)
        {
            dteFromDate.EditValue = DateTime.Now.ToString("01/MM/yyyy");
            dteToDate.EditValue = DateTime.Now.ToString("dd/MM/yyyy");
           
            GetThongKe();
            GetChart();
        }

        public void GetThongKe()
        {
            try
            {
                DataTable data = ExecSQL.ExecProcedureDataAsDataTable("spGetThongKe", new
                {
                    TuNgay = Convert.ToDateTime(dteFromDate.EditValue),
                    DenNgay = Convert.ToDateTime(dteToDate.EditValue)
                });
                if (data == null || data.Rows.Count == 0) { return; }
                tileItem1.Text = Convert.ToDecimal(data.Rows[0]["TongDonHang"]).ToString("N0") + " đơn";
                tileItem2.Text = Convert.ToDecimal(data.Rows[0]["DoanhThu"]).ToString("N0") + "đ";
                tileItem3.Text = Convert.ToDecimal(data.Rows[0]["TongNhanVien"]).ToString("N0");
            }
            catch (Exception)
            {

               
            }
           
        }


        public async void GetChart()
        {
            try
            {
                chartControl1.Series.Clear();
                var dataTable = ExecSQL.ExecProcedureDataAsDataTable("spTopSanPhamBanChay", new
                {
                    TuNgay = Convert.ToDateTime(dteFromDate.EditValue),
                    DenNgay = Convert.ToDateTime(dteToDate.EditValue)
                });
                await Task.Factory.StartNew(new Action(() =>
                {

                    chartControl1.DataSource = dataTable;
                    chartControl1.BeginInvoke(new Action(() =>
                    {
                        Series seriesTopSPBanChay = new Series("Sản phẩm bán chạy", ViewType.Bar);
                        seriesTopSPBanChay.LabelsVisibility = DefaultBoolean.True;

                        // Add points to them, with their arguments different.
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            seriesTopSPBanChay.Points.Add(new SeriesPoint(dr["TenSanPham"], dr["SoLuong"]));
                        }
                        chartControl1.Series.AddRange(new Series[] { seriesTopSPBanChay });
                        chartControl1.Legend.Visibility = DefaultBoolean.True;

                        seriesTopSPBanChay.Label.TextPattern = "{V:#,##0}";

                        Legend legend = chartControl1.Legend;
                        legend.Margins.All = 8;
                        legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
                        legend.AlignmentVertical = LegendAlignmentVertical.Top;
                        legend.Direction = LegendDirection.LeftToRight;
                    }));
                }));
            }
            catch (Exception)
            {

               
            }
           
        }

        private void BarButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetThongKe();
            GetChart();
        }
    }
}
