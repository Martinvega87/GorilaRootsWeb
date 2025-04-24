using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLProyectoCeleste;

namespace BL
{
    public class prueba
    {
        public DataSet tPruebaDataSet(int id)
        {
            manager MG = new manager();

            // Utiliza el parámetro id en la consulta SQL
            string consulta = $"SELECT [id],[nombre],[apellido] FROM [dbo].[tablaPrueba1] WHERE id = {id}";

            return MG.ExecuteDataSet(consulta);
        }
    }
}
