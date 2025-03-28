using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;

namespace ConexionDB
{
    /// <summary>
    /// Interaction logic for Actualizar.xaml
    /// </summary>
    public partial class Actualizar : Window
    {
        SqlConnection conexionSql;

        private int idCliente;

        public Actualizar(int idClientes)
        {
            InitializeComponent();

            idCliente = idClientes;

            string cc = ConfigurationManager.ConnectionStrings["ConexionDB.Properties.Settings.Prueba1ConnectionString"].ConnectionString;

            conexionSql = new SqlConnection(cc);


        }


        //UPDATE

        // Actualizar Clientes
        private void actualizarClientes()
        {
            try
            {
                string consulta = "update Clientes set nombre = @nombre where idCliente = " + idCliente;

                SqlCommand comandoSql = new SqlCommand(consulta, conexionSql);


                //Ejecutar la consulta que borrara el registro
                conexionSql.Open();

                comandoSql.Parameters.AddWithValue("@nombre", cuadroActualizar.Text);

                comandoSql.ExecuteNonQuery();

                conexionSql.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show("No se pudo insertar el cliente\n" + e.ToString());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            actualizarClientes();

            MessageBox.Show("Usuario actualizado con exito!");

            this.Close();
        }
    }
}
