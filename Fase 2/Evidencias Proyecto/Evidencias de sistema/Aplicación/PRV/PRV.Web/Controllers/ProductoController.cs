using Microsoft.AspNetCore.Mvc;
using PRV.Web.Models;
using PRV.Web.Services;

namespace PRV.Web.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ProductoService _productoService;

        public ProductoController(ProductoService productoService)
        {
            _productoService = productoService;
        }


        // ==========================================
        // LISTAR PRODUCTOS
        // ==========================================
        public IActionResult Index()
        {
            var idEmpresa = HttpContext.Session.GetString("IdEmpresa");

            if (string.IsNullOrEmpty(idEmpresa))
            {
                return RedirectToAction("Index", "Inicio");
            }

            var productos = _productoService.ListarMantenedor(
                long.Parse(idEmpresa)
            );

            return View(productos);
        }


        // ==========================================
        // CREAR PRODUCTO
        // ==========================================
        [HttpPost]
        public IActionResult Crear([FromBody] Producto producto)
        {
            var idEmpresa = HttpContext.Session.GetString("IdEmpresa");

            if (string.IsNullOrEmpty(idEmpresa))
            {
                return BadRequest();
            }

            producto.IdEmpresa = long.Parse(idEmpresa);

            var idProducto = _productoService.Crear(producto);

            return Json(new
            {
                idProducto = idProducto
            });
        }

        // ==========================================
        // EDITAR PRODUCTO
        // ==========================================
        [HttpPost]
        public IActionResult Editar([FromBody] Producto producto)
        {
            var idEmpresa = HttpContext.Session.GetString("IdEmpresa");

            if (string.IsNullOrEmpty(idEmpresa))
            {
                return BadRequest();
            }

            producto.IdEmpresa = long.Parse(idEmpresa);

            _productoService.Editar(producto);

            return Ok();
        }
    }
}