using System.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmpresaXYZ.Data;
using System.IO;

namespace EmpresaXYZ
{
    class Serializar
    {
        private string resultado = String.Empty;

        public string Serializando(string filename, Employee empleado, out bool proceso)
        {
            TextWriter writer = null;
            proceso = false;
            try
            {
                var empleadoSD = new EmployeeSD
                {
                    EmployeeID = empleado.EmployeeID,
                    FirstName = empleado.FirstName,
                    LastName = empleado.LastName,
                    DateOfBirth = (DateTime)empleado.DateOfBirth,
                    Branch = (int)empleado.Branch,
                    JobTitle = (int)empleado.JobTitle
                };

                // Create an instance of the XmlSerializer class; specify the type of object to serialize.
                var serializer = new XmlSerializer(typeof(EmployeeSD));

                writer = new StreamWriter(filename);
                serializer.Serialize(writer, empleadoSD);
                resultado = "Serialización realizado correctamente";
                proceso = true;
            }
            catch (Exception ex)
            {
                resultado = ex.Message.ToString();
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
            return resultado;
        }
        public string Deserializando(string filename, out Employee empleado,out bool proceso)
        {
            // Declare an object variable of the type to be deserialized.
            var empleadoDS = new EmployeeSD();
            empleado = new Employee();
            proceso = false;
            FileStream fs = null;
            try
            {
                // Create an instance of the XmlSerializer class;// specify the type of object
                // to be deserialized.
                var serializer = new XmlSerializer(typeof(EmployeeSD));

                /* If the XML document has been altered with unknown nodes or attributes,
                 handle them with the UnknownNode and UnknownAttribute events.*/
                serializer.UnknownNode += new XmlNodeEventHandler(Serializer_UnknownNode);
                serializer.UnknownAttribute += new
                XmlAttributeEventHandler(serializer_UnknownAttribute);

                // A FileStream is needed to read the XML document.
                fs = new FileStream(filename, FileMode.Open);

                /* Use the Deserialize method to restore the object's state with
                data from the XML document. */
                empleadoDS = (EmployeeSD)serializer.Deserialize(fs);
                empleado.EmployeeID = empleadoDS.EmployeeID;
                empleado.FirstName = empleadoDS.FirstName;
                empleado.LastName = empleadoDS.LastName;
                empleado.DateOfBirth = (DateTime)empleadoDS.DateOfBirth;
                empleado.Branch = (int)empleadoDS.Branch;
                empleado.JobTitle = (int)empleadoDS.JobTitle;
                resultado = "fichero deserializado correctamente";
                proceso = true;
            }
            catch (Exception ex)
            {
                resultado = ex.Message.ToString();
                empleado = null;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
            return resultado;
        }


        private void Serializer_UnknownNode(object sender, XmlNodeEventArgs e)
        {
            resultado += $@"Unknown Node:{e.Name} - {e.Text}{System.Environment.NewLine}";
        }

        private void serializer_UnknownAttribute(object sender, XmlAttributeEventArgs e)
        {
            System.Xml.XmlAttribute attr = e.Attr;
            resultado += $"Unknown attribute {attr.Name}='{attr.Value}{System.Environment.NewLine}";
        }

    }

    [XmlRootAttribute("EmpresaXYZemp", Namespace = "http://www.empresaxyz.com", IsNullable = false)]
    public class EmployeeSD
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Branch { get; set; }
        public int JobTitle { get; set; }
    }

}
