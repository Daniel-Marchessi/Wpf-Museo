using Museo.Models;
using Museoapp.Models;
using Museoapp.Views;
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
using WpfAppTEST.Views;
using Museo.Controllers;
using System.Globalization;
using System.Windows.Media.Media3D;

namespace Museo.Views
{
    /// <summary>
    /// Lógica de interacción para Editorial.xaml
    /// </summary>
    public partial class Editorial : Window
    {
        public Editorial()
        {
            InitializeComponent();
            ListarEditoriales();
            Loaded += ListaEditoriales_Loaded;
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
        private void CrearArchivo_Click(object sender, RoutedEventArgs e)
        {
            Archivo archivo = new Archivo();
            archivo.Show();
            this.Close();

        }
        private void CrearCategoria_Click(object sender, RoutedEventArgs e)
        {
            Categoria categoria = new Categoria();
            categoria.Show();
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
            Museoapp.Views.Material material = new Museoapp.Views.Material();
            material.Show();
            this.Close();
        }

        private void ListaEditoriales_Loaded(object sender, RoutedEventArgs e)
        {
            Autorizaciones autorizaciones = new Autorizaciones();
            DataGridColumn columnaAEditar = dataGrid.Columns[2];
            DataGridColumn columnaAEliminar = dataGrid.Columns[1];
            autorizaciones.Autorizacion(sender, e, columnaAEditar, columnaAEliminar);
            ListarEditoriales();
        }

        private void EnviarEditorial_Click(Object sender, RoutedEventArgs e)
         {
            string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();

                string query = "SELECT COUNT(*) FROM Editorial WHERE Nombre = @Nombre";
                SqlCommand checkCommand = new SqlCommand(query, conexion);
                Editoriales editoriales = new Editoriales();
                TextInfo Mayuscula = CultureInfo.CurrentCulture.TextInfo;

                editoriales.Nombre = Mayuscula.ToTitleCase(Editorial1.Text.ToLower());

                checkCommand.Parameters.AddWithValue("@Nombre", editoriales.Nombre);


                if (editoriales.Nombre == "")
                {
                    MessageBox.Show("La editorial  debe tener un nombre");

                }
                else
                {


                    int existingRecordsCount = (int)checkCommand.ExecuteScalar();

                    if (existingRecordsCount > 0)
                    {
                        MessageBox.Show("La Editorial ya existe");
                        return;
                    }

                    string insertQuery = "INSERT INTO Editorial (Nombre) VALUES (@Nombre)";
                    SqlCommand insertCommand = new SqlCommand(insertQuery, conexion);
                    insertCommand.Parameters.AddWithValue("@Nombre", editoriales.Nombre);

                    insertCommand.ExecuteNonQuery();

                    MessageBox.Show("Se ingresó una Editorial");

                    Editorial1.Text = "";
                    ListarEditoriales();
                }
            }
         }
       
        private void ListarEditoriales()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
            string query = "SELECT [id_editorial] ,[Nombre]  FROM [dbo].[Editorial]";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                List<Editoriales> editorial = new List<Editoriales>();

                while (reader.Read())
                {
                    Editoriales edit = new Editoriales();
                    edit.Editorial_id = reader.GetInt32(0);
                    edit.Nombre = reader.GetString(1);

                    editorial.Add(edit);
                }

                dataGrid.ItemsSource = editorial;
            }
        }
        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            Button eliminarButton = (Button)sender;
            if (eliminarButton.CommandParameter is Editoriales editorial)
            {
                MessageBoxResult result = MessageBox.Show("¿Estás seguro de eliminar este registro?", "Confirmación de eliminación", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
                    string queryActualizarLibros = "SELECT COUNT(*) FROM Libros WHERE Editorial_id = @id_Editorial";
                    string queryEliminarEditorial = "DELETE FROM [dbo].[Editorial] WHERE [id_editorial] = @id_editorial";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command1 = new SqlCommand(queryActualizarLibros, connection);
                        command1.Parameters.AddWithValue("@id_editorial", editorial.Editorial_id);

                        // Obtiene el número de libros asociados a la categoría
                        int librosAsociados = (int)command1.ExecuteScalar();

                        if (librosAsociados > 0)
                        {
                            MessageBox.Show("No se puede eliminar la categoría, ya que está asociada a un libro.");
                        }
                        else
                        {
                            SqlCommand command2 = new SqlCommand(queryEliminarEditorial, connection);
                            command2.Parameters.AddWithValue("@id_editorial", editorial.Editorial_id);

                            int rowsDeleted = command2.ExecuteNonQuery();

                            if (rowsDeleted > 0)
                            {
                                MessageBox.Show("La categoría se eliminó correctamente.");
                            }
                            else
                            {
                                MessageBox.Show("No se pudo eliminar la categoría.");
                            }
                        }
                    }

                    // Actualiza la lista de categorías después de la operación
                    ListarEditoriales();
                }
            }
        }
        private void Editar_Click(object sender, RoutedEventArgs e)
        {

        }
        private void BuscarPorEditorial(object sender, RoutedEventArgs e)
        {
            TextInfo Mayuscula = CultureInfo.CurrentCulture.TextInfo;
            string textoBusqueda = Mayuscula.ToTitleCase(PorEditorial.Text.Trim().ToLower());
            for (int i = 0; i < dataGrid.Items.Count; i++)
            {
                Editoriales item = (Editoriales)dataGrid.Items[i];
                DataGridRow dataGridRow = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(i);

                if (item.Nombre.Contains(textoBusqueda))
                {
                    // Mostrar el elemento si coincide con la búsqueda
                    dataGridRow.Visibility = Visibility.Visible;
                }
                else
                {
                    // Ocultar el elemento si no coincide con la búsqueda
                    dataGridRow.Visibility = Visibility.Collapsed;
                }
            }

            PorEditorial.Text = "";
        }
    }
}
