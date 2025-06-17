using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCar.Data;
using RentCar.Models;
using SendEmail.Services;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace RentCar.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailService _emailService;

        public HomeController(ApplicationDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }



        //Vistas Generales 


        public IActionResult Index() => View();

        public IActionResult About() => View();

        public IActionResult Vehicles()
        {
            var vehiculos = _context.vehiculos.ToList();
            return View(vehiculos);
        }



        public IActionResult ConfirmacionReserva() => View();



        public IActionResult Contact() => View();

        public IActionResult Travels() => View();

        public IActionResult AlquilarVehiculo() => View();






        //EL GET DE RESERVA 

        [HttpGet]
        public async Task<IActionResult> DatosPersonales(int id)
        {
            var vehiculo = await _context.vehiculos.FirstOrDefaultAsync(v => v.Id == id);

            if (vehiculo == null)
            {
                
                TempData["ErrorMessage"] = "El vehículo solicitado no se encontró.";    
                return RedirectToAction("Vehicles");
            }

            var viewModel = new ReservaRequest
            {
                VehiculoId = vehiculo.Id,
                Vehiculo = vehiculo, 
                FechaRecogida = DateTime.Today,
                HoraRecogida = new TimeSpan(9, 0, 0),
                FechaEntrega = DateTime.Today.AddDays(1),
                HoraEntrega = new TimeSpan(9, 0, 0),
                FechaNacimiento = DateTime.Today.AddYears(-25),
                Tripulantes = 1
            };

            return View(viewModel);
        }





        //EL POSYT DE RESERVA 


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DatosPersonales(ReservaRequest model)
        {
            
            ModelState.Remove("Vehiculo");



            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Errores de validación:");
                foreach (var state in ModelState.Values)
                {
                    foreach (var error in state.Errors)
                    {
                        Debug.WriteLine(error.ErrorMessage);
                    }
                }
                
                return View(model);
            }

            try
            {
                model.FechaHoraRecogidaCompleta = model.FechaRecogida.Date + model.HoraRecogida;
                model.FechaHoraEntregaCompleta = model.FechaEntrega.Date + model.HoraEntrega;

                if (model.FechaHoraEntregaCompleta <= model.FechaHoraRecogidaCompleta)
                {
                    ModelState.AddModelError(string.Empty, "La fecha y hora de entrega deben ser posteriores a la de recogida.");
                    
                    return View(model);
                }
                

                _context.reservaRequests.Add(model);
                await _context.SaveChangesAsync();


                await _emailService.SendEmail(model);


                TempData["MensajeExito"] = "¡Tu reserva ha sido registrada con éxito!";
                return RedirectToAction("ConfirmacionReserva");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al guardar la reserva: {ex.Message}. Por favor, inténtalo de nuevo.");
                Debug.WriteLine($"Error al guardar la reserva: {ex}");
                
                return View(model);
            }
        }




        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}