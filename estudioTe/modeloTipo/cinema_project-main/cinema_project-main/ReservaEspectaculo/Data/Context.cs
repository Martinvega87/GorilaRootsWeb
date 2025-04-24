using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReservaEspectaculo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaEspectaculo.Data
{
    public class Context : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
    {
        public Context(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Tablas para identity
            modelBuilder.Entity<IdentityUser<Guid>>().ToTable("Personas");
            modelBuilder.Entity<IdentityRole<Guid>>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("PersonasRoles");
            #endregion
            #region Validaciones para campos unicos
            modelBuilder.Entity<Sala>().HasIndex(u => u.Numero).IsUnique();
            modelBuilder.Entity<Empleado>().HasIndex(u => u.Legajo).IsUnique();
            modelBuilder.Entity<Genero>().HasIndex(u => u.Nombre).IsUnique();
            modelBuilder.Entity<Pelicula>().HasIndex(u => u.Titulo).IsUnique();
            #endregion

        }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Empleado> Empleados { get; set; }

        public DbSet<Funcion> Funciones { get; set; }

        public DbSet<Genero> Generos { get; set; }

        public DbSet<Pelicula> Peliculas { get; set; }

        public DbSet<Persona> Personas { get; set; }

        public DbSet<Reserva> Reservas { get; set; }

        public DbSet<Sala> Salas { get; set; }

        public DbSet<TipoSala> TiposSalas { get; set; }

        public DbSet<Rol> Roles { get; set; }
    }
}
