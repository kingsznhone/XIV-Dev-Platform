﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mscan
{
    public class Combatant
    {
        public IntPtr Pointer;

        public uint ID;

        public uint OwnerID;

        public int Order;
        //1:Player 2:Other
        public byte type;

        public int Job;

        public int Level;

        public string Name;

        public int CurrentHP;

        public int MaxHP;

        public int CurrentMP;

        public int MaxMP;

        public int CurrentTP;

        public int MaxTP;

        public int CurrentCP;

        public int MaxCP;

        public int CurrentGP;

        public int MaxGP;

        public bool IsCasting;

        public uint CastBuffID;

        public uint CastTargetID;

        public float CastDurationCurrent;

        public float CastDurationMax;

        public float PosX;

        public float PosZ;

        public float PosY;

        //public PartyTypeEnum PartyType;

        public int WorldID;

        public string WorldName;

        public uint BNpcNameID;

        public uint BNpcID;
    }
}
