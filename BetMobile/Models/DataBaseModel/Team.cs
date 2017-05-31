using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetMobile.Models.DataBaseModel
{
    public class Team
    {
        public int id_druz { get; set; }
        public string nazwa { get; set; }
        public string stadion { get; set; }
        public Nullable<int> id_liga { get; set; }
        public string liga_mistrzow { get; set; }
        public string liga_europy { get; set; }
    }
}