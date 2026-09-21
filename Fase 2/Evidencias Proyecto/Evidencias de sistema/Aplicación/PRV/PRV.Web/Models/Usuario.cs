namespace PRV.Web.Models
{
    public class Usuario
    {
        public long IdUsuario { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Rut { get; set; }

        public string Telefono { get; set; }

        public string Direccion { get; set; }

        public string PasswordHash { get; set; }

        public string TipoCliente { get; set; }

        public bool Estado { get; set; } = true;

        public DateTime FechaRegistro { get; set; }

        public long? IdEmpresa { get; set; }

        public long? IdRol { get; set; }
    }
}