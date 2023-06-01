using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppTEST.Models
{
    class piezas
    {
        private string Nombre;
        public string Nombrep
        {
            get { return Nombre; }
            set { Nombre = value; }
        }

        private string Cantidad;
        public string Cantidadp
        {
            get { return Cantidad; }
            set { Cantidad = value; }
        }



        private string Tipo;

        public string Tipop {
            get { return Tipo; }
            set { Tipo = value; }

        }
        private string Periodo { get; set; }
        public string Periodop
        {
            get { return Periodo; }
            set { Periodo = value; }
        }
        private int Alto { get; set; }

        public int Altop
        {
            get { return Alto; }
            set
            {
                Alto = value;
            }
        }
        private float Diametro { get; set; }

        public float Diametrop
        {
            get { return Diametro; }
            set { Diametro = value; }
        }
        private string Forma_ingreso { get; set; }
        public string Forma_ingresop
        {
            get { return Forma_ingreso; }
            set
            {
                Forma_ingreso = value;
            }
        }
        private string Conservacion { get; set; }

        public string Conservacionp
        {
            get { return Conservacion; }
            set
            {
                Conservacion = value;
            }
        }
        private string Ubicacion { get; set; }

        public string Ubicacionp
        {
            get { return Ubicacion; }
            set
            {
                Ubicacion = value;
            }
        }
        private string Titulo { get; set; }
        public string Titulop
        {
            get { return Titulo; }
            set
            {
                Titulo = value;
            }
        }


        public piezas() { }

    }





}
