using System;
using System.Linq;
using System.Web.Mvc;
using EnergyMonitor.Models;
using EnergyMonitor.Models.ViewModels;
using Newtonsoft.Json;

namespace EnergyMonitor.Controllers
{
    public class DashboardController : Controller
    {
        private readonly EnergyDbContext db = new EnergyDbContext();

        public ActionResult Index(int? deviceId, string range = "1h")
        {
            var devId = deviceId ?? db.Devices.Select(d => d.Id).FirstOrDefault();
            DateTime from = DateTime.UtcNow.AddHours(-1);
            if (range == "24h") from = DateTime.UtcNow.AddDays(-1);
            else if (range == "30d") from = DateTime.UtcNow.AddDays(-30);

            var last = db.Readings
                .Where(r => r.DeviceId == devId && r.CreatedAt >= from)
                .OrderByDescending(r => r.CreatedAt)
                .Take(50)
                .ToList();

            if (!last.Any())
            {
                last = db.Readings.Where(r => r.DeviceId == devId)
                    .OrderByDescending(r => r.CreatedAt)
                    .Take(50)
                    .ToList();
            }

            var vm = new EnergyMonitor.Models.ViewModels.DashboardViewModel
            {
                Devices = db.Devices.OrderBy(d => d.Name).ToList(),
                LastReadings = last.OrderBy(r => r.CreatedAt).ToList(),
                Alerts = db.Alerts.OrderByDescending(a => a.CreatedAt).Take(20).ToList(),
                CurrentDeviceId = devId
            };
            return View(vm);
        }

        public ActionResult Series(int deviceId, string range = "1h")
        {
            DateTime nowUtc = DateTime.UtcNow;
            DateTime from = nowUtc.AddHours(-1);
            if (range == "24h") from = nowUtc.AddDays(-1);
            else if (range == "30d") from = nowUtc.AddDays(-30);

            var points = db.Readings
                .Where(r => r.DeviceId == deviceId && r.CreatedAt >= from)
                .OrderBy(r => r.CreatedAt)
                .Select(r => new { t = r.CreatedAt, y = r.Watts })
                .ToList();

            if (points.Count == 0)
            {
                points = db.Readings.Where(r => r.DeviceId == deviceId)
                    .OrderByDescending(r => r.CreatedAt)
                    .Take(300)
                    .OrderBy(r => r.CreatedAt)
                    .Select(r => new { t = r.CreatedAt, y = r.Watts })
                    .ToList();
            }

            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(points), "application/json");
        }


    }
}
