using Microsoft.Win32;
using Museo.Views;
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


        private void CrearArchivo_Click(object sender, RoutedEventArgs e)
        {
            Archivo archivo = new Archivo();
            archivo.Show();
            this.Close();

        }
        //private void EditarLibro(object sender, SelectionChangedEventArgs e)
        //{

        //    Libros registroSeleccionado = (Libros)listView.SelectedItem;
        //    int id = registroSeleccionado.LibroId;
        //    string connectionString = "server=DESKTOP-TI2N3QM; database=Museo1 ; integrated security = true";
        //    string query = "SELECT * FROM LIBROS";
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        SqlCommand command = new SqlCommand(query, connection);
        //        connection.Open();
        //        SqlDataReader reader = command.ExecuteReader();
               
        //        while (reader.Read() )
        //        {
        //            int id_edit = reader.GetInt32(0);
        //            if(id_edit == id)
        //            {
        //                string nuevoTitulo = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el nuevo título", "Editar Registro");
        //                string nuevoOrigen = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el nuevo Origen", "Editar Registro");
        //                reader.Close();
        //                string query2 = "Update Libros set Titulo = @Titulo, Origen = @Origen where id_libro=@id";
        //                SqlCommand command2 = new SqlCommand(query2, connection);
        //                command2.Parameters.AddWithValue("@Titulo", nuevoTitulo);
        //                command2.Parameters.AddWithValue("@Origen", nuevoOrigen);
        //                command2.Parameters.AddWithValue("@id", id);
        
        //                command2.ExecuteNonQuery();
             
        //                break;
        //            }
                   
        //        }

        //        reader.Close();
        //    }
        //}
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

            
                dataGrid.ItemsSource = libros;
                       dataGrid.AutoGenerateColumns = false;
            }
        }
        private void BuscarPorTitulo(object sender, RoutedEventArgs e)
        {
            string textoBusqueda = PorTitulo.Text.Trim();

            foreach (var item in dataGrid.Items)
            {
                DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(item);

                if (row != null)
                {
                    Libros libro = (Libros)item;

                    if (libro.Titulo.Contains(textoBusqueda))
                    {
                        // Mostrar la fila si coincide con la búsqueda
                        row.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        // Ocultar la fila si no coincide con la búsqueda
                        row.Visibility = Visibility.Collapsed;
                    }
                }
            }

            PorTitulo.Text = "";
        }


        private void BuscarPorDescripcion(object sender, RoutedEventArgs e)
        {
            string textoBusqueda = PorDescripcion.Text.Trim();

            foreach (var item in dataGrid.Items)
            {
                DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(item);

                if (row != null)
                {
                    Libros libro = (Libros)item;

                    if (libro.Descripcion.Contains(textoBusqueda))
                    {
                        // Mostrar la fila si coincide con la búsqueda
                        row.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        // Ocultar la fila si no coincide con la búsqueda
                        row.Visibility = Visibility.Collapsed;
                    }
                }
            }

            PorDescripcion.Text = "";
        }

        private void BuscarPorCategoria(object sender, RoutedEventArgs e) {
            //string textoBusqueda = PorCategoria.Text.Trim();

            //foreach (var item in dataGrid.Items)
            //{
            //    DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(item);

            //    if (row != null)
            //    {
            //        Libros libro = (Libros)item;

            //        if (libro.CategoriaId.Contains(textoBusqueda))
            //        {
            //            // Mostrar la fila si coincide con la búsqueda
            //            row.Visibility = Visibility.Visible;
            //        }
            //        else
            //        {
            //            // Ocultar la fila si no coincide con la búsqueda
            //            row.Visibility = Visibility.Collapsed;
            //        }
            //    }
            //}

            //PorCategoria.Text = "";
        }

        private void BuscarPorAutor(object sender, RoutedEventArgs e) {

        //    string textoBusqueda = PorAutor.Text.Trim();

        //    foreach (var item in dataGrid.Items)
        //    {
        //        DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(item);

        //        if (row != null)
        //        {
        //            Libros libro = (Libros)item;

        //            if (libro.AutorId.Contains(textoBusqueda))
        //            {
        //                // Mostrar la fila si coincide con la búsqueda
        //                row.Visibility = Visibility.Visible;
        //            }
        //            else
        //            {
        //                // Ocultar la fila si no coincide con la búsqueda
        //                row.Visibility = Visibility.Collapsed;
        //            }
        //        }
        //    }

        //    PorAutor.Text = "";
        }

        private void Refrescar(object sender, RoutedEventArgs e)
        {
            // Mostrar todos los elementos del DataGrid nuevamente
            foreach (var item in dataGrid.Items)
            {
                DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(item);

                if (row != null)
                {
                    // Mostrar la fila
                    row.Visibility = Visibility.Visible;
                }
            }

            // Limpiar el campo de búsqueda
            PorTitulo.Text = "";
        }
        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Editar_Click(Object sender, RoutedEventArgs e)
        {

        }
    }
}
