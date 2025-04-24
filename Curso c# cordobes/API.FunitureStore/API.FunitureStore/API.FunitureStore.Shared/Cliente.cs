using System.ComponentModel.DataAnnotations;

namespace API.FunitureStore.Shared
{
    public class Cliente
    { 
    
        public int Id { get; set; }

        public string Nombre { get; set; }

        [Required]
        [MinLength(2)]
        public string Apellido { get; set; }

        public DateTime FechNac {  get; set; }

        [Phone]
        public String  telefono {  get; set; }

        [EmailAddress]
        public string direccion { get; set; }



    }
}
