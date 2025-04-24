using API.FunitureStore.Data;
using API.FunitureStore.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.FunitureStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly  APIFunitureStoreContext _context;

        public ClientesController (APIFunitureStoreContext context)
        {
            _context = context;
        }

        /// <summary>
        /// aca DEVUELVO todos los clientes (((GET)))
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Cliente>> Get()
        {
            return await _context.Clientes.ToListAsync();
        }

        /// <summary>
        /// Corresponde a devolver un solo cliente IACTIONRESULT me permite devolver respuestas del tipo httpRepost
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("{id}")] //accion de devolver decorador o tambien llamado Verbo, definicion puntual del endpoint, le pongo id para pasarlo como parametro

        public async Task<IActionResult> GetDetails(int id)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id); //sino encuentra devuelve un NULL  y NO da ERROR 

            if (cliente == null)
            {
                return NotFound();//es un httpResponse devuelve un 404
            }

            return Ok(cliente);
        }


    }
}
