using API_Animes_Pro.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Animes_Pro.Data.Map
{
    public class LogSistemaMap : IEntityTypeConfiguration<LogSistemaModel>
    {
        public void Configure(EntityTypeBuilder<LogSistemaModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Retorno).IsRequired();
            builder.Property(x => x.Acao).IsRequired();
            builder.Property(x => x.DataHora);
        }
    }
}
