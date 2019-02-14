using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Xml;
using MySql.Data.MySqlClient;


namespace MiniDeluxe
{
    class Log4OMConnector
    {
        //public event Log4OMEventHandler Log4OMEvent;

        private readonly MiniDeluxe _parent;

        public bool isConnected=false;
        private log4om.log4omService.WCFServiceClient _log4om;
        private MySqlConnection _mysql;
        public bool dbConnected=true;

        public Log4OMConnector(MiniDeluxe parent)
        {

            try
            {
                _log4om = new log4om.log4omService.WCFServiceClient();

                if (!_log4om.ConnectionTest())
                {
                    isConnected = false;
                    MiniDeluxe.Debug("Failed to Connect to Log4OM");
                }
                else
                {
                    isConnected = true;
                    MiniDeluxe.Debug("Successfully Connected to Log4OM");
                }
                // Connect to Database
                _mysql = new MySqlConnection("SERVER=" + Properties.Settings.Default.Log4OMDBServer + ";" + 
                                                "DATABASE=" + Properties.Settings.Default.Log4OMDBName + ";" +
                                                "UID="+ Properties.Settings.Default.Log4OMDBUser + ";" +
                                                "PASSWORD="+ Properties.Settings.Default.Log4OMDBPass + ";");
                try
                {
                    _mysql.Open();
                } catch (Exception ex)
                {
                    dbConnected = false;
                    throw new Exception("Failed to connect to Log4OM Database: " + ex.ToString());
                }

                _parent = parent;
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong with Log4OM: " + ex.ToString());
            }
        }

        public bool InsertQSO(String ADIF)
        {
            if (isConnected)
            {
                _log4om.AddQSO(ADIF);
            }

            return true;
        }

        public void GetQSOs(String callsign)
        {
            if (dbConnected)
            {
            }

        }

        public String GetWorkedBands(String callsign)
        {

            MiniDeluxe.Debug("GetWorkedBands running");
            String ret = "";
            if (dbConnected)
            {
                try
                {
                    // There is NO getqso function within the Log4OM WCF service so will need to query MySQL directly!
                    String query = "select `band` from `log` where `call`='" + callsign + "' group by `band` order by `band`";
                    MiniDeluxe.Debug("Query: " + query);
                    MySqlCommand cmd = new MySqlCommand(query, _mysql);
                    MySqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ret = ret + dr["band"] + ",";
                    }
                    dr.Close();
                    ret = ret.TrimEnd(','); // Remove final comma
                    MiniDeluxe.Debug("Found bands:" + ret);
                }
                catch (Exception ex)
                {
                    MiniDeluxe.Debug("MySQL Error: " + ex.ToString());
                }
            }

            return ret;
        }

        public String GetBandModes(String callsign, String band)
        {

            MiniDeluxe.Debug("GetBandModes running");
            String ret = "";
            if (dbConnected)
            {
                try
                {
                    // There is NO getqso function within the Log4OM WCF service so will need to query MySQL directly!
                    String query = "select `mode` from `log` where `call`='" + callsign + "' and `band`='" + band + "' group by `mode` order by `mode`";
                    MiniDeluxe.Debug("Query: " + query);
                    MySqlCommand cmd = new MySqlCommand(query, _mysql);
                    MySqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ret = ret + dr["mode"] + ",";
                    }
                    dr.Close();
                    ret = ret.TrimEnd(','); // Remove final comma
                    MiniDeluxe.Debug("Found modes:" + ret);
                }
                catch (Exception ex)
                {
                    MiniDeluxe.Debug("MySQL Error: " + ex.ToString());
                }
            }

            return ret;
        }

        public int GetWorkedThisBand(String callsign, String band)
        {

            MiniDeluxe.Debug("GetWorkedThisBand running");
            int ret = -1;
            if (dbConnected)
            {
                try
                {
                    // There is NO getqso function within the Log4OM WCF service so will need to query MySQL directly!
                    String query = "select count(*) from `log` where `call`='" + callsign + "' and `band`='" + band + "'";
                    MiniDeluxe.Debug("Query: " + query);
                    MySqlCommand cmd = new MySqlCommand(query, _mysql);
                    ret = int.Parse(cmd.ExecuteScalar()+"");
                    MiniDeluxe.Debug("Got Count of QSOs:" + ret.ToString());
                }
                catch (Exception ex)
                {
                    MiniDeluxe.Debug("MySQL Error: " + ex.ToString());
                }
            }

            return ret;
        }



        public void Close()
        {
            if (_log4om != null)
                _log4om.Abort();
            if (_mysql != null)
                _mysql.Close();
        }

    }

    public delegate void Log4OMEventHandler(object sender, N1MMEventArgs e);
    public class Log4OMEventArgs : EventArgs
    {
        public String Data { get; set; }
        public String Command { get; set; }

        public Log4OMEventArgs(String command, String data)
        {
            Command = command;
            Data = data;
        }
    }

}