using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BloodLetter_Fantasy;
using KeyboardApi;

namespace BloodLetter_Fantasy
{
    class Autopilot
    {
        struct SkillHotKey
        {
            public uint Param;
            public uint vK;
        }

        SkillHotKey skillhotkey;
        IntPtr Gameptr;
        bool Activated = false;

        public void Install(IntPtr Gameptr, string Param, string vK)
        {
            this.Gameptr = Gameptr;
            skillhotkey.Param = Simulator.Transcoding(Param);
            skillhotkey.vK = Simulator.Transcoding(vK);

        }

        public void Uninstall()
        {
            Gameptr = (IntPtr)0;
            skillhotkey.Param = 0;
            skillhotkey.vK = 0;
        }

        public void Trigger()
        {
            Activated = true;
            Simulator.Press(Gameptr, skillhotkey.Param, skillhotkey.vK, 20);
            Thread.Sleep(30);
            Activated = false;
        }

    }
}
