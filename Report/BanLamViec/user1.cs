using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace QuanLyBanHang.Report.BanLamViec
{
    public partial class user1 : DevExpress.XtraEditors.XtraUserControl
    {
        public user1()
        {
            InitializeComponent();
            dteFromDate.EditValue = DateTime.Now.ToString("01/MM/yyyy");
            dteToDate.EditValue = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void user1_Load(object sender, EventArgs e)
        {
            GetChart();
        }

        public async void GetChart()
        {
            try
            {
                chartControl1.Series.Clear();
                var dataTable = ExecSQL.ExecProcedureDataAsDataTable("spThongKeDoanhSo", new
                {
                    TuNgay = Convert.ToDateTime(dteFromDate.EditValue),
                    DenNgay = Convert.ToDateTime(dteToDate.EditValue)
                });
                await Task.Factory.StartNew(new Action(() =>
                {

                    chartControl1.DataSource = dataTable;
                    chartControl1.BeginInvoke(new Action(() =>
                    {
                        Series seriesGiatri = new Series("Doanh số", ViewType.Bar);
                        seriesGiatri.LabelsVisibility = DefaultBoolean.True;

                        foreach (DataRow dr in dataTable.Rows)
                        {
                            seriesGiatri.Points.Add(new SeriesPoint(dr["ThangNam"], dr["ThanhTien"]));
                        }

                        seriesGiatri.Label.TextPattern = "{V:#,##0}";

                        chartControl1.Series.AddRange(new[] { seriesGiatri });
                        chartControl1.Legend.Visibility = DefaultBoolean.True;

                        Legend legend = chartControl1.Legend;
                        // chartControl1.Legend.AlignmentVertical = LegendAlignmentVertical.Center
                        legend.Margins.All = 8;
                        legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
                        legend.AlignmentVertical = LegendAlignmentVertical.Top;
                        legend.Direction = LegendDirection.LeftToRight;

                    }));
                }));
            }
            catch (Exception ex)
            {
              
            }
            
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetChart();
        }
    }
}
