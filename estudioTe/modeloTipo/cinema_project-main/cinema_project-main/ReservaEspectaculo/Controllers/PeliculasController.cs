using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservaEspectaculo.Data;
using ReservaEspectaculo.Models;

namespace ReservaEspectaculo.Controllers
{
    public class PeliculasController : Controller
    {
        private readonly Context _context;

        public PeliculasController(Context context)
        {
            _context = context;
        }

        // GET: Peliculas
 
        public async Task<IActionResult> Index()
        {
            var context = _context.Peliculas.Include(p => p.Genero);
            return View(await context.ToListAsync());
        }

        // GET: Peliculas/Details/5
     
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas
                .Include(p => p.Genero)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // GET: Peliculas/Create
        [Authorize(Roles = "Admin,Empleado")]
        public IActionResult Create()
        {
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Nombre");
            return View();
        }

        // POST: Peliculas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Cfgs.RolAdmin + ", " + Cfgs.RolEmpleado)]
        public async Task<IActionResult> Create([Bind("Id,FechaLanzamiento,Titulo,Descripcion,GeneroId")] Pelicula pelicula)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pelicula.Id = Guid.NewGuid();
                    _context.Add(pelicula);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("Titulo", ErrMsgs.PeliculaRepetida);
            }
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Nombre", pelicula.GeneroId);
            return View(pelicula);
        }

        // GET: Peliculas/Edit/5
        [Authorize(Roles = "Admin,Empleado")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula == null)
            {
                return NotFound();
            }
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Nombre", pelicula.GeneroId);
            return View(pelicula);
        }

        // POST: Peliculas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Cfgs.RolAdmin + ", " + Cfgs.RolEmpleado)]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,FechaLanzamiento,Titulo,Descripcion,GeneroId")] Pelicula pelicula)
        {
            if (id != pelicula.Id)
            {
                return NotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(pelicula);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PeliculaExists(pelicula.Id))
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
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("Titulo", ErrMsgs.PeliculaRepetida);
            }
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Nombre", pelicula.GeneroId);
            return View(pelicula);
        }

        // GET: Peliculas/Delete/5
        [Authorize(Roles = "Admin,Empleado")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas
                .Include(p => p.Genero)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // POST: Peliculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Cfgs.RolAdmin + ", " + Cfgs.RolEmpleado)]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var pelicula = await _context.Peliculas.FindAsync(id);
            _context.Peliculas.Remove(pelicula);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeliculaExists(Guid id)
        {
            return _context.Peliculas.Any(e => e.Id == id);
        }

        public async Task<IActionResult> CrearFuncion(Guid id) {
            var pelicula = await _context.Peliculas.FindAsync(id);
            return RedirectToAction("Create", "Funciones", new { peliId = pelicula.Id });
        }
    }
}