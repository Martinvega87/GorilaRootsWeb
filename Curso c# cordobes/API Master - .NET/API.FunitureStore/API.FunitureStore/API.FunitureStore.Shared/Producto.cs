using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FunitureStore.Shared
{
    public class Producto
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public decimal Precio { get; set; }

        public int CategoriaProductoId { get; set; }

        // un producto puede estar en muchas ordenes detalle
        public List<DetalleOrden> DetalleOrdenes { get; set; }


    }
}
