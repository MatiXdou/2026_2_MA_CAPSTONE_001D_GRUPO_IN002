
using Microsoft.AspNetCore.Mvc;
using PRV.Web.Services;

namespace PRV.Web.Controllers
{
    public class CuentaController : Controller
    {
        private readonly UsuarioService _usuarioService;

        public CuentaController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("IdUsuario")))
            {
                return RedirectToAction("Index", "Inicio");
            }

            return View();
        }

        [HttpPost]
        public IActionResult CambiarClave(
            string claveActual,
            string claveNueva,
            string confirmarClave)
        {
            var idSesion = HttpContext.Session.GetString("IdUsuario");

            if (!long.TryParse(idSesion, out long idUsuario))
                return Unauthorized();

            if (string.IsNullOrWhiteSpace(claveActual) ||
                string.IsNullOrWhiteSpace(claveNueva) ||
                claveNueva != confirmarClave ||
                claveActual == claveNueva ||
                claveNueva.Length > 255)
            {
                return Json(new
                {
                    correcto = false,
                    mensaje = "Verifique las claves ingresadas."
                });
            }

            var usuario = _usuarioService.BuscarPorIdUsuario(idUsuario);

            if (usuario == null || !usuario.Estado)
            {
                return Json(new
                {
                    correcto = false,
                    mensaje = "Usuario no encontrado o inactivo."
                });
            }

            if (usuario.PasswordHash != claveActual)
            {
                return Json(new
                {
                    correcto = false,
                    mensaje = "La clave actual es incorrecta."
                });
            }

            int resultado = _usuarioService.CambiarClave(
                idUsuario, claveNueva);

            return Json(new
            {
                correcto = resultado == 1,
                mensaje = resultado == 1
                    ? "Clave cambiada correctamente."
                    : "No se pudo cambiar la clave."
            });
        }
    }
}
