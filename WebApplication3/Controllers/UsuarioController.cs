using Microsoft.AspNetCore.Mvc;
using WebApi.Repository;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
    

        [HttpGet("{nombreUsuario}/{contraseña}")]
        public Usuario GetUsuarioByContraseña(string nombreUsuario, string contraseña)
        {
            var usuario = TraerUsuario.GetUsuarioByPassword(nombreUsuario, contraseña);

            return usuario == null ? new Usuario() : usuario;
        }

        [HttpGet("{nombreUsuario}")]
        public Usuario GetUsuarioByNombre(string nombreUsuario)
        {
            var usuario = TraerUsuario.GetUsuarioByUserName(nombreUsuario);

            return usuario == null ? new Usuario() : usuario;
        }



        [HttpPost]
        public void PostUsuario(Usuario usuario)
        {
            TraerUsuario.InsertUsuario(usuario);
        }

        [HttpPut]
        public bool PutUsuario(Usuario usuario)
        {
            return TraerUsuario.UpdateUsuario(usuario);
        }


        [HttpDelete]
        public void Eliminar([FromBody] int Id)
        {
        }
    }
}
