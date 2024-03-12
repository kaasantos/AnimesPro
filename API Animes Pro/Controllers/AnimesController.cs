using API_Animes_Pro.Models;
using API_Animes_Pro.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Animes_Pro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimesController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] IAnimesService service)
        {
            var listaTodosAnimes = await service.ListaTodosAnimes();
            return Ok(listaTodosAnimes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromServices] IAnimesService service, int id)
        {
            var animePorId = await service.ListaAnimePorId(id);
            return Ok(animePorId);
        }

        [HttpGet("{key}/{filter}")]
        public async Task<IActionResult> GetByKey([FromServices] IAnimesService service, string key, string filter)
        {
            var animePorChave = await service.ListaAnimePorChave(key, filter);
            return Ok(animePorChave);
        }

        [HttpGet("Pagination")]
        public async Task<IActionResult> Pagination([FromServices] IAnimesService service, int page, int pageSize, string filter = "", string keyFilter = "")
        {
            var paginacao = await service.Paginacao(page, pageSize, filter, keyFilter);
            return Ok(paginacao);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromServices] IAnimesService service, AnimesModel anime)
        {
            var _anime = await service.AdicionarAnime(anime);
            return Ok(_anime);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromServices] IAnimesService service, AnimesModel anime)
        {
            var _anime = await service.AtualizarAnimes(anime);
            return Ok(_anime);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromServices] IAnimesService service, int id)
        {
            var _anime = await service.DeletarAnime(id);
            return Ok(_anime);
        }
    }
}
