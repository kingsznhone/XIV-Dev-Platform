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

        public void Install(IntPtr Gameptr, string Param, string vK)
        {
            this.Gameptr = Gameptr;
            if (Param == "") skillhotkey.Param = 0;
            else skillhotkey.Param = Simulator.Transcoding(Param);
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
            Simulator.Press(Gameptr, skillhotkey.Param, skillhotkey.vK, 20);
        }

    }
}
