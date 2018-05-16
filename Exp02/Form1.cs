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
        public Random RandomNo = new Random();
        public string[] CharacterSet = { "", "", "", "", "", "", "" };

        private void button1_Click(object sender, EventArgs e)
        {
            ListView LV1 = listView1;
            int Numbering = LV1.Items.Count + 1, mLevel = 1, Atk = Roller(), Def = Roller(), HP = Atk * 1 + Def * 10;
            String mName = Numbering.ToString() + "Char";
            mLevel = 1;

            String[] CharacterSet = { Numbering.ToString(), mName, mLevel.ToString(), Atk.ToString(), Def.ToString(), HP.ToString(), "90" };
            ListViewItem lvt = new ListViewItem(CharacterSet);
            LV1.Items.Add(lvt);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CreateEnemy();
            System.Threading.TimerCallback callback = TimerEvent;
            timer = new System.Threading.Timer(callback, "", 0, 10);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
        }

        public void TimerEvent(Object obj) { this.Invoke(new MethodInvoker(delegate () { Caller(); })); }

        public void Caller()
        {
            ListView LV1 = listView1;
            ListView LV2 = listView2;
            LV1.BeginUpdate();
            LV2.BeginUpdate();
            int result = Battle();
            LV1.EndUpdate();
            LV2.EndUpdate();
            if( result == 0 )
                timer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
        }

        public int Battle()
        {
            ListView LV1 = listView1;
            ListView LV2 = listView2;
            AlianceTurn();
            EnemyTurn();
            return LV2.Items.Count;
        }

        public void CreateEnemy()
        {
            ListView LV1 = listView1;
            ListView LV2 = listView2;
            LV1.BeginUpdate();
            LV2.BeginUpdate();
            int Warrior = RandomNo.Next(1, LV1.Items.Count);
            int LoopIndex = 0;
            do
            {
                LoopIndex++;
                int Numbering = LV2.Items.Count + 1, mLevel = 1, Atk = Roller(), Def = Roller(), HP = Atk + Def * 10;
                String mName = "Enemy " + Numbering.ToString();
                mLevel = 1;

                String[] CharacterSet = { Numbering.ToString(), mName, mLevel.ToString(), Atk.ToString(), Def.ToString(), HP.ToString(), "0" };
                ListViewItem lvt = new ListViewItem(CharacterSet);
                LV2.Items.Add(lvt);
            } while (LoopIndex < Warrior);
            LV1.EndUpdate();
            LV2.EndUpdate();
        }

        public void AlianceTurn()
        {
            ListView LV1 = listView1;
            ListView LV2 = listView2;
            int Attacker=0;
            do
            {
                if (LV2.Items.Count == 0)
                    break;

                LV1.BeginUpdate();
                LV2.BeginUpdate();
                int Defender = RandomNo.Next(1, LV2.Items.Count) - 1;

                int Numbering1 = int.Parse(LV1.Items[Attacker].SubItems[0].Text);
                string mName1 = LV1.Items[Attacker].SubItems[1].Text;
                int mLevel1 = int.Parse(LV1.Items[Attacker].SubItems[2].Text);
                int Atk1 = int.Parse(LV1.Items[Attacker].SubItems[3].Text);
                int Def1 = int.Parse(LV1.Items[Attacker].SubItems[4].Text);
                int HP1 = int.Parse(LV1.Items[Attacker].SubItems[5].Text);
                int Exp1 = int.Parse(LV1.Items[Attacker].SubItems[6].Text);

                int Numbering2 = int.Parse(LV2.Items[Defender].SubItems[0].Text);
                string mName2 = LV2.Items[Defender].SubItems[1].Text;
                int mLevel2 = int.Parse(LV2.Items[Defender].SubItems[2].Text);
                int Atk2 = int.Parse(LV2.Items[Defender].SubItems[3].Text);
                int Def2 = int.Parse(LV2.Items[Defender].SubItems[4].Text);
                int HP2 = int.Parse(LV2.Items[Defender].SubItems[5].Text);

                HP2 = (Atk1 > Def2) ? (HP2 - (Atk1 - Def2)) : (HP2 - 1);
                Exp1 = (HP2 > 0) ? Exp1 : (Exp1 + mLevel2);
                if (Exp1 >= 100)
                    LevelUp(Numbering1);

                LV1.Items.RemoveAt(Attacker);
                String[] CharacterSet = { Numbering1.ToString(), mName1, mLevel1.ToString(), Atk1.ToString(), Def1.ToString(), HP1.ToString(), Exp1.ToString() };
                ListViewItem lvt = new ListViewItem(CharacterSet);
                LV1.Items.Add(lvt);

                if(HP2 >0)
                {
                    LV2.Items.RemoveAt(Defender);
                    String[] CharacterSet2 = { Numbering2.ToString(), mName2, mLevel1.ToString(), Atk2.ToString(), Def2.ToString(), HP2.ToString() };
                    ListViewItem lvt2 = new ListViewItem(CharacterSet2);
                    LV2.Items.Add(lvt2);
                }
                else
                    LV2.Items.RemoveAt(Defender);

                Attacker++;
                LV1.EndUpdate();
                LV2.EndUpdate();
            } while (Attacker < LV1.Items.Count);
        }

        public void EnemyTurn()
        {
            /*
            ListView LV1 = listView1;
            ListView LV2 = listView2;
            int Defender = RandomNo.Next(1, LV1.Items.Count);
            int Attacker = RandomNo.Next(1, LV2.Items.Count);
            */
        }

        public void LevelUp(int CharIndex)
        {
            ListView LV1 = listView1;

            int Number = CharIndex - 1;
            int Numbering = int.Parse(LV1.Items[Number].SubItems[0].Text);
            string mName = LV1.Items[Number].SubItems[1].Text;
            int mLevel = int.Parse(LV1.Items[Number].SubItems[2].Text);
            int Atk = int.Parse(LV1.Items[Number].SubItems[3].Text);
            int Def = int.Parse(LV1.Items[Number].SubItems[4].Text);
            
            mLevel++;

            int sUp = RandomNo.Next(1, 2);
            Atk += (sUp == 1) ? 1 : 0;
            Def += (sUp == 2) ? 1 : 0;
            int HP = Atk + Def * 10;

            LV1.Items.RemoveAt(Number);
            String[] CharacterSet = { Numbering.ToString(), mName, mLevel.ToString(), Atk.ToString(), Def.ToString(), HP.ToString(), "0" };
            ListViewItem lvt = new ListViewItem(CharacterSet);
            LV1.Items.Add(lvt);
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
            listView1.Columns.Add("No", 30);
            listView1.Columns.Add("Name", 100);
            listView1.Columns.Add("Level", 60);
            listView1.Columns.Add("Atk", 40);
            listView1.Columns.Add("Def", 40);
            listView1.Columns.Add("HP", 40);
            listView1.Columns.Add("Exp", 40);
            listView2.View = View.Details;
            listView2.FullRowSelect = true;
            listView2.GridLines = true;
            listView2.Columns.Add("No", 30);
            listView2.Columns.Add("Name", 100);
            listView2.Columns.Add("Level", 60);
            listView2.Columns.Add("Atk", 40);
            listView2.Columns.Add("Def", 40);
            listView2.Columns.Add("HP", 40);
        }
    }
}
