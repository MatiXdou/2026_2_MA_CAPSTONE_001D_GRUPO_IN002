using Microsoft.AspNetCore.Mvc;
using PRV.Web.Models;
using PRV.Web.Services;

namespace PRV.Web.Controllers
{
    public class AdministracionController : Controller
    {
        private readonly UsuarioService _usuarioService;

        public AdministracionController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }


        // ==========================================
        // MOSTRAR LOGIN
        // ==========================================
        public IActionResult Index()
        {
            return View();
        }


        // ==========================================
        // INGRESAR
        // ==========================================
        [HttpPost]
        public IActionResult Ingresar([FromBody] Usuario usuario)
        {
            var usuarioValidado = _usuarioService.ValidarAccesoAdministracion(
                usuario.Email,
                usuario.PasswordHash ?? ""
            );

            if (usuarioValidado == null)
            {
                return Json(new { valido = false });
            }

            // Guardar datos del usuario en Session
            HttpContext.Session.SetString("IdUsuario", usuarioValidado.IdUsuario.ToString());
            HttpContext.Session.SetString("Nombre", usuarioValidado.Nombre);
            HttpContext.Session.SetString("Email", usuarioValidado.Email);
            HttpContext.Session.SetString("IdEmpresa", usuarioValidado.IdEmpresa?.ToString() ?? "");
            HttpContext.Session.SetString("IdRol", usuarioValidado.IdRol?.ToString() ?? "");

            return Json(new
            {
                valido = true,
                url = Url.Action("Index", "Home")
            });
        }

        // ==========================================
        // SALIR
        // ==========================================
        public IActionResult Salir()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Inicio");
        }
    }
}