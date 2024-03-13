using API_Animes_Pro.Controllers;
using API_Animes_Pro.Repository.Interfaces;
using Moq;

namespace API_Animes_Pro.Testes.Services
{
    public class LogSistemaServiceTestes
    {
        LogSistemaService _logSistemaService;
        Mock<ILogSistemaRepository> _mockLogSistemaRepository;

        public LogSistemaServiceTestes()
        {
            _mockLogSistemaRepository = new Mock<ILogSistemaRepository>();
            _logSistemaService = new LogSistemaService(_mockLogSistemaRepository.Object);
        }

        [Fact]
        public void RecebeLogPorIntervaloDeHorario_HorarioFinalMaiorQueInicial()
        {
            var consulta = _logSistemaService.RecebeLogPorIntervaloDeHorario(DateTime.Now.AddSeconds(1), DateTime.Now);

            var exception = Assert.ThrowsAsync<Exception>(() => consulta);
            Assert.Equal("Data inicial maior que a data final.", exception.Result.Message);
        }
    }
}
