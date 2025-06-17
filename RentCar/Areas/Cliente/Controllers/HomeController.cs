using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCar.Data;
using RentCar.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RentCar.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

       
        public IActionResult Index() => View();

        public IActionResult About() => View();

        public IActionResult Vehicles()
        {
            var vehiculos = _context.vehiculos.ToList();
            return View(vehiculos);
        }

        public IActionResult Contact() => View();

        public IActionResult Travels() => View();

        public IActionResult AlquilarVehiculo() => View();



        [HttpGet]
        public async Task<IActionResult> DatosPersonales(int id)
        {
            var vehiculo = await _context.vehiculos.FirstOrDefaultAsync(v => v.Id == id);
            if (vehiculo == null)
                return NotFound();


            var viewModel = new ReservaRequest
            {
                VehiculoId = vehiculo.Id,
                FechaRecogida = DateTime.Today,
                HoraRecogida = new TimeSpan(9, 0, 0),
                FechaEntrega = DateTime.Today.AddDays(1),
                HoraEntrega = new TimeSpan(9, 0, 0),
                FechaNacimiento = DateTime.Today.AddYears(-25),
                Tripulantes = 1
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DatosPersonales(ReservaRequest model)
        {
           
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
                await LoadVehiculoDataForView(model.VehiculoId.GetValueOrDefault());
                return View(model);
            }

            try
            {
               
                model.FechaHoraRecogidaCompleta = model.FechaRecogida.Date + model.HoraRecogida;
                model.FechaHoraEntregaCompleta = model.FechaEntrega.Date + model.HoraEntrega;

               
                if (model.FechaHoraEntregaCompleta <= model.FechaHoraRecogidaCompleta)
                {
                    ModelState.AddModelError(string.Empty, "La fecha y hora de entrega deben ser posteriores a la de recogida.");
                    await LoadVehiculoDataForView(model.VehiculoId.GetValueOrDefault());
                    return View(model);
                }

               
                _context.reservaRequests.Add(model); 
                
                await _context.SaveChangesAsync();
               
                TempData["MensajeExito"] = "¡Tu reserva ha sido registrada con éxito!";
                return RedirectToAction("ConfirmacionReserva");
            }
            catch (Exception ex)
            {
                
                ModelState.AddModelError(string.Empty, $"Error al guardar la reserva: {ex.Message}. Por favor, inténtalo de nuevo.");
                Debug.WriteLine($"Error al guardar la reserva: {ex}"); 
                
                await LoadVehiculoDataForView(model.VehiculoId.GetValueOrDefault());
                return View(model);
            }
        }

        private async Task LoadVehiculoDataForView(int vehiculoId)
        {
            var vehiculo = await _context.vehiculos.FirstOrDefaultAsync(v => v.Id == vehiculoId);
            if (vehiculo != null)
            {
                ViewData["VehiculoMarca"] = vehiculo.Marca;
                ViewData["VehiculoModelo"] = vehiculo.Modelo;
                ViewData["VehiculoFoto"] = vehiculo.Foto != null ? Convert.ToBase64String(vehiculo.Foto) : null;
                ViewData["Transmision"] = vehiculo.Transmision;
                ViewData["CapacidadPersonas"] = vehiculo.CapacidadPersonas;
                ViewData["CapacidadMaletero"] = vehiculo.CapacidadMaletero;
            }
        }

        



      public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
             }     }
  
}
