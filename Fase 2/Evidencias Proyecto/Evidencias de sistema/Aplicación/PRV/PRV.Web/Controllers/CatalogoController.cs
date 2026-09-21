using Microsoft.AspNetCore.Mvc;

namespace PRV.Web.Controllers
{
    public class CatalogoController : Controller
    {
        public IActionResult Index(long idEmpresa)
        {
            ViewBag.IdEmpresa = idEmpresa;

            return View();
        }
    }
}