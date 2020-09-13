using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntingRadar.Models
{
	public class Effect
	{
		public uint TargetID
		{
			get;
			set;
		}

		public uint BuffID
		{
			get;
			set;
		}

		public float Timer
		{
			get;
			set;
		}

		public uint ActorID
		{
			get;
			set;
		}

		public int Unknown1
		{
			get;
			set;
		}
	}
}
