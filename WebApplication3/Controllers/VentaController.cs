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
        //private readonly ILogger<ProductoVendido> _logger;

        //public VentaController(ILogger<ProductoVendido> logger)
        //{
        //    _logger = logger;
        //}

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
    }
}
