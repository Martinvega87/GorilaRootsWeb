using API.FunitureStore.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace API.FunitureStore.Data
{
    public  class APIFunitureStoreContext : DbContext
    {
        public APIFunitureStoreContext(DbContextOptions options) : base(options) { }

         //DBSET es la representacion de la tabla en el codigo

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<CategoriaProducto> CategoriaProductos { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<Producto> Productos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite();
        }

    }
}
