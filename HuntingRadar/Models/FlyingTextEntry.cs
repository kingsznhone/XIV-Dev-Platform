using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntingRadar.Models
{
	public class FlyingTextEntry
	{
		public long ID
		{
			get;
			set;
		}

		public uint TargetID
		{
			get;
			set;
		}

		public int Amount
		{
			get;
			set;
		}

		public int ComboAmount
		{
			get;
			set;
		}

		public int Type1
		{
			get;
			set;
		}

		public int Type2
		{
			get;
			set;
		}

		public uint BuffID
		{
			get;
			set;
		}
	}
}
