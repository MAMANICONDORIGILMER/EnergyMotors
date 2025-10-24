using EnergyMonitor.Models;
using EnergyMonitor.Models.Dto;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace EnergyMonitor.Controllers
{
    [RoutePrefix("api/ingest")]
    public class IngestController : ApiController
    {
        [HttpPost]
        [Route("reading")]
        public async Task<IHttpActionResult> PostReading([FromBody] ReadingDto dto)
        {
            if (dto == null || dto.DeviceId <= 0)
                return BadRequest("Payload inválido");

            using (var db = new EnergyDbContext())
            {
                var entity = new Reading
                {
                    DeviceId = dto.DeviceId,
                    Watts = dto.Watts,
                    Voltage = dto.Voltage,
                    Current = dto.Current,
                    KwhTotal = dto.KwhTotal,
                    Source = string.IsNullOrWhiteSpace(dto.Source) ? "UNKNOWN" : dto.Source,
                    CreatedAt = DateTime.UtcNow
                };

                db.Readings.Add(entity);
                await db.SaveChangesAsync();
            }

            return Ok(new { ok = true });
        }
    }
}
