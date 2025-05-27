using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Threading.Tasks;
using VisualStudio2019.Class;

namespace QuanLyBanHang.Report.BanLamViec
{
    public partial class userTyLeBanHang : XtraUserControl
    {
        public userTyLeBanHang()
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
           
          
            GetChart();
        }



        public async void GetChart()
        {
            chartControl1.Series.Clear();
            var dataTable = ExecSQL.ExecProcedureDataAsDataTable("spTopSanPhamBanChayv2", new
            {
                TuNgay = Convert.ToDateTime(dteFromDate.EditValue),
                DenNgay = Convert.ToDateTime(dteToDate.EditValue)
            });

            await Task.Factory.StartNew(new Action(() =>
            {
                chartControl1.DataSource = dataTable;

                chartControl1.BeginInvoke(new Action(() =>
                {
                    Series seriesTopSPBanChay = new Series("Sản phẩm bán chạy", ViewType.Pie);
                    seriesTopSPBanChay.LabelsVisibility = DefaultBoolean.True;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        seriesTopSPBanChay.Points.Add(new SeriesPoint(dr["TenSanPham"], dr["SoLuong"]));
                    }

                    // Gán series vào biểu đồ
                    chartControl1.Series.Add(seriesTopSPBanChay);

                    // Cài đặt hiển thị label dạng phần trăm
                    seriesTopSPBanChay.Label.TextPattern = "{A}: {VP:p1}";

                    // Tuỳ chỉnh giao diện pie view nếu cần
                    PieSeriesView pieView = (PieSeriesView)seriesTopSPBanChay.View;
                    pieView.RuntimeExploding = true; // click vào slice để bung ra
                    pieView.ExplodedDistancePercentage = 5;

                    // Hiển thị chú thích
                    //chartControl1.Legend.Visibility = DefaultBoolean.True;
                    //chartControl1.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
                    //chartControl1.Legend.AlignmentVertical = LegendAlignmentVertical.Center;
                    //chartControl1.Legend.Direction = LegendDirection.TopToBottom;
                    //chartControl1.Legend.Margins.All = 10;

                }));
            }));
        }


        private void BarButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {            
            GetChart();
        }
    }
}
