using Museo.Models;
using Museoapp.Views;
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
using WpfAppTEST.Views;
using WpfAppTEST.Models;
using static System.Net.Mime.MediaTypeNames;
using Museoapp.Models;
using System.Configuration;

namespace Museo.Views
{
    /// <summary>
    /// Lógica de interacción para EditarLibro.xaml
    /// </summary>
    public partial class EditarLibro : Window
    {
        public EditarLibro(int idlibro, string tit, string origen, int n_pag, string desc, string edicion, string anio, string autores, int codigo)
        {
            InitializeComponent();
            id_libro.Text = idlibro.ToString();
            Titulo.Text = tit.ToString();
            Origen.Text = origen.ToString();    
            N_paginas.Text = n_pag.ToString();
            Descripcion.Text = desc.ToString();
            Edicion.Text = edicion.ToString();
            AnioEdicion.Text = anio.ToString();
          //  Autores.Text = autores.ToString();
            Codigo.Text = codigo.ToString();
            getAutor();

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



        private void Enviar(object sender, RoutedEventArgs e)
        {
          
            int id_Libro = Convert.ToInt32(id_libro.Text);
            string titulo = Convert.ToString(Titulo.Text);  
            string origen = Convert.ToString(Origen.Text);
            string desc = Convert.ToString(Descripcion.Text);
            string edicion = Convert.ToString(Edicion.Text);
            string n_pag = Convert.ToString(N_paginas.Text);
            string anio = Convert.ToString(AnioEdicion.Text);
          //  string autores = Convert.ToString(Autores.Text);
            string cod = Convert.ToString(Codigo.Text);

            //// Realizar las operaciones de actualización en la base de datos
            string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
            string queryUpdate = "UPDATE LIBROS SET Titulo = @Titulo, Origen = @Origen, Descripcion = @Descripcion, Edicion = @Edicion, " +
                " N_paginas = @N_paginas, AnioEdicion = @AnioEdicion, Codigo = @Codigo  WHERE id_libro = @id_libro";

            string queryLibros = "UPDATE Libro_Autor SET id_autor = @IdAutor WHERE id_libro = @id_libro";



            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                connection.Open();
                SqlCommand commandUpdate = new SqlCommand(queryUpdate, connection);

                SqlCommand comandLibro = new SqlCommand(queryLibros, connection);

                commandUpdate.Parameters.AddWithValue("@Titulo", titulo);
                commandUpdate.Parameters.AddWithValue("@Origen", origen );
                commandUpdate.Parameters.AddWithValue("@Descripcion",desc );
                commandUpdate.Parameters.AddWithValue("@Edicion", edicion);
               commandUpdate.Parameters.AddWithValue("@N_paginas", n_pag );
               commandUpdate.Parameters.AddWithValue("@AnioEdicion", anio );
               commandUpdate.Parameters.AddWithValue("@Codigo", cod );
               commandUpdate.Parameters.AddWithValue("@id_libro", id_Libro );

                // commandUpdate.Parameters.AddWithValue("@Codigo", cod );

                foreach (Autores item in Autores12.SelectedItems)
                {

                    int idAutores = Convert.ToInt32(item.id_autor);

                    MessageBox.Show(idAutores.ToString());
                    comandLibro.Parameters.AddWithValue("@IdAutor", idAutores);
                    comandLibro.Parameters.AddWithValue("@id_libro", id_Libro);
                    comandLibro.ExecuteNonQuery();

                }


                // SqlCommand autorUpdate = new SqlCommand(queryAutor, connection);

                // autorUpdate.Parameters.AddWithValue()

                commandUpdate.ExecuteNonQuery();
            }
            
            ////     Mostrar un mensaje o realizar cualquier otra acción después de guardar los cambios //EVITAR AMBIGUEDAD SYSTEM.WINDOWS
            //System.Windows.MessageBox.Show("Los cambios se han guardado correctamente.");
            //// Cerrar la ventana de edición
            ///
            MessageBox.Show("Los cambios se han realizado correctamente");
            this.Close();
        }

       
    }
}
