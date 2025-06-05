using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCar.Data;
using RentCar.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace RentCar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VehiculosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VehiculosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Vehiculo vehiculo, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        vehiculo.Foto = ms.ToArray(); 
                    }
                }

                _context.vehiculos.Add(vehiculo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vehiculo);
        }
    }
}
