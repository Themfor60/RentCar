using Microsoft.AspNetCore.Mvc;
using RentCar.Data;
using RentCar.Models;

namespace RentCar.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class Dashboard : Controller
    {
        private readonly ApplicationDbContext _context;

        public Dashboard(ApplicationDbContext context)
        {
            _context = context;

        }

        public IActionResult Index()
        {
            var vehiculos = _context.vehiculos.ToList();
            return View(vehiculos);
        }

        [HttpPost]
        public IActionResult AgregarVehiculo(Vehiculo vehiculo)
        {
            if (ModelState.IsValid)
            {
                _context.vehiculos.Add(vehiculo);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }

            // Si algo falla, recarga la vista con errores
            var vehiculos = _context.vehiculos.ToList();
            return View("Dashboard", vehiculos);
        }
    }
}
