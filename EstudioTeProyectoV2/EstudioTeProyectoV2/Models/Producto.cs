using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EstudioTeProyectoV2.Models
{
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProducto { get; set; }

        public string NombreProducto { get; set; }

        public string ColorBase { get; set; }// podria ser un enumerable

        public string ComplementoUno { get; set; } //averiguar si es parte del producto o de la OC

        public string ComplementoDos { get; set; } //averiguar si es parte del producto o de la OC

        public string Variable { get; set; }

        public string Stock { get; set; }

        public double PrecioUnitario { get; set; } //podria tambien ser Float

        public double IVA { get; set; }// % que se le va a sumar al PRECIO UNITARIO
    }
}
