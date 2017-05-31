using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetMobile.Models.DataBaseModel
{
    public class Bet
    {
        public int id_zaklad { get; set; }
        public int kwota { get; set; }
        public int wygrana { get; set; }
        public Nullable<int> id_mecz { get; set; }
        public Nullable<int> id_user { get; set; }
        public string status { get; set; }
        public Nullable<int> typ { get; set; }

    }
}