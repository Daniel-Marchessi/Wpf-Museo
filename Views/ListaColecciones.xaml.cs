using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfAppTEST.Models;

namespace Museoapp.Views
{

    public partial class ListaColecciones : Window
    {
        public ListaColecciones()
        {
            InitializeComponent();

            string connectionString = "server=DESKTOP-TI2N3QM; database=Museo1 ; integrated security = true";
            string query = "SELECT [Coleccion_id], [Nombre], [Cantidad], [Titulo_alias], [Lugar_proce], " +
                "[Periodo], [Material_id], [Alto], [Ancho], [Largo], [Diametro], [Integridad], " +
                "[Conservacion], [Ubicacion], [Ingreso], [Localidad_id], [Autor_id], [Foto] " +
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

                    piezas.Add(coleccion);
                }

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

        private void BuscarPorNombre(object sender, EventArgs e)
        {
            string textoBusqueda = PorNombre.Text;

           
            listView.SelectedItems.Clear();

          
            for (int i = 0; i < listView.Items.Count; i++)
            {
                Piezas item = (Piezas)listView.Items[i];
                if (item.Nombre.Contains(textoBusqueda))
                {
                    listView.SelectedItem = item;
                    listView.Focus();
                    listView.ScrollIntoView(item);
                    PorNombre.Text = "";

                }
            }
        }
        private void BuscarPorlugar(object sender, EventArgs e)
        {
            string textoBusqueda = Porlugar.Text;

            listView.SelectedItems.Clear();

      
            for (int i = 0; i < listView.Items.Count; i++)
            {
                Piezas item = (Piezas)listView.Items[i];
                if (item.Lugar_proce.Contains(textoBusqueda))
                {
                    listView.SelectedItem = item;
                    listView.Focus();
                    listView.ScrollIntoView(item);
                    Porlugar.Text = "";

                }
            }
        }
        private void BuscarPorTituloAlias(object sender, EventArgs e)
        {
            string textoBusqueda = PorTitulo.Text;

      
            listView.SelectedItems.Clear();

           
            for (int i = 0; i < listView.Items.Count; i++)
            {
                Piezas item = (Piezas)listView.Items[i];
                if (item.Titulo_alias.Contains(textoBusqueda))
                {
                    listView.SelectedItem = item;
                    listView.Focus();
                    listView.ScrollIntoView(item);
                    PorTitulo.Text = "";
                }
            }
        }


    }
}