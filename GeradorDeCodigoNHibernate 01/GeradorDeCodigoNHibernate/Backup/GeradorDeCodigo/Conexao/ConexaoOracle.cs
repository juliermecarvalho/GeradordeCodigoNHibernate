using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using System.Data.SqlClient;

namespace GeradorDeCodigo
{
    public class ConexaoOracle : IConexao
    {
        OracleConnection oraconnection = new OracleConnection(StringConexao.Stringconexao);
        private static ConexaoOracle instace;

        private ConexaoOracle()
        {
        }

        public static ConexaoOracle GetInstace()
        {
            if (instace == null)
                instace = new ConexaoOracle();
            return instace;
        }

        public void Dispose()
        {
            if (oraconnection.State == ConnectionState.Open)
            {
                oraconnection.Close();
                Oralceconnection.Dispose();
            }

            GC.SuppressFinalize(this);
        }

        public SqlConnection Sqlconnection
        {
            get { throw new NotImplementedException(); }
        }

        public OracleConnection Oralceconnection
        {
            get
            {
                if (oraconnection.State == ConnectionState.Closed)
                {
                    try
                    {

                        oraconnection.Open();
                    }
                    catch
                    {
                        throw;
                    }
                }
                return oraconnection;
            }

        }

        public bool TesteConexao()
        {
            if (Oralceconnection.State == ConnectionState.Open)
            {
                return true;
            }
            return false;
        }




    }
}
