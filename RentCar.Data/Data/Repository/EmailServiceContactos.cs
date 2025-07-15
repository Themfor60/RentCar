using RentCar.Models;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;


namespace RentCar.Data.Data.Repository
{
    public class EmailServiceContactos
    {

        private readonly IConfiguration _config;

        public EmailServiceContactos(IConfiguration config)
        {
            _config = config;
        }




        public async Task SendEmailContacto(MensajeDeContactoModels MensajeDeContacto)
        {
            var emailSettings = _config.GetSection("Email");

            var correoDestino = emailSettings["DestinoContacto"]; 
            var asunto = $"Nuevo mensaje de contacto: {MensajeDeContacto.Asunto}";

            var cuerpo = $@"
        <h2>Mensaje de Contacto</h2>
        <p><strong>Nombre:</strong> {MensajeDeContacto.Nombre}</p>
        <p><strong>Asunto:</strong> {MensajeDeContacto.Asunto}</p>
        <p><strong>Mensaje:</strong><br/>{MensajeDeContacto.Mensaje}</p>
    ";

            var smtpClient = new SmtpClient(emailSettings["Host"])
            {
                Port = int.Parse(emailSettings["Port"]),
                Credentials = new NetworkCredential(emailSettings["UserName"], emailSettings["PassWord"]),
                EnableSsl = true,
            };

            var mensaje = new MailMessage
            {
                From = new MailAddress(emailSettings["UserName"], "RentCar"),
                Subject = asunto,
                Body = cuerpo,
                IsBodyHtml = true,
            };

            mensaje.To.Add(correoDestino); 

            await smtpClient.SendMailAsync(mensaje);
        }

    }
}
