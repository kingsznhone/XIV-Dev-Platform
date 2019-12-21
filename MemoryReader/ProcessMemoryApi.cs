﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MemoryApi
{
    class Win32MemoryApi
    {
        // constants information can be found in <winnt.h>
        [Flags]
        public enum ProcessAccessType
        {
            PROCESS_TERMINATE = (0x0001),
            PROCESS_CREATE_THREAD = (0x0002),
            PROCESS_SET_SESSIONID = (0x0004),
            PROCESS_VM_OPERATION = (0x0008),
            PROCESS_VM_READ = (0x0010),
            PROCESS_VM_WRITE = (0x0020),
            PROCESS_DUP_HANDLE = (0x0040),
            PROCESS_CREATE_PROCESS = (0x0080),
            PROCESS_SET_QUOTA = (0x0100),
            PROCESS_SET_INFORMATION = (0x0200),
            PROCESS_QUERY_INFORMATION = (0x0400),
            PROCESS_QUERY_LIMITED_INFORMATION = (0x1000)
        }
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(uint dwDesiredAccess, int bInheritHandle, uint dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern Int32 CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll")]
        public static extern Int32 ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, UInt32 size, out IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        public static extern Int32 WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, UInt32 size, out IntPtr lpNumberOfBytesWritten);
    }

    public class ProcessMemoryReader
    {
        IntPtr pBytesRead=IntPtr.Zero;
        IntPtr pBytesWritten = IntPtr.Zero;
        public Process process { get; set; }
        public IntPtr handle;

        public bool FindProcess(string name)
        {
            process = Process.GetProcessesByName(name).ToList().FirstOrDefault();
            return (process != null) ? true : false;
        }

        public void OpenProcess()
        {
            Win32MemoryApi.ProcessAccessType access =
                Win32MemoryApi.ProcessAccessType.PROCESS_QUERY_INFORMATION |
                Win32MemoryApi.ProcessAccessType.PROCESS_VM_READ |
                Win32MemoryApi.ProcessAccessType.PROCESS_VM_WRITE |
                Win32MemoryApi.ProcessAccessType.PROCESS_VM_OPERATION;
            handle = Win32MemoryApi.OpenProcess((uint)access, 1, (uint)process.Id);
        }

        public void CloseHandle()
        {
            int returnValue = Win32MemoryApi.CloseHandle(handle);
            if (returnValue != 0)
            {
                throw new Exception("Closing handle failed.");
            }
        }

        public long ReadPtr_x64(long[] PointerChain)
        {
            bool isbase = true;
            IntPtr pBytesRead;
            byte[] buffer = new byte[8];
            long Pointer = 0;
            foreach (long Chain in PointerChain)
            {
                if (isbase)
                {
                    Win32MemoryApi.ReadProcessMemory(handle, (IntPtr)((long)process.Modules[0].BaseAddress + Chain), buffer, 8, out pBytesRead);
                    Pointer = BitConverter.ToInt64(buffer,0);
                    isbase = false;
                }
                else
                {
                    Win32MemoryApi.ReadProcessMemory(handle, (IntPtr)(Pointer +Chain), buffer, 8, out pBytesRead);
                    Pointer = BitConverter.ToInt64(buffer,0);
                }
            }
            return Pointer;
        }

        public byte ReadByte(IntPtr memoryAddress)
        {
            byte[] buffer = new byte[1];
            Win32MemoryApi.ReadProcessMemory(handle, memoryAddress, buffer, 1, out pBytesRead);
            return buffer[0];
        }

        public byte[] ReadByteArray(IntPtr memoryAddress, uint length)
        {
            byte[] buffer = new byte[length];
            Win32MemoryApi.ReadProcessMemory(handle, memoryAddress, buffer, length, out pBytesRead);
            return buffer;
        }

        public int ReadInt16(IntPtr memoryAddress)
        {
            byte[] buffer = new byte[2];
            Win32MemoryApi.ReadProcessMemory(handle, memoryAddress, buffer, 2, out pBytesRead);
            short i = BitConverter.ToInt16(buffer, 0);
            return i;
        }

        public int ReadInt32(IntPtr memoryAddress)
        {
            byte[] buffer = new byte[4];
            Win32MemoryApi.ReadProcessMemory(handle, memoryAddress, buffer, 4, out pBytesRead);
            int i = BitConverter.ToInt32(buffer,0);
            return i;
        }

        public long ReadInt64(IntPtr memoryAddress)
        {
            byte[] buffer = new byte[8];
            Win32MemoryApi.ReadProcessMemory(handle, memoryAddress, buffer, 8, out pBytesRead);
            long i = BitConverter.ToInt64(buffer, 0);
            return i;
        }

        public float ReadSingle(IntPtr memoryAddress)
        {
            byte[] buffer = new byte[4];
            Win32MemoryApi.ReadProcessMemory(handle, memoryAddress, buffer, 4, out pBytesRead);
            float i = BitConverter.ToSingle(buffer, 0);
            return i;
        }

        public double ReadDouble(IntPtr memoryAddress)
        {
            byte[] buffer = new byte[8];
            Win32MemoryApi.ReadProcessMemory(handle, memoryAddress, buffer, 8, out pBytesRead);
            double i = BitConverter.ToDouble(buffer, 0);
            return i;
        }

        public void WriteToPtr_x64(long[] PointerChain,byte[] Data)
        {
            IntPtr pBytesWritten;
            IntPtr pBytesRead;
            byte[] buffer = new byte[8];
            bool isbase = true;
            long Pointer = 0;
            foreach (long Chain in PointerChain)
            {
                if (isbase)
                {
                    Win32MemoryApi.ReadProcessMemory(handle, (IntPtr)((long)process.Modules[0].BaseAddress + Chain), buffer, 8, out pBytesRead);
                    Pointer = BitConverter.ToInt64(buffer, 0);
                    isbase = false;
                }
                else
                {
                    Win32MemoryApi.ReadProcessMemory(handle, (IntPtr)(Pointer + Chain), buffer, 8, out pBytesRead);
                    Pointer = BitConverter.ToInt64(buffer, 0);
                }
            }
            Win32MemoryApi.WriteProcessMemory(handle, (IntPtr)Pointer, Data, (uint)Data.Length, out pBytesWritten);

        }

        public void WriteByte(IntPtr memoryAddress, byte byteToWrite)
        {
            byte[] buffer = new byte[1];
            buffer[0] = byteToWrite;
            pBytesWritten = IntPtr.Zero;
            Win32MemoryApi.WriteProcessMemory(handle, memoryAddress, buffer, 1, out pBytesWritten);
        }

        public void WriteByteArray(IntPtr memoryAddress, byte[] bytesToWrite)
        {
            pBytesWritten = IntPtr.Zero;
            Win32MemoryApi.WriteProcessMemory(handle, memoryAddress, bytesToWrite, (uint)bytesToWrite.Length, out pBytesWritten);
        }

        public void WriteInt16(IntPtr memoryAddress, short i)
        {
            pBytesWritten = IntPtr.Zero;
            byte[] buffer = BitConverter.GetBytes(i);
            Win32MemoryApi.WriteProcessMemory(handle, memoryAddress, buffer, 2, out pBytesWritten);
        }

        public void WriteInt32(IntPtr memoryAddress, int i)
        {
            pBytesWritten = IntPtr.Zero;
            byte[] buffer = BitConverter.GetBytes(i);
            Win32MemoryApi.WriteProcessMemory(handle, memoryAddress, buffer, 4, out pBytesWritten);
        }

        public void WriteInt64(IntPtr memoryAddress, long i)
        {
            pBytesWritten = IntPtr.Zero;
            byte[] buffer = BitConverter.GetBytes(i);
            Win32MemoryApi.WriteProcessMemory(handle, memoryAddress, buffer, 8, out pBytesWritten);
        }

        public void WriteSingle(IntPtr memoryAddress, float i)
        {
            pBytesWritten = IntPtr.Zero;
            byte[] buffer = BitConverter.GetBytes(i);
            Win32MemoryApi.WriteProcessMemory(handle, memoryAddress, buffer, 4, out pBytesWritten);
        }

        public void WriteDouble(IntPtr memoryAddress, double i)
        {
            pBytesWritten = IntPtr.Zero;
            byte[] buffer = BitConverter.GetBytes(i);
            Win32MemoryApi.WriteProcessMemory(handle, memoryAddress, buffer, 8, out pBytesWritten);
        }
    }
}