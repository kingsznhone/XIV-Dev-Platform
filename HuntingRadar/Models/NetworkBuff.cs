using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntingRadar.Models
{

    public class NetworkBuff
    {
        public ushort BuffID { get; set; }

        public ushort BuffExtra { get; set; }

        public DateTime Timestamp { get; set; }

        public float Duration { get; set; }

        public uint ActorID { get; set; }

        public string ActorName { get; set; }

        public uint TargetID { get; set; }

        public string TargetName { get; set; }
    }

}
