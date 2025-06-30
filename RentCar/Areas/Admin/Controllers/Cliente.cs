using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCar.Data;
using RentCar.Data.Data.Repository.IRepository;
using RentCar.Models;

namespace RentCar.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperUsuario")]
    public class Cliente : Controller
    {
        private readonly ApplicationDbContext _context;

        public Cliente(ApplicationDbContext context)
        {
            _context = context;

        }

        //controlador en listar  los cliente del dashboa

        public async Task<IActionResult> Clientes() 
        {
            
            var reservas = await _context.reservaRequests
                                         .Include(r => r.Vehiculo) 
                                         .ToListAsync();
            return View(reservas);
        }



        //controlador para boorar los cliente dell dashboar 
        //[Authorize(Roles = "SuperUsuario")]
        [HttpGet]
        public async Task<IActionResult> BorrarCliente(int id)
        {
            var cliente = await _context.reservaRequests
                                        .Include(r => r.Vehiculo)
                                        .FirstOrDefaultAsync(r => r.IdReserva == id);

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente); 
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("BorrarCliente")]
        public async Task<IActionResult> BorrarConfirmado(int id)
        {
            var cliente = await _context.reservaRequests.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            _context.reservaRequests.Remove(cliente);
            await _context.SaveChangesAsync();

            return RedirectToAction("Clientes");
        }


        //controlador para editar los cliente dell dashboar 
        //[Authorize(Roles = "SuperUsuario")]
        [HttpGet]
        public async Task<IActionResult> EditarCliente(int id)
        {
            var cliente = await _context.reservaRequests
                                        .Include(r => r.Vehiculo)
                                        .FirstOrDefaultAsync(r => r.IdReserva == id);

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }


        //[Authorize(Roles = "SuperUsuario")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarCliente(ReservaRequest reservaRequest)
        {
            if (ModelState.IsValid)
            {
                _context.reservaRequests.Update(reservaRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction("Clientes");
            }



            return View("EditarCliente");
        }


        //Control para ver el detalle
        public async Task<IActionResult> DetalleCliente(int id)
        {
            var cliente = await _context.reservaRequests
                                        .Include(r => r.Vehiculo)
                                        .FirstOrDefaultAsync(r => r.IdReserva == id);

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }



    }




}