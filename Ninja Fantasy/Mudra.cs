using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KeyboardApi;
namespace Ninja_Assistant
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
        Ten ten;
        Chi chi;
        Jin jin;
        Ninjutsu nj;
        IntPtr Gameptr;
        bool Activated = false;
        public void Install(IntPtr Gameptr, string[] setting)
        {
            this.Gameptr = Gameptr;
            
            ten.Param = Simulator.Transcoding(setting[0]);
            ten.vK = Simulator.Transcoding(setting[1]);

            chi.Param = Simulator.Transcoding(setting[2]);
            chi.vK = Simulator.Transcoding(setting[3]);

            jin.Param = Simulator.Transcoding(setting[4]);
            jin.vK = Simulator.Transcoding(setting[5]);

            nj.Param = Simulator.Transcoding(setting[6]);
            nj.vK = Simulator.Transcoding(setting[7]);
        }

        public void Uninstall()
        {
            ten.Param = 0;
            ten.vK = 0;
            chi.Param = 0;
            chi.vK = 0;
            jin.Param = 0;
            jin.vK = 0;
            nj.Param = 0;
            nj.vK = 0;
        }

        public void Huton()
        {
            if (!Activated)
            { 
                for (int i = 0; i < 1; i++)
                {
                    Simulator.Press(Gameptr, jin.Param, jin.vK, duration);
                }
            Thread.Sleep(300);
            for (int i = 0; i < 1; i++)
            {
                Simulator.Press(Gameptr, chi.Param, chi.vK, duration);
            }
            Thread.Sleep(400);
            for (int i = 0; i < 1; i++)
            {
                Simulator.Press(Gameptr, ten.Param, ten.vK, duration);
            }
            Thread.Sleep(400);
            for (int i = 0; i < 10; i++)
            {
                Simulator.Press(Gameptr, nj.Param, nj.vK, duration);
            }
            Activated = false;
        }

        }

        public void Doton()
        {
            if (!Activated)
            {
                for (int i = 0; i < 1; i++)
                {
                    Simulator.Press(Gameptr, jin.Param, jin.vK,duration);
                }

                Thread.Sleep(300);
                for (int i = 0; i < 1; i++)
                {
                    Simulator.Press(Gameptr, ten.Param, ten.vK,duration);
                }

                Thread.Sleep(400);
                for (int i = 0; i < 1; i++)
                {
                    Simulator.Press(Gameptr, chi.Param, chi.vK,duration);
                }

                Thread.Sleep(400);
                for (int i = 0; i < 10; i++)
                {
                    Simulator.Press(Gameptr, nj.Param, nj.vK,duration);
                }
                Activated = false;
            }
        }

        public void Suiton()
        {
            if (!Activated)
            {
                for (int i = 0; i < 1; i++)
                {
                    Simulator.Press(Gameptr, ten.Param, ten.vK,duration);
                }

                Thread.Sleep(300);
                for (int i = 0; i < 1; i++)
                {
                    Simulator.Press(Gameptr, chi.Param, chi.vK,duration);
                }

                Thread.Sleep(400);
                for (int i = 0; i < 1; i++)
                {
                    Simulator.Press(Gameptr, jin.Param, jin.vK,duration);
                }

                Thread.Sleep(400);
                for (int i = 0; i < 10; i++)
                {
                    Simulator.Press(Gameptr, nj.Param, nj.vK,duration);
                }
                Activated = false;
            }
        }

        public void Raiton()
        {
            if (!Activated)
            {
                for (int i = 0; i < 1; i++)
                {
                    Simulator.Press(Gameptr, ten.Param, ten.vK,duration);
                }

                Thread.Sleep(300);
                for (int i = 0; i < 1; i++)
                {
                    Simulator.Press(Gameptr, chi.Param, chi.vK,duration);
                }

                Thread.Sleep(400);
                for (int i = 0; i < 10; i++)
                {
                    Simulator.Press(Gameptr, nj.Param, nj.vK,duration);
                }
                Activated = false;
            }
        }

        public void Katon()
        {
            if (!Activated)
            {
                for (int i = 0; i < 1; i++)
                {
                    Simulator.Press(Gameptr, jin.Param, jin.vK,duration);
                }

                Thread.Sleep(300);
                for (int i = 0; i < 1; i++)
                {
                    Simulator.Press(Gameptr, ten.Param, ten.vK,duration);
                }

                Thread.Sleep(400);
                for (int i = 0; i < 10; i++)
                {
                    Simulator.Press(Gameptr, nj.Param, nj.vK,duration);
                }
                Activated = false;
            }
        }

        public void Hyoton()
        {
            if (!Activated)
            {
                for (int i = 0; i < 1; i++)
                {
                    Simulator.Press(Gameptr, chi.Param, chi.vK, duration);
                }

                Thread.Sleep(300);
                for (int i = 0; i < 1; i++)
                {
                    Simulator.Press(Gameptr, jin.Param, jin.vK, duration);
                }

                Thread.Sleep(400);
                for (int i = 0; i < 10; i++)
                {
                    Simulator.Press(Gameptr, nj.Param, nj.vK, duration);
                }
                Activated = false;
            }
        }
    

        public void Shuriken()
        {
            if (!Activated)
            {
                for (int i = 0; i < 1; i++)
                {
                    Simulator.Press(Gameptr, ten.Param, ten.vK,duration);
                }

                Thread.Sleep(300);

                for (int i = 0; i < 10; i++)
                {
                    Simulator.Press(Gameptr, nj.Param, nj.vK,duration);
                }
                Activated = false;
            }
        }

        public void TCJ()
        {
            if (!Activated)
            {
                for (int i = 0; i < 1; i++)
                {
                    Simulator.Press(Gameptr, ten.Param, ten.vK,duration);
                }

                Thread.Sleep(300);
                for (int i = 0; i < 10; i++)
                {
                    Simulator.Press(Gameptr, nj.Param, nj.vK,duration);
                }

                Thread.Sleep(500);
                for (int i = 0; i < 1; i++)
                {
                    Simulator.Press(Gameptr, chi.Param, chi.vK,duration);
                }

                Thread.Sleep(500);
                for (int i = 0; i < 10; i++)
                {
                    Simulator.Press(Gameptr, nj.Param, nj.vK,duration);
                }

                Thread.Sleep(500);
                for (int i = 0; i < 1; i++)
                {
                    Simulator.Press(Gameptr, jin.Param, jin.vK,duration);
                }

                Thread.Sleep(500);
                for (int i = 0; i < 10; i++)
                {
                    Simulator.Press(Gameptr, nj.Param, nj.vK,duration);
                }
                Activated = false;
            }
        }
    }
}
