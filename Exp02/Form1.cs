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
        public System.Threading.Timer timer;
        public Random RandomNo = new Random();
        public string[] CharacterSet = { "", "", "", "", "", "", "" };
        public int MaxAtk = 10, MaxDef = 10;
        public SortOrder Sorting { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            for(int i = 1;i <= 10; i++)
            { 
                ListView LV1 = listView1;
                int Numbering = LV1.Items.Count + 1, mLevel = 0, Atk = 10, Def = 10, HP = Atk / 10 + Def;
                String mName = Numbering.ToString() + "Char";

                String[] CharacterSet = { Numbering.ToString(), mName, mLevel.ToString(), Atk.ToString(), Def.ToString(), HP.ToString(), "0" };
                ListViewItem lvt = new ListViewItem(CharacterSet);
                LV1.Items.Add(lvt);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Battle();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer.Dispose();
        }

        public void TimerEvent(Object obj) { this.Invoke(new MethodInvoker(delegate () { Caller(); })); }

        public void Battle()
        {
            ListView LV1 = listView1;
            ListView LV2 = listView2;
            int Enemy = LV2.Items.Count;
            if (Enemy == 0)
            {
                int Warrior = RandomNo.Next(LV1.Items.Count / 2, LV1.Items.Count * 2);
                for(int i = 1; i < Warrior; i++)
                {
                    int Numbering = LV2.Items.Count + 1, Atk = RandomNo.Next(8, MaxAtk * 15 / 10), Def = RandomNo.Next(8, MaxDef * 15 / 10), HP = Atk / 10 + Def;
                    String mName = "Enemy " + Numbering.ToString();
                    
                    String[] CharacterSet = { Numbering.ToString(), mName, Atk.ToString(), Def.ToString(), HP.ToString(), "0", "0" };
                    ListViewItem lvt = new ListViewItem(CharacterSet);
                    LV2.Items.Add(lvt);
                }
                // LV1.Sorting = SortOrder.Ascending;
            }

            System.Threading.TimerCallback callback = TimerEvent;
            timer = new System.Threading.Timer(callback, "", 0, 1);
        }

        public void Caller()
        {
            ListView LV1 = listView1;
            ListView LV2 = listView2;
            int Battlechecker = 0;
            LV1.BeginUpdate();
            LV2.BeginUpdate();
            
            if (Battlechecker == 0)
            {
                Battlechecker++;
                if (LV2.Items.Count != 0)
                {
                    int Defender = RandomNo.Next(1, LV2.Items.Count) - 1;

                    int Numbering1 = int.Parse(LV1.Items[0].SubItems[0].Text);
                    string mName1 = LV1.Items[0].SubItems[1].Text;
                    int mLevel1 = int.Parse(LV1.Items[0].SubItems[2].Text);
                    int Atk1 = int.Parse(LV1.Items[0].SubItems[3].Text);
                    int Def1 = int.Parse(LV1.Items[0].SubItems[4].Text);
                    int HP1 = int.Parse(LV1.Items[0].SubItems[5].Text);
                    int Exp1 = int.Parse(LV1.Items[0].SubItems[6].Text);

                    int Numbering2 = int.Parse(LV2.Items[Defender].SubItems[0].Text);
                    string mName2 = LV2.Items[Defender].SubItems[1].Text;
                    int Atk2 = int.Parse(LV2.Items[Defender].SubItems[2].Text);
                    int Def2 = int.Parse(LV2.Items[Defender].SubItems[3].Text);
                    int HP2 = int.Parse(LV2.Items[Defender].SubItems[4].Text);

                    HP2 = (Atk1 > Def2) ? (HP2 - (Atk1 - Def2)) : (HP2 - 1);
                    Exp1 = (HP2 > 0) ? Exp1 : (Exp1 + Def2 + Atk2 / 10);

                    if (Exp1 >= mLevel1 * 100)
                    {
                        mLevel1++;
                        int sUp = RandomNo.Next(1, 100);
                        Atk1 += (sUp <= 55) ? 1 : 0;
                        Def1 += (sUp >= 45) ? 1 : 0;
                        HP1 = (Atk1 + Def1 * 10) / 10;
                        Exp1 = 0;
                    }

                    LV1.Items.RemoveAt(0);
                    String[] CharacterSet = { Numbering1.ToString(), mName1, mLevel1.ToString(), Atk1.ToString(), Def1.ToString(), HP1.ToString(), Exp1.ToString() };
                    ListViewItem lvt = new ListViewItem(CharacterSet);
                    LV1.Items.Add(lvt);

                    if (HP2 > 0)
                    {
                        LV2.Items.RemoveAt(Defender);
                        String[] CharacterSet2 = { Numbering2.ToString(), mName2, Atk2.ToString(), Def2.ToString(), HP2.ToString() };
                        ListViewItem lvt2 = new ListViewItem(CharacterSet2);
                        LV2.Items.Add(lvt2);
                    }
                    else
                        LV2.Items.RemoveAt(Defender);
                    
                    MaxDef = (MaxDef <= Def1) ? Def1 : MaxDef;
                    MaxAtk = (MaxAtk <= Atk1) ? Atk1 : MaxAtk;
                }
            }
            else
            {
                Battlechecker--;
                if (LV1.Items.Count != 0)
                {

                }
            }
            LV2.EndUpdate();
            LV1.EndUpdate();

            if(LV1.Items.Count == 0 || LV2.Items.Count == 0)
            {
                timer.Dispose();
                //timer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
                //Battle();
            }
        }
        
        public int Roller(int a = 3, int b = 6)
        {
            int Result = 0;
            int LoopCount = 0;
            do
            {
                Result += RandomNo.Next(1, b);
                LoopCount++;
            } while (LoopCount < a);

            return Result;
        }

        public Form1()
        {
            InitializeComponent();
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.Columns.Add("No", -2, HorizontalAlignment.Center);
            listView1.Columns.Add("Name", -2, HorizontalAlignment.Center);
            listView1.Columns.Add("Level", -2, HorizontalAlignment.Center);
            listView1.Columns.Add("Atk", -2, HorizontalAlignment.Center);
            listView1.Columns.Add("Def", -2, HorizontalAlignment.Center);
            listView1.Columns.Add("HP", -2, HorizontalAlignment.Center);
            listView1.Columns.Add("Exp", -2, HorizontalAlignment.Center);
            listView2.View = View.Details;
            listView2.FullRowSelect = true;
            listView2.GridLines = true;
            listView2.Columns.Add("No", -2, HorizontalAlignment.Center);
            listView2.Columns.Add("Name", -2, HorizontalAlignment.Center);
            listView2.Columns.Add("Atk", -2, HorizontalAlignment.Center);
            listView2.Columns.Add("Def", -2, HorizontalAlignment.Center);
            listView2.Columns.Add("HP", -2, HorizontalAlignment.Center);
        }
    }
}
