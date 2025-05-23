using Microsoft.AspNetCore.Mvc;
using RentCar.Models;
using System.Diagnostics;

namespace RentCar.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
       //controlador del index
        public IActionResult Index()
        {
            return View();
        }



        //controlador del About
        public IActionResult About()
        {
            return View();
        }



        //controlador del Contact
        public IActionResult Contact()
        {
            return View();
        }



        //controlador de las secciones o catalogo de vehiculos
        public IActionResult Vehicles()
        {
            return View("~/Areas/Cliente/Views/Vehiculos/vehicles.cshtml");

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
