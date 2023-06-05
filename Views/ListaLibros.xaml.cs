using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using WpfAppTEST.Models;

namespace Museoapp.Views
{

    public partial class ListaLibros : Window
    {
        public ListaLibros()
        {
            InitializeComponent();

            string connectionString = "server=DESKTOP-TI2N3QM; database=Museo1 ; integrated security = true";
            string query = "SELECT [id_libro],[Titulo],[Origen] ,[Autor_id],[N_paginas] ,[Descripcion],[Categoria_id],[Editorial_id] FROM [dbo].[Libros]";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Crear una lista de objetos Libro
                List<Libros> libros = new List<Libros>();

                while (reader.Read())
                {
                    Libros libro = new Libros();
                    libro.LibroId = reader.GetInt32(0);
                    libro.Titulo = reader.GetString(1);
                    libro.Origen = reader.GetString(2);
                    //libro.AutorId = reader.GetInt32(3);
                    libro.N_paginas = reader.GetInt32(4);
                    libro.Descripcion = reader.GetString(5);
                    //libro.CategoriaId = reader.GetInt32(6);
                    //libro.EditorialId = reader.GetInt32(7);

                    libros.Add(libro);
                }

                // Asignar la lista de libros como origen de datos de la ListView
                listView.ItemsSource = libros;
            }



        }
    }
}