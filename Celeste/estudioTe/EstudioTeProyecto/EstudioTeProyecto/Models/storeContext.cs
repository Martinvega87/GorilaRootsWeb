using Microsoft.EntityFrameworkCore;

namespace EstudioTeProyecto.Models
{
    public class storeContext : DbContext
    {
        public storeContext(DbContextOptions<storeContext> options) : base(options)
        { 
            
        }

       public DbSet<AbstractPersona> Personas { get; set; }
       public DbSet<producto> Productos { get; set; }


    }
}
