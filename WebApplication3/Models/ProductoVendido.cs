using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class ProductoVendido
    {
        //Modelo
      

        public long Id { get; set; }
        public int Stock { get; set; }
        public decimal IdProducto { get; set; }
        public decimal IdVenta { get; set; }


        //Constructor
        public ProductoVendido()
        {
            Id = 0;
            IdProducto = 0;
            Stock = 0;
            IdVenta = 0;
        }
    }
}
