using FluentMigrator.Runner;

namespace DataBaseConfig
{
    public static class MigrationsDataBase
    {
        public static void RunMigration(string conexao)
        {
            var serviceProvider = CreateServices(conexao);
            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }

        private static IServiceProvider CreateServices(string conexao)
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer()
                    .WithGlobalConnectionString(conexao)
                    .ScanIn(typeof(Migrations._000001_CreateTables).Assembly)
                    .For.Migrations()
                    .For.EmbeddedResources()
                    )
                    .AddLogging(lb => lb.AddFluentMigratorConsole())
                    .BuildServiceProvider(false);
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}
