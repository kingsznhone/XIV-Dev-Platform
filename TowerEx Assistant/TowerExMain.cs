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
using System.Xml;
using KeyboardApi;
using ProcessMemoryApi;
using System.Text.Json;

namespace TowerEx_Assistant
{
    public partial class TowerExMain : Form
    {
        [DllImport("user32.dll")] private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        PicoHttpServer server;
        private KeyboardHook _keyboardHook = null;
        bool HotkeyPress = false;
        bool CtrlFlag = false;
        bool ShiftFlag = false;
        bool HotkeyFlag = false;
        uint Hotkeycode = 0;

        public IntPtr Gameptr;

        private const int WM_HOTKEY = 0x312;
        private const int WM_CREATE = 0x1;
        private const int WM_DESTROY = 0x2;
        private const int WM_SYSKEYDOWN = 0x0104;
        private const int WM_SYSKEYUP = 0x0105;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private const int HKID = 0x3721;

        const long Base_Offset = 0x1C5E510;
        private long Actual_Base_Offset;

        private long pModule;
        const long Offset_X = 0xA0;
        const long Offset_Z = 0xA4;
        const long Offset_Y = 0xA8;
        const long Offset_RAD = 0xB0;

        private long ZoneIDPtr;



        private long pFlyModule;
        const long Fly_X = 0x10;
        const long Fly_Z = 0x14;
        const long Fly_Y = 0x18;

        Coordinate CurrentCoord = new Coordinate();
        Coordinate DeltaCoord = new Coordinate();
        Coordinate DestinationCoord = new Coordinate();
        List<Coordinate> HistoryRecord = new List<Coordinate>();

        ProcessMemoryReader mReader = new ProcessMemoryReader();

        private XmlDocument CoordXML = new XmlDocument();
        private XmlLoader loader;
        private const string CoordDataPath = "CoordData.xml";

        public TowerExMain()
        {
            InitializeComponent();
            if (mReader.FindProcess("ffxiv_dx11"))
            {
                Process p = Process.GetProcessesByName("ffxiv_dx11").ToList().FirstOrDefault();
                mReader.process = p;
                mReader.OpenProcess();

                Actual_Base_Offset = 0;
                FindModulePtr(mReader);

                LoadHookHotkey();
                _keyboardHook = new KeyboardHook();

                loader = new XmlLoader();
                Loadxml();


                timer1.Enabled = true;
                int port = 7410;
                server = new PicoHttpServer(port);
                Thread Rx = new Thread(NetworkEvent);
                Rx.Start();
                NetworkStatus.Text = string.Format("正在监听本地端口{0}", port);
            }
            else
            {
                MessageBox.Show("游戏未运行", "严重错误：上溢", MessageBoxButtons.OK);
                Environment.Exit(0);
            }

        }

        public void FindModulePtr(ProcessMemoryReader mReader)
        {
            string Movepattern = "f30f105b40488d5424304C8b4318488d0d";
            string Mappattern2 = "f30f1043044c8d836cffffff0fb705";
            SignatureScanner signatureScanner = new SignatureScanner(mReader);
            Actual_Base_Offset = (long)signatureScanner.ScanMovePtr(Movepattern)[0];
            ZoneIDPtr = (long)signatureScanner.ScanPtrBySig(Mappattern2)[0];
            if (Base_Offset != Actual_Base_Offset)
            {
                DialogResult dialogResult = MessageBox.Show("基址不一致，是否使用增强扫描？", "沙沙沙沙……时空狭缝不需要能量吗？", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    pModule = BitConverter.ToInt64(mReader.ReadByteArray((IntPtr)((long)mReader.process.Modules[0].BaseAddress + Actual_Base_Offset), 8u), 0);
                }
                else
                {
                    pModule = BitConverter.ToInt64(mReader.ReadByteArray((IntPtr)((long)mReader.process.Modules[0].BaseAddress + Base_Offset), 8u), 0);
                }
            }
            else
            {
                pModule = BitConverter.ToInt64(mReader.ReadByteArray((IntPtr)((long)mReader.process.Modules[0].BaseAddress + Actual_Base_Offset), 8u), 0);
            }

            string FlyingSig = "40534883EC20488BD9488B89********4885C9741BF605********04751233D2E8********84C0488D05";
            long F_Offset = (long)mReader.ScanPtrBySig(FlyingSig).FirstOrDefault();
            pFlyModule = (long)mReader.process.Modules[0].BaseAddress + F_Offset;
        }

