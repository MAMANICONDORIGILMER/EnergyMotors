using System.Linq;
using System.Web.Mvc;
using EnergyMonitor.Models;

namespace EnergyMonitor.Controllers
{
    public class AlertsController : Controller
    {
        private readonly EnergyDbContext db = new EnergyDbContext();

        public ActionResult Index()
        {
            var items = db.Alerts
                          .OrderByDescending(a => a.CreatedAt)
                          .Take(100)
                          .ToList();
            return View(items);
        }

        public ActionResult Ack(long id)
        {
            var a = db.Alerts.Find(id);
            if (a != null)
            {
                a.Acknowledged = true;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
