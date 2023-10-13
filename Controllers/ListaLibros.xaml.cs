using Microsoft.Win32;
using Museo.Controllers;
using Museo.Models;
using Museo.Views;
using Museoapp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfAppTEST.Models;
using WpfAppTEST.Views;
using Museo.Controllers;

namespace Museoapp.Views
{
    public partial class ListaLibros : Window
    {
        public ListaLibros()
        {
            InitializeComponent();
            Loaded += ListaLibros_Loaded;
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

        //Autorizacion
        private void ListaLibros_Loaded(object sender, RoutedEventArgs e)
        {
            Autorizaciones autorizaciones = new Autorizaciones();
            DataGridColumn columnaAEditar = dataGrid.Columns[11];
            DataGridColumn columnaAEliminar = dataGrid.Columns[10];
            autorizaciones.Autorizacion(sender, e, columnaAEditar, columnaAEliminar);
            ListarLibros();
        }

        private void ListarLibros()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
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
                    if (item is Libros libro1)
                    {
                        if (libro1.Titulo.Contains(textSearch))
                        {
                            // Mostrar la fila si coincide con la búsqueda
                            dataGrid.UpdateLayout();
                            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(libro1);
                            if (row != null)
                            {
                                row.Visibility = Visibility.Visible;
                            }
                        }
                        else
                        {
                            // Ocultar la fila si no coincide con la búsqueda
                            dataGrid.UpdateLayout();
                            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(libro1);
                            if (row != null)
                            {
                                row.Visibility = Visibility.Collapsed;
                            }

                        }
                    }
                }

            }
            if (Busqueda1.Text == "Categoria")
            {
                string nombre = textSearch;
                string query = "SELECT Categoria.id_categoria, Categoria.Nombre, Libros.id_libro " +
                "FROM Categoria " +
                "JOIN Libros ON Categoria.id_categoria = Libros.Categoria_id " +
                "WHERE Categoria.Nombre LIKE @CategoriaNombre";

                string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
                List<string> categorias_list = new List<string>();
                List<int> categorias_ids = new List<int>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CategoriaNombre", "%" + nombre + "%");

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int autor_id = reader.GetInt32(0);
                                string nombrecategoria = reader.GetString(1);

                                int libro_id = reader.GetInt32(2);

                                categorias_ids.Add(libro_id);
                                categorias_list.Add(nombrecategoria);
                            }
                        }
                    }
                }
                foreach (var it in dataGrid.Items)
                {
                    if (it is Libros libro1)
                    {
                        bool autorEncontrado = categorias_ids.Contains(libro1.LibroId);

                        if (autorEncontrado)
                        {

                            DataGridRow row1 = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(libro1);
                            if (row1 != null)
                            {
                                row1.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                row1.Visibility = Visibility.Collapsed;

                            }

                        }
                        else
                        {
                            // Ocultar la fila si no coincide con la búsqueda
                            DataGridRow row1 = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(libro1);
                            if (row1 != null)
                            {
                                row1.Visibility = Visibility.Collapsed;
                            }
                        }
                    }

                }
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
                // CON ESTA CONSULTA TRAIGO TODOS LOS DATOS DE LA COLECCION A PARTIR DE UN ID

                string nombre = textSearch;

                string query = "SELECT Autor.id_autor, Autor.Nombre,Libros.Titulo, Libros.id_libro, Autor.Apellido FROM Libro_Autor " +
                              "JOIN Libros ON Libros.id_libro = Libro_Autor.id_libro " +
                              "JOIN Autor ON Autor.id_autor = Libro_Autor.id_autor " +
                              "WHERE Autor.Nombre + ' ' + Autor.Apellido LIKE @AutorNombre";

              

                string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
                List<string> autores_list = new List<string>();
                List<int> autores_ids = new List<int>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@AutorNombre", "%" + nombre + "%");

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id_autor = reader.GetInt32(0);
                                string nombreAutor = reader.GetString(1);
                                string nombreColeccion = reader.GetString(2);
                                int libroId = reader.GetInt32(3);
                                string apellidoAutor = reader.GetString(4);
                                autores_ids.Add(libroId);
                                autores_list.Add(nombreAutor + " " + apellidoAutor);
                            }
                        }
                    }
                }
                foreach (var it in dataGrid.Items)
                {
                    if (it is Libros libro1)
                    {
                        bool autorEncontrado = autores_ids.Contains(libro1.LibroId);

                        if (autorEncontrado)
                        {
                            
                            DataGridRow row1 = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(libro1);
                            if (row1 != null)
                            {
                                row1.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                row1.Visibility = Visibility.Collapsed;

                            }

                        }
                        else
                        {
                            // Ocultar la fila si no coincide con la búsqueda
                            DataGridRow row1 = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(libro1);
                            if (row1 != null)
                            {
                                row1.Visibility = Visibility.Collapsed;
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
                    string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
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

