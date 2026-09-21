using Microsoft.AspNetCore.Mvc;

namespace PRV.Web.Controllers
{
    public class AdministracionController : Controller
    {
        // Muestra el login
        public IActionResult Index()
        {
            return View();
        }


        // SuperAdministrador - temporal
        [HttpPost]
        public IActionResult Ingresar()
        {
            HttpContext.Session.SetString("IdRol", "1");
            HttpContext.Session.SetString("Nombre", "SuperAdministrador");

            return RedirectToAction("Index", "Home");
        }


        // Administrador de empresa - temporal
        public IActionResult IngresarAdministrador()
        {
            HttpContext.Session.SetString("IdRol", "2");
            HttpContext.Session.SetString("Nombre", "Administrador");

            return RedirectToAction("Index", "Home");
        }


        public IActionResult Salir()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Inicio");
        }
    }
}