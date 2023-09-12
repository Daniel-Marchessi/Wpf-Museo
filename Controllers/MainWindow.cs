using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAppTEST.Views;

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
            string query = "Select Usuario, Password from Usuario";
            SqlCommand comand = new SqlCommand(query, conexion);
            SqlDataReader registro = comand.ExecuteReader();
            bool usuarioValido = false;
            bool contraseniaValida = false;

            while (registro.Read())
            {
                string dbUsuario = registro.GetString(0);
                string dbContrasenia = registro.GetString(1);

                if (dbUsuario == usuario)
                {
                    usuarioValido = true;
                }

                if (dbContrasenia == contrasenia)
                {
                    contraseniaValida = true;
                }

        
                if (usuarioValido && contraseniaValida)
                {
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
