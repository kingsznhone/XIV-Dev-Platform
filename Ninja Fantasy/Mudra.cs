using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KeyboardApi;
namespace Ninja_Fantasy
{
    class Mudra
    {
        public const int duration = 10;
        struct Ten
        {
            public uint Param;
            public uint vK;
        }

        struct Chi
        {
            public uint Param;
            public uint vK;
        }

        struct Jin
        {
            public uint Param;
            public uint vK;
        }

        struct Ninjutsu
        {
            public uint Param;
            public uint vK;
        }
        Ten t;
        Chi c;
        Jin j;
        Ninjutsu n;
        IntPtr Gameptr;
        bool Activated = false;
        public void Install(IntPtr Gameptr, string[] setting)
        {
            this.Gameptr = Gameptr;
            
            t.Param = Simulator.Transcoding(setting[0]);
            t.vK = Simulator.Transcoding(setting[1]);

            c.Param = Simulator.Transcoding(setting[2]);
            c.vK = Simulator.Transcoding(setting[3]);

            j.Param = Simulator.Transcoding(setting[4]);
            j.vK = Simulator.Transcoding(setting[5]);

            n.Param = Simulator.Transcoding(setting[6]);
            n.vK = Simulator.Transcoding(setting[7]);
        }

        public void Uninstall()
        {
            t.Param = 0;
            t.vK = 0;
            c.Param = 0;
            c.vK = 0;
            j.Param = 0;
            j.vK = 0;
            n.Param = 0;
            n.vK = 0;
        }

        public void Huton()
        {
            if (!Activated)
            {
                Thread.Sleep(100);
                for (int i = 0; i < 1; i++)
                {
                    Simulator.Press(Gameptr, j.Param, j.vK,duration);
                }
                Thread.Sleep(300);
                for (int i = 0; i < 1; i++)
                {
                    Simulator.Press(Gameptr, c.Param, c.vK,duration);
                }
                Thread.Sleep(400);
                for (int i = 0; i < 1; i++)
                {
                    Simulator.Press(Gameptr, t.Param, t.vK,duration);
                }
                Thread.Sleep(400);
                for (int i = 0; i < 10; i++)
                {
                    Simulator.Press(Gameptr, n.Param, n.vK,duration);
                }
                Activated = false;
            }

        }

        public void Doton()
        {
            if (!Activated)
            {
                Thread.Sleep(100);
                for (int i = 0; i < 1; i++)
                {
                    Simulator.Press(Gameptr, j.Param, j.vK,duration);
                }

                Thread.Sleep(300);
                for (int i = 0; i < 1; i++)
                {
                    Simulator.Press(Gameptr, t.Param, t.vK,duration);
                }

                Thread.Sleep(400);
                for (int i = 0; i < 1; i++)
                {
                    Simulator.Press(Gameptr, c.Param, c.vK,duration);
                }

                Thread.Sleep(400);
                for (int i = 0; i < 10; i++)
                {
                    Simulator.Press(Gameptr, n.Param, n.vK,duration);
                }
                Activated = false;
            }
        }

        public void Suiton()
        {
            if (!Activated)
            {
                Thread.Sleep(100);
                for (int i = 0; i < 1; i++)
                {
                    Simulator.Press(Gameptr, t.Param, t.vK,duration);
                }

                Thread.Sleep(300);
                for (int i = 0; i < 1; i++)
                {
                    Simulator.Press(Gameptr, c.Param, c.vK,duration);
                }

                Thread.Sleep(400);
                for (int i = 0; i < 1; i++)
                {
                    Simulator.Press(Gameptr, j.Param, j.vK,duration);
                }

                Thread.Sleep(400);
                for (int i = 0; i < 10; i++)
                {
                    Simulator.Press(Gameptr, n.Param, n.vK,duration);
                }
                Activated = false;
            }
        }

        public void Raiton()
        {
            if (!Activated)
            {
                Thread.Sleep(100);
                for (int i = 0; i < 1; i++)
                {
                    Simulator.Press(Gameptr, t.Param, t.vK,duration);
                }

                Thread.Sleep(300);
                for (int i = 0; i < 1; i++)
                {
                    Simulator.Press(Gameptr, c.Param, c.vK,duration);
                }

                Thread.Sleep(400);
                for (int i = 0; i < 10; i++)
                {
                    Simulator.Press(Gameptr, n.Param, n.vK,duration);
                }
                Activated = false;
            }
        }

        public void Katon()
        {
            if (!Activated)
            {
                Thread.Sleep(100);
                for (int i = 0; i < 1; i++)
                {
                    Simulator.Press(Gameptr, j.Param, j.vK,duration);
                }

                Thread.Sleep(300);
                for (int i = 0; i < 1; i++)
                {
                    Simulator.Press(Gameptr, t.Param, t.vK,duration);
                }

                Thread.Sleep(400);
                for (int i = 0; i < 10; i++)
                {
                    Simulator.Press(Gameptr, n.Param, n.vK,duration);
                }
                Activated = false;
            }
        }

        public void Shuriken()
        {
            if (!Activated)
            {
                Thread.Sleep(100);
                for (int i = 0; i < 1; i++)
                {
                    Simulator.Press(Gameptr, t.Param, t.vK,duration);
                }

                Thread.Sleep(300);

                for (int i = 0; i < 10; i++)
                {
                    Simulator.Press(Gameptr, n.Param, n.vK,duration);
                }
                Activated = false;
            }
        }

        public void TCJ()
        {
            if (!Activated)
            {
                Thread.Sleep(100);
                for (int i = 0; i < 1; i++)
                {
                    Simulator.Press(Gameptr, t.Param, t.vK,duration);
                }

                Thread.Sleep(300);
                for (int i = 0; i < 10; i++)
                {
                    Simulator.Press(Gameptr, n.Param, n.vK,duration);
                }

                Thread.Sleep(500);
                for (int i = 0; i < 1; i++)
                {
                    Simulator.Press(Gameptr, c.Param, c.vK,duration);
                }

                Thread.Sleep(500);
                for (int i = 0; i < 10; i++)
                {
                    Simulator.Press(Gameptr, n.Param, n.vK,duration);
                }

                Thread.Sleep(500);
                for (int i = 0; i < 1; i++)
                {
                    Simulator.Press(Gameptr, j.Param, j.vK,duration);
                }

                Thread.Sleep(500);
                for (int i = 0; i < 10; i++)
                {
                    Simulator.Press(Gameptr, n.Param, n.vK,duration);
                }
                Activated = false;
            }
        }
    }
}
