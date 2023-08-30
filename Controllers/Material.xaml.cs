using Museo.Views;
using Museoapp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
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
        private void CrearArchivo_Click(object sender, RoutedEventArgs e)
        {
            Archivo archivo = new Archivo();
            archivo.Show();
            this.Close();

        }
        private void CrearColeccion_Click(object sender, RoutedEventArgs e)
        {
            Coleccion coleccion = new Coleccion();

            coleccion.Show();
            this.Close();


        }

        private void CrearLibro_Click(object sender, RoutedEventArgs e)
        {
            Libro libro = new Libro();
            libro.Show();
            this.Close();

        }

        private void CrearListaLibro_Click(object sender, RoutedEventArgs e)
        {
            ListaLibros listalibro = new ListaLibros();
            listalibro.Show();
            this.Close();

        }

        private void CrearListaColeccion_Click(object sender, RoutedEventArgs e)
        {
            ListaColecciones listaColeccion = new ListaColecciones();
            listaColeccion.Show();
            this.Close();

        }

        private void CrearAutor_Click(object sender, RoutedEventArgs e)
        {
            Autor autor = new Autor();
            autor.Show();
            this.Close();

        }


        private void ListarMateriales()
        {
            string connectionString = "server=DESKTOP-9MTUTME; database=Museo1 ; integrated security = true";
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

                dataGrid.ItemsSource = materiales; // Asignar la lista de materiales al DataGrid
            }
        }


        private void EnviarMaterial_Click(object sender, RoutedEventArgs e)
        {
            var conexion = new SqlConnection("server=DESKTOP-9MTUTME; database=Museo1 ; integrated security = true");
            conexion.Open();

            string query = "SELECT COUNT(*) FROM Material WHERE Nombre = @Nombre";
            SqlCommand verificarComando = new SqlCommand(query, conexion);
            verificarComando.Parameters.AddWithValue("@Nombre", Nombre1.Text.ToLower());

            int count = (int)verificarComando.ExecuteScalar();

            if (count > 0)
            {
                MessageBox.Show("El material ya existe");
            }
            else
            {
                string insertQuery = "INSERT INTO Material (Nombre) VALUES (@Nombre)";
                Materiales materialin = new Materiales();
                SqlCommand insertComando = new SqlCommand(insertQuery, conexion);

                TextInfo Mayuscula = CultureInfo.CurrentCulture.TextInfo;
                materialin.Nombre = Mayuscula.ToTitleCase(Nombre1.Text.ToLower());

                insertComando.Parameters.AddWithValue("@Nombre", materialin.Nombre);

                insertComando.ExecuteNonQuery();
                MessageBox.Show("Se ingresó un material");
            }

            conexion.Close();

            LimpiarCampos();
            ListarMateriales();
        }
        private void BuscarPorNombre(object sender, RoutedEventArgs e)
        {
            string textoBusqueda = PorNombre.Text.Trim();

            for (int i = 0; i < dataGrid.Items.Count; i++)
            {
                Materiales item = (Materiales)dataGrid.Items[i];
                DataGridRow dataGridRow = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(i);

                if (item.Nombre.Contains(textoBusqueda))
                {
                    // Mostrar la fila si coincide con la búsqueda
                    dataGridRow.Visibility = Visibility.Visible;
                }
                else
                {
                    // Ocultar la fila si no coincide con la búsqueda
                    dataGridRow.Visibility = Visibility.Collapsed;
                }
            }

            PorNombre.Text = "";
        }
        private void Refrescar(Object sender, RoutedEventArgs e)
        {
            //// Mostrar todos los elementos de la lista nuevamente
            //for (int i = 0; i < listView.Items.Count; i++)
            //{
            //    ListViewItem listViewItem = listView.ItemContainerGenerator.ContainerFromIndex(i) as ListViewItem;
            //    if (listViewItem != null)
            //    {
            //        // Mostrar el elemento
            //        listViewItem.Visibility = Visibility.Visible;
            //    }
            //}

            //// Limpiar el campo de búsqueda
            //PorNombre.Text = "";
        }
        private void LimpiarCampos()
        {
            Nombre1.Text = "";

        }
        private void Eliminar_Click(Object sender, RoutedEventArgs e) { }
        private void Editar_Click(Object sender, RoutedEventArgs e) { }
        public partial class EditarMaterial : Window
        {
            public EditarMaterial()
            {
              
            }
        }

    }
}
