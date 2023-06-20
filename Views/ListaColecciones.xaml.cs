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
            string connectionString = "server=DESKTOP-TI2N3QM; database=Museo1; integrated security=true";

            string query = @"SELECT Nombre, Cantidad, Periodo, Alto, Ancho, Diametro, Url, Largo, Ingreso, Conservacion, Ubicacion, Integridad, Lugar, Titulo, Materiales, Autores
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

            listView.ItemsSource = piezas;
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