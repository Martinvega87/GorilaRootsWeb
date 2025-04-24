using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EstudioTeProyecto.Models
{
    public abstract class  AbstractPersona
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id 
        {
            get
            {
                // Obtener la letra correspondiente según el tipo de cliente
                char letra = Tipo switch
                {
                    TipoCliente.Cliente => 'C',//cliente
                    TipoCliente.Usuario => 'U',//usuario
                    TipoCliente.Externo => 'E',//externo
                    _ => throw new InvalidOperationException("Tipo de usuario no válido."),
                };

                // Concatenar la letra con el valor numérico de Id
                return $"{letra}{Id}";
            }
            set
            {

            }

        }// verificar como el id tiene una letra predesesora C, U o E dependiendo que tipo de cliente sea


        public enum TipoCliente
        {
            Cliente,
            Usuario,
            Externo
        }

        public TipoCliente Tipo { get; set; }

        public int DNI { get; set; }

        public int  Cuil { get; set; } // aca lo pusimos asi por que se cargaria sin guion 

        public string Mail { get; set; }

        public string Localidad { get; set; }

        public string Direccion { get; set; }

        public string Provincia { get; set; }// podria ser Enum

        public int CodigoPostal { get; set; }

        public DateTime FechaAlta { get; set; }

        public string TipoUsuario { get; set; }// cliente, o usuario interno

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string PrimerContacto { get; set; }

        public enum TipoGenero
        {
          Femenino,
          Masculino,
          NoBinario,
          NoEspecifica
            
        }

        public TipoGenero genero { get; set;}

        


    }
}
