using System;
using System.Data;
using System.Globalization;

namespace QuanLyBanHang.Class
{
    public static class DateTimeExtensions
    {
        public static DateTime? TryParseNullable(string val)
        {
            DateTime outValue;
            if (DateTime.TryParse(val, out outValue))
            {
                if (outValue.ToString("dd/MM/yyyy") == "01/01/0001")
                {
                    return null;
                }
                else
                {
                    return outValue;
                }
            }
            else
            {
                return null;
            }
        }
        public static DataTable GetDateOfWeek(this DateTime date)
        {
            var table = new DataTable();
            var cultures = CultureInfo.CreateSpecificCulture("vi-VN");
            table.Columns.Add("id", typeof(int));
            table.Columns.Add("name", typeof(string));
            table.Columns.Add("from", typeof(DateTime));
            table.Columns.Add("to", typeof(DateTime));

            table.Rows.Add(1, "Hôm nay", DateTime.Parse(date.Date.ToString("dd/MM/yyyy"), cultures), DateTime.Parse(date.Date.ToString("dd/MM/yyyy"), cultures));
            table.Rows.Add(28, "Hôm qua", DateTime.Parse(date.AddDays(-1).ToString("dd/MM/yyyy"), cultures), DateTime.Parse(date.AddDays(-1).ToString("dd/MM/yyyy"), cultures));
            table.Rows.Add(2, "Tuần này", DateTime.Parse(date.FirstDayOfWeek().ToString("dd/MM/yyyy"), cultures), DateTime.Parse(date.LastDayOfWeek().ToString("dd/MM/yyyy"), cultures));
            table.Rows.Add(3, "Đầu tuần đến hiện tại", DateTime.Parse(date.FirstDayOfWeek().ToString("dd/MM/yyyy"), cultures), DateTime.Parse(date.Date.ToString("dd/MM/yyyy"), cultures));
            table.Rows.Add(4, "Tháng này", DateTime.Parse(date.ToString("01/MM/yyyy"), cultures), DateTime.Parse(date.LastDayOfMonth().ToString("dd/MM/yyyy"), cultures));
            table.Rows.Add(5, "Đầu tháng đến hiện tại", DateTime.Parse(date.ToString("01/MM/yyyy"), cultures), DateTime.Parse(date.ToString("dd/MM/yyyy"), cultures));
            string quyfrom = "", quyto = "";
            Get_Quarter(date, ref quyfrom, ref quyto);
            table.Rows.Add(6, "Quý này", DateTime.Parse(quyfrom, cultures), DateTime.Parse(quyto, cultures));
            table.Rows.Add(7, "Đầu quý đến hiện tại", quyfrom, DateTime.Parse(date.Date.ToString("dd/MM/yyyy"), cultures));
            table.Rows.Add(8, "Năm này", DateTime.Parse(date.ToString("01/01/yyyy"), cultures), DateTime.Parse(date.ToString("31/12/yyyy"), cultures));
            table.Rows.Add(9, "Đầu năm đến hiện tại", DateTime.Parse(date.ToString("01/01/yyyy"), cultures), DateTime.Parse(date.Date.ToString("dd/MM/yyyy"), cultures));
            table.Rows.Add(10, "6 tháng đầu năm", DateTime.Parse(date.ToString("01/01/yyyy"), cultures), DateTime.Parse(date.ToString("30/06/yyyy"), cultures));
            table.Rows.Add(11, "6 tháng cuối năm", DateTime.Parse(date.ToString("01/07/yyyy"), cultures), DateTime.Parse(date.ToString("31/12/yyyy"), cultures));
            table.Rows.Add(12, "Tháng 1", DateTime.Parse(date.ToString("01/01/yyyy"), cultures), DateTime.Parse(date.ToString("31/01/yyyy"), cultures));
            table.Rows.Add(13, "Tháng 2", DateTime.Parse(date.ToString("01/02/yyyy"), cultures), Convert.ToDateTime(date.ToString("01/02/yyyy"), cultures).LastDayOfMonth());
            table.Rows.Add(14, "Tháng 3", DateTime.Parse(date.ToString("01/03/yyyy"), cultures), DateTime.Parse(date.ToString("31/03/yyyy"), cultures));
            table.Rows.Add(15, "Tháng 4", DateTime.Parse(date.ToString("01/04/yyyy"), cultures), DateTime.Parse(date.ToString("30/04/yyyy"), cultures));
            table.Rows.Add(16, "Tháng 5", DateTime.Parse(date.ToString("01/05/yyyy"), cultures), DateTime.Parse(date.ToString("31/05/yyyy"), cultures));
            table.Rows.Add(17, "Tháng 6", DateTime.Parse(date.ToString("01/06/yyyy"), cultures), DateTime.Parse(date.ToString("30/06/yyyy"), cultures));
            table.Rows.Add(18, "Tháng 7", DateTime.Parse(date.ToString("01/07/yyyy"), cultures), DateTime.Parse(date.ToString("31/07/yyyy"), cultures)); table.Rows.Add(19, "Tháng 8", DateTime.Parse(date.ToString("01/08/yyyy"), cultures), DateTime.Parse(date.ToString("31/08/yyyy"), cultures));
            table.Rows.Add(20, "Tháng 9", DateTime.Parse(date.ToString("01/09/yyyy"), cultures), DateTime.Parse(date.ToString("30/09/yyyy"), cultures));
            table.Rows.Add(21, "Tháng 10", DateTime.Parse(date.ToString("01/10/yyyy"), cultures), DateTime.Parse(date.ToString("31/10/yyyy"), cultures));
            table.Rows.Add(22, "Tháng 11", DateTime.Parse(date.ToString("01/11/yyyy"), cultures), DateTime.Parse(date.ToString("30/11/yyyy"), cultures));
            table.Rows.Add(23, "Tháng 12", DateTime.Parse(date.ToString("01/12/yyyy"), cultures), DateTime.Parse(date.ToString("31/12/yyyy"), cultures));
            table.Rows.Add(24, "Quý 1", DateTime.Parse(date.ToString("01/01/yyyy"), cultures), DateTime.Parse(date.ToString("31/03/yyyy"), cultures));
            table.Rows.Add(25, "Quý 2", DateTime.Parse(date.ToString("01/04/yyyy"), cultures), DateTime.Parse(date.ToString("30/06/yyyy"), cultures));
            table.Rows.Add(26, "Quý 3", DateTime.Parse(date.ToString("01/07/yyyy"), cultures), DateTime.Parse(date.ToString("30/09/yyyy"), cultures));
            table.Rows.Add(27, "Quý 4", DateTime.Parse(date.ToString("01/10/yyyy"), cultures), DateTime.Parse(date.ToString("31/12/yyyy"), cultures));
            table.Rows.Add(29, "Tuần trước", DateTime.Parse(date.AddDays(-7).FirstDayOfWeek().ToString("dd/MM/yyyy"), cultures), DateTime.Parse(date.AddDays(-7).LastDayOfWeek().ToString("dd/MM/yyyy"), cultures));
            table.Rows.Add(30, "Tháng trước", DateTime.Parse(date.AddMonths(-1).FirstDayOfMonth().ToString("dd/MM/yyyy"), cultures), DateTime.Parse(date.AddDays(-7).AddMonths(-1).LastDayOfMonth().ToString("dd/MM/yyyy"), cultures));
            table.Rows.Add(31, "Năm trước", DateTime.Parse(date.AddYears(-1).ToString("01/01/yyyy"), cultures), DateTime.Parse(date.AddYears(-1).ToString("01/12/yyyy"), cultures));
            table.Rows.Add(32, "Tuần sau", DateTime.Parse(date.AddDays(7).FirstDayOfWeek().ToString("dd/MM/yyyy"), cultures), DateTime.Parse(date.AddDays(7).LastDayOfWeek().ToString("dd/MM/yyyy"), cultures));
            table.Rows.Add(33, "Tháng sau", DateTime.Parse(date.AddMonths(1).FirstDayOfMonth().ToString("dd/MM/yyyy"), cultures), DateTime.Parse(date.AddDays(7).AddMonths(1).LastDayOfMonth().ToString("dd/MM/yyyy"), cultures));
            table.Rows.Add(34, "Năm sau", DateTime.Parse(date.AddYears(1).ToString("01/01/yyyy"), cultures), DateTime.Parse(date.AddYears(1).ToString("01/12/yyyy"), cultures));
            return table;
        }

        public static void Get_Quarter(DateTime date, ref string from, ref string to)
        {
            int quarterNumber = (date.Month - 1) / 3 + 1;
            var firstDayOfQuarter = new DateTime(date.Year, (quarterNumber - 1) * 3 + 1, 1);
            from = new DateTime(date.Year, (quarterNumber - 1) * 3 + 1, 1).ToString("dd/MM/yyyy");
            to = firstDayOfQuarter.AddMonths(3).AddDays(-1).ToString("dd/MM/yyyy");
        }
        public static DateTime FirstDayOfWeek(this DateTime dt)
        {
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var diff = dt.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }

            return dt.AddDays(-diff + 1).Date;
        }

        public static DateTime LastDayOfWeek(this DateTime dt)
        {
            return dt.FirstDayOfWeek().AddDays(6);
        }

        public static DateTime FirstDayOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }

        public static DateTime LastDayOfMonth(this DateTime dt)
        {
            return dt.FirstDayOfMonth().AddMonths(1).AddDays(-1);
        }

        public static DateTime FirstDayOfNextMonth(this DateTime dt)
        {
            return dt.FirstDayOfMonth().AddMonths(1);
        }
    }
}
