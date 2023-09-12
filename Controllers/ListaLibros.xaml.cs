using Microsoft.Win32;
using Museo.Models;
using Museo.Views;
using Museoapp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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
        private void ListarLibros()
        {

            string connectionString = "server=DESKTOP-TI2N3QM; database=Museo1 ; integrated security = true";
            string query = "SELECT [id_libro] ,[Titulo],[Origen] ,[N_paginas] ,[Descripcion], [Edicion], [AnioEdicion],[Autores], [Codigo] FROM [dbo].[Libros]";
           
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
                    libro.N_paginas = reader.GetInt32(3);
                    libro.Descripcion = reader.GetString(4);
                    libro.Edicion = reader.GetString(5);
                    libro.AnioEdicion = reader.GetString(6);
                    libro.Autores = reader.GetString(7);
                    libro.Codigo = reader.GetInt32(8);
                    //libro.CategoriaId = reader.GetInt32(6);
                    //libro.EditorialId = reader.GetInt32(7);

                    libros.Add(libro);
                }


                dataGrid.ItemsSource = libros;
                dataGrid.AutoGenerateColumns = false;
            }
        }


        private void BuscarPorTitulo(object sender, RoutedEventArgs e)
        {
            string textSearch = search.Text.Trim();

            if (Busqueda1.Text == "Titulo")
            {

                foreach (var item in dataGrid.Items)
                {

                    if (item is Libros libro)
                    {
                        if (libro.Titulo.Contains(textSearch))
                        {
                            // Mostrar la fila si coincide con la búsqueda
                            dataGrid.UpdateLayout();
                            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(libro);
                            if (row != null)
                            {
                                row.Visibility = Visibility.Visible;
                            }
                        }
                        else
                        {
                            // Ocultar la fila si no coincide con la búsqueda
                            dataGrid.UpdateLayout();
                            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(libro);
                            if (row != null)
                            {
                                row.Visibility = Visibility.Collapsed;
                            }

                        }
                    }
                    MessageBox.Show("Busqueda por titulo");
                }

            }
            //if (Busqueda1.Text == "Categoria")
            //{
            //    var resultados = from libro in Libro where libro.CategoriaId
            //                     == GetCategoriaIdPorNombre(categoriaBusqueda) select libro;

            //    dataGrid.ItemsSource = resultados.ToList();
            //    MessageBox.Show("categoria");
            //}

            if (Busqueda1.Text == "Descripcion")

            {
                foreach (var item in dataGrid.Items)
                {

                    if (item is Libros libro)
                    {
                        if (libro.Descripcion.Contains(textSearch))
                        {
                            // Mostrar la fila si coincide con la búsqueda
                            dataGrid.UpdateLayout();
                            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(libro);
                            if (row != null)
                            {
                                row.Visibility = Visibility.Visible;
                            }
                        }
                        else
                        {
                            // Ocultar la fila si no coincide con la búsqueda
                            dataGrid.UpdateLayout();
                            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(libro);
                            if (row != null)
                            {
                                row.Visibility = Visibility.Collapsed;
                            }

                        }
                    }

                }
            }
            if (Busqueda1.Text == "Autor")
            {
                foreach (var item in dataGrid.Items)
                {
                    if (item is Libros libro)
                    {

                        if (libro.Titulo.Contains(textSearch))
                        {
                            // Mostrar la fila si coincide con la búsqueda
                            dataGrid.UpdateLayout();
                            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(libro);
                            if (row != null)
                            {
                                row.Visibility = Visibility.Visible;
                            }
                        }
                        else
                        {
                            // Ocultar la fila si no coincide con la búsqueda
                            dataGrid.UpdateLayout();
                            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(libro);
                            if (row != null)
                            {
                                row.Visibility = Visibility.Collapsed;
                            }

                        }
                    }
                }
            }

            //  string textoBusqueda = PorTitulo.Text.Trim();
        }


        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            Button eliminarButton = (Button)sender;
            if (eliminarButton.CommandParameter is Libros libro)
            {
                MessageBoxResult result = MessageBox.Show("¿Estás seguro de eliminar este registro?", "Confirmación de eliminación", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    string connectionString = "server=DESKTOP-TI2N3QM; database=Museo1; integrated security = true";
                    string query = "DELETE FROM [dbo].[Libros] WHERE [id_libro] = @id_libro";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@id_libro", libro.LibroId);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("El archivo se eliminó correctamente.");
                            ListarLibros();
                        }
                    }
                   
                }
            }
        }
        private void Editar_Click(Object sender, RoutedEventArgs e)
        {
            Libros libro = (Libros)dataGrid.SelectedItem;

            if (libro != null)
            {
                EditarLibro ventanaEditar = new EditarLibro(libro.LibroId, libro.Titulo, libro.Origen, libro.N_paginas, libro.Descripcion, libro.Edicion, libro.AnioEdicion, libro.Autores, libro.Codigo);

                ventanaEditar.ShowDialog();

            }
            ListarLibros();
        }


       
    }
}

