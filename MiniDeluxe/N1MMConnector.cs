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

   N1MM+ Integration Copyright (C) 2017 by M0VSE
*/

using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Xml;


namespace MiniDeluxe
{
    class N1MMConnector
    {
        public event N1MMEventHandler N1MMEvent;

        private readonly MiniDeluxe _parent;
        private UdpClient _client;
        private IPEndPoint _endpoint;
        private readonly Thread _readThread;
        private StringBuilder _buffer;
        private bool _stopThread;


        public N1MMConnector(MiniDeluxe parent)
        {
            _buffer = new StringBuilder();

            try
            {
                _parent = parent;
                _client = new UdpClient(Properties.Settings.Default.RadioPort);
                _endpoint = new IPEndPoint(Properties.Settings.Default.LocalOnly ? IPAddress.Loopback : IPAddress.Any, 0);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to bind to UDP port: " + ex.ToString());
            }
            _readThread = new Thread(ReadThread);
            _readThread.Start();
        }
        private void ReadThread()
        {
            while (!_stopThread)
            {
                try
                {
                    Byte[] b = _client.Receive(ref _endpoint);
                    _buffer.Append(System.Text.Encoding.UTF8.GetString(b));
                    ProcessData();
                }
                catch
                { }

            }
            _client.Close();
        }
        private void ProcessData()
        {
            // Have we received a valid XML string from N1MM?
            XmlDocument doc = new XmlDocument();
            try
            {
                String Data = "";
                String Command = "";

                doc.LoadXml(_buffer.ToString());

                XmlNodeList nodes = doc.SelectNodes("/RadioInfo");

                if (nodes.Count > 0)
                {
                    //Console.WriteLine("Radio Info Received: " + nodes.Count.ToString() + " nodes");
                    foreach (XmlNode node in nodes)
                    {
                        /*
                        Console.WriteLine(node.Name);
                        foreach (XmlNode n in node)
                        {
                            switch (n.Name)
                            {
                                case "StationName":
                                    Command = "ZZIF";
                                    Data = "";
                                    break;
                                

                            }
                            Console.WriteLine("Node: " + n.Name + " Value: " + n.InnerText);
                            

                        } */

                        String stationName = node.SelectSingleNode("StationName").InnerText;
                        String radioNr = node.SelectSingleNode("RadioNr").InnerText;
                        Int32 Freq = Convert.ToInt32(node.SelectSingleNode("Freq").InnerText);
                        Int32 Step = 10;
                        String TXFreq = node.SelectSingleNode("TXFreq").InnerText;
                        String Mode = node.SelectSingleNode("Mode").InnerText;
                        String opCall = node.SelectSingleNode("OpCall").InnerText;

                        Console.WriteLine("S: " + stationName + " R: " + radioNr + " Mode: " + Mode);
                        // Generate ZZIF command!
                        Command = "ZZIF";
                        Data = (Freq * Step).ToString().PadRight(11) + Step.ToString().PadRight(4) + "-0".PadRight(6) + "0" + "0" + "0" + "00" + "0" + opMode(Mode).PadRight(2) + "0" + "0" + "0" + "00" + "0";
                        N1MMEventArgs nea = new N1MMEventArgs(Command, Data);
                        N1MMEvent(this, nea);
                    }
                    // Lets generate some commands!

                }

            }
            catch
            {
                Console.WriteLine("Invalid XML: " + _buffer.ToString());
            }
            _buffer = new StringBuilder();
        }


        private string opMode(string value)
        {
            string _mode = "99";
            switch (value)
            {
                case "LSB":
                    _mode = "00";
                    break;
                case "USB":
                    _mode = "01";
                    break;
                case "CW":
                    _mode = "03";
                    break;
                case "FM":
                    _mode = "04";
                    break;
                case "RTTY":
                    _mode = "06";
                    break;
                default:
                    _mode = "09";
                    break;
            }
            return _mode;

        }

    }
    public delegate void N1MMEventHandler(object sender, N1MMEventArgs e);
    public class N1MMEventArgs : EventArgs
    {
        public String Data { get; set; }
        public String Command { get; set; }

        public N1MMEventArgs(String command, String data)
        {
            Command = command;
            Data = data;
        }

        public N1MMEventArgs() { }
    }
}
