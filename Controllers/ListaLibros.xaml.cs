using Microsoft.Win32;
using Museo.Views;
using Museoapp.Models;
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
        //    string connectionString = "server=DESKTOP-9MTUTME; database=Museo1 ; integrated security = true";
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

            string connectionString = "server=DESKTOP-9MTUTME; database=Museo1 ; integrated security = true";
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
            if (Busqueda1.Text == "Categoria")
            {
                var resultados = from libro in Libro where libro.CategoriaId
                                 == GetCategoriaIdPorNombre(categoriaBusqueda) select libro;

                dataGrid.ItemsSource = resultados.ToList();
                MessageBox.Show("categoria");
            }

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





        private void BuscarPorDescripcion(object sender, RoutedEventArgs e)
        {
            string textoBusqueda = PorDescripcion.Text.Trim();

           

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


        //METODO PARA BUSCAR 
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox; // Accediendo al ComboBox por su nombre
            ComboBoxItem selectedItem = comboBox.SelectedItem as ComboBoxItem;

            

            if (selectedItem != null)
            {
                string selectedItemContent = selectedItem.Content.ToString();

                MessageBox.Show(selectedItemContent);

                // Ejecuta el método según el ítem seleccionado
                switch (selectedItemContent)
                {
                    case "Titulo":
                        MessageBox.Show("Por titulo");
                        break;
                    case "Categoria":
                        MessageBox.Show("Por categoria");

                        //  EjecutarMetodoParaCategoria();
                        break;
                    case "Descripcion":
                        MessageBox.Show("Por descripcion");

                        //  EjecutarMetodoParaDescripcion();
                        break;
                    case "Autor":
                        MessageBox.Show("Por autor");

                        //  EjecutarMetodoParaAutor();
                        break;
                    default:
                        // Código para manejar cualquier otro caso
                        break;
                }
            }
        }






    }
}
