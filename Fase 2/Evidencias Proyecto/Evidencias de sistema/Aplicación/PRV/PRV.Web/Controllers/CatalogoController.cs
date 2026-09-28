using Microsoft.AspNetCore.Mvc;
using PRV.Web.Services;

namespace PRV.Web.Controllers
{
    public class CatalogoController : Controller
    {
        private readonly EmpresaService _empresaService;
        private readonly ProductoService _productoService;

        public CatalogoController(
            EmpresaService empresaService,
            ProductoService productoService)
        {
            _empresaService = empresaService;
            _productoService = productoService;
        }

        public IActionResult Index(long idEmpresa)
        {
            var empresa = _empresaService.Listar()
                .FirstOrDefault(e => e.IdEmpresa == idEmpresa);

            if (empresa == null)
            {
                return RedirectToAction("Index", "Inicio");
            }

            var productos = _productoService.ListarPorEmpresa(idEmpresa)
                .Where(p => p.Estado == "Activo" && p.Stock > 0)
                .ToList();

            ViewBag.Empresa = empresa;

            return View(productos);
        }
    }
}