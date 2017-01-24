using BetConsole.localhost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var ws = new WSToDatabase();

            while (true)
            {
                ws.UpdateMatchMinute();
                ws.UpdateBetStatus();
                
                Console.WriteLine("UpdateMinute");

                Console.ReadKey();
            }
        }
    }
}
