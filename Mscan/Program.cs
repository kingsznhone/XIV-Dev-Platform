using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MemoryApi;
namespace Mscan
{
    class Program
    {
        ProcessMemoryReader mReader = new ProcessMemoryReader();
        
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World");
            //Console.WriteLine( Convert.ToString( Console.CursorTop) );
            
            init();
            Console.ReadLine();
        }

        public static void init()
        {
            string pattern = "488b420848c1e8033da701000077248bc0488d0d";
            SignatureScanner scanner = new SignatureScanner();
            List<IntPtr> L = scanner.ScanCombatantArray(pattern);
            Console.WriteLine(L.Count);
            Console.WriteLine(L[0]);
            
            while (true)
            {
                List<Combatant> combatants = scanner.GetCombatantList(IntPtr.Add(scanner.pBaseAddress, (int)L[0]));
                foreach (Combatant c in combatants)
                {
                    Console.WriteLine(c.Name);
                }
                Console.WriteLine(combatants.Count);
                Thread.Sleep(3000);
                Console.Clear();
            }
        }


        
    }
}
