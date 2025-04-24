using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ReservaEspectaculo.Data;

namespace ReservaEspectaculo.Models
{
    public class Sala
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = ErrMsgs.Requerido)]
        public int Numero { get; set; }

        [Display(Name = "Tipo de Sala")]
        public TipoSala TipoSala { get; set; }

        [Required(ErrorMessage = ErrMsgs.Requerido)]
        public Guid TipoSalaId { get; set; }

        [Display(Name = "Capacidad de Butacas")]
        [Range(Cfgs.CapacidadButacasMin, Cfgs.CapacidadButacasMax, ErrorMessage = ErrMsgs.RangoNumero)]
        public int CapacidadButacas { get; set; }

        public List<Funcion> Funciones { get; set; }

        public string NumeroTipo
        {
            get
            {
                if (TipoSala == null)
                {
                    return "";
                }
                else
                {
                    return Numero + ", " + TipoSala.Nombre;
                }
            }
        }
    }
    public class TipoSala
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = ErrMsgs.Requerido)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = ErrMsgs.Requerido)]
        public double Precio { get; set; }
    }
}
