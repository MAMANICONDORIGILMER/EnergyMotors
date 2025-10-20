using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using EnergyMonitor.Models;

namespace EnergyMonitor.Controllers
{
    public class DeviceController : Controller
    {
        private readonly EnergyDbContext db = new EnergyDbContext();

        public ActionResult Index() => View(db.Devices.OrderBy(d => d.Name).ToList());

        public ActionResult Edit(int? id)
        {
            if (id == null) return View(new Device());
            var dev = db.Devices.Find(id);
            if (dev == null) return HttpNotFound();
            return View(dev);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(Device model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.Id == 0) db.Devices.Add(model);
            else db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var dev = db.Devices.Find(id);
            if (dev == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            db.Devices.Remove(dev);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
