using ReservaEspectaculo.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaEspectaculo.ViewModels
{
    public class EditarCliente
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = ErrMsgs.Requerido)]
        [Range(Cfgs.TelefonoMin, Cfgs.TelefonoMax, ErrorMessage = ErrMsgs.RangoNumero)]
        public long Telefono { get; set; }

        [Required(ErrorMessage = ErrMsgs.Requerido)]
        [StringLength(Cfgs.DireccionMax, ErrorMessage = ErrMsgs.MaxTexto)]
        public string Direccion { get; set; }
    }
}
