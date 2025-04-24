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
    public class TiposSalasController : Controller
    {
        private readonly Context _context;

        public TiposSalasController(Context context)
        {
            _context = context;
        }

        // GET: TiposSalas
        [Authorize(Roles = "Admin,Empleado")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.TiposSalas.ToListAsync());
        }

        // GET: TiposSalas/Details/5
        [Authorize(Roles = "Admin,Empleado")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoSala = await _context.TiposSalas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoSala == null)
            {
                return NotFound();
            }

            return View(tipoSala);
        }

        // GET: TiposSalas/Create
        [Authorize(Roles = "Admin,Empleado")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: TiposSalas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Cfgs.RolAdmin + ", " + Cfgs.RolEmpleado)]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Precio")] TipoSala tipoSala)
        {
            if (ModelState.IsValid)
            {
                tipoSala.Id = Guid.NewGuid();
                _context.Add(tipoSala);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoSala);
        }

        // GET: TiposSalas/Edit/5
        [Authorize(Roles = "Admin,Empleado")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoSala = await _context.TiposSalas.FindAsync(id);
            if (tipoSala == null)
            {
                return NotFound();
            }
            return View(tipoSala);
        }

        // POST: TiposSalas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Cfgs.RolAdmin + ", " + Cfgs.RolEmpleado)]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nombre,Precio")] TipoSala tipoSala)
        {
            if (id != tipoSala.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoSala);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoSalaExists(tipoSala.Id))
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
            return View(tipoSala);
        }

        private bool TipoSalaExists(Guid id)
        {
            return _context.TiposSalas.Any(e => e.Id == id);
        }
    }
}
