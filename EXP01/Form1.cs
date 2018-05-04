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


namespace EXP01
{

    public partial class Form1 : Form
    {
        public int checker = 2;

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

        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://mabinogi.nexon.com/page/main/index.asp");

            ready();

            webBrowser1.Document.GetElementById("id").InnerText = "ied8911@gmail.com";
            webBrowser1.Document.GetElementById("pw").InnerText = "ied44nexon!";
            webBrowser1.Document.InvokeScript("NexonLogin");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            webBrowser1.Document.GetElementById("OTP_PW").InnerText = textBox1.Text;
            webBrowser1.Document.InvokeScript("checkLoginInfo");

            ready();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (checker==2)
            {
                this.webBrowser1.Size = new System.Drawing.Size(1920, 720);
                this.webBrowser2.Size = new System.Drawing.Size(0, 0);
                checker = 1;
            }
            else
            {
                this.webBrowser1.Size = new System.Drawing.Size(0, 0);
                this.webBrowser2.Size = new System.Drawing.Size(1920, 720);
                checker = 2;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            webBrowser2.Navigate("http://mabinogi.nexon.com/page/main/index.asp");

            ready();
        }

        public void gamestart()
        {
            webBrowser1.Document.InvokeScript("MabinogiGameStart");
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
    }
}
