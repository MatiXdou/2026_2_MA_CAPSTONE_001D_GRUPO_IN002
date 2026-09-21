using Microsoft.AspNetCore.Mvc;

namespace PRV.Web.Controllers
{
    public class HomeMayoristaController : Controller
    {
        public IActionResult Index()
        {
            //var tipoCliente = HttpContext.Session.GetString("TipoCliente");

            //if (tipoCliente != "Mayorista")
            //{
            //    return RedirectToAction("Index", "Inicio");
            //}

            return View();
        }
    }
}
