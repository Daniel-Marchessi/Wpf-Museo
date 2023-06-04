using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Museoapp.Views
{
    /// <summary>
    /// Lógica de interacción para ListaLibros.xaml
    /// </summary>
    public partial class ListaLibros : Window
    {
        public ListaLibros()
        {
            InitializeComponent();

            // Conectarse a la base de datos y recuperar los datos
            string connectionString = "server=DESKTOP-TI2N3QM; database=Museo1 ; integrated security = true"; // Reemplaza con tu cadena de conexión
            string query = "SELECT [id_libro],[Titulo],[Origen] ,[Autor_id],[N_paginas] ,[Descripcion],[Categoria_id],[Editorial_id] FROM [dbo].[Libros]"; // Reemplaza con tu consulta SQL

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Crear un DataTable para almacenar los datos
                DataTable table = new DataTable();
                table.Load(reader);

                // Asignar el DataTable como origen de datos de la ListView
                listView.ItemsSource = table.DefaultView;
            }
        }
    }
}