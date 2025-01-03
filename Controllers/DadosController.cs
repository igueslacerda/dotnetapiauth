using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apiauth.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class DadosController : ControllerBase
    {
        /// <summary>
        /// Simples teste de acesso autenticado.
        /// </summary>
        /// <returns>Mensagem de confirmação de acesso.</returns>
        /// <response code="200">Solicitação com sucesso.</response>
        /// <response code="401">Acesso não autorizado.</response>
        [HttpGet("teste")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult TesteAcesso()
        {
            return Ok("Acesso bem sucedido!");
        }
    }
}