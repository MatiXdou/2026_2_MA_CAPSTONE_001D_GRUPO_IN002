using Microsoft.AspNetCore.Mvc;
using PRV.Web.Models;
using PRV.Web.Services;

namespace PRV.Web.Controllers
{
    public class InicioController : Controller
    {
        private readonly EmpresaService _empresaService;
        private readonly UsuarioService _usuarioService;

        public InicioController(EmpresaService empresaService, UsuarioService usuarioService)
        {
            _empresaService = empresaService;
            _usuarioService = usuarioService;
        }

        public IActionResult Index()
        {
            //filtra solo empresa activa
            var empresas = _empresaService.Listar().Where(e => e.Estado).ToList();

            // Lista todas las empresas
            //var empresas = _empresaService.Listar();


            return View(empresas);
        }

        public IActionResult Ingresar(long idEmpresa)
        {

            var empresa = _empresaService.Listar().FirstOrDefault(e => e.IdEmpresa == idEmpresa);

            if (empresa == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.IdEmpresa = empresa.IdEmpresa;
            ViewBag.RazonSocial = empresa.RazonSocial;

            return View();
        }


        [HttpPost]
        public IActionResult BuscarUsuario([FromBody] Usuario usuario)
        {

            var usuarioEncontrado = _usuarioService.BuscarPorEmailEmpresa(usuario.Email, usuario.IdEmpresa!.Value);

            if (usuarioEncontrado == null)
            {
                return Json(new { existe = false });
            }

            return Json(new
            {
                existe = true,
                nombre = usuarioEncontrado.Nombre,
                tipoCliente = usuarioEncontrado.TipoCliente
            });
        }

        [HttpPost]
        public IActionResult CrearUsuario([FromBody] Usuario usuario)
        {
            if (usuario.TipoCliente == "Solicitante")
            {
                usuario.TipoCliente = "Solicitante";
            }
            else
            {
                usuario.TipoCliente = "Comun";
            }

            usuario.Estado = true;
            usuario.IdRol = null;

            _usuarioService.Crear(usuario);

            return Json(new
            {
                creado = true,
                url = Url.Action(
                "Index",
                "Catalogo",
                new { idEmpresa = usuario.IdEmpresa }
            )

            });
        }

        [HttpPost]
        public IActionResult ValidarClave([FromBody] Usuario usuario)
        {
            if (usuario.IdEmpresa == null)
            {
                return Json(new { valido = false });
            }

            var usuarioValidado = _usuarioService.ValidarClave(
                usuario.Email,
                usuario.IdEmpresa.Value,
                usuario.PasswordHash ?? ""
            );

            if (usuarioValidado == null)
            {
                return Json(new { valido = false });
            }

            // Guardar datos del usuario en Session
            GuardarSesion(usuarioValidado);


            // Definir página de destino
            var url = usuarioValidado.TipoCliente == "Mayorista"
                ? Url.Action("Index", "HomeMayorista")
                : Url.Action("Index", "Home");


            return Json(new
            {
                valido = true,
                nombre = usuarioValidado.Nombre,
                url = url

            });
        }


        private void GuardarSesion(Usuario usuario)
        {
            HttpContext.Session.SetString("Nombre",usuario.Nombre);
            HttpContext.Session.SetString("Email",usuario.Email);
            HttpContext.Session.SetString("TipoCliente",usuario.TipoCliente ?? "");
            HttpContext.Session.SetString("IdEmpresa",usuario.IdEmpresa.ToString() ?? "");
            HttpContext.Session.SetString("IdRol",usuario.IdRol?.ToString() ?? "");

        }



    }
}