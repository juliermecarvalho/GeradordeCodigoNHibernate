using System.Data.SqlClient;
using System.Data.OracleClient;


namespace GeradorDeCodigo
{
    public interface IConexao
    {
        
        void Dispose();
        OracleConnection Oralceconnection { get; }
        SqlConnection Sqlconnection { get; }
        bool TesteConexao();
    }
}
