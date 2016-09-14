using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Globalization;

namespace PrintTest
{
    class SerialNumber
    {
        static SqlConnectionStringBuilder _db_con_str = new SqlConnectionStringBuilder(Properties.Settings.Default.DBConnectionString);

        /// <summary>
        /// Gets the week number based on database current date
        /// </summary>
        /// <returns>week number of -1</returns>
        static string getWeekYearNumber()
        {
            string weekyear_num = null;
            int year = -1;
            int week = -1;
            using (SqlConnection con = new SqlConnection(_db_con_str.ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("Select getdate()", con))
                {
                    DateTime date = (DateTime)cmd.ExecuteScalar();

                    week = DateTimeFormatInfo.CurrentInfo.Calendar.GetWeekOfYear(
                        date, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);

                    year = DateTimeFormatInfo.CurrentInfo.Calendar.GetYear(date);


                }
                con.Close();
            }

            if (year < 0 || week < 0)
            {
                throw new Exception("Unable to determine week or year info");
            }

            string year_str = year.ToString().Substring(2);
            weekyear_num = string.Format("{0:D2}{1}", week, year_str);
            return weekyear_num;
        }

        public static string BuildSerial(int product_id)
        {

            string week_number = getWeekYearNumber();

            string serial_number = string.Format("{0:D3}{1}", product_id, week_number);

            return serial_number;
        }
    }
}
