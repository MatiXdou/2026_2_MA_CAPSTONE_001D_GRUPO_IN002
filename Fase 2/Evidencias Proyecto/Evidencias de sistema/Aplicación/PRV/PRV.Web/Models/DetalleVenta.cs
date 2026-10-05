namespace PRV.Web.Models
{
    public class DetalleVenta
    {
        public long IdDetalleVenta { get; set; }

        public long IdProducto { get; set; }

        public string Producto { get; set; } = "";

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public decimal Subtotal { get; set; }
    }
}