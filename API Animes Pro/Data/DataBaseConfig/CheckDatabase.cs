using API_Animes_Pro.Data.DataBaseConfig;
using Dapper;

namespace DataBaseConfig
{
    public class CheckDatabase
    {
        public readonly IConfiguration _configuration;
        public CheckDatabase(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static void DatabaseExist(string connectionString)
        {
            var _dataBaseName = ExtractDataBaseName(connectionString);
            if(string.IsNullOrWhiteSpace(_dataBaseName))
                throw new Exception("Não foi possível identificar o banco de dados;");
            
            var _conexao = ConnectionRemoveDatabaseName(connectionString);
            ChecaSeExisteDataBase(_conexao, _dataBaseName);
        }

        private static void ChecaSeExisteDataBase(string connectionSemBD, string dataBase)
        {
            try
            {
                string sqlCheck = $"if not exists(select * from sys.databases where name='{dataBase}') BEGIN " +
                                   $"exec('Create database {dataBase}') END";

                var dapp = new SqlDapper(connectionSemBD);
                using(var conn = dapp.GetConection())
                {
                    conn.ExecuteScalar(sqlCheck);
                    conn.Dispose();
                }
                dapp.FecharConexao();              
            }
            catch(Exception)
            {
                throw new Exception("Não foi possivel identificar o banco de dados;");
            }
        }

        private static string ExtractDataBaseName(string connectionString)
        {
            string databaseName = "";

            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                var quebra = connectionString.Split(';', StringSplitOptions.RemoveEmptyEntries);
              
                foreach (var parte in quebra)
                {
                    if (parte.Contains("Catalog"))
                        databaseName = parte.Split('=')[1];                    
                }
            }            
            return databaseName;
        }

        private static string ConnectionRemoveDatabaseName(string connectionString)
        {
            string connection = "";

            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                var quebra = connectionString.Split(';');
                foreach (var parte in quebra)
                {
                    if (!string.IsNullOrWhiteSpace(parte))
                    {
                        if (!parte.Contains("Catalog"))
                            connection += parte + ";";
                    }
                    
                }
            }
            return connection;
        }
    }
}
