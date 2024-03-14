using API_Animes_Pro.Controllers;
using API_Animes_Pro.Data;
using API_Animes_Pro.Repository;
using API_Animes_Pro.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace API_Animes_Pro
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(C =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                C.IncludeXmlComments(xmlPath);

                C.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API Animes Pro",
                    Version = "v1",
                    Description = "API para gerenciamento de animes. " +
                        "Ferramentas utilizadas: NET versão 7.0, Visual Studio 2022 e SQL Server 2022.",
                    Contact = new OpenApiContact
                    {
                        Name = "Kaio Éverton",
                        Email = "kaioufs@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/kaio-%C3%A9verton-0325a413a/")
                    }
                });
            });
       
            var connection = builder.Configuration.GetConnectionString("DataBase");
            DataBaseConfig.CheckDatabase.DatabaseExist(connection);
            DataBaseConfig.MigrationsDataBase.RunMigration(connection);
            builder.Services.AddEntityFrameworkSqlServer().AddDbContext<DBContext>(
                options => options.UseSqlServer(connection)
                );

            builder.Services.AddTransient<IAnimesRepository, AnimesRepository>();
            builder.Services.AddTransient<ILogSistemaRepository, LogSistemaRepository>();

            builder.Services.AddScoped<IAnimesService, AnimesService>();
            builder.Services.AddScoped<ILogSistemaService, LogSistemaService>();

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