namespace EnergyMonitor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Readings
    {
        public long Id { get; set; }

        public int DeviceId { get; set; }

        public double Watts { get; set; }

        public double? KwhTotal { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }

        public virtual Devices Devices { get; set; }
    }
}
