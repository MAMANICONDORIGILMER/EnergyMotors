using System;
using System.Collections.Generic;

namespace EnergyMonitor.Models.ViewModels
{
    public class ReadingChartViewModel
    {
        public int DeviceId { get; set; }
        public List<DateTime> Labels { get; set; }
        public List<double> Watts { get; set; }
    }
}
