using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace EnergyMonitor
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Alerts> Alerts { get; set; }
        public virtual DbSet<Devices> Devices { get; set; }
        public virtual DbSet<Readings> Readings { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Thresholds> Thresholds { get; set; }
        public virtual DbSet<Tips> Tips { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alerts>()
                .Property(e => e.CreatedAt)
                .HasPrecision(3);

            modelBuilder.Entity<Devices>()
                .HasMany(e => e.Alerts)
                .WithRequired(e => e.Devices)
                .HasForeignKey(e => e.DeviceId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Devices>()
                .HasMany(e => e.Readings)
                .WithRequired(e => e.Devices)
                .HasForeignKey(e => e.DeviceId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Devices>()
                .HasMany(e => e.Thresholds)
                .WithRequired(e => e.Devices)
                .HasForeignKey(e => e.DeviceId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Readings>()
                .Property(e => e.CreatedAt)
                .HasPrecision(3);
        }
    }
}
