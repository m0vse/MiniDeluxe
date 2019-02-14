/* This file is part of MiniDeluxe.
   MiniDeluxe is free software: you can redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation, either version 3 of the License, or
   (at your option) any later version.

   MiniDeluxe is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   GNU General Public License for more details.

   You should have received a copy of the GNU General Public License
   along with MiniDeluxe.  If not, see <http://www.gnu.org/licenses/>.
   
   MiniDeluxe is Copyright (C) 2010 by K1FSY
*/
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.IO.Ports;
using System.Timers;
using System.IO;
using System.Data;
using System.Diagnostics;
using Microsoft.VisualBasic;
using RIOX;
using Flex.Smoothlake.FlexLib;

namespace MiniDeluxe
{
    public class MiniDeluxe
    {
        private Timer _timerShort;
        private Timer _timerLong;
        private CATConnector _cat;
        private RIOX.RIOXClient _riox;
        private HRDTCPServer _server;
        private HRDTCPServer _logbook;
        private N1MMConnector _n1mm;
        private DM780Connector _dm780;
        private Log4OMConnector _log4om;
        private volatile bool _inCommand;
        private QRZcomConnector _qrzcom;
        private readonly NotifyIcon _notifyIcon;
        private bool _stopping;
        //private bool _listenOnly;
        private bool _usingRIOX;
        private bool _usingN1MM;
        private bool _usingPSDR;
        private bool _usingSSDR;
        private bool _usingLOG4OM;
        private bool _usingQRZcom;
        private Radio _thisRadio;
        private Slice _thisSlice;
        private MiniDeluxeForm _form;

        RadioData _data;
        struct RadioData
        {
            private string _mode;
            private string _band;
            private string _displayMode;
            private string _agc;
            private string _smeter;
            private string _dspfilter;
            private string _preamp;
            public string vfoa;
            public string vfob;
            public string rawmode;
            public string dspfilters;
            public ArrayList dspfilterarray;
            public bool mox;            

            public string Mode
            {
                get { return _mode; }
                set
                {
                    rawmode = value;
                    switch (value)
                    {

                        // DIGU,DIGL,RTTY,CW,LSB,USB,AM
                        case "00":
                            _mode = "DIGU";
                            break;
                        case "01":
                            _mode = "DIGL";
                            break;
                        case "02":
                            _mode = "RTTY";
                            break;
                        case "03":
                            _mode = "CW";
                            break;
                        case "04":
                            _mode = "LSB";
                            break;
                        case "05":
                            _mode = "USB";
                            break;
                        case "06":
                            _mode = "AM";
                            break;
                        case "99":
                            _mode = "OFF";
                            break;
                        default:
                            _mode = value;
                            break;                            
                    }
                }
            }
            public string Band
            {
                get { return _band; }
                set
                {
                    switch (value)
                    {
                        case "160":
                            _band = "160m";
                            break;
                        case "080":
                            _band = "80m";
                            break;
                        case "060":
                            _band = "60m";
                            break;
                        case "040":
                            _band = "40m";
                            break;
                        case "030":
                            _band = "30m";
                            break;
                        case "020":
                            _band = "20m";
                            break;
                        case "017":
                            _band = "17m";
                            break;
                        case "015":
                            _band = "15m";
                            break;
                        case "012":
                            _band = "12m";
                            break;
                        case "010":
                            _band = "10m";
                            break;
                        case "006":
                            _band = "6m";
                            break;
                        case "002":
                            _band = "2m";
                            break;
                        case "888":
                            _band = "GEN";
                            break;
                        case "999":
                            _band = "WWV";
                            break;
                        default:
                            _band = value;
                            break;
                    }
                }
            }
            public string DisplayMode
            {
                get { return _displayMode; }
                set
                {
                    switch(value)
                    {
                        case "0":
                            _displayMode = "Spectrum";
                            break;
                        case "1":
                            _displayMode = "Panadapter";
                            break;
                        case "2":
                            _displayMode = "Scope";
                            break;
                        case "3":
                            _displayMode = "Phase";
                            break;
                        case "4":
                            _displayMode = "Phase2";
                            break;
                        case "5":
                            _displayMode = "Waterfall";
                            break;
                        case "6":
                            _displayMode = "Histogram";
                            break;
                        case "7":
                            _displayMode = "Off";
                            break;
                    }
                }
            }
            public string AGC
            {
                get { return _agc; }
                set
                {
                    switch (value)
                    {
                        case "0":
                            _agc = "Fixed";
                            break;
                        case "1":
                            _agc = "Long";
                            break;
                        case "2":
                            _agc = "Slow";
                            break;
                        case "3":
                            _agc = "Med";
                            break;
                        case "4":
                            _agc = "Fast";
                            break;
                        case "5":
                            _agc = "Custom";
                            break;
                    }
                }
            }
            public string Smeter
            {
                get { return _smeter; }
                set
                {
                    float i = (float.Parse(value) / 2f) - 121f;
                    if (i < -121) _smeter = "0";
                    else if (i < -115) _smeter = "1";
                    else if (i < -109) _smeter = "2";
                    else if (i < -103) _smeter = "3";
                    else if (i < -97) _smeter = "4";
                    else if (i < -91) _smeter = "5";
                    else if (i < -85) _smeter = "6";
                    else if (i < -79) _smeter = "7";
                    else if (i < -73) _smeter = "8";
                    else if (i < -63) _smeter = "9";
                    else if (i < -53) _smeter = "10";
                    else if (i < -43) _smeter = "11";
                    else if (i < -33) _smeter = "12";
                    else if (i < -23) _smeter = "13";
                    else if (i < -13) _smeter = "14";
                }
            }
            public string SSDRmeter
            {
                get { return _smeter; }
                set
                {
                    float i = float.Parse(value);
                    if (i < -121) _smeter = "14";
                    else if (i < -115) _smeter = "13";
                    else if (i < -109) _smeter = "12";
                    else if (i < -103) _smeter = "11";
                    else if (i < -97) _smeter = "10";
                    else if (i < -91) _smeter = "9";
                    else if (i < -85) _smeter = "8";
                    else if (i < -79) _smeter = "7";
                    else if (i < -73) _smeter = "6";
                    else if (i < -63) _smeter = "5";
                    else if (i < -53) _smeter = "4";
                    else if (i < -43) _smeter = "3";
                    else if (i < -33) _smeter = "2";
                    else if (i < -23) _smeter = "1";
                    else if (i < -13) _smeter = "0";
                }
            }
            public string DSPFilter
            {
                get { return _dspfilter;  }
                set
                {
                    try
                    {
                        _dspfilter = (String) dspfilterarray[int.Parse(value)];                        
                    }
                    catch (Exception)
                    {
                        _dspfilter = "UNKN";
                    }
                }
            }
            public string Preamp
            {
                get { return _preamp; }
                set
                {
                    switch (value)
                    {
                        case "0":
                            _preamp = "Off";
                            break;
                        case "1":
                            _preamp = "Low";
                            break;
                        case "2":
                            _preamp = "Med";
                            break;
                        case "3":
                            _preamp = "High";
                            break;


                    }
                }
            }

