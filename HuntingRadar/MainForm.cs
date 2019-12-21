using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MemoryApi;
namespace HuntingRadar
{
    public partial class MainForm : Form
    {
        ProcessMemoryReader mReader = new ProcessMemoryReader();
        Antenna A = new Antenna();
        Dictionary<string, string> huntinglist = new Dictionary<string, string>();

        public MainForm()
        {
            InitializeComponent();
            Thread Find = new Thread(new ThreadStart(Find_Game));
            Find.Start();
            ReadHuntingList();
            
        }

        public void Find_Game()
        {
            while (true)
            {
                Thread.Sleep(3000);
                if (mReader.FindProcess("ffxiv_dx11"))
                {
                    Action isrunning = delegate ()
                    {
                        lGamestatus.Text = "Game Is Running";
                        lGamestatus.ForeColor = Color.Green;
                        A.StartUpdate();
                    };
                    Invoke(isrunning);
                }
                else
                {
                    Action notrunning = delegate ()
                    {
                        lGamestatus.Text = "Game Is Not Running";
                        lGamestatus.ForeColor = Color.Red;
                        A.EndUpdate();
                    };
                    Invoke(notrunning);
                }
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            
            for (int i = 0;i<A.combatants.Count;i++)
            {
                try
                {
                    CombatantListBox.Items[i] = A.combatants[i].Name+"|"+ A.combatants[i].ID;
                }
                catch
                {
                    CombatantListBox.Items.Add(A.combatants[i].Name);
                }
            }
            
            while(true)
            {
                if (CombatantListBox.Items.Count == A.combatants.Count) break;
                else CombatantListBox.Items.RemoveAt(CombatantListBox.Items.Count-1);
            }
        }

        private void ReadHuntingList()
        {
            Regex reg = new Regex(@"^(.*),(.*)");
            StreamReader sr;
            string line = "";
            try
            {
                sr = new StreamReader("Hunting List.txt");
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.StartsWith("#")) continue;
                    Match match = reg.Match(line);
                    huntinglist[match.Groups[1].Value.Trim()] = match.Groups[2].Value.Trim();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(),"狩猎怪名单错误");
            }
            
        }

    }
}
