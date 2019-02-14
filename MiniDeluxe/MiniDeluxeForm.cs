using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.Collections;
using RIOX;


namespace MiniDeluxe
{
    public partial class MiniDeluxeForm : Form
    {
        private readonly MiniDeluxe _parent;
        private RIOX.RIOXClient _c;

        public MiniDeluxeForm(MiniDeluxe parent)
        {
            _parent = parent;
            InitializeComponent();
        }

        private void MiniDeluxeForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;

            ToolTip t = new ToolTip();
            t.AutoPopDelay = 10000;
            t.InitialDelay = 1000;
            t.ReshowDelay = 500;

            lblStatus.Text = _parent.HRDTCPServer_IsListening() ? "Server enabled." : "Server disabled.";

            foreach (String port in SerialPort.GetPortNames())
                cbSerialport.Items.Add(port);

            try
            {
                //if(Properties.Settings.Default.SerialPortIdx != 0)
                cbSerialport.SelectedIndex = Properties.Settings.Default.SerialPortIdx;
            }
            catch (Exception)
            {

            }

            tbPort.Text = Properties.Settings.Default.Port.ToString();
            tbLogPort.Text = Properties.Settings.Default.LogPort.ToString();
            tbHigh.Text = Properties.Settings.Default.HighInterval.ToString();
            tbLow.Text = Properties.Settings.Default.LowInterval.ToString();
            cbLocalOnly.Checked = Properties.Settings.Default.LocalOnly;
            txtRIOXIP.Text = Properties.Settings.Default.RIOXIP;
            txtRIOXport.Text = Properties.Settings.Default.RIOXPort.ToString();
            cbPSDR.Checked = Properties.Settings.Default.PSDR;
            cbSSDR.Checked = Properties.Settings.Default.SSDR;
            cbRIOX.Checked = Properties.Settings.Default.RIOX;
            cbN1MM.Checked = Properties.Settings.Default.N1MM;
            cbLog4OM.Checked = Properties.Settings.Default.Log4OM;
            log4omService.Text = Properties.Settings.Default.Log4OMService;
            log4omDBServer.Text = Properties.Settings.Default.Log4OMDBServer;
            log4omDBName.Text = Properties.Settings.Default.Log4OMDBName;
            log4omDBUser.Text = Properties.Settings.Default.Log4OMDBUser;
            log4omDBPass.Text = Properties.Settings.Default.Log4OMDBPass;
            cbQRZcom.Checked = Properties.Settings.Default.QRZcom;
            QRZUsername.Text = Properties.Settings.Default.QRZUsername;
            QRZPassword.Text = Properties.Settings.Default.QRZPassword;
            QRZdburl.Text=Properties.Settings.Default.QRZdburl;
            radioPort.Text = Properties.Settings.Default.RadioPort.ToString();

        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveSettings();
                _parent.Restart();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to save: " + ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        private void SaveSettings()
        {
            try
            {
                Properties.Settings.Default.SerialPortIdx = cbSerialport.SelectedIndex;
                Properties.Settings.Default.SerialPort = cbSerialport.Text;
                Properties.Settings.Default.Port = int.Parse(tbPort.Text);
                Properties.Settings.Default.LogPort = int.Parse(tbLogPort.Text);
                Properties.Settings.Default.HighInterval = double.Parse(tbHigh.Text);
                Properties.Settings.Default.LowInterval = double.Parse(tbLow.Text);
                Properties.Settings.Default.FirstRun = false;
                Properties.Settings.Default.LocalOnly = cbLocalOnly.Checked;
                Properties.Settings.Default.SSDR = cbSSDR.Checked;
                Properties.Settings.Default.PSDR = cbPSDR.Checked;
                Properties.Settings.Default.RIOX = cbRIOX.Checked;
                Properties.Settings.Default.N1MM = cbN1MM.Checked;
                Properties.Settings.Default.RIOXIP = txtRIOXIP.Text;
                Properties.Settings.Default.RIOXPort = int.Parse(txtRIOXport.Text);
                Properties.Settings.Default.RadioPort = int.Parse(radioPort.Text);
                Properties.Settings.Default.Log4OM = cbLog4OM.Checked;
                Properties.Settings.Default.Log4OMService = log4omService.Text;
                Properties.Settings.Default.Log4OMDBServer = log4omDBServer.Text;
                Properties.Settings.Default.Log4OMDBName = log4omDBName.Text;
                Properties.Settings.Default.Log4OMDBUser = log4omDBUser.Text;
                Properties.Settings.Default.Log4OMDBPass = log4omDBPass.Text;
                Properties.Settings.Default.QRZcom = cbQRZcom.Checked;
                Properties.Settings.Default.QRZUsername = QRZUsername.Text;
                Properties.Settings.Default.QRZPassword = QRZPassword.Text;
                Properties.Settings.Default.QRZdburl = QRZdburl.Text;
                Properties.Settings.Default.Save();
            }
            catch (Exception e)
            {
                throw new Exception("Save failed: " + e.Message);
            }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            //throw new Exception("AboutBoxException");
            AboutBox a = new AboutBox();
            a.Show();
        }

