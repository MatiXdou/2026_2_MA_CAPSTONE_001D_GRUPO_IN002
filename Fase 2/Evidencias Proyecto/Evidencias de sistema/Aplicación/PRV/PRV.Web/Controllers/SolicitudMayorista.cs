using Microsoft.AspNetCore.Mvc;
using PRV.Web.Models;
using PRV.Web.Services;

namespace PRV.Web.Controllers
{
    public class SolicitudMayorista : Controller
    {
        private readonly UsuarioService _usuarioService;
        private readonly CorreoService _correoService;

        public SolicitudMayorista(
            UsuarioService usuarioService,
            CorreoService correoService)
        {
            _usuarioService = usuarioService;
            _correoService = correoService;
        }


        // ==========================================
        // LISTAR SOLICITUDES MAYORISTAS
        // ==========================================
        public IActionResult Index()
        {
            var idEmpresa = HttpContext.Session.GetString("IdEmpresa");

            if (string.IsNullOrEmpty(idEmpresa))
            {
                return RedirectToAction("Index", "Administracion");
            }

            var usuarios = _usuarioService.ListarSolicitantes(
                long.Parse(idEmpresa)
            );

            return View(usuarios);
        }


        // ==========================================
        // APROBAR SOLICITUD MAYORISTA
        // ==========================================
        [HttpPost]
        public IActionResult Aprobar([FromBody] Usuario usuario)
        {
            var idEmpresa = long.Parse(
                HttpContext.Session.GetString("IdEmpresa")!
            );

            // Buscar datos del solicitante
            var solicitante = _usuarioService.BuscarPorId(
                usuario.IdUsuario,
                idEmpresa
            );

            if (solicitante == null)
            {
                return Json(new
                {
                    aprobado = false
                });
            }


            // Generar clave temporal de 4 dígitos
            //string claveTemporal = Random.Shared
            //    .Next(1000, 10000)
            //    .ToString();

            //TODO clave temporal
            string claveTemporal = "demo";

            // Aprobar mayorista y guardar clave temporal
            _usuarioService.AprobarMayorista(
                usuario.IdUsuario,
                idEmpresa,
                claveTemporal
            );


            // Enviar correo al mayorista
            try
            {
                _correoService.Enviar(
                    solicitante.Email,
                    "Solicitud mayorista aprobada",
                    "Estimado/a " + solicitante.Nombre + ":\n\n" +
                    "Su solicitud para acceder como cliente mayorista ha sido aprobada.\n\n" +
                    "Ya puede ingresar a PRV utilizando su correo registrado.\n\n" +
                    "Su clave temporal es: " + claveTemporal + "\n\n" +
                    "Por seguridad, debe cambiar esta clave después de ingresar al sistema.\n\n" +
                    "Saludos,\n" +
                    "PRV - Planificador de Recursos de Ventas"
                );
            }
            catch (Exception)
            {
                // Si falla el correo, la aprobación continúa normalmente.
            }

            return Json(new
            {
                aprobado = true
            });
        }
    }
}