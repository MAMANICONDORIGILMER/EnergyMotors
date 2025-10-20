using EnergyMonitor.Models;
using System;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

namespace EnergyMonitor.Controllers
{
    public class SimulatorController : Controller
    {
        private static Timer _timer;
        private static readonly Random Rng = new Random();
        private static int _deviceId = 1;

        public string Start(int deviceId = 1, int seconds = 5)
        {
            _deviceId = deviceId;
            _timer?.Dispose();
            _timer = new Timer(_ =>
            {
                try
                {
                    using (var db = new EnergyDbContext())
                    {
                        // picos entre 1 200 y 2 400 W
                        var watts = 1200 + Rng.Next(1200);
                        db.Readings.Add(new Reading { DeviceId = _deviceId, Watts = watts, CreatedAt = DateTime.UtcNow });
                        db.SaveChanges();

                        // disparador simple de alertas (por si no usas el SP aquí)
                        var maxW = db.Thresholds.Where(t => t.DeviceId == _deviceId && t.Enabled)
                                                .Select(t => t.MaxWatts).FirstOrDefault();
                        if (maxW > 0 && watts > maxW)
                        {
                            db.Alerts.Add(new Alert
                            {
                                DeviceId = _deviceId,
                                Kind = "OverThreshold",
                                Message = $"Potencia {watts}W > {maxW}W",
                                CreatedAt = DateTime.UtcNow
                            });
                            db.SaveChanges();
                        }
                    }
                }
                catch { /* demo */ }
            }, null, TimeSpan.Zero, TimeSpan.FromSeconds(seconds));

            return $"Simulator ON (Device {_deviceId}, every {seconds}s)";
        }

        public string Stop()
        {
            _timer?.Dispose();
            _timer = null;
            return "Simulator OFF";
        }
    }
}
