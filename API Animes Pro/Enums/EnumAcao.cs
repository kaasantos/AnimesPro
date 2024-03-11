using System.ComponentModel;

namespace API_Animes_Pro.Enums
{
    public enum EnumAcao : short
    {
        [Description("Receber Todos Registros")]
        GetAll = 1,
        [Description("Receber Um Registro Específico por id")]
        GetById = 2,
        [Description("Receber Um Registro Específico por Uma Chave")]
        GetByKey = 3,
        [Description("Adicionar Um Registro")]
        Add = 4,
        [Description("Atualizar Um Registro")]
        Update = 5,
        [Description("Remover Um Registro")]
        Delete = 6,
        [Description("Paginação de Uma Tabela")]
        Pagination = 7,
        [Description("Receber um Registro entre Horarios")]
        GetByHours = 8
    }
}
