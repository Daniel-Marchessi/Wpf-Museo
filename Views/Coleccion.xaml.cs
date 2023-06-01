using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using WpfAppTEST.Models;

namespace WpfAppTEST.Views
{
    /// <summary>
    /// Lógica de interacción para Coleccion.xaml
    /// </summary>
    public partial class Coleccion : Window
    {
        public Coleccion()
        {
            InitializeComponent();
        }

       

        private void EnviarColeccion_Click_1(object sender, RoutedEventArgs e)
        {

            var conexion = new SqlConnection("server=DESKTOP-TI2N3QM; database=Museo1 ; integrated security = true");
            conexion.Open();



            string query = "insert into Coleccion(Nombre, Cantidad, Titulo_alias, Lugar_proce, Periodo, Alto, Ancho, Diametro, Integridad, Conservacion, Ubicacion, Ingreso, Foto) " +
                "Values ('" + Nombre.Text + "', " + Cantidad1.Text + ", '" + TituloA.Text + "', '" + Lugarprocedencia.Text + "', '" + Periodo1.Text + "', " + Alto1.Text + ", " +
                Ancho1.Text + ", " + Diametro1.Text + ", '" + Integridad1.Text + "', '" + Conservacion1.Text + "', '" +
                Ubicacion1.Text + "', '" + Ingreso1.Text + "', '" + Url_Foto.Text + "')";


            SqlCommand comand = new SqlCommand(query, conexion);
            //comand.Parameters.AddWithValue("@Nombre", Nombre.Text);
            //comand.Parameters.AddWithValue("@Cantidad", Cantidad1.Text);
            //comand.Parameters.AddWithValue("Lugar_proce", Lugarprocedencia.Text);
            //comand.Parameters.AddWithValue("@Periodo", Periodo1.Text);
            //comand.Parameters.AddWithValue("@Alto", Alto1.Text);
            //comand.Parameters.AddWithValue("@Ancho", Ancho1.Text);
            //comand.Parameters.AddWithValue("@Diametro", Diametro1.Text);
            //comand.Parameters.AddWithValue("@Integridad", Integridad1.Text);
            //comand.Parameters.AddWithValue("@Conservacion", Conservacion1.Text);
            //comand.Parameters.AddWithValue("@Ubicacion", Ubicacion1.Text);
            //comand.Parameters.AddWithValue("@Ingreso", Ingreso1.Text);
            comand.ExecuteNonQuery();
            MessageBox.Show("Se ingreso una pieza");
            conexion.Close();

        }

        private void ImagenEdit_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.gif;*.bmp|Todos los archivos|*.*";
            ofd.FilterIndex = 1;
            ofd.Multiselect = false;

            if(ofd.ShowDialog() == true)
            {
                Url_Foto.Text = "";

                try
                {
                    BitmapImage foto = new BitmapImage();
                    foto.BeginInit();
                    foto.UriSource = new Uri(ofd.FileName);
                    foto.EndInit();
                    foto.Freeze();

                    imgFoto.Source = foto;
                    Url_Foto.Text = ofd.FileName;
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error al cargar la imagen:" + ex.Message, "Error");
                }
            }
        }
    }
}
