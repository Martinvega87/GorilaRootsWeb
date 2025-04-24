namespace EstudioTeProyecto.Models
{
    public class Factura
    {
        public int IdFactura { get; set; }

        public OrdenDeCompra OrdenDeCompra
        {
            get => default;
            set
            {
            }
        }
    }
}
