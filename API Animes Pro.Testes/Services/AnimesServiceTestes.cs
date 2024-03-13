using API_Animes_Pro.Controllers;
using API_Animes_Pro.Models;
using API_Animes_Pro.Repository.Interfaces;
using Moq;

namespace API_Animes_Pro.Testes.Services
{
    public class AnimesServiceTestes
    {
        AnimesService _animeService;
        Mock<IAnimesRepository> _mockAnimeRepository;
        Mock<ILogSistemaRepository> _mockLogRepository;

        public AnimesServiceTestes()
        {
            _mockAnimeRepository = new Mock<IAnimesRepository>();
            _mockLogRepository = new Mock<ILogSistemaRepository>();
            _animeService = new AnimesService(_mockAnimeRepository.Object, _mockLogRepository.Object);
        }

        [Fact]
        public void ListaAnimePorId_IdZero()
        {
            var exception = Assert.ThrowsAsync<Exception>(() => _animeService.ListaAnimePorId(0));
            Assert.Equal("Objeto nulo ou inválido.", exception.Result.Message);
        }

        [Fact]
        public void ListaAnimePorId_RegistroNaoEncontrado()
        {
            var exception = Assert.ThrowsAsync<Exception>(() => _animeService.ListaAnimePorId(999999));
            Assert.Equal("Anime: 999999 não encontrado.", exception.Result.Message);
        }

        [Fact]
        public void ListaAnimePorChave_EntradaNula()
        {
            var chaveNula = Assert.ThrowsAsync<Exception>(() => _animeService.ListaAnimePorChave("teste", string.Empty));
            var filtroNulo = Assert.ThrowsAsync<Exception>(() => _animeService.ListaAnimePorChave(string.Empty, "teste"));
            var ambosNulos = Assert.ThrowsAsync<Exception>(() => _animeService.ListaAnimePorChave(string.Empty, string.Empty));
            var retorno = "Chave ou filtro nulo.";

            Assert.Equal(retorno, chaveNula.Result.Message);
            Assert.Equal(retorno, filtroNulo.Result.Message);
            Assert.Equal(retorno, ambosNulos.Result.Message);
        }

        [Fact]
        public void Paginacao_PaginasRegistrosNulos()
        {
            var paginaZero = Assert.ThrowsAsync<Exception>(() => _animeService.Paginacao(0, 1));
            var registroZero = Assert.ThrowsAsync<Exception>(() => _animeService.Paginacao(1, 0));
            var ambosZerados = Assert.ThrowsAsync<Exception>(() => _animeService.Paginacao(0, 0));
            var retorno = "Pagina e registro por páginas devem ser maior que 0.";

            Assert.Equal(retorno, paginaZero.Result.Message);
            Assert.Equal(retorno, registroZero.Result.Message);
            Assert.Equal(retorno, ambosZerados.Result.Message);
        }

        [Fact]
        public void Paginacao_LimiteDeRegistros()
        {
            var exception = Assert.ThrowsAsync<Exception>(() => _animeService.Paginacao(1, 1001));

            Assert.Equal("Limite de dados por página excedido.", exception.Result.Message);
        }

        [Fact]
        public void AdicionarAnime_IdZero()
        {
            var exception = Assert.ThrowsAsync<Exception>(() => _animeService.AdicionarAnime(new AnimesModel() { Id = 1 }));

            Assert.Equal("Id obrigatóriamente deve ser igual a 0.", exception.Result.Message);
        }

        [Fact]
        public void AdicionarAnime_NomeNaoExiste()
        {
            var exception = Assert.ThrowsAsync<Exception>(() => _animeService.AdicionarAnime(new AnimesModel { Id = 0, Diretor = "teste" }));

            Assert.Equal("Nome do anime é obrigatório.", exception.Result.Message);
        }

        [Fact]
        public void AdicionarAnime_DiretorNaoExiste()
        {
            var exception = Assert.ThrowsAsync<Exception>(() => _animeService.AdicionarAnime(new AnimesModel { Id = 0, Nome = "teste" }));

            Assert.Equal("Nome do diretor é obrigatório.", exception.Result.Message);
        }

        [Fact]
        public void AtualizarAnimes_IdZero()
        {
            var exception = Assert.ThrowsAsync<Exception>(() => _animeService.AtualizarAnimes(new AnimesModel { Id = 0 }));

            Assert.Equal("Id do anime deve ser maior que 0.", exception.Result.Message);
        }

        [Fact]
        public void AtualizarAnimes_NomeNaoExiste()
        {
            var exception = Assert.ThrowsAsync<Exception>(() => _animeService.AtualizarAnimes(new AnimesModel { Id = 1, Diretor = "teste" }));

            Assert.Equal("Nome do anime é obrigatório.", exception.Result.Message);
        }

        [Fact]
        public void AtualizarAnimes_DiretorNaoExiste()
        {
            var exception = Assert.ThrowsAsync<Exception>(() => _animeService.AtualizarAnimes(new AnimesModel { Id = 1, Nome = "teste" }));

            Assert.Equal("Nome do diretor é obrigatório.", exception.Result.Message);
        }

        [Fact]
        public void AtualizarAnimes_AnimeNaoExistente()
        {
            var exception = Assert.ThrowsAsync<Exception>(() => _animeService.AtualizarAnimes(new AnimesModel { Id = 999999, Nome = "teste", Diretor = "teste" }));

            Assert.Equal("Anime: 999999 não encontrado.", exception.Result.Message);
        }


        [Fact]
        public void DeletarAnime_AnimeZerado()
        {
            var exception = Assert.ThrowsAsync<Exception>(() => _animeService.DeletarAnime(0));

            Assert.Equal("Objeto nulo ou inválido.", exception.Result.Message);
        }

        [Fact]
        public void DeletarAnime_AnimeNaoExiste()
        {
            var exception = Assert.ThrowsAsync<Exception>(() => _animeService.DeletarAnime(999999));

            Assert.Equal("Anime: 999999 não encontrado.", exception.Result.Message);
        }
    }
}
