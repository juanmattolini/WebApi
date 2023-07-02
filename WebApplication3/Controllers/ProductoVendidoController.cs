using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Repository;
using WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoVendidoController : ControllerBase
    {

      

        [HttpGet("{idUsuario}")]
        public IEnumerable<ProductoVendido> GetAllProductosVendidos(int idUsuario)
        {
            return TraerProductoVendido.GetProductosVendidos(idUsuario);
        }
    }
}
