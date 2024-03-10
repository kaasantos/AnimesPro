using API_Animes_Pro.Models;
using API_Animes_Pro.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Animes_Pro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogSistemaController : ControllerBase
    {
        private readonly ILogSistemaRepository _logSistemaRepository;

        public LogSistemaController(ILogSistemaRepository logSistemaRepository)
        {
            _logSistemaRepository = logSistemaRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<LogSistemaModel>>> GetAll()
        {
            try
            {
                var listaTodosAnimes = await _logSistemaRepository.GetAll();
                await _logSistemaRepository.FazLog(Enums.EnumAcao.GetAll, "Consulta ao Log Executada Com Sucesso!");

                return Ok(listaTodosAnimes);
            }
            catch(Exception ex)
            {
                await _logSistemaRepository.FazLog(Enums.EnumAcao.GetAll, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ChecarLogPorData")]
        public async Task<ActionResult<AnimesModel>> ChecarLogPorData(DateTime dataInicial, DateTime dataFinal)
        {
            try
            {
                var anime = await _logSistemaRepository.ChecarLogPorData(dataInicial, dataFinal);
                await _logSistemaRepository.FazLog(Enums.EnumAcao.GetById, "Consulta ao Log Executada Com Sucesso!");

                return Ok(anime);
            }
            catch(Exception ex)
            {
                await _logSistemaRepository.FazLog(Enums.EnumAcao.GetById, ex.Message);
                return BadRequest(ex.Message);
            }

        }
    }
}
