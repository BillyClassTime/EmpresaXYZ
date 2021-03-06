﻿using EmpresaXYZ.Data;
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
using System.Windows.Shapes;

namespace EmpresaXYZ
{
    /// <summary>
    /// Interaction logic for Empleados.xaml
    /// </summary>
    public partial class Empleados : Window
    {
        public int SucursalId;
        public Empleados()
        {
            InitializeComponent();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Salir_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void ExportarXML_Click(object sender, RoutedEventArgs e)
        {
            var newEmp = new Employee()
            {
                EmployeeID = int.Parse(ID.Text),
                FirstName = Nombre.Text,
                LastName = Apellido.Text,
                //DateOfBirth = DateTime.ParseExact(
                //    fEmp.FechaNacimiento.Text,"MM/dddd/YYYY",
                //    CultureInfo.InvariantCulture),
                DateOfBirth = DateTime.Parse(FechaNacimiento.Text),
                JobTitle = int.Parse(Cargo.Text),
                Branch = SucursalId
            };
            var seria = new Serializar();
            var filename = "EmpleadoXML_Out.xml";
            var resultado = seria.Serializando(filename, newEmp, out bool proceso);

            resultado = proceso ? $"Llamada:{resultado}" : $"Error:{resultado}";
            MessageBox.Show(resultado, "Mensaje de la exportación",
                    MessageBoxButton.OK, MessageBoxImage.Information);

        }
    }
}
