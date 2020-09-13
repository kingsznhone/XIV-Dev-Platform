using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;



namespace BloodLetter_Fantasy
{
    public struct HotbarRow
    {
        public HotbarUnit[] hotbarUnits;
    }


    [StructLayout(LayoutKind.Explicit)]
    public struct HotbarUnit 
    {
        [FieldOffset(0x0)]
        public int unittype;
        [FieldOffset(0x4)]
        public int category;
        [FieldOffset(0x8)]
        public int immediate;
        [FieldOffset(0xC)]
        public int skillID;
        [FieldOffset(0x10)]
        public int iconID;
        [FieldOffset(0x14)]
        public int availability;
        [FieldOffset(0x18)]
        public int unknow1;
        [FieldOffset(0x1C)]
        public int unknow2;
        [FieldOffset(0x20)]
        public int refreshprogress;
        [FieldOffset(0x24)]
        public int unknow3;
        [FieldOffset(0x28)]
        public int MPCD;
        [FieldOffset(0x2C)]
        public int amount;
        [FieldOffset(0x30)]
        public int unknow4;
        [FieldOffset(0x34)]
        public int stack;
        [FieldOffset(0x38)]
        public int highlight;
        [FieldOffset(0x3C)]
        public int unknow5;
    }

    public class HotbarCollection
    {
        HotbarRow[] hotbars;

        public HotbarCollection()
        {
            hotbars = new HotbarRow[10];
            for(int i =0; i<hotbars.Length;i++)
            {
                hotbars[i].hotbarUnits = new HotbarUnit[12];
            }
        }
        
        public void AddUnit(byte[] array,int row,int unit)
        {
            HotbarUnit hotbarUnit = (HotbarUnit)ByteToStruct(array, typeof(HotbarUnit));
            hotbars[row].hotbarUnits[unit] = hotbarUnit;
        }

        public object ByteToStruct(byte[] bytes, Type type)
        {
            int size = Marshal.SizeOf(type);
            if (size > bytes.Length)
            {
                return null;
            }
            //分配结构体内存空间
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            //将byte数组拷贝到分配好的内存空间
            Marshal.Copy(bytes, 0, structPtr, size);
            //将内存空间转换为目标结构体
            object obj = Marshal.PtrToStructure(structPtr, type);
            //释放内存空间
            Marshal.FreeHGlobal(structPtr);
            return obj;
        }
    }
}
