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
        public static extern UIntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern UIntPtr FindWindowEx(UIntPtr hWnd1, UIntPtr hWnd2, string lpsz1, string lpsz2);
        [DllImport("user32.dll")]
        public static extern UIntPtr SendMessage(UIntPtr hWnd, UIntPtr wMsg, UIntPtr wParam, UIntPtr lParam);

        System.Threading.Timer timer;
        public Random RandomStatus = new Random();

        private void button3_Click(object sender, EventArgs e)
        {
            ListView LV = listView1;
            System.Threading.TimerCallback callback = TimerEvent;
            timer = new System.Threading.Timer(callback, "", 0, 10);
        }

        public void TimerEvent(Object obj) { this.Invoke(new MethodInvoker(delegate () { Caller(); })); }

        public void Caller()
        {
            ListView LV = listView1;
            LV.BeginUpdate();
            int Number = RandomStatus.Next(1, LV.Items.Count) - 1;
            int Numbering = int.Parse(LV.Items[Number].SubItems[0].Text);
            string mName = LV.Items[Number].SubItems[1].Text;
            int mLevel;
            int mStr = int.Parse(LV.Items[Number].SubItems[3].Text);
            int mInt = int.Parse(LV.Items[Number].SubItems[4].Text);
            int mDex = int.Parse(LV.Items[Number].SubItems[5].Text);

            int sUp = RandomStatus.Next(1, 3);
            mStr += (sUp == 1) ? 1 : 0;
            mInt += (sUp == 2) ? 1 : 0;
            mDex += (sUp == 3) ? 1 : 0;
            mLevel = mStr + mInt + mDex;

            LV.Items.RemoveAt(Number);
            String[] CharacterSet = { Numbering.ToString(), mName, mLevel.ToString(), mStr.ToString(), mInt.ToString(), mDex.ToString() };
            ListViewItem lvt = new ListViewItem(CharacterSet);
            LV.Items.Add(lvt);
            LV.EndUpdate();
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
    }
}
