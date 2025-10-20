using System.Collections.Generic;
using System.Data.Entity;

namespace EnergyMonitor.Models
{
    public class EnergyDbContext : DbContext
    {
        // Debe existir en Web.config con ese nombre
        public EnergyDbContext() : base("EnergyDb") { }

        public DbSet<Device> Devices { get; set; }
        public DbSet<Reading> Readings { get; set; }
        public DbSet<Threshold> Thresholds { get; set; }
        public DbSet<Alert> Alerts { get; set; }
        public DbSet<Tip> Tips { get; set; }
    }
}
