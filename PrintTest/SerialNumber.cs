using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Globalization;

using System.Net.NetworkInformation;
using Microsoft.Win32;

namespace PrintTest
{
    class SerialNumber
    {
        static SqlConnectionStringBuilder _db_con_str = new SqlConnectionStringBuilder(Properties.Settings.Default.DBConnectionString);

        static int _site_id = -1;
        static public int Site_ID
        {
            get
            {
                if (_site_id < 0)
                {
                    try
                    {
                        string macaddr_str = GetMacAndIpAddress().Item1;
                        ManufacturingStore_DataDataContext dc = new ManufacturingStore_DataDataContext(_db_con_str.ConnectionString);
                        _site_id = dc.StationSites.Where(d => d.StationMac == macaddr_str).Select(s => s.ProductionSiteId).Single<int>();
                    }
                    catch{};
                }
                return _site_id;
            }
        }

        static int _machine_id = -1;
        static public int Machine_ID
        {
            get
            {
                if (_machine_id < 0)
                {
                    try
                    {
                        ManufacturingStore_DataDataContext dc = new ManufacturingStore_DataDataContext(_db_con_str.ConnectionString);
                        try
                        {

                            // Set machine id
                            // This is commented out because we are updating data below
                            //_machine_id = dc.TestStationMachines.Where(m => m.Name == Environment.MachineName).Select(s => s.Id).Single<int>();

                            // Machine guid column was added after data had already been inserted
                            // Update data
                            TestStationMachine machine = dc.TestStationMachines.Single<TestStationMachine>(m => m.Name == Environment.MachineName);
                            _machine_id = machine.Id;
                            try
                            {
                                // Update machine GUI if null
                                if (machine.MachineGuid == null)
                                {
                                    machine.MachineGuid = GetMachineGuid();
                                    dc.SubmitChanges();
                                }
                            }
                            catch (Exception ex) { string m = ex.Message; };
                        }
                        catch { };

                        if (_machine_id < 0)
                        {
                            _machine_id = insertMachine();
                        }
                    }
                    catch (Exception ex) { string msg = ex.Message; };
                }

                return _machine_id;
            }
        }

        /// <summary>
        /// Gets the week number based on database current date
        /// </summary>
        /// <returns>4 digit week + year string</returns>
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

            ManufacturingStore_DataDataContext dc = new ManufacturingStore_DataDataContext(_db_con_str.ConnectionString);

            int teststation_id = dc.TestStationMachines.Where(m => m.Name == Environment.MachineName).Select(s => s.Id).Single<int>();


            string week_number = getWeekYearNumber();

            string serial_number = string.Format("{0:D3}{1}", product_id, week_number);

            return serial_number;
        }

        /// <summary>
        /// Gets the machine id from database
        /// It creates an entry if not found
        /// </summary>
        /// <returns></returns>
        public static int insertMachine()
        {
            var mac_ip = GetMacAndIpAddress();
            string macaddr_str = mac_ip.Item1;
            string ip_str = mac_ip.Item2;

            ManufacturingStore_DataDataContext dc = new ManufacturingStore_DataDataContext(_db_con_str.ConnectionString);

            string description = null;
            try { description = GetComputerDescription(); }
            catch (Exception) { };
            // Set a computer description based on domain and nic if one was not found
            if (description == null || description == "")
            {
                string production_site_name = null;
                try
                {
                    var ss = dc.StationSites.Where(d => d.StationMac == macaddr_str).Single();
                    production_site_name = ss.ProductionSite.Name;
                }
                catch (Exception) { };

                NetworkInterface nic = GetFirstNic();
                if (production_site_name != null)
                    description = string.Format("{0}, {1}", production_site_name, nic.Description);
                else
                    description = string.Format("{0}, {1}", Environment.UserDomainName, nic.Description);
            }


            TestStationMachine machine = new TestStationMachine();
            machine.Name = Environment.MachineName;
            machine.IpAddress = ip_str;
            machine.MacAddress = macaddr_str;
            machine.Description = description;
            try
            {
                string machineguid = GetMachineGuid();
                // Database should default to "00000000-0000-0000-0000-000000000000"
                // Just check to make sure we got the same length
                if (machineguid.Length <= 36)
                    machine.MachineGuid = machineguid;
            }
            catch { };

            dc.TestStationMachines.InsertOnSubmit(machine);
            dc.SubmitChanges();

            return machine.Id;

        }

        /// <summary>
        /// Gets the specified Nics mac and ip address
        /// </summary>
        /// <param name="nic"></param>
        /// <returns></returns>
        public static Tuple<string, string> GetMacAndIpAddress()
        {
            string macaddr_str = "000000000000";
            string ip_str = "0.0.0.0";

            // Get the first network interface
            NetworkInterface nic = null;
            try { nic = GetFirstNic(); }
            catch (Exception) { };
            if (nic != null)
            {
                try
                {
                    macaddr_str = nic.GetPhysicalAddress().ToString();
                    foreach (var ua in nic.GetIPProperties().UnicastAddresses)
                    {
                        if (ua.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            ip_str = ua.Address.ToString();
                            break;
                        }
                    }
                }
                catch (Exception) { };
            }

            return Tuple.Create(macaddr_str, ip_str);
        }


        /// <summary>
        /// Gets the first Network Interface of the system
        /// </summary>
        /// <returns>First Network Interface of the system</returns>
        public static NetworkInterface GetFirstNic()
        {
            //var myInterfaceAddress = NetworkInterface.GetAllNetworkInterfaces()
            //    .Where(n => n.OperationalStatus == OperationalStatus.Up && n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
            //    .OrderByDescending(n => n.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
            //    .Select(n => n.GetPhysicalAddress())
            //    .FirstOrDefault();

            NetworkInterface myInterfaceAddress = NetworkInterface.GetAllNetworkInterfaces()
                .Where(n => n.OperationalStatus == OperationalStatus.Up && n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .OrderByDescending(n => n.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                .FirstOrDefault();

            return myInterfaceAddress;
        }

        /// <summary>
        /// Gets the machine gui id stored in the registry
        /// </summary>
        /// <returns></returns>
        public static string GetMachineGuid()
        {
            string location = @"SOFTWARE\Microsoft\Cryptography";
            string name = "MachineGuid";

            using (RegistryKey localMachineX64View =
                RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            {
                using (RegistryKey rk = localMachineX64View.OpenSubKey(location))
                {
                    if (rk == null)
                        throw new KeyNotFoundException(
                            string.Format("Key Not Found: {0}", location));

                    object machineGuid = rk.GetValue(name);
                    if (machineGuid == null)
                        throw new IndexOutOfRangeException(
                            string.Format("Index Not Found: {0}", name));

                    return machineGuid.ToString();
                }
            }
        }

        /// <summary>
        /// Returns the computer description
        /// </summary>
        /// <returns>the computer description</returns>
        public static string GetComputerDescription()
        {
            string key = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\lanmanserver\parameters";
            string computerDescription = (string)Registry.GetValue(key, "srvcomment", null);

            return computerDescription;
        }

    }
}
