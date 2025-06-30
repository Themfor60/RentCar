using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCar.Data;
using RentCar.Models;
using System.Linq;

namespace RentCar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FinanzasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FinanzasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Finanzas()
        {
            var reservas = _context.reservaRequests
                .Include(r => r.Vehiculo)
                .ToList();

            var resumen = reservas
                .Where(r => r.FechaHoraEntregaCompleta != null && r.Vehiculo != null)
                .GroupBy(r => r.FechaHoraEntregaCompleta.Date)
                .Select(g => new FinanzasResumen
                {
                    Fecha = g.Key.ToString("yyyy-MM-dd"),
                    Ingresos = g.Sum(r => r.Vehiculo.Precio),
                    Alquileres = g.Count()
                })
                .OrderBy(d => d.Fecha)
                .ToList();

            ViewBag.Datos = resumen;
            return View(reservas);
        }
    }
}
