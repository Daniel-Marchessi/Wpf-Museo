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




namespace WpfAppTEST
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_enviar_Click(object sender, RoutedEventArgs e)
        {
            string usuario = user.Text.ToString();
           string  contrasenia = contra.Text.ToString();

            var conexion  = new SqlConnection("server=DESKTOP-9MTUTME ; database=Museo1 ; integrated security = true");
            conexion.Open();


            string query = "Select Usuario, Password from Usuario";

            SqlCommand comand = new SqlCommand(query, conexion);

            SqlDataReader registro = comand.ExecuteReader();
            while (registro.Read())
            {

               if((registro.GetString(0) == usuario) && registro.GetString(1) == contrasenia)
                {

                    MessageBox.Show("USUARIO CORRECTO", usuario);

                }
                else
                {
                    MessageBox.Show("no se encontrro");
                }

            }
            //MessageBox.Show("Se abrió la conexión con el servidor SQL Server y se seleccionó la base de datos");

            conexion.Close();
            MessageBox.Show("Se cerró la conexión.");
        }

        
    }
}
