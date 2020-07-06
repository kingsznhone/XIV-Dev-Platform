using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace KeyboardApi
{
	public class KeyboardHook
	{
		private delegate int HookHandle(int nCode, int wParam, IntPtr lParam);

		public delegate void ProcessKeyHandle(int nCode, int wParam, HookStruct param, out bool handle);

		[StructLayout(LayoutKind.Sequential)]
		public class HookStruct
		{
			public int vkCode;

			public int scanCode;

			public int flags;

			public int time;

			public int dwExtraInfo;
		}

		private const int WH_KEYBOARD_LL = 13;

		private const int WM_KEYDOWN = 256;

		private const int WM_KEYUP = 257;

		private const int WM_SYSKEYDOWN = 260;

		private const int WM_SYSKEYUP = 261;

		private static HookHandle _keyBoardHookProcedure;

		private static ProcessKeyHandle _clientMethod = null;

		private static int _hHookValue = 0;

		private IntPtr _hookWindowPtr = IntPtr.Zero;

		[DllImport("user32.dll")]
		private static extern int SetWindowsHookEx(int idHook, HookHandle lpfn, IntPtr hInstance, int threadId);

		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
		public static extern bool UnhookWindowsHookEx(int idHook);

		[DllImport("user32.dll")]
		public static extern int CallNextHookEx(int idHook, int nCode, int wParam, IntPtr lParam);

		[DllImport("kernel32.dll")]
		public static extern int GetCurrentThreadId();

		[DllImport("kernel32.dll")]
		public static extern IntPtr GetModuleHandle(string name);

		public void InstallHook(ProcessKeyHandle clientMethod)
		{
			_clientMethod = clientMethod;
			if (_hHookValue == 0)
			{
				_keyBoardHookProcedure = GetHookProc;
				_hookWindowPtr = GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName);
				_hHookValue = SetWindowsHookEx(13, _keyBoardHookProcedure, _hookWindowPtr, 0);
				if (_hHookValue == 0)
				{
					UninstallHook();
				}
			}
		}

		public void UninstallHook()
		{
			if (_hHookValue != 0 && UnhookWindowsHookEx(_hHookValue))
			{
				_hHookValue = 0;
			}
		}

		private static int GetHookProc(int nCode, int wParam, IntPtr lParam)
		{
			if (nCode >= 0)
			{
				HookStruct param = (HookStruct)Marshal.PtrToStructure(lParam, typeof(HookStruct));
				bool handle = false;
				_clientMethod(nCode, wParam, param, out handle);
				if (false)
				{
					return 1;
				}
			}
			return CallNextHookEx(_hHookValue, nCode, wParam, lParam);
		}
	}
}
