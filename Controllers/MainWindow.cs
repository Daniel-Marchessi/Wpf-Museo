using Museo.Models;
using Museo.Views;

using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAppTEST.Models;
using WpfAppTEST.Views;
using Museo.Controllers;
using System;

namespace WpfAppTEST
{
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();
            user.Focus();
        }


      
        private void Btn_enviar_Click(object sender, RoutedEventArgs e)
        {
            string usuario = user.Text.ToString();
            string contrasenia = contra.Password.ToString();
            var conexion = new SqlConnection("server=DESKTOP-TI2N3QM; database=Museo1 ; integrated security = true");
            conexion.Open();
            string query = "SELECT Nombre, Password, Rol, Usuario_id FROM Usuario";
            SqlCommand comand = new SqlCommand(query, conexion);
            SqlDataReader registro = comand.ExecuteReader();
            bool usuarioValido = false;
            bool contraseniaValida = false;

            while (registro.Read())
            {
                Usuarios user = new Usuarios();
                user.Usuario = registro.GetString(0);
                user.Contrasenia = registro.GetString(1);
               

                if (user.Usuario == usuario)
                {
                    usuarioValido = true;
                }

                if (user.Contrasenia == contrasenia)
                {
                    contraseniaValida = true;
                }

        
                if (usuarioValido && contraseniaValida)
                {
                    Usuarios.RolUsuario= registro.GetString(2);
                    Usuarios.Usuario_id = registro.GetInt32(3);
                    Home inicio = new Home();
                    inicio.Show();
                    this.Close();
                    break; 
                }
            }

            if (!usuarioValido && !contraseniaValida)
            {
                MessageBox.Show("El usuario y la contraseña son incorrectos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!usuarioValido)
            {
                MessageBox.Show("El usuario es incorrecto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!contraseniaValida)
            {
                MessageBox.Show("La contraseña es incorrecta", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            conexion.Close();
        }

    }
}
