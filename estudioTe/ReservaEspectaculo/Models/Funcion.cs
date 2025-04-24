using ReservaEspectaculo.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaEspectaculo.Models
{
    public class Funcion
    {


        public Guid Id { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = ErrMsgs.Requerido)]
        public DateTime Fecha { get; set; }

        [DataType(DataType.Time)]
        [Required(ErrorMessage = ErrMsgs.Requerido)]
        public DateTime Hora { get; set; }

        [Required(ErrorMessage = ErrMsgs.Requerido)]
        public bool Confirmada { get; set; }

        public Pelicula Pelicula { get; set; }

        [Required(ErrorMessage = ErrMsgs.Requerido)]
        [ForeignKey("Pelicula")]
        public Guid PeliculaId { get; set; }

        public Sala Sala { get; set; }

        [Required(ErrorMessage = ErrMsgs.Requerido)]
        public Guid SalaId { get; set; }

        public List<Reserva> Reservas { get; set; }

        [NotMapped]
        public string Descripcion
        {
            get
            {
                if (Pelicula != null)
                {
                    return Pelicula.Titulo + " " + Fecha.Day + "/" + Fecha.Month + "/" + Fecha.Year + ", " + Hora.TimeOfDay;
                }
                else
                {
                    return "";
                }
            }
        }

        [NotMapped]
        [Display(Name = "Butacas disponibles")]
        public int ButacasDisponibles
        {
            get
            {
                if (Sala != null)
                {
                    return Sala.CapacidadButacas - ButacasReservadas;
                }
                else { return 0; }
            }
        }

        [NotMapped]
        public int ButacasReservadas
        {
            get
            {
                int result = 0;
                if (Reservas != null) { 
                foreach (Reserva r in Reservas)
                {
                    result = result + r.CantidadButacas;
                }
            }
                return result;
            }
        } 

    }
}