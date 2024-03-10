namespace API_Animes_Pro.Models
{
    public class AnimesModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Resumo { get; set; } = string.Empty;
        public string? Diretor { get; set; }
    }
}
