using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using KeyboardApi;
namespace Ninja_Assistant
{
    public partial class MainForm : Form
    {
        [DllImport("user32.dll")] private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        public IntPtr Gameptr;
        private bool APengage = false;

        private const int WM_HOTKEY = 0x312;
        private const int WM_CREATE = 0x1;
        private const int WM_DESTROY = 0x2;

        private const int Dun1ID = 0x13571;
        private const int Dun2ID = 0x13572;
        private const int Dun3ID = 0x13573;
        private const int Dun4ID = 0x13574;
        private const int Dun5ID = 0x13575;
        private const int Dun6ID = 0x13576;
        private const int Dun7ID = 0x13577;
        private const int Dun8ID = 0x13578;
        Mudra M = new Mudra();

        public MainForm()
        {
            InitializeComponent();
            debugbox.AppendText("全局供电开启...\r\n");
            Thread Find = new Thread(new ThreadStart(Find_Game));
            Find.Start();
        }

        public void Find_Game()
        {
            while (true)
            {
                Thread.Sleep(1000);

                IntPtr ptr = FindWindow(null, "最终幻想XIV");
                if (ptr != (IntPtr)0)
                {
                    Action isrunning = delegate ()
                    {
                        Isopen.Text = "游戏已运行";
                        Isopen.ForeColor = Color.Green;
                        Gameptr = ptr;
                        StartBtn.Enabled = true;
                    };
                    Invoke(isrunning);
                }
                else
                {
                    Action notrunning = delegate ()
                    {
                        Isopen.Text = "游戏未运行";
                        Isopen.ForeColor = Color.Red;
                        StartBtn.Enabled = false;
                    };
                    Invoke(notrunning);
                }
            }
        }

        private void start_Click(object sender, EventArgs e)
        {
            LoadCFG();
            if (APengage)
            {
                Tparam.Enabled = true;
                TvK.Enabled = true;
                Cparam.Enabled = true;
                CvK.Enabled = true;
                Jparam.Enabled = true;
                JvK.Enabled = true;
                NinjutsuParam.Enabled = true;
                NinjutsuvK.Enabled = true;

                M.Uninstall();

                APengage = false;
                StartBtn.Text = "开启辅助";
                StartBtn.ForeColor = Color.Green;
                debugbox.AppendText("停止一键兔忍。" + Environment.NewLine);
            }
            else
            {
                Tparam.Enabled = false;
                TvK.Enabled = false;
                Cparam.Enabled = false;
                CvK.Enabled = false;
                Jparam.Enabled = false;
                JvK.Enabled = false;
                NinjutsuParam.Enabled = false;
                NinjutsuvK.Enabled = false;

                string[] setting = new string[8];
                setting[0] = Tparam.Text;
                setting[1] = TvK.Text;
                setting[2] = Cparam.Text;
                setting[3] = CvK.Text;
                setting[4] = Jparam.Text;
                setting[5] = JvK.Text;
                setting[6] = NinjutsuParam.Text;
                setting[7] = NinjutsuvK.Text;
                M.Install(Gameptr, setting);

                APengage = true;
                StartBtn.Text = "关闭辅助";
                StartBtn.ForeColor = Color.Red;
                debugbox.AppendText("开始一键兔忍。" + Environment.NewLine);
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    if (APengage)
                    {
                        switch (m.WParam.ToInt32())
                        {
                            case Dun1ID:
                                Thread Huton = new Thread(new ThreadStart(M.Huton));
                                Huton.Start();
                                break;
                            case Dun2ID:
                                Thread Raiton = new Thread(new ThreadStart(M.Raiton));
                                Raiton.Start();
                                break;
                            case Dun3ID:
                                Thread Katon = new Thread(new ThreadStart(M.Katon));
                                Katon.Start();
                                break;
                            case Dun4ID:
                                Thread Doton = new Thread(new ThreadStart(M.Doton));
                                Doton.Start();
                                break;
                            case Dun5ID:
                                Thread Suiton = new Thread(new ThreadStart(M.Suiton));
                                Suiton.Start();
                                break;
                            case Dun6ID:
                                Thread Hyoton = new Thread(new ThreadStart(M.Hyoton));
                                Hyoton.Start();
                                break;
                            case Dun7ID:
                                Thread Shuriken = new Thread(new ThreadStart(M.Shuriken));
                                Shuriken.Start();
                                break;
                            case Dun8ID:
                                Thread TCJ = new Thread(new ThreadStart(M.TCJ));
                                TCJ.Start();
                                break;

                            default:
                                break;
                        }
                    }
                    break;
                case WM_DESTROY:
                    break;
                default:
                    break;
            }
        }

