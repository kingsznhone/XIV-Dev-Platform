using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerEx_Assistant
{
    public enum TeleportMode
    {
        Relative = 0,
        Polar= 1,
        Absolute =2
    }

    public class Coordinate
    {
        public float X { get; set; }
        public float Z { get; set; }
        public float Y { get; set; }
        public float Theta { get; set; }

        public Coordinate()
        {
            X = 0;
            Y = 0;
            Z = 0;
            Theta = 0;
        }
        public static Coordinate operator + (Coordinate C1, Coordinate C2)
        {
            Coordinate C = new Coordinate();
            C.X = C1.X + C2.X;
            C.Y = C1.Y + C2.Y;
            C.Z = C1.Z+ C2.Z;
            return C;
        }

        public static Coordinate operator - (Coordinate C1, Coordinate C2)
        {
            Coordinate C = new Coordinate();
            C.X = C1.X - C2.X;
            C.Y = C1.Y - C2.Y;
            C.Z = C1.Z - C2.Z;
            return C;
        }

    }

}
