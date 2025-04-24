using ReservaEspectaculo.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaEspectaculo.Models
{
    public class Cliente : Persona
    {
        public List<Reserva> Reservas { get; set; }

        public bool TieneReservaActiva
        {
            get
            {
                bool result = false;
                if (Reservas != null)
                {
                    foreach (Reserva r in Reservas)
                    {
                        if (r.Funcion.Fecha.CompareTo(DateTime.Now) >= 0)
                        {
                            result = true;
                        }
                    }
                }
                return result;
            }
        }
    }
}
