namespace EnergyMonitor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Alerts
    {
        public long Id { get; set; }

        public int DeviceId { get; set; }

        [Required]
        [StringLength(32)]
        public string Kind { get; set; }

        [Required]
        [StringLength(256)]
        public string Message { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }

        public bool Acknowledged { get; set; }

        public virtual Devices Devices { get; set; }
    }
}
