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
        public static void addMatch(int start, int stop)
        {
            var ws = new WSToDatabase();
            DateTime data = DateTime.Now;
            int idGosc = 0, idGosp = 0;
            int kurs;
            int minuta;
            Random rand = new Random();
            kurs = rand.Next(1, 5);
            minuta = rand.Next(1, 3);
            while (idGosp == idGosc)
            {
                idGosc = rand.Next(start, stop);
                idGosp = rand.Next(start, stop);
            }
            ws.AddMatch(kurs, idGosc, idGosp, data, 0, 0, minuta);

        }
        static void Main(string[] args)
        {
            Console.ReadKey();
            var ws = new WSToDatabase();

            for (int i = 0; i < 7; i++)
            {
                addMatch(1, 10);
            }
            while (true)
            {
                for (int i = 0; i < 10; i++)
                {
                    ws.UpdateMatchMinute();
                    ws.UpdateBetStatus();
                }
                Console.WriteLine("UpdateMinute");

                Console.ReadKey();
            }
        }
    }
}
