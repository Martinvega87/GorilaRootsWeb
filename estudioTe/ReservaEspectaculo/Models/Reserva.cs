using ReservaEspectaculo.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaEspectaculo.Models
{
    public class Reserva
    {
        //Autogenerado
        public Guid Id { get; set; }

        public Funcion Funcion { get; set; }

        [Required(ErrorMessage = ErrMsgs.Requerido)]
        public Guid FuncionId { get; set; }

        public Cliente Cliente { get; set; }

        [Required(ErrorMessage = ErrMsgs.Requerido)]
        public Guid ClienteId { get; set; }

        [Required(ErrorMessage = ErrMsgs.Requerido)]
        [Range(Cfgs.ButacasReservaMin, int.MaxValue, ErrorMessage = ErrMsgs.RangoButacasReserva)]
        [Display(Name = "Cantidad de butacas")]
        public int CantidadButacas { get; set; }

        //Autogenerado
        [Display(Name = "Fecha de creacion")]
        public DateTime FechaAlta { get; set; }

        public Reserva() { 
            {
                this.FechaAlta = DateTime.Now;
            }
        }
    }
}