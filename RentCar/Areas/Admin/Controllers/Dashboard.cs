using Microsoft.AspNetCore.Mvc;
using RentCar.Data;
using RentCar.Data.Data.Repository.IRepository;
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

            
            var vehiculos = _context.vehiculos.ToList();
            return View("Dashboard", vehiculos);
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var vehiculo = _context.vehiculos.FirstOrDefault(v => v.Id == id);

            if (vehiculo == null)
            {
                return NotFound();
            }

            return View(vehiculo);
        }



        [HttpPost]
        public IActionResult Editar(Vehiculo vehiculo)
        {
            if (ModelState.IsValid)
            {
                _context.vehiculos.Update(vehiculo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vehiculo);
        }



        [HttpGet]
        public IActionResult Borrar(int id)
        {
            var vehiculo = _context.vehiculos.FirstOrDefault(v => v.Id == id);

            if (vehiculo == null)
            {
                return NotFound();
            }

            return View(vehiculo);
        }


        [HttpPost]
        [ActionName("Borrar")]
        public IActionResult BorrarConfirmado(int id)
        {
            var vehiculo = _context.vehiculos.FirstOrDefault(v => v.Id == id);

            if (vehiculo == null)
            {
                return NotFound();
            }

            _context.vehiculos.Remove(vehiculo);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }





    }
}
