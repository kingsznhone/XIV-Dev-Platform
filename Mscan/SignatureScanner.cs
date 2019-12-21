using MemoryApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mscan
{
    public class SignatureScanner
    {
        ProcessMemoryReader mReader = new ProcessMemoryReader();
        public IntPtr pBaseAddress;
        IntPtr pEndAddress;
        IntPtr pStartPoint;
        long ModuleSize;
        byte[] ModuleCopy;

        public SignatureScanner()
        {
            Process p = Process.GetProcessesByName("ffxiv_dx11").ToList().FirstOrDefault();
            mReader.process = p;
            mReader.OpenProcess();
            pBaseAddress = mReader.process.MainModule.BaseAddress;
            pEndAddress = IntPtr.Add(pBaseAddress, mReader.process.MainModule.ModuleMemorySize);
            pStartPoint = pBaseAddress;
            ModuleSize = (long)pEndAddress - (long)pBaseAddress;
            ModuleCopy = new byte[ModuleSize];
        }

        public  void ReleaseMemory()
        {
            ModuleCopy = null;
        }

        public List<IntPtr> ScanCombatantArray(string pattern = "")
        {
            byte?[] SigArray = pattern2SigArray(pattern);
            List<IntPtr> list = new List<IntPtr>();
            if (pattern == null || pattern.Length % 2 != 0) return new List<IntPtr>();

            ModuleCopy = mReader.ReadByteArray(pStartPoint, (uint)ModuleSize);

            //Search in Module
            for (int i = 0; i < ModuleCopy.Length - SigArray.Length - 4 + 1; i++)
            {
                int Checking = 0;
                for (int j = 0; j < SigArray.Length; j++)
                {
                    if (!SigArray[j].HasValue) Checking++;
                    else
                    {
                        if (SigArray[j].Value != ModuleCopy[i + j]) break;
                        Checking++;
                    }
                }
                if (Checking == SigArray.Length)
                {
                    IntPtr item;
                    item = new IntPtr(BitConverter.ToInt32(ModuleCopy, i + SigArray.Length));
                    long num3 = pStartPoint.ToInt64() + i + SigArray.Length + 4 + item.ToInt64();
                    item = new IntPtr(num3 - (long)mReader.process.MainModule.BaseAddress);
                    list.Add(item);
                }
            }
            
            return list;
        }

        private byte?[] pattern2SigArray(string pattern)
        {
            byte?[] Buffer = new byte?[pattern.Length / 2];
            for (int i = 0; i < pattern.Length / 2; i++)
            {
                string text = pattern.Substring(i * 2, 2);
                if (text == "**") Buffer[i] = null;
                else Buffer[i] = Convert.ToByte(text, 16);
            }
            return Buffer;
        }


        public List<Combatant> GetCombatantList(IntPtr pointer)
        {
            bool Initialized = true;

            List<Combatant> list = new List<Combatant>();
            if (!Initialized)
            {
                return list;
            }
            if (pointer == IntPtr.Zero)
            {
                return list;
            }
            int num = 421;
            int num2 = 8;
            byte[] array = mReader.ReadByteArray(pointer, (uint)(num * num2));
            if (array == null || array.Length == 0)
            {
                return list;
            }
            for (int i = 0; i < num; i++)
            {
                IntPtr intPtr = new IntPtr(BitConverter.ToInt64(array, i * num2));
                if (!(intPtr == IntPtr.Zero))
                {
                    Combatant combatantByPtr = GetCombatantByPtr(intPtr);
                    if (combatantByPtr != null)
                    {
                        bool flag = false;
                        for (int j = 0; j < list.Count; j++)
                        {
                            if (list[j].ID == combatantByPtr.ID)
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (!flag)
                        {
                            combatantByPtr.Order = i + 1;
                            list.Add(combatantByPtr);
                        }
                    }
                }
            }
            return list;
        }

        public Combatant GetCombatantByPtr(IntPtr CombatantPtr)
        {
            byte b = mReader.ReadByte(IntPtr.Add(CombatantPtr, 140));
            if (b != 1 && b != 2)
            {
                return null;
            }
            Combatant combatant = new Combatant();
            combatant.Pointer = CombatantPtr;
            combatant.type = b;
            combatant.ID = (uint)mReader.ReadInt32(IntPtr.Add(CombatantPtr, 116));
            if (combatant.ID == 0 || combatant.ID == 3758096384u)
            {
                return null;
            }
            byte[] array = mReader.ReadByteArray(IntPtr.Add(CombatantPtr, 48), 64);
            if (array == null || array.Length == 0)
            {
                return null;
            }
            combatant.Name = (Encoding.UTF8.GetString(array) ?? "");
            if (combatant.Name.IndexOf('\0') > 0)
            {
                combatant.Name = combatant.Name.Substring(0, combatant.Name.IndexOf('\0'));
            }
            if (combatant.Name.IndexOf('\0') == 0)
            {
                return null;
            }
            if (combatant.Name.Length == 0)
            {
                return null;
            }
            combatant.OwnerID = (uint)mReader.ReadInt32(IntPtr.Add(CombatantPtr, 132));
            if (combatant.OwnerID == 3758096384u)
            {
                combatant.OwnerID = 0u;
            }
            combatant.BNpcID = (uint)mReader.ReadInt32(IntPtr.Add(CombatantPtr, 128));
            byte[] value = mReader.ReadByteArray(IntPtr.Add(CombatantPtr, 160), 20);
            combatant.PosX = BitConverter.ToSingle(value, 0);
            combatant.PosZ = BitConverter.ToSingle(value, 4);
            combatant.PosY = BitConverter.ToSingle(value, 8);
            int WorldOffset = 5966;
            combatant.WorldID = BitConverter.ToUInt16(mReader.ReadByteArray(IntPtr.Add(CombatantPtr, WorldOffset), 2), 0);
            if (combatant.WorldID == 65535)
            {
                combatant.WorldID = 0;
                combatant.WorldName = "";
            }
            else
            {

                //combatant.WorldName = WorldList.Instance.GetWorldNameById(combatant.WorldID);
            }
            int offset2 = 5936;
            combatant.BNpcNameID = (uint)mReader.ReadInt32(IntPtr.Add(CombatantPtr, offset2));
            int offset3 = 5976;
            byte[] array2 = mReader.ReadByteArray(IntPtr.Add(CombatantPtr, offset3), 60);
            if (array2 != null && array2.Length == 60)
            {
                combatant.CurrentHP = BitConverter.ToInt32(array2, 0);
                combatant.MaxHP = BitConverter.ToInt32(array2, 4);
                combatant.CurrentMP = BitConverter.ToInt32(array2, 8);
                combatant.MaxMP = BitConverter.ToInt32(array2, 12);
                combatant.CurrentTP = BitConverter.ToInt16(array2, 16);
                combatant.MaxTP = 1000;
                if (combatant.CurrentTP < 0)
                {
                    combatant.CurrentTP = 0;
                }
                else if (combatant.CurrentTP > combatant.MaxTP)
                {
                    combatant.CurrentTP = combatant.MaxTP;
                }
                combatant.CurrentGP = BitConverter.ToInt16(array2, 18);
                combatant.MaxGP = BitConverter.ToInt16(array2, 20);
                if (combatant.MaxGP == 0)
                {
                    combatant.CurrentGP = 0;
                }
                combatant.CurrentCP = BitConverter.ToInt16(array2, 22);
                combatant.MaxCP = BitConverter.ToInt16(array2, 24);
                combatant.Job = array2[56];
                combatant.Level = array2[58];
            }
            int offset4 = 6912;
            byte[] array3 = mReader.ReadByteArray(IntPtr.Add(CombatantPtr, offset4), 64);
            if (array3 != null && array3.Length == 64)
            {
                combatant.IsCasting = (array3[0] == 1 && array3[1] == 1);
                combatant.CastBuffID = BitConverter.ToUInt32(array3, 4);
                combatant.CastTargetID = BitConverter.ToUInt32(array3, 16);
                combatant.CastDurationCurrent = BitConverter.ToSingle(array3, 52);
                combatant.CastDurationMax = BitConverter.ToSingle(array3, 56);
            }
            return combatant;
        }

    }
}
