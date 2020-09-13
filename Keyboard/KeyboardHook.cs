using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardApi
{
    public class KeyboardHook
    {
        //�������ͣ�����
        private const int WH_KEYBOARD_LL = 13; //ȫ�ֹ��Ӽ���Ϊ13���̹߳��Ӽ���Ϊ2
        private const int WM_KEYDOWN = 0x0100; //������
        private const int WM_KEYUP = 0x0101; //������
        //ȫ��ϵͳ����
        private const int WM_SYSKEYDOWN = 0x104;
        private const int WM_SYSKEYUP = 0x105;
        //���̴���ί���¼�,����������룬����ί�з���
        private delegate int HookHandle(int nCode, int wParam, IntPtr lParam);
        private static HookHandle _keyBoardHookProcedure;

        //�ͻ��˼��̴���ί���¼�
        public delegate void ProcessKeyHandle(int nCode, int wParam, HookStructen.Param, out bool handle);
        private static ProcessKeyHandle _clientMethod = null;

        //����SetWindowsHookEx����ֵ   �ж��Ƿ�װ����
        private static int _hHookValue = 0;

        //Hook�ṹ �洢������Ϣ�Ľṹ��
        [StructLayout(LayoutKind.Sequential)]
        public class HookStruct
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

        //��װ����
        //idHookΪ13������̹���Ϊ14������깳�ӣ�lpfnΪ����ָ�룬ָ����Ҫִ�еĺ�����hIntanceΪָ����̿��ָ�룬threadIdĬ��Ϊ0�Ϳ�����
        [DllImport("user32.dll")]
        private static extern int SetWindowsHookEx(int idHook, HookHandle lpfn, IntPtr hInstance, int threadId);
        //ȡ������
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);
        //������һ������
        [DllImport("user32.dll")]
        public static extern int CallNextHookEx(int idHook, int nCode, int wParam, IntPtr lParam);
        //��ȡ��ǰ�߳�id
        [DllImport("kernel32.dll")]
        public static extern int GetCurrentThreadId();
        //ͨ���߳�Id,��ȡ���̿�
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(String name);

        private IntPtr _hookWindowPtr = IntPtr.Zero;

        public KeyboardHook() { }

        #region
        //���Ͽͻ��˷�����ί�еİ�װ����
        public void InstallHook(ProcessKeyHandle clientMethod)
        {
            //�ͻ���ί���¼� 
            _clientMethod = clientMethod;
            //��װ���̹���
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
                    //���ù���ʧ��
                    UninstallHook();

                }
            }
        }
        #endregion


        //ȡ�������¼�
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
            //��nCode����0������ʱ�����¼�ʱΪ1
            //if (nCode >= 0 && ((wParam == WM_SYSKEYDOWN) || (wParam == WM_KEYDOWN)))
            if (nCode >= 0)
            {
                //��������Ϣת��Ϊ�ṹ��
                HookStruct hookStruc = (HookStruct)Marshal.PtrToStructure(lParam, typeof(HookStruct));
                //�Ƿ����ذ����ı�ʶ����Ĭ�ϲ�����
                bool handle = false;
                //�ͻ��˵���  
                _clientMethod(nCode, wParam, hookStruc, out handle); //��������˿ͻ���ί�У��͵���
                handle = false; //������handleΪfalse����������ش˴ΰ��������� windows�ȼ���ִ�пͻ��˼�ֵ��ʾ������ͬʱҲ������ü����еĹ��ܡ�

                if (handle) { return 1; }
            }
            return CallNextHookEx(_hHookValue, nCode, wParam, lParam);
        }


    }
}
