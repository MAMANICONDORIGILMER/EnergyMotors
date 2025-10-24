using System;

namespace EnergyMonitor.Models
{
    public class Reading
    {
        public long Id { get; set; }
        public int DeviceId { get; set; }
        public decimal? Watts { get; set; }
        public decimal? Voltage { get; set; }
        public decimal? Current { get; set; }
        public decimal? KwhTotal { get; set; }
        public string Source { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual Device Device { get; set; }
    }
}
