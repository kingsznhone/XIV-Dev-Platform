using HuntingRadar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntingRadar.Models
{
	public enum PartyTypeEnum
	{
		None,
		Party,
		Alliance
	}

	public class Combatant
	{
		public NetworkBuff[] NetworkBuffs;

		public uint ID
		{
			get;
			set;
		}

		public uint OwnerID
		{
			get;
			set;
		}

		public byte type
		{
			get;
			set;
		}

		public int Job
		{
			get;
			set;
		}

		public int Level
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public uint CurrentHP
		{
			get;
			set;
		}

		public uint MaxHP
		{
			get;
			set;
		}

		public uint CurrentMP
		{
			get;
			set;
		}

		public uint MaxMP
		{
			get;
			set;
		}

		public uint CurrentTP
		{
			get;
			set;
		}

		public uint MaxTP
		{
			get;
			set;
		}

		public uint CurrentCP
		{
			get;
			set;
		}

		public uint MaxCP
		{
			get;
			set;
		}

		public uint CurrentGP
		{
			get;
			set;
		}

		public uint MaxGP
		{
			get;
			set;
		}

		public bool IsCasting
		{
			get;
			set;
		}

		public uint CastBuffID
		{
			get;
			set;
		}

		public uint CastTargetID
		{
			get;
			set;
		}

		public float CastDurationCurrent
		{
			get;
			set;
		}

		public float CastDurationMax
		{
			get;
			set;
		}

		public float PosX
		{
			get;
			set;
		}

		public float PosY
		{
			get;
			set;
		}

		public float PosZ
		{
			get;
			set;
		}

		public float Heading
		{
			get;
			set;
		}

		public uint CurrentWorldID
		{
			get;
			set;
		}

		public uint WorldID
		{
			get;
			set;
		}

		public string WorldName
		{
			get;
			set;
		}

		public uint BNpcNameID
		{
			get;
			set;
		}

		public uint BNpcID
		{
			get;
			set;
		}

		public uint TargetID
		{
			get;
			set;
		}

		public byte EffectiveDistance
		{
			get;
			set;
		}

		public PartyTypeEnum PartyType
		{
			get;
			set;
		}

		public IntPtr Pointer
		{
			get;
			set;
		}

		public int Order
		{
			get;
			set;
		}

		public IncomingAbility[] IncomingAbilities
		{
			get;
		}

		public OutgoingAbility OutgoingAbility
		{
			get;
		}

		public Effect[] Effects
		{
			get;
		}

		public FlyingText FlyingText
		{
			get;
		}

		public Combatant()
		{
			IncomingAbilities = new IncomingAbility[30];
			OutgoingAbility = new OutgoingAbility();
			Effects = new Effect[30];
			FlyingText = new FlyingText
			{
				Entries = new FlyingTextEntry[0]
			};
			NetworkBuffs = new NetworkBuff[30];
		}
	}
}
