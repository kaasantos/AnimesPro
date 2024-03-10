using API_Animes_Pro.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Animes_Pro.Data.Map
{
    public class AnimeMap : IEntityTypeConfiguration<AnimesModel>
    {
        public void Configure(EntityTypeBuilder<AnimesModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Diretor).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Resumo);
        }
    }
}
