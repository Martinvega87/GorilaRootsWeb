using API.FunitureStore.Data;
using API.FunitureStore.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.FunitureStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaProductosController : ControllerBase
    {
        private readonly APIFurnitureStoreContext _context;

        public CategoriaProductosController(APIFurnitureStoreContext context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<IEnumerable<CategoriaProducto>> Get()
        {
            return await _context.categororiaProductos.ToListAsync();
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetDetails(int id)
        {
            var categoriaProducto = await _context.productos.FirstOrDefaultAsync(x => x.Id == id);
            if (categoriaProducto == null)
            {
                return NotFound();
            }
            return Ok(categoriaProducto);
        }

        [HttpPost]

        public async Task<IActionResult> post(CategoriaProducto catProd)
        {
            await _context.categororiaProductos.AddAsync(catProd);
            await _context.SaveChangesAsync();

            return CreatedAtAction("post", catProd.Id, catProd);
        }

        [HttpPut]

        public async Task<IActionResult> Put(CategoriaProducto catProd)
        {
            _context.categororiaProductos.Update(catProd);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]

        public async Task<IActionResult> Delete(CategoriaProducto catProd)
        {
            if (catProd == null) return NotFound();

            else
                _context.categororiaProductos.Remove(catProd);
            await _context.SaveChangesAsync();
            return NoContent();

        }
    }
}
