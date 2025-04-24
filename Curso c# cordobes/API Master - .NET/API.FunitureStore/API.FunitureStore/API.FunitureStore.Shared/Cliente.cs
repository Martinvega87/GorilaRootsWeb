using System.Globalization;

namespace API.FunitureStore.Shared
{
    public class Cliente
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; } 

        public DateTime FechaCumpleAños { get; set; }

        public string Telefono { get; set; }

        public string Direccion {  get; set; }


    }
}
