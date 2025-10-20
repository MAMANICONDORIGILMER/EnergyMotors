using System.Web.Mvc;

namespace EnergyMonitor.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login() => View();

        [HttpPost]
        public ActionResult Login(string user, string pass)
        {
            if (user == "admin" && pass == "admin")
            {
                Session["user"] = "admin";
                return RedirectToAction("Index", "Dashboard");
            }
            ViewBag.Error = "Credenciales inválidas";
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}
