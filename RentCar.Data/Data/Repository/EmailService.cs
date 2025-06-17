using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using RentCar.Models;
using System.Threading.Tasks;

namespace SendEmail.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmail(ReservaRequest reserva)
        {
            var emailSettings = _config.GetSection("Email");

            var correoDestino = reserva.EmailCliente;
            var asunto = "Confirmación de Reserva - Lucerna RentCar";

            var cuerpo = $@"
                <h2>¡Gracias por tu reserva, {reserva.Nombre} {reserva.Apellido}!</h2>
                <p><strong>Vehículo:</strong> {reserva.Vehiculo?.Marca} {reserva.Vehiculo?.Modelo}</p>
                <p><strong>Fecha de Recogida:</strong> {reserva.FechaRecogida:dd/MM/yyyy} a las {reserva.HoraRecogida}</p>
                <p><strong>Fecha de Entrega:</strong> {reserva.FechaEntrega:dd/MM/yyyy} a las {reserva.HoraEntrega}</p>
                <p><strong>Ubicación:</strong> {reserva.CiudadCodigo}</p>
                <br/>
                <p>Se le estara contactando para completar la reservacion</p>
                <p>Gracias por confiar en Lucerna RentCar.</p>
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
