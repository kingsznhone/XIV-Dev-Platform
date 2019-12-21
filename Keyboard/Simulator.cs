using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardApi
{
    public static class Simulator
    {
        [DllImport("user32.dll")] private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")] private static extern int SendMessage(IntPtr hWnd, uint Msg, uint wParam, uint lParam);


        private const int SW_SHOWNORMAL = 1;
        private const int SW_RESTORE = 9;
        private const int SW_SHOWNOACTIVATE = 4;

        private const int WM_KEYDOWN = 0X100;
        private const int WM_KEYUP = 0X101;
        private const int WM_CHAR = 0X102;
        private const int WM_SYSCHAR = 0X106;
        private const int WM_SYSKEYUP = 0X105;
        private const int WM_SYSKEYDOWN = 0X104;


        public static void Press(IntPtr GamePtr, uint Param, uint Key, int duration)
        {
            if (Param != 0)
            {
                SendMessage(GamePtr, WM_SYSKEYDOWN, Param, 0);
                SendMessage(GamePtr, WM_SYSKEYDOWN, Key, 0);
                System.Threading.Thread.Sleep(duration);
                SendMessage(GamePtr, WM_SYSKEYUP, Param, 0);
                SendMessage(GamePtr, WM_SYSKEYUP, Key, 0);
            }
            else
            {
                SendMessage(GamePtr, WM_KEYDOWN, Key, 0);
                System.Threading.Thread.Sleep(duration);
                SendMessage(GamePtr, WM_KEYUP, Key, 0);
            }
        }

        public static uint Transcoding(string keyname)
        {
            try
            {
                VK KeyList = new VK();
                uint code = 0;
                code = Convert.ToUInt32(KeyList.GetType().GetField("VK_" + keyname).GetValue(KeyList));
                return code;
            }
            catch (NullReferenceException e)
            {
                return 0;
            }
        }
    }
}
