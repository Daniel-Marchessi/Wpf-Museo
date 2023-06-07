using Museoapp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfAppTEST.Models;
using WpfAppTEST.Views;

namespace Museoapp.Views
{
    /// <summary>
    /// Lógica de interacción para Material.xaml
    /// </summary>
    public partial class Material : Window
    {
        public Material()
        {
            InitializeComponent();
            ListarMateriales();
        }
        private void ListarMateriales()
        {
            string connectionString = "server=DESKTOP-TI2N3QM; database=Museo1 ; integrated security = true";
            string query = "SELECT [Nombre] FROM [dbo].[Material]";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                List<Materiales> materiales = new List<Materiales>();

                while (reader.Read())
                {
                    Materiales material = new Materiales();
                    material.Nombre = reader.GetString(0);
                    materiales.Add(material);
                }

                listView.ItemsSource = materiales;
            }
        }


        private void EnviarMaterial_Click(object sender, RoutedEventArgs e)
        {
            var conexion = new SqlConnection("server=DESKTOP-TI2N3QM; database=Museo1 ; integrated security = true");
            conexion.Open();

            string query = "insert into Material(Nombre) " +
            "Values (@Nombre)";
            Materiales materialin = new Materiales();
            SqlCommand comand = new SqlCommand(query, conexion);

            materialin.Nombre = Nombre1.Text;

            comand.Parameters.AddWithValue("@Nombre", materialin.Nombre);

            comand.ExecuteNonQuery();
            MessageBox.Show("Se ingreso un Material");
            conexion.Close();

            //Limpiar campos al ingresar
            Nombre1.Text = "";
            ListarMateriales();
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
