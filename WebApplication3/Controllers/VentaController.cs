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
    public class VentaController : ControllerBase
    {
       
        [HttpGet("{idUsuario}")]
        public IEnumerable<Venta> GetVentas(int idUsuario)
        {
            return TraerVenta.GetVentas(idUsuario);
        }

        [HttpPost("{idUsuario}")]
        public void PostVenta(List<Producto> productos, int idUsuario)
        {
            TraerVenta.InsertVenta(productos, idUsuario);
        }

        [HttpDelete("{Id}")]
        public void DeleteVenta(int Id)
        {
            TraerVenta.EliminarVenta(Id);
        }
    }
}
