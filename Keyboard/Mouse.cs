using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace KeyboardApi
{
	public static class Mouse
	{
		private const int WM_LBUTTONDOWN = 0x0201;

		private const int WM_LBUTTONUP = 0x0202;

		private const uint WM_RBUTTONDOWN = 0x0204;

		private const uint WM_RBUTTONUP = 0x0205;

		private static bool busy = false;

		[DllImport("user32.dll")]
		private static extern int SetCursorPos(int x, int y);

		[DllImport("user32.dll")]
		private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		[DllImport("user32.dll")]
		private static extern int SendMessage(IntPtr hWnd, uint Msg, uint wParam, uint lParam);

		public static int MakeLParam(int LoWord, int HiWord)
		{
			return (HiWord << 16) | (LoWord & 0xFFFF);
		}

		public static int LeftClick(IntPtr hWnd, int dx, int dy)
		{
			if (busy)
			{
				return 0;
			}
			busy = true;
			Thread thread = new Thread((ThreadStart)delegate
			{
				SetCursorPos(dx, dy);
				SendMessage(hWnd, WM_LBUTTONDOWN, 1u, (uint)MakeLParam(dx, dy));
				Thread.Sleep(100);
				SendMessage(hWnd, WM_LBUTTONUP, 0u, (uint)MakeLParam(dx, dy));
				busy = false;
			});
			thread.Start();
			return 1;
		}

		public static int RightClick(IntPtr hWnd, int dx, int dy)
		{
			if (busy)
			{
				return 0;
			}
			busy = true;
			Thread thread = new Thread((ThreadStart)delegate
			{
				SetCursorPos(dx, dy);
				SendMessage(hWnd, WM_RBUTTONDOWN, 1u, (uint)MakeLParam(dx, dy));
				Thread.Sleep(100);
				SendMessage(hWnd, WM_RBUTTONUP, 0u, (uint)MakeLParam(dx, dy));
				busy = false;
			});
			thread.Start();
			return 1;
		}

		public static int MoveMouse(IntPtr hWnd, int dx, int dy)
		{
			if (busy)
			{
				return 0;
			}
			busy = true;
			Thread thread = new Thread((ThreadStart)delegate
			{
				SetCursorPos(dx, dy);
				busy = false;
			});
			thread.Start();
			return 1;
		}
	}
}
