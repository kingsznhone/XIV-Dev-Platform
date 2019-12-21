using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MemoryApi;
namespace ZoomHack
{
    

    public partial class MainForm : Form
    {
        IntPtr bytesOut;
        ProcessMemoryReader mReader = new ProcessMemoryReader();

        long BaseOffset = 0x1AA4520;

        long pCurrentZoom = 0x118;
        long pMinZoom = 0x11C;
        long pMaxZoom = 0x120;
        long pCurrentPOV = 0x124;
        long pMinPOV = 0x128;
        long pMaxPOV = 0x12C;

        long pModule;

        public MainForm()
        {
            InitializeComponent();
            Thread Find = new Thread(new ThreadStart(Find_Game));
            Find.Start();
        }

        public void Find_Game()
        {
            while (true)
            {
                Thread.Sleep(3000);
                if (mReader.FindProcess("ffxiv_dx11"))
                {
                    FindPtr();
                    Action isrunning = delegate ()
                    {
                        GameStatuts.Text = "游戏已运行";
                        GameStatuts.ForeColor = Color.Green;
                        
                    };
                    Invoke(isrunning);
                }
                else
                {
                    Action notrunning = delegate ()
                    {
                        GameStatuts.Text = "游戏未运行";
                        GameStatuts.ForeColor = Color.Red;
                    };
                    Invoke(notrunning);
                }
            }
        }

        public void FindPtr()
        {
            Process p = Process.GetProcessesByName("ffxiv_dx11").ToList().FirstOrDefault();
            mReader.process = p;
            mReader.OpenProcess();
            pModule = mReader.ReadInt64((IntPtr)((long)p.Modules[0].BaseAddress + BaseOffset));
        }

        private void ZoomUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (ZoomUpDown.Value >= 100) ZoomUpDown.BackColor = Color.Red;
            else ZoomUpDown.BackColor = Color.White;

            if (GameStatuts.ForeColor == Color.Green)
            {
                byte[] buffer = BitConverter.GetBytes(Convert.ToSingle(ZoomUpDown.Value));
                mReader.WriteByteArray((IntPtr)(pModule + pMaxZoom), buffer);
                mReader.WriteByteArray((IntPtr)(pModule + pCurrentZoom), buffer);
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void POVUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (POVUpDown.Value>=120) POVUpDown.BackColor = Color.Red;
            else POVUpDown.BackColor = Color.White;

            if (GameStatuts.ForeColor == Color.Green)
            {
                float POVf = Convert.ToSingle( Convert.ToDouble(POVUpDown.Value/180)*Math.PI);
                byte[] buffer = BitConverter.GetBytes(POVf);
                mReader.WriteByteArray((IntPtr)(pModule + pMinPOV), BitConverter.GetBytes(Convert.ToSingle(0.01)));
                mReader.WriteByteArray((IntPtr)(pModule + pMaxPOV), buffer);
                mReader.WriteByteArray((IntPtr)(pModule + pCurrentPOV), buffer);
            }
        }

        private void SetDefaultbtn_Click(object sender, EventArgs e)
        {
            mReader.WriteByteArray((IntPtr)(pModule + pCurrentZoom), DefaultValue.CurrentZoom);
            mReader.WriteByteArray((IntPtr)(pModule + pMinZoom), DefaultValue.MinZoom);
            mReader.WriteByteArray((IntPtr)(pModule + pMaxZoom), DefaultValue.MaxZoom);
            mReader.WriteByteArray((IntPtr)(pModule + pCurrentPOV), DefaultValue.CurrentPOV);
            mReader.WriteByteArray((IntPtr)(pModule + pMinPOV), DefaultValue.MinPOV);
            mReader.WriteByteArray((IntPtr)(pModule + pMaxPOV), DefaultValue.MaxPOV);
            POVUpDown.Value = 45;
            ZoomUpDown.Value = 20;
        }
    }

    public static class DefaultValue
    {
        public static byte[] CurrentZoom =  { 0x00, 0x00, 0xA0, 0x41 };
        public static byte[] MinZoom =      { 0x00, 0x00, 0xC0, 0x3F };
        public static byte[] MaxZoom =      { 0x00, 0x00, 0xA0, 0x41 };
        public static byte[] CurrentPOV =   { 0x14, 0xAE, 0x47, 0x3F };
        public static byte[] MinPOV =       { 0xD7, 0xA3, 0x30, 0x3F };
        public static byte[] MaxPOV =       { 0x14, 0xAE, 0x47, 0x3F };
    }
}
