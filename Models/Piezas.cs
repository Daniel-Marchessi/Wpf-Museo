using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfAppTEST.Models
{
    class Piezas
    {
        private int id;
        public int Coleccion_id
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

        private int cantidad;
        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

        private string titulo;
        public string Titulo_alias
        {
            get { return titulo; }
            set { titulo = value; }
        }

        private string lugarprocedencia;
        public string Lugar_proce
        {
            get { return lugarprocedencia; }
            set { lugarprocedencia = value; }
        }


        private string periodo;
        public string Periodo
        {
            get { return periodo; }
            set { periodo = value; }
        }

        private int materialid;
        public int Material_id
        {
            get { return materialid; }
            set { materialid = value; }
        }

        private int alto;
        public int Alto
        {
            get { return alto; }
            set { alto = value; }
        }

        private int ancho;
        public int Ancho
        {
            get { return ancho; }
            set { ancho = value; }
        }

        private int largo;
        public int Largo
        {
            get { return largo; }
            set { largo = value; }
        }

        private double diametro;
        public double Diametro
        {
            get { return diametro; }
            set { diametro = value; }
        }


        private string integridad;
        public string Integridad
        {
            get { return integridad; }
            set { integridad = value; }
        }

        private string conservacion;
        public string Conservacion
        {
            get { return conservacion; }
            set { conservacion = value; }
        }

        private string ubicacion;
        public string Ubicacion
        {
            get { return ubicacion; }
            set { ubicacion = value; }
        }

        private string forma_ingreso;
        public string Ingreso
        {
            get { return forma_ingreso; }
            set { forma_ingreso = value; }
        }

        private int localidadid;
        public int Localidad_id
        {
            get { return localidadid; }
            set { localidadid = value; }
        }

        private int autorid;
        public int Autor_id
        {
            get { return autorid; }
            set { autorid = value; }
        }

        private ImageSource foto;
        public ImageSource Foto
        {
            get { return foto; }
            set { foto = value; }
        }



        public Piezas() { }
    }
}