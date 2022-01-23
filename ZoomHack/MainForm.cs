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
using ProcessMemoryApi;
namespace Zoom_Assistant
{

    public partial class MainForm : Form
    {
        ProcessMemoryReader mReader;


        private long BaseOffset = 0L;
        private long pModule;

        private const long pCurrentZoom = 0x114;
        private const long pMinZoom = 0x118;
        private const long pMaxZoom = 0x11C;
        private const long pCurrentFOV = 0x120;
        private const long pMinFOV = 0x124;
        private const long pMaxFOV = 0x128;
        public MainForm()
        {
            InitializeComponent();
            if (Process.GetProcessesByName("ffxiv_dx11").FirstOrDefault()!=null) FindPtr();
            else
            {
                MessageBox.Show("游戏未运行", "严重错误：上溢", MessageBoxButtons.OK);
                Environment.Exit(0);
            }
        }

        public void FindPtr()
        {
            Process process = Process.GetProcessesByName("ffxiv_dx11").ToList().FirstOrDefault();
            mReader = new ProcessMemoryReader(process.Id);
            BaseOffset = (long)mReader.ScanPtrBySig("48833D********007411488B0D********4885C97405E8********488D0D")[0];
            pModule = mReader.ReadInt64((IntPtr)((long)process.Modules[0].BaseAddress + BaseOffset));
        }

        private void ZoomUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (ZoomUpDown.Value >= 100m) ZoomUpDown.BackColor = Color.Red;

            else ZoomUpDown.BackColor = Color.White;

            byte[] bytes = BitConverter.GetBytes(Convert.ToSingle(ZoomUpDown.Value));
            mReader.WriteByteArray((IntPtr)(pModule + pMinZoom), BitConverter.GetBytes(Convert.ToSingle(0.01)));
            mReader.WriteByteArray((IntPtr)(pModule + pMaxZoom), bytes);
            mReader.WriteByteArray((IntPtr)(pModule + pCurrentZoom), bytes);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void FOVUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (FOVUpDown.Value >= 120m) FOVUpDown.BackColor = Color.Red;
            else FOVUpDown.BackColor = Color.White;
            float value = Convert.ToSingle(Convert.ToDouble(FOVUpDown.Value / 180m) * Math.PI);
            byte[] bytes = BitConverter.GetBytes(value);
            mReader.WriteByteArray((IntPtr)(pModule + pMinFOV), BitConverter.GetBytes(Convert.ToSingle(0.01)));
            mReader.WriteByteArray((IntPtr)(pModule + pMaxFOV), bytes);
            mReader.WriteByteArray((IntPtr)(pModule + pCurrentFOV), bytes);
        }

        private void SetDefaultbtn_Click(object sender, EventArgs e)
        {
            mReader.WriteByteArray((IntPtr)(pModule + pCurrentZoom), DefaultValue.CurrentZoom);
            mReader.WriteByteArray((IntPtr)(pModule + pMinZoom), DefaultValue.MinZoom);
            mReader.WriteByteArray((IntPtr)(pModule + pMaxZoom), DefaultValue.MaxZoom);
            mReader.WriteByteArray((IntPtr)(pModule + pCurrentFOV), DefaultValue.CurrentPOV);
            mReader.WriteByteArray((IntPtr)(pModule + pMinFOV), DefaultValue.MinPOV);
            mReader.WriteByteArray((IntPtr)(pModule + pMaxFOV), DefaultValue.MaxPOV);
            FOVUpDown.Value = 45m;
            ZoomUpDown.Value = 20m;
        }
    }

    public static class DefaultValue
    {
        public static byte[] CurrentZoom = { 0x00, 0x00, 0xA0, 0x41 };
        public static byte[] MinZoom = { 0x00, 0x00, 0xC0, 0x3F };
        public static byte[] MaxZoom = { 0x00, 0x00, 0xA0, 0x41 };
        public static byte[] CurrentPOV = { 0x14, 0xAE, 0x47, 0x3F };
        public static byte[] MinPOV = { 0xD7, 0xA3, 0x30, 0x3F };
        public static byte[] MaxPOV = { 0x14, 0xAE, 0x47, 0x3F };
    }
}
