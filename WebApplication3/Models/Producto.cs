using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Producto
    {
        //Modelo        
     



        public long Id { get; set; }
        public string Descripciones { get; set; }
        public decimal Costo { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Stock { get; set; }
        public long IdUsuario { get; set; }

        //Constructor
        public Producto()
        {
            Id = 0;
            Descripciones = string.Empty;
            Costo = 0;
            PrecioVenta = 0;
            Stock = 0;
            IdUsuario = 0;
        }
    }
}
