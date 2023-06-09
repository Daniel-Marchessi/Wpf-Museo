﻿using Museo.Views;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfAppTEST.Models;
using WpfAppTEST.Views;

namespace Museoapp.Views
{

    public partial class ListaColecciones : Window
    {
        public ListaColecciones()
        {
            InitializeComponent();
            ListarColecciones();
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

        private void CrearListaLibro_Click(object sender, RoutedEventArgs e)
        {
            ListaLibros listalibro = new ListaLibros();
            listalibro.Show();
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


        private void ListarColecciones()
        {
            string connectionString = "server=DESKTOP-TI2N3QM; database=Museo1; integrated security=true";

            string query = @"SELECT Nombre, Cantidad, Periodo, Alto, Ancho, Diametro, Url, Largo, Ingreso, Conservacion, Ubicacion, Integridad, Lugar, Titulo, Materiales, Autores, Coleccion_id
                     FROM dbo.Coleccion";
            //string queryMateriales = @"SELECT Material.Nombre
            //                   FROM Material
            //                   JOIN Coleccion_Material ON Material.id_material = Coleccion_Material.id_material
            //                   WHERE Coleccion_Material.id_coleccion = @ColeccionId";

            List<Piezas> piezas = new List<Piezas>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Piezas coleccion = new Piezas();
                    coleccion.Nombre = reader.GetString(0);
                    coleccion.Cantidad = reader.GetInt32(1);
                    coleccion.Periodo = reader.GetString(2);
                    coleccion.Alto = reader.GetInt32(3);
                    coleccion.Ancho = reader.GetInt32(4);
                    coleccion.Diametro = reader.GetDouble(5);
                    coleccion.UrlFoto = reader.GetString(6);
                    coleccion.Largo = reader.GetInt32(7);
                    coleccion.Ingreso = reader.GetString(8);
                    coleccion.Conservacion = reader.GetString(9);
                    coleccion.Ubicacion = reader.GetString(10);
                    coleccion.Integridad = reader.GetString(11);
                    coleccion.Lugar_proce = reader.GetString(12);
                    coleccion.Titulo_alias = reader.GetString(13);
                    coleccion.Materiales = reader.GetString(14);
                    coleccion.Autores = reader.GetString(15);
                    coleccion.Coleccion_id = reader.GetInt32(16);

                    if (!reader.IsDBNull(6))
                    {
                        string fotoUrl = reader.GetString(6);
                        coleccion.Foto = LoadImageFromUrl(fotoUrl);
                    }
                    else
                    {
                        coleccion.Foto = null;
                    }

                    piezas.Add(coleccion);
                }

                reader.Close();

                connection.Close();
            }
            dataGrid.AutoGenerateColumns = false;
            dataGrid.ItemsSource = piezas;
        }
        private ImageSource LoadImageFromUrl(string url)
            {
                try
                {
                    BitmapImage image = new BitmapImage(new Uri(url));
                    return image;
                }
                catch (Exception)
                {
                    return null;
                }
            }

        private void BuscarPorNombre(object sender, RoutedEventArgs e)
        {
            string textoBusqueda = PorNombre.Text.Trim();

            for (int i = 0; i < dataGrid.Items.Count; i++)
            {
                Piezas item = (Piezas)dataGrid.Items[i];
                DataGridRow dataGridRow = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(item);
                if (dataGrid != null)
                {
                    if (item.Nombre.Contains(textoBusqueda))
                    {
                        // Mostrar el elemento si coincide con la búsqueda
                        dataGrid.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        // Ocultar el elemento si no coincide con la búsqueda
                        dataGrid.Visibility = Visibility.Collapsed;
                    }
                }
            }

            PorNombre.Text = "";
        }

        private void BuscarPorLugar(object sender, RoutedEventArgs e)
        {
            string textoBusqueda = Porlugar.Text.Trim();

            for (int i = 0; i < dataGrid.Items.Count; i++)
            {
                Piezas item = (Piezas)dataGrid.Items[i];
                DataGridRow dataGridRow = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(item);

                if (item.Lugar_proce.Contains(textoBusqueda))
                {
                    // Mostrar el elemento si coincide con la búsqueda
                    dataGridRow.Visibility = Visibility.Visible;
                }
                else
                {
                    // Ocultar el elemento si no coincide con la búsqueda
                    dataGridRow.Visibility = Visibility.Collapsed;
                }
            }

            Porlugar.Text = "";
        }

