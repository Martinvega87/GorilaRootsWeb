using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EstudioTeProyecto.Models
{
    public class Usuario : AbstractPersona
    {
        public string Rol { get; set; }//admin, supervisor, operador
    }
}
