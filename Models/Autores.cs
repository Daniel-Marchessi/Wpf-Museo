using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Museoapp.Models
{
    internal class Autores
    {
        private int id;
        public int id_autor
        {
            get { return id; }
            set { id = value; }
        }
       


        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }


        private string apellido;
        

        public string Apellido
        {
            get { return apellido; }
            set { apellido = value; }
        }

        private string nc;

        public string NombreCompleto
        {
            get { return nc; }
            set { nc = value; }
        }
    }
}
