using Microsoft.AspNetCore.Mvc;
using PRV.Web.Models;
using System.Diagnostics;

namespace PRV.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            var idUsuario = HttpContext.Session.GetString("IdUsuario");

            if (string.IsNullOrEmpty(idUsuario))
            {
                return RedirectToAction("Index", "Inicio");
            }

            return View();
        }

    }
}
