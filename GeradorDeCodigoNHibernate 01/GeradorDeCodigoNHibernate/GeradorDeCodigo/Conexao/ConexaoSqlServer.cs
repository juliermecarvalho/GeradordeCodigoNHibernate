using System;
using System.Data.SqlClient;
using System.Data;
using System.Data.OracleClient;

namespace GeradorDeCodigo
{
    public class ConexaoSqlServer : IConexao
    {

        static SqlConnection sqlconnection;
        private static ConexaoSqlServer instace;

        private ConexaoSqlServer()
        {
        }


        public static ConexaoSqlServer GetInstace()
        {
            if (instace == null)
                instace = new ConexaoSqlServer();
            //a string de conexão muda  por isso new tem fica para atualiza a string
            sqlconnection = new SqlConnection(StringConexao.Stringconexao);
            return instace;
        }

        public void Dispose()
        {
            if (sqlconnection.State == ConnectionState.Open)
            {
                sqlconnection.Close();
                Sqlconnection.Dispose();
            }

            GC.SuppressFinalize(this);
        }

        public OracleConnection Oralceconnection
        {
            get { throw new NotImplementedException(); }
        }

        public SqlConnection Sqlconnection
        {
            get
            {
                if (sqlconnection.State == ConnectionState.Closed)
                {
                    try
                    {

                        sqlconnection.Open();
                    }
                    catch
                    {
                        throw;
                    }
                }
                return sqlconnection;
            }

        }

        public bool TesteConexao()
        {
            if (Sqlconnection.State == ConnectionState.Open)
            {
                return true;
            }
            return false;
        }


    }
}
