﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class STS2Entities : DbContext
    {
        public STS2Entities()
            : base("name=STS2Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Attrib> Attribs { get; set; }
        public virtual DbSet<Bet> Bets { get; set; }
        public virtual DbSet<League> Leagues { get; set; }
        public virtual DbSet<Match> Matches { get; set; }
        public virtual DbSet<Participation> Participations { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
