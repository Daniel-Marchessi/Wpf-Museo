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
using Museo.Controllers;
using System.Text.RegularExpressions;

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
            Loaded += ListaMateriales_Loaded;
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


        private void CrearCarpeta_Click(object sender, RoutedEventArgs e)
        {
            Carpeta carpeta = new Carpeta();

            carpeta.Show();
            this.Close();

        }

        private void ListaMateriales_Loaded(object sender, RoutedEventArgs e)
        {
            Autorizaciones autorizaciones = new Autorizaciones();
            //DataGridColumn columnaAEditar = dataGrid.Columns[2];
            DataGridColumn columnaAEditar = null;
            DataGridColumn columnaAEliminar = dataGrid.Columns[1];
            autorizaciones.Autorizacion(sender, e, columnaAEditar, columnaAEliminar);
            ListarMateriales();
        }


        private void ListarMateriales()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
            string query = "SELECT [id_material], [Nombre] FROM [dbo].[Material]";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                List<Materiales> materiales = new List<Materiales>();

                while (reader.Read())
                {
                    Materiales material = new Materiales();
                    material.id_material = reader.GetInt32(0);
                    material.Nombre = reader.GetString(1);
                    materiales.Add(material);
                }

                dataGrid.ItemsSource = materiales; // Asignar la lista de materiales al DataGrid
            }
        }



        private void EnviarMaterial_Click(object sender, RoutedEventArgs e)
        {
            var conexion = new SqlConnection("server=DESKTOP-TI2N3QM; database=Museo1 ; integrated security = true");
            conexion.Open();

            string query = "SELECT COUNT(*) FROM Material WHERE Nombre = @Nombre";
            SqlCommand verificarComando = new SqlCommand(query, conexion);
            verificarComando.Parameters.AddWithValue("@Nombre", Nombre1.Text.ToLower());
            Materiales materialin = new Materiales();




            string insertQuery = "INSERT INTO Material (Nombre) VALUES (@Nombre)";
            SqlCommand insertComando = new SqlCommand(insertQuery, conexion);

            TextInfo Mayuscula = CultureInfo.CurrentCulture.TextInfo;
            materialin.Nombre = Mayuscula.ToTitleCase(Nombre1.Text.ToLower());

            insertComando.Parameters.AddWithValue("@Nombre", materialin.Nombre);


            if (materialin.Nombre == "")
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
                    MessageBox.Show("Se ingresó un material correctamente");
                }

                conexion.Close();

                LimpiarCampos();
                ListarMateriales();
            }
        }
        private void BuscarPorNombre(object sender, RoutedEventArgs e)
        {
            TextInfo Mayuscula = CultureInfo.CurrentCulture.TextInfo;
            string textoBusqueda = Mayuscula.ToTitleCase(PorNombre.Text.Trim().ToLower());

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
   
        private void LimpiarCampos()
        {
            Nombre1.Text = "";

        }


        //FALTA VER TABLAS INTERMEDIAS
        private void Eliminar_Click(Object sender, RoutedEventArgs e) {
            Button eliminarButton = (Button)sender;
            if (eliminarButton.CommandParameter is Materiales material)
            {
                MessageBoxResult result = MessageBox.Show("¿Estás seguro de eliminar este registro?", "Confirmación de eliminación", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
                    
                    string queryMaterial = "DELETE FROM [dbo].[Material] WHERE [id_material] = @id_material";
                    string queryColeccion_Material = "DELETE FROM [dbo].[Coleccion_Material] WHERE [id_material] = @id_material";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command1 = new SqlCommand(queryMaterial, connection);
                        SqlCommand command2 = new SqlCommand(queryColeccion_Material, connection);
                        command1.Parameters.AddWithValue("@id_material", material.id_material);
                        command2.Parameters.AddWithValue("@id_material", material.id_material);

                        command1.ExecuteNonQuery();

                        int rowsAffected = command2.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("El autor se eliminó correctamente.");
                        }
                    }
                    ListarMateriales();
                }
            }
        }


        private void Material_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
