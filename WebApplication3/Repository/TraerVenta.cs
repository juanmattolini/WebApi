using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using WebApi.Models;



namespace WebApi.Repository
{



    public class TraerVenta
    {
   

        public static List<Venta> GetVentas(int Id)
        {




            string connectionString = @"Server=swdmdzbaspi02;Database=SistemaGestion;Trusted_Connection=True;";
            var query = "SELECT Id,Comentarios FROM Venta";
            var listaVentas = new List<Venta>();

            using (SqlConnection conect = new SqlConnection(connectionString))
            {
                conect.Open();
                using (SqlCommand comando = new SqlCommand(query, conect))
                {
                    using (SqlDataReader dr = comando.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            var venta = new Venta();
                            venta.Id = dr.GetInt64("Id");
                            venta.Comentarios = dr.GetString("Comentarios");

                            listaVentas.Add(venta);
                        }

                    }

                }
            }
            return listaVentas;
        }
        public static void InsertVenta(List<Producto> productos, int IdUsuario)
        {
            Venta venta = new Venta();
            string connectionString = @"Server=swdmdzbaspi02;Database=SistemaGestion;Trusted_Connection=True;";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Connection.Open();
            sqlCommand.CommandText = @"INSERT INTO Venta
                                ([Comentarios]
                                ,[IdUsuario])
                                VALUES
                                (@Comentarios,
                                    @IdUsuario)";

            sqlCommand.Parameters.AddWithValue("@Comentarios", "");
            sqlCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);

            sqlCommand.ExecuteNonQuery(); //Se ejecuta realmente el INSERT INTO
            venta.Id = GetId.Get(sqlCommand);
            venta.IdUsuario = IdUsuario;

            foreach (Producto producto in productos)
            {
                sqlCommand.CommandText = @"INSERT INTO ProductoVendido
                                ([Stock]
                                ,[IdProducto]
                                ,[IdVenta])
                                VALUES
                                (@Stock,
                                @IdProducto,
                                @IdVenta)";

                sqlCommand.Parameters.AddWithValue("@Stock", producto.Stock);
                sqlCommand.Parameters.AddWithValue("@IdProducto", producto.Id);
                sqlCommand.Parameters.AddWithValue("@IdVenta", venta.Id);

                sqlCommand.ExecuteNonQuery(); //Se ejecuta realmente el INSERT INTO
                sqlCommand.Parameters.Clear();

                sqlCommand.CommandText = @" UPDATE Producto
                                                SET 
                                                Stock = Stock - @Stock
                                                WHERE id = @IdProducto";

                sqlCommand.Parameters.AddWithValue("@Stock", producto.Stock);
                sqlCommand.Parameters.AddWithValue("@IdProducto", producto.Id);

                sqlCommand.ExecuteNonQuery(); //Se ejecuta realmente el INSERT INTO
                sqlCommand.Parameters.Clear();
            }
            sqlCommand.Connection.Close();
        }


        public static bool EliminarVenta(int Id)
        {
            string connectionString = @"Server=swdmdzbaspi02;Database=SistemaGestion;Trusted_Connection=True;";
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.Connection.Open();

                    sqlCommand.CommandText = @" DELETE 
                                                    Venta
                                                WHERE 
                                                    Id = @ID
                                            ";

                    sqlCommand.Parameters.AddWithValue("@ID", Id);


                    int recordsAffected = sqlCommand.ExecuteNonQuery(); //Se ejecuta realmente el DELETE

                    sqlCommand.Connection.Close();

                    if (recordsAffected != 1)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }

    }
}
