using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetConsole
{
    class Uzytkownik
    {
        public Uzytkownik()
        {
        }

        public int id_user { get; set; }
        public string login { get; set; }
        public string haslo { get; set; }
        public int wiek { get; set; }
        public Nullable<int> stan_konta { get; set; }

    }
    
}
