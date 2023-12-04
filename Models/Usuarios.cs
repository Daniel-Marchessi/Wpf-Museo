using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private static int id;

        public static int Usuario_id
        {
            get { return id; }
            set { id = value; }
        }






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



        //Estos campos y propiedades están vinculados a instancias específicas
        //de la clase, por lo que necesitas una instancia de la clase para acceder a ellos.


        //HISTORIAL DE USUARIOS

        private string rol;
        public string RolUsuario1
        {
            get { return rol; }
            set { rol = value; }
        }


        public int UsuarioId { get; set; }
        public DateTime FechaHoraAcceso { get; set; }
        public DateTime? FechaHoraSalida { get; set; }





    }
}
