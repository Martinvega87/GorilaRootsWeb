using Microsoft.EntityFrameworkCore;

namespace EstudioTeProyectoV2.Models
{
    public class StoreContext: DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options) 
        {
            
        }

        public DbSet<Persona> Personas { get; set; }
        public DbSet<Producto> Productos { get; set; }

    }
}
