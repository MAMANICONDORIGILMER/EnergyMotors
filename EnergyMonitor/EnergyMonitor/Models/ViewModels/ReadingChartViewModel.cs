using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnergyMonitor.Models.ViewModels
{
    public class ReadingChartViewModel : Controller
    {
        // GET: ReadingChartViewModel
        public ActionResult Index()
        {
            return View();
        }
    }
}