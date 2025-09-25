using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnergyMonitor.Models.ViewModels
{
    public class DashboardViewModel : Controller
    {
        // GET: DashboardViewModel
        public ActionResult Index()
        {
            return View();
        }
    }
}