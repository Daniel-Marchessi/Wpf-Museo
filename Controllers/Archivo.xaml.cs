using Museo.Controllers;
using Museo.Models;
using Museoapp.Models;
using Museoapp.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.RegularExpressions;
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
using Xceed.Wpf.Toolkit;
using MessageBox = System.Windows.MessageBox;

namespace Museo.Views
{

    //Controlar vista Archivo
    public partial class Archivo : Window
    {
            public Archivo()
            {
            InitializeComponent();
            Loaded += ListaArchivos_Loaded;
            ListarArchivos(null);
            //ListarCarpetas();
            TraerCategorias();
            traerCarpetas();

            }

        private void CrearCarpeta_Click(object sender, RoutedEventArgs e)
            {
                Carpeta carpeta = new Carpeta();

                carpeta.Show();
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
            private void ListaArchivos_Loaded(object sender, RoutedEventArgs e)
            {
                Autorizaciones autorizaciones = new Autorizaciones();
                DataGridColumn columnaAEditar = dataGrid.Columns[4];
                DataGridColumn columnaAEliminar = dataGrid.Columns[3];
                autorizaciones.Autorizacion(sender, e, columnaAEditar, columnaAEliminar);
                ListarArchivos(null);
            }


        private void TraerCategorias()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                string query = "SELECT Nombre, id_categoria FROM Categoria";
                SqlCommand comand = new SqlCommand(query, conexion);
                SqlDataReader reader = comand.ExecuteReader();
                while (reader.Read())
                {
                    var categoria = new Categorias
                    {
                        id_categoria = Convert.ToInt32(reader["id_categoria"]),
                        Nombre = reader["Nombre"].ToString(),
                    };
                    Categoria.Items.Add(categoria);

                }
                Categoria.DisplayMemberPath = "Nombre";
                Categoria.SelectedValue = "id_categoria";
            }

        }

