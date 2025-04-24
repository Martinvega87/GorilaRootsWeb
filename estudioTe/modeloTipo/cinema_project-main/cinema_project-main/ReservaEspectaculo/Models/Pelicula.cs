using ReservaEspectaculo.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaEspectaculo.Models
{
    public class Pelicula
    {
        public Guid Id { get; set; }

        [Display(Name = "Fecha de estreno")]
        [Required(ErrorMessage = ErrMsgs.Requerido)]
        [DataType(DataType.Date)]
        public DateTime FechaLanzamiento { get; set; }

        [Required(ErrorMessage = ErrMsgs.Requerido)]
        [StringLength(Cfgs.TituloMax, MinimumLength = Cfgs.TituloMin, ErrorMessage = ErrMsgs.RangoTexto)]
        public string Titulo { get; set; }

        [StringLength(Cfgs.DescripcionMax, MinimumLength = Cfgs.DescripcionMin, ErrorMessage = ErrMsgs.RangoTexto)]
        public string Descripcion { get; set; }

        // Validacion para que sea un genero existente
        public Genero Genero { get; set; }

        [Display(Name = "Genero")]
        [Required(ErrorMessage = ErrMsgs.Requerido)]
        public Guid GeneroId { get; set; }

        // Validacion para que no sea nulo 
        public List<Funcion> Funciones { get; set; }

    }
}