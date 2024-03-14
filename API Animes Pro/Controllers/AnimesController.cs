using API_Animes_Pro.Models;
using API_Animes_Pro.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Animes_Pro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimesController : ControllerBase
    {

        /// <summary>
        /// Lista todos os animes cadastrados no sistema.
        /// </summary>
        /// <returns>Lista de Animes.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] IAnimesService service)
        {
            var listaTodosAnimes = await service.ListaTodosAnimes();
            return Ok(listaTodosAnimes);
        }

        /// <summary>
        /// Recebe um anime específico de acordo com o id.
        /// </summary>
        ///<remarks>
        ///Condições: id maior que 0 e anime existir no sistema.
        ///</remarks>
        /// <returns>Anime</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromServices] IAnimesService service, int id)
        {
            var animePorId = await service.ListaAnimePorId(id);
            return Ok(animePorId);
        }

        /// <summary>
        /// Recebe um ou mais registro de acordo com a chave e o filtro.
        /// </summary>
        /// <remarks>
        /// Regras: filtros - 'nomes', 'diretor', 'resumo' e 'todos'. 
        /// Condições:  chave e filtros não nulos.
        /// </remarks>
        /// <returns>Lista de animes</returns>
        [HttpGet("{key}/{filter}")]
        public async Task<IActionResult> GetByKey([FromServices] IAnimesService service, string key, string filter)
        {
            var animePorChave = await service.ListaAnimePorChave(key, filter);
            return Ok(animePorChave);
        }

        /// <summary>
        /// Registros paginados de acordo com as entradas.
        /// </summary>
        /// <remarks>
        /// Condições: Page e pageSize devem ser maior que 0 e pageSize menor ou igual a 1000.
        /// </remarks>
        /// <returns>Lista de animes</returns>
        [HttpGet("Pagination")]
        public async Task<IActionResult> Pagination([FromServices] IAnimesService service, int page, int pageSize, string filter = "", string keyFilter = "")
        {
            var paginacao = await service.Paginacao(page, pageSize, filter, keyFilter);
            return Ok(paginacao);
        }

        /// <summary>
        /// Adiciona um anime em específico.
        /// </summary>
        /// <remarks>
        /// Condições: id igual a 0, diretor não nulo e nome do anime não nulo e diferente de um outro existente no sistema.
        /// </remarks>
        /// <returns>Anime adicionado</returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromServices] IAnimesService service, [FromBody] AnimesModel anime)
        {
            var _anime = await service.AdicionarAnime(anime);
            return Ok(_anime);
        }

        /// <summary>
        /// Atualiza um anime específico.
        /// </summary>
        /// <remarks>
        /// Condições: id igual maior que 0, nome do anime não nulo, nome do diretor não nulo, id do registro existir no sistema e não existir OUTRO anime no sistema com aquele nome cadastrado.
        /// </remarks>
        /// <returns>Anime atualizado</returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromServices] IAnimesService service, [FromBody] AnimesModel anime)
        {
            var _anime = await service.AtualizarAnimes(anime);
            return Ok(_anime);
        }

        /// <summary>
        /// Deleta um anime específico.
        /// </summary>
        /// <remarks>
        /// Condições: id diferente de 0 e existir um anime com aquele id cadastrado no sistema.
        /// </remarks>
        /// <returns>Mensagem de confirmação</returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromServices] IAnimesService service, int id)
        {
            var _anime = await service.DeletarAnime(id);
            return Ok(_anime);
        }
    }
}
