using ControlzEx.Standard;
using Museo.Models;
using Museo.Views;
using Museoapp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfAppTEST;
using WpfAppTEST.Models;
using WpfAppTEST.Views;
using Xceed.Wpf.Toolkit;
using MessageBox = Xceed.Wpf.Toolkit.MessageBox;
using Museo.Controllers;
using System.Windows.Automation;

namespace Museoapp.Views
{
    public partial class ListaColecciones : Window
    {
        public ListaColecciones()
        {
            InitializeComponent();
            Loaded += ListaColecciones_Loaded;
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
        private void ListaColecciones_Loaded(object sender, RoutedEventArgs e)
        {
            Autorizaciones autorizaciones = new Autorizaciones();
            DataGridColumn columnaAEditar = dataGrid.Columns[17];
            DataGridColumn columnaAEliminar = dataGrid.Columns[16];
            autorizaciones.Autorizacion(sender, e, columnaAEditar, columnaAEliminar);
            ListarColecciones();
        }




        //CRUD
        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            Piezas piezin = (Piezas)dataGrid.SelectedItem;

            if (piezin != null)
            {
                Museo.Views.EditarColeccion ventanaEditar = new Museo.Views.EditarColeccion(piezin.Nombre, piezin.Cantidad, piezin.Periodo,
                                   piezin.Alto, piezin.Ancho, piezin.Diametro, piezin.UrlFoto, piezin.Largo, piezin.Ingreso,
                                   piezin.Conservacion, piezin.Ubicacion, piezin.Integridad, piezin.Cultura, piezin.Titulo_alias, piezin.Coleccion_id);
                ventanaEditar.ShowDialog();
            }
            ListarColecciones();
        }

        private void ListarColecciones()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
            string query = @"SELECT Nombre, Cantidad, Periodo, Alto, Ancho, Diametro, Url, Largo, Ingreso, Conservacion, Ubicacion, Integridad, Cultura, Titulo, Coleccion_id
                     FROM dbo.Coleccion";
            string queryautores = "SELECT Autor.Nombre, Autor.Apellido " +
                     "FROM Autor " +
                     "JOIN Coleccion_Autor ON Autor.id_autor = Coleccion_Autor.id_autor " +
                     "JOIN Coleccion ON Coleccion_Autor.id_coleccion = Coleccion.Coleccion_id " +
                     "WHERE Coleccion.Coleccion_id = @coleccionId";
            string queryMateriales = "SELECT Material.Nombre " +
                         "FROM Material " +
                         "JOIN Coleccion_Material ON Material.id_material = Coleccion_Material.id_material " +
                         "JOIN Coleccion ON Coleccion_Material.id_coleccion = Coleccion.Coleccion_id " +
                         "WHERE Coleccion.Coleccion_id = @coleccionId";

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
                    coleccion.Cultura = reader.GetString(12);
                    coleccion.Titulo_alias = reader.GetString(13);
                    coleccion.Coleccion_id = reader.GetInt32(14);
                    if (!string.IsNullOrEmpty(coleccion.UrlFoto))
                    {
                        coleccion.Imagen = new BitmapImage(new Uri(coleccion.UrlFoto));
                    }
                   
