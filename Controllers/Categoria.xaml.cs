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
    /// Lógica de interacción para Categoria.xaml
    /// </summary>
    public partial class Categoria : Window
    {
        public Categoria()
        {
            InitializeComponent();
            ListarCategorias();
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
            Material material = new Material();
            material.Show();
            this.Close();
        }
        private void CrearEditorial_Click(object sender, RoutedEventArgs e)
        {
            Categoria categoria = new Categoria();
            categoria.Show();
            this.Close();
        }
        private void ListarCategorias()
        {
            string connectionString = "server=DESKTOP-TI2N3QM; database=Museo1 ; integrated security = true";
            string query = "SELECT [Nombre]  FROM [dbo].[Categoria]";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                List<Categorias> categoria = new List<Categorias>();

                while (reader.Read())
                {
                    Categorias categ = new Categorias();
                    categ.Nombre = reader.GetString(0);
              
                    categoria.Add(categ);
                }

                dataGrid.ItemsSource = categoria;
            }
        }

        private void EnviarCategoria_Click(object sender, RoutedEventArgs e)
        {
            var conexion = new SqlConnection("server=DESKTOP-TI2N3QM; database=Museo1 ; integrated security = true");
            conexion.Open();

            string query = "SELECT COUNT(*) FROM Categoria WHERE Nombre = @Nombre";
            SqlCommand checkCommand = new SqlCommand(query, conexion);
            checkCommand.Parameters.AddWithValue("@Nombre", Categoria1.Text);
           

            int existingRecordsCount = (int)checkCommand.ExecuteScalar();

            if (existingRecordsCount > 0)
            {
                MessageBox.Show("La categoria ya existe");
                conexion.Close();
                return;
            }

            string insertQuery = "INSERT INTO Categoria (Nombre) VALUES (@Nombre)";
            SqlCommand insertCommand = new SqlCommand(insertQuery, conexion);
            insertCommand.Parameters.AddWithValue("@Nombre", Categoria1.Text);
           
            insertCommand.ExecuteNonQuery();

            MessageBox.Show("Se ingresó una Categoria");
            conexion.Close();
            Categoria1.Text = "";
            ListarCategorias();
        }
       
        private void BuscarCategoria(object sender, RoutedEventArgs e)
        {

        }
        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Editar_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
