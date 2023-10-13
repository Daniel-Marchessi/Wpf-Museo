using Museo.Controllers;
using Museo.Views;
using Museoapp.Models;
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
using WpfAppTEST.Models;
using WpfAppTEST.Views;
using Museo.Controllers;
using Xceed.Wpf.Toolkit;
using MessageBox = Xceed.Wpf.Toolkit.MessageBox;

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
            Loaded += ListaAutores_Loaded;

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


        private void ListaAutores_Loaded(object sender, RoutedEventArgs e)
        {
            Autorizaciones autorizaciones = new Autorizaciones();
            DataGridColumn columnaAEditar = dataGrid.Columns[3];
            DataGridColumn columnaAEliminar = dataGrid.Columns[2];
            autorizaciones.Autorizacion(sender, e, columnaAEditar, columnaAEliminar);
            ListarAutores();
        }

        private void ListarAutores()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
            string query = "SELECT [id_autor], [Nombre], [Apellido]  FROM [dbo].[Autor]";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                List<Autores> autores = new List<Autores>();

                while (reader.Read())
                {
                    Autores autorin = new Autores();
                    autorin.id_autor = reader.GetInt32(0);
                    autorin.Nombre = reader.GetString(1);
                    autorin.Apellido = reader.GetString(2);
                    autores.Add(autorin);
                }

                dataGrid.ItemsSource = autores;
            }
        }

        private void EnviarAutor_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();

                string query = "SELECT COUNT(*) FROM Autor WHERE Nombre = @Nombre AND Apellido = @Apellido";
                SqlCommand Command = new SqlCommand(query, conexion);


                TextInfo Mayuscula = CultureInfo.CurrentCulture.TextInfo;
                Autores autorin = new Autores();
                autorin.Nombre = Mayuscula.ToTitleCase(Nombre1.Text.ToLower());
                autorin.Apellido = Mayuscula.ToTitleCase(Apellido1.Text.ToLower());

                Command.Parameters.AddWithValue("@Nombre", autorin.Nombre);
                Command.Parameters.AddWithValue("@Apellido", autorin.Apellido);

                if( autorin.Nombre == ""){
                    MessageBox.Show("El autor debe tener un nombre");

                }
                else
                {
                    if (autorin.Apellido == "")
                    {
                        MessageBox.Show("El autor debe tener un Apellido");
                    }
                    else
                    {
                        int existingRecordsCount = (int)Command.ExecuteScalar();

                        if (existingRecordsCount > 0)
                        {
                            MessageBox.Show("El autor ya existe");
                            return;
                        }


                        string insertQuery = "INSERT INTO Autor (Nombre, Apellido) VALUES (@Nombre, @Apellido)";
                        SqlCommand insertCommand = new SqlCommand(insertQuery, conexion);
                        insertCommand.Parameters.AddWithValue("@Nombre", autorin.Nombre);
                        insertCommand.Parameters.AddWithValue("@Apellido", autorin.Apellido);
                        insertCommand.ExecuteNonQuery();

                        MessageBox.Show("Se ingresó un Autor");
                        LimpiarCampos();
                        ListarAutores();
                    }
                }
               
            }
        }
        private void BuscarPorNombre(object sender, RoutedEventArgs e)
        {
            
            string textoBusqueda = PorNombre.Text.Trim();
            for (int i = 0; i < dataGrid.Items.Count; i++)
            {
                Autores item = (Autores)dataGrid.Items[i];
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

            PorNombre.Text = "";
        }

        private void LimpiarCampos()
        {
            Nombre1.Text = "";
            Apellido1.Text = "";
        }
        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            Button eliminarButton = (Button)sender;
            if (eliminarButton.CommandParameter is Autores autor)
            {
                MessageBoxResult result = MessageBox.Show("¿Estás seguro de eliminar este registro?", "Confirmación de eliminación", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
                    string queryColeccion_Autor = "DELETE FROM [dbo].[Coleccion_Autor] WHERE [id_autor] = @id_autor";
                    string queryLibro_Autor = "DELETE FROM [dbo].[Libro_Autor] WHERE [id_autor] = @id_autor";
                    string queryAutor = "DELETE FROM [dbo].[Autor] WHERE [id_autor] = @id_autor";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command1 = new SqlCommand(queryColeccion_Autor, connection);
                        command1.Parameters.AddWithValue("@id_autor", autor.id_autor);
                        SqlCommand command2 = new SqlCommand(queryLibro_Autor, connection);
                        command2.Parameters.AddWithValue("@id_autor", autor.id_autor);
                        SqlCommand command3 = new SqlCommand(queryAutor, connection);
                        command3.Parameters.AddWithValue("@id_autor", autor.id_autor);

                        command1.ExecuteNonQuery();
                        command2.ExecuteNonQuery();
                        int rowsAffected = command3.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("El autor se eliminó correctamente.");
                        }
                    }
                    ListarAutores();
                }
            }
        }


        //Metodos para controlar el tipo de dato que se puede ingresar por teclado
        //Cadenas de texto
        private void  SoloString_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            if (!char.IsLetter(e.Text[0]) && !char.IsWhiteSpace(e.Text[0]))
            {
                e.Handled = true;
            }
        }

        private void Editar_Click(object sender, RoutedEventArgs e) { }

        public partial class EditarAutor : Window
        {
            public EditarAutor()
            {
              
            }
        }
    }
}
