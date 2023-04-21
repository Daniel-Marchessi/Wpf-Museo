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

       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
          SqlConnection conexion = new SqlConnection("server=DESKTOP-9MTUTME; database=Museo1 ; integrated security = true");
            conexion.Open();

            string usuario = UsuarioText.Text;
            string contraseña = ContraseñaText.Text;

            string query = "Select usuario, password from Usuario";

            SqlCommand comando  = new SqlCommand(query, conexion);
            SqlDataReader registro = comando.ExecuteReader();

            while (registro.Read())
            {
                string col1 = registro.GetString(0);
                string col2 = registro.GetString(1);

                MessageBox.Show(col1);
                MessageBox.Show(col2);

            }
            
            registro.Close();
            //MessageBox.Show("Se abrió la conexión con el servidor SQL Server y se seleccionó la base de datos");
            conexion.Close();
            //MessageBox.Show("Se cerró la conexión.");


        }

    }
}
