using Microsoft.Win32;
using Museoapp.Models;
using Museoapp.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            getAutor();
            getMateriales();

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



        public void getAutor()

        {
            var conexion = new SqlConnection("server=DESKTOP-TI2N3QM; database=Museo1 ; integrated security = true");
            conexion.Open();

            string query = "SELECT [Nombre],[id_autor], [Apellido] FROM [dbo].[Autor]";



            SqlCommand comand = new SqlCommand(query, conexion);





            SqlDataReader reader = comand.ExecuteReader();


            while (reader.Read())
            {
                var autor = new Autores
                {
                    id_autor = Convert.ToInt32(reader["id_autor"]),

                    NombreCompleto = reader["Nombre"].ToString() + " " + reader["Apellido"].ToString(),


                };
                MessageBox.Show(autor.id_autor.ToString());

                Autores12.Items.Add(autor);
            }

            Autores12.DisplayMemberPath = "NombreCompleto";
            Autores12.SelectedValue = "id_autor";

            //int idSeleccionado = Convert.ToInt32(Materiales.SelectedValue);

            //int idAutorSelecccionado = Convert.ToInt32(Autores.SelectedValue);
            //int idAutorSelec = (int)Autores.SelectedValue;
            conexion.Close();
        }

        public void getMateriales()
        {

            var conexion = new SqlConnection("server=DESKTOP-TI2N3QM; database=Museo1 ; integrated security = true");
            conexion.Open();

            string query = "SELECT [Nombre], [id_material] FROM [dbo].[Material]";


            SqlCommand comand = new SqlCommand(query, conexion);





            SqlDataReader reader = comand.ExecuteReader();


            while (reader.Read())
            {
                var material = new Materiales
                {
                    id_material = Convert.ToInt32(reader["id_material"]),

                    Nombre = reader["Nombre"].ToString(),

                };
                MessageBox.Show(material.id_material.ToString());
                MessageBox.Show(material.Nombre.ToString());

                Materiales12.Items.Add(material);
            }

            Materiales12.DisplayMemberPath = "Nombre";
            Materiales12.SelectedValue = "id_autor";

            //int idSeleccionado = Convert.ToInt32(Materiales.SelectedValue);

            //int idAutorSelecccionado = Convert.ToInt32(Autores.SelectedValue);
            //int idAutorSelec = (int)Autores.SelectedValue;
            conexion.Close();



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

            string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
            string queryUpdate = "UPDATE Coleccion SET Nombre = @Nombre, Cantidad = @Cantidad, Periodo = @Periodo," +
                " Alto = @Alto,  Ancho = @Ancho,  Diametro = @Diametro, Url = @Url, Largo = @Largo, Ingreso = @Ingreso," +
                "Conservacion = @Conservacion, Ubicacion = @Ubicacion, Integridad = @Integridad, Cultura = @Cultura, Titulo = @Titulo " +
                "WHERE Coleccion_id = @idColeccion";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                //query para tabla intermedia
                string queryAutor = "UPDATE Coleccion_Autor SET id_autor = @IdAutor WHERE id_coleccion = @idColeccion";

                //Tabla intemedia de materiales 
                string queryMaterial = "UPDATE Coleccion_Material SET id_material = @id_Material WHERE id_coleccion = @idColeccion";

                SqlCommand commandMaterial = new SqlCommand(queryMaterial, connection);

                SqlCommand comandAutor = new SqlCommand(queryAutor, connection);

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
                comand.Parameters.AddWithValue("@Cultura", nuevoCultura);
                comand.Parameters.AddWithValue("@Titulo", nuevoTitulo);
                comand.Parameters.AddWithValue("@idColeccion", idColeccion);

                connection.Open();
                comand.ExecuteNonQuery();




                foreach (Autores item in Autores12.SelectedItems)
                {

                    int idAutores = Convert.ToInt32(item.id_autor);

                    //MessageBox.Show(idAutores.ToString());
                    comandAutor.Parameters.AddWithValue("@IdAutor", idAutores);
                    comandAutor.Parameters.AddWithValue("@idColeccion", idColeccion);
                    comandAutor.ExecuteNonQuery();

                }

                foreach (Materiales item in Materiales12.SelectedItems)
                {

                    int id_mateteriales = Convert.ToInt32(item.id_material);

                    //MessageBox.Show(id_mateteriales.ToString());
                    commandMaterial.Parameters.AddWithValue("@id_Material", id_mateteriales);
                    commandMaterial.Parameters.AddWithValue("@idColeccion", idColeccion);
                    commandMaterial.ExecuteNonQuery();

                }



            }
            // Mostrar un mensaje o realizar cualquier otra acción después de guardar los cambios //EVITAR AMBIGUEDAD SYSTEM.WINDOWS
            System.Windows.MessageBox.Show("Los cambios se han guardado correctamente.");
           
            // Cerrar la ventana de edición
            this.Close();
        }
    }
}
