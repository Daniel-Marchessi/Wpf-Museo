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
using System.Text.RegularExpressions;
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

        public EditarColeccion(string nombre, int cantidad, string periodo, int alto, int ancho, decimal diametro, string url,
        int largo, string ingreso, string conservacion, string ubicacion, string integridad, string cultura, string titulo, int id)
        {

            CultureInfo culture = new CultureInfo("en-US");
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
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
            //int nuevaCantidad = Convert.ToInt32(Cantidad.Text);
            string nuevoPeriodo = Mayuscula.ToTitleCase(Periodo.Text.ToLower());
            //int nuevoAlto = Convert.ToInt32(Alto.Text);
            //int nuevoAncho = Convert.ToInt32(Ancho.Text);
            decimal nuevoDiametro;
            int nuevaCantidad;
            int nuevoAlto;
            int nuevoAncho;
            int nuevoLargo;
            //double nuevoDiametro = Convert.(Diametro.Text);

            string nuevourl = Mayuscula.ToTitleCase(Urlfoto.Text.ToLower());
            //int nuevoLargo = Convert.ToInt32(Largo.Text);
            string nuevoIngreso = Mayuscula.ToTitleCase(Ingreso.Text.ToLower());
            string nuevoConservacion = Mayuscula.ToTitleCase(Conservacion.Text.ToLower());
            string nuevoUbicacion = Mayuscula.ToTitleCase(Ubicacion.Text.ToLower());
            string nuevoIntegridad = Mayuscula.ToTitleCase(Integridad.Text.ToLower());
            string nuevoCultura = Mayuscula.ToTitleCase(Cultura.Text.ToLower());
            string nuevoTitulo = Mayuscula.ToTitleCase(Titulo_alias.Text.ToLower());
            int idColeccion = Convert.ToInt32(id_coleccion.Text.Trim());

            int cantid;

            //out cantid es una forma de pasar una variable por referencia. Si la conversión tiene éxito, TryParse() 
            //    asignará el valor convertido a cantid, y la función devolverá true para indicar que la conversión 
            //    fue exitosa.Si la conversión falla, cantid se establecerá en 0 por defecto y la función devolverá
            //    false para indicar que la conversión no fue exitosa.

            if (int.TryParse(Cantidad.Text, out cantid))
            {
                nuevaCantidad = cantid;
            }
            else
            {
                nuevaCantidad = 0;
            }

            //setear valor 0 Alto


            if (int.TryParse(Alto.Text, out cantid))
            {
                nuevoAlto = cantid;
            }
            else
            {
                nuevoAlto = 0;
            }
            //setear valor 0 Largo

            if (int.TryParse(Largo.Text, out cantid))
            {
                nuevoLargo = cantid;
            }
            else
            {
                nuevoLargo = 0;
            }

            //setear valor 0 Ancho

            if (int.TryParse(Ancho.Text, out cantid))
            {
                nuevoAncho = cantid;
            }
            else
            {
                nuevoAncho = 0;
            }
            //setear valor 0 Diametro
            decimal valorTemporal;

            if (decimal.TryParse(Diametro.Text, NumberStyles.Float, CultureInfo.CurrentCulture, out valorTemporal))
            {
                nuevoDiametro = valorTemporal;
            }
            else
            {
                nuevoDiametro = 0; // Otra acción adecuada si la conversión falla
            }


            if (string.IsNullOrEmpty(nuevoNombre))
            {
                MessageBox.Show("La colección debe tener un Nombre");
            }
            else
            {
                string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
                string queryUpdate = "UPDATE Coleccion SET Nombre = @Nombre, Cantidad = @Cantidad, Periodo = @Periodo," +
                                     " Alto = @Alto,  Ancho = @Ancho,  Diametro = @Diametro, Url = @Url, Largo = @Largo, Ingreso = @Ingreso," +
                                     "Conservacion = @Conservacion, Ubicacion = @Ubicacion, Integridad = @Integridad, Cultura = @Cultura, Titulo = @Titulo " +
                                     "WHERE Coleccion_id = @idColeccion";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    //Borrar las relaciones que hay de las tablas intermedias
                    string deleteAuthorsQuery = "DELETE FROM Coleccion_Autor WHERE id_coleccion = @idColeccion";
                    SqlCommand deleteAuthorsCommand = new SqlCommand(deleteAuthorsQuery, connection);
                    deleteAuthorsCommand.Parameters.AddWithValue("@idColeccion", idColeccion);
                    deleteAuthorsCommand.ExecuteNonQuery();

                    string deleteMaterialsQuery = "DELETE FROM Coleccion_Material WHERE id_coleccion = @idColeccion";
                    SqlCommand deleteMaterialsCommand = new SqlCommand(deleteMaterialsQuery, connection);
                    deleteMaterialsCommand.Parameters.AddWithValue("@idColeccion", idColeccion);
                    deleteMaterialsCommand.ExecuteNonQuery();

                    //Crear las nuevas relaciones

                    foreach (Autores item in Autores12.SelectedItems)
                    {
                        int idAutores = Convert.ToInt32(item.id_autor);
                        string insertAuthorQuery = "INSERT INTO Coleccion_Autor (id_coleccion, id_autor) VALUES (@idColeccion, @IdAutor)";
                        SqlCommand insertAuthorCommand = new SqlCommand(insertAuthorQuery, connection);
                        insertAuthorCommand.Parameters.AddWithValue("@IdAutor", idAutores);
                        insertAuthorCommand.Parameters.AddWithValue("@idColeccion", idColeccion);
                        insertAuthorCommand.ExecuteNonQuery();
                    }

                    foreach (Materiales item in Materiales12.SelectedItems)
                    {
                        int id_material = Convert.ToInt32(item.id_material);
                        string insertMaterialQuery = "INSERT INTO Coleccion_Material (id_coleccion, id_material) VALUES (@idColeccion, @id_Material)";
                        SqlCommand insertMaterialCommand = new SqlCommand(insertMaterialQuery, connection);
                        insertMaterialCommand.Parameters.AddWithValue("@id_Material", id_material);
                        insertMaterialCommand.Parameters.AddWithValue("@idColeccion", idColeccion);
                        insertMaterialCommand.ExecuteNonQuery();
                    }




                    //Por ultimo updatear la tabla principal
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

                    comand.ExecuteNonQuery();
                }

                System.Windows.MessageBox.Show("Los cambios se han guardado correctamente.");

                this.Close();
            }
        }
        //Reales
        private void Diametro1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var Codigo = sender as TextBox;
            // Si se ingresó un carácter que no es un número o un punto, o si ya hay un punto en el TextBox, bloquea la entrada
            if (!Regex.IsMatch(e.Text, @"[0-9]") && (e.Text != "." || Diametro.Text.Contains(".")))
            {
                e.Handled = true;
            }
        }
        //Enteros


        private void Soloenteros_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var Codigo = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

    }
}
