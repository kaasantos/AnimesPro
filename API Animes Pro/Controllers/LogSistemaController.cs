using API_Animes_Pro.Models;
using API_Animes_Pro.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Animes_Pro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogSistemaController: ControllerBase
    {
        /// <summary>
        /// Lista todos os logs do sistema.
        /// </summary>
        /// <returns>Lista de logs</returns>
        [HttpGet]
        public async Task<IActionResult> ExibirTodosLogs([FromServices] ILogSistemaService service)
        {
            var retorno = await service.RecebeTodosLogs();
            return Ok(retorno);
        }

        /// <summary>
        /// Lista logs do sistema no intervalo de datas.
        /// </summary>
        /// <remarks>
        /// Regras:  data no formado date time, exemplo: 03/13/2024 19:30:00 (mês/dia/ano hora/minuto/segundo).
        /// Condições: data inicial ser menor que a data final.
        /// </remarks>
        /// <returns>Lista de logs</returns>
        [HttpGet("ChecarLogPorData")]
        public async Task<ActionResult<AnimesModel>> ChecarLogPorData([FromServices] ILogSistemaService service, DateTime dataInicial, DateTime dataFinal)
        {
            var retorno = await service.RecebeLogPorIntervaloDeHorario(dataInicial, dataFinal);
            return Ok(retorno);
        }
    }
}
