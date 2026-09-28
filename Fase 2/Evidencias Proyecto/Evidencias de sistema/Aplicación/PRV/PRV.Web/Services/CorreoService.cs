using System.Net;
using System.Net.Mail;

namespace PRV.Web.Services
{
    public class CorreoService
    {
        private readonly IConfiguration _configuration;

        public CorreoService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

     
        public void Enviar(
            string destinatario,
            string asunto,
            string mensaje)
        {
            var smtp = _configuration["Correo:Smtp"];
            var puerto = int.Parse(_configuration["Correo:Puerto"]!);
            var usuario = _configuration["Correo:Usuario"];
            var password = _configuration["Correo:Password"];


            using var cliente = new SmtpClient(smtp, puerto);

            cliente.Credentials =
                new NetworkCredential(usuario, password);

            cliente.EnableSsl = true;


            using var correo = new MailMessage();
            correo.From = new MailAddress(usuario!, "PRV - Clave temporal");
            correo.To.Add(destinatario);
            correo.Subject = asunto;
            correo.Body = mensaje;

            cliente.Send(correo);
        }
    }
}