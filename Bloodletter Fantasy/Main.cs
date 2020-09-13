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
using KeyboardApi;
using ProcessMemoryApi;
using BloodLetter_Fantasy.Properties;

namespace BloodLetter_Fantasy
{
    public partial class Main : Form
    {
        [DllImport("user32.dll")] private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        public IntPtr Gameptr;

        Bitmap BlackOutBL; 
        SpeechSynthesizer Speaker = new SpeechSynthesizer();
        private bool APEngage = false;
        long[] ModulePointers = new long[] { 0x1C626F0, 0x38, 0x18, 0x30, 0x20 };

        long pModuleHotbar;
        long CDptr = 0;

        int CD;

        private const int WM_HOTKEY = 0x312;
        private const int WM_CREATE = 0x1;
        private const int WM_DESTROY = 0x2;
        private const int HKID = 0x3721;

        const int BLID = 0x6E;

        ProcessMemoryReader mReader = new ProcessMemoryReader();

        Autopilot BLAP = new Autopilot();

        HotbarCollection hotbar;
        public Main()
        {
            if (!mReader.FindProcess("ffxiv_dx11"))
            {
                MessageBox.Show("游戏未运行", "严重错误：上溢", MessageBoxButtons.OK);
                Environment.Exit(0);
            }

            InitializeComponent();
            LoadCFG();

            Process p = Process.GetProcessesByName("ffxiv_dx11").ToList().FirstOrDefault();
            mReader.process = p;
            mReader.OpenProcess();

            Thread F = new Thread(new ThreadStart(FindPtr));
            F.Start();

            hotbar = new HotbarCollection();
            InitTTS();
            BlackOutBL = LoadBlackOut();

            BLAP.Uninstall();
            AppHotKey.UnRegKey(Handle, HKID);

            uint Modifier = AppHotKey.ModifyKey(Settings.Default.AutoC, Settings.Default.AutoA, Settings.Default.AutoS);
            uint vK = Simulator.Transcoding(Settings.Default.AutovK);
            AppHotKey.RegKey(Handle, HKID, Modifier, vK);
            IntPtr gameptr = FindWindow(null, "最终幻想XIV");
            BLAP.Install(gameptr, BLParam.Text, BLvK.Text);
        }

        public void InitTTS()
        {
            Speaker.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Teen);
            Speaker.Volume = 100;  // (0 - 100)
            Speaker.Rate = 0;
            Speaker.SpeakAsync("Initialized");
        }

        public void FindPtr()
        {
            while (true)
            {
                Thread.Sleep(1000);
                pModuleHotbar = mReader.ReadPtr_x64(ModulePointers);
                CDptr = ScanHotbar();
            }
            
        }

        public long ScanHotbar()
        {
            int skillID;
            long cdptr = 0;
            long unitPtr = pModuleHotbar + 0x3C;
            //byte[] buffer;
            for (int row = 0; row < 10; row++)
            {
                for (int unit = 0; unit < 12; unit++)
                {
                    //buffer = mReader.ReadByteArray((IntPtr)(unitPtr), 0x40);
                    //hotbar.AddUnit(buffer, row, unit);

                    if (mReader.ReadInt32((IntPtr)(unitPtr)) == 0x2c)
                    {
                        skillID = mReader.ReadInt32((IntPtr)(unitPtr + 0xC));
                        if (skillID == BLID) cdptr = unitPtr + 0x28;
                    }
                    unitPtr += 0x40;
                }
                unitPtr += 0x100;
            }
            return cdptr;
        }

        public int ScanCoolDown(long CDptr)
        {
            int CD;
            bool Availability;
            if (CDptr != 0)
            {
                Availability = BitConverter.ToBoolean(mReader.ReadByteArray((IntPtr)(CDptr - 0x14), 4), 0);
                if (Availability)
                {
                    CD = mReader.ReadInt32((IntPtr)(CDptr));
                    return CD == -1 ? 0 : CD;
                }
                else return -1;
            }
            else return -2;
        }

        public void LoadCFG()
        {
            BLParam.Text =Settings.Default.BLParam;
            BLvK.Text = Settings.Default.BLvK;
            AutoC.Checked = Settings.Default.AutoC;
            AutoA.Checked = Settings.Default.AutoA;
            AutoS.Checked = Settings.Default.AutoS;
            AutovK.Text = Settings.Default.AutovK;
        }

        public void SaveCFG()
        {
            Settings.Default.BLParam = BLParam.Text;
            Settings.Default.BLvK = BLvK.Text;
            Settings.Default.AutoC = AutoC.Checked;
            Settings.Default.AutoA = AutoA.Checked;
            Settings.Default.AutoS = AutoS.Checked;
            Settings.Default.AutovK = AutovK.Text;
            Settings.Default.Save();
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
                    switch (m.WParam.ToInt32())
                    {
                        case HKID:
                            APEngage = !APEngage;
                            break;
                        default:
                            break;
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
            CD = ScanCoolDown(CDptr);
            switch (CD)
            {
                case -1:
                    //Unable
                    this.pictureBox1.Image = BlackOutBL;
                    break;
                case -2:
                    this.pictureBox1.Image = BlackOutBL;

                    //Undetected
                    break;
                case 0:
                    //CD = 0
                    this.pictureBox1.Image = Properties.Resources.BloodLetter;
                    if (APEngage) BLAP.Trigger();
                    break;
                default:
                    break;
            }
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            SaveCFG();
            AppHotKey.UnRegKey(Handle, HKID);
            BLAP.Uninstall();

            uint Modifier = AppHotKey.ModifyKey(Settings.Default.AutoC, Settings.Default.AutoA, Settings.Default.AutoS);
            uint vK = Simulator.Transcoding(Settings.Default.AutovK);
            AppHotKey.RegKey(Handle, HKID, Modifier, vK);
            IntPtr gameptr = FindWindow(null, "最终幻想XIV");
            BLAP.Install(gameptr, BLParam.Text, BLvK.Text);
        }

        private Bitmap LoadBlackOut()
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
            return BlackOutBMP;
        }

        private void Modifier_CheckedChanged(object sender, EventArgs e)
        {
            //AppHotKey.UnRegKey(Handle, HKID);
            //BLAP.Uninstall();
            //uint Modifier = AppHotKey.ModifyKey(Settings.Default.AutoC, Settings.Default.AutoA, Settings.Default.AutoS);
            //uint vK = Simulator.Transcoding(Settings.Default.AutovK);
            //AppHotKey.RegKey(Handle, HKID, Modifier, vK);
            //IntPtr gameptr = FindWindow(null, "最终幻想XIV");
            //BLAP.Install(gameptr, BLParam.Text, BLvK.Text);
        }
    }
}
