using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Repository;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {


        [HttpGet("{idUsuario}")]
        public IEnumerable<Producto> GetAllProductos(int idUsuario)
        {
            return TraerProducto.GetProductos(idUsuario);
        }

        [HttpPut]
        public void PutProductos(Producto producto)
        {
            
             TraerProducto.ModificarProductos(producto);
        }

        [HttpPost]
        public void PostProducto(Producto producto)
        {
            TraerProducto.InsertProducto(producto);
        }

        [HttpDelete("{idProducto}")]
        public void DeleteProductos(int idProducto)
        {
            TraerProducto.EliminarProducto(idProducto);
        }
    }
}
