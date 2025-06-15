using RentCar.Models;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Formats.Asn1.AsnWriter;

namespace SendEmail.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void SendEmail(RentaFormularioViewModel request)
        {
            if (string.IsNullOrWhiteSpace(request.EmailDestino))
                throw new ArgumentException("No se proporcionó un correo electrónico de destino.");

            var email = new MimeMessage();

            // Dirección "From" configurada en appsettings.json
            email.From.Add(MailboxAddress.Parse(_config.GetSection("Email:UserName").Value));

            // Dirección "To" desde el modelo
            email.To.Add(MailboxAddress.Parse(request.EmailDestino));

            email.Subject = "Confirmación de solicitud de alquiler";

            // Construimos el cuerpo con formato HTML para que se vea mejor (puedes usar Text si prefieres texto plano)
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = $@"
                <div class=""bg-light"">
                  <div class=""container my-5"">
                    <div class=""card shadow-lg"">
                      <div class=""card-body"">
                        <h2 class=""card-title text-center text-success mb-4"">Confirmación de Reserva</h2>

                        <p>Estimado/a <strong>{request/*.Nombre*/}</strong>,</p>
                        <p>Gracias por reservar con <strong>RentCar</strong>. Aquí tienes los detalles de tu reserva:</p>

                        <table class=""table table-bordered mt-4"">
                          <tbody>
                            <tr>
                              <th scope=""row"">Vehículo</th>
                              <td>{request/*.Vehiculo*/}</td>
                            </tr>
                            <tr>
                              <th scope=""row"">Fecha de recogida</th>
                              <td><strong>{request.FechaRecogida:yyyy-MM-dd} a las {request.HoraRecogida}</strong></td>
                            </tr>
                            <tr>
                              <th scope=""row"">Fecha de entrega</th>
                              <td><strong>{request.FechaEntrega:yyyy-MM-dd} a las {request.HoraEntrega}</strong></td>
                            </tr>
                            <tr>
                              <th scope=""row"">Dirección de entrega</th>
                              <td><strong>{request.CiudadCodigo}</strong></td>
                            </tr>
                          </tbody>
                        </table>

                        <p>Si necesitas realizar algún cambio o tienes preguntas, no dudes en contactarnos.</p>

                        <p class=""mb-0"">Gracias por elegirnos,</p>
                        <p class=""fw-bold"">Lucerna RentCar</p>
                      </div>

                      <div class=""card-footer text-center text-muted small"">
                        © 2025 RentCar. Tel: (809) 555-1234 | soporte@Lucernarentcar.com
                      </div>
                    </div>
                  </div>
                </div>"
            };

            using var smtp = new SmtpClient();

            smtp.Connect(
                _config.GetSection("Email:Host").Value,
                Convert.ToInt32(_config.GetSection("Email:Port").Value),
                SecureSocketOptions.StartTls
            );

            smtp.Authenticate(
                _config.GetSection("Email:UserName").Value,
                _config.GetSection("Email:PassWord").Value
            );

            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}

