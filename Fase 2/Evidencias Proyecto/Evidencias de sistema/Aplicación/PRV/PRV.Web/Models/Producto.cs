namespace PRV.Web.Models
{
    public class Producto
    {
        public long IdProducto { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public decimal PrecioVenta { get; set; }

        public decimal PrecioMayorista { get; set; }

        public int Stock { get; set; }

        public string Estado { get; set; } = string.Empty;

        public DateTime FechaRegistro { get; set; }

        public long IdEmpresa { get; set; }
    }
}