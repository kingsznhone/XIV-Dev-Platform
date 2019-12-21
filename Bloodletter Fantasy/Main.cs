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
using System.Speech.Synthesis;
using System.Diagnostics;
using MemoryApi;
using KeyboardApi;

namespace BloodLetter_Fantasy
{
    public partial class Main : Form
    {
        [DllImport("user32.dll")] private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        public IntPtr Gameptr;

        SpeechSynthesizer Speaker = new SpeechSynthesizer();
        private bool MasterSwitch = false;
        private bool APEngage = false;
        long[] ModulePointers = new long[] { 0x1AABDE0, 0x38, 0x18, 0x30, 0x20 };

        long pModuleHotbar;
        long BLptr = 0;

        int CD;

        private const int WM_HOTKEY = 0x312;
        private const int WM_CREATE = 0x1;
        private const int WM_DESTROY = 0x2;
        private const int HKID = 0x3721;

        const int BLID = 0x6E;
        const int MEID = 0x67;
        const int SSID = 0x19;

        ProcessMemoryReader mReader = new ProcessMemoryReader();

        Autopilot BLAP = new Autopilot();

        public Main()
        {
            InitializeComponent();
            Thread F = new Thread(new ThreadStart(Find_Game));
            F.Start();
            LoadCFG();
            InitTTS();
            AppHotKey.UnRegKey(Handle, HKID);
            LoadBlackOut();
        }

        public void InitTTS()
        {
            Speaker.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Teen);
            Speaker.Volume = 100;  // (0 - 100)
            Speaker.Rate = 0;
            Speaker.SpeakAsync("Initialized");
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
                        GameStatusLB.Text = "Game Is Running";
                        GameStatusLB.ForeColor = Color.Green;
                        StartBtn.Enabled = true;

                        Process p = Process.GetProcessesByName("ffxiv_dx11").ToList().FirstOrDefault();
                        mReader.process = p;
                        mReader.OpenProcess();
                        FindPtr();

                        timer1.Enabled = true;
                    };
                    Invoke(isrunning);
                }
                else
                {
                    Action notrunning = delegate ()
                    {
                        GameStatusLB.Text = "Game Is Not Running";
                        GameStatusLB.ForeColor = Color.Red;
                        StartBtn.Enabled = false;
                        GameStatusLB.Enabled = false;
                        timer1.Enabled = false;
                    };
                    Invoke(notrunning);
                }
            }
        }

        public void FindPtr()
        {

            pModuleHotbar = mReader.ReadPtr_x64(ModulePointers);
            BLptr = ScanSkill(BLID);
        }



        public long ScanSkill(long ID)
        {
            int HotbarID;
            long IDPtr = pModuleHotbar + 0x3C;
            for (int j = 1; j <= 10; j++)
            {
                for (int i = 1; i <= 12; i++)
                {
                    if (mReader.ReadInt32((IntPtr)(IDPtr)) == 42)
                    {
                        HotbarID = mReader.ReadInt32((IntPtr)(IDPtr + 0xC));

                        if (HotbarID == ID)
                        {
                            this.pictureBox1.Image = Properties.Resources.BloodLetter;
                            return IDPtr + 0x1C;
                        }
                        else
                        {
                            IDPtr += 0x2C;
                        }
                    }
                    else
                    {
                        IDPtr += 0x2C;
                    }
                }
                IDPtr += 0x108;
            }
            LoadBlackOut();
            return 0;
        }

        public int ScanCoolDown(long Skillptr)
        {
            int CD;
            bool Availability;
            if (Skillptr != 0)
            {
                Availability = BitConverter.ToBoolean(mReader.ReadByteArray((IntPtr)(Skillptr - 0x4), 4), 0);
                if (Availability)
                {
                    CD = mReader.ReadInt32((IntPtr)(Skillptr));
                    return CD == -1 ? 0 : CD;
                }
                else return -1;
            }
            else return -2;
        }


        public void LoadCFG()
        {
            BLParam.Text = Configuration.Default.BLParam;
            BLvK.Text = Configuration.Default.BLvK;
            AutoC.Checked = Configuration.Default.AutoC;
            AutoA.Checked = Configuration.Default.AutoA;
            AutoS.Checked = Configuration.Default.AutoS;
            AutovK.Text = Configuration.Default.AutovK;
        }

        public void SaveCFG()
        {
            Configuration.Default.BLParam = BLParam.Text;
            Configuration.Default.BLvK = BLvK.Text;
            Configuration.Default.AutoC = AutoC.Checked;
            Configuration.Default.AutoA = AutoA.Checked;
            Configuration.Default.AutoS = AutoS.Checked;
            Configuration.Default.AutovK = AutovK.Text;
            Configuration.Default.Save();
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            AppHotKey.UnRegKey(Handle, HKID);
            Environment.Exit(0);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);


            switch (m.Msg)
            {

                case WM_HOTKEY:
                    if (MasterSwitch)
                    {
                        switch (m.WParam.ToInt32())
                        {

                            case HKID:
                                if (APEngage)
                                {
                                    APEngage = false;
                                    Speaker.SpeakAsync("Deactivated");
                                }
                                else
                                {
                                    APEngage = true;
                                    Speaker.SpeakAsync("Activated");
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case WM_DESTROY:
                    AppHotKey.UnRegKey(Handle, HKID);
                    break;
                default:
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CD = ScanCoolDown(BLptr);
            switch (CD)
            {
                case -1:
                    //Cooling
                    break;
                case -2:
                    //Undetected
                    break;
                case 0:
                    //CD = 0
                    if (APEngage) BLAP.Trigger();
                    break;
                default:
                    break;
            }
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            if (MasterSwitch)
            {
                APEngage = false;

                AppHotKey.UnRegKey(Handle, HKID);
                MasterSwitch = false;
                StartBtn.Text = "Arm";
                StartBtn.ForeColor = Color.Green;
                Speaker.SpeakAsync("Hotkey Unregistered");
                BLAP.Uninstall();
            }
            else
            {
                SaveCFG();
                uint Modifier = AppHotKey.ModifyKey(Configuration.Default.AutoC, Configuration.Default.AutoA, Configuration.Default.AutoS);
                uint vK = Simulator.Transcoding(Configuration.Default.AutovK);
                AppHotKey.RegKey(Handle, HKID, Modifier, vK);

                //CN Window
                IntPtr gameptr = FindWindow(null, "最终幻想XIV");
                
                BLAP.Install(gameptr, BLParam.Text, BLvK.Text);
                MasterSwitch = true;

                StartBtn.Text = "Disarm";
                StartBtn.ForeColor = Color.Red;
                Speaker.SpeakAsync("Hotkey Registered");
            }

        }

        private void LoadBlackOut()
        {
            int Height = Properties.Resources.BloodLetter.Height;
            int Width = Properties.Resources.BloodLetter.Width;
            Bitmap OriginBMP = (Bitmap)Properties.Resources.BloodLetter;
            Bitmap BlackOutBMP = new Bitmap(Width, Height);
            
            Color pixel;
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    pixel = OriginBMP.GetPixel(x, y);
                    int r, g, b, Result = 0;
                    r = pixel.R;
                    g = pixel.G;
                    b = pixel.B;
                    //实例程序以加权平均值法产生黑白图像  

                    Result = ((r + g + b) / 3);

                    BlackOutBMP.SetPixel(x, y, Color.FromArgb(Result, Result, Result));
                }
            this.pictureBox1.Image = BlackOutBMP;
        }
    }
}
