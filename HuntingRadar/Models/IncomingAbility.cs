using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntingRadar.Models
{
    public class IncomingAbility
    {
		public uint TargetID
		{
			get;
			set;
		}

		public uint Code1
		{
			get;
			set;
		}

		public uint SequenceID
		{
			get;
			set;
		}

		public uint ActorID
		{
			get;
			set;
		}

		public uint Damage
		{
			get;
			set;
		}

		public uint SkillID
		{
			get;
			set;
		}

		public uint[] EffectData
		{
			get;
			set;
		}
	}
}
