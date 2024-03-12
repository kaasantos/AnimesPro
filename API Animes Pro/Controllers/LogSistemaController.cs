using API_Animes_Pro.Models;
using API_Animes_Pro.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Animes_Pro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogSistemaController: ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> ExibirTodosLogs([FromServices] ILogSistemaService service)
        {
            var retorno = await service.RecebeTodosLogs();
            return Ok(retorno);
        }

        [HttpGet("ChecarLogPorData")]
        public async Task<ActionResult<AnimesModel>> ChecarLogPorData([FromServices] ILogSistemaService service, DateTime dataInicial, DateTime dataFinal)
        {
            var retorno = await service.RecebeLogPorIntervaloDeHorario(dataInicial, dataFinal);
            return Ok(retorno);
        }
    }
}
