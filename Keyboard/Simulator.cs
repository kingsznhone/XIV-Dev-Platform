using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace KeyboardApi
{
	public static class Simulator
	{
		private const int SW_SHOWNORMAL = 1;

		private const int SW_RESTORE = 9;

		private const int SW_SHOWNOACTIVATE = 4;

		private const int WM_KEYDOWN = 256;

		private const int WM_KEYUP = 257;

		private const int WM_CHAR = 258;

		private const int WM_SYSCHAR = 262;

		private const int WM_SYSKEYUP = 261;

		private const int WM_SYSKEYDOWN = 260;

		private static bool busy = false;

		[DllImport("user32.dll")]
		private static extern int SendMessage(IntPtr hWnd, uint Msg, uint wParam, uint lParam);

		public static int Press(IntPtr GamePtr, uint Param, uint Key, int duration)
		{
			if (busy)
			{
				return 0;
			}
			busy = true;
			Thread thread = new Thread((ThreadStart)delegate
			{
				if (Param != 0)
				{
					SendMessage(GamePtr, WM_SYSKEYDOWN, Param, 0u);
					SendMessage(GamePtr, WM_SYSKEYDOWN, Key, 0u);
					Thread.Sleep(duration);
					SendMessage(GamePtr, WM_SYSKEYUP, Param, 0u);
					SendMessage(GamePtr, WM_SYSKEYUP, Key, 0u);
				}
				else
				{
					SendMessage(GamePtr, WM_KEYDOWN, Key, 0u);
					Thread.Sleep(duration);
					SendMessage(GamePtr, WM_KEYUP, Key, 0u);
				}
				busy = false;
			});
			thread.Start();
			return 1;
		}

		public static uint Transcoding(string keyname)
		{
			try
			{
				VK vK = new VK();
				uint num = 0u;
				return Convert.ToUInt32(vK.GetType().GetField("VK_" + keyname).GetValue(vK));
			}
			catch (NullReferenceException)
			{
				return 0u;
			}
		}
	}
}
