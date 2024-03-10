using System.Data.Common;
using System.Data.SqlClient;

namespace DAP
{
    public class SqlDapper
    {
        private SqlConnection conn;
        private readonly string _conexao;

        public SqlDapper(string connectionString)
        {
            _conexao = connectionString;
        }

        public DbConnection GetConection()
        {
            conn = new SqlConnection(_conexao);
            conn.Open();           
            return conn;
        }

        public void FecharConexao()
        {
            conn.Close();
        }
    }
}