            public string SSDRPreamp
            {
                get { return _preamp; }
                set
                {
                    switch (value)
                    {
                        case "0":
                            _preamp = "-10";
                            break;
                        case "1":
                            _preamp = "+0";
                            break;
                        case "2":
                            _preamp = "+10";
                            break;
                        case "3":
                            _preamp = "+20";
                            break;
                        default:
                            _preamp = value;
                            break;

                    }
                }
            }

        }

        public MiniDeluxe()
        {
            _notifyIcon = new NotifyIcon(this);

            _form = new MiniDeluxeForm(this);

            if (Properties.Settings.Default.FirstRun)
                ShowOptionsForm();
            else
                Start();
        }

        class Command
        {
            public string id;
            public string callsign;
            public string logbook;
            public string band;
            public Dictionary<string, string> fields;
        }

        Command getLogbookCommand(String xml)
        {
            Command cmd = new Command();
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(xml);
                XmlNodeList nodes = doc.DocumentElement.SelectNodes("/HRDLogbookInterface/Command");
                foreach (XmlNode node in nodes)
                {
                    try
                    {
                        cmd.id = node.InnerText;
                        if (node.Attributes != null)
                        {
                            if (node.Attributes["Fields_count"] != null)
                            {
                                cmd.fields = new Dictionary<string, string>();
                                int fieldscount = Int32.Parse(node.Attributes["Fields_count"].Value);
                                Debug("Got " + fieldscount.ToString() + " ADIF fields");
                                // Get the fields:
                                for (int f = 0; f < fieldscount; f++)
                                {
                                    try
                                    {
                                        String name = "";
                                        String value = "";
                                        //Debug("Trying value: (Fields_value_" + f.ToString() + ")");
                                        if (node.Attributes["Fields_value_" + f.ToString()] != null)
                                            name = node.Attributes["Fields_value_" + f.ToString()].Value;

                                        if (node.Attributes["Values_value_" + f.ToString()] != null)   
                                            value = node.Attributes["Values_value_" + f.ToString()].Value;
                                        /* Correction to return frequency in Khz instead of Hz! */
                                        if (name != null && value != null) {
                                            if (name == "FREQ")
                                            {
                                                Decimal temp = Decimal.Parse(value);
                                                temp = temp / 1000000;
                                                value = temp.ToString();
                                            }

                                            cmd.fields.Add(name, value);
                                        }
                                        else { 
                                            Debug("Null value: (Fields_value_" + f.ToString() + ")");
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Debug("ADIF Field error: " + e.Message);
                                    }
                                }
                            }
                            if (node.Attributes["Callsign"] != null)
                                cmd.callsign = node.Attributes["Callsign"].Value;
                            if (node.Attributes["Band"] != null)
                                cmd.band = node.Attributes["Band"].Value;
                            if (node.Attributes["Logbook"] != null)
                                cmd.logbook = node.Attributes["Logbook"].Value;
                        }
                    }
                    catch (XmlException e)
                    {
                        Debug("Error Parsing node: "+e.Message);
                    }
                }
            }
            catch (XmlException e)
            {
                Debug("XML Parse Error:"+e.Message);
            }

            return cmd;
        }

        string setLogbookCommand(String header,Command cmd, Dictionary<string, string> resp)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement el = (XmlElement)doc.AppendChild(doc.CreateElement("HRDLogbookInterface"));
            XmlElement command = (XmlElement)el.AppendChild(doc.CreateElement("Command"));
            el.SetAttribute("xml:lang", "EN");
            el.SetAttribute("Description", "Layouts");
            el.SetAttribute("Created", DateTime.Now.ToString("dd-MMM-yyyy hh:mm"));

            command.InnerText = cmd.id;
            if (cmd.callsign != null)
                command.SetAttribute("Callsign", cmd.callsign);
            XmlElement result = (XmlElement)el.AppendChild(doc.CreateElement("Return"));
            // result.InnerText = "";
            foreach (var item in resp)
            {
                result.SetAttribute(item.Key, item.Value);
            }
            //<HRDLogbookInterface xml:lang=\"EN\" Description=\"Layouts\" Created=\"21-Aug-2017 18:35\"><Command>ResetCache</Command><Return/></HRDLogbookInterface>
            Debug("XML:" + header + doc.OuterXml);

            return header + doc.OuterXml;
        }


