using System;
using System.Runtime.InteropServices;

namespace KeyboardApi
{
	public static class FormHotkey
	{
		[Flags]
		public enum KeyModifiers
		{
			None = 0x0,
			Alt = 0x1,
			Ctrl = 0x2,
			Shift = 0x4,
			WindowsKey = 0x8
		}

		[DllImport("kernel32.dll")]
		public static extern uint GetLastError();

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

		public static int RegKey(IntPtr hwnd, int hotKey_id, uint keyModifiers, uint key)
		{
			try
			{
				if (!RegisterHotKey(hwnd, hotKey_id, keyModifiers, key))
				{
					if (Marshal.GetLastWin32Error() == 1409)
					{
						//热键被占用
						return -1;
					}
					//未知错误
					else return 0;
				}
				//成功
				return 1;
			}
			catch (Exception)
			{
				//未知错误
				return 0;
			}
		}

		public static int UnRegKey(IntPtr hwnd, int hotKey_id)
		{
			try
			{
				UnregisterHotKey(hwnd, hotKey_id);
				return 1;
			}
			catch (Exception)
			{
				return 0;
			}
		}

		public static uint MakeModifier(bool ctrl, bool alt, bool shift)
		{
			uint n = 0u;
			if (alt)
			{
				n = 1;
			}
			if (ctrl)
			{
				n |= 2;
			}
			if (shift)
			{
				n |= 4;
			}
			return n;
		}
	}
}
