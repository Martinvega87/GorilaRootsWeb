using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FunitureStore.Shared
{
    public class Orden
    {
        public int Id  { get; set; }

        public int NumeroOrden { get; set; }

        public int ClienteId { get; set; }

        public DateTime OrderDate {  get; set; }

        public DateTime DeliveryDate { get; set; } 

        public List<DetalleOrden> DetalleOrdenes { get; set; }
    }
}
