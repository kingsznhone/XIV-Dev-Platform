using ProcessMemoryApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TowerEx_Assistant
{
	public class SignatureScanner
	{
		private ProcessMemoryReader mReader;

		public IntPtr pBaseAddress;

		private IntPtr pEndAddress;

		private IntPtr pStartPoint;

		private long ModuleSize;

		private byte[] ModuleCopy;

		public SignatureScanner(ProcessMemoryReader _mReader)
		{
			mReader = _mReader;
			pBaseAddress = mReader.process.MainModule.BaseAddress;
			pEndAddress = IntPtr.Add(pBaseAddress, mReader.process.MainModule.ModuleMemorySize);
			pStartPoint = pBaseAddress;
			ModuleSize = (long)pEndAddress - (long)pBaseAddress;
			ModuleCopy = new byte[ModuleSize];
		}

		public void ReleaseMemorybuffer()
		{
			ModuleCopy = null;
		}

		public List<IntPtr> ScanPtrBySig(string pattern = "")
		{
			byte?[] array = pattern2SigArray(pattern);
			List<IntPtr> list = new List<IntPtr>();
			if (pattern == null || pattern.Length % 2 != 0)
			{
				return new List<IntPtr>();
			}
			ModuleCopy = mReader.ReadByteArray(pStartPoint, (uint)ModuleSize);
			for (int i = 0; i < ModuleCopy.Length - array.Length - 4 + 1; i++)
			{
				int Checking = 0;
				for (int j = 0; j < array.Length; j++)
				{
					if (!array[j].HasValue)
					{
						Checking++;
						continue;
					}
					if (array[j].Value != ModuleCopy[i + j])
					{
						break;
					}
					Checking++;
				}
				if (Checking == array.Length)
				{
					IntPtr item = new IntPtr(BitConverter.ToInt32(ModuleCopy, i + array.Length));
					long num3 = pStartPoint.ToInt64() + i + array.Length + 4 + item.ToInt64();
					item = new IntPtr(num3 - (long)mReader.process.MainModule.BaseAddress);
					list.Add(item);
				}
			}
			return list;
		}

		public List<IntPtr> ScanMovePtr(string pattern = "")
		{
			byte?[] array = pattern2SigArray(pattern);
			List<IntPtr> list = new List<IntPtr>();
			if (pattern == null || pattern.Length % 2 != 0) return new List<IntPtr>();
			
			ModuleCopy = mReader.ReadByteArray(pStartPoint, (uint)ModuleSize);
			for (int i = 0; i < ModuleCopy.Length - array.Length - 4 + 1; i++)
			{
				int Checking = 0;
				for (int j = 0; j < array.Length; j++)
				{
					if (!array[j].HasValue)
					{
						Checking++;
						continue;
					}
					if (array[j].Value != ModuleCopy[i + j])break;

					Checking++;
				}
				if (Checking == array.Length)
				{
					IntPtr item = new IntPtr(BitConverter.ToInt32(ModuleCopy, i + array.Length));
					long num2 = pStartPoint.ToInt64() + i + array.Length + 4 + item.ToInt64();
					item = new IntPtr(num2 - (long)mReader.process.MainModule.BaseAddress + 336);
					list.Add(item);
				}
			}
			return list;
		}

		private byte?[] pattern2SigArray(string pattern)
		{
			byte?[] array = new byte?[pattern.Length / 2];
			for (int i = 0; i < pattern.Length / 2; i++)
			{
				string text = pattern.Substring(i * 2, 2);
				if (text == "**") array[i] = null;
				else array[i] = Convert.ToByte(text, 16);
			}
			return array;
		}
	}
}
