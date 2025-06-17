using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCar.Data;
using RentCar.Data.Data.Repository.IRepository;
using RentCar.Models;

namespace RentCar.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class Cliente : Controller
    {
        private readonly ApplicationDbContext _context;

        public Cliente(ApplicationDbContext context)
        {
            _context = context;

        }

        public async Task<IActionResult> Clientes() 
        {
            
            var reservas = await _context.reservaRequests
                                         .Include(r => r.Vehiculo) 
                                         .ToListAsync();
            return View(reservas);
        }
    }
}