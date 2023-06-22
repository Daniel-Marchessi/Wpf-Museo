using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using WpfAppTEST.Models;
using WpfAppTEST.Views;

namespace Museoapp.Views
{

    public partial class ListaLibros : Window
    {
        public ListaLibros()
        {
            InitializeComponent();
            ListarLibros();

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
        private void ListarLibros()
        {
            string connectionString = "server=DESKTOP-TI2N3QM; database=Museo1 ; integrated security = true";
            string query = "SELECT [id_libro],[Titulo],[Origen] ,[Autor_id],[N_paginas] ,[Descripcion],[Categoria_id],[Editorial_id] FROM [dbo].[Libros]";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Crear una lista de objetos Libro
                List<Libros> libros = new List<Libros>();

                while (reader.Read())
                {
                    Libros libro = new Libros();
                    libro.LibroId = reader.GetInt32(0);
                    libro.Titulo = reader.GetString(1);
                    libro.Origen = reader.GetString(2);
                    //libro.AutorId = reader.GetInt32(3);
                    libro.N_paginas = reader.GetInt32(4);
                    libro.Descripcion = reader.GetString(5);
                    //libro.CategoriaId = reader.GetInt32(6);
                    //libro.EditorialId = reader.GetInt32(7);

                    libros.Add(libro);
                }

                // Asignar la lista de libros como origen de datos de la ListView
                listView.ItemsSource = libros;
            }
        }
            private void BuscarPorTitulo(object sender, RoutedEventArgs e)
        {
            string textoBusqueda = PorTitulo.Text.Trim();

            for (int i = 0; i < listView.Items.Count; i++)
            {
                Libros item = (Libros)listView.Items[i];
                ListViewItem listViewItem = listView.ItemContainerGenerator.ContainerFromIndex(i) as ListViewItem;

                if (item.Titulo.Contains(textoBusqueda))
                {
                    // Mostrar el elemento si coincide con la búsqueda
                    listViewItem.Visibility = Visibility.Visible;
                }
                else
                {
                    // Ocultar el elemento si no coincide con la búsqueda
                    listViewItem.Visibility = Visibility.Collapsed;
                }
            }

            PorTitulo.Text = "";
        }


        private void BuscarPorDescripcion(object sender, RoutedEventArgs e)
        {
            string textoBusqueda = PorDescripcion.Text.Trim();

            for (int i = 0; i < listView.Items.Count; i++)
            {
                Libros item = (Libros)listView.Items[i];
                ListViewItem listViewItem = listView.ItemContainerGenerator.ContainerFromIndex(i) as ListViewItem;

                if (item.Descripcion.Contains(textoBusqueda))
                {
                    // Mostrar el elemento si coincide con la búsqueda
                    listViewItem.Visibility = Visibility.Visible;
                }
                else
                {
                    // Ocultar el elemento si no coincide con la búsqueda
                    listViewItem.Visibility = Visibility.Collapsed;
                }
            }

            PorDescripcion.Text = "";
        }

        //private void BuscarPorCategoria(object sender, RoutedEventArgs e) { }


        private void Refrescar(Object sender, RoutedEventArgs e)
        {
            // Mostrar todos los elementos de la lista nuevamente
            for (int i = 0; i < listView.Items.Count; i++)
            {
                ListViewItem listViewItem = listView.ItemContainerGenerator.ContainerFromIndex(i) as ListViewItem;
                if (listViewItem != null)
                {
                    // Mostrar el elemento
                    listViewItem.Visibility = Visibility.Visible;
                }
            }

            // Limpiar el campo de búsqueda
            PorTitulo.Text = "";
        }
    }
}
