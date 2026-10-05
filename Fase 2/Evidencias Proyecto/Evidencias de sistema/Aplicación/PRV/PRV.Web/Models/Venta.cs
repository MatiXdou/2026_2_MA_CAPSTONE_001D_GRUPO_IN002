namespace PRV.Web.Models
{
    public class Venta
    {
        public long IdVenta { get; set; }

        public DateTime FechaVenta { get; set; }

        public decimal Total { get; set; }

        public string Estado { get; set; } = "";

        public string Cliente { get; set; } = "";
    }
}