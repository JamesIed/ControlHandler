using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exp02
{
    public partial class Form1 : Form
    {
        System.Threading.Timer timer;
        public Random RandomStatus = new Random();

        public Form1()
        {
            InitializeComponent();
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.Columns.Add("No", 30);
            listView1.Columns.Add("Name", 100);
            listView1.Columns.Add("Level", 60);
            listView1.Columns.Add("Str", 40);
            listView1.Columns.Add("Int", 40);
            listView1.Columns.Add("Dex", 40);
            listView1.Columns.Add("Ascend", 60);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ListView LV = listView1;
            int Numbering = LV.Items.Count + 1, mLevel, mStr, mInt, mDex;
            String mName = Numbering.ToString() + "Char";
            mStr = Roller();
            mInt = Roller();
            mDex = Roller();
            mLevel = mStr + mInt + mDex;

            String[] CharacterSet = { Numbering.ToString(), mName, mLevel.ToString(), mStr.ToString(), mInt.ToString(), mDex.ToString() };
            ListViewItem lvt = new ListViewItem(CharacterSet);
            LV.Items.Add(lvt);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ListView LV = listView1;
            System.Threading.TimerCallback callback = TimerEvent;
            timer = new System.Threading.Timer(callback, "", 0, 1000);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ListView LV = listView1;
            System.Threading.TimerCallback callback = TimerEvent;
            timer = new System.Threading.Timer(callback, "", 0, 10);
        }

        public void TimerEvent(Object obj) { this.Invoke(new MethodInvoker( delegate() { Caller();} ) ); }

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

        public int Roller(int a = 3, int b = 6)
        {
            int Result = 0;
            int LoopCount = 0;
            do
            {
                Result += RandomStatus.Next(1, b);
                LoopCount++;
            } while (LoopCount < a);

            return Result;
        }
    }
}
