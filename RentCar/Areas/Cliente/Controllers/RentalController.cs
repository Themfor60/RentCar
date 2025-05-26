using Microsoft.AspNetCore.Mvc;
using RentCar.Data.Data.Repository.IRepository;
using RentCar.Models;

public class VehiculosController : Controller
{
    private readonly IEmailService _emailService;

    public VehiculosController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost]
    public async Task<IActionResult> Alquilar(AlquilerViewModel model)
    {
        if (!ModelState.IsValid)
            return RedirectToAction("Index");

        decimal total = model.PrecioPorDia * model.Dias;

        await _emailService.SendRentalConfirmationEmailAsync(
            toEmail: model.Correo,
            userName: "Cliente", 
            vehicleModel: model.VehicleModel,
            rentalDays: model.Dias,
            totalPrice: total,
            userAddress: model.Direccion
        );

        TempData["Mensaje"] = "¡Correo enviado con éxito!";
        return RedirectToAction("Index");
    }
}
