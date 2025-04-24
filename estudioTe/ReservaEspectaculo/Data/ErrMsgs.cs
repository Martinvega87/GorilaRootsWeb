using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaEspectaculo.Data
{
    public static class ErrMsgs
    {
        //Para modelos
        public const string Requerido = "El campo {0} es requerido";
        public const string RangoTexto = "El campo {0} debe tener entre {2} y {1} caracteres.";
        public const string MaxTexto = "El campo {0} no puede tener mas de {1} caracteres";
        public const string RangoNumero = "El campo {0} debe encontrarse entre {1} y {2}";
        public const string Formato = "Formato incorrecto.";
        public const string RangoButacasReserva = "Ingrese una cantidad positiva de butacas.";
        public const string PasswordErrorCaracteres = "La Password debe tener al menos 8 caracteres y contener 3 o 4 de los siguienres: Mayùsculas (A-Z), Minùsculas (a-z), nùmeros(0-9) y caracteres especiales (ej. !@#$%^&*)";
        //Para validaciones en controllers
        public const string FechaFuncion = "La fecha debe ser igual o mayor al estreno de la pelicula.";
        public const string NumeroSala = "Este numero de sala ya se encuentra en uso.";
        public const string FuncionYaExistente = "Ya existe una funcion para esta fecha y horario en esta sala.";
        public const string LegajoRepetido = "Ya existe un empleado con este legajo.";
        public const string GeneroRepetido = "Ya existe este genero.";
        public const string PeliculaRepetida = "Esta pelicula ya se encuentra cargada en el sistema.";
        public const string MuchasButacas = "La funcion no tiene suficientes butacas disponibles";
        public const string TieneReserva = "Este usuario cuenta con una reserva activa";
        public const string FuncionConReservasElim = "Esta funcion ya cuenta con reservas y no puede ser eliminada.";
        public const string FuncionConReservasEdit = "Esta funcion ya cuenta con reservas y no puede ser editada.";
        //Validaciones registro y login
        public const string UsuarioYaExiste = "Ya existe un usuario registrado con este correo";
        public const string DebeLoguearse = "Para acceder a este recurso, primero debe Iniciar sesión";
        public const string ErrorLogin = "El mail o password es incorrecto.";
        public const string ErrorPasswordConfirm = "La password de confirmación no es igual";
    }
}
