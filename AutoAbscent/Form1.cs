using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Runtime.InteropServices;

namespace AutoAbscent
{
    public partial class Form1 : Form
    {

        [DllImport("user32.dll")]
        static extern int GetForegroundWindow();
        [DllImport("user32.dll")]
        static extern int GetWindowText(int hWnd, StringBuilder text, int count);
        [DllImport("user32.dll")]
        public static extern UIntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern UIntPtr FindWindowEx(UIntPtr hWnd1, UIntPtr hWnd2, string lpsz1, string lpsz2);
        [DllImport("user32.dll")]
        public static extern UIntPtr SendMessage(UIntPtr hWnd, UIntPtr wMsg, UIntPtr wParam, UIntPtr lParam);

        private const int INTERNET_OPTION_END_BROWSER_SESSION = 42;

        [System.Runtime.InteropServices.DllImport("wininet.dll", SetLastError = true)]
        private static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr IpBuffer, int IpdwBufferLength);

        public void Init()
        {
            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_END_BROWSER_SESSION, IntPtr.Zero, 0);
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://2tcafe.com/attendance/attendance.php?3");

            ready();

            HtmlDocument HD = webBrowser1.Document;

            ready();
            HD.GetElementById("mb_id").SetAttribute("value", "plmdml");
            HD.GetElementById("mb_password").InnerText = "7367";
            //HD.GetElementById("login-button");
            webBrowser1.Document.InvokeScript("login-button");
            /*
            ID.SetAttribute("value", "plmdml");
            PW.SetAttribute("value", "ied4inven!");
            F1.InvokeMember("submit");
            */
            ready();

        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://supserver.xyz/attendance_amzg33");

            ready();

            webBrowser1.Document.GetElementById("user_id").InnerText = "plmdml";
            webBrowser1.Document.GetElementById("password").InnerText = "qwe123";
            webBrowser1.Document.GetElementById("GPE_login").InvokeMember("submit");
        }
        

        public void ready()
        {
            while (webBrowser1.ReadyState != WebBrowserReadyState.Complete)
            {
                Application.DoEvents();
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
