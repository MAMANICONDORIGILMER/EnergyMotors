namespace EnergyMonitor.Models.Dto
{
    public class ReadingDto
    {
        public int DeviceId { get; set; }
        public decimal? Watts { get; set; }
        public decimal? Voltage { get; set; }
        public decimal? Current { get; set; }
        public decimal? KwhTotal { get; set; }
        public string Source { get; set; }
        // CreatedAt lo pone el servidor si no viene
    }
}
