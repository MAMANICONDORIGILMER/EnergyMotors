using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnergyMonitor.Models
{
    public class Device : Controller
    {
        // GET: Device
        public int Id { get; set; }
        public string Name { get; set; }
        public string Zone { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual ICollection<Reading> Readings { get; set; }
    }

}