        void LogbookHRDTCPEvent(object sender, HRDTCPEventArgs e)
        {
            if (_inCommand)
                return;
            _inCommand = true;


            string myHeader = @"<?xml version=""1.0""?>
<!--                                                          -->
<!-- ===================== WARNING ========================== -->
<!-- | The contents of this file must not be changed if the | -->
<!-- | program is to operate correctly. To avoid problems,  | -->
<!-- | please do NOT edit this file.                        | -->
<!-- ======================================================== -->
<!--                                                          -->
<!--Filename..: HRDLogbookInterface                           -->
<!--Created...: 27/08/2017 20:23:29                           -->
<!--Computer..: PHIL-W81                                      -->
<!--Username..: Phil                                          -->
<!--Program...: Digital Master.exe                            -->
<!--                                                          -->
<!--HRDLogbookXML.cpp line 52                                 -->
<!--                                                          -->
";
            String s = e.ToString();
            BinaryWriter bw = new BinaryWriter(e.Client.GetStream());

            s = s.Remove(s.IndexOf('\0'));
            Command cmd = getLogbookCommand(s);


#if DEBUG
          //  Debug(String.Format("RX:{0}", s));
#endif

            Debug("Processing command: " + cmd.id);
            if (cmd.callsign != null)
                Debug("Received callsign: " + cmd.callsign);

            var responses =new Dictionary<string, string>();
            switch (cmd.id)
            {
                case "ResetCache":
                    //bw.Write(HRDMessage.HRDMessageToByteArray(myHeader + "<HRDLogbookInterface xml:lang=\"EN\" Description=\"Layouts\" Created=\"21-Aug-2017 18:35\"><Command>ResetCache</Command><Return/></HRDLogbookInterface>", true));
                    break;
                case "GetStatus":
                    responses.Add("Connected", "1");
                    responses.Add("Database", "My Logbook");
                    //bw.Write(HRDMessage.HRDMessageToByteArray(myHeader + "<HRDLogbookInterface xml:lang=\"EN\" Description=\"Layouts\" Created=\"21-Aug-2017 18:35\"><Command>GetStatus</Command><Return Connected=\"1\" Database=\"My Logbook\"/></HRDLogbookInterface>",true));
                    break;
                case "GetCountryNames":
                    String DXCCs="";
                    String Names="";
                    try
                    {
                        using (var reader = new StreamReader(@"ADIFcnt.txt"))
                        {
                            while (!reader.EndOfStream)
                            {
                                var line = reader.ReadLine();
                                var values = line.Split(',');
                                DXCCs = DXCCs + "\t" + values[1];
                                Names = Names + "\t" + values[0].Replace("\"", "");
                            }
                        }
                    } catch
                    {
                        DXCCs = "0";
                        Names = "Error getting countries";
                    }

                    responses.Add("DXCCs", DXCCs);
                    responses.Add("Names", Names);
                    //bw.Write(HRDMessage.HRDMessageToByteArray(myHeader + "<HRDLogbookInterface xml:lang=\"EN\" Description=\"Layouts\" Created=\"21-Aug-2017 18:35\"><Command>GetCountryNames</Command><Return DXCCs=\"\" Names=\"Test\tTest2\tTest3\tTest4\tTest5\"/></HRDLogbookInterface>", true));
                    break;
                case "KeepAlive":
                    responses.Add("Connected", "1");
                    //bw.Write(HRDMessage.HRDMessageToByteArray(myHeader + "<HRDLogbookInterface xml:lang=\"EN\" Description=\"Layouts\" Created=\"21-Aug-2017 18:35\"><Command>Keepalive</Command><Return Connected=\"1\"/></HRDLogbookInterface>",true));
                    break;
                case "GetDabataseNames":
                case "GetDatabaseNames":
                    responses.Add("Names", "My Logbook");
                    //bw.Write(HRDMessage.HRDMessageToByteArray(myHeader + "<HRDLogbookInterface xml:lang=\"EN\" Description=\"Layouts\" Created=\"21-Aug-2017 18:35\"><Command>GetDabataseNames</Command><Return Names=\"My Logbook\"/></HRDLogbookInterface>",true));
                    break;
                case "CallsignLookup":
                    String XMLText = "";
                    DataRow dr;
                    String name = "";
                    // First lets lookup the callsign
                    XmlDocument doc = new XmlDocument();
                    XmlElement el = (XmlElement)doc.AppendChild(doc.CreateElement("HRD"));
                    XmlElement body = (XmlElement)el.AppendChild(doc.CreateElement("body"));
                    el.SetAttribute("xml:lang", "EN");

                    if (_qrzcom != null)
                    {
                        if (_qrzcom.GetCallsign(cmd.callsign))
                        {
                            Debug("Getting callsign");
                            // result.InnerText = "";


                            dr = _qrzcom.GetFields();
                            try
                            {
                                name = _qrzcom.QRZField(dr, "fname") + " " + _qrzcom.QRZField(dr, "name");
                            }
                            catch {
                                Debug("Exception getting name");
                            }

                            if (name != "" && name != null)
                            {
                                Debug("Processing Valid Callsign");
                                foreach (DataColumn item in dr.Table.Columns)
                                {
                                    try
                                    {
                                        string column = item.ColumnName.ToString();
                                        if (column == "addr1") column = "Address1";
                                        else if (column == "addr2") column = "Address2";
                                        else if (column == "call") column = "Callsign";
                                        else if (column == "class") column = "Class";
                                        else if (column == "country") column = "Country";
                                        else if (column == "dxcc") column = "DXCC";
                                        else if (column == "email") column = "Email";
                                        else if (column == "eqsl") column = "Eqsl";
                                        else if (column == "fname") column = "FName";
                                        else if (column == "grid") column = "Gridsquare";
                                        else if (column == "iota") column = "IOTA";
                                        else if (column == "mqsl") column = "Mqsl";
                                        else if (column == "name") column = "Name";
                                        else if (column == "zip") column = "Zip";
                                        else if (column == "moddate") column = "LastUpdate";
                                        else if (column == "lat") column = "Latitude";
                                        else if (column == "lon") column = "Longitude";
                                        else if (column == "land") column = "Land";
                                        XmlElement temp = (XmlElement)body.AppendChild(doc.CreateElement(column));
                                        if (column == "Name")
                                            temp.InnerText = name;
                                        else
                                            temp.InnerText = dr[item].ToString();
                                    }
                                    catch {
                                        Debug("Exception in callsign processing");
                                    }
                                }
                            } else
                            {
                                Debug("No Callsign found");
                            }
                        }
                    }
                    XmlElement temp2 = (XmlElement)body.AppendChild(doc.CreateElement("Bands"));
                    temp2.InnerText = "";
                    if (cmd.callsign != null)
                        temp2.InnerText = _log4om.GetWorkedBands(cmd.callsign);
                    
                    XmlElement settings = (XmlElement)el.AppendChild(doc.CreateElement("settings"));
                    settings.SetAttribute("FirstName", "0");

                    XmlElement adif = (XmlElement)el.AppendChild(doc.CreateElement("adif"));
                    adif.SetAttribute("fields_count", "0");
                    adif.SetAttribute("values_count", "0");

                    //<settings FirstName=\"0\"/><adif fields_count=\"0\" values_count=\"0\"/>
                    XMLText = "<?xml version=\"1.0\"?>" + doc.OuterXml;
                    Debug("Original XML: " + XMLText);
                    responses.Add("Lookup", System.Net.WebUtility.HtmlEncode(XMLText));

                    //string test= "<?xml version=\"1.0\"?><HRD xml:lang=\"EN\"><body><Address1>104 Winstanley Drive</Address1><Address2>Leicester</Address2><Born>1972</Born><Callsign>M0VSE</Callsign><Class>AD</Class><Country>England</Country><cqzone>14</cqzone><DXCC>223</DXCC><Email>phil@m0vse.uk</Email><Eqsl>1</Eqsl><Fname>Phil</Fname><Gridsquare>IO92jp</Gridsquare><IOTA>EU-005</IOTA><ituzone>27</ituzone><Land>England</Land><LastUpdate>2016-09-26 06:51:29</LastUpdate><Latitude>52.645000</Latitude><Locref>3</Locref><Longitude>-1.210000</Longitude><Mqsl>1</Mqsl><Name>Taylor</Name><Zip>LE31PA</Zip><Bands></Bands></body><settings FirstName=\"0\"/><adif fields_count=\"0\" values_count=\"0\"/></HRD>";
                    //resp.text= "&lt;?xmlversion=&quot;1.0&quot;?&gt;&#xA;&lt;HRD xml:lang=&quot;EN&quot;&gt;&lt;body&gt;&lt;Address1&gt;104 Winstanley Drive&lt;/Address1&gt;&lt;Address2&gt;Leicester&lt;/Address2&gt;&lt;Born&gt;1972&lt;/Born&gt;&lt;Callsign&gt;M0VSE&lt;/Callsign&gt;&lt;Class&gt;AD&lt;/Class&gt;&lt;Country&gt;England&lt;/Country&gt;&lt;cqzone&gt;14&lt;/cqzone&gt;&lt;DXCC&gt;223&lt;/DXCC&gt;&lt;Email&gt;phil@m0vse.uk&lt;/Email&gt;&lt;Eqsl&gt;1&lt;/Eqsl&gt;&lt;Fname&gt;Phil&lt;/Fname&gt;&lt;Gridsquare&gt;IO92jp&lt;/Gridsquare&gt;&lt;IOTA&gt;EU-005&lt;/IOTA&gt;&lt;ituzone&gt;27&lt;/ituzone&gt;&lt;Land&gt;England&lt;/Land&gt;&lt;LastUpdate&gt;2016-09-26 06:51:29&lt;/LastUpdate&gt;&lt;Latitude&gt;52.645000&lt;/Latitude&gt;&lt;Locref&gt;3&lt;/Locref&gt;&lt;Longitude&gt;-1.210000&lt;/Longitude&gt;&lt;Mqsl&gt;1&lt;/Mqsl&gt;&lt;Name&gt;Taylor&lt;/Name&gt;&lt;Zip&gt;LE31PA&lt;/Zip&gt;&lt;Bands&gt;&lt;/Bands&gt;&lt;/body&gt;&lt;settings FirstName=&quot;0&quot;/&gt;&lt; adif fields_count=&quot;0&quot; values_count=&quot;0&quot;/&gt;&lt;/HRD&gt;&#xA";
                    //bw.Write(HRDMessage.HRDMessageToByteArray(myHeader + "<HRDLogbookInterface xml:lang=\"EN\" Description=\"Layouts\" Created=\"21-Aug-2017 18:35\"><Command Callsign=\"M0VSE\">CallSignLookup</Command><Return Lookup=\"&lt;?xmlversion=&quot;1.0&quot;?&gt;&#xA;&lt;HRDxml:lang=&quot;EN&quot;&gt;&lt;body&gt;&lt;Address1&gt;104 Winstanley Drive&lt;/Address1&gt;&lt;Address2&gt;Leicester&lt;/Address2&gt;&lt;Born&gt;1972&lt;/Born&gt;&lt;Callsign&gt;M0VSE&lt;/Callsign&gt;&lt;Class&gt;AD&lt;/Class&gt;&lt;Country&gt;England&lt;/Country&gt;&lt;cqzone&gt;14&lt;/cqzone&gt;&lt;DXCC&gt;223&lt;/DXCC&gt;&lt;Email&gt;phil@m0vse.uk&lt;/Email&gt;&lt;Eqsl&gt;1&lt;/Eqsl&gt;&lt;Fname&gt;Phil&lt;/Fname&gt;&lt;Gridsquare&gt;IO92jp&lt;/Gridsquare&gt;&lt;IOTA&gt;EU-005&lt;/IOTA&gt;&lt;ituzone&gt;27&lt;/ituzone&gt;&lt;Land&gt;England&lt;/Land&gt;&lt;LastUpdate&gt;2016-09-2606:51:29&lt;/LastUpdate&gt;&lt;Latitude&gt;52.645000&lt;/Latitude&gt;&lt;Locref&gt;3&lt;/Locref&gt;&lt;Longitude&gt;-1.210000&lt;/Longitude&gt;&lt;Mqsl&gt;1&lt;/Mqsl&gt;&lt;Name&gt;Taylor&lt;/Name&gt;&lt;Zip&gt;LE31PA&lt;/Zip&gt;&lt;Bands&gt;&lt;/Bands&gt;&lt;/body&gt;&lt;settingsFirstName=&quot;0&quot;/&gt;&lt;adiffields_count=&quot;0&quot;values_count=&quot;0&quot;/&gt;&lt;/HRD&gt;&#xA\"/></HRDLogbookInterface>", true));
                    break;
                case "SelectDabatase":
                case "SelectDatabase": // In case they EVER fix the spelling of Database!
                    responses.Add("Name", "My Logbook");
                    //bw.Write(HRDMessage.HRDMessageToByteArray(myHeader + "<HRDLogbookInterface xml:lang=\"EN\" Description=\"Layouts\" Created=\"21-Aug-2017 18:35\"><Command NAME=\"My Logbook\">SelectDabatase</Command><Return Name=\"My Logbook\"/></HRDLogbookInterface>",true));
                    break;
                case "CallsignsByBand":
                    responses.Add("Calls", "");
                    break;
                case "CallsignCountryStatus":
                    responses.Add("CallsignBands", _log4om.GetWorkedBands(cmd.callsign));
                    responses.Add("CallsignModesThisBand", _log4om.GetBandModes(cmd.callsign,cmd.band));
                    responses.Add("CountryBands", "");
                    responses.Add("CountryModesThisBand", "");
                    responses.Add("Country", "");
                    responses.Add("CallsignLastQSO", "");
                    responses.Add("CallsignWorkedThisBand", _log4om.GetWorkedThisBand(cmd.callsign,cmd.band).ToString());
                    responses.Add("CountryWorkedThisBand", "0");
                    responses.Add("DXCC", "0");
                    break;
                case "AddRecord":
                    // Create ADIF file:
                    string ADIF = "";
                    if (cmd.fields!=null)
                    {
                        foreach (var item in cmd.fields)
                        {
                           ADIF=ADIF+ String.Format("<{0}:{1}>{2}", item.Key, item.Value.Length, item.Value);
                        }
                        ADIF = ADIF + "<eor>";
                        _log4om.InsertQSO(ADIF);
                    }
                    Debug("ADIF:" + ADIF);
                    break;
                default:
                    Debug("Original text: " + s.ToString());
                    break;
              
            }
            bw.Write(HRDMessage.HRDMessageToByteArray(setLogbookCommand("",cmd,responses),true)); // Generate our response!
            GC.Collect();
            GC.WaitForPendingFinalizers();
            _inCommand = false;
        }