        public void ReadCoord()
        {
            byte[] value = mReader.ReadByteArray((IntPtr)(pModule + Offset_X), 4);
            CurrentCoord.X = BitConverter.ToSingle(value, 0);
            value = mReader.ReadByteArray((IntPtr)(pModule + Offset_Y), 4);
            CurrentCoord.Y = BitConverter.ToSingle(value, 0);
            value = mReader.ReadByteArray((IntPtr)(pModule + Offset_Z), 4);
            CurrentCoord.Z = BitConverter.ToSingle(value, 0);
            value = mReader.ReadByteArray((IntPtr)(pModule + Offset_RAD), 4);
            CurrentCoord.Theta = BitConverter.ToSingle(value, 0);
        }

        public void ReadMapID()
        {
            MapIDLabel.Text = "区域ID:" + mReader.ReadInt32((IntPtr)((long)mReader.process.Modules[0].BaseAddress + ZoneIDPtr));
        }

        public void CalcDelta()
        {
            if (RelativeModeCheck.Checked == true)
            {
                DeltaCoord.X = Convert.ToSingle(MultipleX.Value);
                DeltaCoord.Y = Convert.ToSingle(MultipleY.Value);
                DeltaCoord.Z = Convert.ToSingle(MultipleZ.Value);
            }
            else if (PolarModeCheck.Checked == true)
            {
                DeltaCoord.X = Convert.ToSingle(Math.Sin(Convert.ToDouble(CurrentCoord.Theta)) * (float)MultipleRho.Value);
                DeltaCoord.Y = Convert.ToSingle(Math.Cos(Convert.ToDouble(CurrentCoord.Theta)) * (float)MultipleRho.Value);
                DeltaCoord.Z = MultipleZ.Value;
            }
            else
            {
                try
                {
                    DeltaCoord.X = Convert.ToSingle(DirectX.Text) - Convert.ToSingle(CurrentCoord.X);
                    DeltaCoord.Y = Convert.ToSingle(DirectY.Text) - Convert.ToSingle(CurrentCoord.Y);
                    DeltaCoord.Z = Convert.ToSingle(DirectZ.Text) - Convert.ToSingle(CurrentCoord.Z);
                }
                catch { }
            }
        }

        public void CalcDest()
        {
            DestinationCoord = CurrentCoord + DeltaCoord;
        }

        #region UI
        private void timer1_Tick(object sender, EventArgs e)
        {

            if (HotkeyPress)
            {
                Teleport(CurrentCoord);
                return;
            }

            ReadCoord();
            CalcDelta();
            CalcDest();
            ReadMapID();
            CheckMaximumDist();

            XLabel.Text = "X : " + Convert.ToString(Math.Round(CurrentCoord.X, 2));
            YLabel.Text = "Y : " + Convert.ToString(Math.Round(CurrentCoord.Y, 2));
            ZLabel.Text = "Z : " + Convert.ToString(Math.Round(CurrentCoord.Z, 2));
            RADLabel.Text = "RAD : " + Convert.ToString(Math.Round(CurrentCoord.Theta, 2));
            DeltaX.Text = "△X : " + Convert.ToString(Math.Round(DeltaCoord.X, 2));
            DeltaY.Text = "△Y : " + Convert.ToString(Math.Round(DeltaCoord.Y, 2));
            DeltaZ.Text = "△Z : " + Convert.ToString(Math.Round(DeltaCoord.Z, 2));
            DestX.Text = "DestX : " + Convert.ToString(Math.Round(DestinationCoord.X, 2));
            DestY.Text = "DestY : " + Convert.ToString(Math.Round(DestinationCoord.Y, 2));
            DestZ.Text = "DestZ : " + Convert.ToString(Math.Round(DestinationCoord.Z, 2));
        }

        public void CheckMaximumDist()
        {
            if (Math.Pow(MultipleX.Value, 2) + Math.Pow(MultipleY.Value, 2) >= 225 || Math.Abs(MultipleRho.Value) >= 15)
            {
                MaxDistAlert.Visible = true;
            }
            else
            {
                MaxDistAlert.Visible = false;
            }
        }

