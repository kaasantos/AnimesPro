using API_Animes_Pro.Models;
using System.ComponentModel;

namespace API_Animes_Pro.Repository.Interfaces
{
    public interface IAnimesService
    {
        Task<List<AnimesModel>> ListaTodosAnimes();
        Task<AnimesModel> ListaAnimePorId(int id);
        Task<List<AnimesModel>> ListaAnimePorChave(string key, string filter);
        Task<List<AnimesModel>> Paginacao(int page, int pageSize, string key = "", string filter = "Default");
        Task<AnimesModel> AdicionarAnime(AnimesModel anime);
        Task<AnimesModel> AtualizarAnimes(AnimesModel anime);
        Task<string> DeletarAnime(int id);
    }
}
