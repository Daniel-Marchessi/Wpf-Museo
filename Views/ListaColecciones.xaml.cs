using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Museoapp.Views
{
    /// <summary>
    /// Lógica de interacción para ListaColecciones.xaml
    /// </summary>
    public partial class ListaColecciones : Window
    {
        public ListaColecciones()
        {
            InitializeComponent();
           
            string connectionString = "server=DESKTOP-TI2N3QM; database=Museo1 ; integrated security = true"; 
            string query = "SELECT[Coleccion_id],[Nombre],[Cantidad],[Titulo_alias],[Lugar_proce]" +
                ",[Periodo],[Material_id],[Alto],[Ancho],[Largo],[Diametro],[Integridad]," +
                "[Conservacion],[Ubicacion],[Ingreso],[Localidad_id],[Autor_id],[Foto] " +
                "FROM[dbo].[Coleccion]"; 

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Crear un DataTable para almacenar los datos
                DataTable table = new DataTable();
                table.Load(reader);

                // Convertir la URL de la foto en una imagen en el DataTable
                ConvertUrlsToImages(table);


                // Asignar el DataTable como origen de datos de la ListView
                listView.ItemsSource = table.DefaultView;
            }

        }
        private void ConvertUrlsToImages(DataTable table)
        {
            DataColumn fotoColumn = table.Columns["Foto"];
            foreach (DataRow row in table.Rows)
            {
                string url = row.Field<string>(fotoColumn);
                if (!string.IsNullOrEmpty(url))
                {
                    try
                    {
                        BitmapImage image = new BitmapImage(new Uri(url));
                        row.SetField(fotoColumn, image);
                    }
                    catch (Exception)
                    {
                        // Si ocurre algún error al cargar la imagen, puedes asignar una imagen de "imagen no disponible" o null
                    }
                }
            }
        }
    }
}
  