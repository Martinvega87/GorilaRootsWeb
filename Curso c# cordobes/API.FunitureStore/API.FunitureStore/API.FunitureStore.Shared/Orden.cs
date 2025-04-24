using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FunitureStore.Shared
{
    public class Orden
    {

        public int Id { get; set; }

        public int NumeroDeOrden { get; set; }

        public int ClienteId { get; set; }

        public DateTime FechaDeOrden { get; set; }

        public DateTime FechaDeEntrega { get; set; }


    }
}
