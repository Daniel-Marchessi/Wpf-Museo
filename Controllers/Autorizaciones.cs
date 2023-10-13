using Museo.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Museo.Controllers
{
    internal class Autorizaciones
    {
        public void Autorizacion(object sender, RoutedEventArgs e, DataGridColumn columnaAEditar, DataGridColumn columnaAEliminar)
        {
            string rol = Usuarios.RolUsuario.ToString();
            string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                string query = "SELECT Nombre, Password, Rol FROM Usuario";
                SqlCommand comand = new SqlCommand(query, conexion);
                SqlDataReader registro = comand.ExecuteReader();
                while (registro.Read())
                {
                    if (rol == "Admin")
                    {
                        columnaAEditar.Visibility = Visibility.Visible;
                        columnaAEliminar.Visibility = Visibility.Visible;
                        break;
                    }
                    else
                    {
                        columnaAEliminar.Visibility = Visibility.Collapsed;
                        columnaAEditar.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }
    }

}
