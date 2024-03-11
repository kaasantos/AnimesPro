using API_Animes_Pro.Data;
using API_Animes_Pro.Models;
using API_Animes_Pro.Repository.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;

namespace API_Animes_Pro.Repository
{
    public class AnimesRepository : IAnimesRepository
    {
        private readonly DBContext _dbContext;
        public AnimesRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<AnimesModel>> GetAll()
        {
            var animes = await _dbContext.Animes
                .AsNoTracking()
                .OrderBy(a => a.Id)
                .ToListAsync();
            if (animes == null)
                throw new Exception("Lista de Animes Não Encontrada!");

            return animes;
        }

        public async Task<AnimesModel> GetById(int id)
        {
            if (id <= 0)
                throw new Exception("Objeto Nulo ou Inválido!");

            var anime = await _dbContext.Animes
                .AsNoTracking()
                .OrderBy(a => a.Id)
                .FirstOrDefaultAsync(a => a.Id == id);
            if (anime == null)
                throw new Exception($"Anime: {id} Não Encontrado!");

            return anime;
        }

        public async Task<List<AnimesModel>> GetByKey(string key, string filter)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new Exception("Chave Nula ou Inválida!");

            if (string.IsNullOrWhiteSpace(filter))
                throw new Exception("Filtro Nulo ou Inválido!");

            var animes = _dbContext.Animes
                .Select(a => new AnimesModel
                {
                    Id = a.Id,
                    Nome = a.Nome,
                    Diretor = a.Diretor,
                    Resumo = a.Resumo,
                })
                .AsNoTracking();

            animes = FiltrarQuery(filter, key, animes);

            return await animes.ToListAsync();
        }

        public async Task<List<AnimesModel>> Pagination(int page, int pageSize, string key = "", string filter = "Default")
        {
            if (page <= 0 || pageSize <= 0) 
                throw new Exception("Pagina e Registro Por Páginas Devem Ser Maior Que 0.");

            if (pageSize > 1000)
                throw new Exception("Limite de Dados por Página Excedido.");

            var paginacao = _dbContext.Animes
                .Select(a => new AnimesModel
                {
                    Id = a.Id,
                    Nome = a.Nome,
                    Diretor = a.Diretor,
                    Resumo = a.Resumo
                })
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(filter))
                paginacao = FiltrarQuery(filter, key, paginacao);
            else
                paginacao = paginacao
                    .OrderBy(a => a.Id);

            //Lembrar de por na documentação que a paginação inicia em 1
            paginacao = paginacao
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize);
            return await paginacao.ToListAsync();
        }

        public async Task<AnimesModel> Add(AnimesModel anime)
        {
            if (anime == null)
                throw new Exception("Objeto Nulo ou Inválido");

            if (anime.Id != 0)
                throw new Exception("Id Deve Ser Igual a 0!");

            var _animeNome = await GetByKey(anime.Nome, "nomes");
            if (_animeNome.Count() > 0)
                throw new Exception("Existe Um Anime Com Esse Nome.");

            await _dbContext.AddAsync(anime);
            await _dbContext.SaveChangesAsync();

            return anime;
        }

        public async Task<AnimesModel> Put(AnimesModel anime)
        {
            if (anime == null)
                throw new Exception("Objeto Nulo ou Inválido");

            var _animeId = await GetById(anime.Id);
            if (_animeId == null)
                throw new Exception($"Anime: {anime.Id} Não Encontrado!");

            var _checagemNome = await GetByKey(anime.Nome, "nomes");
            if (_checagemNome.Count() > 0)
            {
                foreach (var checaNomes in _checagemNome)
                {
                    if (_animeId.Id != checaNomes.Id && anime.Nome.Trim().ToLower() == checaNomes.Nome.Trim().ToLower())
                        throw new Exception("Existe Outro Anime Com Esse Nome!");
                }
            }

            _animeId.Diretor = anime.Diretor;
            _animeId.Id = anime.Id;
            _animeId.Resumo = anime.Resumo;
            _animeId.Nome = anime.Nome;

            _dbContext.Animes.Update(_animeId);
            await _dbContext.SaveChangesAsync();

            return _animeId;
        }

        public async Task<string> Delete(int id)
        {
            if (id <= 0)
                throw new Exception("Objeto Nulo ou Inválido!");

            var _animeId = await GetById(id);
            if (_animeId == null)
                throw new Exception($"Anime: {id} Não Encontrado!");

            _dbContext.Animes.Remove(_animeId);
            await _dbContext.SaveChangesAsync();

            return $"Registro: {id} Excluido com Sucesso!";
        }

        public static IQueryable<AnimesModel> FiltrarQuery(string filter, string key, IQueryable<AnimesModel> paginacao)
        {
            var filtroAplicado = paginacao;
            switch (filter.ToLower())
            {
                case "nomes":
                    filtroAplicado = paginacao
                        .Where(a => a.Nome == key)
                        .OrderBy(a => a.Nome);
                    break;
                case "diretor":
                    filtroAplicado = paginacao
                        .Where(a => a.Diretor == key)
                        .OrderBy(a => a.Diretor);
                    break;
                case "resumo":
                    filtroAplicado = paginacao
                        .Where(a => a.Resumo.Contains(key))
                        .OrderBy(a => a.Resumo);
                    break;
                case "todos":
                default:
                    filtroAplicado = paginacao
                        .Where(a => a.Nome == key || a.Diretor == key || a.Resumo.Contains(key))
                        .OrderBy(a => a.Id);
                    break;
            }

            return filtroAplicado;
        }
    }
}
