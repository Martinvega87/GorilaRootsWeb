using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EstudioTeProyecto.Models
{
    public class OrdenDeCompra : IEnumerable<producto>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdOrdenDeCompra { get; set; }

        public DateTime PlazoEntrega { get; set; }

        public DateTime FechaEstimadaEntrega { get; set; }

        public int descuento { get; set; } // %

        public int PorcentajeAnticipo { get; set; } // que cargan aca

        public double Anticipo { get; set; }

        public bool Embalaje { get; set; } // verificar si corresponde distintos TIPOS DE EMBALAJE

        public double PrecioEmbalaje { get; set; }

        public double MontoAnticipo { get; set; }// que diferencia hay con el campo Anticipo

        public string MedioDePago { get; set; }//esto podria ser enumerable. transferencia tarjeta efectico....

        public string TipoFactura { get; set; }//A B C Verificar donde va

        public string Comentarios { get; set; }//Detalle

        public string Cantidad { get; set; }

        private List<producto> misProductos = new List<producto>();

        public IEnumerator<producto> GetEnumerator()
        {
            return misProductos.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
