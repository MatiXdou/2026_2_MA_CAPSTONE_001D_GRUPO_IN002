using Microsoft.AspNetCore.Mvc;
using PRV.Web.Models;
using System.Diagnostics;

namespace PRV.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            ViewBag.Nombre = HttpContext.Session.GetString("Nombre");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewBag.TipoCliente = HttpContext.Session.GetString("TipoCliente");
            ViewBag.IdEmpresa = HttpContext.Session.GetString("IdEmpresa");

            return View();
        }

        
    }
}
