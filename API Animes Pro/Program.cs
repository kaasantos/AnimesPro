using API_Animes_Pro.Data;
using API_Animes_Pro.Repository;
using API_Animes_Pro.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API_Animes_Pro
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var connection = builder.Configuration.GetConnectionString("DataBase");
            DataBaseConfig.CheckDatabase.DatabaseExist(connection);
            DataBaseConfig.MigrationsDataBase.RunMigration(connection);
            builder.Services.AddEntityFrameworkSqlServer().AddDbContext<DBContext>(
                options => options.UseSqlServer(connection)
                ); ;

            builder.Services.AddScoped<IAnimesRepository, AnimesRepository>();
            builder.Services.AddScoped<ILogSistemaRepository, LogSistemaRepository>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}