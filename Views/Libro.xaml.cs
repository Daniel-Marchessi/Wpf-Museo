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
            

            var conexion = new SqlConnection("server=DESKTOP-TI2N3QM ; database=Museo1 ; integrated security = true");
            conexion.Open();
            

            string query = "insert into Libros(Titulo, Origen, N_paginas, Descripcion) " +
                "Values ('" + Titulo1.Text + "', '" + Origen1.Text + "', '" + Numpaginas1.Text + "', '" + Descripcion1.Text + "')";


            SqlCommand comand = new SqlCommand(query, conexion);
         
            comand.ExecuteNonQuery();
            MessageBox.Show("Se ingreso un libro");
            conexion.Close();

            //Limpiar campos al ingresar
            Titulo1.Text = "";
            Origen1.Text = "";
            Numpaginas1.Text = "";
            Descripcion1.Text = "";

        }
    }
}
