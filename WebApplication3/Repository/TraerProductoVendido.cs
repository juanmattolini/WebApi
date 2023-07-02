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



    public class TraerProductoVendido
    {
   

        public static List<ProductoVendido> GetProductosVendidos(int Id)
        {




            string connectionString = @"Server=swdmdzbaspi02;Database=SistemaGestion;Trusted_Connection=True;";
            var query = "SELECT Id,Stock,IdProducto,IdVenta FROM productovendido";
            var listaproductovendidos = new List<ProductoVendido>();

            using (SqlConnection conect = new SqlConnection(connectionString))
            {
                conect.Open();
                using (SqlCommand comando = new SqlCommand(query, conect))
                {
                    using (SqlDataReader dr = comando.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            var productovendido = new ProductoVendido();
                            productovendido.Id = dr.GetInt64("Id");
                            productovendido.Stock = dr.GetInt32("Stock");
                            productovendido.IdProducto = dr.GetInt64("IdProducto");
                            productovendido.IdVenta = dr.GetInt64("IdVenta");
                            listaproductovendidos.Add(productovendido);
                        }

                    }

                }
            }
            return listaproductovendidos;
        }

 
    }
}
