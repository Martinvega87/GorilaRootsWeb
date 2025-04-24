using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.FunitureStore.Shared;

namespace API.FunitureStore.Data
{
    public class APIFurnitureStoreContext : DbContext
    {

        public APIFurnitureStoreContext(DbContextOptions options) :base(options) { }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Producto> productos { get; set; }

        public DbSet<Orden> Ordenes { get; set; }

        public DbSet<CategoriaProducto> categororiaProductos { get; set; }

        public DbSet<DetalleOrden> DetalleOrdenes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DetalleOrden>()// le indico cuales son las key de DetalleOrden, 
                        .HasKey(od => new { od.OrdenId, od.ProductoId });     
        }

    }
}
