using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EnergyMonitor.Models
{
    public class Device
    {
        public int Id { get; set; }

        [Required, StringLength(80)]
        public string Name { get; set; }

        [StringLength(80)]
        public string Zone { get; set; }

        [StringLength(64)]
        public string Secret { get; set; }

        public bool IsActive { get; set; } = true;

        public virtual ICollection<Reading> Readings { get; set; }
        public virtual ICollection<Threshold> Thresholds { get; set; }
    }
}
