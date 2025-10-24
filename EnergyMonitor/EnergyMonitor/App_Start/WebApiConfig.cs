using System.Web.Http;
using System.Web.Http.Cors;

namespace EnergyMonitor
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // (Opcional) habilitar CORS local
            config.EnableCors();

            // Rutas por atributo
            config.MapHttpAttributeRoutes();

            // Ruta por convención (fallback)
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
