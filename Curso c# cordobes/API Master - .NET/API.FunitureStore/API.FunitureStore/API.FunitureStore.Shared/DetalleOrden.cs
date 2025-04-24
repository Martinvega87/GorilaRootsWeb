using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FunitureStore.Shared
{
    public class DetalleOrden
    {
        // CUANTAS UNIDADES DEL PRODUCTO SE AGREGARON A LA ORDEN (ordenId)
        //clave compuesta entre ordenId Y ProductoId
        public int OrdenId { get; set; }

        public int ProductoId { get; set;}

        public int cantdidad {  get; set; }

    }
}
