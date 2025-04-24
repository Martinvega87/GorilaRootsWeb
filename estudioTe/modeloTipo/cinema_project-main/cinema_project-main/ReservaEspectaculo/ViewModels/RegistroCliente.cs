using Microsoft.AspNetCore.Mvc;
using ReservaEspectaculo.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaEspectaculo.ViewModels
{
    public class RegistroCliente
    {
        [Required(ErrorMessage = ErrMsgs.Requerido)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = ErrMsgs.Requerido)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = ErrMsgs.Requerido)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmación de Password")]
        [Compare("Password", ErrorMessage = ErrMsgs.ErrorPasswordConfirm)]
        public string ConfirmacionPassword { get; set; }

        [Required(ErrorMessage = ErrMsgs.Requerido)]
        [StringLength(Cfgs.NomAppMax, MinimumLength = Cfgs.NomAppMin, ErrorMessage = ErrMsgs.RangoTexto)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = ErrMsgs.Requerido)]
        [StringLength(Cfgs.NomAppMax, MinimumLength = Cfgs.NomAppMin, ErrorMessage = ErrMsgs.RangoTexto)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = ErrMsgs.Requerido)]
        [Range(Cfgs.DniMin, Cfgs.DniMax, ErrorMessage = ErrMsgs.RangoNumero)]
        public int Dni { get; set; }

        [Required(ErrorMessage = ErrMsgs.Requerido)]
        [Range(Cfgs.TelefonoMin, Cfgs.TelefonoMax, ErrorMessage = ErrMsgs.RangoNumero)]
        //Se cambió a long para poder admitir numeros largos con codigos de pais / provincia
        public long Telefono { get; set; }

        [Required(ErrorMessage = ErrMsgs.Requerido)]
        [StringLength(Cfgs.DireccionMax, ErrorMessage = ErrMsgs.MaxTexto)]
        public string Direccion { get; set; }
    }
}
