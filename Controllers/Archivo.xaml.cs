using Museoapp.Models;
using Museoapp.Views;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection.PortableExecutable;
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
namespace Museo.Views
{
    public partial class Archivo : Window
    {
        public Archivo()
        {
            InitializeComponent();
            ListarArchivos(null);
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

      
        private void CarpetasComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string carpetaSeleccionada = carpetasComboBox.SelectedItem as string;
            ListarArchivos(carpetaSeleccionada);

            for (int i = 0; i < dataGrid.Items.Count; i++)
            {
                Archivos item = dataGrid.Items[i] as Archivos;
                DataGridRow dataGridRow = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(item);

                if (dataGridRow != null)
                {
                    if (item.Carpeta == carpetaSeleccionada)
                    {
                        // Mostrar la fila si coincide con la carpeta seleccionada
                        dataGridRow.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        // Ocultar la fila si no coincide con la carpeta seleccionada
                        dataGridRow.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        private void ListarCarpetas()
        {
            string connectionString = "server=DESKTOP-TI2N3QM; database=Museo1; integrated security=true";
            string query = "SELECT DISTINCT [Carpeta] FROM [dbo].[Archivos]";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                List<string> carpetas = new List<string>();

                while (reader.Read())
                {
                    string carpeta = reader.GetString(0);
                    carpetas.Add(carpeta);
                }

                carpetasComboBox.ItemsSource = carpetas;
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
                    string connectionString = "server=DESKTOP-TI2N3QM; database=Museo1; integrated security = true";
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
            string connectionString = "server=DESKTOP-TI2N3QM; database=Museo1; integrated security = true";
            string query = "SELECT DISTINCT   [id_archivo], [carpeta],[Categoria], [Codigo], [Titulo] FROM [dbo].[Archivos]";

            if (!string.IsNullOrEmpty(carpetaSeleccionada))
            {
                query += " WHERE [Carpeta] = @carpeta";
            }

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
                    archivo.Carpeta = reader.GetString(1);
                    archivo.Categoria = reader.GetString(2);
                    archivo.Codigo = reader.GetInt32(3);
                    archivo.Titulo = reader.GetString(4);

                    archivos.Add(archivo);
                }
             
                dataGrid.ItemsSource = archivos;
                dataGrid.AutoGenerateColumns = false;

            }
        }
        private void EnviarArchivo_Click(object sender, RoutedEventArgs e)
        {
            var conexion = new SqlConnection("server=DESKTOP-TI2N3QM; database=Museo1; integrated security = true");
            conexion.Open();
            string queryExists = "SELECT COUNT(*) FROM Archivos WHERE Codigo = @Codigo";
            SqlCommand commandExists = new SqlCommand(queryExists, conexion);
            commandExists.Parameters.AddWithValue("@Codigo", Convert.ToInt32(Codigo.Text));
            int count = Convert.ToInt32(commandExists.ExecuteScalar());

            if (count > 0)
            {
                MessageBox.Show("El código ya existe. Por favor, elija otro código.");
            }
            else
            {
                string queryInsert = "INSERT INTO Archivos (Carpeta, Categoria, Codigo, Titulo) " +
                                    "VALUES (@Carpeta, @Categoria, @Codigo, @Titulo)";
                Archivos archivos = new Archivos();
                SqlCommand commandInsert = new SqlCommand(queryInsert, conexion);

                TextInfo Mayuscula = CultureInfo.CurrentCulture.TextInfo;
                archivos.Carpeta = Mayuscula.ToTitleCase(Carpeta.Text.ToLower());
                archivos.Categoria = Mayuscula.ToTitleCase(Categoria.Text.ToLower());
                archivos.Codigo = Convert.ToInt32(Codigo.Text);
                archivos.Titulo = Mayuscula.ToTitleCase(Titulo.Text.ToLower());
                commandInsert.Parameters.AddWithValue("@Carpeta", archivos.Carpeta);
                commandInsert.Parameters.AddWithValue("@Categoria", archivos.Categoria);
                commandInsert.Parameters.AddWithValue("@Codigo", archivos.Codigo);
                commandInsert.Parameters.AddWithValue("@Titulo", archivos.Titulo);

                commandInsert.ExecuteNonQuery();
                MessageBox.Show("Se ingresó un Archivo");
                ListarArchivos(null);
                ListarCarpetas();
                LimpiarCampos();
            }

            conexion.Close();
        }
        
        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            Archivos archivoSeleccionado = (Archivos)dataGrid.SelectedItem;

            if (archivoSeleccionado != null)
            {
                EditarArchivo ventanaEditar = new EditarArchivo(archivoSeleccionado.Carpeta, archivoSeleccionado.Categoria, archivoSeleccionado.Titulo, archivoSeleccionado.Codigo, archivoSeleccionado.id_archivo);

                 ventanaEditar.ShowDialog();

            }
            ListarArchivos(null);

        }
        private void LimpiarCampos()
        {
            Carpeta.Text = "";
            Categoria.Text = "";
            Codigo.Text = "";
            Titulo.Text = "";
        }
    }
}