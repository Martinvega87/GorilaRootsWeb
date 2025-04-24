using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReservaEspectaculo.Data;
using ReservaEspectaculo.Models;
using ReservaEspectaculo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaEspectaculo.Controllers
{
    public class AccountsController : Controller
    {
        private readonly UserManager<Persona> _userManager;
        private readonly SignInManager<Persona> _signInManager;
        private readonly RoleManager<Rol> _roleManager;
        private readonly Context _context;

        public AccountsController(UserManager<Persona> userManager, SignInManager<Persona> signInManager, RoleManager<Rol> roleManager, Context context)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
            this._context = context;
        }

        [HttpGet]
  
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(RegistroCliente registroCliente)
        {
            if (ModelState.IsValid)
            {
                var Cliente = new Cliente
                {
                    Nombre = registroCliente.Nombre,
                    Apellido = registroCliente.Apellido,
                    Dni = registroCliente.Dni,
                    Telefono = registroCliente.Telefono,
                    Direccion = registroCliente.Direccion,
                    UserName = registroCliente.Email,
                    Email = registroCliente.Email
                };

                var resultRegistracion = await _userManager.CreateAsync(Cliente, registroCliente.Password);

                if (resultRegistracion.Succeeded)
                {
                    await _userManager.AddToRoleAsync(Cliente, "Cliente");
                    await _signInManager.SignInAsync(Cliente, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in resultRegistracion.Errors)
                {
                    if (error.Code.Contains("Password"))
                    {
                        ModelState.AddModelError("Password", error.Description);
                    }
                    if (error.Code.Contains("DuplicateUserName")) {
                        ModelState.AddModelError("Email", ErrMsgs.UsuarioYaExiste);
                    }
                }
            }

            return View(registroCliente);
        }

        [HttpGet]
   
        public IActionResult IniciarSesion(string returnUrl)
        {
            TempData["returnUrl"] = returnUrl;
            if (returnUrl != null)
            {
                ViewBag.Mensaje = ErrMsgs.DebeLoguearse;
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesionAsync(Login modeloLogin)
        {
            string returnUrl = TempData["returnUrl"] as string;

            if (ModelState.IsValid)
            {
                var resultadoLogin = await _signInManager.PasswordSignInAsync(modeloLogin.Email, modeloLogin.Password, modeloLogin.Recordarme, false);

                if (resultadoLogin.Succeeded)
                {
                    if (!string.IsNullOrWhiteSpace(returnUrl))
                        return Redirect(returnUrl);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, ErrMsgs.ErrorLogin);
            }
            return View(modeloLogin);
        }

        public async Task<IActionResult> CerrarSesion()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> CrearRoles()
        {
            if (!_roleManager.Roles.Any())
            {
                Rol rol1, rol2, rol3;
                rol1 = rol2 = rol3 = new Rol();

                rol1.Name = "Admin";
                rol2.Name = "Empleado";
                rol3.Name = "Cliente";

                var resAdmin = await _roleManager.CreateAsync(rol1);
                var resEmpleado = await _roleManager.CreateAsync(rol2);
                var resCliente = await _roleManager.CreateAsync(rol3);
            };

            return RedirectToAction("Roles");
        }


        public async Task<IActionResult> Roles()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        [HttpGet, AllowAnonymous]
    
        public IActionResult AccesoDenegado()
        {
            return View();
        }
    }
}
