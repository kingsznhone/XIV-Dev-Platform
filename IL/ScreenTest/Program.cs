using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScreenSearchApi;
namespace ScreenTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Scanner scanner = new Scanner();
            int x;
            int y;
            long elapse;
            Console.ReadLine();
            Console.WriteLine( scanner.FindScreen(@"C:\Hacker\IL\ScreenSearchApi\test.bmp",out x,out y ,out elapse));
            Console.WriteLine("X:{0} Y:{1}  Time:{2}", x, y, elapse);
            Console.ReadLine();
        }
    }
}
