using API_Animes_Pro.Enums;
using API_Animes_Pro.Models;

namespace API_Animes_Pro.Repository.Interfaces
{
    public interface ILogSistemaRepository
    {
        Task<List<LogSistemaModel>> GetAll();
        Task<List<LogSistemaModel>> GetByInterval(DateTime dataInicial, DateTime dataFinal);
        Task<LogSistemaModel> AddLog(EnumAcao acao, string retorno, string filtros = "");
    }
}
