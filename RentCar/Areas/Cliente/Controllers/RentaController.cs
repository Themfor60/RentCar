using Microsoft.AspNetCore.Mvc;
using RentCar.Data;
using RentCar.Models;
using SendEmail.Services;
using System;
using System.Threading.Tasks;

[Area("Cliente")]
public class RentaController : Controller
{
    private readonly IEmailService _emailService;
    private readonly WhatsAppService _whatsAppService;
    private readonly ApplicationDbContext _context;

    public RentaController(IEmailService emailService, WhatsAppService whatsAppService, ApplicationDbContext context)
    {
        _emailService = emailService;
        _whatsAppService = whatsAppService;
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> EnviarConfirmacion(ReservaRequest model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            
            _emailService.SendEmail(model);

            
            var reserva = new ReservaRequest
            {
                Nombre = model.Nombre,
                Apellido = model.Apellido,
                EmailCliente = model.EmailCliente,
                Telefono = model.Telefono,
                Cedula = model.Cedula,
                CiudadCodigo = model.CiudadCodigo,
                FechaRecogida = model.FechaRecogida,
                HoraRecogida = model.HoraRecogida,
                FechaEntrega = model.FechaEntrega,
                HoraEntrega = model.HoraEntrega,
                FechaNacimiento = model.FechaNacimiento,
                Tripulantes = model.Tripulantes,
                VehiculoId = model.VehiculoId
            };

            _context.reservaRequests.Add(reserva);
            await _context.SaveChangesAsync();

            
            await _whatsAppService.EnviarNotificacionReservaAsync(reserva);

            TempData["AlquilerConfirmado"] = "true";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ViewBag.Error = "Ocurrió un error al procesar la solicitud: " + ex.Message;
            return View("Error");
        }
    }

    public IActionResult Index()
    {
        return View();
    }
}

