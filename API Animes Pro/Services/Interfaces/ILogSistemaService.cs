using API_Animes_Pro.Models;

namespace API_Animes_Pro.Repository.Interfaces
{
    public interface ILogSistemaService
    {
        Task<List<LogSistemaModel>> RecebeTodosLogs();
        Task<List<LogSistemaModel>> RecebeLogPorIntervaloDeHorario(DateTime dataInicial, DateTime dataFinal);
    }
}
