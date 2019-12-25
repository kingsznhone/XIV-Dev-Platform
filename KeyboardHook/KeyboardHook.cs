using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyboardHook
{
    class KeyboardHook
    {
        //钩子类型：键盘
        private const int WH_KEYBOARD_LL = 13; //全局钩子键盘为13，线程钩子键盘为2
        private const int WM_KEYDOWN = 0x0100; //键按下
        private const int WM_KEYUP = 0x0101; //键弹起
        //全局系统按键
        private const int WM_SYSKEYDOWN = 0x104;
        private const int WM_SYSKEYUP = 0x105;
        //键盘处理委托事件,捕获键盘输入，调用委托方法
        private delegate int HookHandle(int nCode, int wParam, IntPtr lParam);
        private static HookHandle _keyBoardHookProcedure;

        //客户端键盘处理委托事件
        public delegate void ProcessKeyHandle(int nCode,int wParam,HookStruct param, out bool handle);
        private static ProcessKeyHandle _clientMethod = null;

        //接收SetWindowsHookEx返回值   判断是否安装钩子
        private static int _hHookValue = 0;

        //Hook结构 存储按键信息的结构体
        [StructLayout(LayoutKind.Sequential)]
        public class HookStruct
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

        //安装钩子
        //idHook为13代表键盘钩子为14代表鼠标钩子，lpfn为函数指针，指向需要执行的函数，hIntance为指向进程快的指针，threadId默认为0就可以了
        [DllImport("user32.dll")]
        private static extern int SetWindowsHookEx(int idHook, HookHandle lpfn, IntPtr hInstance, int threadId);
        //取消钩子
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);
        //调用下一个钩子
        [DllImport("user32.dll")]
        public static extern int CallNextHookEx(int idHook, int nCode, int wParam, IntPtr lParam);
        //获取当前线程id
        [DllImport("kernel32.dll")]
        public static extern int GetCurrentThreadId();
        //通过线程Id,获取进程快
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(String name);

        private IntPtr _hookWindowPtr = IntPtr.Zero;

        public KeyboardHook() { }

        #region
        //加上客户端方法的委托的安装钩子
        public void InstallHook(ProcessKeyHandle clientMethod)
        {
            //客户端委托事件 
            _clientMethod = clientMethod;
            //安装键盘钩子
            if (_hHookValue == 0)
            {
                _keyBoardHookProcedure = new HookHandle(GetHookProc);
                _hookWindowPtr = GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName);
                _hHookValue = SetWindowsHookEx(
                    WH_KEYBOARD_LL,
                    _keyBoardHookProcedure,
                    _hookWindowPtr,
                    0
                    );
                if (_hHookValue == 0)
                {
                    //设置钩子失败
                    UninstallHook();
                    
                }
            }
        }
        #endregion


        //取消钩子事件
        public void UninstallHook()
        {
            if (_hHookValue != 0)
            {
                bool ret = UnhookWindowsHookEx(_hHookValue);
                if (ret)
                {
                    _hHookValue = 0;
                }
            }
        }

        private static int GetHookProc(int nCode, int wParam, IntPtr lParam)
        {
            //当nCode大于0，并且时按下事件时为1
               //if (nCode >= 0 && ((wParam == WM_SYSKEYDOWN) || (wParam == WM_KEYDOWN)))
                if (nCode >= 0)
                {
                //将按键信息转换为结构体
                HookStruct hookStruc = (HookStruct)Marshal.PtrToStructure(lParam, typeof(HookStruct));
                //是否拦截按键的标识符，默认不拦截
                bool handle = false;
                //客户端调用  
                _clientMethod(nCode,wParam,hookStruc, out handle); //如果传递了客户端委托，就调用
                handle = false; //如果这个handle为false，则代表不拦截此次按键，按了 windows等键会执行客户端键值显示，但是同时也会产生该键本有的功能。

                if (handle) { return 1; }
            }
            return CallNextHookEx(_hHookValue, nCode, wParam, lParam);
        }

    
    }
}
