using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using KeyboardApi;
using MemoryApi;

namespace TowerEx_Assistant
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")] private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        Preset preset = new Preset();

        public IntPtr Gameptr;

        private const int WM_HOTKEY = 0x312;
        private const int WM_CREATE = 0x1;
        private const int WM_DESTROY = 0x2;
        private const int WM_SYSKEYDOWN = 0x0104;
        private const int WM_SYSKEYUP = 0x0105;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private const int HKID = 0x3721;

        const long Base_Offset = 0x1B24AF0;
        const long Offset_X = 0xA0;
        const long Offset_Z = 0xA4;
        const long Offset_Y = 0xA8;
        const long Offset_RAD = 0xB0;
        const long MAPID = 0x1A5C590;
        long pModule;

        Coordinate CurrentCoord = new Coordinate();
        Coordinate DeltaS = new Coordinate();
        Coordinate Destination = new Coordinate();
        List<Coordinate> HistoryRecord = new List<Coordinate>();

        ProcessMemoryReader mReader = new ProcessMemoryReader();

        public Form1()
        {
            InitializeComponent();

            Thread Find = new Thread(new ThreadStart(Find_Game));
            Find.Start();
            LoadPreset();
            listBox.Enabled = true;
            
        }

        public void Find_Game()
        {
            while (true)
            {
                Thread.Sleep(3000);
                if (mReader.FindProcess("ffxiv_dx11"))
                {
                    Process p = Process.GetProcessesByName("ffxiv_dx11").ToList().FirstOrDefault();
                    mReader.process = p;
                    mReader.OpenProcess();

                    Action isrunning = delegate ()
                    {
                        GameOn.Text = "游戏已运行";
                        GameOn.ForeColor = Color.Green;
                        TPBtn.Enabled = true;
                        FindModulePtr();
                        timer1.Enabled = true;
                    };
                    Invoke(isrunning);
                }
                else
                {
                    Action notrunning = delegate ()
                    {
                        GameOn.Text = "游戏未运行";
                        GameOn.ForeColor = Color.Red;
                        TPBtn.Enabled = false;
                        GameOn.Enabled = false;
                        timer1.Enabled = false;
                    };
                    Invoke(notrunning);
                }
            }
        }

        public void FindModulePtr()
        {
            pModule = BitConverter.ToInt64(mReader.ReadByteArray((IntPtr)((long)mReader.process.Modules[0].BaseAddress + Base_Offset), 8), 0);
        }

        public void ReadCoord()
        {
            byte[] buffer = mReader.ReadByteArray((IntPtr)(pModule + Offset_X), 4);
            CurrentCoord.X = BitConverter.ToSingle(buffer, 0);
            buffer = mReader.ReadByteArray((IntPtr)(pModule + Offset_Y), 4);
            CurrentCoord.Y = BitConverter.ToSingle(buffer, 0);
            buffer = mReader.ReadByteArray((IntPtr)(pModule + Offset_Z), 4);
            CurrentCoord.Z = BitConverter.ToSingle(buffer, 0);
            buffer = mReader.ReadByteArray((IntPtr)(pModule + Offset_RAD), 4);
            CurrentCoord.RAD = BitConverter.ToSingle(buffer, 0);
        }

        public void ReadMapID()
        {
            MapIDLabel.Text = "地图ID:" + mReader.ReadInt32((IntPtr)((long)mReader.process.Modules[0].BaseAddress + MAPID));
        }

        public void CalcDelta()
        {
            if (CheckCCS.Checked == true)
            {
                DeltaS.X = Convert.ToSingle(MultipleX.Value);
                DeltaS.Y = Convert.ToSingle(MultipleY.Value);
                DeltaS.Z = Convert.ToSingle(MultipleZ.Value);
            }
            else
            {
                DeltaS.X = Convert.ToSingle(Math.Sin(Convert.ToDouble(CurrentCoord.RAD)) * (float)MultipleP.Value);
                DeltaS.Y = Convert.ToSingle(Math.Cos(Convert.ToDouble(CurrentCoord.RAD)) * (float)MultipleP.Value);
                DeltaS.Z = MultipleZ.Value;
            }

        }

        public void CalcDest()
        {
            Destination.X = CurrentCoord.X + DeltaS.X;
            Destination.Y = CurrentCoord.Y + DeltaS.Y;
            Destination.Z = CurrentCoord.Z + DeltaS.Z;
        }

        public void SaveRollback()
        {
            Coordinate Buffer = new Coordinate();
            Buffer.X = CurrentCoord.X;
            Buffer.Y = CurrentCoord.Y;
            Buffer.Z = CurrentCoord.Z;
            HistoryRecord.Add(Buffer);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (CoordLock.Checked)
            {
                Teleport(CurrentCoord);
            };
            ReadCoord();
            CalcDelta();
            CalcDest();
            ReadMapID();
            CheckMaximumDist();

            XLabel.Text = "X : " + Convert.ToString(Math.Round(CurrentCoord.X, 2));
            YLabel.Text = "Y : " + Convert.ToString(Math.Round(CurrentCoord.Y, 2));
            ZLabel.Text = "Z : " + Convert.ToString(Math.Round(CurrentCoord.Z, 2));
            RADLabel.Text = "RAD : " + Convert.ToString(Math.Round(CurrentCoord.RAD, 2));
            DeltaX.Text = "△X : " + Convert.ToString(Math.Round(DeltaS.X, 2));
            DeltaY.Text = "△Y : " + Convert.ToString(Math.Round(DeltaS.Y, 2));
            DeltaZ.Text = "△Z : " + Convert.ToString(Math.Round(DeltaS.Z, 2));
            DestX.Text = "DestX : " + Convert.ToString(Math.Round(Destination.X, 2));
            DestY.Text = "DestY : " + Convert.ToString(Math.Round(Destination.Y, 2));
            DestZ.Text = "DestZ : " + Convert.ToString(Math.Round(Destination.Z, 2));
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        public void Teleport(Coordinate C)
        {
            byte[] buffer = new byte[12];
            Buffer.BlockCopy(BitConverter.GetBytes(C.X), 0, buffer, 0, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(C.Z), 0, buffer, 4, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(C.Y), 0, buffer, 8, 4);
            mReader.WriteByteArray((IntPtr)(pModule + Offset_X), buffer);
        }

        private void TP_Click(object sender, EventArgs e)
        {
            SaveRollback();
            RollBackBtn.Enabled = true;

            if (CheckCCS.Checked == true || CheckPCS.Checked == true)
            {
                Teleport(Destination);
            }
            else
            {
                Coordinate DirectTo = new Coordinate();
                try
                {
                    DirectTo.X = Convert.ToSingle(DirectX.Text);
                    DirectTo.Y = Convert.ToSingle(DirectY.Text);
                    DirectTo.Z = Convert.ToSingle(DirectZ.Text);
                    Teleport(DirectTo);
                }
                catch
                {
                    MessageBox.Show("请输入数字", "坐标格式错误", MessageBoxButtons.OK);
                }
            }
        }

        public void CheckMaximumDist()
        {
            if (Math.Pow(MultipleX.Value, 2) + Math.Pow(MultipleY.Value, 2) >= 225 || Math.Abs(MultipleP.Value) >= 15)
            {
                MaxDistAlert.Visible = true;
            }
            else
            {
                MaxDistAlert.Visible = false;
            }
        }

        private void MultipleX_Scroll(object sender, EventArgs e)
        {
            Xvalue.Text = Convert.ToString(MultipleX.Value);
        }

        private void MultipleY_Scroll(object sender, EventArgs e)
        {
            Yvalue.Text = Convert.ToString(MultipleY.Value);
        }

        private void MultipleZ_Scroll(object sender, EventArgs e)
        {
            Zvalue.Text = Convert.ToString(MultipleZ.Value);
        }

        private void MultipleP_Scroll(object sender, EventArgs e)
        {
            Pvalue.Text = Convert.ToString(MultipleP.Value);
        }

        private void RollBackBtn_Click(object sender, EventArgs e)
        {
            if(HistoryRecord.Count >0)
            {
                Teleport(HistoryRecord[HistoryRecord.Count - 1]);
                HistoryRecord.RemoveAt(HistoryRecord.Count - 1);
            }
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            MultipleX.Value = 0;
            Xvalue.Text = "0";
            MultipleY.Value = 0;
            Yvalue.Text = "0";
            MultipleZ.Value = 0;
            Zvalue.Text = "0";
            MultipleP.Value = 0;
            Pvalue.Text = "0";
            CheckCCS.Checked = true;
        }

        private void CheckCCS_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckCCS.Checked == true)
            {
                MultipleX.Enabled = true;
                MultipleY.Enabled = true;
                MultipleZ.Enabled = true;
                MultipleP.Enabled = false;
            }
        }

        private void CheckPCS_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckPCS.Checked == true)
            {
                MultipleX.Enabled = false;
                MultipleY.Enabled = false;
                MultipleZ.Enabled = true;
                MultipleP.Enabled = true;
            }
        }

        private void CheckACS_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckACS.Checked == true)
            {
                MultipleX.Enabled = false;
                MultipleY.Enabled = false;
                MultipleZ.Enabled = false;
                MultipleP.Enabled = false;
            }
        }

        public void LoadPreset()
        {
            listBox.Items.Clear();
            foreach (string key in preset.List.Keys)
            {
                listBox.Items.Add(key);
            }
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DirectX.Text = Convert.ToString(preset.List[listBox.Text].X);
                DirectY.Text = Convert.ToString(preset.List[listBox.Text].Y);
                DirectZ.Text = Convert.ToString(preset.List[listBox.Text].Z);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "严重错误：上溢", MessageBoxButtons.OK);
            }
        }

        private void SaveCoord_Click(object sender, EventArgs e)
        {
            string Coordname = string.Empty;
            InputDialog.Show(out Coordname);
            try
            {
                Coordinate buffer = new Coordinate();
                buffer.X = Convert.ToSingle(DirectX.Text);
                buffer.Y = Convert.ToSingle(DirectY.Text);
                buffer.Z = Convert.ToSingle(DirectZ.Text);

                if (preset.List.ContainsKey(Coordname))
                {
                    DialogResult dr = MessageBox.Show("是否覆盖？", "坐标名称冲突", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dr == DialogResult.OK)
                    {
                        preset.List[Coordname] = buffer;
                        preset.Save();
                    }
                }
                else
                {
                    preset.List.Add(Coordname, buffer);
                    preset.Save();
                }
                LoadPreset();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "严重错误：上溢", MessageBoxButtons.OK);
            }
            
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            string selected = listBox.Text;
            preset.List.Remove(selected);
            preset.Save();
            LoadPreset();
        }

        private void CopyCoord_Click(object sender, EventArgs e)
        {
            DirectX.Text = Convert.ToString(CurrentCoord.X);
            DirectY.Text = Convert.ToString(CurrentCoord.Y);
            DirectZ.Text = Convert.ToString(CurrentCoord.Z);
        }

        private void CoordLock_CheckedChanged(object sender, EventArgs e)
        {
            if (CoordLock.Checked == true)
            {
                
            }
            else
            {
                AppHotKey.UnRegKey(Handle, HKID);
            }
  
        }




    }
}
