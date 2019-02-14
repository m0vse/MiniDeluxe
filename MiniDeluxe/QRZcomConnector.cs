using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Xml;
using System.Data;
using System.IO;

namespace MiniDeluxe
{
    class QRZcomConnector
    {
        public event QRZcomEventHandler QRZcomEvent;

        private readonly MiniDeluxe _parent;

        private string _dburl;
        private string _username;
        private string _password;
        private readonly Thread _readThread;

        private string xmlSession = "";
        private string xmlError = "";
        private string xmlMessage = "";


        private string _Lversion;
        private string _xmlError;
        private string _xmlSession;
        private string _xmlMessage;
        private string _LGMTime;
        private string _Lcount;
        private string _LSubExp;
        private string _LKey;



        public bool _isOnline = false;

        private WebClient wc = new WebClient();
        private DataSet QRZData = new DataSet("QData");
        private DataRow dr;

        public QRZcomConnector(MiniDeluxe parent)
        {
            _dburl = Properties.Settings.Default.QRZdburl;
            _username = Properties.Settings.Default.QRZUsername;
            _password = Properties.Settings.Default.QRZPassword;
            doLogin();
        }

        public string QRZField(DataRow row, string f)
        {
            if (row.Table.Columns.Contains(f)) return row[f].ToString(); else return "";
        }
        private void callQRZ(string url)
        {
            MiniDeluxe.Debug("callQRZ running with: "+url);
            Stream qrzstrm;
            try
            {
                QRZData.Clear();
                qrzstrm = wc.OpenRead(url);
                QRZData.ReadXml(qrzstrm, XmlReadMode.InferSchema);
                qrzstrm.Close();
                if (!QRZData.Tables.Contains("QRZDatabase"))
                {
                    MiniDeluxe.Debug("Error: failed to receive QRZDatabase object");
                    return;
                }
                DataRow dr = QRZData.Tables["QRZDatabase"].Rows[0];
                DataTable sess = QRZData.Tables["Session"];
                DataRow sr = sess.Rows[0];
                _xmlError = QRZField(sr, "Error");
                _xmlSession = QRZField(sr, "Key");
                _LGMTime = QRZField(sr, "GMTime");
                _Lcount = QRZField(sr, "Count");
                _LSubExp = QRZField(sr, "SubExp");
                _LKey = _xmlSession;

            }
            catch (Exception err)
            {
                MiniDeluxe.Debug("QRZ.com Error: "+err.Message);
            }
            _isOnline = (_xmlSession.Length > 0) ? true : false;
            MiniDeluxe.Debug("QRZ Session Key: " + _LKey);
        }

        public void doLogin()
        {
            string url = _dburl + "?username=" + _username + ";password=" + _password;
            callQRZ(url);
            if (_isOnline)
            {
                MiniDeluxe.Debug("Login OK");
            }
        }

        public DataRow GetFields ()
        {
            if (!QRZData.Tables.Contains("Callsign")) return null;
            DataTable callTable = QRZData.Tables["Callsign"];
            if (callTable.Rows.Count == 0) return null;
            DataRow dr = callTable.Rows[0];
            return dr;
        }

        public bool GetCallsign(string cs)
        {
            MiniDeluxe.Debug("Lookup Callsign: " + cs);
            if (cs.Length < 3) return false;
            string url = _dburl + "?s=" + _xmlSession + ";callsign=" + cs;
            callQRZ(url);
            if (!_isOnline)
            {
                doLogin();
                if (_isOnline)
                {
                    GetCallsign(cs);
                }
            }
            return true;
        }
    }



    public delegate void QRZcomEventHandler(object sender, QRZcomEventArgs e);
    public class QRZcomEventArgs : EventArgs
    {
        public String Data { get; set; }
        public String Command { get; set; }

        public QRZcomEventArgs(String command, String data)
        {
            Command = command;
            Data = data;
        }
    }
}