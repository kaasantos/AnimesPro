namespace Migrations
{
    [FluentMigrator.Migration(00001)]
    public class _000001_CreateTables : FluentMigrator.Migration
    {
        public override void Down()
        {
        }

        public override void Up()
        {
            Execute.Sql("if not exists(select 1 from syscolumns where id = object_id('Animes')) BEGIN " +
                "CREATE TABLE Animes (Id int IDENTITY(1,1) PRIMARY KEY, Nome varchar(255) NOT NULL Default '', " +
                "Diretor varchar(255) NOT NULL default '', Resumo varchar(max) NULL);  END ");
             
            Execute.Sql("if not exists(select 1 from syscolumns where id = object_id('LogSistema')) BEGIN " +
                "CREATE TABLE LogSistema (Id int IDENTITY(1,1) PRIMARY KEY, Retorno varchar(max) NOT NULL Default 0, " +
                "Acao smallint NOT NULL default 0, DataHora datetime);  END");
        }
    }
}
