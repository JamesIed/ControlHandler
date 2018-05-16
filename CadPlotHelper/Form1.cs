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

namespace CadPlotHelper
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        static extern int GetForegroundWindow();
        [DllImport("user32.dll")]
        static extern int GetWindowText(int hWnd, StringBuilder text, int count);
        [DllImport("user32.dll")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern int FindWindowEx(int hWnd1, int hWnd2, string lpsz1, string lpsz2);
        [DllImport("user32.dll")]
        public static extern int SendMessage(int hWnd, int wMsg, int wParam, int lParam);

        System.Threading.Timer timer;
        public Random RandomStatus = new Random();

        public void TestHandleGet()
        {
            int hWndA = FindWindow("AfxMDIFrame140u", null);
            SendMessage(hWndA, 0, 17, 1900545);
            SendMessage(hWndA, 0, 80, 1638401);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ListView LV = listView1;
            LV.BeginUpdate();
            int PageNo = LV.Items.Count + 1;
            TextBox[] TB = { textBox1, textBox2, textBox3, textBox4 };
            String[] PosSet = { PageNo.ToString(), TB[0].Text, TB[1].Text, TB[2].Text, TB[3].Text };
            ListViewItem lvt = new ListViewItem(PosSet);
            LV.Items.Add(lvt);
            LV.EndUpdate();
            TestHandleGet();
        }

        public Form1()
        {
            InitializeComponent();
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.Columns.Add("Page", 80);
            listView1.Columns.Add("X1", 60);
            listView1.Columns.Add("Y1", 60);
            listView1.Columns.Add("X2", 60);
            listView1.Columns.Add("Y2", 60);
        }

        /*
        private void button3_Click(object sender, EventArgs e)
        {
            ListView LV = listView1;
            System.Threading.TimerCallback callback = TimerEvent;
            timer = new System.Threading.Timer(callback, "", 0, 10);
        }

        public void TimerEvent(Object obj) { this.Invoke(new MethodInvoker(delegate () { Caller(); })); }

        public void Caller()
        {
            
            LV.Items.RemoveAt(Number);
            LV.EndUpdate();
        }
        */
    }
}
