using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerEx_Assistant
{
    public class Coordinate
    {
        public float X { get; set; }
        public float Z { get; set; }
        public float Y { get; set; }
        public float RAD { get; set; }

        public Coordinate()
        {
            X = 0;
            Y = 0;
            Z = 0;
            RAD = 0;
        }
    }

}
