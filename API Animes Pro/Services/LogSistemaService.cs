using API_Animes_Pro.Models;
using API_Animes_Pro.Repository.Interfaces;

namespace API_Animes_Pro.Controllers
{
    public class LogSistemaService : ILogSistemaService
    {
        private readonly ILogSistemaRepository _logSistemaRepository;

        public LogSistemaService(ILogSistemaRepository logSistemaRepository)
        {
            _logSistemaRepository = logSistemaRepository;
        }

        public async Task<List<LogSistemaModel>> RecebeTodosLogs()
        {
            try
            {
                var listaTodosLogs = await _logSistemaRepository.GetAll();

                if(listaTodosLogs == null)
                    throw new Exception("Lista de logs não encontrada.");

                await _logSistemaRepository.AddLog(Enums.EnumAcao.GetAll, "Consulta ao log executada com sucesso!");
                return listaTodosLogs;
            }
            catch(Exception ex)
            {
                await _logSistemaRepository.AddLog(Enums.EnumAcao.GetAll, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<LogSistemaModel>> RecebeLogPorIntervaloDeHorario(DateTime dataInicial, DateTime dataFinal)
        {
            try
            {
                if(dataFinal < dataInicial)
                    throw new Exception("Data inicial maior que a data final.");

                var logsNoIntervalo = await _logSistemaRepository.GetByInterval(dataInicial, dataFinal);

                await _logSistemaRepository.AddLog(Enums.EnumAcao.GetByHours, "Consulta ao log executada com sucesso!", 
                    $"Data Inicial: {dataInicial}, Data Final: {dataFinal}");
                return logsNoIntervalo;
            }
            catch(Exception ex)
            {
                await _logSistemaRepository.AddLog(Enums.EnumAcao.GetByHours, ex.Message, $"Data Inicial: {dataInicial}, Data Final: {dataFinal}");
                throw new Exception(ex.Message);
            }
        }
    }
}
