using Microsoft.VisualBasic;
using Microsoft.Win32;
using Museo.Views;
using Museoapp.Models;
using Museoapp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WpfAppTEST.Models;
using Xceed.Wpf.Toolkit.Primitives;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace WpfAppTEST.Views
{

    public partial class Coleccion : Window
    {
        private List<int> selectedIds;
        public Coleccion()
        {
            InitializeComponent();
            Nombre.Focus();
            TraerAutores();
            TraerMateriales();
            selectedIds = new List<int>();
        }

        //MANEJO DE VENTANAS
        private void CrearArchivo_Click(object sender, RoutedEventArgs e)
        {
            Archivo archivo = new Archivo();
            archivo.Show();
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

        private void CrearListaColeccion_Click(object sender, RoutedEventArgs e)
        {
            ListaColecciones listaColeccion = new ListaColecciones();
            listaColeccion.Show();
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





        public void TraerAutores()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
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

                    Autores.Items.Add(autor);
                }
                Autores.DisplayMemberPath = "NombreCompleto";
                Autores.SelectedValue = "id_autor";
            }

        }

        private void TraerMateriales()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                string query = "SELECT [Nombre], [id_material] FROM [dbo].[Material]";
                SqlCommand comand = new SqlCommand(query, conexion);

                SqlDataReader reader = comand.ExecuteReader();

                while (reader.Read())
                {
                    var material = new Materiales
                    {
                        id_material = Convert.ToInt32(reader["id_material"]),
                        Nombre = reader["Nombre"].ToString()
                    };

                    Materiales.Items.Add(material);
                }
                Materiales.DisplayMemberPath = "Nombre";
                Materiales.SelectedValue = "id_material";

            }

        }

        private void ComboBox_SelectionChanged(object sender, ItemSelectionChangedEventArgs e)
        {
            selectedIds.Clear();
            foreach (var selectedItem in Materiales.SelectedItems)
            {
                var material = selectedItem as Materiales;
                if (material != null)
                {
                    selectedIds.Add(material.id_material);
                }
            }

        }
        private void EnviarColeccion_Click_1(object sender, RoutedEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                string queryColeccion = "INSERT INTO Coleccion (Nombre, Cantidad, Periodo, Alto, Ancho, Diametro, Url, Largo, Ingreso, Conservacion, Ubicacion, Integridad, Cultura, Titulo) VALUES (@Nombre, @Cantidad, @Periodo, @Alto, @Ancho, @Diametro, @Url, @Largo, @Ingreso, @Conservacion, @Ubicacion, @Integridad, @Cultura, @Titulo); SELECT SCOPE_IDENTITY();";
                string queryRelacionColeccion_Material = "INSERT INTO Coleccion_Material (id_material, id_coleccion) VALUES (@IdMaterial, @IdColeccion)";
                string queryRelacionColeccion_Autor = "INSERT INTO Coleccion_Autor (id_autor, id_coleccion) VALUES (@IdAutor, @IdColeccion)";

                SqlCommand comand = new SqlCommand(queryColeccion, conexion);
                SqlCommand comand2 = new SqlCommand(queryRelacionColeccion_Material, conexion);
                SqlCommand comand3 = new SqlCommand(queryRelacionColeccion_Autor, conexion);


                // Obtener los materiales seleccionados en el CheckComboBox
                //Lista de materiales ID
                //List<int> materialesSeleccionados = new List<int>();
                List<string> nombresMateriales = new List<string>();
                List<string> nombresAutores = new List<string>();

                Piezas pieza = new Piezas();


                //obtener una instancia de TextInfo utilizando CultureInfo.CurrentCulture.TextInfo.
                //Utilizar el método ToTitleCase() de la instancia de TextInfo para realizar la conversión.
                TextInfo Mayuscula = CultureInfo.CurrentCulture.TextInfo;
                pieza.Nombre = Mayuscula.ToTitleCase(Nombre.Text.ToLower());
                pieza.Titulo_alias = Mayuscula.ToTitleCase(TituloA.Text.ToLower());
                pieza.Cultura = Mayuscula.ToTitleCase(Lugarprocedencia.Text.ToLower());
                pieza.Periodo = Mayuscula.ToTitleCase(Periodo1.Text.ToLower());
                //pieza.Alto = Convert.ToInt32(Alto1.Text);
                //pieza.Ancho = Convert.ToInt32(Ancho1.Text);
                //pieza.Largo = Convert.ToInt32(Largo1.Text);
                //pieza.Diametro = Convert.ToDouble(Diametro1.Text);
                pieza.Integridad = Integridad1.Text;
                pieza.Conservacion = Conservacion1.Text;
                pieza.Ubicacion = Mayuscula.ToTitleCase(Ubicacion1.Text.ToLower());
                pieza.Ingreso = Ingreso1.Text;
                pieza.UrlFoto = Url_Foto.Text;
                //pieza.Cantidad = Convert.ToInt32(Cantidad1.Text);

                //setear valor 0 cantidad

                int cantid;



                //out cantid es una forma de pasar una variable por referencia. Si la conversión tiene éxito, TryParse() 
                //    asignará el valor convertido a cantid, y la función devolverá true para indicar que la conversión 
                //    fue exitosa.Si la conversión falla, cantid se establecerá en 0 por defecto y la función devolverá
                //    false para indicar que la conversión no fue exitosa.

                if (int.TryParse(Cantidad1.Text, out cantid))
                {
                    pieza.Cantidad = cantid;
                }
                else
                {
                    pieza.Cantidad = 0;
                }

                //setear valor 0 Alto


                if (int.TryParse(Alto1.Text, out cantid))
                {
                    pieza.Alto = cantid;
                }
                else
                {
                    pieza.Alto = 0;
                }

                //setear valor 0 Ancho

                if (int.TryParse(Ancho1.Text, out cantid))
                {
                    pieza.Ancho = cantid;
                }
                else
                {
                    pieza.Ancho = 0;
                }
                //setear valor 0 Diametro

                if (int.TryParse(Diametro1.Text, out cantid))
                {
                    pieza.Diametro = cantid;
                }
                else
                {
                    pieza.Diametro = 0;
                }

                //setear valor 0 Largo

                if (int.TryParse(Largo1.Text, out cantid))
                {
                    pieza.Largo = cantid;
                }
                else
                {
                    pieza.Largo = 0;
                }



                if (string.IsNullOrEmpty(pieza.Nombre))
                {
                   MessageBox.Show("La coleccion debe tener un Nombre");
                   
                }
                else
                {



                    // POR OTRA PARTE SE CONTROLA QUE NO HAYAN REGISTROS IGUALES
                    string registrosrepetidos = "SELECT COUNT(*) FROM Coleccion WHERE Nombre = @Nombre";
                    SqlCommand repetido = new SqlCommand(registrosrepetidos, conexion);
                    repetido.Parameters.AddWithValue("@Nombre", pieza.Nombre);



                    int ContadorDeConsulta = (int)repetido.ExecuteScalar();
                    if (ContadorDeConsulta > 0)
                    {
                        Xceed.Wpf.Toolkit.MessageBox.Show("La Coleccion ya existe");
                        return;
                    }
                    else
                    {
                        Xceed.Wpf.Toolkit.MessageBox.Show("Se ingresó un Coleccion");
                        LimpiarCampos();
                    }

                    // Insertar la pieza en la tabla 'Coleccion' y obtener el ID de la pieza insertada
                    comand.Parameters.AddWithValue("@Nombre", pieza.Nombre);
                    comand.Parameters.AddWithValue("@Cantidad", pieza.Cantidad);
                    comand.Parameters.AddWithValue("@Periodo", pieza.Periodo);
                    comand.Parameters.AddWithValue("@Titulo", pieza.Titulo_alias);
                    comand.Parameters.AddWithValue("@Lugar", pieza.Cultura);
                    comand.Parameters.AddWithValue("@Alto", pieza.Alto);
                    comand.Parameters.AddWithValue("@Ancho", pieza.Ancho);
                    comand.Parameters.AddWithValue("@Largo", pieza.Largo);
                    comand.Parameters.AddWithValue("@Diametro", pieza.Diametro);
                    comand.Parameters.AddWithValue("@Cultura", pieza.Cultura);
                    comand.Parameters.AddWithValue("@Integridad", pieza.Integridad);
                    comand.Parameters.AddWithValue("@Conservacion", pieza.Conservacion);
                    comand.Parameters.AddWithValue("@Ubicacion", pieza.Ubicacion);
                    comand.Parameters.AddWithValue("@Ingreso", pieza.Ingreso);
                    comand.Parameters.AddWithValue("@Url", pieza.UrlFoto);

                    foreach (Materiales item in Materiales.SelectedItems)
                    {
                        string nombreMaterial = item.Nombre;
                        nombresMateriales.Add(nombreMaterial);

                    }
                    foreach (Autores item in Autores.SelectedItems)
                    {
                        string nombreAutor = item.NombreCompleto;
                        nombresAutores.Add(nombreAutor);
                    }

                    int idPieza = Convert.ToInt32(comand.ExecuteScalar()); // Obtener el ID de la pieza insertada

                    foreach (Materiales item in Materiales.SelectedItems)
                    {
                        //materialesSeleccionados.Add(idMaterial);
                        int idMaterial = Convert.ToInt32(item.id_material);

                        // Insertar la relación en la tabla 'Coleccion_Material'
                        // en idColeccion le pones el id que guardamos cuando ingresamos la coleccion
                        comand2.Parameters.Clear();
                        comand2.Parameters.AddWithValue("@IdMaterial", idMaterial);
                        comand2.Parameters.AddWithValue("@IdColeccion", idPieza);
                        comand2.ExecuteNonQuery();

                    }

                    foreach (Autores item in Autores.SelectedItems)
                    {

                        //materialesSeleccionados.Add(idAutor);
                        int idAutores = Convert.ToInt32(item.id_autor);
                        // Insertar la relación en la tabla 'Coleccion_Autor'
                        // en idColeccion le pones el id que guardamos cuando ingresamos la coleccion
                        comand3.Parameters.Clear();
                        comand3.Parameters.AddWithValue("@IdAutor", idAutores);
                        comand3.Parameters.AddWithValue("@IdColeccion", idPieza);
                        comand3.ExecuteNonQuery();

                    }

                    conexion.Close();

                    LimpiarCampos();
                    MessageBox.Show("Se ingresó una pieza correctamente.");
                }
            }
        }

        private void ImagenEdit_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.gif;*.bmp|Todos los archivos|*.*";
            ofd.FilterIndex = 1;
            ofd.Multiselect = false;

            if (ofd.ShowDialog() == true)
            {
                Url_Foto.Text = "";

                try
                {
                    BitmapImage foto = new BitmapImage();
                    foto.BeginInit();
                    foto.UriSource = new Uri(ofd.FileName);
                    foto.EndInit();
                    foto.Freeze();

                    //imgFoto.Source = foto;
                    Url_Foto.Text = ofd.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar la imagen:" + ex.Message, "Error");
                }
            }
        }

        private void LimpiarCampos()
        {
            Nombre.Text = "";
            Cantidad1.Text = "";
            TituloA.Text = "";
            Lugarprocedencia.Text = "";
            Periodo1.Text = "";
            Alto1.Text = "";
            Ancho1.Text = "";
            Diametro1.Text = "";
            Integridad1.Text = "";
            Conservacion1.Text = "";
            Ubicacion1.Text = "";
            Ingreso1.Text = "";
            Url_Foto.Text = "";
            Largo1.Text = "";
            Materiales.Text = "";
            Autores.Text = "";
        }


        //Metodos para controlar el tipo de dato que se puede ingresar por teclado


        //Enteros


        private void Soloenteros_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var Codigo = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }


  

        //Reales
        private void Diametro1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var Codigo = sender as TextBox;
            // Si se ingresó un carácter que no es un número o un punto, o si ya hay un punto en el TextBox, bloquea la entrada
            if (!Regex.IsMatch(e.Text, @"[0-9]") && (e.Text != "." || Diametro1.Text.Contains(".")))
            {
                e.Handled = true;
            }
        }
     


    }

    public partial class EditarColeccion : Window
    {
        public EditarColeccion()
        {
            
        }
    }
}
