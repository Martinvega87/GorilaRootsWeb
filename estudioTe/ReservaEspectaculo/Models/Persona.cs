using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReservaEspectaculo.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaEspectaculo.Models
{
    public class Persona: IdentityUser<Guid>
    {
        //Autogenerado
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //Por alguna razon si le hacemos el override, no toma este id.
        //public override Guid Id { get; set; }

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

        //Este campo tiene que ser autogenerado al momento de dar de alta a la persona.
        [Display(Name = "Fecha de Alta")]
        public DateTime FechaAlta { get; set; } = DateTime.UtcNow;

       // [Required(ErrorMessage = ErrMsgs.Requerido)]
       // [EmailAddress(ErrorMessage = ErrMsgs.Formato)]
       // [StringLength(Cfgs.UsuarioMailMax, MinimumLength = Cfgs.UsuarioMailMin, ErrorMessage = ErrMsgs.RangoTexto)]
       // public override string Email { get; set; }

        
        [DataType(DataType.Password)]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = ErrMsgs.PasswordErrorCaracteres)]
        [StringLength(Cfgs.UsuarioPasswordMax, MinimumLength = Cfgs.UsuarioPasswordMin, ErrorMessage = ErrMsgs.RangoTexto)]
        public string Password { get; set; }

        [NotMapped]
        [Display(Name = "Nombre y Apellido")]
        public string NombreApellido { get { return Nombre + ", " + Apellido; } }
    }
}
