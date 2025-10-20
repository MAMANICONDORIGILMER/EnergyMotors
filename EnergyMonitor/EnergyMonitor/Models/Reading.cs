using System;

namespace EnergyMonitor.Models
{
    public class Reading
    {
        public long Id { get; set; }
        public int DeviceId { get; set; }
        public virtual Device Device { get; set; }

        public double Watts { get; set; }
        public double? KwhTotal { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
