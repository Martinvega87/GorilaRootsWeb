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
    public class SalasController : Controller
    {
        private readonly Context _context;

        public SalasController(Context context)
        {
            _context = context;
        }

        // GET: Salas
        [Authorize(Roles = "Admin,Empleado")]
        public async Task<IActionResult> Index()
        {
            var context = _context.Salas.Include(s => s.TipoSala).Include(s => s.Funciones);
            return View(await context.ToListAsync());
        }

        // GET: Salas/Details/5
        [Authorize(Roles = "Admin,Empleado")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sala = await _context.Salas
                .Include(p => p.TipoSala)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sala == null)
            {
                return NotFound();
            }

            return View(sala);
        }

        // GET: Salas/Create
        [Authorize(Roles = "Admin,Empleado")]
        public IActionResult Create()
        {
            ViewData["TipoSalaId"] = new SelectList(_context.TiposSalas, "Id", "Nombre");
            return View();
        }

        // POST: Salas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Cfgs.RolAdmin + ", " + Cfgs.RolEmpleado)]
        public async Task<IActionResult> Create([Bind("Id,Numero,CapacidadButacas,TipoSalaId")] Sala sala)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    sala.Id = Guid.NewGuid();
                    _context.Add(sala);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("Numero", ErrMsgs.NumeroSala);
            }
            ViewData["TipoSalaId"] = new SelectList(_context.TiposSalas, "Id", "Nombre", sala.TipoSalaId);
            return View(sala);
        }

        // GET: Salas/Edit/5
        [Authorize(Roles = "Admin,Empleado")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sala = await _context.Salas.FindAsync(id);
            if (sala == null)
            {
                return NotFound();
            }
            ViewData["TipoSalaId"] = new SelectList(_context.TiposSalas, "Id", "Nombre", sala.TipoSalaId);
            return View(sala);
        }

        // POST: Salas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Cfgs.RolAdmin + ", " + Cfgs.RolEmpleado)]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Numero,CapacidadButacas,TipoSalaId")] Sala sala)
        {
            if (id != sala.Id)
            {
                return NotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(sala);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!SalaExists(sala.Id))
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
                ModelState.AddModelError("Numero", ErrMsgs.NumeroSala);
            }
            ViewData["TipoSalaId"] = new SelectList(_context.TiposSalas, "Id", "Nombre", sala.TipoSalaId);
            return View(sala);
        }

        private bool SalaExists(Guid id)
        {
            return _context.Salas.Any(e => e.Id == id);
        }
    }
}
