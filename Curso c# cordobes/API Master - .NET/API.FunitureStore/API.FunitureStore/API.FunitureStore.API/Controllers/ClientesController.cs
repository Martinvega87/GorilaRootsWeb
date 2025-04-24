using API.FunitureStore.Data;
using API.FunitureStore.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace API.FunitureStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly APIFurnitureStoreContext _context;
        /// <summary>
        /// inyecto dependencia nexo de base de datos
        /// </summary>
        /// <param name="context"></param>
        public ClientesController(APIFurnitureStoreContext context)
        {
            _context = context;
        }
        //hago un CRUD de clientes

        [HttpGet]
        public async Task<IEnumerable<Cliente>> Get()
        {
           return await _context.Clientes.ToListAsync();
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetDetails(int id)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id);
                
            if (cliente == null) return NotFound();

            return Ok(cliente);

        }
        [HttpPost]
        public async Task<IActionResult> post(Cliente Cliente)
        {
            await _context.Clientes.AddAsync(Cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("post", Cliente.Id,Cliente);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Cliente Cliente)
        {
            _context.Clientes.Update(Cliente);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Cliente Cliente)
        {
            if(Cliente == null) return NotFound();

            else 
                _context.Clientes.Remove(Cliente);
                await _context.SaveChangesAsync();
                return NoContent();
        }
    }
}
