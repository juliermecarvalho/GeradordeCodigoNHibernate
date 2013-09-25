using System;
using System.Data.SqlClient;
using Oracle.DataAccess.Client;

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
