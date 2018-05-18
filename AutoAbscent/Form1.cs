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
using System.Collections;

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

            HtmlElementCollection element = webBrowser1.Document.All;
            listView1.BeginUpdate();
            for (int i = 0; i < element.Count; i++)
            {
                string TempValue = "", TmepID = "";

                if (element[i].GetAttribute("value") != null)
                    TempValue = element[i].GetAttribute("value");
                
                if (element[i].GetAttribute("id") != null)
                    TmepID = element[i].GetAttribute("ID");

                String[] CharacterSet = { TmepID, TempValue, i.ToString() };
                ListViewItem lvt = new ListViewItem(CharacterSet);
                listView1.Items.Add(lvt);
            }
            listView1.EndUpdate();
            /*
            listView1.Sorting = SortOrder.Descending;
            listView1.ListViewItemSorter = null;

            */

            ready();
            /*
            HD.GetElementById("mb_id").InnerText = "plmdml";
            HD.GetElementById("mb_password").InnerText = "7367";
            
            HD.GetElementById("mb_id").SetAttribute("value", "plmdml");
            HD.GetElementById("mb_password").SetAttribute("value", "7367");
            HD.GetElementById("flogins").InvokeMember("SUBMIT");
            */
            ready();
            /*
            HtmlElementCollection element = HD.GetElementsByTagName("input");
            for (int i = 0; i < element.Count; i++)
            {
                if (element[i].GetAttribute("Class") == "login-button")
                {
                    element[i].InvokeMember("click");
                    listView1.Items.Add("List item text", 3);
                }
            }
            ready();
            */
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://supserver.xyz/attendance_amzg33");

            ready();

            HtmlDocument HD = webBrowser1.Document;

            HD.GetElementById("user_id").InnerText = "plmdml";
            HD.GetElementById("password").InnerText = "qwe123";
            /*
            var element = webBrowser1.Document.All;
            //HtmlElementCollection element = HD.GetElementsByTagName("input");
            HD.GetType();
            for (int i = 0; i < element.Count; i++)
            {
                if (element[i].GetType() == "로그인")
                {
                    element[i].InvokeMember("click");
                }
            }
            */
            ready();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://imart.inven.co.kr/attendance/");

            ready();

            HtmlDocument HD = webBrowser1.Document;

            HD.GetElementById("comLeftLoginId").InnerText = "plmdml";
            HD.GetElementById("comLoginPassword").InnerText = "ied4inven!";
            HD.GetElementById("comLeftLoginForm").InvokeMember("SUBMIT");

            ready();

            String ThisUrl= webBrowser1.Url.ToString();

            if(ThisUrl == "http://imart.inven.co.kr/attendance/")
            {
                HD.InvokeScript("JavaScriptFunctionWithoutParameters");
                webBrowser1.Document.InvokeScript("function", new object[] {"a" });
                
            }/*
            else if(ThisUrl == base)
            {
                HD.GetElementById("btn-ok").InvokeMember("Click");
            }
            */
            ready();
            
            
            /*
            HtmlElementCollection element = HD.GetElementsByTagName("attendBttn");
            for (int i = 0; i < element.Count; i++)
            {
                if (element[i].GetAttribute("value") == "로그인")
                {
                    element[i].InvokeMember("click");
                }
            }
            HD.Body.All["attendBttn"].InvokeMember("click");
            //HD.InvokeScript("JavaScriptFunctionWithoutParameters");
            */
        }


        public void ready()
        {
            while (webBrowser1.ReadyState != WebBrowserReadyState.Complete)
            {
                Application.DoEvents();
            }
        }
        
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.Sorting == SortOrder.Ascending)
                listView1.Sorting = SortOrder.Descending;
            else
                listView1.Sorting = SortOrder.Ascending;
        }

        public Form1()
        {
            InitializeComponent();
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
        }
    }
}
