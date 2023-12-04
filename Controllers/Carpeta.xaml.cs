using Museo.Models;
using Museoapp.Models;
using Museoapp.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
using System.Windows.Shapes;
using WpfAppTEST.Views;

namespace Museo.Views
{
    /// <summary>
    /// Lógica de interacción para Carpeta.xaml
    /// </summary>
    public partial class Carpeta : Window
    {
        public Carpeta()
        {
            InitializeComponent();
            ListarCarpetas();
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
        private void CrearMaterial_Click(object sender, RoutedEventArgs e)
        {
            Material material = new Material();
            material.Show();
            this.Close();
        }
        private void CrearEditorial_Click(object sender, RoutedEventArgs e)
        {
            Editorial editorial = new Editorial();
            editorial.Show();
            this.Close();

        }
        private void CrearCategoria_Click(object sender, RoutedEventArgs e)
        {
            Categoria categoria = new Categoria();
            categoria.Show();
            this.Close();
        }

        private void CrearArchivo_Click(object sender, RoutedEventArgs e)
        {
            Archivo archivo = new Archivo();
            archivo.Show();
            this.Close();

        }



        private void ListarCarpetas()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
            string query = "SELECT [id_carpeta], [NombreCarpeta] FROM [dbo].[Carpeta]";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                List<Carpetas> carpetas = new List<Carpetas>();

                while (reader.Read())
                {
                    Carpetas carpetin = new Carpetas();
                    carpetin.id_carpeta = reader.GetInt32(0);
                    carpetin.Nombre = reader.GetString(1);
                    carpetas.Add(carpetin);
                }

                dataGrid.ItemsSource = carpetas; // Asignar la lista de materiales al DataGrid
            }
        }




        private void BuscarPorNombre(object sender, RoutedEventArgs e)
        {
            TextInfo Mayuscula = CultureInfo.CurrentCulture.TextInfo;
            string textoBusqueda = Mayuscula.ToTitleCase(PorNombre.Text.Trim().ToLower());

            for (int i = 0; i < dataGrid.Items.Count; i++)
            {
                Carpetas item = (Carpetas)dataGrid.Items[i];
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


        private void EnviarCarpeta_click(object sender, RoutedEventArgs e)
        {
            var conexion = new SqlConnection("server=DESKTOP-TI2N3QM; database=Museo1 ; integrated security = true");
            conexion.Open();

            string query = "SELECT COUNT(*) FROM Carpeta WHERE NombreCarpeta = @Nombre";
            SqlCommand verificarComando = new SqlCommand(query, conexion);
            verificarComando.Parameters.AddWithValue("@Nombre", Nombre.Text.ToLower());
            Carpetas carpetin = new Carpetas();

            string insertQuery = "INSERT INTO Carpeta (NombreCarpeta) VALUES (@Nombre)";
            SqlCommand insertComando = new SqlCommand(insertQuery, conexion);

            TextInfo Mayuscula = CultureInfo.CurrentCulture.TextInfo;
            carpetin.Nombre = Mayuscula.ToTitleCase(Nombre.Text.ToLower());

            insertComando.Parameters.AddWithValue("@Nombre", carpetin.Nombre);


            if (carpetin.Nombre == "")
            {
                MessageBox.Show("El Material debe tener un nombre");

            }
            else
            {
                int count = (int)verificarComando.ExecuteScalar();
                if (count > 0)
                {
                    MessageBox.Show("El material ya existe");
                }
                else
                {

                    insertComando.ExecuteNonQuery();
                    MessageBox.Show("Se ingresó un material");
                }

                conexion.Close();

                LimpiarCampos();
                ListarCarpetas();
            }
        }


        private void LimpiarCampos()
        {
            Nombre.Text = "";

        }


        //FALTA VER TABLAS INTERMEDIAS
        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            Button eliminarButton = (Button)sender;
            if (eliminarButton.CommandParameter is Carpetas carp)
            {
                MessageBoxResult result = MessageBox.Show("¿Estás seguro de eliminar este registro?", "Confirmación de eliminación", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;

                    // Consulta para verificar si hay libros asociados a la categoría
                    string queryActualizarLibros = "SELECT COUNT(*) FROM Archivos WHERE  id_carpeta = @id_carpeta";

                    // Consulta para eliminar la categoría
                    string queryEliminarCategoria = "DELETE FROM [dbo].[Carpeta] WHERE [id_carpeta] = @id_carpeta";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command1 = new SqlCommand(queryActualizarLibros, connection);
                        command1.Parameters.AddWithValue("@id_carpeta", carp.id_carpeta);

                        // Obtiene el número de libros asociados a la categoría
                        int librosAsociados = (int)command1.ExecuteScalar();

                        if (librosAsociados > 0)
                        {
                            MessageBox.Show("No se puede eliminar la Carpeta, ya que tiene asociada uno o mas archivos.");
                        }
                        else
                        {
                            SqlCommand command2 = new SqlCommand(queryEliminarCategoria, connection);
                            command2.Parameters.AddWithValue("@id_carpeta", carp.id_carpeta);

                            int rowsDeleted = command2.ExecuteNonQuery();

                            if (rowsDeleted > 0)
                            {
                                MessageBox.Show("La Carpeta se eliminó correctamente.");
                            }
                            else
                            {
                                MessageBox.Show("No se pudo eliminar la Carpeta.");
                            }
                        }
                    }

                    // Actualiza la lista de carpetas después de la operación
                    ListarCarpetas();
                }
            }
        }



    }
}
