using API_Animes_Pro.Data;
using API_Animes_Pro.Models;
using API_Animes_Pro.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

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
                .OrderByDescending(a => a.Id)
                .ToListAsync();

            return animes;
        }

        public async Task<AnimesModel?> GetById(int id)
        {
            var anime = await _dbContext.Animes
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
            
            return anime;
        }

        public async Task<List<AnimesModel>> GetByKey(string key, string filter)
        {
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

        public async Task<List<AnimesModel>> Pagination(int page, int pageSize, string key = "", string filter = "")
        {
            var paginacao = _dbContext.Animes
                .Select(a => new AnimesModel
                {
                    Id = a.Id,
                    Nome = a.Nome,
                    Diretor = a.Diretor,
                    Resumo = a.Resumo
                })
                .AsNoTracking();

            paginacao = FiltrarQuery(filter, key, paginacao);

            paginacao = paginacao
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize);

            return await paginacao.ToListAsync();
        }

        public async Task<AnimesModel> Add(AnimesModel anime)
        {
            await _dbContext.AddAsync(anime);
            await _dbContext.SaveChangesAsync();

            return anime;
        }

        public async Task<AnimesModel> Put(AnimesModel anime)
        {
            var _anime = new AnimesModel()
            {
                Id = anime.Id,
                Diretor = anime.Diretor,
                Resumo = anime.Resumo,
                Nome = anime.Nome
            };

            _dbContext.Animes.Update(_anime);
            await _dbContext.SaveChangesAsync();

            return _anime;
        }

        public async Task<string> Delete(AnimesModel anime)
        {
            _dbContext.Animes.Remove(anime);
            await _dbContext.SaveChangesAsync();

            return $"Anime: {anime.Id} excluído com sucesso!";
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
                        .OrderByDescending(a => a.Id);
                    break;
            }

            return filtroAplicado;
        }
    }
}