        private void RegDun1(object sender, EventArgs e)
        {
            LoadCFG();
            uint Modifier = AppHotKey.ModifyKey(Configuration.Default.Ton1c, Configuration.Default.Ton1a, Configuration.Default.Ton1s);
            uint vK = Simulator.Transcoding(Configuration.Default.Ton1vK);
            if (chkActivate1.Checked == true)
            {
                AppHotKey.RegKey(Handle, Dun1ID, Modifier, vK);
                Ton1c.Enabled = false;
                Ton1a.Enabled = false;
                Ton1s.Enabled = false;
                Ton1vK.Enabled = false;
                debugbox.AppendText("风遁已注册。" + Environment.NewLine);
            }
            else
            {
                AppHotKey.UnRegKey(Handle, Dun1ID);
                Ton1c.Enabled = true;
                Ton1a.Enabled = true;
                Ton1s.Enabled = true;
                Ton1vK.Enabled = true;
                debugbox.AppendText("风遁已卸载。" + Environment.NewLine);
            }
        }

        private void RegDun2(object sender, EventArgs e)
        {
            LoadCFG();
            uint Modifier = AppHotKey.ModifyKey(Configuration.Default.Ton2c, Configuration.Default.Ton2a, Configuration.Default.Ton2s);
            uint vK = Simulator.Transcoding(Configuration.Default.Ton2vK);
            if (chkActivate2.Checked == true)
            {
                AppHotKey.RegKey(Handle, Dun2ID, Modifier, vK);
                Ton2c.Enabled = false;
                Ton2a.Enabled = false;
                Ton2s.Enabled = false;
                Ton2vK.Enabled = false;
                debugbox.AppendText("雷遁已注册。" + Environment.NewLine);
            }
            else
            {
                AppHotKey.UnRegKey(Handle, Dun2ID);
                Ton2c.Enabled = true;
                Ton2a.Enabled = true;
                Ton2s.Enabled = true;
                Ton2vK.Enabled = true;
                debugbox.AppendText("雷遁已卸载。" + Environment.NewLine);
            }
        }

        private void RegDun3(object sender, EventArgs e)
        {
            LoadCFG();
            uint Modifier = AppHotKey.ModifyKey(Configuration.Default.Ton3c, Configuration.Default.Ton3a, Configuration.Default.Ton3s);
            uint vK = Simulator.Transcoding(Configuration.Default.Ton3vK);
            if (chkActivate3.Checked == true)
            {
                AppHotKey.RegKey(Handle, Dun3ID, Modifier, vK);
                Ton3c.Enabled = false;
                Ton3a.Enabled = false;
                Ton3s.Enabled = false;
                Ton3vK.Enabled = false;
                debugbox.AppendText("火遁已注册。" + Environment.NewLine);
            }
            else
            {
                AppHotKey.UnRegKey(Handle, Dun3ID);
                Ton3c.Enabled = true;
                Ton3a.Enabled = true;
                Ton3s.Enabled = true;
                Ton3vK.Enabled = true;
                debugbox.AppendText("火遁已卸载。" + Environment.NewLine);
            }
        }

        private void RegDun4(object sender, EventArgs e)
        {
            LoadCFG();
            uint Modifier = AppHotKey.ModifyKey(Configuration.Default.Ton4c, Configuration.Default.Ton4a, Configuration.Default.Ton4s);
            uint vK = Simulator.Transcoding(Configuration.Default.Ton4vK);
            if (chkActivate4.Checked == true)
            {
                AppHotKey.RegKey(Handle, Dun4ID, Modifier, vK);
                Ton4c.Enabled = false;
                Ton4a.Enabled = false;
                Ton4s.Enabled = false;
                Ton4vK.Enabled = false;
                debugbox.AppendText("土遁已注册。" + Environment.NewLine);
            }
            else
            {
                AppHotKey.UnRegKey(Handle, Dun4ID);
                Ton4c.Enabled = true;
                Ton4a.Enabled = true;
                Ton4s.Enabled = true;
                Ton4vK.Enabled = true;
                debugbox.AppendText("土遁已卸载。" + Environment.NewLine);
            }
        }

        private void RegDun5(object sender, EventArgs e)
        {
            LoadCFG();
            uint Modifier = AppHotKey.ModifyKey(Configuration.Default.Ton5c, Configuration.Default.Ton5a, Configuration.Default.Ton5s);
            uint vK = Simulator.Transcoding(Configuration.Default.Ton5vK);
            if (chkActivate5.Checked == true)
            {
                AppHotKey.RegKey(Handle, Dun5ID, Modifier, vK);
                Ton5c.Enabled = false;
                Ton5a.Enabled = false;
                Ton5s.Enabled = false;
                Ton5vK.Enabled = false;
                debugbox.AppendText("水遁已注册。" + Environment.NewLine);
            }
            else
            {
                AppHotKey.UnRegKey(Handle, Dun5ID);
                Ton5c.Enabled = true;
                Ton5a.Enabled = true;
                Ton5s.Enabled = true;
                Ton5vK.Enabled = true;
                debugbox.AppendText("水遁已卸载。" + Environment.NewLine);
            }
        }

