using Microsoft.EntityFrameworkCore;
using ReservaEspectaculo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaEspectaculo.Data
{
    public class Helpers
    {

        public static bool ValidarFechaDesde(DateTime fechaDesde, DateTime fecha)
        {

            return fechaDesde.Date.CompareTo(fecha.Date) <= 0;
        }

        public static bool ExisteFuncion(Funcion func, Sala sala, Context contexto)
        {
            bool result = false;
            int i = 0;
            var funciones = contexto.Funciones.AsNoTracking().Include(f => f.Sala).ToArray();
            while (i < funciones.Count() && result == false)
            {
                result = ValidacionDosHoras(funciones[i].Hora, func.Hora)
                    && funciones[i].Sala.Numero == sala.Numero
                    && funciones[i].Fecha == func.Fecha
                    && funciones[i].Id != func.Id;
                i++;
            }

            return result;
        }

        public static bool ValidarButacas(int cant, Funcion funcion)
        {
            var butacasDisponibles = 0;
            var butacasReservadas = 0;
            foreach (Reserva r in funcion.Reservas)
            {
                    butacasReservadas += r.CantidadButacas;
            }
            butacasDisponibles = funcion.Sala.CapacidadButacas - butacasReservadas;

            return butacasDisponibles >= cant;
        }

        public static bool ValidarButacasEdit(Reserva reserva, Funcion funcion) {
            var butacasDisponibles = 0;
            var butacasReservadas = 0;
            foreach(Reserva r in funcion.Reservas){
                if (r.Id != reserva.Id) {
                    butacasReservadas += r.CantidadButacas;
                }
            }
            butacasDisponibles = funcion.Sala.CapacidadButacas - butacasReservadas;

            return butacasDisponibles >= reserva.CantidadButacas;
        }

        public static bool TieneFunciones(Sala sala)
        {
            return sala.Funciones.Any();
        }

        private static bool ValidacionDosHoras(DateTime funcionCreada, DateTime funcionACrear) {
            TimeSpan horaFuncionCreada = funcionCreada.TimeOfDay;
            TimeSpan horaFuncionACrear = funcionACrear.TimeOfDay;
            TimeSpan antesDe = horaFuncionCreada.Add(new TimeSpan(-2, 0, 0));
            TimeSpan despuesDe = horaFuncionCreada.Add(new TimeSpan(2, 0, 0));
            return horaFuncionACrear.CompareTo(antesDe) > 0 && horaFuncionACrear.CompareTo(despuesDe) < 0;
        }

        public static bool PuedeCancelar(Reserva reserva) {
            var fechaFuncion = reserva.Funcion.Hora;
            TimeSpan diferencia = reserva.FechaAlta - fechaFuncion;
            return diferencia.TotalDays <= -1;
        }

        public static bool esPasada(Funcion funcion) {
            return funcion.Fecha.CompareTo(DateTime.Now) < 0;
        }

        public static bool tieneReservasActivas(Cliente cliente) {
            bool tiene = false;
            foreach (Reserva r in cliente.Reservas) {
                if (r.Funcion.Fecha.CompareTo(DateTime.Now) >= 0) {
                    tiene = true;
                }
            }
            return tiene;
        }


    }
}
