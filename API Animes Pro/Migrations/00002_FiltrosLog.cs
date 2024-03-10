namespace Migrations
{
    [FluentMigrator.Migration(00002)]
    public class _000002_FiltrosLog: FluentMigrator.Migration
    {
        public override void Down()
        {
        }

        public override void Up()
        {
            Execute.Sql("ALTER TABLE LogSistema ADD filtrosLog varchar(100) NULL");
        }
    }
}
