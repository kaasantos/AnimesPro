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
                await _geraLog.FazLog(Enums.EnumAcao.GetAll, "Requisição GetAll Executada!");

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
                await _geraLog.FazLog(Enums.EnumAcao.GetById, "Requisição GetById Executada!", id.ToString());

                return Ok(anime);
            }
            catch(Exception ex)
            {
                await _geraLog.FazLog(Enums.EnumAcao.GetById, ex.Message, id.ToString());
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{key}/{filter}")]
        public async Task<ActionResult<AnimesModel>> GetByKey(string key, string filter)
        {
            try
            {
                var anime = await _animesRepository.GetByKey(key, filter);
                await _geraLog.FazLog(Enums.EnumAcao.GetByKey, "Requisição GetByKey Executada!", $"Chave: {key}, Filtro: {filter}");

                return Ok(anime);
            }
            catch (Exception ex)
            {
                await _geraLog.FazLog(Enums.EnumAcao.GetByKey, ex.Message, $"Chave: {key}, Filtro: {filter}");
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{Pagination}")]
        public async Task<ActionResult<List<AnimesModel>>> Pagination(int page, int pageSize, string filter = "", string keyFilter = "Default")
        {
            try
            {
                var paginacao = await _animesRepository.Pagination(page, pageSize, filter, keyFilter);
                await _geraLog.FazLog(Enums.EnumAcao.Pagination, "Paginação Executada!", 
                    $"Pagina: {page}, Registros: {pageSize}, Filtro: {filter}, Chave do Filtro: {keyFilter}");

                return Ok(paginacao);
            }
            catch (Exception ex)
            {
                await _geraLog.FazLog(Enums.EnumAcao.Pagination, ex.Message,
                  $"Pagina: {page}, Registros: {pageSize}, Filtro: {filter}, Chave do Filtro: {keyFilter}");
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<ActionResult<AnimesModel>> Add(AnimesModel anime)
        {
            try
            {
                var _anime = await _animesRepository.Add(anime);
                await _geraLog.FazLog(Enums.EnumAcao.Add, $"Registro: {anime.Id} Inserido!", anime.Id.ToString());

                return Ok(_anime);
            }
            catch (Exception ex)
            {
                await _geraLog.FazLog(Enums.EnumAcao.Add, ex.Message, anime.Id.ToString());
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<AnimesModel>> Put(AnimesModel anime)
        {
            try
            {
                var _anime = await _animesRepository.Put(anime);
                await _geraLog.FazLog(Enums.EnumAcao.Update, $"Registro: {anime.Id} Atualizado!", anime.Id.ToString());

                return Ok(_anime);
            }
            catch (Exception ex)
            {
                await _geraLog.FazLog(Enums.EnumAcao.Update, ex.Message, anime.Id.ToString());
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<AnimesModel>> Delete(int id)
        {
            try
            {
                var _anime = await _animesRepository.Delete(id);
                await _geraLog.FazLog(Enums.EnumAcao.Delete, $"Registro: {id} Excluido!", id.ToString());

                return Ok(_anime);
            }
            catch (Exception ex)
            {
                await _geraLog.FazLog(Enums.EnumAcao.Delete, ex.Message, id.ToString());
                return BadRequest(ex.Message);
            }
        }
    }
}
