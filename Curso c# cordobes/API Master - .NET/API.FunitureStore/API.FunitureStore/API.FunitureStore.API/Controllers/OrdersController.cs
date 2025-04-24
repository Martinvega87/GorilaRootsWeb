using API.FunitureStore.Data;
using API.FunitureStore.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.FunitureStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly APIFurnitureStoreContext _context;

        public OrdersController(APIFurnitureStoreContext context) 
        {
            _context = context;
        }

        [HttpGet]

        public async Task<IEnumerable<Orden>> get()
        {
           return await _context.Ordenes.Include(o => o.DetalleOrdenes).ToListAsync();
        }

        [HttpGet("{id}")]

        public async Task<ActionResult> GetDetails(int id)
        {
            var order = await _context.Ordenes.Include(o => o.DetalleOrdenes).FirstOrDefaultAsync(o => o.Id == id);

            if(order == null) return NotFound();

            return Ok(order);
        }

        [HttpPost]

        public async Task<IActionResult> Post(Orden orden)
        {
            if (orden == null) return NotFound();

            if (orden.DetalleOrdenes == null)
                return BadRequest("Order should have at least one details");
            await _context.Ordenes.AddAsync(orden);
            await _context.DetalleOrdenes.AddRangeAsync(orden.DetalleOrdenes);

            await _context.SaveChangesAsync();

            return CreatedAtAction("post", orden.Id, orden);
        }

        [HttpPut]

        public async Task<IActionResult> put(Orden orden)
        {
            if (orden == null) return NotFound();
            if (orden.Id <= 0) return NotFound();

            var existeOren = await _context.Ordenes.Include(o => o.DetalleOrdenes).FirstOrDefaultAsync(o => o.Id == orden.Id);
            if(existeOren == null) return NotFound();

            existeOren.NumeroOrden = orden.NumeroOrden;
            existeOren.OrderDate = orden.OrderDate;
            existeOren.DeliveryDate = orden.DeliveryDate;
            existeOren.ClienteId = orden.ClienteId;

            _context.DetalleOrdenes.RemoveRange(existeOren.DetalleOrdenes);

            _context.Ordenes.Update(existeOren);
            _context.DetalleOrdenes.AddRange(orden.DetalleOrdenes);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]

        public async Task<IActionResult> delete(Orden orden) // aca tambien podria recibir un ID 
        {
            if(orden == null) return NotFound(); 
            
            var existeOrden = await _context.Ordenes.Include(o=> o.DetalleOrdenes).FirstOrDefaultAsync(o => o.Id == orden.Id);
            if(existeOrden == null) return NotFound();

            _context.DetalleOrdenes.RemoveRange(existeOrden.DetalleOrdenes);
            _context.Ordenes.Remove(existeOrden);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
