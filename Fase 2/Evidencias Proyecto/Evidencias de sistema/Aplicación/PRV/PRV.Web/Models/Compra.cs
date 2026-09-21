namespace PRV.Web.Models
{
    public class Compra
    {
        public long IdUsuario { get; set; }

        public List<DetalleCompra> Detalle { get; set; } = new List<DetalleCompra>();
    }
}