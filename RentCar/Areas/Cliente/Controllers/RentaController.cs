using Microsoft.AspNetCore.Mvc;
using RentCar.Models;
using SendEmail.Services;
using System.Threading.Tasks;

[Area("Cliente")]
public class RentaController : Controller
{
    private readonly IEmailService _emailService;
    private readonly WhatsAppService _whatsAppService;

    public RentaController(IEmailService emailService, WhatsAppService whatsAppService)
    {
        _emailService = emailService;
        _whatsAppService = whatsAppService;
    }

    [HttpPost]
    public async Task<IActionResult> EnviarConfirmacion(RentaFormularioViewModel request)
    {
        try
        {
            
             _emailService.SendEmail(request);

            
            var reserva = new ReservaRequest
            {
                Nombre = "Nombre que tengas o pasa desde request si lo tienes",
                EmailCliente = request.EmailDestino,
                
            };

            await _whatsAppService.EnviarNotificacionReservaAsync(reserva);

            TempData["AlquilerConfirmado"] = "true";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ViewBag.Error = "Ocurrió un error al enviar el correo o WhatsApp: " + ex.Message;
            return View("Error");
        }
    }
}
