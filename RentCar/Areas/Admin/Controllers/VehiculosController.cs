using Microsoft.AspNetCore.Mvc;
using RentCar.Data;
using RentCar.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Create(Vehiculo vehiculo, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await file.CopyToAsync(ms);
                        vehiculo.Foto = ms.ToArray();
                    }
                }
                else
                {
                   
                    vehiculo.Foto = null; 
                }

                _context.vehiculos.Add(vehiculo);
                await _context.SaveChangesAsync();

                return RedirectToAction("Vehicles", "Home", new { area = "Cliente" });
            }

            return View(vehiculo);
        }
    }
}
