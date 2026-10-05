using Microsoft.AspNetCore.Mvc;
using PRV.Web.Services;

namespace PRV.Web.Controllers
{
    public class VentaController : Controller
    {
        private readonly VentaService _ventaService;

        public VentaController(VentaService ventaService)
        {
            _ventaService = ventaService;
        }


        // ==========================================
        // VISTA PRINCIPAL
        // ==========================================
        public IActionResult Index()
        {
            return View();
        }


        // ==========================================
        // CONSULTAR VENTAS
        // ==========================================
        [HttpGet]
        public IActionResult Consultar(
            DateTime fechaDesde,
            DateTime fechaHasta)
        {
            var idEmpresa = long.Parse(
                HttpContext.Session.GetString("IdEmpresa")!
            );

            var ventas = _ventaService.Consultar(
                idEmpresa,
                fechaDesde,
                fechaHasta
            );

            return Json(ventas);
        }


        // ==========================================
        // CONSULTAR DETALLE
        // ==========================================
        [HttpGet]
        public IActionResult Detalle(long idVenta)
        {
            var idEmpresa = long.Parse(
                HttpContext.Session.GetString("IdEmpresa")!
            );

            var detalle = _ventaService.ConsultarDetalle(
                idVenta,
                idEmpresa
            );

            return Json(detalle);
        }


        // ==========================================
        // ANULAR VENTA
        // ==========================================
        [HttpPost]
        public IActionResult Anular(long idVenta)
        {
            var idEmpresa = long.Parse(
                HttpContext.Session.GetString("IdEmpresa")!
            );

            try
            {
                _ventaService.Anular(
                    idVenta,
                    idEmpresa
                );

                return Json(new
                {
                    ok = true,
                    mensaje = "Venta anulada correctamente."
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