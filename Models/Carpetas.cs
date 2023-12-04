using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Museo.Models
{
    internal class Carpetas
    {
        private int id;
        public int id_carpeta
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
    }
}
