using API_Animes_Pro.Enums;
using API_Animes_Pro.Models;

namespace API_Animes_Pro.Repository.Interfaces
{
    public interface ILogSistemaRepository
    {
        Task<List<LogSistemaModel>> GetAll();
        Task<List<LogSistemaModel>> ChecarLogPorData(DateTime dataInicial, DateTime dataFinal);
        Task<LogSistemaModel> FazLog(EnumAcao acao, string retorno, string filtros = "");
    }
}
