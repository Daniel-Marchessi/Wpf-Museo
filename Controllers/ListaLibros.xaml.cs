using Microsoft.Win32;
using Museo.Models;
using Museo.Views;
using Museoapp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
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
            string connectionString = "server=DESKTOP-TI2N3QM; database=Museo1; integrated security=true;MultipleActiveResultSets=True";
            string query = "SELECT [id_libro] ,[Titulo],[Origen] ,[N_paginas] ,[Descripcion], [Edicion], [AnioEdicion], [Codigo], [Categoria_id],[Editorial_id] FROM [dbo].[Libros]";
            string queryautores = "SELECT Autor.Nombre, Autor.Apellido, Libros.id_libro " +
                               "FROM Autor " +
                               "JOIN Libro_Autor ON Autor.id_autor = Libro_Autor.id_autor " +
                               "JOIN Libros ON Libros.id_libro = Libro_Autor.id_libro " +
                               "WHERE Libros.id_libro = @libroId";

            string querycategoria = "SELECT Categoria.Nombre FROM Categoria " +
                                "JOIN Libros ON Categoria.id_categoria = Libros.Categoria_id " +
                                "WHERE Libros.id_libro = @libroId";

            string queryeditorial = "SELECT Editorial.Nombre FROM Editorial " +
                                    "JOIN Libros ON Editorial.id_editorial = Libros.Editorial_id " +
                                    "WHERE Libros.id_libro = @libroId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
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
                    libro.Codigo = reader.GetInt32(7);
                    libro.CategoriaId = reader.GetInt32(8);
                    libro.EditorialId = reader.GetInt32(9);
                    List<Autores> autoresLista = new List<Autores>();

                    using (SqlCommand autoresCommand = new SqlCommand(queryautores, connection))
                    {
                        autoresCommand.Parameters.AddWithValue("@libroId", libro.LibroId);
                        SqlDataReader autoresReader = autoresCommand.ExecuteReader();
                        while (autoresReader.Read())
                        {
                            string nombreAutor = autoresReader.GetString(0);
                            string apellidoAutor = autoresReader.GetString(1);
                            string nombreCompletoAutor = $"{nombreAutor} {apellidoAutor}";
                            autoresLista.Add(new Autores { NombreCompleto = nombreCompletoAutor });
                        }
                        autoresReader.Close();
                        dataGrid.ItemsSource = autoresLista;
                    }

                    string nombresAutores = string.Join(", ", autoresLista.Select(a => a.NombreCompleto));
                    libro.Autores = nombresAutores;

                    List<Categorias> categoriasLista = new List<Categorias>();
                    using (SqlCommand categoriasCommand = new SqlCommand(querycategoria, connection))
                    {
                        categoriasCommand.Parameters.AddWithValue("@libroId", libro.LibroId);
                        SqlDataReader categoriasReader = categoriasCommand.ExecuteReader();
                        while (categoriasReader.Read())
                        {
                            string nombreCategoria = categoriasReader.GetString(0);
                            Categorias categorias = new Categorias { Nombre = nombreCategoria };
                            categoriasLista.Add(categorias);
                        }
                        categoriasReader.Close();
                        dataGrid.ItemsSource = categoriasLista;
                    }
                    string nombresCategorias = string.Join(", ", categoriasLista.Select(m => m.Nombre));
                    libro.Categorias = nombresCategorias;

                    List<Editoriales> editorialesLista = new List<Editoriales>();
                    using (SqlCommand editorialesCommand = new SqlCommand(queryeditorial, connection))
                    {
                        editorialesCommand.Parameters.AddWithValue("@libroId", libro.LibroId);

                        SqlDataReader editorialesReader = editorialesCommand.ExecuteReader();
                        while (editorialesReader.Read())
                        {
                            string nombreEditorial = editorialesReader.GetString(0);
                            Editoriales editoriales = new Editoriales { Nombre = nombreEditorial };
                            editorialesLista.Add(editoriales);
                        }
                        editorialesReader.Close();
                        dataGrid.ItemsSource = editorialesLista;
                    }
                    string nombresEditoriales = string.Join(", ", editorialesLista.Select(m => m.Nombre));
                    libro.Editoriales = nombresEditoriales;
                    libros.Add(libro);
                }
                dataGrid.AutoGenerateColumns = false;
                dataGrid.ItemsSource = libros;
                reader.Close();
                connection.Close();
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
                    string queryDeleteLibro = "DELETE FROM [dbo].[Libros] WHERE [id_libro] = @id_libro";
                    string queryDeleteLibro_Autor = "DELETE FROM [dbo].[Libro_Autor] WHERE [id_libro] = @id_libro";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(queryDeleteLibro_Autor, connection);
                        command.Parameters.AddWithValue("@id_libro", libro.LibroId);

                        SqlCommand command2 = new SqlCommand(queryDeleteLibro, connection);
                        command2.Parameters.AddWithValue("@id_libro", libro.LibroId);

                        command.ExecuteNonQuery();

                        int rowsAffected = command2.ExecuteNonQuery();

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

