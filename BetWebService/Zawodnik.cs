//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BetWebService
{
    using System;
    using System.Collections.Generic;
    
    public partial class Zawodnik
    {
        public int id_zaw { get; set; }
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public Nullable<int> id_druz { get; set; }
    
        public virtual Druzyna Druzyna { get; set; }
    }
}