        private void btnCheckForUpdate_Click(object sender, EventArgs e)
        {
            /* disabled for now
            String updateUrl = CheckForUpdate.CheckForXMLUpdate("http://vhfwiki.com/xml/minideluxe.xml");
            if(updateUrl.Equals(String.Empty))
            {            
                MessageBox.Show("No updates available.");
                return;
            }

            const string title = "Update available!";
            const string question = "Open web browser to download new version?";
            if (DialogResult.Yes ==
             MessageBox.Show(this, question, title,
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question))
            {
                System.Diagnostics.Process.Start(updateUrl);
            }            
            */
        }

        private void btnRIOXTest_Click(object sender, EventArgs e)
        {
            try
            {
                //DDUtilState.RadioData r = new DDUtilState.RadioData();
                RIOX.RIOXData r = new RIOX.RIOXData();
                _c = new RIOX.RIOXClient(r.GetType(), txtRIOXIP.Text, int.Parse(txtRIOXport.Text));
                _c.SendCommand(new RIOX.RIOXCommand("UpDateType", "PSH:500"));
                _c.SendCommand(new RIOX.RIOXCommand("Sub", "ZZFA;"));
                _c.ObjectReceivedEvent += new RIOX.RIOXClient.ObjectReceivedEventHandler(c_ObjectReceivedEvent);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failure: " + ex.Message);
            }
        }

        void c_ObjectReceivedEvent(object o, RIOX.RIOXClient.ObjectReceivedEventArgs e)
        {
            //DDUtilState.RadioData r = (DDUtilState.RadioData)e.DataObject;
            RIOX.RIOXData r = (RIOX.RIOXData)e.DataObject;
            Console.WriteLine("R: " + r.ToString());
            foreach (DictionaryEntry de in r)
            {
                Console.WriteLine("Key: {0} Value {1}", de.Key, de.Value);
            }
            MessageBox.Show("Success: Received Frequency: " + r["ZZFA"]);
            _c.Close();
        }

        private void cbSSDR_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cbSerialport_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbPort_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void cbSmart_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSSDR.Checked)
            {
                gbSSDR.Enabled = true;
                cbPSDR.Checked = false; // Force SSDR disabled if PSDR is enabled.
                cbN1MM.Checked = false;
                cbRIOX.Checked = false;
            }
            else
            {
                gbSSDR.Enabled = false;
            }
        }

        private void cbPSDR_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPSDR.Checked)
            {
                gbPSDR.Enabled = true;
                cbSSDR.Checked = false; // Force SSDR disabled if PSDR is enabled.
                cbN1MM.Checked = false;
                cbRIOX.Checked = false;
            }
            else
            {
                gbPSDR.Enabled = false;
            }
        }

        private void cbRIOX_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRIOX.Checked)
            {
                gbRIOX.Enabled = true;
                cbSSDR.Checked = false; // Force SSDR disabled if PSDR is enabled.
                cbPSDR.Checked = false;
                cbN1MM.Checked = false;
            }
            else
            {
                gbRIOX.Enabled = false;
            }

        }

        private void cbN1MM_CheckedChanged(object sender, EventArgs e)
        {
            if (cbN1MM.Checked)
            {
                gbN1MM.Enabled = true;
                cbPSDR.Checked = false;
                cbSSDR.Checked = false;
                cbRIOX.Checked = false;
            }
            else
            {
                gbN1MM.Enabled = false;
            }

        }

        private void MiniDeluxeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void SaveClose_Click(object sender, EventArgs e)
        {
            try
            {
                SaveSettings();
                _parent.Restart();
                (this).Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to save: " + ex.Message + "\r\n" + ex.StackTrace);
                (this).Hide();
            }

        }


        private void cbLog4OM_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLog4OM.Checked)
            {
                gbLog4OM.Enabled = true;
            }
            else
            {
                gbLog4OM.Enabled = false;
            }
        }

        private void cbQRZcom_CheckedChanged(object sender, EventArgs e)
        {
            if (cbQRZcom.Checked)
            {
                gbQRZcom.Enabled = true;
            }
            else
            {
                gbQRZcom.Enabled = false;
            }

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_3(object sender, EventArgs e)
        {

        }
    }
}
