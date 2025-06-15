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





        //Controladores de las vistas////////
        
        public IActionResult Index()
        {
            return View();
            
        }

        public IActionResult About()
        {
            return View();

        }

        public ActionResult Vehicles()
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

        public IActionResult DatosPersonales()
        {
            return View();

        }

        //Fin de los controladores de las vista 

























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
