namespace PRV.Web.Models
{
    public class Empresa
    {
        public long IdEmpresa { get; set; }
        public string Rut { get; set; } = string.Empty;
        public string RazonSocial { get; set; } = string.Empty;
        public string EmailCorporativo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaRegistro { get; set; }

        // Administrador de la empresa
        public string NombreAdministrador { get; set; }

        public string EmailAdministrador { get; set; }

    }

}

