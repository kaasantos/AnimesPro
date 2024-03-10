using API_Animes_Pro.Data.Map;
using API_Animes_Pro.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Animes_Pro.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) 
        { 
        }

        public DbSet<AnimesModel> Animes { get; set; }
        public DbSet<LogSistemaModel> LogSistema { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AnimeMap());
            modelBuilder.ApplyConfiguration(new LogSistemaMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
