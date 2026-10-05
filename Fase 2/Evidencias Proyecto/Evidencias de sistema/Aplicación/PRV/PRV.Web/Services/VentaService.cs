using PRV.Web.Models;
using PRV.Web.Repositories;

namespace PRV.Web.Services
{
    public class VentaService
    {
        private readonly VentaRepository _ventaRepository;

        public VentaService(VentaRepository ventaRepository)
        {
            _ventaRepository = ventaRepository;
        }


        // ==========================================
        // CONSULTAR VENTAS
        // ==========================================
        public List<Venta> Consultar(
            long idEmpresa,
            DateTime fechaDesde,
            DateTime fechaHasta)
        {
            return _ventaRepository.Consultar(
                idEmpresa,
                fechaDesde,
                fechaHasta);
        }


        // ==========================================
        // CONSULTAR DETALLE
        // ==========================================
        public List<DetalleVenta> ConsultarDetalle(
            long idVenta,
            long idEmpresa)
        {
            return _ventaRepository.ConsultarDetalle(
                idVenta,
                idEmpresa);
        }


        // ==========================================
        // ANULAR VENTA
        // ==========================================
        public void Anular(long idVenta, long idEmpresa)
        {
            _ventaRepository.Anular(idVenta, idEmpresa);
        }
    }
}