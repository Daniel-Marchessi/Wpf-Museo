﻿using Museo.Views;
using Museoapp.Models;
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
using WpfAppTEST.Models;
using WpfAppTEST.Views;

namespace Museoapp.Views
{
    /// <summary>
    /// Lógica de interacción para Autor.xaml
    /// </summary>
    public partial class Autor : Window
    {
        public Autor()
        {
            InitializeComponent();

            ListarAutores();

        }

        private void CrearArchivo_Click(object sender, RoutedEventArgs e)
        {
            Archivo archivo = new Archivo();
            archivo.Show();
            this.Close();

        }
        private void CrearColeccion_Click(object sender, RoutedEventArgs e)
        {
            Coleccion coleccion = new Coleccion();

            coleccion.Show();
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


        private void CrearMaterial_Click(object sender, RoutedEventArgs e)
        {
            Material material = new Material();
            material.Show();
            this.Close();

        }
        private void ListarAutores()
        {
            string connectionString = "server=DESKTOP-TI2N3QM; database=Museo1 ; integrated security = true";
            string query = "SELECT [Nombre], [Apellido]  FROM [dbo].[Autor]";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                List<Autores> autores = new List<Autores>();

                while (reader.Read())
                {
                    Autores autorin = new Autores();
                    autorin.Nombre = reader.GetString(0);
                    autorin.Apellido = reader.GetString(1);
                    autores.Add(autorin);
                }

                listView.ItemsSource = autores;
            }
        }

        private void EnviarAutor_Click(object sender, RoutedEventArgs e)
        {
            var conexion = new SqlConnection("server=DESKTOP-TI2N3QM; database=Museo1 ; integrated security = true");
            conexion.Open();

            string query = "SELECT COUNT(*) FROM Autor WHERE Nombre = @Nombre AND Apellido = @Apellido";
            SqlCommand checkCommand = new SqlCommand(query, conexion);
            checkCommand.Parameters.AddWithValue("@Nombre", Nombre1.Text);
            checkCommand.Parameters.AddWithValue("@Apellido", Apellido1.Text);

            int existingRecordsCount = (int)checkCommand.ExecuteScalar();

            if (existingRecordsCount > 0)
            {
                MessageBox.Show("El autor ya existe");
                conexion.Close();
                return;
            }

            string insertQuery = "INSERT INTO Autor (Nombre, Apellido) VALUES (@Nombre, @Apellido)";
            SqlCommand insertCommand = new SqlCommand(insertQuery, conexion);
            insertCommand.Parameters.AddWithValue("@Nombre", Nombre1.Text);
            insertCommand.Parameters.AddWithValue("@Apellido", Apellido1.Text);
            insertCommand.ExecuteNonQuery();

            MessageBox.Show("Se ingresó un Autor");
            conexion.Close();

            LimpiarCampos();
            ListarAutores();
        }
        private void BuscarPorNombre(object sender, RoutedEventArgs e)
        {
            string textoBusqueda = PorNombre.Text.Trim();

            for (int i = 0; i < listView.Items.Count; i++)
            {
                Materiales item = (Materiales)listView.Items[i];
                ListViewItem listViewItem = listView.ItemContainerGenerator.ContainerFromIndex(i) as ListViewItem;

                if (item.Nombre.Contains(textoBusqueda))
                {
                    // Mostrar el elemento si coincide con la búsqueda
                    listViewItem.Visibility = Visibility.Visible;
                }
                else
                {
                    // Ocultar el elemento si no coincide con la búsqueda
                    listViewItem.Visibility = Visibility.Collapsed;
                }
            }

            PorNombre.Text = "";
        }
        private void Refrescar(Object sender, RoutedEventArgs e)
        {
            // Mostrar todos los elementos de la lista nuevamente
            for (int i = 0; i < listView.Items.Count; i++)
            {
                ListViewItem listViewItem = listView.ItemContainerGenerator.ContainerFromIndex(i) as ListViewItem;
                if (listViewItem != null)
                {
                    // Mostrar el elemento
                    listViewItem.Visibility = Visibility.Visible;
                }
            }

            // Limpiar el campo de búsqueda
            PorNombre.Text = "";
        }
        private void LimpiarCampos()
        {
            Nombre1.Text = "";
            Apellido1.Text = "";
        }


    }
}
