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
using Microsoft.Win32;
using Museo.Controllers;
using Museo.Models;

using Museoapp.Models;

using WpfAppTEST.Models;

namespace Museo.Views
{
    /// <summary>
    /// Lógica de interacción para historialdeusuarios.xaml
    /// </summary>
    public partial class historialdeusuarios : Window
    {
        public historialdeusuarios()
        {
            InitializeComponent();
            mostrarusuarios();
        }

        private void mostrarusuarios()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                //Hago una sola consulta join para traer todo
                string query = "SELECT Usuario.Usuario_id, Usuario.Nombre, Usuario.Rol, RegistrosAcceso.FechaHoraAcceso, " +
                    "RegistrosAcceso.FechaHoraSalida FROM Usuario JOIN RegistrosAcceso " +
                    "ON Usuario.Usuario_id = RegistrosAcceso.UsuarioId";

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                List<Usuarios> listaUsuariosRegistros = new List<Usuarios>();

                while (reader.Read())
                {
                    //En el modelo de usuario defino datos como datetime etc
                    Usuarios usuarioRegistro = new Usuarios();
                    usuarioRegistro.UsuarioId = reader.GetInt32(0);
                    usuarioRegistro.Usuario = reader.GetString(1);
                    usuarioRegistro.RolUsuario1 = reader.GetString(2);
                    usuarioRegistro.FechaHoraAcceso = reader.GetDateTime(3);
                    usuarioRegistro.FechaHoraSalida = reader.GetDateTime(4);

                    listaUsuariosRegistros.Add(usuarioRegistro);
                }

                reader.Close();
                connection.Close();

                // Asigna la lista de UsuarioRegistro como origen de datos del DataGrid
                dataGrid.ItemsSource = listaUsuariosRegistros;
            }
        }


        private void EliminarRegistros_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

              
                string eliminarRegistrosQuery = "DELETE FROM RegistrosAcceso";

                using (SqlCommand eliminarCommand = new SqlCommand(eliminarRegistrosQuery, connection))
                {
                    MessageBoxResult result = MessageBox.Show("¿Estás seguro de eliminar todos los registro?", "Confirmación de eliminación", MessageBoxButton.YesNo);


                    if (result == MessageBoxResult.Yes)
                    {
                        eliminarCommand.ExecuteNonQuery();
                        MessageBox.Show("Los registros se eliminaron correctamente.");
                    }
                }

                mostrarusuarios();
            }
        }
    }
}
