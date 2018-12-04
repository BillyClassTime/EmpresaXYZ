using EmpresaXYZ.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace EmpresaXYZ
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private EmpresaXYZEntities dBContext = null;
        private Branch sucursal = null;
        IList<Employee> listaEmpleados = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dBContext = new EmpresaXYZEntities();
            IList<Branch> branch = (from a in dBContext.Branches
                                    orderby a.BranchName
                                    select a).ToList();
            cbSucursales.ItemsSource = branch;
            cbSucursales.DisplayMemberPath = "BranchName";
            cbSucursales.SelectedValuePath = "BranchID";

        }

        private void cbSucursales_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sucursal = (Branch) cbSucursales.SelectedItem;
            listaEmpleados = (from a in dBContext.Employees
                              where a.Branch == sucursal.BranchID
                              select a).ToList();
            if (listaEmpleados.Count == 0)
                MessageBox.Show(
          $"La sucursal:{sucursal.BranchName}, no tiene empleados",
          "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
            ActualizarListaEmpleados();
        }

        private void ActualizarListaEmpleados()
        {
            lvEmpleados.ItemsSource = null;
            lvEmpleados.ItemsSource = listaEmpleados;
            lvEmpleados.DisplayMemberPath = "FirtsName";
        }

        private void lvEmpleados_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    Employee empleado = (Employee) lvEmpleados.SelectedItem;
                    EditarEmpleado(empleado);
                    break;
                case Key.Insert:
                    InsertarNuevoEmpleado();
                    break;
                case Key.Delete:
                    empleado = lvEmpleados.SelectedItem as Employee;
                    BorrarEmpleado(empleado);
                    break;
                default:
                    break;
            }
        }

        private void BorrarEmpleado(Employee empleado)
        {
            MessageBoxResult respuesta = MessageBox.Show(
          $"¿Borrar al empleado:{empleado.FirstName} {empleado.LastName}?",
          "Confirme", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (respuesta == MessageBoxResult.Yes)
            {
                listaEmpleados.Remove(empleado);
                dBContext.Employees.Remove(empleado);
                SavarBD.IsEnabled = true;
                ActualizarListaEmpleados();
            }
        }
        private void InsertarNuevoEmpleado()
        {
            Empleados fEmp = new Empleados();
            fEmp.Title =
                $"Nuevo empleado para la sucursal:{sucursal.BranchName}";
            fEmp.Sucursal.Text = sucursal.BranchName;
            if (fEmp.ShowDialog().Value)
            {
                Employee newEmp = new Employee
                {
                    EmployeeID = int.Parse(fEmp.ID.Text),
                    FirstName = fEmp.Nombre.Text,
                    LastName = fEmp.Apellido.Text,
                    //DateOfBirth = DateTime.ParseExact(
                    //    fEmp.FechaNacimiento.Text,"MM/dddd/YYYY",
                    //    CultureInfo.InvariantCulture),
                    DateOfBirth = DateTime.Parse(fEmp.FechaNacimiento.Text),
                    JobTitle = int.Parse(fEmp.Cargo.Text)
                };
                //Asignamos el empleado a la sucursal
                sucursal.Employees.Add(newEmp);
                // Añadimos el empleado a la lista
                listaEmpleados.Add(newEmp);
                ActualizarListaEmpleados();
                SavarBD.IsEnabled = true;
            }
        }

        private void EditarEmpleado(Employee empleado)
        {
            var fEmp = new Empleados
            {
                Title = $"Editando el empleado:{empleado.FirstName} {empleado.LastName} "
            };
            fEmp.ID.Text = empleado.EmployeeID.ToString();
            fEmp.ID.IsEnabled = false;
            fEmp.Nombre.Text = empleado.FirstName;
            fEmp.Apellido.Text = empleado.LastName;
            fEmp.FechaNacimiento.Text = empleado.DateOfBirth.ToString();
            fEmp.Sucursal.Text = sucursal.BranchName;
            fEmp.Cargo.Text = empleado.JobTitle.ToString();
            fEmp.SucursalId = sucursal.BranchID;
            if (fEmp.ShowDialog().Value)
            {
                empleado.EmployeeID = int.Parse(fEmp.ID.Text);
                empleado.FirstName = fEmp.Nombre.Text;
                empleado.LastName = fEmp.Apellido.Text;
                //DateOfBirth = DateTime.ParseExact(
                //    fEmp.FechaNacimiento.Text,"MM/dddd/YYYY",
                //    CultureInfo.InvariantCulture),
                empleado.DateOfBirth = DateTime.Parse(fEmp.FechaNacimiento.Text);
                empleado.JobTitle = int.Parse(fEmp.Cargo.Text);
                //actualizamos el listView
                ActualizarListaEmpleados();
                SavarBD.IsEnabled = true;
            }
        }

        private void lvEmpleados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var empleado = (Employee)lvEmpleados.SelectedItem;
            EditarEmpleado(empleado);
        }

        private void SavarBD_Click(object sender, RoutedEventArgs e)
        {
           /* try
            {*/
                dBContext.SaveChanges();
                SavarBD.IsEnabled = false;
            /*}
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al guardar:{ex.InnerException.Message}");
            }*/
        }

        private void Salir_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void ImportarXML_Click(object sender, RoutedEventArgs e)
        {
            Employee newEmp = new Employee();
            var filename = "EmpleadoXML_In.xml";
            var seria = new Serializar();
            var resultado = seria.Deserializando(filename, out newEmp, out bool proceso);
            if (proceso)
            {
                //Asignamos el empleado a la sucursal
                sucursal.Employees.Add(newEmp);
                // Añadimos el empleado a la lista
                listaEmpleados.Add(newEmp);
                ActualizarListaEmpleados();
                SavarBD.IsEnabled = true;
            }
            else
            {
                MessageBox.Show(resultado, "No se ha insertado el empleado", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
