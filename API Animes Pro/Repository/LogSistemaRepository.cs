using API_Animes_Pro.Data;
using API_Animes_Pro.Enums;
using API_Animes_Pro.Models;
using API_Animes_Pro.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API_Animes_Pro.Repository
{
    public class LogSistemaRepository : ILogSistemaRepository
    {
        private readonly DBContext _dbContext;
        public LogSistemaRepository(DBContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<List<LogSistemaModel>> GetAll()
        {
            var log = await _dbContext.LogSistema
                .OrderBy(ls => ls.Id)
                .ToListAsync();

            return log;
        }

        public async Task<List<LogSistemaModel>> ChecarLogPorData(DateTime dataInicial, DateTime dataFinal)
        {
            var log = await _dbContext.LogSistema
                .Where(ls => ls.DataHora >= dataInicial && ls.DataHora <= dataFinal)
                .OrderBy(ls => ls.Id)
                .ToListAsync();

            return log;
        }

        public async Task<LogSistemaModel> FazLog(EnumAcao acao, string retorno)
        {
            var _log = new LogSistemaModel(){
                Acao = acao,
                Retorno = retorno
            };

            await _dbContext.LogSistema.AddAsync(_log);
            await _dbContext.SaveChangesAsync();

            return _log;
        }
    }
}
