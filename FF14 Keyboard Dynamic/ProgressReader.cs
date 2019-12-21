using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using KeyboardApi;

namespace XIV_Keyboard_Dynamic
{
    public static class ProgressReader
    {
        
        public static Dictionary<string, string> ReadKeyset(string setting)
        {
            Dictionary<string, string> K = new Dictionary<string, string>();
            StreamReader sr = new StreamReader(setting, Encoding.UTF8);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                //line = line.Replace(Environment.NewLine, "").Replace(";", "").Replace(" ", "");
                //string[] tmp = line.Split(',');
                //K[tmp[0]] = tmp[1] + "," + tmp[2];
                Regex reg = new Regex(@"^(.*),(.*),(.*);");
                Match match = reg.Match(line);
                K[match.Groups[1].Value] = match.Groups[2].Value + "," + match.Groups[3].Value;
            }
            return K;
        }

        public static int[] Transcoding(string cmd)
        {
            VK KeyList = new VK();
            int[] Kcode= new int[2];
            string [] tmp = cmd.Split(',');
            if (tmp[0]=="0")
            {
                Kcode[0] = 0;
            }
            else
            {
                Kcode[0] = Convert.ToInt32(KeyList.GetType().GetField("Key_"+tmp[0]).GetValue(KeyList));
            }
            Kcode[1] =Convert.ToInt32(KeyList.GetType().GetField("Key_" + tmp[1]).GetValue(KeyList));
            return Kcode;
        }

        public static List<string> Preload (string pipeline)
        {
            string line;
            List<string> Process = new List<string> { };
            StreamReader sr = new StreamReader(pipeline, Encoding.UTF8);
            while ((line = sr.ReadLine()) != null)
            {
                line = line.Replace(Environment.NewLine, "").Replace(";", "").Replace(" ", "");
                Process.Add(line);
            }
            return Process;
        }
    }
}
