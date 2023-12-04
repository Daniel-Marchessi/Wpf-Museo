using Museo.Models;
using Museo.Views;
using Museoapp.Models;
using Museoapp.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
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

namespace WpfAppTEST.Views
{
    /// <summary>
    /// Lógica de interacción para Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        private DateTime horaEntrada;
        public Home()
        {
        InitializeComponent();
            Loaded += Home_Loaded;
            Closing += Home_Closing;
            horaEntrada = ObtenerHoraEntrada();
        }

        //Controlar menuitem historias de acceso
        private void VerificarRolDeUsuario()
        {
            if (Usuarios.RolUsuario != "Admin")
            {
                Historia.Visibility = Visibility.Collapsed;
            }
        }

        private void MenuItem_Loaded(object sender, RoutedEventArgs e)
        {
            // Cuando el MenuItem se carga, verifica el rol del usuario y actúa en consecuencia
            VerificarRolDeUsuario();
        }

   

        //metodos de Horas
        private void Home_Loaded(object sender, RoutedEventArgs e)
        {
            // Captura la hora de entrada cuando se carga la ventana
            horaEntrada = ObtenerHoraEntrada();
        }

        private void Home_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Captura la hora de salida cuando se cierra la ventana
            DateTime horaSalida = ObtenerHoraSalida();
            // paso al metodo id horaentrada y horasalida
            int usuarioId = Usuarios.Usuario_id; 
            RegistrarHoraEntradaYSalida(usuarioId, horaEntrada, horaSalida);
        }

        private DateTime ObtenerHoraEntrada()
        {
            return DateTime.Now;
        }

        private DateTime ObtenerHoraSalida()
        {  
            return DateTime.Now;
        }
        
        //Menuitem SALIDA
        private void Salir_Click(object sender, RoutedEventArgs e)
        {
            DateTime horaSalida = ObtenerHoraSalida();
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
            this.Close();
        }


        private void RegistrarHoraEntradaYSalida(int usuarioId, DateTime horaEntrada, DateTime horaSalida)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string insertarQuery = "INSERT INTO RegistrosAcceso (UsuarioId, FechaHoraAcceso, FechaHoraSalida) VALUES (@UsuarioId, @FechaHoraAcceso, @FechaHoraSalida)";
                using (SqlCommand queryRegistrosAcceso= new SqlCommand(insertarQuery, connection))
                {
                    queryRegistrosAcceso.Parameters.AddWithValue("@UsuarioId", usuarioId);
                    queryRegistrosAcceso.Parameters.AddWithValue("@FechaHoraAcceso", horaEntrada);
                    queryRegistrosAcceso.Parameters.AddWithValue("@FechaHoraSalida", horaSalida);
                    queryRegistrosAcceso.ExecuteNonQuery();
                }
            }
        }

        private void HistorialDeUsuarios_Click(object sender, RoutedEventArgs e)
        {
            historialdeusuarios historialdeusuarios = new historialdeusuarios();
            historialdeusuarios.Show();
        }

        private void CrearColeccion_Click(object sender, RoutedEventArgs e)
        {
            Coleccion coleccion = new Coleccion();

            coleccion.Show();
            
        }
        private void CrearLibro_Click(object sender, RoutedEventArgs e)
        {
            Libro libro = new Libro();
            libro.Show();
        }
        private void CrearListaLibro_Click(object sender, RoutedEventArgs e)
        {
            ListaLibros listalibro = new ListaLibros();
            listalibro.Show();
        }
        private void CrearListaColeccion_Click(object sender, RoutedEventArgs e)
        {
            ListaColecciones listaColeccion = new ListaColecciones();
            listaColeccion.Show();
        }
        private void CrearArchivo_Click(object sender, RoutedEventArgs e)
        {
            Archivo archivo = new Archivo();
            archivo.Show();
        }
        private void CrearAutor_Click(object sender, RoutedEventArgs e)
        {
            Autor autor = new Autor();
            autor.Show();
        }
 
        private void CrearMaterial_Click(object sender, RoutedEventArgs e)
        {
            Material material = new Material();
            material.Show();
        }
        private void CrearEditorial_Click(object sender, RoutedEventArgs e)
        {
            Editorial editorial = new Editorial();
            editorial.Show();
        }
        private void CrearCategoria_Click(object sender, RoutedEventArgs e)
        {
            Categoria categoria = new Categoria();
            categoria.Show();
        }

        private void CrearCarpeta_Click(object sender, RoutedEventArgs e)
        {
            Carpeta carpeta = new Carpeta();

            carpeta.Show();

        }




    }
}
