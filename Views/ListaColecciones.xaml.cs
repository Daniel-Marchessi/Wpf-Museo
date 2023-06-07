using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
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
        private void ListarColecciones()
        {
            string connectionString = "server=DESKTOP-TI2N3QM; database=Museo1 ; integrated security = true";

            string query = "SELECT [Coleccion_id], [Nombre], [Cantidad], [Titulo_alias], [Lugar_proce], " +
                "[Periodo], [Material_id], [Alto], [Ancho], [Largo], [Diametro], [Integridad], " +
                "[Conservacion], [Ubicacion], [Ingreso], [Localidad_id], [Autor_id], [Foto], [Materiales] " +
                "FROM [dbo].[Coleccion]";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();


                List<Piezas> piezas = new List<Piezas>();

                while (reader.Read())
                {
                    Piezas coleccion = new Piezas();
                    coleccion.Coleccion_id = reader.GetInt32(0);
                    coleccion.Nombre = reader.GetString(1);
                    coleccion.Cantidad = reader.GetInt32(2);
                    coleccion.Titulo_alias = reader.GetString(3);
                    coleccion.Lugar_proce = reader.GetString(4);
                    coleccion.Periodo = reader.GetString(5);
                    //coleccion.MaterialId = reader.GetInt32(6);
                    coleccion.Alto = reader.GetInt32(7);
                    coleccion.Ancho = reader.GetInt32(8);
                    coleccion.Largo = reader.GetInt32(9);
                    coleccion.Diametro = reader.GetDouble(10);
                    coleccion.Integridad = reader.GetString(11);
                    coleccion.Conservacion = reader.GetString(12);
                    coleccion.Ubicacion = reader.GetString(13);
                    coleccion.Ingreso = reader.GetString(14);
                    //coleccion.LocalidadId = reader.GetInt32(15);
                    //coleccion.AutorId = reader.GetInt32(16);
                  
                    if (!reader.IsDBNull(17))
                    {
                        string fotoUrl = reader.GetString(17);
                        coleccion.Foto = LoadImageFromUrl(fotoUrl);
                    }
                    else
                    {
                        coleccion.Foto = null;
                    }

                    if (!reader.IsDBNull(18))
                    {
                        coleccion.Materiales = reader.GetString(18);
                    }
                    else
                    {
                        coleccion.Materiales = string.Empty;
                    }


                    piezas.Add(coleccion);
                }
                connection.Close();

                listView.ItemsSource = piezas;
            }
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

            for (int i = 0; i < listView.Items.Count; i++)
            {
                Piezas item = (Piezas)listView.Items[i];
                ListViewItem listViewItem = listView.ItemContainerGenerator.ContainerFromIndex(i) as ListViewItem;

                if (listViewItem != null)
                {
                    if (item.Nombre.Contains(textoBusqueda))
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
            }

            PorNombre.Text = "";
        }

        private void BuscarPorLugar(object sender, RoutedEventArgs e)
        {
            string textoBusqueda = Porlugar.Text.Trim();

            for (int i = 0; i < listView.Items.Count; i++)
            {
                Piezas item = (Piezas)listView.Items[i];
                ListViewItem listViewItem = listView.ItemContainerGenerator.ContainerFromIndex(i) as ListViewItem;

                if (item.Lugar_proce.Contains(textoBusqueda))
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

            Porlugar.Text = "";
        }

        private void BuscarPorTitulo(object sender, RoutedEventArgs e)
        {
            string textoBusqueda = PorTitulo.Text.Trim();

            for (int i = 0; i < listView.Items.Count; i++)
            {
                Piezas item = (Piezas)listView.Items[i];
                ListViewItem listViewItem = listView.ItemContainerGenerator.ContainerFromIndex(i) as ListViewItem;

                if (item.Titulo_alias.Contains(textoBusqueda))
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
            Porlugar.Text = "";
            PorNombre.Text = "";
        }
    }
}