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


        private int? carpeta;

        public int? id_carpeta
        {
            get { return carpeta; }
            set { carpeta = value; }
        }


        private int? categoria;


        public int? id_categoria
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


        private string cate;
        public string Categorias
        {
            get { return cate; }
            set { cate = value; }
        }


        private string carpe;
        public string Carpetas
        {
            get { return carpe; }
            set { carpe = value; }
        }








    }
}
