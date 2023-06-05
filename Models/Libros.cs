using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppTEST.Models
{
    public class Libros
    {
       


        private int id;
        public int LibroId
        {
            get { return id; }
            set { id = value; }
        }


        private string titulo;

        public string Titulo
        {
            get { return titulo; }
            set { titulo = value; }
        }


        private string origen;
        public string Origen
        {
            get { return origen; }
            set { origen = value; }
        }

        private int autorId;
        public int AutorId
        {
            get { return autorId; }
            set { autorId = value; }
        }

        private int numeroPaginas;
        public int N_paginas
        {
            get { return numeroPaginas; }
            set { numeroPaginas = value; }
        }

        private string descripcion;
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        private int categoriaId;
        public int CategoriaId
        {
            get { return categoriaId; }
            set { categoriaId = value; }
        }


        private int editorialId;
        public int EditorialId
        {
            get { return editorialId; }
            set { editorialId = value; }
        }
    }
}