        private void traerCarpetas()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                string query = "SELECT NombreCarpeta, id_carpeta FROM Carpeta";
                SqlCommand comand = new SqlCommand(query, conexion);
                SqlDataReader reader = comand.ExecuteReader();
                while (reader.Read())
                {
                    var carpetin = new Carpetas
                    {
                        id_carpeta = Convert.ToInt32(reader["id_carpeta"]),
                        Nombre = reader["NombreCarpeta"].ToString(),
                    };
                    Carpeta.Items.Add(carpetin);
                    //FiltrarCarpeta.Items.Add(carpetin);
                }
                Carpeta.DisplayMemberPath = "Nombre";
                Carpeta.SelectedValue = "id_carpeta";
                //FiltrarCarpeta.DisplayMemberPath = "Nombre";
                //FiltrarCarpeta.SelectedValue = "id_carpeta";
            }

        }





        private void Eliminar_Click(object sender, RoutedEventArgs e)
            {
                Button eliminarButton = (Button)sender;
                if (eliminarButton.CommandParameter is Archivos archivo)
                {
                    MessageBoxResult result = MessageBox.Show("¿Estás seguro de eliminar este registro?", "Confirmación de eliminación", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
                        string query = "DELETE FROM [dbo].[Archivos] WHERE [id_archivo] = @idArchivo";

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@idArchivo", archivo.id_archivo);

                            connection.Open();
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("El archivo se eliminó correctamente.");
                            }
                        }

                        ListarArchivos(null);
                    }
                }
            }
        private void ListarArchivos(string carpetaSeleccionada)
        {
            dataGrid.IsReadOnly = true;
            string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;

            string querycategoria = "SELECT Categoria.Nombre FROM Categoria " +
                                    "JOIN Archivos ON Categoria.id_categoria = Archivos.id_categoria " +
                                    "WHERE Archivos.id_archivo = @archivoId";
            string querycCarpeta = "SELECT Carpeta.NombreCarpeta FROM Carpeta " +
                                    "JOIN Archivos ON Carpeta.id_carpeta = Archivos.id_Carpeta " +
                                    "WHERE Archivos.id_archivo = @archivoId";
            string query = "SELECT DISTINCT [id_archivo], [id_carpeta],[id_categoria], [Codigo], [Titulo] FROM [dbo].[Archivos]";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                if (!string.IsNullOrEmpty(carpetaSeleccionada))
                {
                    command.Parameters.AddWithValue("@carpeta", carpetaSeleccionada);
                }

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Archivos> archivos = new List<Archivos>();

                while (reader.Read())
                {
                    Archivos archivo = new Archivos();
                    archivo.id_archivo = reader.GetInt32(0);
                    archivo.id_carpeta = reader.IsDBNull(1) ? (int?)null : reader.GetInt32(1);
                    archivo.id_categoria = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2);
                    archivo.Codigo = reader.GetInt32(3);
                    archivo.Titulo = reader.GetString(4);

                    List<Categorias> categoriasLista = new List<Categorias>();
                    using (SqlCommand categoriasCommand = new SqlCommand(querycategoria, connection))
                    {
                        categoriasCommand.Parameters.AddWithValue("@archivoId", archivo.id_archivo);
                        SqlDataReader categoriasReader = categoriasCommand.ExecuteReader();
                        while (categoriasReader.Read())
                        {
                            string nombreCategoria = categoriasReader.GetString(0);
                            Categorias categorias = new Categorias { Nombre = nombreCategoria };
                            categoriasLista.Add(categorias);
                        }
                        categoriasReader.Close();
                    }
                    string nombresCategorias = string.Join(", ", categoriasLista.Select(m => m.Nombre));
                    archivo.Categorias = nombresCategorias;

                    List<Carpetas> carpetasLista = new List<Carpetas>();
                    using (SqlCommand carpetasCommand = new SqlCommand(querycCarpeta, connection))
                    {
                        carpetasCommand.Parameters.AddWithValue("@archivoId", archivo.id_archivo);
                        SqlDataReader carpetasReader = carpetasCommand.ExecuteReader();
                        while (carpetasReader.Read())
                        {
                            string nombreCarpeta = carpetasReader.GetString(0);
                            Carpetas carpetas = new Carpetas { Nombre = nombreCarpeta };
                            carpetasLista.Add(carpetas);
                        }
                        carpetasReader.Close();
                    }
                    string nombresCarpetas = string.Join(", ", carpetasLista.Select(m => m.Nombre));
                    archivo.Carpetas = nombresCarpetas;

                    archivos.Add(archivo);
                }

                // Establecer el ItemsSource después de procesar todos los registros
                dataGrid.AutoGenerateColumns = false;
                dataGrid.ItemsSource = archivos;

                reader.Close();
                connection.Close();
            }
        }







        private void EnviarArchivo_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                Archivos archivos = new Archivos();
                TextInfo Mayuscula = CultureInfo.CurrentCulture.TextInfo;
                //archivos.id_carpeta = Convert.ToInt32(Carpeta.Text);
                //archivos.id_categoria = Convert.ToInt32(Categoria.Text);
                archivos.Titulo = Mayuscula.ToTitleCase(Titulo.Text.ToLower());

                if (Carpeta.SelectedItem != null)
                {
                    archivos.id_carpeta = ((Carpetas)Carpeta.SelectedItem).id_carpeta;
                }
                else
                {
                    archivos.id_carpeta = null;
                }

                if (Categoria.SelectedItem != null)
                {
                    archivos.id_categoria = ((Categorias)Categoria.SelectedItem).id_categoria;
                }
                else
                {
                    archivos.id_categoria = null;
                }

                // Validar si el campo Codigo es un valor numérico válido
                archivos.Codigo = 0; // Establecer el valor predeterminado a 0

                if (!string.IsNullOrEmpty(Codigo.Text) && int.TryParse(Codigo.Text, out int codigo))
                {
                    archivos.Codigo = codigo;
                }

                string queryExists = "SELECT COUNT(*) FROM Archivos WHERE Codigo = @Codigo";
                SqlCommand commandExists = new SqlCommand(queryExists, conexion);
                commandExists.Parameters.AddWithValue("@Codigo", archivos.Codigo);

            

                if (archivos.Codigo == 0)
                {
                    MessageBox.Show("El archivo debe tener un código");
                }
                else
                {
                    if (archivos.id_carpeta == null)
                    {
                        MessageBox.Show("El archivo debe pertenecer a una carpeta");
                    }
                    else
                    {
                        if (archivos.Titulo == "")
                        {
                            MessageBox.Show("El archivo debe tener un título");
                        }
                        else
                        {
                            int count = Convert.ToInt32(commandExists.ExecuteScalar());
                            if (count > 0)
                            {
                                MessageBox.Show("El codigo elegido ya esta asociado a un archivo.");
                            }
                            else
                            {
                                string queryInsert = "INSERT INTO Archivos (id_carpeta, id_categoria, Codigo, Titulo) " +
                                                     "VALUES (@Carpeta, @Categoria, @Codigo, @Titulo)";
                                SqlCommand commandInsert = new SqlCommand(queryInsert, conexion);
                              
                                if (archivos.id_categoria.HasValue)
                                {
                                    commandInsert.Parameters.AddWithValue("@Categoria", archivos.id_categoria.Value);
                                }
                                else
                                {
                                    // Si CategoriaId es nulo, asignamos DBNull.Value
                                    commandInsert.Parameters.AddWithValue("@Categoria", DBNull.Value);
                                }

                                if (archivos.id_carpeta.HasValue)
                                {
                                    commandInsert.Parameters.AddWithValue("@Carpeta", archivos.id_carpeta.Value);
                                }
                                else
                                {
                                    // Si EditorialId es nulo, asignamos DBNull.Value
                                    commandInsert.Parameters.AddWithValue("@Carpeta", DBNull.Value);
                                }


                                commandInsert.Parameters.AddWithValue("@Codigo", archivos.Codigo);
                                commandInsert.Parameters.AddWithValue("@Titulo", archivos.Titulo);
                                commandInsert.ExecuteNonQuery();
                                MessageBox.Show("Se ingresó un Archivo");
                                ListarArchivos(null);
                                LimpiarCampos();
                            }
                        }
                    }
                }
            }
        }


        private void BuscarPorCodigo(object sender, RoutedEventArgs e)
            {
                string textoBusqueda = PorNombre.Text.Trim();
                int codigoBusqueda;

                // Intentar convertir el texto de búsqueda a un número entero
                if (int.TryParse(textoBusqueda, out codigoBusqueda))
                {
                    for (int i = 0; i < dataGrid.Items.Count; i++)
                    {
                        Archivos item = (Archivos)dataGrid.Items[i];
                        DataGridRow dataGridRow = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(i);

                        if (item.Codigo == codigoBusqueda)
                        {
                            dataGridRow.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            // Ocultar el elemento si el código no coincide con la búsqueda
                            dataGridRow.Visibility = Visibility.Collapsed;
                        }
                  
                    }
                }

                 else
                 {
                        //si esta vacio el campo mostrar todo
                        if (string.IsNullOrWhiteSpace(textoBusqueda))
                        {
                            for (int i = 0; i < dataGrid.Items.Count; i++)
                            {
                                DataGridRow dataGridRow = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(i);
                                dataGridRow.Visibility = Visibility.Visible;
                            }
                        }
                 }

                PorNombre.Text = "";
            }

            private void LimpiarCampos()
            {
                Carpeta.Text = "";
                Categoria.Text = "";
                Codigo.Text = "";
                Titulo.Text = "";
            }



        //Metodos para controlar el tipo de dato que se puede ingresar por teclado

        //Enteros


        private void Soloenteros_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var Codigo = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            Archivos archivoSeleccionado = (Archivos)dataGrid.SelectedItem;

            if (archivoSeleccionado != null)
            {
                EditarArchivo ventanaEditar = new EditarArchivo(archivoSeleccionado.id_carpeta, archivoSeleccionado.id_categoria, archivoSeleccionado.Titulo, archivoSeleccionado.Codigo, archivoSeleccionado.id_archivo);

                ventanaEditar.ShowDialog();

            }
            ListarArchivos(null);
        }
    }


    //Controlador vista EditarArchivo
    public partial class EditarArchivo : Window
    {
        public EditarArchivo(int? carpeta, int? categoria, string titulo, int codigo, int? idArchivo)
        {
            InitializeComponent();
            Carpeta.Text = carpeta.ToString();
            Categoria.Text = categoria.ToString();
            Titulo.Text = titulo;
            Codigo.Text = codigo.ToString();
            id_archivo.Text = idArchivo.ToString();
            TraerCategorias();
            traerCarpetas();
        }

        private void TraerCategorias()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                string query = "SELECT Nombre, id_categoria FROM Categoria";
                SqlCommand comand = new SqlCommand(query, conexion);
                SqlDataReader reader = comand.ExecuteReader();
                while (reader.Read())
                {
                    var categoria = new Categorias
                    {
                        id_categoria = Convert.ToInt32(reader["id_categoria"]),
                        Nombre = reader["Nombre"].ToString(),
                    };
                    Categoria.Items.Add(categoria);

                }
                Categoria.DisplayMemberPath = "Nombre";
                Categoria.SelectedValue = "id_categoria";
            }

        }

        private void traerCarpetas()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                string query = "SELECT NombreCarpeta, id_carpeta FROM Carpeta";
                SqlCommand comand = new SqlCommand(query, conexion);
                SqlDataReader reader = comand.ExecuteReader();
                while (reader.Read())
                {
                    var carpetin = new Carpetas
                    {
                        id_carpeta = Convert.ToInt32(reader["id_carpeta"]),
                        Nombre = reader["NombreCarpeta"].ToString(),
                    };
                    Carpeta.Items.Add(carpetin);
                    //FiltrarCarpeta.Items.Add(carpetin);
                }
                Carpeta.DisplayMemberPath = "Nombre";
                Carpeta.SelectedValue = "id_carpeta";
                //FiltrarCarpeta.DisplayMemberPath = "Nombre";
                //FiltrarCarpeta.SelectedValue = "id_carpeta";
            }

        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;

            // Obtener los valores editados de los TextBox
            TextInfo Mayuscula = CultureInfo.CurrentCulture.TextInfo;
            int? id_carpeta = (Carpeta.SelectedItem as Carpetas)?.id_carpeta;
            int? id_categoria = (Categoria.SelectedItem as Categorias)?.id_categoria;

            string codigoText = Codigo.Text;
            if (string.IsNullOrEmpty(codigoText))
            {
                MessageBox.Show("El archivo debe tener un código");
            }
            else
            {
                if (int.TryParse(codigoText, out int nuevoCodigo))
                {
                    string nuevoTitulo = Mayuscula.ToTitleCase(Titulo.Text.ToLower());
                    int idArchivo = Convert.ToInt32(id_archivo.Text.Trim());

                    using (SqlConnection conexion = new SqlConnection(connectionString))
                    {
                        conexion.Open();

                        // Verificar si la combinación de Codigo, Carpeta y Categoria ya existe
                        string queryExists = "SELECT COUNT(*) FROM Archivos WHERE Codigo = @Codigo AND id_archivo <> @idArchivo";
                        SqlCommand commandExists = new SqlCommand(queryExists, conexion);
                        commandExists.Parameters.AddWithValue("@Codigo", nuevoCodigo);
                        commandExists.Parameters.AddWithValue("@idArchivo", idArchivo);

                        if (Carpeta.SelectedItem == null)
                        {
                            MessageBox.Show("El archivo debe pertenecer a una carpeta");
                        }
                        else
                        {
                            int count = Convert.ToInt32(commandExists.ExecuteScalar());
                            if (count > 0)
                            {
                                MessageBox.Show("El archivo ya existe");
                            }
                            else
                            {

                                string queryUpdate = "UPDATE Archivos SET id_carpeta = @Carpeta, id_categoria = @Categoria, Titulo = @Titulo, Codigo = @Codigo WHERE id_archivo = @idArchivo";
                                SqlCommand commandUpdate = new SqlCommand(queryUpdate, conexion);
                                commandUpdate.Parameters.AddWithValue("@Carpeta", id_carpeta);

                                // Manejar el caso en que id_categoria es nulo
                                if (id_categoria.HasValue)
                                {
                                    commandUpdate.Parameters.AddWithValue("@Categoria", id_categoria);
                                }
                                else
                                {
                                    // Si CategoriaId es nulo, asignamos DBNull.Value
                                    commandUpdate.Parameters.AddWithValue("@Categoria", DBNull.Value);
                                }
                                commandUpdate.Parameters.AddWithValue("@Titulo", nuevoTitulo);
                                commandUpdate.Parameters.AddWithValue("@Codigo", nuevoCodigo);
                                commandUpdate.Parameters.AddWithValue("@idArchivo", idArchivo);
                                commandUpdate.ExecuteNonQuery();
                                System.Windows.MessageBox.Show("Los cambios se han guardado correctamente");
                                this.Close();
                            }
                        }
                    }
                }
     
            }
        }





        private void Soloenteros_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var Codigo = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

    }
}