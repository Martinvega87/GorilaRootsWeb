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
using ReservaEspectaculo.ViewModels;
using System.Security.Claims;

namespace ReservaEspectaculo.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<Persona> _userManager;

        public EmpleadosController(Context context, UserManager<Persona> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Empleados
        [Authorize(Roles = Cfgs.RolAdmin)]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Empleados.ToListAsync());
        }

        // GET: Empleados/Details/5
        [Authorize(Roles = Cfgs.RolAdmin + ", " + Cfgs.RolEmpleado)]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                if (User.IsInRole(Cfgs.RolEmpleado))
                {
                    id = Guid.Parse(_userManager.GetUserId(HttpContext.User));
                }
                else
                {
                    return NotFound();
                }
            }

            var empleado = await _context.Empleados
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // GET: Empleados/Create
        [Authorize(Roles = Cfgs.RolAdmin)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Empleados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Cfgs.RolAdmin)]
        public async Task<IActionResult> Create([Bind("Legajo,Id,Nombre,Apellido,Dni,Telefono,Direccion,FechaAlta,Email,Password")] Empleado empleado)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    empleado.UserName = empleado.Email;
                    var resultRegistracion = await _userManager.CreateAsync(empleado, Cfgs.PassComun);
                    if (resultRegistracion.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(empleado, Cfgs.RolEmpleado);
                        return RedirectToAction(nameof(Index));
                    }

                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("Legajo", ErrMsgs.LegajoRepetido);
            }
            return View(empleado);
        }

        // GET: Empleados/Edit/5
        [Authorize(Roles = Cfgs.RolAdmin + ", " + Cfgs.RolEmpleado)]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }
            EditarEmpleado editar = new EditarEmpleado { Id = empleado.Id, Telefono = empleado.Telefono, Direccion = empleado.Direccion };
            return View(editar);
        }

        // POST: Empleados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Cfgs.RolAdmin + ", " + Cfgs.RolEmpleado)]
        public async Task<IActionResult> Edit(EditarEmpleado empleado)
        {
            var id = empleado.Id;
            var returnView = nameof(Index);
            if (User.IsInRole(Cfgs.RolEmpleado))
            {
                id = Guid.Parse(_userManager.GetUserId(HttpContext.User));
                returnView = nameof(Details);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _context.Empleados.FindAsync(id);
                    if (user == null)
                    {
                        return NotFound();
                    }
                    user.Telefono = empleado.Telefono;
                    user.Direccion = empleado.Direccion;
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoExists(empleado.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(returnView);
            }
            return View(empleado);
        }

        // GET: Empleados/Delete/5
        [Authorize(Roles = Cfgs.RolAdmin)]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Cfgs.RolAdmin)]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            _context.Empleados.Remove(empleado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoExists(Guid id)
        {
            return _context.Empleados.Any(e => e.Id == id);
        }
    }
}
