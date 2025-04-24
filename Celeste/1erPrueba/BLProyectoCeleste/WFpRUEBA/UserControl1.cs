using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BL;

namespace WFpRUEBA
{
    public partial class UserControl1: UserControl
    {
        public UserControl1()
        {
            InitializeComponent();

        }

        private void UserControl1_Load(object sender, EventArgs e)
        {

            try
            {
                // Crear una instancia de la clase 'prueba'
                prueba miPrueba = new prueba();

                // Obtener el conjunto de datos utilizando el método 'tPruebaDataSet'
                DataSet resultadoDataSet = miPrueba.tPruebaDataSet(3);

                // Verificar si el conjunto de datos contiene al menos una tabla y si es así, asignarlo al DataGridView
                if (resultadoDataSet != null && resultadoDataSet.Tables.Count > 0)
                {
                    dgvPtueba.DataSource = resultadoDataSet.Tables[0];
                }
                else
                {
                    // Manejar el caso en el que el conjunto de datos está vacío
                    MessageBox.Show("No se encontraron datos.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la carga de datos
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
    }
}
