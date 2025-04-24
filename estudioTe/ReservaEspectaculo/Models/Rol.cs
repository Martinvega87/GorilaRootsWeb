using Microsoft.AspNetCore.Identity;
using ReservaEspectaculo.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaEspectaculo.Models
{
    public class Rol: IdentityRole<Guid>
    {
    }
}
