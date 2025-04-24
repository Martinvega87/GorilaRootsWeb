using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservaEspectaculo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaEspectaculo.Data
{
    public class DbInicializador : IDbInicializador
    {
        private readonly UserManager<Persona> _userManager;
        private readonly RoleManager<Rol> _rolManager;
        private readonly Context _context;

        public DbInicializador(UserManager<Persona> userManager, RoleManager<Rol> rolManager, Context context)
        {
            this._userManager = userManager;
            this._rolManager = rolManager;
            this._context = context;

        }

        public async Task Seed()
        {

            if (!_rolManager.Roles.Any())
            {
                InicializarRoles();
            }

            if (!_userManager.Users.Any())
            {
                InicializarUsuarios();
            }

            if (!_context.TiposSalas.Any())
            {
                await InicializarTipoSala();
            }

            if (!_context.Salas.Any())
            {
                await InicializarSalas();
            }

            if (!_context.Generos.Any())
            {
                await InicializarGeneros();
            }

            if (!_context.Peliculas.Any())
            {
                await InicializarPeliculas();
            }

            if (!_context.Funciones.Any())
            {
                await InicializarFunciones();
            }

        }

     
        private void InicializarRoles()
        {
            _rolManager.CreateAsync(new Rol() { Name = Cfgs.RolAdmin }).Wait();
            _rolManager.CreateAsync(new Rol() { Name = Cfgs.RolEmpleado }).Wait();
            _rolManager.CreateAsync(new Rol() { Name = Cfgs.RolCliente }).Wait();
        }

        private void InicializarUsuarios()
        {
            #region Crear admin
            var mailAdmin = Cfgs.UsuarioAdmin;
            var passAdmin = Cfgs.PassComun;
            var rolAdmin = Cfgs.RolAdmin;
            if (_userManager.FindByEmailAsync(mailAdmin).Result == null)
            {
                Persona admin = new Persona();
                admin.UserName = mailAdmin;
                admin.Email = admin.UserName;
                admin.Nombre = "El";
                admin.Apellido = Cfgs.RolAdmin;
                admin.Dni = 12345678;
                admin.Telefono = 1111111111;
                admin.Direccion = "En la empresa 1";


                var result = _userManager.CreateAsync(admin, passAdmin).Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(admin, rolAdmin).Wait();
                }
            }
            #endregion
            #region Crear empleado
            var mailEmpleado = Cfgs.UsuarioEmpleado;
            var passEmpleado = Cfgs.PassComun;
            var rolEmpleado = Cfgs.RolEmpleado;
            if (_userManager.FindByEmailAsync(mailEmpleado).Result == null)
            {
                Empleado empleado = new Empleado();
                empleado.Legajo = 1;
                empleado.UserName = mailEmpleado;
                empleado.Email = empleado.UserName;
                empleado.Nombre = "Gil";
                empleado.Apellido = "Laburante";
                empleado.Dni = 12345679;
                empleado.Telefono = 22222222222;
                empleado.Direccion = "Laburando 1";

                var result = _userManager.CreateAsync(empleado, passEmpleado).Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(empleado, rolEmpleado).Wait();
                }
            }
            #endregion
            #region Crear cliente
            var mailCliente = Cfgs.UsuarioCliente;
            var passCliente = Cfgs.PassComun;
            var rolCliente = Cfgs.RolCliente;
            if (_userManager.FindByEmailAsync(mailCliente).Result == null)
            {
                Cliente cliente = new Cliente();
                cliente.UserName = mailCliente;
                cliente.Email = cliente.UserName;
                cliente.Nombre = "Cliente";
                cliente.Apellido = "Reservador";
                cliente.Dni = 12345680;
                cliente.Telefono = 33333333333;
                cliente.Direccion = "Calle Falsa 123";

                var result = _userManager.CreateAsync(cliente, passCliente).Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(cliente, rolCliente).Wait();
                }
            }
            #endregion
        }

        private async Task InicializarTipoSala()
        {
            var nombre1 = "Deluxe";
            if (!_context.TiposSalas.Any(t => t.Nombre.Equals(nombre1)))
            {
                TipoSala tipoSala = new TipoSala();
                tipoSala.Id = Guid.NewGuid();
                tipoSala.Nombre = nombre1;
                tipoSala.Precio = 200;
                _context.Add(tipoSala);
                await _context.SaveChangesAsync();
            }

            var nombre2 = "Normal";
            if (!_context.TiposSalas.Any(t => t.Nombre.Equals(nombre2)))
            {
                TipoSala tipoSala = new TipoSala();
                tipoSala.Id = Guid.NewGuid();
                tipoSala.Nombre = nombre2;
                tipoSala.Precio = 100;
                _context.Add(tipoSala);
                await _context.SaveChangesAsync();
            }
        }

        private async Task InicializarSalas()
        {
            var numero1 = 1;
            if (!_context.Salas.Any(s => s.Numero == numero1))
            {
                Sala sala = new Sala();
                sala.Id = Guid.NewGuid();
                sala.Numero = numero1;
                sala.CapacidadButacas = 50;
                sala.TipoSalaId = _context.TiposSalas.First().Id;
                _context.Add(sala);
                await _context.SaveChangesAsync();
            }

            var numero2 = 2;
            if (!_context.Salas.Any(s => s.Numero == numero2))
            {
                Sala sala = new Sala();
                sala.Id = Guid.NewGuid();
                sala.Numero = numero2;
                sala.CapacidadButacas = 30;
                sala.TipoSalaId = _context.TiposSalas.First().Id;
                _context.Add(sala);
                await _context.SaveChangesAsync();
            }
        }

        private async Task InicializarGeneros()
        {
            var nombre1 = "Romance";
            if(!_context.Generos.Any(g => g.Nombre.Equals(nombre1)))
            {
                Genero genero = new Genero();
                genero.Id = Guid.NewGuid();
                genero.Nombre = nombre1;
                _context.Add(genero);
                await _context.SaveChangesAsync();
            }

            var nombre2 = "Accion";
            if (!_context.Generos.Any(g => g.Nombre.Equals(nombre2)))
            {
                Genero genero = new Genero();
                genero.Id = Guid.NewGuid();
                genero.Nombre = nombre2;
                _context.Add(genero);
                await _context.SaveChangesAsync();

            }

            var nombre3 = "Drama";
            if (!_context.Generos.Any(g => g.Nombre.Equals(nombre3)))
            {
                Genero genero = new Genero();
                genero.Id = Guid.NewGuid();
                genero.Nombre = nombre3;
                _context.Add(genero);
                await _context.SaveChangesAsync();

            }

            var nombre4 = "Comedia";
            if (!_context.Generos.Any(g => g.Nombre.Equals(nombre4)))
            {
                Genero genero = new Genero();
                genero.Id = Guid.NewGuid();
                genero.Nombre = nombre4;
                _context.Add(genero);
                await _context.SaveChangesAsync();

            }


        }

        private async Task InicializarPeliculas()
        {
            var generos = _context.Generos.AsNoTracking().ToArray();

            var titulo1 = "Titanic";
            if (!_context.Peliculas.Any(p => p.Titulo.Equals(titulo1)))
            {
                Pelicula pelicula = new Pelicula();
                pelicula.Id = Guid.NewGuid();
                pelicula.Titulo = titulo1;
                pelicula.FechaLanzamiento = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 00, 00, 00);
                pelicula.Descripcion = "Rose y Jack se enamoran en un barco que se hunde y luego Rose deja morir a Jack.";
                pelicula.GeneroId = generos[3].Id;
                _context.Add(pelicula);
                await _context.SaveChangesAsync();
            }

            var titulo2 = "Duro de Matar 17";
            if (!_context.Peliculas.Any(p => p.Titulo.Equals(titulo2)))
            {
                Pelicula pelicula = new Pelicula();
                pelicula.Id = Guid.NewGuid();
                pelicula.Titulo = titulo2;
                pelicula.FechaLanzamiento = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 07, 00, 00);
                pelicula.Descripcion = "Vuelve John McClane con una nueva mision donde nunca muere, de nuevo.";
                pelicula.GeneroId = generos[0].Id;
                _context.Add(pelicula);
                await _context.SaveChangesAsync();
            }

            var titulo3 = "El Padrino 5";
            if (!_context.Peliculas.Any(p => p.Titulo.Equals(titulo3)))
            {
                Pelicula pelicula = new Pelicula();
                pelicula.Id = Guid.NewGuid();
                pelicula.Titulo = titulo3;
                pelicula.FechaLanzamiento = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(2).Day, 13, 00, 00);
                pelicula.Descripcion = "Una nueva guerra entre la familia Corleone y la familia Tattaglia se avecina cuando una de las familias le pone ketchup a la pasta.";
                pelicula.GeneroId = generos[2].Id;
                _context.Add(pelicula);
                await _context.SaveChangesAsync();
            }

            var titulo4 = "Zoolander 3";
            if (!_context.Peliculas.Any(p => p.Titulo.Equals(titulo4)))
            {
                Pelicula pelicula = new Pelicula();
                pelicula.Id = Guid.NewGuid();
                pelicula.Titulo = titulo4;
                pelicula.FechaLanzamiento = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(3).Day, 19, 00, 00);
                pelicula.Descripcion = "Vuelve una vez mas Derek Zoolander a salvar la industria de la moda.";
                pelicula.GeneroId = generos[1].Id;
                _context.Add(pelicula);
                await _context.SaveChangesAsync();
            }
        }

        private async Task InicializarFunciones()
         {

            var peliculas = _context.Peliculas.AsNoTracking().ToArray();
            var salas = _context.Salas.AsNoTracking().ToArray();
            
            if (!_context.Funciones.Any())
            {
                foreach (Pelicula p in peliculas)
                {
                    for (var i = 0; i < 5; i++)
                    {
                        var fechaF = p.FechaLanzamiento.AddDays(i);
                        Funcion funcion = new Funcion();
                        funcion.Id = Guid.NewGuid();
                        funcion.Fecha = new DateTime(fechaF.Year, fechaF.Month, fechaF.Day, p.FechaLanzamiento.Hour, 00, 00);
                        funcion.Hora = new DateTime(fechaF.Year, fechaF.Month, fechaF.Day, p.FechaLanzamiento.Hour + i, 00, 00);
                        funcion.Confirmada = true;
                        funcion.PeliculaId = p.Id;
                        funcion.SalaId = salas[i % 2 == 0 ? 0 : 1].Id;
                        _context.Add(funcion);

                    }

                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
