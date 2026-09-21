using PRV.Web.Models;
using PRV.Web.Repositories;

namespace PRV.Web.Services
{
    public class ProductoService
    {
        private readonly ProductoRepository _productoRepository;

        public ProductoService(ProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }


        // ==========================================
        // LISTAR PRODUCTOS POR EMPRESA
        // ==========================================
        public List<Producto> ListarPorEmpresa(long idEmpresa)
        {
            return _productoRepository.ListarPorEmpresa(idEmpresa);
        }
    }
}