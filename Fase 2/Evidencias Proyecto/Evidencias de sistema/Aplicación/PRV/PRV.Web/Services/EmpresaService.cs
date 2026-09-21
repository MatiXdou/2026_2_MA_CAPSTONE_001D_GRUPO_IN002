using PRV.Web.Models;
using PRV.Web.Repositories;

namespace PRV.Web.Services
{
    public class EmpresaService
    {
        private readonly EmpresaRepository _empresaRepository;

        public EmpresaService(EmpresaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }


        // ==========================================
        // LISTAR EMPRESAS
        // ==========================================
        public List<Empresa> Listar()
        {
            return _empresaRepository.Listar();
        }


        // ==========================================
        // CREAR EMPRESA
        // ==========================================
        public void Crear(Empresa empresa)
        {
            _empresaRepository.Crear(empresa);
        }


        // ==========================================
        // EDITAR EMPRESA
        // ==========================================
        public void Editar(Empresa empresa)
        {
            _empresaRepository.Editar(empresa);
        }

        // ==========================================
        // ELIMINAR EMPRESA
        // ==========================================
        public void Eliminar(long idEmpresa)
        {
            _empresaRepository.Eliminar(idEmpresa);
        }


    }
}