using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntingRadar.Models
{
	public class FlyingText
	{
		public FlyingTextEntry[] Entries;

		public uint MaxLen
		{
			get;
			set;
		}

		public uint Start
		{
			get;
			set;
		}

		public uint End
		{
			get;
			set;
		}
	}

}
