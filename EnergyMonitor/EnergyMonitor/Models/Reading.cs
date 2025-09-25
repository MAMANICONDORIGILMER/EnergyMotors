using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnergyMonitor.Models
{
    public class Reading : Controller
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public double Watts { get; set; }
        public double? KwhTotal { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Device Device { get; set; }
    }
}