        private void RegDun6(object sender, EventArgs e)
        {
            LoadCFG();
            uint Modifier = AppHotKey.ModifyKey(Configuration.Default.Ton6c, Configuration.Default.Ton6a, Configuration.Default.Ton6s);
            uint vK = Simulator.Transcoding(Configuration.Default.Ton6vK);
            if (chkActivate6.Checked == true)
            {
                AppHotKey.RegKey(Handle, Dun6ID, Modifier, vK);
                Ton6c.Enabled = false;
                Ton6a.Enabled = false;
                Ton6s.Enabled = false;
                Ton6vK.Enabled = false;
                debugbox.AppendText("冰遁已注册。" + Environment.NewLine);
            }
            else
            {
                AppHotKey.UnRegKey(Handle, Dun6ID);
                Ton6c.Enabled = true;
                Ton6a.Enabled = true;
                Ton6s.Enabled = true;
                Ton6vK.Enabled = true;
                debugbox.AppendText("冰遁已卸载。" + Environment.NewLine);
            }
        }

        private void RegDun7(object sender, EventArgs e)
        {
            LoadCFG();
            uint Modifier = AppHotKey.ModifyKey(Configuration.Default.Ton7c, Configuration.Default.Ton7a, Configuration.Default.Ton7s);
            uint vK = Simulator.Transcoding(Configuration.Default.Ton7vK);
            if (chkActivate7.Checked == true)
            {
                AppHotKey.RegKey(Handle, Dun7ID, Modifier, vK);
                Ton7c.Enabled = false;
                Ton7a.Enabled = false;
                Ton7s.Enabled = false;
                Ton7vK.Enabled = false;
                debugbox.AppendText("手里剑已注册。" + Environment.NewLine);
            }
            else
            {
                AppHotKey.UnRegKey(Handle, Dun7ID);
                Ton7c.Enabled = true;
                Ton7a.Enabled = true;
                Ton7s.Enabled = true;
                Ton7vK.Enabled = true;
                debugbox.AppendText("手里剑已卸载。" + Environment.NewLine);
            }
        }

