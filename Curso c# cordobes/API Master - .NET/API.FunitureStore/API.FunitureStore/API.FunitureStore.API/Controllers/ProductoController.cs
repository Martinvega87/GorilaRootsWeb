using API.FunitureStore.Data;
using API.FunitureStore.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.FunitureStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly APIFurnitureStoreContext _context;

        public ProductoController(APIFurnitureStoreContext context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<IEnumerable<Producto>> Get()
        {
            return await _context.productos.ToListAsync();
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetDetails(int id)
        {
            var producto = await _context.productos.FirstOrDefaultAsync(x => x.Id == id);
            if (producto == null) 
            {
                return NotFound();
            }
            return Ok(producto);
        }

        [HttpGet("GetPorCategoria/CategoriaProductoId")]

        public async Task<IEnumerable<Producto>> GetPorCategoria(int productoCategoriaId)
        {
            return await _context.productos
                                 .Where(p => p.CategoriaProductoId == productoCategoriaId)
                                 .ToListAsync();
        }


        [HttpPost]

        public async Task<IActionResult> post(Producto prod)
        {
            await _context.productos.AddAsync(prod);
            await _context.SaveChangesAsync();

            return CreatedAtAction("post", prod.Id,prod);
        }

        [HttpPut]
        
        public async Task<IActionResult> Put(Producto prod)
        {
            _context.productos.Update(prod);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]

        public async Task<IActionResult> Delete(Producto prod)
        {
            if (prod == null) return NotFound();

            else
                _context.productos.Remove(prod);
                await _context.SaveChangesAsync();
                return NoContent();

        }
    }
}