                    List<Autores> autoresLista = new List<Autores>();
                    List<Materiales> materialesLista = new List<Materiales>();
                    using (SqlCommand autoresCommand = new SqlCommand(queryautores, connection))
                    {
                        autoresCommand.Parameters.AddWithValue("@coleccionId", coleccion.Coleccion_id);

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

                    coleccion.Autores = nombresAutores;


                    using (SqlCommand materialesCommand = new SqlCommand(queryMateriales, connection))
                    {
                        materialesCommand.Parameters.AddWithValue("@coleccionId", coleccion.Coleccion_id);

                        SqlDataReader materialesReader = materialesCommand.ExecuteReader();
                        while (materialesReader.Read())
                        {
                            string nombreMaterial = materialesReader.GetString(0);

                            Materiales material = new Materiales { Nombre = nombreMaterial };

                            materialesLista.Add(material);
                        }
                        materialesReader.Close();                 
                        string nombresMateriales = string.Join(", ", materialesLista.Select(m => m.Nombre));
                        coleccion.Materiales = nombresMateriales;
                        dataGrid.ItemsSource = materialesLista;
                    }   
                    piezas.Add(coleccion);
                }
                reader.Close();
                connection.Close();
            }
            dataGrid.AutoGenerateColumns = false;
            dataGrid.ItemsSource = piezas;
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
        }

        //Metodo para mostrar imagen grande al clickear
        private void dobleclikmagen(object sender, MouseButtonEventArgs e)
        {
            Button button = (Button)sender;

            if (button.DataContext is Piezas selectedPieza)
            {
                // Verifica si hay una foto disponible
                if (selectedPieza.Imagen != null)
                {
                    // Crea una nueva ventana para mostrar la foto
                    Window imageWindow = new Window
                    {
                        //  WindowState = WindowState.Normal,
                        Content = new Image
                        {
                            Source = selectedPieza.Imagen,
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
                    string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
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
                        MessageBox.Show(coleccion.Coleccion_id.ToString());
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


        //metodo para realizar busqeudas

        private void BuscarPorTitulo(object sender, RoutedEventArgs e)
        {

            string textSearch = search.Text.Trim();

            if (Busqueda1.Text == "Nombre")
            {

                foreach (var item in dataGrid.Items)
                {
                    //Type prueba = item.GetType();
                    //MessageBox.Show(prueba.ToString());

                    if (item is Piezas pieza)
                    {
                        if (pieza.Nombre.Contains(textSearch))
                        {
                            // Mostrar la fila si coincide con la búsqueda
                            dataGrid.UpdateLayout();
                            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(pieza);
                            if (row != null)
                            {
                                row.Visibility = Visibility.Visible;
                            }
                        }
                        else
                        {
                            // Ocultar la fila si no coincide con la búsqueda
                            dataGrid.UpdateLayout();
                            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(pieza);
                            if (row != null)
                            {
                                row.Visibility = Visibility.Collapsed;
                            }

                        }
                    }

                }

            }
            if (Busqueda1.Text == "Titulo_Alias")
            {
                foreach (var item in dataGrid.Items)
                {

                    if (item is Piezas pieza)
                    {
                        if (pieza.Titulo_alias.Contains(textSearch))
                        {

                            // Mostrar la fila si coincide con la búsqueda
                            dataGrid.UpdateLayout();
                            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(pieza);
                            if (row != null)
                            {
                                row.Visibility = Visibility.Visible;
                            }
                        }
                        else
                        {
                            // Ocultar la fila si no coincide con la búsqueda
                            dataGrid.UpdateLayout();
                            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(pieza);
                            if (row != null)
                            {
                                row.Visibility = Visibility.Collapsed;
                            }

                        }
                    }

                }
            }
            if (Busqueda1.Text == "Cultura")

            {
                foreach (var item in dataGrid.Items)
                {

                    if (item is Piezas pieza)
                    {
                        if (pieza.Cultura.Contains(textSearch))
                        {
                            // Mostrar la fila si coincide con la búsqueda

                            dataGrid.UpdateLayout();
                            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(pieza);
                            if (row != null)
                            {
                                row.Visibility = Visibility.Visible;
                            }
                        }
                        else
                        {
                            // Ocultar la fila si no coincide con la búsqueda
                            dataGrid.UpdateLayout();
                            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(pieza);
                            if (row != null)
                            {
                                row.Visibility = Visibility.Collapsed;
                            }

                        }
                    }
                }
            }
            //BUSCA PERO POR NOMBRE Y APELLIDO
            if (Busqueda1.Text == "Autor")
            {
                // CON ESTA CONSULTA TRAIGO TODOS LOS DATOS DE LA COLECCION A PARTIR DE UN ID
                string nombre = textSearch;
                string query = "SELECT Autor.id_autor, Autor.Nombre,Coleccion.Nombre, Coleccion.Coleccion_id, Autor.Apellido FROM Coleccion_Autor " +
                              "JOIN Coleccion ON Coleccion.Coleccion_id = Coleccion_Autor.id_coleccion " +
                              "JOIN Autor ON Autor.id_autor = Coleccion_Autor.id_autor " +
                              "WHERE Autor.Nombre + ' ' + Autor.Apellido LIKE @AutorNombre";

               // string autores = "select Autor.Nombre, Autor.Apellido from Autor join " +
               //"Coleccion_Autor on Autor.id_autor= Coleccion_Autor.id_autor" +
               //" join Coleccion on Coleccion_Autor.id_coleccion = Coleccion.Coleccion_id";

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
                                int coleccionId = reader.GetInt32(3);
                                string apellidoAutor = reader.GetString(4);
                                autores_ids.Add(coleccionId);
                                autores_list.Add(nombreAutor + " " + apellidoAutor);
                            }
                        }
                    }
                }
                    foreach (var it in dataGrid.Items)
                    {
                    if (it is Piezas pieza1)
                        {
                        bool autorEncontrado = autores_ids.Contains(pieza1.Coleccion_id);

                        if (autorEncontrado)
                            {
                               // MessageBox.Show(pieza1.Coleccion_id.ToString());
                                //dataGrid.UpdateLayout();
                                DataGridRow row1 = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(pieza1);
                                if (row1 != null)
                                {
                                   // MessageBox.Show("Ingreso");
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
                                //dataGrid.UpdateLayout();
                                DataGridRow row1 = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(pieza1);
                                if (row1 != null)
                                {
                                    row1.Visibility = Visibility.Collapsed;
                                }
                            }
                    }
                
                }

            }
            //string textoBusqueda = PorTitulo.Text.Trim();
        }
    }

}