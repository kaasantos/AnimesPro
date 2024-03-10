using API_Animes_Pro.Enums;

namespace API_Animes_Pro.Models
{
    public class LogSistemaModel
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; } = DateTime.Now;
        public EnumAcao Acao { get; set; }
        public string Retorno { get; set; } = string.Empty;
        public string FiltrosLog { get; set;} = string.Empty;
    }
}
