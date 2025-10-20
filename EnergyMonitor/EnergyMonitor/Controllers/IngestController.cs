using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web.Mvc;
using EnergyMonitor.Models;
using Newtonsoft.Json;

namespace EnergyMonitor.Controllers
{
    public class IngestController : Controller
    {
        private readonly EnergyDbContext db = new EnergyDbContext();

        public class Payload { public double watts; public double? kwhTotal; public DateTime? atUtc; }

        [HttpPost]
        public async Task<ActionResult> Post(int deviceId)
        {
            // lee JSON del cuerpo
            string json = new System.IO.StreamReader(Request.InputStream).ReadToEnd();
            var p = JsonConvert.DeserializeObject<Payload>(json) ?? new Payload();

            // Opción A: insertar directo con EF
            var r = new Reading
            {
                DeviceId = deviceId,
                Watts = p.watts,
                KwhTotal = p.kwhTotal,
                CreatedAt = p.atUtc ?? DateTime.UtcNow
            };
            db.Readings.Add(r);
            await db.SaveChangesAsync();

            // Opción B (opcional): usar tu SP sp_InsertReading
            // db.Database.ExecuteSqlCommand(
            //   "EXEC dbo.sp_InsertReading @DeviceId,@Watts,@KwhTotal,@AtUtc",
            //   new SqlParameter("@DeviceId", deviceId),
            //   new SqlParameter("@Watts", p.watts),
            //   new SqlParameter("@KwhTotal", (object)p.kwhTotal ?? DBNull.Value),
            //   new SqlParameter("@AtUtc", (object)p.atUtc ?? DBNull.Value));

            return Json(new { ok = true });
        }
    }
}
