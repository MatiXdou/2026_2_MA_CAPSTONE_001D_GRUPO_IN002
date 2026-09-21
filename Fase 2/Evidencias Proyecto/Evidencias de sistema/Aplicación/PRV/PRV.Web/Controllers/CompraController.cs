using Microsoft.AspNetCore.Mvc;
using PRV.Web.Models;
using PRV.Web.Services;

namespace PRV.Web.Controllers
{
    public class CompraController : Controller
    {
        private readonly CompraService _compraService;
        private readonly ProductoService _productoService;

        public CompraController(
            CompraService compraService,
            ProductoService productoService)
        {
            _compraService = compraService;
            _productoService = productoService;
        }


        // ==========================================
        // COMPRA
        // ==========================================
        public IActionResult Index(long? idProducto)
        {
            var idEmpresa = HttpContext.Session.GetString("IdEmpresa");

            if (string.IsNullOrEmpty(idEmpresa))
            {
                return RedirectToAction("Index", "Inicio");
            }


            var productos = _productoService.ListarPorEmpresa(
                long.Parse(idEmpresa)
            );


            // Solo productos activos y con stock
            productos = productos
                .Where(p => p.Estado == "Activo" && p.Stock > 0)
                .ToList();


            ViewBag.IdProductoSeleccionado = idProducto;

            return View(productos);
        }


        // ==========================================
        // PAGAR COMPRA
        // ==========================================
        [HttpPost]
        public IActionResult Pagar([FromBody] Compra compra)
        {
            try
            {
                var idUsuario = HttpContext.Session.GetString("IdUsuario");

                if (string.IsNullOrEmpty(idUsuario))
                {
                    return Json(new
                    {
                        ok = false,
                        mensaje = "La sesión del usuario no es válida."
                    });
                }


                if (compra.Detalle == null || compra.Detalle.Count == 0)
                {
                    return Json(new
                    {
                        ok = false,
                        mensaje = "Debe agregar al menos un producto."
                    });
                }


                compra.IdUsuario = long.Parse(idUsuario);


                _compraService.Pagar(compra);


                return Json(new
                {
                    ok = true,
                    mensaje = "Compra realizada satisfactoriamente."
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    ok = false,
                    mensaje = ex.Message
                });
            }
        }
    }
}