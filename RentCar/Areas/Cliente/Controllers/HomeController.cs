using Microsoft.AspNetCore.Mvc;
using RentCar.Data;
using RentCar.Models;
using System.Diagnostics;

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

        public IActionResult Index()
        {
            // Simulación de lista de vehículos (esto lo puedes cambiar para cargar desde base de datos)
            var listaVehiculos = new List<Vehicle>
            {
                new Vehicle { Marca = "Toyota", Modelo = "Corolla", Precio = 1000 },
                new Vehicle { Marca = "Honda", Modelo = "Civic", Precio = 1200 }
            };

            return View(listaVehiculos);
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
