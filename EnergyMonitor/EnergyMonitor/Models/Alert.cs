using System;

namespace EnergyMonitor.Models
{
    public class Alert
    {
        public long Id { get; set; }
        public int DeviceId { get; set; }
        public virtual Device Device { get; set; }

        public string Kind { get; set; }      // OverThreshold, Peak, Info
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool Acknowledged { get; set; } = false;
    }
}
