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
                .OrderBy(a => a.Id)
                .ToListAsync();
            if(animes == null)
                throw new Exception("Lista de Animes não Encontrada!");
            
            return animes;
        }

        public async Task<AnimesModel> GetById(int id)
        {
            if (id <= 0)
                throw new Exception("Objeto Nulo ou Inválido!");

            var anime = await _dbContext.Animes
                .OrderBy(a => a.Id)
                .FirstOrDefaultAsync(a => a.Id == id);
            if (anime == null)
                throw new Exception($"Anime: {id} não encontrado!");
            
            return anime;
        }

        public Task<List<AnimesModel>> Pagination(int page, int pageSize, string filter = "", string keyFilter = "Default")
        {
            throw new NotImplementedException();
        }

        public async Task<AnimesModel> Add(AnimesModel anime)
        {
            if (anime == null)
                throw new Exception("Objeto Nulo ou Inválido");

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
                throw new Exception($"Anime: {anime.Id} não Encontrado!");

            _animeId.Diretor = anime.Diretor;
            _animeId.Id = anime.Id;
            _animeId.Resumo = anime.Resumo;

            _dbContext.Animes.Update(_animeId);
            await _dbContext.SaveChangesAsync();

            return _animeId;
        }
        
        public async Task<bool> Delete(int id)
        {
            if (id <= 0)
                throw new Exception("Objeto Nulo ou Inválido!");
       
            var _animeId = await GetById(id);
            if (_animeId == null)
                throw new Exception($"Anime: {id} não encontrado!");

            _dbContext.Animes.Remove(_animeId);
            await _dbContext.SaveChangesAsync();

            return true;
        }

    }
}