        void ServerHRDTCPEvent(object sender, HRDTCPEventArgs e)
        {

            String s = e.ToString().ToUpper();
            BinaryWriter bw = new BinaryWriter(e.Client.GetStream());

            s = s.Remove(s.IndexOf('\0'));

#if DEBUG
            //Debug(String.Format("RX: {0}", s));
#endif

            if (s.Contains("GET"))
                ProcessHRDTCPGetCommand(s,bw);                                                      
            else if(s.Contains("SET"))            
                ProcessHRDTCPSetCommand(s,bw);

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        void TimerShortElapsed(object sender, ElapsedEventArgs e)
        {
            _cat.WriteCommand("ZZIF;");
            _cat.WriteCommand("ZZFA;");
            _cat.WriteCommand("ZZFB;");
            _cat.WriteCommand("ZZSM0;");
            _cat.WriteCommand("ZZFI;");
        }

        void TimerLongElapsed(object sender, ElapsedEventArgs e)
        {
            if (!_usingRIOX && !_usingN1MM && !_usingSSDR)
            {
                _cat.WriteCommand("ZZBS;");
                _cat.WriteCommand("ZZDM;");
                _cat.WriteCommand("ZZGT;");
                _cat.WriteCommand("ZZPA;");
                return;
            }
            else if (_usingRIOX)
            {
                // RIOX connection check
                if (_riox.IsConnected == false)
                {
                    SetNotifyIconText("MiniDeluxe - RIOX Disconnected (" + _server.ConnectionCount + " connections)");
                    _riox.Connect();
                }

                _riox.SendCommand(new RIOXCommand("Sub", "ZZIF;ZZFA;ZZFB;ZZBS;ZZDM;ZZGT;ZZPA;ZZFI;"));
            } else if (_usingN1MM)
            {
            }
        }


        void CatcatEvent(object sender, CATEventArgs e)
        {
            switch (e.Command)
            {
                // vfoa, mode, xmit status
                case "ZZIF":
                case "IF":
                    if (!_usingRIOX) // may be an issue with ddutil and zzif; will investigate
                        _data.vfoa = e.Data.Substring(0, 11);
                    // has the mode changed? if so, ask for new dsp string.
                    if (!_usingN1MM && !_data.rawmode.Equals(e.Data.Substring(27, 2)))
                        WriteCommand("ZZMN" + e.Data.Substring(27, 2) + "");
                    _data.Mode = e.Data.Substring(27, 2);
                    _data.mox = (e.Data.Substring(26, 1).Equals("1")) ? true : false;
                    break;
                case "ZZFA":
                    _data.vfoa = e.Data;
                    break;
                // vfob
                case "ZZFB":
                    _data.vfob = e.Data;
                    break;
                // band
                case "ZZBS":
                    _data.Band = e.Data;
                    break;
                // display mode
                case "ZZDM":
                    _data.DisplayMode = e.Data;
                    break;
                // agc
                case "ZZGT":
                    _data.AGC = e.Data;
                    break;
                // mode dsp filters
                case "ZZMN":
                    ProcessDSPFilters(e.Data);
                    break;
                case "ZZSM":
                    _data.Smeter = e.Data.Substring(1);
                    break;
                case "ZZTX":
                    _data.mox = true;
                    break;
                case "ZZFI":
                    _data.DSPFilter = e.Data;
                    break;
                case "ZZPA":
                    _data.Preamp = e.Data;
                    break;
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        void n1mmEvent(object sender, N1MMEventArgs e)
        {
            Console.WriteLine("Cmd: "+e.Command + " Data:"+e.Data);
            switch (e.Command)
            {
                // vfoa, mode, xmit status
                case "ZZIF":
                case "IF":
                    if (!_usingRIOX) // may be an issue with ddutil and zzif; will investigate
                        _data.vfoa = e.Data.Substring(0, 11);
                    // has the mode changed? if so, ask for new dsp string.
                    if (!_data.rawmode.Equals(e.Data.Substring(27, 2)))
                        WriteCommand("ZZMN" + e.Data.Substring(27, 2) + "");
                    _data.Mode = e.Data.Substring(27, 2);
                    _data.mox = (e.Data.Substring(26, 1).Equals("1")) ? true : false;
                    break;
                case "ZZFA":
                    _data.vfoa = e.Data;
                    break;
                // vfob
                case "ZZFB":
                    _data.vfob = e.Data;
                    break;
                // band
                case "ZZBS":
                    _data.Band = e.Data;
                    break;
                // display mode
                case "ZZDM":
                    _data.DisplayMode = e.Data;
                    break;
                // agc
                case "ZZGT":
                    _data.AGC = e.Data;
                    break;
                // mode dsp filters
                case "ZZMN":
                    ProcessDSPFilters(e.Data);
                    break;
                case "ZZSM":
                    _data.Smeter = e.Data.Substring(1);
                    break;
                case "ZZTX":
                    _data.mox = true;
                    break;
                case "ZZFI":
                    _data.DSPFilter = e.Data;
                    break;
                case "ZZPA":
                    _data.Preamp = e.Data;
                    break;
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        void ProcessDSPFilters(String data)
        {
            StringBuilder s = new StringBuilder();
            String filters = data.Substring(2);
            s.Append(filters.Substring(0, 5) + ",");
            s.Append(filters.Substring(15, 5) + ",");
            s.Append(filters.Substring(30, 5) + ",");
            s.Append(filters.Substring(45, 5) + ",");
            s.Append(filters.Substring(60, 5) + ",");
            s.Append(filters.Substring(75, 5) + ",");
            s.Append(filters.Substring(90, 5) + ",");
            s.Append(filters.Substring(105, 5) + ",");
            s.Append(filters.Substring(120, 5) + ",");
            s.Append(filters.Substring(135, 5) + ",");
            s.Append(filters.Substring(150, 5) + ",");
            s.Append(filters.Substring(165, 5));

            _data.dspfilterarray = new ArrayList
                                       {
                                           filters.Substring(0, 5),
                                           filters.Substring(15, 5),
                                           filters.Substring(30, 5),
                                           filters.Substring(45, 5),
                                           filters.Substring(60, 5),
                                           filters.Substring(75, 5),
                                           filters.Substring(90, 5),
                                           filters.Substring(105, 5),
                                           filters.Substring(120, 5),
                                           filters.Substring(135, 5),
                                           filters.Substring(150, 5),
                                           filters.Substring(165, 5),
                                           "UNKN"
                                       };

            _data.dspfilters = Regex.Replace(s.ToString(), " ", "", RegexOptions.Compiled);   
        }

        void ProcessHRDTCPGetCommand(String s, BinaryWriter bw)
        {            
            if (s.Contains("GET ID"))
                bw.Write(HRDMessage.HRDMessageToByteArray("Ham Radio Deluxe"));
            else if (s.Contains("GET VERSION"))            
                bw.Write(HRDMessage.HRDMessageToByteArray("v6.40")); // W0DHB fix for v5.23
            else if (s.Contains("GET FREQUENCY"))            
                bw.Write(HRDMessage.HRDMessageToByteArray(_data.vfoa));            
            else if (s.Contains("GET RADIO"))
            {

                if (s.Contains("GET RADIOS"))
                {
                    if (_usingN1MM)
                    {
                        bw.Write(HRDMessage.HRDMessageToByteArray("9:N1MM+"));
                    }
                    else if (_usingSSDR)
                    {
                        bw.Write(HRDMessage.HRDMessageToByteArray("9:SmartSDR"));
                    }
                    else
                    {
                        bw.Write(HRDMessage.HRDMessageToByteArray("11:PowerSDR"));
                    }
                }
                else
                {
                    if (_usingN1MM)
                    {
                        bw.Write(HRDMessage.HRDMessageToByteArray("N1MM+"));
                    }
                    else if (_usingSSDR)
                    {
                        bw.Write(HRDMessage.HRDMessageToByteArray("SmartSDR"));
                    }
                    else
                    {
                        bw.Write(HRDMessage.HRDMessageToByteArray("PowerSDR"));
                    }
                }
            }
            else if (s.Contains("GET CONTEXT"))            
                bw.Write(HRDMessage.HRDMessageToByteArray("1"));           
            else if (s.Contains("GET FREQUENCIES"))            
                bw.Write(HRDMessage.HRDMessageToByteArray(_data.vfoa + "-" + _data.vfob));
            else if (s.Contains("GET DROPDOWN-TEXT"))            
                bw.Write(HRDMessage.HRDMessageToByteArray(GetDropdownText(s)));            
            else if (s.Contains("GET DROPDOWN-LIST"))            
                bw.Write(HRDMessage.HRDMessageToByteArray(GetDropdownList(s)));
            else if (s.Contains("GET LOGBOOKUPDATES"))
                bw.Write(HRDMessage.HRDMessageToByteArray("0"));
            else if (s.Contains("GET BUTTONS"))
                bw.Write(HRDMessage.HRDMessageToByteArray(GetButtons()));
            else if (s.Contains("GET SMETER-MAIN"))            
                bw.Write(HRDMessage.HRDMessageToByteArray(String.Format("S{0},{0},14", 14 - int.Parse(_data.Smeter))));            
            else if (s.Contains("GET BUTTON-SELECT TX"))
                bw.Write(HRDMessage.HRDMessageToByteArray(_data.mox ? "1" : "0"));
            else if (s.Contains("GET DROPDOWNS"))
                bw.Write(HRDMessage.HRDMessageToByteArray(GetDropdowns()));
            else if (s.Contains("GET VFO-COUNT"))
                bw.Write(HRDMessage.HRDMessageToByteArray("2"));
            else
                bw.Write(HRDMessage.HRDMessageToByteArray("0"));            
        }

        void ProcessHRDTCPSetCommand(String s, BinaryWriter bw)
        {            
            Debug(String.Format("SET COMMAND: {0}", s));

            if (s.Contains("SET DROPDOWN"))
                SetDropdown(s);
            else if (s.Contains("SET FREQUENCIES-HZ"))
            {
                Match m = Regex.Match(s, "FREQUENCIES-HZ (\\d+) (\\d+)");
                if (!m.Success) return;
                String vfoa = String.Format("{0:00000000000}", long.Parse(m.Groups[1].Value));
                String vfob = String.Format("{0:00000000000}", long.Parse(m.Groups[2].Value));
                if (!_usingSSDR)
                {
                    if (vfoa != "00000000000")
                        WriteCommand("ZZFA" + vfoa + ";");
                    if (vfob != "00000000000")
                        WriteCommand("ZZFB" + vfob + ";");
                }
                _data.vfoa = vfoa;
                _data.vfob = vfob;
                if (_usingSSDR)
                {
                    double freq = double.Parse(vfoa) / 1000000;
                    System.Diagnostics.Debug.Print("Frequency: " + freq.ToString());
                    _thisSlice.Freq = freq;
                }
            }
            else if (s.Contains("SET BUTTON-SELECT"))
                SetButton(s);
            else if (s.Contains("SET FREQUENCY-HZ"))
            {
                Match m = Regex.Match(s, "FREQUENCY-HZ (\\d+)");
                if(!m.Success) return;
                String vfoa = String.Format("{0:00000000000}", long.Parse(m.Groups[1].Value));
                if (!_usingSSDR)
                {
                    if (vfoa != "00000000000")
                        WriteCommand("ZZFA" + vfoa + ";");
                }
                _data.vfoa = vfoa;
                if (_usingSSDR)
                {
                    double freq = double.Parse(vfoa) / 1000000;
                    System.Diagnostics.Debug.Print("Frequency: " + freq.ToString());
                    _thisSlice.Freq = freq;
                }
            }
            // tell the program that the command executed OK, regardless if it did or not.
            bw.Write(HRDMessage.HRDMessageToByteArray("OK"));
        }
                
        static String GetButtons()
        {
            return "TX";
        }

        static String GetDropdowns()
        {
            return "Mode,Band,AGC,Display,Preamp,DSP Fltr";
        }

        String GetDropdownText(String s)
        {
            StringBuilder output = new StringBuilder();            
            MatchCollection mc = Regex.Matches(s, "{([A-Z~]+)}", RegexOptions.Compiled);            

            if(mc.Count == 0) return String.Empty; 

            foreach (Match m in mc)
            {                
                switch (m.Groups[1].Value)
                {
                    case "MODE":
                        output.Append("Mode: " + _data.Mode + "\u0009");
                        break; 
                    case "BAND":
                        output.Append("Band: " + _data.Band + "\u0009");
                        break;
                    case "AGC":
                        output.Append("AGC: " + _data.AGC + "\u0009");
                        break;
                    case "DISPLAY":
                        output.Append("Display: " + _data.DisplayMode + "\u0009");
                        break;
                    case "PREAMP":
                        output.Append("Preamp: " + _data.Preamp + "\u0009");
                        break;
                    case "DSP~FLTR":
                        output.Append("DSP Fltr:" + _data.DSPFilter + "" + "\u0009");
                        break;
                    default:
                        output.Append(m.Groups[1].Value + ": " + "\u0009");
                        break;
                }
            }
            
            // remove trailing \u0009 or else HRD Logbook wont parse it properly       
            return output.ToString().Remove(output.ToString().LastIndexOf('\u0009'));
        }

        String GetDropdownList(String s)
        {
            String q = s.Substring(s.IndexOf("{") + 1, (s.IndexOf("}") - s.IndexOf("{") - 1));
            String output;

            switch (q)
            {
                case "MODE":
                    output = "DIGU,DIGL,RTTY,CW,LSB,USB,AM";
                    break;
                case "AGC":
                    output = "Fixed,Long,Slow,Med,Fast";
                    break;
                case "BAND":
                    output = "160m,80m,60m,40m,30m,20m,17m,15m,12m,10m,6m,2m,GEN,WWV";
                    break;
                case "DISPLAY":
                    output = "Spectrum,Panadapter,Scope,Phase,Phase2,Waterfall,Histogram,Off";
                    break;
                case "DSP FLTR":
                    // we need set these based on mode...
                    switch (_data.Mode)
                    {
                        default:
                        case "USB":
                        case "LSB":
                            output = "4.0kHz,3.3kHz,2.9kHz,2.7Khz,1.8Khz,1.6Khz";
                            break;
                        case "DIGL":
                        case "DIGU":
                            output = "5.0Khz,3.0Khz,2.0Khz,1.5Khz,1.0Khz,600Hz,300Hz,100Hz";
                            break;
                        case "CW":
                            output = "3.0Khz,1.5Khz,1.0Khz,800Hz,400Hz,250Hz,100Hz,50Hz";
                            break;
                    }
                   
                    break;
                case "PREAMP":
                    if (_usingSSDR)
                        output = "-10,+0,+10,+20";
                    else
                        output = "Off,Low,Medium,High";
                    break;
                default:
                    output = String.Empty;
                    break;
            }

            return output;
        }        

        void SetDropdown(String s)
        {
            Match m = Regex.Match(s, "SET DROPDOWN ([\\w~]+) ([^.]+) (\\d+)", RegexOptions.Compiled);
           if(!m.Success)
                Debug ("Dropdown failed?");

            switch(m.Groups[1].Value)
            {
                case "MODE":
                    String mode = String.Format("{0:00}", int.Parse(m.Groups[3].Value));
                    Debug("Received mode from HRD: " + mode);
                    if (!_usingSSDR)
                    {
                        WriteCommand("ZZMD" + mode + ";");
                        WriteCommand("ZZMN" + mode + ";");
                    }
                    if (!_usingRIOX) WriteCommand("ZZFI;");
                    _data.Mode = mode;
                    if (_usingSSDR)
                    {
                        Debug("Sending mode to SSDR: " + _data.Mode);
                        _thisSlice.DemodMode = _data.Mode;
                    }
                    break;
                case "DSP~FLTR":                    
                    String fltr = String.Format("{0:00}", int.Parse(m.Groups[3].Value));
                    if (!_usingSSDR)
                    {
                        WriteCommand("ZZFI" + fltr + ";");
                    }
                    _data.DSPFilter = fltr;
                    if (_usingSSDR) { }
                    break;
                case "AGC":
                    if (!_usingSSDR)
                    {
                        WriteCommand("ZZGT" + m.Groups[3].Value + ";");
                    }
                    _data.AGC = m.Groups[3].Value;
                    if (_usingSSDR) { }
                    break;
                case "BAND":
                    String band = Regex.Replace(m.Groups[2].Value, "M", "");

                    if (!_usingSSDR)
                    {

                        if (band.Equals("GEN"))
                        {
                            _data.Band = "888";
                            WriteCommand("ZZBS888;");
                            break;
                        }
                        if (band.Equals("WWV"))
                        {
                            _data.Band = "999";
                            WriteCommand("ZZBS999;");
                            break;
                        }
                        if (band.Contains("V")) return; // not implementing vhf band switching yet

                        String output = String.Format("{0:000}", int.Parse(band));
                        _data.Band = output;
                        WriteCommand("ZZBS" + output + ";");
                    }
                    if (_usingSSDR)
                    {
                        _thisSlice.Panadapter.Band = band;

                    }

                    break;
                case "DISPLAY":
                    if (!_usingSSDR)
                    {
                        WriteCommand("ZZDM" + m.Groups[3].Value + ";");
                    }
                    _data.DisplayMode = m.Groups[3].Value;
                    if (_usingSSDR) { }
                    break;

                     
                case "PREAMP":
                    if (!_usingSSDR)
                    {
                        WriteCommand("ZZPA" + m.Groups[3].Value + ";");
                    }
                    _data.Preamp = m.Groups[3].Value;
                    if (_usingSSDR) {
                        _data.SSDRPreamp = m.Groups[3].Value;
                        Debug("New Preamp: " + int.Parse(_data.SSDRPreamp).ToString("+0;-#"));
                        Debug("Current Preamp: " + _thisSlice.Panadapter.RFGain.ToString("+0;-#"));
                        _thisSlice.RFGain=int.Parse(_data.SSDRPreamp);
                    }
                    break;
            }
        }

        String SetButton(String s)
        {
            Match m = Regex.Match(s, "SET BUTTON-SELECT (\\w+) (\\d)", RegexOptions.Compiled);
            if(!m.Success) return String.Empty;

            switch(m.Groups[1].Value)
            {
                case "TX":                                                     
                    _data.mox = _data.mox ? false : true;
                    if (_usingSSDR)
                    {
                        _thisRadio.Mox = _data.mox;
                    }
                    else
                    {
                        WriteCommand("ZZTX" + (_data.mox ? "1" : "0") + ";");
                    }        
                    break;
                case "split":
                case "Split":
                    _data.mox = _data.mox ? false : true;
                    if (_usingSSDR)
                    {
                        // Add split command?
                        _thisRadio.Mox = _data.mox;
                    }
                    else
                    {
                        WriteCommand("ZZSP" + (_data.mox ? "1" : "0") + ";");
                    }
                    break;                    

            }

            return "OK";
        }

        public void ShowOptionsForm()
        {
            try
            {
                _form.Show();
            }
            catch (Exception e)
            {
                Debug(e.Message);
                Debug(e.StackTrace);
            }
        }

        public bool HRDTCPServer_IsListening()
        {
            if(_server != null && _logbook !=null)
            {
                return _server.IsListening;
            }
            return false;
        }

        public void SetNotifyIconText(String s)
        {
            _notifyIcon.SetNotifyText(s);
        }

        public void Restart()
        {
            Stop();

            while (_stopping)
            {
            }

            Start();
        }

        public void Stop()
        {
            _stopping = true;
            try
            {
                if (_timerShort != null) _timerShort.Stop();
                if (_timerLong != null) _timerLong.Stop();
                if (_cat != null) _cat.Close();
                if (_server != null) _server.Close();
                if (_logbook != null) _logbook.Close();
                if (_log4om != null) _log4om.Close();
                if (_riox != null)
                {
                    if (_riox.IsConnected) _riox.SendCommand(new RIOXCommand("UnSub", "NONE"));
                    _riox.Close();
                }
                if (_thisSlice != null)
                {
                    _thisSlice.Close();
                }
                if (_thisRadio != null)
                {
                    _thisRadio.Disconnect();
                }

                _cat = null;
                _server = null;
                _logbook = null;
                _timerShort = null;
                _timerLong = null;
                _log4om = null;
                SetNotifyIconText("MiniDeluxe - Stopped");
            }
            catch (Exception e)
            {
                _notifyIcon.MessageBox("While stopping: " + e.Message + "\n" + e.StackTrace);
            }
            finally
            {
                _stopping = false;
            }

        }

        public void Start()
        {
            try
            {
                _data = new RadioData
                {
                    vfoa = "0",
                    vfob = "0",
                    Mode = "99",
                    mox = false,
                    DisplayMode = "0",
                    Smeter = "0",
                    dspfilters = "",
                    dspfilterarray = new ArrayList
                                         {
                                         "6.0","4.0","2.6","2.1","1.0",
                                         "500","250","100","50","25","VAR1","VAR2","UNKN"
                                         },
                };

               

                if (Properties.Settings.Default.RIOX == true)
                {
                    // RIOX
                    try
                    {
                        Console.WriteLine("RIOX initializing");
                        _riox = new RIOX.RIOXClient(typeof(RIOXData), Properties.Settings.Default.RIOXIP, Properties.Settings.Default.RIOXPort);
                        _riox.ObjectReceivedEvent += new RIOX.RIOXClient.ObjectReceivedEventHandler(_riox_ObjectReceivedEvent);
                        _usingRIOX = true;
                        _usingN1MM = false;
                        _usingSSDR = false;
                    }
                    catch (Exception ex)
                    {
                        _notifyIcon.MessageBox("RIOX error: " + ex.Message + "\r\n" + ex.StackTrace);
                        throw ex;
                    }
                }
                else if (Properties.Settings.Default.N1MM == true)
                {
                    Console.WriteLine("N1MM initializing");
                    _n1mm = new N1MMConnector(this);
                    _timerShort = new Timer(Properties.Settings.Default.HighInterval);
                    _n1mm.N1MMEvent += n1mmEvent;
                    _timerShort.Elapsed += TimerShortElapsed;
                    _usingRIOX = false;
                    _usingN1MM = true;
                    _usingSSDR = false;
                }
                else if (Properties.Settings.Default.SSDR == true)
                {
                    Console.WriteLine("SSDR initializing");
                    API.RadioAdded += SSDR_RadioAdded;
                    API.RadioRemoved += SSDR_RadioRemoved;
                    API.ProgramName = "MiniDeluxe";
                    API.Init();
                    _usingRIOX = false;
                    _usingN1MM = false;
                    _usingSSDR = true;
                }
                else
                {
                    _cat = new CATConnector(new SerialPort(Properties.Settings.Default.SerialPort));
                    _timerShort = new Timer(Properties.Settings.Default.HighInterval);
                    _cat.CATEvent += CatcatEvent;
                    _timerShort.Elapsed += TimerShortElapsed;
                    _usingRIOX = false;
                    _usingN1MM = false;
                    _usingSSDR = false;
                }

                if (Properties.Settings.Default.Log4OM == true)
                {
                    if (_log4om == null)
                    {
                        _usingLOG4OM = true;
                        _log4om = new Log4OMConnector(this);
                    }
                }

                if (Properties.Settings.Default.QRZcom == true)
                {
                    if (_qrzcom == null)
                    {
                        _usingQRZcom = true;
                        _qrzcom = new QRZcomConnector(this);
                    }
                }
                _timerLong = new Timer(Properties.Settings.Default.LowInterval);
                _timerLong.Elapsed += TimerLongElapsed;

                _dm780 = new DM780Connector(this);
                //_listenOnly = Properties.Settings.Default.ListenOnly;
                _server = new HRDTCPServer(this,Properties.Settings.Default.Port);
                _logbook = new HRDTCPServer(this,Properties.Settings.Default.LogPort);
                _form.lblStatus.Text = "Server enabled.";


            }
            catch (Exception e)
            {
                _notifyIcon.MessageBox("While starting: " + e.Message + "\n" +
                                       "Server is disabled. Please check configuration.\nIf using RIOX mode, please make sure DDUtil is running.");
                _form.lblStatus.Text = "Server disabled.";
                ShowOptionsForm();
                return;
            }

            _server.HRDTCPEvent += ServerHRDTCPEvent;
            _logbook.HRDTCPEvent += LogbookHRDTCPEvent;
            _logbook.isLogbook = true;
            // Start the timers only if serial polling is enabled
            if (!_usingRIOX && !_usingN1MM && !_usingSSDR)
            {
                // write initial commands to the radio to fill in initial data
                WriteCommand("ZZIF;");
                WriteCommand("ZZFA;");
                WriteCommand("ZZFB;");
                WriteCommand("ZZBS;");
                WriteCommand("ZZDM;");
                WriteCommand("ZZGT;");              
                WriteCommand("ZZPA;");
                WriteCommand("ZZFI;");

                _timerShort.Start();
                //_timerLong.Start();
            }
            else if (_usingRIOX)// using RIOX
            {
                // subscribe to what we want from DDUtil
                _riox.SendCommand(new RIOXCommand("UpDateType", "PSH:500"));
                //_riox.SendCommand(new RIOXCommand("UnSub", "NONE"));
                _riox.SendCommand(new RIOXCommand("Sub","ZZIF;ZZFA;ZZFB;ZZBS;ZZDM;ZZGT;ZZPA;ZZFI;"));               
            }

            _timerLong.Start();
            _server.Start();
            _logbook.Start();
            SetNotifyIconText("MiniDeluxe - Running (0 connections)");
        }

        private void SSDR_RadioRemoved(Radio radio)
        {

        }
        private void SSDR_RadioAdded(Radio radio)
        {
            _thisRadio = radio;
            _thisRadio.Connect();
            _form.SSDRmodel.Text = radio.Model;
            _form.SSDRserial.Text = radio.Serial;
            _form.SSDRip.Text = radio.IP.ToString();
            radio.SliceAdded += SSDR_SliceAdded;
            radio.PropertyChanged += SSDR_PropertyChanged;
        }

        private void SSDR_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {


            if (e.PropertyName.Equals("Nickname"))
            {
                _form.SSDRname.Text = _thisRadio.Nickname;
            }
            if (e.PropertyName.Equals("Callsign"))
            {
                _form.SSDRname.Text = _thisRadio.Callsign;
            }
            if (e.PropertyName.Equals("Mox"))
            {
                _data.mox = _thisRadio.Mox;
            }

        }

        private void SSDR_SliceAdded(Slice slice)
        {

            slice.PropertyChanged += new PropertyChangedEventHandler(SSDR_Slice_PropertyChanged);// makes freqtxtbx show freq from SSDR 

            if (slice.Active && slice.IsTransmitSlice)
            {
                SSDR_GetSliceData(slice);
                _thisSlice = slice;
                _thisSlice.SMeterDataReady += SSDR_SMeterDataReady;
            }
            //textRFpower.Text = _thisRadio.RFPower.ToString() + " Watts";

            // Below allows reading S Meter data
            // below gets current slice's band info
            //lblBand.Text = bandinfo + " Meters";
            // Slice bandwidth
            //txtBW.Text = _thisRadio.ActiveSlice.FilterHigh.ToString() + " High " +
            //   _thisRadio.ActiveSlice.FilterLow.ToString() + "Low";

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }


        private void SSDR_Slice_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            //      CODE TO DEAL WITH SLICE PROPERTY CHANGES      
            if (_thisRadio.ActiveSlice != null)
            {
                if (_thisRadio.ActiveSlice.Active && _thisRadio.ActiveSlice.IsTransmitSlice)
                {
                    _thisSlice = _thisRadio.ActiveSlice;
                    //                    SSDR_GetSliceData(_thisRadio.ActiveSlice);
                    Debug("Slice Property " + e.PropertyName.ToString() + " Changed");
                    if (e.PropertyName.Equals("Freq"))
                    {
                        _form.SSDRfreq.Text = _thisSlice.Freq.ToString("n6") + " Mhz";
                        _data.vfoa = (_thisSlice.Freq * 1000000).ToString();
                    }
                    else if (e.PropertyName.Equals("DemodMode"))
                    {
                        _form.SSDRmode.Text = _thisSlice.DemodMode.ToString();
                        _data.Mode = _thisSlice.DemodMode.ToString();
                    }
                    else if (e.PropertyName.Equals("Band"))
                        _data.Band = _thisSlice.Panadapter.Band;
                    else if (e.PropertyName.Equals("RFGain"))
                        _data.SSDRPreamp = _thisSlice.RFGain.ToString("+0;-#");

                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                }
            }

        }

        private void SSDR_GetSliceData(Slice slice)
        {
            _form.SSDRfreq.Text = slice.Freq.ToString("n6") + " Mhz";
            _form.SSDRmode.Text = slice.DemodMode.ToString();
            _data.vfoa = (slice.Freq * 1000000).ToString();
            _data.Mode = slice.DemodMode.ToString();
            _data.Band = slice.Panadapter.Band;
            _data.SSDRPreamp = slice.RFGain.ToString("+0;-#");
            System.Diagnostics.Debug.WriteLine("Band Received from SmartSDR: " + slice.Panadapter.Band +" ("+_data.Band+")");
            System.Diagnostics.Debug.WriteLine("Mode Received from SmartSDR: " + slice.DemodMode +" ("+_data.Mode+")");
            System.Diagnostics.Debug.WriteLine("Preamp Received from SmartSDR: " + slice.RFGain.ToString("+0;-#") +" ("+ _data.SSDRPreamp+")");
            Debug( _thisRadio.ActiveSlice.FilterHigh.ToString() + " High " + _thisRadio.ActiveSlice.FilterLow.ToString() + " Low");
            
        }


        void _riox_ObjectReceivedEvent(object o, RIOX.RIOXClient.ObjectReceivedEventArgs e)
        {
            RIOXData rd = (RIOXData)e.DataObject;
            Console.WriteLine("ObjectReceived " + rd.Count);
            
            // pass it through the CAT event parser since DDUtil passes them as command/data key/pair
            foreach(DictionaryEntry de in rd)
            {
                CATEventArgs c = new CATEventArgs((string)de.Key, (string)de.Value);
                Console.WriteLine("Key: {0} Val: {1}", de.Key, de.Value);
                CatcatEvent(this, c);
            }
            
        }

        private void SSDR_SMeterDataReady (float data)
        {
            _data.SSDRmeter = data.ToString();
            //data = (float)Math.Round(data) + 125;
            //_form.SSDRsmeter.Text = data.ToString();

            String test = String.Format("S{0},{0},14", 14 - int.Parse(_data.Smeter));
            _form.SSDRsmeter.Text = test;
            
            GC.Collect();
            GC.WaitForPendingFinalizers();

        }



        public void EndProgram()
        {
            if (_usingSSDR)
                _thisRadio.Disconnect();
            Stop();
            _notifyIcon.EndProgram();

        }

        public static void Debug(String s)
        {
            System.Diagnostics.Debug.WriteLine(s);
            System.Diagnostics.Debug.Flush();
        }

        private void WriteCommand(String data)
        {
            if (_usingRIOX)
            {
                Debug("Writing RIOX command: " + data);
                try
                {
                    _riox.SendCommand(new RIOX.RIOXCommand("ClientCmd", data));
                }
                catch (Exception)
                {
                    // try reconnecting
                    try
                    {
                        _riox.Close();
                        _riox.Connect();
                    }
                    catch (Exception)
                    {
                        // could not reconnect
                    }
                }
            }
            else if (_usingN1MM || _usingSSDR)
            {
            }
            else 
            {
                Debug("Writing CAT command: " + data);
                _cat.WriteCommand(data);
            }
        }
    }  
}
