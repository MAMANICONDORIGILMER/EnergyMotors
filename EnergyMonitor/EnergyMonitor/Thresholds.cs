namespace EnergyMonitor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Thresholds
    {
        public int Id { get; set; }

        public int DeviceId { get; set; }

        public double? MaxWatts { get; set; }

        public double? MaxKwhDay { get; set; }

        public bool Enabled { get; set; }

        public virtual Devices Devices { get; set; }
    }
}
