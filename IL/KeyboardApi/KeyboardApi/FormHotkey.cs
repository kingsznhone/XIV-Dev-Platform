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
						return -1;
					}
					return 0;
				}
				return 1;
			}
			catch (Exception)
			{
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
			uint num = 0u;
			if (ctrl)
			{
				num = 2u;
			}
			if (alt)
			{
				num |= 1;
			}
			if (shift)
			{
				num |= 4;
			}
			return num;
		}
	}
}
