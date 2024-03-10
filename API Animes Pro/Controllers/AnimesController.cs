using API_Animes_Pro.Models;
using API_Animes_Pro.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Animes_Pro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimesController : ControllerBase
    {
        private readonly IAnimesRepository _animesRepository;
        private readonly ILogSistemaRepository _geraLog;

        public AnimesController(IAnimesRepository animesRepository, ILogSistemaRepository logSistema)
        {
            _animesRepository = animesRepository;
            _geraLog = logSistema;
        }

        [HttpGet]
        public async Task<ActionResult<List<AnimesModel>>> GetAll()
        {
            try
            {
                var listaTodosAnimes = await _animesRepository.GetAll();
                await _geraLog.FazLog(Enums.EnumAcao.GetAll, "Requisição GetAll Feita Com Sucesso!");

                return Ok(listaTodosAnimes);
            }
            catch(Exception ex) 
            {
                await _geraLog.FazLog(Enums.EnumAcao.GetAll, ex.Message);
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AnimesModel>> GetById(int id)
        {
            try
            {
                var anime = await _animesRepository.GetById(id);
                await _geraLog.FazLog(Enums.EnumAcao.GetById, "Requisição GetAll Feita Com Sucesso!");
                return Ok(anime);
            }
            catch(Exception ex)
            {
                await _geraLog.FazLog(Enums.EnumAcao.GetById, ex.Message);
                return BadRequest(ex.Message);
            }

        }
    }
}
