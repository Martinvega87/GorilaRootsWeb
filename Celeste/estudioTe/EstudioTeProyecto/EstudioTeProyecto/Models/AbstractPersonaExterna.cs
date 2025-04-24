namespace EstudioTeProyecto.Models
{
    public abstract class AbstractPersonaExterna : AbstractPersona
    {

        public string RazonSocial { get; set; }

        public string UltimoContacto { get; set; }//medio por el cual se cerro la venta

        public string TipoDeVenta { get; set; }

        public string Rubro {  get; set; }

        public string Moneda { get; set; }

        public string CotizacionDLS { get; set; }// puede ser calculada

        public DateTime FechaDePagoAnticipada { get; set; }


    }
}