        private void BuscarPorTitulo(object sender, RoutedEventArgs e)
        {
            string textoBusqueda = PorTitulo.Text.Trim();

            for (int i = 0; i < dataGrid.Items.Count; i++)
            {
                Piezas item = (Piezas)dataGrid.Items[i];
                DataGridRow dataGridRow = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(item);

                if (item.Titulo_alias.Contains(textoBusqueda))
                {
                    // Mostrar el elemento si coincide con la búsqueda
                    dataGrid.Visibility = Visibility.Visible;
                }
                else
                {
                    // Ocultar el elemento si no coincide con la búsqueda
                    dataGrid.Visibility = Visibility.Collapsed;
                }
            }

            PorTitulo.Text = "";
        }
        private void Refrescar(Object sender, RoutedEventArgs e)
        {
            // Mostrar todos los elementos de la lista nuevamente
            for (int i = 0; i < dataGrid.Items.Count; i++)
            {
                Piezas item = (Piezas)dataGrid.Items[i];
                DataGridRow dataGridRow = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(item);

                if (dataGrid != null)
                {
                    // Mostrar el elemento
                    dataGrid.Visibility = Visibility.Visible;
                }
            }

            // Limpiar el campo de búsqueda
            PorTitulo.Text = "";
            Porlugar.Text = "";
            PorNombre.Text = "";
        }
        private void dobleclikmagen(object sender, MouseButtonEventArgs e)
        {
            Button button = (Button)sender;

            if (button.DataContext is Piezas selectedPieza)
            {
                // Verifica si hay una foto disponible
                if (selectedPieza.Foto != null)
                {
                    // Crea una nueva ventana para mostrar la foto
                    Window imageWindow = new Window
                    {
                        WindowState = WindowState.Normal,
                        Content = new Image
                        {
                            Source = selectedPieza.Foto,
                            Stretch = Stretch.Uniform,
                        }
                    };

                    // Muestra la ventana
                    imageWindow.ShowDialog();
                }
            }
        }

        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            Button eliminarButton = (Button)sender;
            if (eliminarButton.CommandParameter is Piezas coleccion)
            {
                MessageBoxResult result = MessageBox.Show("¿Estás seguro de eliminar este registro?", "Confirmación de eliminación", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    string connectionString = "server=DESKTOP-TI2N3QM; database=Museo1; integrated security = true";
                    string queryMaterial = "DELETE FROM [dbo].[Coleccion_Material] WHERE id_coleccion = @idColeccion; ";
                    string queryColeccion = "DELETE FROM [dbo].[Coleccion] WHERE Coleccion_id = @idColeccion; ";
                    string queryAutor = "DELETE FROM [dbo].[Coleccion_Autor] WHERE id_coleccion = @idColeccion; ";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Eliminar registros relacionados en la tabla Coleccion_Material
                        SqlCommand commandMaterial = new SqlCommand(queryMaterial, connection);
                        commandMaterial.Parameters.AddWithValue("@idColeccion", coleccion.Coleccion_id);
                        commandMaterial.ExecuteNonQuery();

                        // Eliminar el registro en la tabla Coleccion_Autor
                        SqlCommand commandAutor = new SqlCommand(queryAutor, connection);
                        commandAutor.Parameters.AddWithValue("@idColeccion", coleccion.Coleccion_id);
                        commandAutor.ExecuteNonQuery();

                        // Eliminar el registro en la tabla Coleccion
                        SqlCommand commandColeccion = new SqlCommand(queryColeccion, connection);
                        commandColeccion.Parameters.AddWithValue("@idColeccion", coleccion.Coleccion_id);
                      
                        int rowsAffectedColeccion = commandColeccion.ExecuteNonQuery();

                        if (rowsAffectedColeccion > 0)
                        {
                            MessageBox.Show("El registro se eliminó correctamente.");
                        }
                    }

                    ListarColecciones();
                }
            }
        }
        private void Editar_Click(object sender, RoutedEventArgs e) { }

    }
}