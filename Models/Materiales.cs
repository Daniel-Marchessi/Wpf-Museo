using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Museoapp.Models
{
    internal class Materiales
    {
        private int id;
        public int id_material
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
