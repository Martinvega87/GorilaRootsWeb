using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservaEspectaculo.Data;
using ReservaEspectaculo.Models;

namespace ReservaEspectaculo.Controllers
{
    public class ReservasController : Controller
    {
        private readonly Context _context;

        public ReservasController(Context context)
        {
            _context = context;
        }

        // GET: Reservas
        public async Task<IActionResult> Index()
        {
            var context = _context.Reservas.Include(r => r.Cliente).Include(r => r.Funcion).Include(r => r.Funcion.Pelicula);
            return View(await context.ToListAsync());
        }

        // GET: Reservas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Funcion)
                .Include(r => r.Funcion.Pelicula)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // GET: Reservas/Create
        public IActionResult Create(Guid funcId)
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "NombreApellido");
            var reserva = new Reserva() { FuncionId = funcId };
            return View(reserva);
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FuncionId,ClienteId,CantidadButacas,FechaAlta")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {


                var funcion = _context.Funciones.Include(f => f.Sala).Include(f => f.Reservas).FirstOrDefault(f => f.Id == reserva.FuncionId);
                if (Helpers.ValidarButacas(reserva.CantidadButacas, funcion))
                {
                    reserva.Id = Guid.NewGuid();
                    if (User.IsInRole("Cliente"))
                    {
                        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                        reserva.ClienteId = Guid.Parse(userId);
                    }
                    var cliente = _context.Clientes.Include(c => c.Reservas).ThenInclude(r => r.Funcion).FirstOrDefault(c => c.Id == reserva.ClienteId);
                    if (!cliente.TieneReservaActiva)
                    {
                        _context.Add(reserva);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "NombreApellido", reserva.ClienteId);
                        ViewData["FuncionId"] = new SelectList(_context.Funciones, "Id", "Descripcion", reserva.FuncionId);
                        ModelState.AddModelError("CantidadButacas", ErrMsgs.TieneReserva);
                    }
                }
                else
                {
                    ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "NombreApellido", reserva.ClienteId);
                    ViewData["FuncionId"] = new SelectList(_context.Funciones, "Id", "Descripcion", reserva.FuncionId);
                    ModelState.AddModelError("CantidadButacas", ErrMsgs.TieneReserva);
                }
            }

            return View(reserva);
        }

        // GET: Reservas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "NombreApellido", reserva.ClienteId);
            ViewData["FuncionId"] = new SelectList(_context.Funciones, "Id", "Descripcion", reserva.FuncionId);
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,FuncionId,ClienteId,CantidadButacas,FechaAlta")] Reserva reserva)
        {
            if (id != reserva.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var funcion = await _context.Funciones.AsNoTracking().Include(f => f.Sala).Include(f => f.Reservas).FirstOrDefaultAsync(f => f.Id == reserva.FuncionId);
                if (Helpers.ValidarButacasEdit(reserva, funcion))
                {
                    try
                    {
                        _context.Update(reserva);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ReservaExists(reserva.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                else {
                    ModelState.AddModelError("CantidadButacas", ErrMsgs.MuchasButacas);
                }
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Apellido", reserva.ClienteId);
            ViewData["FuncionId"] = new SelectList(_context.Funciones, "Id", "Id", reserva.FuncionId);
            return View(reserva);
        }

        // GET: Reservas/Delete/5
        [Authorize(Roles = Cfgs.RolCliente)]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Funcion)
                .Include(r => r.Funcion.Pelicula)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Cfgs.RolCliente)]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            _context.Reservas.Remove(reserva);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaExists(Guid id)
        {
            return _context.Reservas.Any(e => e.Id == id);
        }
    }
}
