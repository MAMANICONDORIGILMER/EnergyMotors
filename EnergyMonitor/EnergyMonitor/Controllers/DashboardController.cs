using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnergyMonitor.Models;

namespace EnergyMonitor.Controllers
{
    public class DashboardController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var devices = db.Devices.ToList();
            var readings = db.Readings.OrderByDescending(r => r.CreatedAt).Take(50).ToList();
            ViewBag.Devices = devices;
            return View(readings);
        }
    }
}