using PRV.Web.Models;
using PRV.Web.Repositories;

namespace PRV.Web.Services
{
    public class CompraService
    {
        private readonly CompraRepository _compraRepository;

        public CompraService(CompraRepository compraRepository)
        {
            _compraRepository = compraRepository;
        }


        // ==========================================
        // PAGAR COMPRA
        // ==========================================
        public void Pagar(Compra compra)
        {
            _compraRepository.Pagar(compra);
        }
    }
}