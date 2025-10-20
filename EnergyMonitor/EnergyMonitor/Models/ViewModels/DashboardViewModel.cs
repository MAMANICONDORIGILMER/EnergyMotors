using System.Collections.Generic;

namespace EnergyMonitor.Models.ViewModels
{
    public class DashboardViewModel
    {
        public IEnumerable<Device> Devices { get; set; }
        public IEnumerable<Reading> LastReadings { get; set; }
        public IEnumerable<Alert> Alerts { get; set; }
        public int? CurrentDeviceId { get; set; }
    }
}
