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
    
    public partial class Participation
    {
        public int id_udzial { get; set; }
        public Nullable<int> id_gosc { get; set; }
        public Nullable<int> id_gosp { get; set; }
        public Nullable<int> id_mecz { get; set; }
    
        public virtual Match Match { get; set; }
        public virtual Team Team { get; set; }
        public virtual Team Team1 { get; set; }
    }
}
