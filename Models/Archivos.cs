using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppTEST.Models
{
    public class Archivos
    {
        private int id;
        public int id_archivo
        {
            get { return id; }
            set { id = value; }
        }


        private string carpeta;

        public string Carpeta
        {
            get { return carpeta; }
            set { carpeta = value; }
        }


        private string categoria;


        public string Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }

        private int codigo;
        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private string titulo;

        public string Titulo
        {
            get { return titulo; }
            set { titulo = value; }
        }
    }
}
