using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservaEspectaculo.Data;
using ReservaEspectaculo.Models;

namespace ReservaEspectaculo.Controllers
{
    public class FuncionesController : Controller
    {
        private readonly Context _context;
        private readonly SignInManager<Persona> _signInManager;

        public FuncionesController(Context context, SignInManager<Persona> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        // GET: Funciones
        public async Task<IActionResult> Index(Guid? id)
        {

            if (id != null && id != Guid.Empty && (User.IsInRole("Cliente") || !_signInManager.IsSignedIn(User)))
            {
                var context = _context.Funciones.Include(f => f.Pelicula).Include(p => p.Sala).Include(p => p.Reservas).Where(f => f.PeliculaId == id).Where(f => f.Fecha.CompareTo(DateTime.Now) >= 0).OrderBy(f => f.Fecha);
                return View(await context.ToListAsync());
            }
            else if (id != null && id != Guid.Empty && (!User.IsInRole("Cliente") && _signInManager.IsSignedIn(User)))
            {
                var context = _context.Funciones.Include(f => f.Pelicula).Include(p => p.Sala).Include(p => p.Reservas).Where(f => f.PeliculaId == id).OrderBy(f => f.Fecha);
                return View(await context.ToListAsync());
            }
            else if (User.IsInRole("Cliente") || !_signInManager.IsSignedIn(User))
            {
                var context = _context.Funciones.Include(f => f.Pelicula).Include(p => p.Sala).Include(p => p.Reservas).Where(f => f.Fecha.CompareTo(DateTime.Now) >= 0).OrderBy(f => f.Pelicula.Titulo);
                return View(await context.ToListAsync());
            }
            else {
                var context = _context.Funciones.Include(f => f.Pelicula).Include(p => p.Sala).Include(p => p.Reservas).OrderBy(f => f.Fecha);
                return View(await context.ToListAsync());
            }

        }

        // GET: Funciones/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcion = await _context.Funciones
                .Include(f => f.Pelicula).Include(p => p.Sala)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcion == null)
            {
                return NotFound();
            }
           
            return View(funcion);
        }

        // GET: Funciones/Create
        [Authorize(Roles = Cfgs.RolAdmin + ", " + Cfgs.RolEmpleado)]
        public IActionResult Create(Guid peliId)
        {
            var func = new Funcion() { Fecha = DateTime.Now, PeliculaId = peliId };
            ViewData["SalaId"] = new SelectList(_context.Salas.Include(s => s.TipoSala), "Id", "NumeroTipo");
            return View(func);
         }

        // POST: Funciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Cfgs.RolAdmin + ", " + Cfgs.RolEmpleado)]
        public async Task<IActionResult> Create([Bind("Id,Fecha,Hora,Descripcion,Confirmada,PeliculaId,SalaId")] Funcion funcion)
        {

            if (ModelState.IsValid)
            {
                var peli = await _context.Peliculas.FindAsync(funcion.PeliculaId);
                if (Helpers.ValidarFechaDesde(peli.FechaLanzamiento, funcion.Fecha))
                {
                    var sala = await _context.Salas.FindAsync(funcion.SalaId);
                    if (!Helpers.ExisteFuncion(funcion, sala, _context))
                    {
                        
                        funcion.Id = Guid.NewGuid();
                        _context.Add(funcion);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewData["SalaId"] = new SelectList(_context.Salas, "Id", "Numero", funcion.SalaId);
                        ModelState.AddModelError(String.Empty, ErrMsgs.FuncionYaExistente);
                    }
                }
                else {
                    ViewData["SalaId"] = new SelectList(_context.Salas, "Id", "Numero", funcion.SalaId);
                    ModelState.AddModelError("Fecha", ErrMsgs.FechaFuncion);
                }
            }

            return View(funcion);
        }

