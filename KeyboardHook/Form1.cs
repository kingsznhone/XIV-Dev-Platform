using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyboardHook
{
    public partial class Form1 : Form
    {
        private KeyboardHook _keyboardHook = null;
        public Form1()
        {
            InitializeComponent();
            _keyboardHook = new KeyboardHook();
            _keyboardHook.InstallHook(Form1_KeyPress);
        }

        void kh_OnKeyDownEvent(object sender, KeyEventArgs e)

        {

            if (e.KeyData == (Keys.S | Keys.Control)) { this.Show(); }//Ctrl+S显示窗口

            if (e.KeyData == (Keys.H | Keys.Control)) { this.Hide(); }//Ctrl+H隐藏窗口

            if (e.KeyData == (Keys.C | Keys.Control)) { this.Close(); }//Ctrl+C 关闭窗口 

            if (e.KeyData == (Keys.A | Keys.Control | Keys.Alt)) { this.Text = "你发现了什么？"; }//Ctrl+Alt+A

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void Form1_KeyPress(int nCode,int wParam,KeyboardHook.HookStruct hookStruct, out bool handle)
        {
            handle = false; //预设不拦截
            ip01.Text = Convert.ToString(hookStruct.vkCode);
            ip02.Text = Convert.ToString(hookStruct.scanCode);
            ip03.Text = Convert.ToString(hookStruct.flags);
            ip04.Text = Convert.ToString(hookStruct.time);
            ip05.Text = Convert.ToString(hookStruct.dwExtraInfo);
            Wp.Text = Convert.ToString(wParam);
            NC.Text = Convert.ToString(nCode);
            Keys key = (Keys)hookStruct.vkCode;
            String keyname = key.ToString();
            richTextBox1.AppendText(Convert.ToString(hookStruct.vkCode)+Environment.NewLine);
            Keydisplay.Text = keyname;
            return;
        }

    }
}