        private void RegDun8(object sender, EventArgs e)
        {
            LoadCFG();
            uint Modifier = AppHotKey.ModifyKey(Configuration.Default.Ton8c, Configuration.Default.Ton8a, Configuration.Default.Ton8s);
            uint vK = Simulator.Transcoding(Configuration.Default.Ton8vK);
            if (chkActivate8.Checked == true)
            {
                AppHotKey.RegKey(Handle, Dun8ID, Modifier, vK);
                Ton8c.Enabled = false;
                Ton8a.Enabled = false;
                Ton8s.Enabled = false;
                Ton8vK.Enabled = false;
                debugbox.AppendText("天地人已注册。" + Environment.NewLine);
            }
            else
            {
                AppHotKey.UnRegKey(Handle, Dun8ID);
                Ton8c.Enabled = true;
                Ton8a.Enabled = true;
                Ton8s.Enabled = true;
                Ton8vK.Enabled = true;
                debugbox.AppendText("天地人已卸载。" + Environment.NewLine);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadCFG();
            debugbox.AppendText("加载配置文件。" + Environment.NewLine);
        }

        private void LoadCFG()
        {
            Tparam.Text = Configuration.Default.Tparam;
            TvK.Text = Configuration.Default.TvK;
            Cparam.Text = Configuration.Default.Cparam;
            CvK.Text = Configuration.Default.CvK;
            Jparam.Text = Configuration.Default.Jparam;
            JvK.Text = Configuration.Default.JvK;
            NinjutsuParam.Text = Configuration.Default.NinjutsuParam;
            NinjutsuvK.Text = Configuration.Default.NinjutsuvK;

            Ton1c.Checked = Configuration.Default.Ton1c;
            Ton1a.Checked = Configuration.Default.Ton1a;
            Ton1s.Checked = Configuration.Default.Ton1s;
            Ton2c.Checked = Configuration.Default.Ton2c;
            Ton2a.Checked = Configuration.Default.Ton2a;
            Ton2s.Checked = Configuration.Default.Ton2s;
            Ton3c.Checked = Configuration.Default.Ton3c;
            Ton3a.Checked = Configuration.Default.Ton3a;
            Ton3s.Checked = Configuration.Default.Ton3s;
            Ton4c.Checked = Configuration.Default.Ton4c;
            Ton4a.Checked = Configuration.Default.Ton4a;
            Ton4s.Checked = Configuration.Default.Ton4s;
            Ton5c.Checked = Configuration.Default.Ton5c;
            Ton5a.Checked = Configuration.Default.Ton5a;
            Ton5s.Checked = Configuration.Default.Ton5s;
            Ton6c.Checked = Configuration.Default.Ton6c;
            Ton6a.Checked = Configuration.Default.Ton6a;
            Ton6s.Checked = Configuration.Default.Ton6s;
            Ton7c.Checked = Configuration.Default.Ton7c;
            Ton7a.Checked = Configuration.Default.Ton7a;
            Ton7s.Checked = Configuration.Default.Ton7s;
            Ton8c.Checked = Configuration.Default.Ton8c;
            Ton8a.Checked = Configuration.Default.Ton8a;
            Ton8s.Checked = Configuration.Default.Ton8s;

            Ton1vK.Text = Configuration.Default.Ton1vK;
            Ton2vK.Text = Configuration.Default.Ton2vK;
            Ton3vK.Text = Configuration.Default.Ton3vK;
            Ton4vK.Text = Configuration.Default.Ton4vK;
            Ton5vK.Text = Configuration.Default.Ton5vK;
            Ton6vK.Text = Configuration.Default.Ton6vK;
            Ton7vK.Text = Configuration.Default.Ton7vK;
            Ton8vK.Text = Configuration.Default.Ton8vK;
        }

        private void SaveCFG()
        {
            Configuration.Default.Tparam = Tparam.Text;
            Configuration.Default.TvK = TvK.Text;
            Configuration.Default.Cparam = Cparam.Text;
            Configuration.Default.CvK = CvK.Text;
            Configuration.Default.Jparam = Jparam.Text;
            Configuration.Default.JvK = JvK.Text;
            Configuration.Default.NinjutsuParam = NinjutsuParam.Text;
            Configuration.Default.NinjutsuvK = NinjutsuvK.Text;

            Configuration.Default.Ton1c = Ton1c.Checked;
            Configuration.Default.Ton1a = Ton1a.Checked;
            Configuration.Default.Ton1s = Ton1s.Checked;
            Configuration.Default.Ton2c = Ton2c.Checked;
            Configuration.Default.Ton2a = Ton2a.Checked;
            Configuration.Default.Ton2s = Ton2s.Checked;
            Configuration.Default.Ton3c = Ton3c.Checked;
            Configuration.Default.Ton3a = Ton3a.Checked;
            Configuration.Default.Ton3s = Ton3s.Checked;
            Configuration.Default.Ton4c = Ton4c.Checked;
            Configuration.Default.Ton4a = Ton4a.Checked;
            Configuration.Default.Ton4s = Ton4s.Checked;
            Configuration.Default.Ton5c = Ton5c.Checked;
            Configuration.Default.Ton5a = Ton5a.Checked;
            Configuration.Default.Ton5s = Ton5s.Checked;
            Configuration.Default.Ton6c = Ton6c.Checked;
            Configuration.Default.Ton6a = Ton6a.Checked;
            Configuration.Default.Ton6s = Ton6s.Checked;
            Configuration.Default.Ton7c = Ton7c.Checked;
            Configuration.Default.Ton7a = Ton7a.Checked;
            Configuration.Default.Ton7s = Ton7s.Checked;
            Configuration.Default.Ton8c = Ton8c.Checked;
            Configuration.Default.Ton8a = Ton8a.Checked;
            Configuration.Default.Ton8s = Ton8s.Checked;

            Configuration.Default.Ton1vK = Ton1vK.Text;
            Configuration.Default.Ton2vK = Ton2vK.Text;
            Configuration.Default.Ton3vK = Ton3vK.Text;
            Configuration.Default.Ton4vK = Ton4vK.Text;
            Configuration.Default.Ton5vK = Ton5vK.Text;
            Configuration.Default.Ton6vK = Ton6vK.Text;
            Configuration.Default.Ton7vK = Ton7vK.Text;
            Configuration.Default.Ton8vK = Ton8vK.Text;

            Configuration.Default.Save();
        }

        private void SaveConfig_Click(object sender, EventArgs e)
        {
            SaveCFG();
            debugbox.AppendText("配置文件已保存。" + Environment.NewLine);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            AppHotKey.UnRegKey(Handle, Dun1ID);
            AppHotKey.UnRegKey(Handle, Dun2ID);
            AppHotKey.UnRegKey(Handle, Dun3ID);
            AppHotKey.UnRegKey(Handle, Dun4ID);
            AppHotKey.UnRegKey(Handle, Dun5ID);
            AppHotKey.UnRegKey(Handle, Dun6ID);
            AppHotKey.UnRegKey(Handle, Dun7ID);
            Environment.Exit(0);
        }

        private void debugbox_TextChanged(object sender, EventArgs e)
        {
            debugbox.SelectionStart = debugbox.Text.Length; //Set the current caret position at the end
            debugbox.ScrollToCaret(); //Now scroll it automatically
        }

        
    }
}
