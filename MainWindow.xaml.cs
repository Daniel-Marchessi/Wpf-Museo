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
           string  contrasenia = contra.Password.ToString();
            var conexion  = new SqlConnection("server=DESKTOP-9MTUTME; database=Museo1 ; integrated security = true");
            conexion.Open();


            string query = "Select Usuario, Password from Usuario";

            SqlCommand comand = new SqlCommand(query, conexion);

            SqlDataReader registro = comand.ExecuteReader();
            if (registro.Read())
            {

                //Validacion por usuario y por contraseña
                if (registro.GetString(0) == usuario)
                {
                    if (registro.GetString(1) == contrasenia)
                    {
                        Home inicio = new Home();
                        inicio.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("La contraseña es incorrecta", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("El usuario es incorrecto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }


            }

            conexion.Close();
          
        }
        
    }
}
