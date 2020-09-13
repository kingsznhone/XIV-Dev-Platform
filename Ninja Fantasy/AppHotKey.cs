using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ninja_Assistant
{
    public static  class AppHotKey
    {
        [DllImport("kernel32.dll")]
        public static extern uint GetLastError();

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(
            IntPtr hWnd,
            int id,
            uint fsModifiers,
            uint vk
            );

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(
            IntPtr hWnd,
            int id
            );
        [Flags()]

        public enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Ctrl = 2,
            Shift = 4,
            WindowsKey = 8
        }

        public static void RegKey(IntPtr hwnd, int hotKey_id, uint keyModifiers, uint key)
        {
            try
            {
                if (!RegisterHotKey(hwnd, hotKey_id, keyModifiers, key))
                {
                    if (Marshal.GetLastWin32Error() == 1409) { MessageBox.Show("热键被占用 ！"); }
                    else
                    {
                        MessageBox.Show("注册热键失败！");
                    }
                }
            }
            catch (Exception) { }
        }

        public static void UnRegKey(IntPtr hwnd, int hotKey_id)
        {
            UnregisterHotKey(hwnd, hotKey_id);
        }

        public static uint ModifyKey(bool ctrl, bool alt, bool shift)
        {
            uint Modifier = 0 ;
            if (ctrl)
            {
                Modifier = (uint)KeyModifiers.Ctrl;
            }
            if(alt)
            {
                Modifier = Modifier | (uint)KeyModifiers.Alt;
            }
            if(shift)
            {
                Modifier = Modifier | (uint)KeyModifiers.Shift;
            }
            return Modifier;
        }

    }
}
