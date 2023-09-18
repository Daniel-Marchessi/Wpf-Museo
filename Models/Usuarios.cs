using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Museo.Models
{
    class Usuarios
    {

        //Pueden ser accedidos desde cualquier lugar de la clase y desde 
        //    fuera de la clase utilizando el nombre de la clase.
        private static string rolUsuario;

        public static string RolUsuario
        {
            get { return rolUsuario; }
            set { rolUsuario = value; }
        }

        //Estos campos y propiedades están vinculados a instancias específicas
        //de la clase, por lo que necesitas una instancia de la clase para acceder a ellos.
        private string usuario;
        public string Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        private string contrasenia;
        public string Contrasenia
        {
            get { return contrasenia; }
            set { contrasenia = value; }
        }
    }
}
