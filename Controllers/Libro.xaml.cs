using Museo.Models;
using Museo.Views;
using Museoapp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows;
using WpfAppTEST.Models;
using WpfAppTEST.Views;

namespace Museoapp.Views
{
    /// <summary>
    /// Lógica de interacción para Libro.xaml
    /// </summary>
    public partial class Libro : Window
    {


        public Libro()
        {
            InitializeComponent();
            Titulo1.Focus();
            Cargaraños();
            TraerAutores();
            TraerEditorial();
            TraerCategorias();
        }
        private void TraerAutores()
        {
            var conexion = new SqlConnection("server=DESKTOP-9MTUTME; database=Museo1 ; integrated security = true");
            conexion.Open();

            string query = "SELECT [Nombre],[id_autor], [Apellido] FROM [dbo].[Autor]";
            SqlCommand comand = new SqlCommand(query, conexion);

            SqlDataReader reader = comand.ExecuteReader();


            while (reader.Read())
            {
                var autor = new Autores
                {
                    id_autor = Convert.ToInt32(reader["id_autor"]),

                    NombreCompleto= reader["Nombre"].ToString() + " " + reader["Apellido"].ToString(),
                    
                    
                };
                Autores12.Items.Add(autor);
               
            }
           
            Autores12.DisplayMemberPath = "NombreCompleto";
            Autores12.SelectedValue = "id_autor";
            MessageBox.Show(Autores12.DisplayMemberPath);
            conexion.Close();
        }

        private void TraerEditorial() {

            var conexion = new SqlConnection("server=DESKTOP-9MTUTME; database=Museo1 ; integrated security = true");
            conexion.Open();

            string query = "SELECT Nombre, id_editorial FROM Editorial";
            SqlCommand comand = new SqlCommand(query, conexion);

            SqlDataReader reader = comand.ExecuteReader();


            while (reader.Read())
            {
                var editorial = new Editorial
                {
                    id_editorial = Convert.ToInt32(reader["id_editorial"]),

                    Nombre = reader["Nombre"].ToString(),


                };
                Editorial.Items.Add(editorial);

            }

            Editorial.DisplayMemberPath = "Nombre";
            Editorial.SelectedValue = "id_editorial";
           
            conexion.Close();

        }


        private void TraerCategorias()
        {

            var conexion = new SqlConnection("server=DESKTOP-9MTUTME; database=Museo1 ; integrated security = true");
            conexion.Open();

            string query = "SELECT Nombre, id_categoria FROM Categoria";
            SqlCommand comand = new SqlCommand(query, conexion);

            SqlDataReader reader = comand.ExecuteReader();


            while (reader.Read())
            {
                var categoria = new Categoria
                {
                    id_categoria = Convert.ToInt32(reader["id_categoria"]),

                    Nombre = reader["Nombre"].ToString(),


                };
                Categoria.Items.Add(categoria);

            }

            Categoria.DisplayMemberPath = "Nombre";
            Categoria.SelectedValue = "id_categoria";

            conexion.Close();

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

        private void EnviarLibro_Click(object sender, RoutedEventArgs e)
        {


            var conexion = new SqlConnection("server=DESKTOP-9MTUTME; database=Museo1 ; integrated security = true");
            conexion.Open();


            string query = "INSERT INTO Libros (Titulo, Origen, N_paginas, Descripcion) " +
               "VALUES (@Titulo, @Origen, @N_paginas, @Descripcion)";

            Libros librin = new Libros();
            SqlCommand comand = new SqlCommand(query, conexion);

            TextInfo Mayuscula = CultureInfo.CurrentCulture.TextInfo;
            librin.Titulo = Mayuscula.ToTitleCase(Titulo1.Text.ToLower());
            librin.Origen = Mayuscula.ToTitleCase(Origen1.Text.ToLower());
            librin.N_paginas = Convert.ToInt32(Numpaginas1.Text);
            librin.Descripcion = Mayuscula.ToTitleCase(Descripcion1.Text.ToLower());

            comand.Parameters.AddWithValue("@Titulo", librin.Titulo);
            comand.Parameters.AddWithValue("@Origen", librin.Origen);
            comand.Parameters.AddWithValue("@N_paginas", librin.N_paginas);
            comand.Parameters.AddWithValue("@Descripcion", librin.Descripcion);

            comand.ExecuteNonQuery();
            MessageBox.Show("Se ingreso un Libro");
            conexion.Close();
            LimpiarCampos();

        }

        private void LimpiarCampos()
        {
            Titulo1.Text = "";
            Origen1.Text = "";
            Numpaginas1.Text = "";
            Descripcion1.Text = "";
        }
        public void Cargaraños()
        {
            int anioInicial = 1990;
            int anioFinal = DateTime.Now.Year;
            List<int> anios = new List<int>();

            for (int anio = anioInicial; anio <= anioFinal; anio++)
            {
                anios.Add(anio);
            }

            comboBoxaños.ItemsSource = anios;
        }


    }





}
