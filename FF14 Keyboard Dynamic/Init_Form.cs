using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using KeyboardApi;
namespace XIV_Keyboard_Dynamic
{
    public partial class Init_Form : Form
    {
        [DllImport("user32.dll")] private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        public IntPtr Gameptr;
        string Pipeline;
        Dictionary<string, string> Keyset = new Dictionary<string, string>();
        List<string> process;
        int R;
        bool Collection; 

        public Init_Form()
        {
            InitializeComponent();
            debug.AppendText("全局供电开启...\r\n");
        }

        private void SelectProgress_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog LoadProgress = new OpenFileDialog();
                LoadProgress.Filter = "文本文档|*.txt";
                LoadProgress.Title = "选择脚本";
                LoadProgress.InitialDirectory = Environment.CurrentDirectory + "";
                if (LoadProgress.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Pipeline = LoadProgress.FileName;
                    ProgressDir.Text = Path.GetFileNameWithoutExtension(Pipeline);
                    process = ProgressReader.Preload(Pipeline);
                    
                    debug.AppendText(String.Format("<{0}> 加载完成...\r\n", ProgressDir.Text));
                    
                }
                StartBtn.Enabled = true;
            }
            catch
            {
                MessageBox.Show("请检查脚本", "配方加载失败");
                Environment.Exit(0);
            }

        }

        private void SelectSetting_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog LoadSetting = new OpenFileDialog();
                LoadSetting.Filter = "文本文档|*.txt";
                LoadSetting.Title = "选择配置";
                LoadSetting.InitialDirectory = Environment.CurrentDirectory + "";
                if (LoadSetting.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Keyset = ProgressReader.ReadKeyset(LoadSetting.FileName);
                    SettingDir.Text = Path.GetFileNameWithoutExtension(LoadSetting.FileName);
                    debug.AppendText("系统初始化已完成...\r\n");
                    debug.AppendText("等待加载流水线...\r\n");
                }
                StartBtn.Enabled = true;
            }
            catch
            {
                MessageBox.Show("请检查脚本", "配方加载失败");
                Environment.Exit(0);
            }

        }

        private void Init_Form_Load(object sender, EventArgs e)
        {
            try
            {
                Thread Find = new Thread(new ThreadStart(Find_Game));
                Find.Start();
            }
            catch
            {
                MessageBox.Show("请检查配置文件", "按键配置加载失败");
                Environment.Exit(0);
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

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

        
        private void Init_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Repeat_ValueChanged(object sender, EventArgs e)
        {
            R = (int)Repeat.Value;
        }

        private void debug_TextChanged(object sender, EventArgs e)
        {
            debug.SelectionStart = debug.Text.Length; //Set the current caret position at the end
            debug.ScrollToCaret(); //Now scroll it automatically
        }

        private void Update(string i,bool complete)
        {
            debug.AppendText(i);
            debug.AppendText(Environment.NewLine);
            if(complete)
            {
                StartBtn.Enabled = true;
            }
        }

        private void StartIndustrialization(object sender, EventArgs e)
        {
            if (Paddling.Checked == true)
            {
                Thread Paddle = new Thread(new ThreadStart(Robot));
                Paddle.Start();
                debug.AppendText("开始划水...\r\n");
                StartBtn.Enabled = false;
            }
            else if (Factory.Checked == true)
            {
                Thread Factory = new Thread(new ThreadStart(Machine));
                Factory.Start();
                debug.AppendText("工业化生产开始...\r\n");
                StartBtn.Enabled = false;
            }
            else
            {

            }
        }

        public void Robot()
        {
            Action<string, bool> status = new Action<string, bool>(Update);

            for (int countdown = 5; countdown >= 0; countdown--)
            {
                Invoke(status, "还有" + countdown.ToString() + "开始...", false);
                Thread.Sleep(1000);
            }

            for (int r = R; r >= 1; r--)
            {
                foreach (string s in process)
                {
                    string[] tmp = s.Split(',');
                    uint[] code = ProgressReader.Transcoding(Keyset[tmp[0]]);
                    for (int cramp = 5; cramp > 0; cramp--)
                    {
                        Simulator.Press(Gameptr, code[0], code[1],20);
                        Thread.Sleep(40);
                    }
                    Thread.Sleep(Convert.ToInt32(tmp[1]));
                }
            }

            Invoke(status, "工业化已完成...\r\n", true);
        }


        public void Machine()
        {
            Action<string, bool> status = new Action<string, bool>(Update);

            for (int countdown = 5; countdown >= 0; countdown--)
            {
                Invoke(status, "还有" + countdown.ToString() + "开始...", false);
                Thread.Sleep(1000);
            }

            for (int r = R; r >= 1; r--)
            {
                Invoke(status, "剩余<" + r.ToString() + ">次作业...", false);
                for (int k = 0; k <= 10; k++)
                {
                    Simulator.Press(Gameptr, 0, 0x60,20);
                    Thread.Sleep(80);
                }
                Thread.Sleep(3000);

                foreach (string s in process)
                {
                    string[] tmp = s.Split(',');
                    uint[] code = ProgressReader.Transcoding(Keyset[tmp[0]]);
                    Simulator.Press(Gameptr, code[0], code[1],20);
                    Thread.Sleep(Convert.ToInt32(tmp[1]));
                }
                if (Collection)
                {
                    Thread.Sleep(3000);
                    for (int k = 0; k <= 10; k++)
                    {
                        Simulator.Press(Gameptr, 0, 0x60,20);
                        Thread.Sleep(80);
                    }
                }

                Thread.Sleep(5000);
            }

            Invoke(status, "工业化已完成...\r\n", true);
        }

        private void Abort_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Valueble_CheckedChanged(object sender, EventArgs e)
        {
            Collection = Valueble.Checked;
        }


        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            StartBtn.Text = "开始划水";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            StartBtn.Text = "开始工业化";
        }
    }
}
