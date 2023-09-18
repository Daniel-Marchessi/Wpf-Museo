using Museo.Models;
using Museoapp.Models;
using Museoapp.Views;
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
using WpfAppTEST.Views;

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
            Material material = new Material();
            material.Show();
            this.Close();
        }
         private void EnviarEditorial_Click(Object sender, RoutedEventArgs e)
        {
            var conexion = new SqlConnection("server=DESKTOP-TI2N3QM; database=Museo1 ; integrated security = true");
            conexion.Open();

            string query = "SELECT COUNT(*) FROM Editorial WHERE Nombre = @Nombre";
            SqlCommand checkCommand = new SqlCommand(query, conexion);
            checkCommand.Parameters.AddWithValue("@Nombre", Editorial1.Text);


            int existingRecordsCount = (int)checkCommand.ExecuteScalar();

            if (existingRecordsCount > 0)
            {
                MessageBox.Show("La Editorial ya existe");
                conexion.Close();
                return;
            }

            string insertQuery = "INSERT INTO Editorial (Nombre) VALUES (@Nombre)";
            SqlCommand insertCommand = new SqlCommand(insertQuery, conexion);
            insertCommand.Parameters.AddWithValue("@Nombre", Editorial1.Text);

            insertCommand.ExecuteNonQuery();

            MessageBox.Show("Se ingresó una Editorial");
            conexion.Close();
            Editorial1.Text = "";
            ListarEditoriales();
        }
       
        private void ListarEditoriales()
        {
            string connectionString = "server=DESKTOP-TI2N3QM; database=Museo1 ; integrated security = true";
            string query = "SELECT [Nombre]  FROM [dbo].[Editorial]";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                List<Editoriales> editorial = new List<Editoriales>();

                while (reader.Read())
                {
                    Editoriales edit = new Editoriales();
                    edit.Nombre = reader.GetString(0);

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
                    string connectionString = "server=DESKTOP-TI2N3QM; database=Museo1; integrated security = true";

                    string queryLibroCampoEditorial = "DELETE FROM [dbo].[Libros] WHERE [Editorial_id] = @Editorial_id";
                    string queryEditorial = "DELETE FROM [dbo].[Editorial] WHERE [Editorial_id] = @Editorial_id";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command1 = new SqlCommand(queryLibroCampoEditorial, connection);
                        command1.Parameters.AddWithValue("@Editorial_id", editorial.id_editorial);
                        SqlCommand command2 = new SqlCommand(queryEditorial, connection);
                        command2.Parameters.AddWithValue("@Editorial_id", editorial.id_editorial);
                        

                        command1.ExecuteNonQuery();
                        int rowsAffected = command2.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("El autor se eliminó correctamente.");
                        }
                    }
                    ListarEditoriales();
                }
            }
        }
        private void Editar_Click(object sender, RoutedEventArgs e)
        {

        }
        private void BuscarPorEditorial(Object sender, RoutedEventArgs e)
        {

        }
    }
}
