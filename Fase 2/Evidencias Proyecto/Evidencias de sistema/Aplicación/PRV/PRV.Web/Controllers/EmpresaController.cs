using Microsoft.AspNetCore.Mvc;
using PRV.Web.Models;
using PRV.Web.Services;

namespace PRV.Web.Controllers
{
    public class EmpresaController : Controller
    {
        private readonly EmpresaService _empresaService;

        public EmpresaController(EmpresaService empresaService)
        {
            _empresaService = empresaService;
        }


        // ==========================================
        // LISTADO DE EMPRESAS
        // ==========================================
        public IActionResult Index()
        {
            var empresas = _empresaService.Listar();

            return View(empresas);
        }


        // ==========================================
        // MODIFICAR EMPRESA
        // ==========================================
        [HttpPost]
        public IActionResult Modificar([FromBody] Empresa empresa)
        {
            if (empresa == null)
            {
                return BadRequest();
            }

            _empresaService.Editar(empresa);

            return Ok();
        }

        // ==========================================
        // CREAR EMPRESA
        // ==========================================
        [HttpPost]
        public IActionResult Crear([FromBody] Empresa empresa)
        {
            if (empresa == null)
            {
                return BadRequest();
            }

            _empresaService.Crear(empresa);

            return Ok();
        }

        // ==========================================
        // ELIMINAR EMPRESA
        // ==========================================
        [HttpPost]
        public IActionResult Eliminar([FromBody] long idEmpresa)
        {
            _empresaService.Eliminar(idEmpresa);

            return Ok();
        }

    }
}