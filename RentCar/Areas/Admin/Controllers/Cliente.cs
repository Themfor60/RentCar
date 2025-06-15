using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Clientes()
        {
            var Clientes = _context.reservaRequests.ToList();
            return View(Clientes);
        }
    }
}