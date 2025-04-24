using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FunitureStore.Data
{
    public  class APIFurnitureStoreContextFactory : IDesignTimeDbContextFactory<APIFunitureStoreContext>
    {

        public APIFunitureStoreContext CreateDbContext(string[] args) 
        {
            var optionBuilder = new DbContextOptionsBuilder<APIFunitureStoreContext>();
            optionBuilder.UseSqlite(connectionString: "Data Source= FurnitureStore.db");

            return new APIFunitureStoreContext(optionBuilder.Options);
        }
    }
}
