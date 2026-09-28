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
        // Catálogo y compra
        // ==========================================
        public List<Producto> ListarPorEmpresa(long idEmpresa)
        {
            return _productoRepository.ListarPorEmpresa(idEmpresa);
        }


        // ==========================================
        // LISTAR PRODUCTOS PARA MANTENEDOR
        // ==========================================
        public List<Producto> ListarMantenedor(long idEmpresa)
        {
            return _productoRepository.ListarMantenedor(idEmpresa);
        }

        // ==========================================
        // CREAR PRODUCTO
        // ==========================================
        public long Crear(Producto producto)
        {
            return _productoRepository.Crear(producto);
        }

        // ==========================================
        // EDITAR PRODUCTO
        // ==========================================
        public void Editar(Producto producto)
        {
            _productoRepository.Editar(producto);
        }



    }
}