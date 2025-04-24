using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaEspectaculo.Data
{
    interface IDbInicializador
    {
        Task Seed();
    }
}
