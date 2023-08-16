using Museo.Models;
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
using static System.Net.Mime.MediaTypeNames;

namespace Museo.Views
{
    /// <summary>
    /// Lógica de interacción para EditarLibro.xaml
    /// </summary>
    public partial class EditarLibro : Window
    {
        public EditarLibro(int idlibro, string Titulo, string Origen, int N_paginas, string Descripcion, string Edicion, string AnioEdicion, string Autores, int codigo)
        {
            InitializeComponent();
            //id_libro.Text = idlibro.ToString();
            //Carpeta.Text = carpeta;



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Obtener los valores editados de los TextBox
            //TextInfo Mayuscula = CultureInfo.CurrentCulture.TextInfo;
            //string nuevaCarpeta = Mayuscula.ToTitleCase(.Text.ToLower());
            //string nuevaCategoria = Mayuscula.ToTitleCase(Categoria.Text.ToLower());
            //int nuevoCodigo = Convert.ToInt32(Codigo.Text);
            //string nuevoTitulo = Mayuscula.ToTitleCase(Titulo.Text.ToLower());
            //int idArchivo = Convert.ToInt32(id_archivo.Text.Trim());
            //// Realizar las operaciones de actualización en la base de datos
            //string connectionString = "server=DESKTOP-TI2N3QM; database=Museo1; integrated security = true";
            //string queryUpdate = "UPDATE Archivos SET Carpeta = @Carpeta, Categoria = @Categoria, Titulo = @Titulo, Codigo = @Codigo WHERE id_archivo = @idArchivo";

            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    SqlCommand commandUpdate = new SqlCommand(queryUpdate, connection);
            //    commandUpdate.Parameters.AddWithValue("@Carpeta", nuevaCarpeta);
            //    commandUpdate.Parameters.AddWithValue("@Categoria", nuevaCategoria);
            //    commandUpdate.Parameters.AddWithValue("@Titulo", nuevoTitulo);
            //    commandUpdate.Parameters.AddWithValue("@Codigo", nuevoCodigo);
            //    commandUpdate.Parameters.AddWithValue("@idArchivo", idArchivo);

            //    connection.Open();
            //    commandUpdate.ExecuteNonQuery();
            //}

            ////     Mostrar un mensaje o realizar cualquier otra acción después de guardar los cambios //EVITAR AMBIGUEDAD SYSTEM.WINDOWS
            //System.Windows.MessageBox.Show("Los cambios se han guardado correctamente.");
            //// Cerrar la ventana de edición
            //this.Close();
        }


    }
}