        public void Teleport(Coordinate coord)
        {
            byte[] buffer = new byte[12];
            Buffer.BlockCopy(BitConverter.GetBytes(coord.X), 0, buffer, 0, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(coord.Z), 0, buffer, 4, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(coord.Y), 0, buffer, 8, 4);
            mReader.WriteByteArray((IntPtr)(pModule + Offset_X), buffer);
            mReader.WriteByteArray((IntPtr)(pFlyModule + Fly_X), buffer);
            //string jsonString = JsonSerializer.Serialize(coord);
            //debugBox.AppendText(jsonString + Environment.NewLine);
        }

        public void SaveRollback()
        {
            Coordinate Buffer = new Coordinate();
            Buffer.X = CurrentCoord.X;
            Buffer.Y = CurrentCoord.Y;
            Buffer.Z = CurrentCoord.Z;
            HistoryRecord.Add(Buffer);
        }

        private void TP_Click(object sender, EventArgs e)
        {
            SaveRollback();
            RollBackBtn.Enabled = true;

            if (RelativeModeCheck.Checked || PolarModeCheck.Checked)
            {
                Teleport(DestinationCoord);
                return;
            }
            try
            {
                Coordinate c = new Coordinate();
                c.X = Convert.ToSingle(DirectX.Text);
                c.Y = Convert.ToSingle(DirectY.Text);
                c.Z = Convert.ToSingle(DirectZ.Text);
                Teleport(c);
            }
            catch
            {
                MessageBox.Show("请输入数字", "坐标格式错误", MessageBoxButtons.OK);
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
            Pvalue.Text = Convert.ToString(MultipleRho.Value);
        }

        private void RollBackBtn_Click(object sender, EventArgs e)
        {
            if (HistoryRecord.Count > 0)
            {
                Teleport(HistoryRecord[HistoryRecord.Count - 1]);
                HistoryRecord.RemoveAt(HistoryRecord.Count - 1);
            }
            else
            {
                RollBackBtn.Enabled = false;
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
            MultipleRho.Value = 0;
            Pvalue.Text = "0";
            RelativeModeCheck.Checked = true;
        }

        private void RelativeModeCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (RelativeModeCheck.Checked == true)
            {
                MultipleX.Enabled = true;
                MultipleY.Enabled = true;
                MultipleZ.Enabled = true;
                MultipleRho.Enabled = false;
            }
        }

        private void PolarModeCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (PolarModeCheck.Checked == true)
            {
                MultipleX.Enabled = false;
                MultipleY.Enabled = false;
                MultipleZ.Enabled = true;
                MultipleRho.Enabled = true;
            }
        }

        private void AbsoluteModeCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (AbsoluteModeCheck.Checked == true)
            {
                MultipleX.Enabled = false;
                MultipleY.Enabled = false;
                MultipleZ.Enabled = false;
                MultipleRho.Enabled = false;
            }
        }
        #endregion

        #region Saved Coord Ctrl
        private void Loadxml()
        {
            CoordXML = loader.Load(CoordDataPath);
            treeView1.Nodes.Clear();
            RecursionTreeControl(CoordXML.DocumentElement, treeView1.Nodes);
        }

        private void RecursionTreeControl(XmlNode xmlNode, TreeNodeCollection treenodes)
        {

            foreach (XmlNode childNode in xmlNode.ChildNodes)
            {
                TreeNode treeNode = new TreeNode();
                treeNode.Name = childNode.Name;
                treeNode.Text = childNode.Name;
                treenodes.Add(treeNode);
                RecursionTreeControl(childNode, treeNode.Nodes);
            }
            treeView1.ExpandAll();
        }

        private void NewAreaBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string strText = string.Empty;
                InputDialog.Show(out strText);
                if (Char.IsDigit(strText, 0)) throw new Exception("开头不能是数字");
                if (strText == "") throw new Exception("名称不能为空");
                if (strText.StartsWith("-")) throw new Exception("非法字符");

                foreach (XmlNode childNode in CoordXML.SelectSingleNode("Root").ChildNodes)
                {
                    if (childNode.Name == strText) throw new Exception("区域已存在");
                }

