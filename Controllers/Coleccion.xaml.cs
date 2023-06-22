using Microsoft.Win32;
using Museoapp.Models;
using Museoapp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Windows;
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

        private void TraerAutores()
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

                Autores.Items.Add(autor);
            }

            Autores.DisplayMemberPath = "NombreCompleto";
            Autores.SelectedValue = "id_autor";

            //int idSeleccionado = Convert.ToInt32(Materiales.SelectedValue);

            //int idAutorSelecccionado = Convert.ToInt32(Autores.SelectedValue);
            //int idAutorSelec = (int)Autores.SelectedValue;
            conexion.Close();
        }

        private void TraerMateriales()
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
                    Nombre = reader["Nombre"].ToString()
                };

                Materiales.Items.Add(material);
            }
            Materiales.DisplayMemberPath = "Nombre";
            Materiales.SelectedValue = "id_material";

            conexion.Close();
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
            var conexion = new SqlConnection("server=DESKTOP-TI2N3QM; database=Museo1; integrated security=true");
            conexion.Open();

            string queryColeccion = "INSERT INTO Coleccion (Nombre, Cantidad, Periodo, Alto, Ancho, Diametro, Url, Largo, Ingreso, Conservacion, Ubicacion, Integridad, Lugar, Titulo, Materiales,Autores) VALUES (@Nombre, @Cantidad, @Periodo, @Alto, @Ancho, @Diametro, @Url, @Largo, @Ingreso, @Conservacion, @Ubicacion, @Integridad, @Lugar, @Titulo, @Materiales,@Autores); SELECT SCOPE_IDENTITY();";
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
            pieza.Lugar_proce = Mayuscula.ToTitleCase(Lugarprocedencia.Text.ToLower());
            pieza.Periodo = Mayuscula.ToTitleCase(Periodo1.Text.ToLower());
            pieza.Alto = Convert.ToInt32(Alto1.Text);
            pieza.Ancho = Convert.ToInt32(Ancho1.Text);
            pieza.Largo = Convert.ToInt32(Largo1.Text);
            pieza.Diametro = Convert.ToDouble(Diametro1.Text);
            pieza.Integridad = Integridad1.Text;
            pieza.Conservacion = Conservacion1.Text;
            pieza.Ubicacion = Mayuscula.ToTitleCase(Ubicacion1.Text.ToLower());
            pieza.Ingreso = Ingreso1.Text;
            pieza.UrlFoto = Url_Foto.Text;

            // Insertar la pieza en la tabla 'Coleccion' y obtener el ID de la pieza insertada
            comand.Parameters.AddWithValue("@Nombre", pieza.Nombre);
            comand.Parameters.AddWithValue("@Cantidad", pieza.Cantidad);
            comand.Parameters.AddWithValue("@Periodo", pieza.Periodo);
            comand.Parameters.AddWithValue("@Titulo", pieza.Titulo_alias);
            comand.Parameters.AddWithValue("@Lugar", pieza.Lugar_proce);
            comand.Parameters.AddWithValue("@Alto", pieza.Alto);
            comand.Parameters.AddWithValue("@Ancho", pieza.Ancho);
            comand.Parameters.AddWithValue("@Largo", pieza.Largo);
            comand.Parameters.AddWithValue("@Diametro", pieza.Diametro);
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
            foreach(Autores item in Autores.SelectedItems)
            {
                string nombreAutor = item.NombreCompleto;
                nombresAutores.Add(nombreAutor);
            }

            // Concatenar los nombres de los materiales seleccionados
            string materialesConcatenados = string.Join(", ", nombresMateriales);
            Mayuscula.ToTitleCase(materialesConcatenados).ToLower();

            // Concatenar los nombres de los autores seleccionados
            string autoresConcatenados = string.Join(", ", nombresAutores);
            Mayuscula.ToTitleCase(materialesConcatenados).ToLower();

            // Agregar el parámetro @Materiales con la lista de nombres de materiales concatenados
            comand.Parameters.AddWithValue("@Materiales", materialesConcatenados);
            comand.Parameters.AddWithValue("@Autores", autoresConcatenados);

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
            //imgFoto.Source = null;
        }
    }
}
