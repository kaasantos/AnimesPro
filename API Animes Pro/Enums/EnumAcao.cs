using System.ComponentModel;

namespace API_Animes_Pro.Enums
{
    public enum EnumAcao
    {
        [Description("Receber Todos Registros")]
        GetAll = 1,
        [Description("Receber Um Registro Específico")]
        GetById = 2,
        [Description("Adicionar Um Registro")]
        Add = 3,
        [Description("Atualizar Um Registro")]
        Update = 4,
        [Description("Remover Um Registro")]
        Delete = 5,
        [Description("Paginação de Uma Tabela")]
        Pagination = 6
    }
}
