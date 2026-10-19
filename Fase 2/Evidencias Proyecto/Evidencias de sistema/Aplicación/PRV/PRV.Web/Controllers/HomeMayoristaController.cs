using Microsoft.AspNetCore.Mvc;
using PRV.Web.Services;

namespace PRV.Web.Controllers
{
    public class HomeMayoristaController : Controller
    {
        private readonly ProductoService _productoService;

        public HomeMayoristaController(ProductoService productoService)
        {
            _productoService = productoService;
        }

        public IActionResult Index()
        {
            var idEmpresa = HttpContext.Session.GetString("IdEmpresa");
            var idUsuario = HttpContext.Session.GetString("IdUsuario");

            if (string.IsNullOrEmpty(idUsuario))
            {
                return RedirectToAction("Index", "Inicio");
            }

            var productos = _productoService.ListarPorEmpresa(
                long.Parse(idEmpresa)
            );

            return View(productos);
        }
    }
}