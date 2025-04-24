using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FunitureStore.Data
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<APIFurnitureStoreContext>
    {
        public APIFurnitureStoreContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<APIFurnitureStoreContext>();
            optionsBuilder.UseSqlite("Data Source=furniture_store.db");
            return new APIFurnitureStoreContext(optionsBuilder.Options);
        }
    }
}
