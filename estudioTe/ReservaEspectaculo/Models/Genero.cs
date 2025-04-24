using ReservaEspectaculo.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaEspectaculo.Models
{
    public class Genero
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = ErrMsgs.Requerido)]
        [StringLength(Cfgs.NomGeneroMax, MinimumLength = Cfgs.NomGeneroMin, ErrorMessage = ErrMsgs.RangoTexto)]
        public string Nombre { get; set; }

        public List<Pelicula> Peliculas { get; set; }
    }
}
