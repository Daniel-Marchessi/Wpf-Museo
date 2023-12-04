using Museo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;

namespace Museo.Logica;

public class UserManager
{
    public bool ValidateUser(string usuario, string contrasenia)
    {

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
                Usuarios.RolUsuario = registro.GetString(2);
                Usuarios.Usuario_id = registro.GetInt32(3);
                MessageBox.Show("Bienvenido");
                return true; // Usuario y contraseña válidos

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
        return false;
    }
}
