﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FlightCompanion
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FlightCompanionEntities : DbContext
    {
        public FlightCompanionEntities()
            : base("name=FlightCompanionEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Aircraft> Aircraft { get; set; }
        public virtual DbSet<FlightHistory> FlightHistories { get; set; }
        public virtual DbSet<IcaoCode> IcaoCodes { get; set; }
        public virtual DbSet<ChartType> ChartTypes { get; set; }
        public virtual DbSet<Chart> Charts { get; set; }
        public virtual DbSet<FlightPlan> FlightPlans { get; set; }
    }
}
