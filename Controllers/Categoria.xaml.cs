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
using System.Text.RegularExpressions;

namespace Museo.Views
{
    /// <summary>
    /// Lógica de interacción para Categoria.xaml
    /// </summary>
    public partial class Categoria : Window
    {
        public Categoria()
        {
            InitializeComponent();
            ListarCategorias();
            Loaded += ListaCategorias_Loaded;
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
        private void CrearEditorial_Click(object sender, RoutedEventArgs e)
        {
            Editorial editorial = new Editorial();
            editorial.Show();
            this.Close();
        }

        private void CrearCarpeta_Click(object sender, RoutedEventArgs e)
        {
            Carpeta carpeta = new Carpeta();

            carpeta.Show();
            this.Close();

        }


        private void ListaCategorias_Loaded(object sender, RoutedEventArgs e)
        {
            Autorizaciones autorizaciones = new Autorizaciones();
            DataGridColumn columnaAEditar = null;
            DataGridColumn columnaAEliminar = dataGrid.Columns[1];
            autorizaciones.Autorizacion(sender, e, columnaAEditar, columnaAEliminar);
            ListarCategorias();
        }


        private void ListarCategorias()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
            string query = "SELECT [id_categoria] ,[Nombre]  FROM [dbo].[Categoria]";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                List<Categorias> categoria = new List<Categorias>();

                while (reader.Read())
                {
                    Categorias categ = new Categorias();
                    categ.id_categoria = reader.GetInt32(0);
                    categ.Nombre = reader.GetString(1);

                    categoria.Add(categ);
                }

                dataGrid.ItemsSource = categoria;
            }
        }

        private void EnviarCategoria_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                string query = "SELECT COUNT(*) FROM Categoria WHERE Nombre = @Nombre";
                SqlCommand checkCommand = new SqlCommand(query, conexion);



                Categorias categorias = new Categorias();


                TextInfo Mayuscula = CultureInfo.CurrentCulture.TextInfo;
                categorias.Nombre = Mayuscula.ToTitleCase(Categoria1.Text.ToLower());
                checkCommand.Parameters.AddWithValue("@Nombre", categorias.Nombre);



                if (categorias.Nombre == "")
                {
                    MessageBox.Show("La categoria debe tener un nombre");

                }
                else
                {

                    int existingRecordsCount = (int)checkCommand.ExecuteScalar();

                    if (existingRecordsCount > 0)
                    {
                        MessageBox.Show("La categoria ya existe");
                        return;
                    }

                    string insertQuery = "INSERT INTO Categoria (Nombre) VALUES (@Nombre)";
                    SqlCommand insertCommand = new SqlCommand(insertQuery, conexion);
                    insertCommand.Parameters.AddWithValue("@Nombre", categorias.Nombre);

                    insertCommand.ExecuteNonQuery();

                    MessageBox.Show("Se ingresó una Categoria");
                    LimpiarCampos();
                    ListarCategorias();
                }
            }
        }

        private void LimpiarCampos()
        {
            Categoria1.Text = "";

        }

        private void BuscarCategoria(object sender, RoutedEventArgs e)
        {

            TextInfo Mayuscula = CultureInfo.CurrentCulture.TextInfo;
            string textoBusqueda = Mayuscula.ToTitleCase(PorCategoria.Text.Trim().ToLower());
            for (int i = 0; i < dataGrid.Items.Count; i++)
            {
                Categorias item = (Categorias)dataGrid.Items[i];
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

            PorCategoria.Text = "";
        }
       private void Eliminar_Click(object sender, RoutedEventArgs e)
       {
                Button eliminarButton = (Button)sender;
                if (eliminarButton.CommandParameter is Categorias categ)
                {
                    MessageBoxResult result = MessageBox.Show("¿Estás seguro de eliminar este registro?", "Confirmación de eliminación", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;

                        // Consulta para verificar si hay libros asociados a la categoría
                        string queryActualizarLibros = "SELECT COUNT(*) FROM Libros WHERE Categoria_id = @id_categoria";

                        // Consulta para eliminar la categoría
                        string queryEliminarCategoria = "DELETE FROM [dbo].[Categoria] WHERE [id_categoria] = @id_categoria";

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            SqlCommand command1 = new SqlCommand(queryActualizarLibros, connection);
                            command1.Parameters.AddWithValue("@id_categoria", categ.id_categoria);

                            // Obtiene el número de libros asociados a la categoría
                            int librosAsociados = (int)command1.ExecuteScalar();

                            if (librosAsociados > 0)
                            {
                                MessageBox.Show("No se puede eliminar la categoría, ya que está asociada a un libro.");
                            }
                            else
                            {
                                SqlCommand command2 = new SqlCommand(queryEliminarCategoria, connection);
                                command2.Parameters.AddWithValue("@id_categoria", categ.id_categoria);

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
                        ListarCategorias();
                    }
                }
       }

        private void Categoria_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;

            // Si se ingresó un carácter que no es una letra, bloquea la entrada
            if (!Regex.IsMatch(e.Text, @"[a-zA-Z]"))
            {
                e.Handled = true;
            }
        }

    }
}