                XmlElement xmlElement = CoordXML.CreateElement(strText);
                XmlAttribute xmlAttribute = CoordXML.CreateAttribute("Class");
                xmlElement.SetAttribute("Class", "Area");
                CoordXML.SelectSingleNode("Root").AppendChild(xmlElement);
                treeView1.Nodes.Clear();
                RecursionTreeControl(CoordXML.DocumentElement, treeView1.Nodes);
                loader.OutputXML(CoordXML, CoordDataPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "严重错误：上溢", MessageBoxButtons.OK);
            }
        }

        private void AddNewCoordBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string NewCoordName;
                InputDialog.Show(out NewCoordName);
                if (Char.IsDigit(NewCoordName, 0)) throw new Exception("开头不能是数字");
                if (NewCoordName == "") throw new Exception("名称不能为空");
                if (NewCoordName.StartsWith("-")) throw new Exception("非法字符");

                string XMLPath = "Root/" + treeView1.SelectedNode.FullPath.Replace("\\", "/");
                string TreeviewAreaPath = "";

                if (CoordXML.SelectSingleNode(XMLPath).Attributes["Class"].Value == "Coord")
                {
                    TreeviewAreaPath = XMLPath.Replace("/" + treeView1.SelectedNode.Name, "");
                }

                if (CoordXML.SelectSingleNode(XMLPath).Attributes["Class"].Value == "Area")
                {
                    TreeviewAreaPath = XMLPath;
                }

                XmlElement newChild = MakeNewElement(NewCoordName);
                foreach (XmlNode childNode in CoordXML.SelectSingleNode(TreeviewAreaPath).ChildNodes)
                {
                    if (childNode.Name == NewCoordName)
                    {
                        DialogResult dialogResult = MessageBox.Show("是否覆盖？", "坐标名称冲突", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.OK)
                        {
                            CoordXML.SelectSingleNode(TreeviewAreaPath).RemoveChild(CoordXML.SelectSingleNode(TreeviewAreaPath + "/" + NewCoordName));
                            CoordXML.SelectSingleNode(TreeviewAreaPath).AppendChild(newChild);
                            treeView1.Nodes.Clear();
                            RecursionTreeControl(CoordXML.DocumentElement, treeView1.Nodes);
                            loader.OutputXML(CoordXML, CoordDataPath);
                        }
                        return;
                    }
                }
                CoordXML.SelectSingleNode(TreeviewAreaPath).AppendChild(newChild);

                treeView1.Nodes.Clear();
                RecursionTreeControl(CoordXML.DocumentElement, treeView1.Nodes);
                loader.OutputXML(CoordXML, CoordDataPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "严重错误：上溢", MessageBoxButtons.OK);
            }
        }

        private XmlElement MakeNewElement(string name)
        {
            XmlElement xmlElement = CoordXML.CreateElement(name);
            XmlAttribute xmlAttribute = CoordXML.CreateAttribute("Class");
            Coordinate coordinate = new Coordinate();
            coordinate.X = Convert.ToSingle(DirectX.Text);
            coordinate.Y = Convert.ToSingle(DirectY.Text);
            coordinate.Z = Convert.ToSingle(DirectZ.Text);
            xmlElement.SetAttribute("Class", "Coord");
            xmlElement.SetAttribute("X", Convert.ToString(coordinate.X));
            xmlElement.SetAttribute("Y", Convert.ToString(coordinate.Y));
            xmlElement.SetAttribute("Z", Convert.ToString(coordinate.Z));
            return xmlElement;
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string text = "Root/" + treeView1.SelectedNode.FullPath.Replace("\\", "/");
                string xpath = text.Replace("/" + treeView1.SelectedNode.Name, "");
                CoordXML.SelectSingleNode(xpath).RemoveChild(CoordXML.SelectSingleNode(text));
                treeView1.Nodes.Clear();
                RecursionTreeControl(CoordXML.DocumentElement, treeView1.Nodes);
                loader.OutputXML(CoordXML, CoordDataPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "严重错误：上溢", MessageBoxButtons.OK);
            }
            loader.OutputXML(CoordXML, CoordDataPath);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string xpath = "Root/" + treeView1.SelectedNode.FullPath.Replace("\\", "/");
            if (CoordXML.SelectSingleNode(xpath).Attributes["Class"].Value == "Coord")
            {
                DirectX.Text = CoordXML.SelectSingleNode(xpath).Attributes["X"].Value;
                DirectY.Text = CoordXML.SelectSingleNode(xpath).Attributes["Y"].Value;
                DirectZ.Text = CoordXML.SelectSingleNode(xpath).Attributes["Z"].Value;
            }
        }

        private void DirectCoord_Enter(object sender, EventArgs e)
        {
            AbsoluteModeCheck.Checked = true;
        }

        private void CopyCoordBtn_Click(object sender, EventArgs e)
        {
            DirectX.Text = Convert.ToString(CurrentCoord.X);
            DirectY.Text = Convert.ToString(CurrentCoord.Y);
            DirectZ.Text = Convert.ToString(CurrentCoord.Z);
        }
        #endregion

        #region Coord Lock
        private void CoordLock_CheckedChanged(object sender, EventArgs e)
        {
            if (CoordLock.Checked == true)
            {
                SaveHookHotkey();
                _keyboardHook.InstallHook(HookedKeyPress);
                Hotkeycode = Simulator.Transcoding(HKcombo.Text);
                checkCtrl.Enabled = false;
                checkShift.Enabled = false;
                HKcombo.Enabled = false;
            }
            else
            {
                _keyboardHook.UninstallHook();
                checkCtrl.Enabled = true;
                checkShift.Enabled = true;
                HKcombo.Enabled = true;
            }
        }

        private void HookedKeyPress(int nCode, int wParam, KeyboardHook.HookStruct hookStruct, out bool handle)
        {
            handle = false; //预设不拦截
            if (hookStruct.vkCode == (int)Keys.LControlKey && wParam == WM_KEYDOWN) CtrlFlag = true;
            if (hookStruct.vkCode == (int)Keys.LControlKey && wParam == WM_KEYUP) CtrlFlag = false;
            if (hookStruct.vkCode == (int)Keys.LShiftKey && wParam == WM_KEYDOWN) ShiftFlag = true;
            if (hookStruct.vkCode == (int)Keys.LShiftKey && wParam == WM_KEYUP) ShiftFlag = false;
            if (hookStruct.vkCode == Hotkeycode && wParam == WM_KEYDOWN) HotkeyFlag = true;
            if (hookStruct.vkCode == Hotkeycode && wParam == WM_KEYUP) HotkeyFlag = false;

            if (checkCtrl.Checked == false && checkShift.Checked == false && HotkeyFlag)
            {
                HotkeyPress = true;
                return;
            }

            if (checkCtrl.Checked == true && checkShift.Checked == false && CtrlFlag && HotkeyFlag)
            {
                HotkeyPress = true;
                return;
            }

            if (checkCtrl.Checked == false && checkShift.Checked == true && ShiftFlag && HotkeyFlag)
            {
                HotkeyPress = true;
                return;
            }

            if (checkCtrl.Checked == true && checkShift.Checked == true && CtrlFlag && ShiftFlag && HotkeyFlag)
            {
                HotkeyPress = true;
                return;
            }
            HotkeyPress = false;
            return;
        }

        private void LoadHookHotkey()
        {
            checkCtrl.Checked = Configuration.Default.ModCtrl;
            checkShift.Checked = Configuration.Default.ModShift;
            HKcombo.Text = Configuration.Default.ModKey;
        }

        private void SaveHookHotkey()
        {
            Configuration.Default.ModCtrl = checkCtrl.Checked;
            Configuration.Default.ModShift = checkShift.Checked;
            Configuration.Default.ModKey = HKcombo.Text;
        }
        #endregion

        private async void NetworkEvent()
        {

            while (true)
            {
                try
                {
                    NetworkCoordBuffer coordbuffer = await server.StartListen();
                    Action refresh = delegate ()
                    {
                        string jsonString = JsonSerializer.Serialize(coordbuffer);
                        //debugBox.AppendText(jsonString + Environment.NewLine);
                    };
                    Invoke(refresh);
                    Coordinate NetCoord = coordbuffer.GetCoordinate();
                    if (coordbuffer.Mode == "Absolute")
                    {
                        Teleport(NetCoord);
                    }
                    if (coordbuffer.Mode == "Relative")
                    {
                        Teleport(CurrentCoord + NetCoord);
                    }
                    if (coordbuffer.Mode == "Polar")
                    {
                        NetCoord.X = (float)Math.Sin(CurrentCoord.Theta + coordbuffer.Theta) * coordbuffer.Rho + CurrentCoord.X;
                        NetCoord.Y = (float)Math.Cos(CurrentCoord.Theta + coordbuffer.Theta) * coordbuffer.Rho + CurrentCoord.Y;
                        NetCoord.Z = CurrentCoord.Z + NetCoord.Z;
                        Teleport(NetCoord);
                    }
                }
                catch { }
            }
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }


    }
}
