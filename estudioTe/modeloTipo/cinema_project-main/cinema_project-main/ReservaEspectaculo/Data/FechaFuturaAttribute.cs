using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaEspectaculo.Data
{
    public class FechaFuturaAttribute : ValidationAttribute
    {
        public FechaFuturaAttribute() {
            ErrorMessage = "La fecha no puede ser igual o menor a la actual";
        }

        public override bool IsValid(object value)
        {
            bool result = false;

            if (value is DateTime fecha) {
                var hoy = DateTime.Today;
                int resultado = hoy.Date.CompareTo(fecha.Date);
                result = resultado < 0;
            }

            return result;
        }
    }
}
