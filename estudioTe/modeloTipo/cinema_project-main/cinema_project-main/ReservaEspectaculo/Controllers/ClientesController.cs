using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservaEspectaculo.Data;
using ReservaEspectaculo.Models;
using ReservaEspectaculo.ViewModels;

namespace ReservaEspectaculo.Controllers
{
    public class ClientesController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<Persona> _userManager;

        public ClientesController(Context context, UserManager<Persona> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Clientes
        [Authorize(Roles = Cfgs.RolAdmin+", "+Cfgs.RolEmpleado)]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clientes.ToListAsync());
        }

        // GET: Clientes/Details/5
        [Authorize(Roles = Cfgs.RolAdmin+", "+Cfgs.RolEmpleado+", "+Cfgs.RolCliente)]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                if (User.IsInRole(Cfgs.RolCliente))
                {
                    id = Guid.Parse(_userManager.GetUserId(HttpContext.User));
                }
                else
                {
                    return NotFound();
                }
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        [Authorize(Roles = Cfgs.RolAdmin+", "+Cfgs.RolEmpleado)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Dni,Telefono,Direccion,FechaAlta,Email,Password")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                cliente.UserName = cliente.Email;
                var resultRegistracion = await _userManager.CreateAsync(cliente, Cfgs.PassComun);
                if (resultRegistracion.Succeeded)
                {
                    await _userManager.AddToRoleAsync(cliente, Cfgs.RolCliente);
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        [Authorize(Roles = Cfgs.RolAdmin+", "+Cfgs.RolEmpleado+", "+Cfgs.RolCliente)]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            EditarCliente editar = new EditarCliente { Id = cliente.Id, Telefono = cliente.Telefono, Direccion = cliente.Direccion };
            return View(editar);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Cfgs.RolAdmin + ", " + Cfgs.RolEmpleado + ", " + Cfgs.RolCliente)]
        public async Task<IActionResult> Edit(EditarCliente cliente)
        {
            var id = cliente.Id;
            var returnView = nameof(Index);
            if (User.IsInRole(Cfgs.RolCliente))
            {
                id = Guid.Parse(_userManager.GetUserId(HttpContext.User));
                returnView = nameof(Details);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _context.Clientes.FindAsync(id);
                    if (user == null) {
                        return NotFound();
                    }
                    user.Telefono = cliente.Telefono;
                    user.Direccion = cliente.Direccion;
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
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
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        [Authorize(Roles = Cfgs.RolAdmin+", "+Cfgs.RolEmpleado)]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Cfgs.RolAdmin + ", " + Cfgs.RolEmpleado)]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(Guid id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }
    }
}
