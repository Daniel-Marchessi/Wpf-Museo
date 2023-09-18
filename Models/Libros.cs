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


        private string edicion;
        public string Edicion
        {
            get { return edicion; }
            set {  edicion = value; }        
        }
        private string anioedicion;
        public string AnioEdicion
        {
            get { return anioedicion; }
            set { anioedicion = value; }
        }

        private string auto;
        public string Autores
        {
            get { return auto; }
            set { auto = value; }
        }



        private int codigo;
        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private string cate;
        public string Categorias
        {
            get { return cate; }
            set { cate = value; }
        }
        private string edit;
        public string Editoriales
        {
            get { return edit; }
            set { edit = value; }
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