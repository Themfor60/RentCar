using Microsoft.AspNetCore.Mvc;
using RentCar.Data;
using RentCar.Models;
using System.Diagnostics;
using System.Linq;

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

        // Controladores de las vistas

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Vehicles()
        {
            var vehiculos = _context.vehiculos.ToList();
            return View(vehiculos);
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Travels()
        {
            return View();
        }

        public IActionResult AlquilarVehiculo()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DatosPersonales(int id)
        {
            var vehiculo = _context.vehiculos.FirstOrDefault(v => v.Id == id);
            if (vehiculo == null) return NotFound();

            var viewModel = new RentaFormularioViewModel
            {
                Vehiculo = vehiculo,
                Request = new ReservaRequest()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult DatosPersonales(RentaFormularioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Vehiculo = _context.vehiculos.FirstOrDefault(v => v.Id == model.Vehiculo.Id);
                return View(model);
            }

            var reserva = new ReservaRequest()
            {
                IdReserva = model.Vehiculo.Id,
                Nombre = model.Request.Nombre,
                Apellido = model.Request.Apellido,
                Telefono = model.Request.Telefono,
                EmailCliente = model.Request.EmailCliente
            };

            _context.reservaRequests.Add(reserva);
            _context.SaveChanges();

            return RedirectToAction("Vehicles");
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
