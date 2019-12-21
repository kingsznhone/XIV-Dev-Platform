using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TowerEx_Assistant
{
    public class Preset
    {
       public Dictionary<string, Coordinate> List = new Dictionary<string, Coordinate>();

        public Preset()
        {
            if (!File.Exists(@"Save.txt"))
            {
                using (StreamWriter sw = File.CreateText(@"Save.txt"))
                {
                }
            }
            else Load();
        }

        public void Load()
        {

            Regex R = new Regex(@"^(.*),(.*),(.*),(.*);");
            using (StreamReader sr = File.OpenText(@"Save.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Match M = R.Match(line);
                    try
                    {
                        Coordinate buffer = new Coordinate();
                        buffer.X = Convert.ToSingle(M.Result("$2"));
                        buffer.Y = Convert.ToSingle(M.Result("$3"));
                        buffer.Z = Convert.ToSingle(M.Result("$4"));
                        List.Add(M.Result("$1"), buffer);
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("读取已保存的坐标失败"+err, "严重错误：上溢", MessageBoxButtons.OK);
                    }                  
                }
            }
        }

        public void Save()
        {
            File.WriteAllText(@"Save.txt", string.Empty);
            using (StreamWriter sr = new StreamWriter(@"Save.txt"))
            {
                foreach (string key in List.Keys)
                {
                    string sbuffer = "";
                    sbuffer = key + ",";
                    Coordinate coordset = new Coordinate();
                    coordset = List[key];
                    sbuffer = sbuffer + Convert.ToString(coordset.X) + ",";
                    sbuffer = sbuffer + Convert.ToString(coordset.Y) + ",";
                    sbuffer = sbuffer + Convert.ToString(coordset.Z) + ";" ;
                    sr.WriteLine(sbuffer);
                }
            }
        }  
    }
}
