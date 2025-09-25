using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnergyMonitor.Models
{
    public class Alert : Controller
    {
        // GET: Alert
        public ActionResult Index()
        {
            return View();
        }
    }
}