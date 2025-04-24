using ReservaEspectaculo.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaEspectaculo.Models
{
    public class Empleado : Persona
    {
        [Required(ErrorMessage = ErrMsgs.Requerido)]
        [Range(Cfgs.LegajoMin, Cfgs.LegajoMax, ErrorMessage = ErrMsgs.RangoNumero)]
        public int Legajo { get; set; } 
    }
}
