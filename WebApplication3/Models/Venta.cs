using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Venta
    {
        //Modelo
        public long Id { get; set; }
        public string Comentarios { get; set; }

        public int IdUsuario { get; set; }



        //Constructor
        public Venta()
        {
            Id = 0;
            Comentarios = string.Empty;
            IdUsuario = 0;
        }
    }
}