using Museoapp.Models;
using System;
using System.Collections.Generic;
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
using WpfAppTEST.Models;

namespace Museoapp.Views
{
    /// <summary>
    /// Lógica de interacción para Autor.xaml
    /// </summary>
    public partial class Autor : Window
    {
        public Autor()
        {
            InitializeComponent();

            ListarAutores();

        }



        private void ListarAutores()
        {
            string connectionString = "server=DESKTOP-TI2N3QM; database=Museo1 ; integrated security = true";
            string query = "SELECT [Nombre], [Apellido]  FROM [dbo].[Autor]";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                List<Autores> autores = new List<Autores>();

                while (reader.Read())
                {
                    Autores autorin = new Autores();
                    autorin.Nombre = reader.GetString(0);
                    autorin.Apellido = reader.GetString(1);
                    autores.Add(autorin);
                }

                listView.ItemsSource = autores;
            }
        }

        private void EnviarAutor_Click(object sender, RoutedEventArgs e)
        {

            var conexion = new SqlConnection("server=DESKTOP-TI2N3QM; database=Museo1 ; integrated security = true");
            conexion.Open();
     

            string query = "insert into Autor(Nombre, Apellido) " +
               "Values (@Nombre, @Apellido)";

            Autores autorin = new Autores();

            SqlCommand comand = new SqlCommand(query, conexion);


            autorin.Nombre = Nombre1.Text;
            autorin.Apellido = Apellido1.Text;



            comand.Parameters.AddWithValue("@Nombre", autorin.Nombre);
            comand.Parameters.AddWithValue("@Apellido", autorin.Apellido);
          

            comand.ExecuteNonQuery();
            MessageBox.Show("Se ingreso un Autor");
            conexion.Close();

            //Limpiar campos al ingresar
            Nombre1.Text = "";
            Apellido1.Text = "";
        }

        private void BuscarPorNombre(object sender, RoutedEventArgs e)
        {
            string textoBusqueda = PorNombre.Text.Trim();

            for (int i = 0; i < listView.Items.Count; i++)
            {
                Materiales item = (Materiales)listView.Items[i];
                ListViewItem listViewItem = listView.ItemContainerGenerator.ContainerFromIndex(i) as ListViewItem;

                if (item.Nombre.Contains(textoBusqueda))
                {
                    // Mostrar el elemento si coincide con la búsqueda
                    listViewItem.Visibility = Visibility.Visible;
                }
                else
                {
                    // Ocultar el elemento si no coincide con la búsqueda
                    listViewItem.Visibility = Visibility.Collapsed;
                }
            }

            PorNombre.Text = "";
        }
        private void Refrescar(Object sender, RoutedEventArgs e)
        {
            // Mostrar todos los elementos de la lista nuevamente
            for (int i = 0; i < listView.Items.Count; i++)
            {
                ListViewItem listViewItem = listView.ItemContainerGenerator.ContainerFromIndex(i) as ListViewItem;
                if (listViewItem != null)
                {
                    // Mostrar el elemento
                    listViewItem.Visibility = Visibility.Visible;
                }
            }

            // Limpiar el campo de búsqueda
            PorNombre.Text = "";
        }

    }
}
