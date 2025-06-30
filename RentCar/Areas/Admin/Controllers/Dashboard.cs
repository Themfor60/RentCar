using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentCar.Data;
using RentCar.Data.Data.Repository.IRepository;
using RentCar.Models;

namespace RentCar.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,SuperUsuario")]

    [Area("Admin")]
    public class Dashboard : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public Dashboard(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        [HttpGet]
        public IActionResult CrearUsuario()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CrearUsuario(string email, string password, string rol)
        {
            var usuario = new IdentityUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };

            var resultado = await _userManager.CreateAsync(usuario, password);

            if (resultado.Succeeded)
            {
                
                await _userManager.AddToRoleAsync(usuario, rol);

                TempData["mensaje"] = "Usuario creado correctamente.";
                return RedirectToAction("Index");
            }

            foreach (var error in resultado.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View();
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
        [ValidateAntiForgeryToken]
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
