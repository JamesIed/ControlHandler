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
        public int MaxAtk = 10, MaxDef = 10;
        public int Battlechecker = 0;
        public int AlianceCount = 0;
        static double Erf(double x)
        {
            // constants
            double a1 = 0.254829592;
            double a2 = -0.284496736;
            double a3 = 1.421413741;
            double a4 = -1.453152027;
            double a5 = 1.061405429;
            double p = 0.3275911;

            // Save the sign of x
            int sign = 1;
            if (x < 0)
                sign = -1;
            x = Math.Abs(x);

            // A&S formula 7.1.26
            double t = 1.0 / (1.0 + p * x);
            double y = 1.0 - (((((a5 * t + a4) * t) + a3) * t + a2) * t + a1) * t * Math.Exp(-x * x);

            return sign * y;
        }

        public double PDF(double x, double mu = 0, double sigma = 1)
        {
            return Math.Exp(-1 * Math.Pow((x - mu), 2) / (2 * Math.Pow(sigma, 2)) / (Math.Sqrt(2 * Math.PI) * sigma)); ;
        }

        public double CDF(double x, double mu = 0, double sigma = 1)
        {
            return (1 + Erf((x - mu) / Math.Sqrt(2) / sigma)) / 2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreatAliance();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CreateEnemy();
            System.Threading.TimerCallback callback = TimerEvent;
            timer = new System.Threading.Timer(callback, "", 0, 1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer.Dispose();
        }

        public void TimerEvent(Object obj) { this.Invoke(new MethodInvoker(delegate () { Caller(); })); }

        public void CreateEnemy()
        {
            ListView LV1 = listView1;
            ListView LV2 = listView2;

            if (LV2.Items.Count == 0)
            {
                int Warrior = RandomNo.Next(LV1.Items.Count / 2, LV1.Items.Count * 15 / 10);
                for(int i = 1; i < Warrior; i++)
                {
                    int Numbering, Atk, Def, HP, Mutant;
                    Numbering = LV2.Items.Count + 1;
                    
                    Mutant = RandomNo.Next(1, 11);
                    Atk = (int)PDF(Mutant, MaxAtk);
                    Mutant = RandomNo.Next(1, 11);
                    Def = (int)PDF(Mutant, MaxDef);

                    HP = (Atk + Def * 2) * 8 / 10;

                    String mName = "Enemy " + Numbering.ToString();
                    
                    String[] CharacterSet = { Numbering.ToString(), mName, Atk.ToString(), Def.ToString(), HP.ToString(), "0", "0" };
                    ListViewItem lvt = new ListViewItem(CharacterSet);
                    LV2.Items.Add(lvt);
                }
                // LV1.Sorting = SortOrder.Ascending;
                MaxAtk = 10;
                MaxDef = 10;
            }
        }

        public void CreatAliance()
        {
            ListView LV1 = listView1;

            AlianceCount++;
            int mLevel = 0, Atk = 10, Def = 10, HP = Atk + Def * 2;
            String mName = "Char " + AlianceCount.ToString();

            String[] CharacterSet = { AlianceCount.ToString(), mName, mLevel.ToString(), Atk.ToString(), Def.ToString(), HP.ToString(), "0" };
            ListViewItem lvt = new ListViewItem(CharacterSet);
            LV1.Items.Add(lvt);

            for (int i = 1; i <= LV1.Items.Count; i++)
            {
                int Numbering1 = int.Parse(LV1.Items[0].SubItems[0].Text);
                string mName1 = LV1.Items[0].SubItems[1].Text;
                int mLevel1 = int.Parse(LV1.Items[0].SubItems[2].Text);
                int Atk1 = int.Parse(LV1.Items[0].SubItems[3].Text);
                int Def1 = int.Parse(LV1.Items[0].SubItems[4].Text);
                int HP1 = int.Parse(LV1.Items[0].SubItems[5].Text);
                int Exp1 = int.Parse(LV1.Items[0].SubItems[6].Text);

                HP1 = Atk1 + Def1 * 2;

                LV1.Items.RemoveAt(0);
                String[] CharacterSet2 = { Numbering1.ToString(), mName1, mLevel1.ToString(), Atk1.ToString(), Def1.ToString(), HP1.ToString(), Exp1.ToString() };
                ListViewItem lvt2 = new ListViewItem(CharacterSet2);
                LV1.Items.Add(lvt2);
            }
        }

        public void Caller()
        {
            ListView LV1 = listView1;
            ListView LV2 = listView2;

            if (LV1.Items.Count < 11)
            {
                CreatAliance();
            }
            else if (Battlechecker == 0 && LV2.Items.Count != 0)
            {
                Battlechecker++;
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
                int HP2 = int.Parse(LV2.Items[Defender].SubItems[4].Text) + 1;

                HP2 -= (2 * Atk1 - Def2 > 0) ? (2 * Atk1 - Def2) : 1;
                Exp1 += (2 * Atk1 - Def2 > 0) ? (2 * Atk1 - Def2) : 1;

                if (Exp1 >= mLevel1 * 100)
                {
                    mLevel1++;
                    int sUp = RandomNo.Next(1, 100);
                    Atk1 += (sUp <= 65) ? 1 : 0;
                    Def1 += (sUp >= 35) ? 1 : 0;
                    HP1 = Atk1 + Def1 * 2;
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
            else if (Battlechecker != 0 && LV2.Items.Count != 0)
            {
                Battlechecker--;
                int Defender = RandomNo.Next(1, LV1.Items.Count) - 1;

                int Numbering1 = int.Parse(LV1.Items[Defender].SubItems[0].Text);
                string mName1 = LV1.Items[Defender].SubItems[1].Text;
                int mLevel1 = int.Parse(LV1.Items[Defender].SubItems[2].Text);
                int Atk1 = int.Parse(LV1.Items[Defender].SubItems[3].Text);
                int Def1 = int.Parse(LV1.Items[Defender].SubItems[4].Text);
                int HP1 = int.Parse(LV1.Items[Defender].SubItems[5].Text);
                int Exp1 = int.Parse(LV1.Items[Defender].SubItems[6].Text);

                int Numbering2 = int.Parse(LV2.Items[0].SubItems[0].Text);
                string mName2 = LV2.Items[0].SubItems[1].Text;
                int Atk2 = int.Parse(LV2.Items[0].SubItems[2].Text);
                int Def2 = int.Parse(LV2.Items[0].SubItems[3].Text);
                int HP2 = int.Parse(LV2.Items[0].SubItems[4].Text) + 1;

                HP1 -= (2 * Atk2 - Def1 > 0) ? (2 * Atk2 - Def1) : 1;

                if (HP1 > 0)
                {
                    LV1.Items.RemoveAt(Defender);
                    String[] CharacterSet1 = { Numbering1.ToString(), mName1, mLevel1.ToString(), Atk1.ToString(), Def1.ToString(), HP1.ToString(), Exp1.ToString() };
                    ListViewItem lvt1 = new ListViewItem(CharacterSet1);
                    LV1.Items.Add(lvt1);
                }
                else
                    LV1.Items.RemoveAt(Defender);
                    
                LV2.Items.RemoveAt(0);
                String[] CharacterSet2 = { Numbering2.ToString(), mName2, Atk2.ToString(), Def2.ToString(), HP2.ToString() };
                ListViewItem lvt2 = new ListViewItem(CharacterSet2);
                LV2.Items.Add(lvt2);
            }
            else if (LV2.Items.Count == 0)
            {
                for (int i = 1; i <= LV1.Items.Count; i++)
                {
                    int Numbering1 = int.Parse(LV1.Items[0].SubItems[0].Text);
                    string mName1 = LV1.Items[0].SubItems[1].Text;
                    int mLevel1 = int.Parse(LV1.Items[0].SubItems[2].Text);
                    int Atk1 = int.Parse(LV1.Items[0].SubItems[3].Text);
                    int Def1 = int.Parse(LV1.Items[0].SubItems[4].Text);
                    int HP1 = int.Parse(LV1.Items[0].SubItems[5].Text);
                    int Exp1 = int.Parse(LV1.Items[0].SubItems[6].Text);
                    
                    HP1 = Atk1 + Def1 * 2;

                    LV1.Items.RemoveAt(0);
                    String[] CharacterSet = { Numbering1.ToString(), mName1, mLevel1.ToString(), Atk1.ToString(), Def1.ToString(), HP1.ToString(), Exp1.ToString() };
                    ListViewItem lvt = new ListViewItem(CharacterSet);
                    LV1.Items.Add(lvt);
                }
                CreateEnemy();
            }
        }

        public Form1()
        {
            InitializeComponent();
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.Columns.Add("No", 30, HorizontalAlignment.Center);
            listView1.Columns.Add("Name", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("Level", 50, HorizontalAlignment.Center);
            listView1.Columns.Add("Atk", 40, HorizontalAlignment.Center);
            listView1.Columns.Add("Def", 40, HorizontalAlignment.Center);
            listView1.Columns.Add("HP", 40, HorizontalAlignment.Center);
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
