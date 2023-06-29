using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using WebApi.Models;
using System.Security.Cryptography.X509Certificates;

namespace WebApi.Repository

{



    public static class TraerProducto
    {

        public static List<Producto> GetProductos(int Id)
        {


           string connectionString = @"Server=swdmdzbaspi02;Database=SistemaGestion;Trusted_Connection=True;";
            var query = "SELECT Id,Descripciones,Costo,PrecioVenta,Stock,IdUsuario FROM Producto";

            var listaProductos = new List<Producto>();

            using (SqlConnection conect = new SqlConnection(connectionString))
            {
                conect.Open();
                using (SqlCommand comando = new SqlCommand(query, conect))
                {
                    using (SqlDataReader dr = comando.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            var producto = new Producto();
                            producto.Id = dr.GetInt64("Id");
                            producto.Descripciones = dr.GetString("Descripciones");
                            producto.Costo = dr.GetDecimal("Costo");
                            producto.PrecioVenta = dr.GetDecimal("PrecioVenta");
                            producto.Stock = dr.GetInt32("Stock");
                            producto.IdUsuario = dr.GetInt64("IdUsuario");
                            listaProductos.Add(producto);
                        }

                    }

                }


            }

            return listaProductos;
        }

        public static bool ModificarProductos(Producto producto)
        {
            bool modificado = false;
            string connectionString = @"Server=swdmdzbaspi02;Database=SistemaGestion;Trusted_Connection=True;";


            if (producto.Descripciones == null ||
                producto.Descripciones == "" ||
                producto.IdUsuario == 0)
            {
                return modificado;
            }
            else
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.Connection.Open();
                        sqlCommand.CommandText = @" UPDATE Producto
                                                SET 
                                                   Descripciones = @Descripciones,
                                                   Costo = @Costo,
                                                   PrecioVenta = @PrecioVenta,
										           Stock = @Stock
                                                WHERE id = @ID";

                        sqlCommand.Parameters.AddWithValue("@Descripciones", producto.Descripciones);
                        sqlCommand.Parameters.AddWithValue("@Costo", producto.Costo);
                        sqlCommand.Parameters.AddWithValue("@PrecioVenta", producto.PrecioVenta);
                        sqlCommand.Parameters.AddWithValue("@Stock", producto.Stock);
                        sqlCommand.Parameters.AddWithValue("@ID", producto.Id);


                        int recordsAffected = sqlCommand.ExecuteNonQuery(); //Se ejecuta realmente UPDATE
                        sqlCommand.Connection.Close();

                        if (recordsAffected == 0)
                        {
                            return modificado;
                            throw new Exception("El registro a modificar no existe.");
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
        }

        public static bool InsertProducto(Producto producto)
        {
            bool alta = false;
            bool modificado = false;
            string connectionString = @"Server=swdmdzbaspi02;Database=SistemaGestion;Trusted_Connection=True;";
            if (producto.Descripciones == null ||
              producto.Descripciones == "" ||
              producto.IdUsuario == 0)
            {
                return modificado;
            }
            else
            {

                SqlConnection sqlConnection = new SqlConnection(connectionString);
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Connection.Open();
                sqlCommand.CommandText = @"INSERT INTO Producto
                                ([Descripciones]
                                ,[Costo]
                                ,[PrecioVenta]
								,[Stock]
                                ,[IdUsuario])
                                VALUES
                                (@Descripciones,
                                    @Costo,
                                    @PrecioVenta,
									@Stock,
                                    @IdUsuario)";



                sqlCommand.Parameters.AddWithValue("@Descripciones", producto.Descripciones);
                sqlCommand.Parameters.AddWithValue("@Costo", producto.Costo);
                sqlCommand.Parameters.AddWithValue("@PrecioVenta", producto.PrecioVenta);
                sqlCommand.Parameters.AddWithValue("@Stock", producto.Stock);
                sqlCommand.Parameters.AddWithValue("@IdUsuario", producto.IdUsuario);

                sqlCommand.ExecuteNonQuery(); //Se ejecuta realmente el INSERT INTO
                producto.Id = GetId.Get(sqlCommand);

                alta = producto.Id != 0 ? true : false;
                sqlCommand.Connection.Close();
                return alta;
            }

        }

        public static bool EliminarProducto(int Id)
        {
            string connectionString = @"Server=swdmdzbaspi02;Database=SistemaGestion;Trusted_Connection=True;";
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.Connection.Open();

                    sqlCommand.CommandText = @" DELETE 
                                                    ProductoVendido
                                                WHERE 
                                                    IdProducto = @ID
                                            ";

                    sqlCommand.Parameters.AddWithValue("@ID", Id);


                    int recordsAffected = sqlCommand.ExecuteNonQuery(); //Se ejecuta realmente el DELETE

                    sqlCommand.CommandText = @" DELETE 
                                                    Producto
                                                WHERE 
                                                    Id = @ID
                                            ";

                    recordsAffected = sqlCommand.ExecuteNonQuery(); //Se ejecuta realmente el DELETE
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