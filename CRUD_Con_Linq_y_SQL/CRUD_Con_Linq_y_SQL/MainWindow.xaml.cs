using System;
using System.Collections.Generic;
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
using System.Configuration;

namespace CRUD_Con_Linq_y_SQL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LinqDCDataContext dataContext;
        public MainWindow()
        {
            InitializeComponent();

            string conexionSql = ConfigurationManager.ConnectionStrings["CRUD_Con_Linq_y_SQL.Properties.Settings.Crud_LinqConnectionString"].ConnectionString;

            dataContext = new LinqDCDataContext(conexionSql);

            
        }

        //Insert / CREATE
        public void insertarEmpresa()
        {
            Empresa empresa = new Empresa();

            empresa.nombreEmpresa = "Google";

            dataContext.Empresas.InsertOnSubmit(empresa);

            Empresa empresa2 = new Empresa();

            empresa2.nombreEmpresa = "Amazon";

            dataContext.Empresas.InsertOnSubmit(empresa2);

            dataContext.SubmitChanges();

            Principal.ItemsSource = dataContext.Empresas;
        }

        public void insertarEmpleados()
        {
            Empresa empresa = dataContext.Empresas.First(em => em.nombreEmpresa.Equals("Google"));
            Empresa empresa2 = dataContext.Empresas.First(em => em.nombreEmpresa.Equals("Amazon"));

            List<Empleado> listaEmpleados = new List<Empleado>();

            listaEmpleados.Add(new Empleado { nombreEmpleado = "Jose", apellidoEmpleado = "Martinez", idEmpresa = empresa.idEmpresa });
            listaEmpleados.Add(new Empleado { nombreEmpleado = "Daniel", apellidoEmpleado = "Guerra", idEmpresa = empresa2.idEmpresa });
            listaEmpleados.Add(new Empleado { nombreEmpleado = "Maria", apellidoEmpleado = "Dolores", idEmpresa = empresa.idEmpresa });
            listaEmpleados.Add(new Empleado { nombreEmpleado = "Antonio", apellidoEmpleado = "Fernandez", idEmpresa = empresa2.idEmpresa });

            dataContext.Empleados.InsertAllOnSubmit(listaEmpleados);

            dataContext.SubmitChanges();

            Principal.ItemsSource = dataContext.Empleados;
        }

        public void insertarCargo()
        {
            List<Cargo> listaCargos= new List<Cargo>();

            listaCargos.Add(new Cargo { nombreCargo = "CEO" });
            listaCargos.Add(new Cargo { nombreCargo = "Programador" });
            listaCargos.Add(new Cargo { nombreCargo = "Obrero" });

            dataContext.Cargos.InsertAllOnSubmit(listaCargos);

            dataContext.SubmitChanges();

            Principal.ItemsSource= dataContext.Cargos;
        }

        public void insertarCargoEmpleado()
        {
            Empleado empleado = dataContext.Empleados.First(ld => ld.idEmpleado == 1);
            Empleado empleado2 = dataContext.Empleados.First(ld => ld.idEmpleado == 2);
            Empleado empleado3 = dataContext.Empleados.First(ld => ld.idEmpleado == 3);
            Empleado empleado4 = dataContext.Empleados.First(ld => ld.idEmpleado == 4);

            Cargo cargo = dataContext.Cargos.First(ld => ld.nombreCargo.Equals("CEO"));
            Cargo cargo2 = dataContext.Cargos.First(ld => ld.nombreCargo.Equals("Programador"));
            Cargo cargo3 = dataContext.Cargos.First(ld => ld.nombreCargo.Equals("Obrero"));
            
            Cargo_Empleado ce = new Cargo_Empleado();
            Cargo_Empleado ce2 = new Cargo_Empleado();
            Cargo_Empleado ce3 = new Cargo_Empleado();
            Cargo_Empleado ce4 = new Cargo_Empleado();

            ce.idEmpleado = empleado.idEmpleado;
            ce2.idEmpleado = empleado2.idEmpleado;
            ce3.idEmpleado = empleado3.idEmpleado;
            ce4.idEmpleado = empleado4.idEmpleado;

            ce.idCargo = cargo.idCargo;
            ce2.idCargo = cargo2.idCargo;
            ce3.idCargo = cargo3.idCargo;
            ce4.idCargo = cargo3.idCargo;

            dataContext.Cargo_Empleados.InsertOnSubmit(ce);
            dataContext.Cargo_Empleados.InsertOnSubmit(ce2);
            dataContext.Cargo_Empleados.InsertOnSubmit(ce3);
            dataContext.Cargo_Empleados.InsertOnSubmit(ce4);

            dataContext.SubmitChanges();

            Principal.ItemsSource = dataContext.Cargo_Empleados;
        }


        // Actualizar / UPDATE
        public void actualizarEmpleado()
        {
            Empleado empleado = dataContext.Empleados.First(emp => emp.idEmpleado == 3);

            empleado.nombreEmpleado = "Ingresar el nuevo nombre";

            dataContext.Empleados.InsertOnSubmit(empleado);

            dataContext.SubmitChanges();

            Principal.ItemsSource = dataContext.Empleados;
        }

        // Borrar / DELETE
        public void borrarEmpleado()
        {
            Empleado empleado = dataContext.Empleados.First(emp => emp.idEmpleado == 3);

            dataContext.Empleados.DeleteOnSubmit(empleado);

            dataContext.SubmitChanges();

            Principal.ItemsSource = dataContext.Empleados;
        }
    }
}
