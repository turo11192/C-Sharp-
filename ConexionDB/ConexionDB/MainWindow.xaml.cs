using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace ConexionDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection conexionSql;

        public MainWindow()
        {
            InitializeComponent();

            string cc = ConfigurationManager.ConnectionStrings["ConexionDB.Properties.Settings.Prueba1ConnectionString"].ConnectionString;

            conexionSql = new SqlConnection(cc);

            //Mostramos los datos (mandamos a llamar la funcion)
            mostrarClientes();
            mostrarTodosPedidos();
        }


        //CREATE (Insertar registros)

        //Insertar un cliente (solo con nombre por el momento)

        private void insertarCliente()
        {
            try
            {
                string consulta = "insert into Clientes (nombre) value (@nombre)";

                SqlCommand comandoSql = new SqlCommand(consulta, conexionSql);


                //Ejecutar la consulta que borrara el registro
                conexionSql.Open();

                comandoSql.Parameters.AddWithValue("@nombre", insertarClientes.Text);

                comandoSql.ExecuteNonQuery();

                conexionSql.Close();

                //Refrescar los registros
                mostrarClientes();

                //Limpiar el cuadro de texto
                insertarClientes.Text = "";
            }
            catch (Exception e)
            {
                MessageBox.Show("No se pudo insertar el cliente\n" + e.ToString());
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            insertarCliente();
        }

       

        //-----READ AND SHOW-----

        //Hacer la consulta y almacenar (en un datatable) la informacion que viene (Cargar los clientes)
        private void mostrarClientes()
        {
            try
            {
                string consulta = "select * from Clientes";

                SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexionSql);

                using (adaptador)
                {
                    DataTable tablaClientes = new DataTable();

                    adaptador.Fill(tablaClientes);

                    listaClientes.DisplayMemberPath = "nombre"; //Especificar que datos queremos mostrar (sin esto no sale nada)
                    listaClientes.SelectedValuePath = "idCliente"; //Especificar el campo clave
                    listaClientes.ItemsSource = tablaClientes.DefaultView; //Especificar el origen de los datos (el origen de los datos viene del datatable de donde se esta insertando la tabla entera)
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("No se pueden mostrar los clientes\n" + e.ToString());
            }
        }

        //Mostrar pedidos (Funcion)
        private void mostrarPedidos()
        {
            try
            {
                string consulta = "select * from Pedidos p inner join Clientes c on c.idCliente = p.idCliente" +
                " where c.idCliente = @idCliente";

                SqlCommand comandoSql = new SqlCommand(consulta, conexionSql);

                SqlDataAdapter adaptador = new SqlDataAdapter(comandoSql);

                using (adaptador)
                {
                    comandoSql.Parameters.AddWithValue("@idCliente", listaClientes.SelectedValue);

                    DataTable tablaPedidos = new DataTable();

                    adaptador.Fill(tablaPedidos);

                    listaPedidos.DisplayMemberPath = "fechaPedido"; //Especificar que datos queremos mostrar (sin esto no sale nada)
                    listaPedidos.SelectedValuePath = "idPedido"; //Especificar el campo clave (Llave primaria)
                    listaPedidos.ItemsSource = tablaPedidos.DefaultView; //Especificar el origen de los datos (el origen de los datos viene del datatable de donde se esta insertando la tabla entera)
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("No se pueden mostrar los pedidos\n" + e.ToString());
            }
        }


        //Mostrar TODOS LOS PEDIDOS DE LA TIENDA VIRTUAL
        private void mostrarTodosPedidos()
        {
            try
            {
                string consulta = "select *, concat(idCliente, ' ', fechaPedido, ' ', formaPago) as datosPedido from Pedidos";

                SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexionSql);

                using (adaptador)
                {
                    DataTable tabla = new DataTable();

                    adaptador.Fill(tabla);

                    listaTodosPedidos.DisplayMemberPath = "datosPedido"; //Especificar que datos queremos mostrar (sin esto no sale nada)
                    listaTodosPedidos.SelectedValuePath = "idPedido"; //Especificar el campo clave
                    listaTodosPedidos.ItemsSource = tabla.DefaultView; //Especificar el origen de los datos (el origen de los datos viene del datatable de donde se esta insertando la tabla entera)
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("No se pueden mostrar todos los pedidos\n" + e.ToString());
            }
        }

        //Mostrar los pedidos de cada cliente seleccionado        
        private void listaClientes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            mostrarPedidos();
        }



        //UPDATE

        //Actualizar usuario

        private void trasladarInfo()
        {
            Actualizar ventanaActualizar = new Actualizar((int) listaClientes.SelectedValue);         

            try
            {
                string consulta = "select nombre from Clientes where idCliente = @idCliente";

                SqlCommand comandoSql = new SqlCommand(consulta, conexionSql);

                SqlDataAdapter adaptador = new SqlDataAdapter(comandoSql);

                using (adaptador)
                {
                    comandoSql.Parameters.AddWithValue("@idCliente", listaClientes.SelectedValue);

                    DataTable tablaCliente = new DataTable();

                    adaptador.Fill(tablaCliente);

                    ventanaActualizar.cuadroActualizar.Text = tablaCliente.Rows[0]["nombre"].ToString();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("No se pueden mostrar los clientes\n" + e.ToString());
            }

            //Mostramos la nueva ventana/Interfaz
            ventanaActualizar.ShowDialog();

            //Refrescamos la lista de Clientes
            mostrarClientes();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {            
            trasladarInfo();
        }

        //-----DELETE-----

        //Borrar registros (Pedidos)
        private void borrarPedidos()
        {
            try
            {
                string consulta = "delete from Pedidos where idPedido = @idPedido";

                SqlCommand comandoSql = new SqlCommand(consulta, conexionSql);


                //Ejecutar la consulta que borrara el registro
                conexionSql.Open();

                comandoSql.Parameters.AddWithValue("@idPedido", listaTodosPedidos.SelectedValue);

                comandoSql.ExecuteNonQuery();

                conexionSql.Close();

                //Refrescar los registros
                mostrarTodosPedidos();
            }
            catch(Exception e)
            {
                MessageBox.Show("No se pudo borrar el pedido\n" + e.ToString());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            borrarPedidos();
        }

        //Borrar un cliente
        private void borrarCliente()
        {
            try
            {
                string consulta = "delete from Clientes where idCliente = @idCliente";

                SqlCommand comandoSql = new SqlCommand(consulta, conexionSql);


                //Ejecutar la consulta que borrara el registro
                conexionSql.Open();

                comandoSql.Parameters.AddWithValue("@idCliente", listaClientes.SelectedValue);

                comandoSql.ExecuteNonQuery();

                conexionSql.Close();

                //Refrescar los registros
                mostrarClientes();
            }
            catch (Exception e)
            {
                MessageBox.Show("No se pudo borrar el Cliente\n" + e.ToString());
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            borrarCliente();
        }

        private void Window_Activated(object sender, EventArgs e)
        {

        }
    }
}