        // GET: Funciones/Edit/5
        [Authorize(Roles = Cfgs.RolAdmin + ", " + Cfgs.RolEmpleado)]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcion = await _context.Funciones.FindAsync(id);
            if (funcion == null)
            {
                return NotFound();
            }
            ViewData["PeliculaId"] = new SelectList(_context.Peliculas, "Id", "Titulo", funcion.PeliculaId);
            ViewData["SalaId"] = new SelectList(_context.Salas.Include(s => s.TipoSala), "Id", "NumeroTipo");
            return View(funcion);
        }

        // POST: Funciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Cfgs.RolAdmin + ", " + Cfgs.RolEmpleado)]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Fecha,Hora,Descripcion,Confirmada,PeliculaId,SalaId")] Funcion funcion)
        {
            if (id != funcion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var funcionActual = await _context.Funciones.AsNoTracking().Include(p => p.Reservas)
                .FirstOrDefaultAsync(m => m.Id == id);
                if (funcionActual.ButacasReservadas > 0)
                {
                    ModelState.AddModelError(String.Empty, ErrMsgs.FuncionConReservasEdit);
                    return View(funcion);
                }

                var peli = await _context.Peliculas.FindAsync(funcion.PeliculaId);
                if (Helpers.ValidarFechaDesde(peli.FechaLanzamiento, funcion.Fecha))
                {
                    var sala = await _context.Salas.FindAsync(funcion.SalaId);
                    if (!Helpers.ExisteFuncion(funcion, sala, _context))
                    {
                        try
                        {
                            _context.Update(funcion);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!FuncionExists(funcion.Id))
                            {
                                return NotFound();
                            }
                            else
                            {
                                throw;
                            }
                        }
                    }
                    else {
                        ViewData["PeliculaId"] = new SelectList(_context.Peliculas, "Id", "Titulo", funcion.PeliculaId);
                        ViewData["SalaId"] = new SelectList(_context.Salas, "Id", "Numero", funcion.SalaId);
                        ModelState.AddModelError(String.Empty, ErrMsgs.FuncionYaExistente);
                        return View(funcion);
                    }
                }
                else {
                    ViewData["PeliculaId"] = new SelectList(_context.Peliculas, "Id", "Titulo", funcion.PeliculaId);
                    ViewData["SalaId"] = new SelectList(_context.Salas, "Id", "Numero", funcion.SalaId);
                    ModelState.AddModelError("Fecha", ErrMsgs.FechaFuncion);
                    return View(funcion);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PeliculaId"] = new SelectList(_context.Peliculas, "Id", "Titulo", funcion.PeliculaId);
            ViewData["SalaId"] = new SelectList(_context.Salas, "Id", "Numero");
            return View(funcion);
        }

        // GET: Funciones/Delete/5
        [Authorize(Roles = Cfgs.RolAdmin + ", " + Cfgs.RolEmpleado)]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcion = await _context.Funciones
                .Include(f => f.Pelicula).Include(p => p.Sala)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcion == null)
            {
                return NotFound();
            }

            return View(funcion);
        }

        // POST: Funciones/Delete/5
        [Authorize(Roles = Cfgs.RolAdmin + ", " + Cfgs.RolEmpleado)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var funcion = await _context.Funciones
                .Include(f => f.Pelicula).Include(p => p.Sala).Include(p => p.Reservas)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcion.ButacasReservadas > 0)
            {
                ModelState.AddModelError(String.Empty, ErrMsgs.FuncionConReservasElim);
                return View(funcion);
            }
            else {
                _context.Funciones.Remove(funcion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

        private bool FuncionExists(Guid id)
        {
            return _context.Funciones.Any(e => e.Id == id);
        }

        [Authorize(Roles = Cfgs.RolAdmin + ", " + Cfgs.RolEmpleado + ", " + Cfgs.RolCliente)]
        public async Task<IActionResult> NuevaReserva(Guid id) {
            var funcion = await _context.Funciones.FindAsync(id);
            return RedirectToAction("Create", "Reservas", new { funcId = funcion.Id });
        }
    }
}
