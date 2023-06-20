using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows;
using WpfAppTEST.Models;

namespace Museoapp.Views
{
    /// <summary>
    /// Lógica de interacción para Libro.xaml
    /// </summary>
    public partial class Libro : Window
    {
        public Libro()
        {
            InitializeComponent();
            Titulo1.Focus();
        }

        private void EnviarLibro_Click(object sender, RoutedEventArgs e)
        {


            var conexion = new SqlConnection("server=DESKTOP-TI2N3QM; database=Museo1 ; integrated security = true");
            conexion.Open();


            string query = "INSERT INTO Libros (Titulo, Origen, N_paginas, Descripcion) " +
               "VALUES (@Titulo, @Origen, @N_paginas, @Descripcion)";

            Libros librin = new Libros();

            SqlCommand comand = new SqlCommand(query, conexion);

            TextInfo Mayuscula = CultureInfo.CurrentCulture.TextInfo;
            librin.Titulo = Mayuscula.ToTitleCase(Titulo1.Text.ToLower());
            librin.Origen = Mayuscula.ToTitleCase(Origen1.Text.ToLower());
            librin.N_paginas = Convert.ToInt32(Numpaginas1.Text);
            librin.Descripcion = Mayuscula.ToTitleCase(Descripcion1.Text.ToLower());

            comand.Parameters.AddWithValue("@Titulo", librin.Titulo);
            comand.Parameters.AddWithValue("@Origen", librin.Origen);
            comand.Parameters.AddWithValue("@N_paginas", librin.N_paginas);
            comand.Parameters.AddWithValue("@Descripcion", librin.Descripcion);


            comand.ExecuteNonQuery();
            MessageBox.Show("Se ingreso un Libro");
            conexion.Close();
            LimpiarCampos();





        }

        private void LimpiarCampos()
        {
            Titulo1.Text = "";
            Origen1.Text = "";
            Numpaginas1.Text = "";
            Descripcion1.Text = "";
        }

    }
}
