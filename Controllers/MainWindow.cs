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
using Museo.Logica;

namespace WpfAppTEST
{
    public partial class MainWindow : Window
    {

        public UserManager UserManager { get; }
        public MainWindow()
        {
            UserManager = new UserManager(); 
            InitializeComponent();
            user.Focus();
        }

        public void Btn_enviar_Click(object sender, RoutedEventArgs e)
        {
            string usuario = user.Text.ToString();
            string contrasenia = contra.Password.ToString();
            UserManager usermanager = new UserManager();

            if (usermanager.ValidateUser(usuario, contrasenia))
            {
                Home inicio = new Home();
                inicio.Show();
                this.Close();

            }
         
        }

    }
}
