using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace BetMobile.Models.DataBaseModel
{
    public class Match
    {
        public int id_mecz { get; set; }
        public int kurs { get; set; }
        public int bramki_gosc { get; set; }
        public int bramki_gosp { get; set; }
        public System.DateTime data { get; set; }
        public Nullable<int> minuta { get; set; }
        public Nullable<int> id_gosc { get; set; }
        public Nullable<int> id_gosp { get; set; }

    }
}