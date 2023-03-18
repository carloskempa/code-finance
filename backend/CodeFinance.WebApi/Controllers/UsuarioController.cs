using CodeFinance.Application.Dtos;
using CodeFinance.Application.Interfaces.Application;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeFinance.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioApp _usuarioApp;

        public UsuarioController(IUsuarioApp usuarioApp)
        {
            _usuarioApp = usuarioApp;
        }


        [HttpPost("cadastrar")]
        public async Task<IActionResult> Create([FromBody] CadastrarUsuarioDto usuario)
        {
            try
            {
                var resultado = await _usuarioApp.Cadastrar(usuario);
                return resultado.Sucesso ? Ok(resultado) : BadRequest(resultado);
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
