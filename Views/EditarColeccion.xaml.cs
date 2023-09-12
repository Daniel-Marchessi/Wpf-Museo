using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
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

namespace Museo.Views
{
    /// <summary>
    /// Lógica de interacción para EditarColeccion.xaml
    /// </summary>
    public partial class EditarColeccion : Window
    {

        public EditarColeccion(string nombre, int cantidad, string periodo, int alto, int ancho, double diametro, string url,
        int largo, string ingreso, string conservacion, string ubicacion, string integridad, string cultura, string titulo, int id)
        {
            InitializeComponent();
            Nombre.Text = nombre;
            Cantidad.Text = cantidad.ToString();
            Periodo.Text = periodo;
            Alto.Text = alto.ToString();
            Ancho.Text = ancho.ToString();
            Diametro.Text = diametro.ToString();
            Urlfoto.Text = url;
            Largo.Text = largo.ToString();
            Ingreso.Text = ingreso;
            Conservacion.Text = conservacion;
            Ubicacion.Text = ubicacion;
            Integridad.Text = integridad;
            Cultura.Text = cultura;
            Titulo_alias.Text = titulo;
            id_coleccion.Text = id.ToString();

        }
        private void EditImagen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.gif;*.bmp|Todos los archivos|*.*";
            ofd.FilterIndex = 1;
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == true)
            {
                Urlfoto.Text = "";
                try
                {
                    BitmapImage foto = new BitmapImage();
                    foto.BeginInit();
                    foto.UriSource = new Uri(ofd.FileName);
                    foto.EndInit();
                    foto.Freeze();
                    //imgFoto.Source = foto;
                    Urlfoto.Text = ofd.FileName;
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Error al cargar la imagen:" + ex.Message, "Error");
                }
            }
        }
        private void Editar_Coleccion(object sender, RoutedEventArgs e)
        {
            // Obtener los valores editados de los TextBox
            TextInfo Mayuscula = CultureInfo.CurrentCulture.TextInfo;
            string nuevoNombre = Mayuscula.ToTitleCase(Nombre.Text.ToLower());
            int nuevaCantidad = Convert.ToInt32(Cantidad.Text);
            string nuevoPeriodo = Mayuscula.ToTitleCase(Periodo.Text.ToLower());
            int nuevoAlto = Convert.ToInt32(Alto.Text);
            int nuevoAncho = Convert.ToInt32(Ancho.Text);
            double nuevoDiametro = Convert.ToDouble(Diametro.Text);
            string nuevourl = Mayuscula.ToTitleCase(Urlfoto.Text.ToLower());
            int nuevoLargo = Convert.ToInt32(Largo.Text);
            string nuevoIngreso = Mayuscula.ToTitleCase(Ingreso.Text.ToLower());

            string nuevoConservacion = Mayuscula.ToTitleCase(Conservacion.Text.ToLower());
            string nuevoUbicacion = Mayuscula.ToTitleCase(Ubicacion.Text.ToLower());
            string nuevoIntegridad = Mayuscula.ToTitleCase(Integridad.Text.ToLower());
            string nuevoCultura = Mayuscula.ToTitleCase(Cultura.Text.ToLower());
            string nuevoTitulo = Mayuscula.ToTitleCase(Titulo_alias.Text.ToLower());
            int idColeccion = Convert.ToInt32(id_coleccion.Text.Trim());

            // Realizar las operaciones de actualización en la base de datos
            string connectionString = "server=DESKTOP-TI2N3QM; database=Museo1; integrated security = true";
            string queryUpdate = "UPDATE Coleccion SET Nombre = @Nombre, Cantidad = @Cantidad, Periodo = @Periodo," +
                " Alto = @Alto,  Ancho = @Ancho,  Diametro = @Diametro, Url = @Url, Largo = @Largo, Ingreso = @Ingreso," +
                "Conservacion = @Conservacion, Ubicacion = @Ubicacion, Integridad = @Integridad, Lugar = @Lugar, Titulo = @Titulo " +
                "WHERE Coleccion_id = @idColeccion";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand comand = new SqlCommand(queryUpdate, connection);
                comand.Parameters.AddWithValue("@Nombre", nuevoNombre);
                comand.Parameters.AddWithValue("@Cantidad", nuevaCantidad);
                comand.Parameters.AddWithValue("@Periodo", nuevoPeriodo);
                comand.Parameters.AddWithValue("@Alto", nuevoAlto);
                comand.Parameters.AddWithValue("@Ancho", nuevoAncho);
                comand.Parameters.AddWithValue("@Diametro", nuevoDiametro);
                comand.Parameters.AddWithValue("@Url", nuevourl);
                comand.Parameters.AddWithValue("@Largo", nuevoLargo);
                comand.Parameters.AddWithValue("@Ingreso", nuevoIngreso);
                comand.Parameters.AddWithValue("@Conservacion", nuevoConservacion);
                comand.Parameters.AddWithValue("@Ubicacion", nuevoUbicacion);
                comand.Parameters.AddWithValue("@Integridad", nuevoIntegridad);
                comand.Parameters.AddWithValue("@Lugar", nuevoCultura);
                comand.Parameters.AddWithValue("@Titulo", nuevoTitulo);
                comand.Parameters.AddWithValue("@idColeccion", idColeccion);

                connection.Open();
                comand.ExecuteNonQuery();
            }
            // Mostrar un mensaje o realizar cualquier otra acción después de guardar los cambios //EVITAR AMBIGUEDAD SYSTEM.WINDOWS
            System.Windows.MessageBox.Show("Los cambios se han guardado correctamente.");
            // Cerrar la ventana de edición
            this.Close();
        }
    }
}
