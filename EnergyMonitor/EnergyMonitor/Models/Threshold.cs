namespace EnergyMonitor.Models
{
    public class Threshold
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public virtual Device Device { get; set; }

        public double? MaxWatts { get; set; }
        public double? MaxKwhDay { get; set; }
        public bool Enabled { get; set; } = true;
    }
}
