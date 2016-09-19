using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Globalization;

using System.Net.NetworkInformation;
using Microsoft.Win32;

using PowerCalibration;

namespace PrintTest
{
    class SerialNumber
    {
        static SqlConnectionStringBuilder _constr = new SqlConnectionStringBuilder(Properties.Settings.Default.DBConnectionString);
        public static SqlConnectionStringBuilder ConnectionSB { get { return _constr; } set { _constr = value; } }

        static PowerCalibration.ManufacturingStore_DataContext _data_context =
            new PowerCalibration.ManufacturingStore_DataContext(ConnectionSB.ConnectionString);


        /// <summary>
        /// Gets the week number based on database current date
        /// </summary>
        /// <returns>4 digit week + year string</returns>
        static string getWeekYearNumber()
        {
            string weekyear_num = null;
            int year = -1;
            int week = -1;
            using (SqlConnection con = new SqlConnection(ConnectionSB.ConnectionString))
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

        public static string BuildSerial(int product_id, int serial_num)
        {

            PowerCalibration.ManufacturingStore_DataContext dc =
                new PowerCalibration.ManufacturingStore_DataContext(ConnectionSB.ConnectionString);

            int teststation_id = dc.TestStationMachines.Where(m => m.Name == Environment.MachineName).Select(s => s.Id).Single<int>();


            string week_number = getWeekYearNumber();

            string serial_number = string.Format("{0:D3}{1}", product_id, week_number);
            serial_number += string.Format("{0:D6}", serial_num);

            return serial_number;
        }

    }
}
