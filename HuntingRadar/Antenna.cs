using ProcessMemoryApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HuntingRadar.Models;
namespace HuntingRadar
{
    class Antenna
    {
        string sCombatantArray = "48c1ea0381faa7010000****8bc2488d0d";

        ProcessMemoryReader mReader = new ProcessMemoryReader();
        IntPtr pStartPoint;
        public IntPtr pBaseAddress;
        byte[] ModuleCopy;

        public IntPtr pMobArray;
        public bool Initialized = false;
        public List<Combatant> combatants = new List<Combatant>();

        Thread Update;

        public Antenna()
        {
        }

        public void StartUpdate()
        {
            if (!Initialized)
            {
                Process p = Process.GetProcessesByName("ffxiv_dx11").ToList().FirstOrDefault();
                mReader.process = p;
                mReader.OpenProcess();
                pBaseAddress = mReader.process.MainModule.BaseAddress;
                pStartPoint = pBaseAddress;
                IntPtr pEndAddress = IntPtr.Add(pBaseAddress, mReader.process.MainModule.ModuleMemorySize);
                long ModuleSize = (long)pEndAddress - (long)pBaseAddress;
                ModuleCopy = mReader.ReadByteArray(pStartPoint, (uint)ModuleSize);
                pMobArray = SearchMobArrayOffset();
                Update = new Thread(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(100);
                        if (Initialized) combatants = GetCombatantList(new IntPtr(((long)pBaseAddress + (long)pMobArray)));
                    }
                });
                Update.Start();
                Initialized = true;
            }
        }
        
        public void EndUpdate()
        {
            try {Update.Abort();}
            catch { }
            Initialized = false;
        }

        private IntPtr SearchMobArrayOffset()
        {
            if (sCombatantArray == null || sCombatantArray.Length % 2 != 0) return new IntPtr();
            byte?[] SigArray = pattern2SigArray(sCombatantArray);
            

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
                    IntPtr pCombatantArray;
                    pCombatantArray = new IntPtr(BitConverter.ToInt32(ModuleCopy, i + SigArray.Length));
                    long num3 = pStartPoint.ToInt64() + i + SigArray.Length + 4 + pCombatantArray.ToInt64();
                    pCombatantArray = new IntPtr(num3 - (long)mReader.process.MainModule.BaseAddress);
                    ModuleCopy = null;
                    return pCombatantArray;
                }
            }
            return new IntPtr();
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

        private List<Combatant> GetCombatantList(IntPtr MobArray)
        {
            List<Combatant> list = new List<Combatant>();
            if (!Initialized)
            {
                return list;
            }
            if (MobArray == IntPtr.Zero)
            {
                return list;
            }
            int MaxCombatants = 421;
            int num2 = 8;
            byte[] array = mReader.ReadByteArray(MobArray, (uint)(MaxCombatants * num2));
            if (array == null || array.Length == 0)
            {
                return list;
            }
            for (int i = 0; i < MaxCombatants; i++)
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

        private Combatant GetCombatantByPtr(IntPtr CombatantPtr)
        {
            

            byte type = mReader.ReadByte(IntPtr.Add(CombatantPtr, 0x8C));
            if (type != 1 && type != 2) return null;

            Combatant combatant = new Combatant();
            combatant.Pointer = CombatantPtr;
            combatant.type = type;
            combatant.ID = (uint)mReader.ReadInt32(IntPtr.Add(CombatantPtr, 0x74));
            if (combatant.ID == 0 || combatant.ID == 3758096384u) return null;

            //Name
            byte[] array = mReader.ReadByteArray(IntPtr.Add(CombatantPtr, 0x30), 0x40);
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
            combatant.BNpcID = (uint)mReader.ReadInt32(IntPtr.Add(CombatantPtr, 0x80));

            combatant.OwnerID = (uint)mReader.ReadInt32(IntPtr.Add(CombatantPtr, 0x84));
            if (combatant.OwnerID == 3758096384u)
            {
                combatant.OwnerID = 0u;
            }
            
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
                combatant.CurrentHP = BitConverter.ToUInt32(array2, 0);
                combatant.MaxHP = BitConverter.ToUInt32(array2, 4);
                combatant.CurrentMP = BitConverter.ToUInt32(array2, 8);
                combatant.MaxMP = BitConverter.ToUInt32(array2, 12);
                combatant.CurrentTP = BitConverter.ToUInt32(array2, 16);
                combatant.MaxTP = 1000;
                if (combatant.CurrentTP < 0)
                {
                    combatant.CurrentTP = 0;
                }
                else if (combatant.CurrentTP > combatant.MaxTP)
                {
                    combatant.CurrentTP = combatant.MaxTP;
                }
                combatant.CurrentGP = BitConverter.ToUInt32(array2, 18);
                combatant.MaxGP = BitConverter.ToUInt32(array2, 20);
                if (combatant.MaxGP == 0)
                {
                    combatant.CurrentGP = 0;
                }
                combatant.CurrentCP = BitConverter.ToUInt32(array2, 22);
                combatant.MaxCP = BitConverter.ToUInt32(array2, 24);
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
