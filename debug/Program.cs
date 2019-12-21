using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MemoryApi;
namespace debug
{
    class Program
    {
        [DllImport("user32.dll")] private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        static void Main(string[] args)
        {
            ProcessMemoryReader p = new ProcessMemoryReader();
            if (p.FindProcess("ffxiv_dx11"))
            {
                p.OpenProcess();
                //not found is 0
                Console.WriteLine(p.handle);
                
                IntPtr ptr = FindWindow(null, "最终幻想XIV");
                //not found is 0
                Console.WriteLine(ptr);
            }
                
            else Console.WriteLine("Not Found");
            
            
            Console.ReadLine();
        }
    }
}
