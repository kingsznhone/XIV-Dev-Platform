using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcessMemoryApi;
namespace YSdubug
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessMemoryReader mreader = new ProcessMemoryReader();
            
            mreader.process = Process.GetProcessById(18668);
            mreader.OpenProcess();
            Console.WriteLine(mreader.pBaseAddress);
            Console.WriteLine(mreader.pEndAddress);
            Console.WriteLine(mreader.ModuleSize);


            Console.ReadLine();
        }
    }
}
