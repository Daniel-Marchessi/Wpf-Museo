using Microsoft.Win32;
using Museoapp.Models;
using Museoapp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Media.Imaging;
using WpfAppTEST.Models;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace WpfAppTEST.Views
{

    public partial class Coleccion : Window
    {
        public Coleccion()
        {
            InitializeComponent();
            Nombre.Focus();
            TraerAutores();
            TraerMateriales();

        }

        private void TraerAutores()
        {
            var conexion = new SqlConnection("server=DESKTOP-9MTUTME; database=Museo1 ; integrated security = true");
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
            var conexion = new SqlConnection("server=DESKTOP-9MTUTME; database=Museo1 ; integrated security = true");

            conexion.Open();

            string query = "SELECT [Nombre], [id_material] FROM [dbo].[Material]";
            SqlCommand comand = new SqlCommand(query, conexion);

            SqlDataReader reader = comand.ExecuteReader();

            while (reader.Read())
            {
                var material = new Materiales
                {
                    id_material = Convert.ToInt32(reader["id_material"]),
                    Nombre= reader["Nombre"].ToString()
                };

                Materiales.Items.Add(material);
            }
            Materiales.DisplayMemberPath = "Nombre";
            Materiales.SelectedValue= "id_material";

            conexion.Close();
        }


        private void EnviarColeccion_Click_1(object sender, RoutedEventArgs e)
        {


            var conexion = new SqlConnection("server=DESKTOP-9MTUTME; database=Museo1 ; integrated security = true");
            conexion.Open();


            string query = "insert into Coleccion(Nombre, Cantidad, Titulo_alias, Lugar_proce, Periodo, Alto, Ancho,Largo, Diametro, Integridad, Conservacion, Ubicacion, Ingreso, Material_id, Autor_id, Coleccion_id) " +
               "Values (@Nombre, @Cantidad, @TituloAlias, @LugarProcedencia, @Periodo, @Alto, @Ancho, @Largo, @Diametro, @Integridad, @Conservacion, @Ubicacion, @Ingreso,@Material_id, @Autor_id, @Coleccion_id)";



            string query3 = "insert into Coleccion_autor(id_autor, id_coleccion) Values (@Autor_id, @id_coleccion)";
            string query2 = "insert into Coleccion_Material(id_material, id_coleccion)  Values(@Material_id, @id_coleccion)";


            Piezas pieza = new Piezas();

            SqlCommand comand = new SqlCommand(query, conexion);
            SqlCommand comand2 = new SqlCommand(query2, conexion);
            SqlCommand comand3 = new SqlCommand(query3, conexion);

            int idAutorSelecccionado = Convert.ToInt32(Autores.SelectedValue);

            int idSeleccionado = Convert.ToInt32(Materiales.SelectedValue);

            comand2.Parameters.AddWithValue("@Material_id", idSeleccionado);
            comand2.Parameters.AddWithValue("@id_coleccion", Convert.ToInt32(Codigo.Text));


            comand3.Parameters.AddWithValue("@Autor_id", idAutorSelecccionado);
            comand3.Parameters.AddWithValue("@id_coleccion", Convert.ToInt32(Codigo.Text));

            pieza.id = Convert.ToInt32(Codigo.Text);
            pieza.Nombre = Nombre.Text;
            pieza.Cantidad = Convert.ToInt32(Cantidad1.Text);
            pieza.Titulo_alias = TituloA.Text;
            pieza.Lugar_proce = Lugarprocedencia.Text;
            pieza.Periodo = Periodo1.Text;
            pieza.Alto = Convert.ToInt32(Alto1.Text);
            pieza.Ancho = Convert.ToInt32(Ancho1.Text);
            pieza.Largo = Convert.ToInt32(Largo1.Text);
            pieza.Diametro = Convert.ToDouble(Diametro1.Text);
            pieza.Integridad = Integridad1.Text;
            pieza.Conservacion = Conservacion1.Text;
            pieza.Ubicacion = Ubicacion1.Text;
            pieza.Ingreso = Ingreso1.Text;
            //pieza.UrlFoto = Url_Foto.Text;
            //pieza.Materiales = Materiales.Text;
            pieza.Material_id = idSeleccionado;
            pieza.Autor_id = idAutorSelecccionado;
          
                

            comand.Parameters.AddWithValue("@Nombre", pieza.Nombre);
            comand.Parameters.AddWithValue("@Cantidad", pieza.Cantidad);
            comand.Parameters.AddWithValue("@TituloAlias", pieza.Titulo_alias);
            comand.Parameters.AddWithValue("@LugarProcedencia", pieza.Lugar_proce);
            comand.Parameters.AddWithValue("@Periodo", pieza.Periodo);
            comand.Parameters.AddWithValue("@Alto", pieza.Alto);
            comand.Parameters.AddWithValue("@Ancho", pieza.Ancho);
            comand.Parameters.AddWithValue("@Largo", pieza.Largo);
            comand.Parameters.AddWithValue("@Diametro", pieza.Diametro);
            comand.Parameters.AddWithValue("@Integridad", pieza.Integridad);
            comand.Parameters.AddWithValue("@Conservacion", pieza.Conservacion);
            comand.Parameters.AddWithValue("@Ubicacion", pieza.Ubicacion);
            comand.Parameters.AddWithValue("@Ingreso", pieza.Ingreso);
            //comand.Parameters.AddWithValue("@UrlFoto", pieza.UrlFoto);
            comand.Parameters.AddWithValue("@Material_id", pieza.Material_id);
            comand.Parameters.AddWithValue("@Autor_id", pieza.Autor_id);
            comand.Parameters.AddWithValue("@id", pieza.Autor_id);
            comand.Parameters.AddWithValue("@Coleccion_id", pieza.id);

            comand.ExecuteNonQuery();
            comand3.ExecuteNonQuery();
            comand2.ExecuteNonQuery();
            MessageBox.Show("Se ingreso una pieza");
            conexion.Close();

            LimpiarCampos();
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

                    imgFoto.Source = foto;
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
            imgFoto.Source = null;
        }
    }
}
