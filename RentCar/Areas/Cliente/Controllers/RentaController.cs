using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using RentCar.Models;

[Area("Cliente")]
public class RentaController : Controller
{
    [HttpPost]
    public IActionResult EnviarConfirmacion(RentaFormularioViewModel modelo)
    {
        try
        {
            var mensaje = new MimeMessage();
            mensaje.From.Add(new MailboxAddress("Alquiler de Vehículos", "tuntankamon01@gmail.com"));
            mensaje.To.Add(new MailboxAddress("", modelo.EmailDestino));
            mensaje.Subject = "Confirmación de solicitud de alquiler";

            mensaje.Body = new TextPart("plain")
            {
                Text = $@"
                ¡Hola!

                Gracias por usar nuestro servicio. Aquí están los detalles de tu solicitud:

                📍 Ciudad o Código Postal: {modelo.CiudadCodigo}
                🕓 Fecha y hora de recogida: {modelo.FechaRecogida:yyyy-MM-dd} a las {modelo.HoraRecogida}
                🕓 Fecha y hora de entrega: {modelo.FechaEntrega:yyyy-MM-dd} a las {modelo.HoraEntrega}
                👤 Fecha de nacimiento del conductor: {modelo.FechaNacimiento:yyyy-MM-dd}
                👥 Número de tripulantes: {modelo.Tripulantes}

                Nos pondremos en contacto contigo pronto.

                Saludos,
                Equipo de Rentas
                "
            };

            using var cliente = new SmtpClient();
            cliente.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            cliente.Authenticate("tuntankamon01@gmail.com", "tu_contraseña_o_token_de_app"); // Usa un token de app si es Gmail
            cliente.Send(mensaje);
            cliente.Disconnect(true);

            return View("Confirmacion"); // ✅ Siempre devuelve algo
        }
        catch (Exception ex)
        {
            // Log del error si quieres
            ViewBag.Error = "Hubo un problema al enviar el correo: " + ex.Message;
            return View("Error"); // ✅ También devuelve algo en caso de error
        }
    }
}
