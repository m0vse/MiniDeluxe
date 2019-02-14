using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;

namespace MiniDeluxe
{



    class DM780Connector
    {

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, [Out] StringBuilder lParam);


        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string className, string lpszWindow);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        private static extern int SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, string lParam);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint GetDlgItemText(IntPtr hDlg, int nIDDlgItem, [Out] StringBuilder lpString, int nMaxCount);

        private const int WM_SETTEXT = 12;
        private const int WM_GETTEXT = 0x000D;
        private const int WM_GETTEXTLENGTH = 0x000E;

        public event DM780EventHandler DM780Event;
        private readonly Thread _readThread;
        private StringBuilder _buffer;
        private bool _stopThread;
        private bool _dm780Running;
        private IntPtr _dmwindow;

        public DM780Connector(MiniDeluxe parent)
        {
            _buffer = new StringBuilder();
            Console.WriteLine("Starting DM780 Connector");
            // We need to locate the DM780 window first.
            _dmwindow = FindWindow("DM780-Application", null);

          

            StringBuilder sb = WindowText(_dmwindow);
            if (sb.Length > 0)
            {
                _dm780Running = true;
                Console.WriteLine("DM780 Window: " + sb.ToString());
                // Now find the handle for the RICHEDIT control

            }

            //IntPtr child = FindWindowEx(_dmwindow, IntPtr.Zero, "RICHEDIT", null);
            StringBuilder sb2 = WindowText((IntPtr)0x00041788);
            if (sb2.Length > 0)
            {
                Console.WriteLine("Window: " + sb2.ToString());
            }
        }


        private StringBuilder WindowText(IntPtr hwnd)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                int length = (int)SendMessage(hwnd, WM_GETTEXTLENGTH, IntPtr.Zero, sb);
                sb = new StringBuilder(length + 1);
                SendMessage(hwnd, WM_GETTEXT, (IntPtr)sb.Capacity, sb);
                if (sb.ToString().Trim().Length == 0)
                {
                    sb.Clear();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Window Error: " + ex.Message);
            }
            return sb;
        }



        public void Close()
        {
            _stopThread = true;
            _dm780Running=false;
        }

    }



    public delegate void DM780EventHandler(object sender, DM780EventArgs e);
    public class DM780EventArgs : EventArgs
    {
            public String Data { get; set; }
            public String Command { get; set; }
            public DM780EventArgs(String command, String data)
            {
                Command = command;
                Data = data;
            }

            public DM780EventArgs() { }
    }
}
