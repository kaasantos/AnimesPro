using API_Animes_Pro.Models;
using API_Animes_Pro.Repository.Interfaces;

namespace API_Animes_Pro.Controllers
{
    public class AnimesService : IAnimesService
    {
        private readonly IAnimesRepository _animesRepository;
        private readonly ILogSistemaRepository _geraLog;

        public AnimesService(IAnimesRepository animesRepository, ILogSistemaRepository logSistema)
        {
            _animesRepository = animesRepository;
            _geraLog = logSistema;
        }

        public async Task<List<AnimesModel>> ListaTodosAnimes()
        {
            try
            {
                var listaTodosAnimes = await _animesRepository.GetAll();

                if (listaTodosAnimes == null)
                    throw new Exception("Lista de animes não encontrada.");

                if (listaTodosAnimes.Count() == 0)
                    throw new Exception("Nenhum anime foi encontrado no sistema.");

                await _geraLog.AddLog(Enums.EnumAcao.GetAll, "Requisição getAll executada.");

                return listaTodosAnimes;
            }
            catch(Exception ex) 
            {
                await _geraLog.AddLog(Enums.EnumAcao.GetAll, ex.Message);
                throw new Exception(ex.Message, ex);
            }

        }

        public async Task<AnimesModel> ListaAnimePorId(int id)
        {
            try
            {
                if (id <= 0)
                    throw new Exception("Objeto nulo ou inválido.");

                var animePorId = await _animesRepository.GetById(id);
                if (animePorId == null)
                    throw new Exception($"Anime: {id} não encontrado.");

                await _geraLog.AddLog(Enums.EnumAcao.GetById, "Anime listado por id.", id.ToString());

                return animePorId;
            }
            catch(Exception ex)
            {
                await _geraLog.AddLog(Enums.EnumAcao.GetById, ex.Message, id.ToString());
                throw new Exception(ex.Message, ex);
            }

        }

        public async Task<List<AnimesModel>> ListaAnimePorChave(string chave, string filtro)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(chave))
                    throw new Exception("Chave nula ou inválida.");

                if (string.IsNullOrWhiteSpace(chave))
                    throw new Exception("Filtro nulo ou inválido.");

                var animePorChave = await _animesRepository.GetByKey(chave, filtro);
                if(animePorChave == null)
                    throw new Exception("Nenhum anime encontrado.");

                await _geraLog.AddLog(Enums.EnumAcao.GetByKey, "Anime listado por chave.", $"Chave: {chave}, Filtro: {filtro}");

                return animePorChave;
            }
            catch (Exception ex)
            {
                await _geraLog.AddLog(Enums.EnumAcao.GetByKey, ex.Message, $"Chave: {chave}, Filtro: {filtro}");
                throw new Exception(ex.Message, ex);
            }

        }

        public async Task<List<AnimesModel>> Paginacao(int pagina, int tamanho, string filtro = "", string chave = "")
        {
            try
            {
                if (pagina <= 0 || tamanho <= 0)
                    throw new Exception("Pagina e registro por páginas devem ser maior que 0.");

                if (tamanho > 1000)
                    throw new Exception("Limite de dados por página excedido.");

                var paginacao = await _animesRepository.Pagination(pagina, tamanho, filtro, chave);
                await _geraLog.AddLog(Enums.EnumAcao.Pagination, "Paginação executada.", 
                    $"Pagina: {pagina}, Registros: {tamanho}, Filtro: {filtro}, Chave do Filtro: {chave}");

                return paginacao;
            }
            catch (Exception ex)
            {
                await _geraLog.AddLog(Enums.EnumAcao.Pagination, ex.Message,
                  $"Pagina: {pagina}, Registros: {tamanho}, Filtro: {filtro}, Chave do Filtro: {chave}");
                throw new Exception(ex.Message, ex);
            }

        }

        public async Task<AnimesModel> AdicionarAnime(AnimesModel anime)
        {
            try
            {
                if (anime == null)
                    throw new Exception("Objeto nulo ou inválido.");

                if (anime.Id != 0)
                    throw new Exception("Id obrigatóriamente deve ser igual a 0.");

                var _animeNome = await _animesRepository.GetByKey(anime.Nome, "nomes");
                if (_animeNome.Count() > 0)
                    throw new Exception("Tentativa de adicionar anime com nome já existente.");

                var _anime = await _animesRepository.Add(anime);

                await _geraLog.AddLog(Enums.EnumAcao.Add, $"Registro: {anime.Id} inserido.", anime.Id.ToString());

                return _anime;
            }
            catch (Exception ex)
            {
                await _geraLog.AddLog(Enums.EnumAcao.Add, ex.Message, anime.Id.ToString());
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<AnimesModel> AtualizarAnimes(AnimesModel anime)
        {
            try
            {
                if (anime == null)
                    throw new Exception("Objeto nulo ou inválido.");

                var _checagemId = await _animesRepository.GetById(anime.Id);
                if (_checagemId == null)
                    throw new Exception($"Anime: {anime.Id} não encontrado.");

                var _checagemNome = await _animesRepository.GetByKey(anime.Nome, "nomes");
                if (_checagemNome.Count() > 0)
                {
                    foreach (var checaNomes in _checagemNome)
                    {
                        if (_checagemId.Id != checaNomes.Id && anime.Nome.Trim().ToLower() == checaNomes.Nome.Trim().ToLower())
                            throw new Exception("Existe outro anime com esse nome.");
                    }
                }

                var _anime = await _animesRepository.Put(anime);
                await _geraLog.AddLog(Enums.EnumAcao.Update, $"Registro: {anime.Id} atualizado.", anime.Id.ToString());

                return _anime;
            }
            catch (Exception ex)
            {
                await _geraLog.AddLog(Enums.EnumAcao.Update, ex.Message, anime.Id.ToString());
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<string> DeletarAnime(int id)
        {
            try
            {
                if (id <= 0)
                    throw new Exception("Objeto nulo ou inválido.");

                var _animeId = await _animesRepository.GetById(id);
                if (_animeId == null)
                    throw new Exception($"Anime: {id} não encontrado.");

                var _anime = await _animesRepository.Delete(_animeId);
                await _geraLog.AddLog(Enums.EnumAcao.Delete, $"Registro: {id} excluído.", id.ToString());

                return _anime;
            }
            catch (Exception ex)
            {
                await _geraLog.AddLog(Enums.EnumAcao.Delete, ex.Message, id.ToString());
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
