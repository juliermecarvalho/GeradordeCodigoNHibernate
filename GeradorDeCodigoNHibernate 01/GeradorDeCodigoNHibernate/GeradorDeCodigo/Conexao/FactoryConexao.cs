using GeradorDeCodigo.Enum;

namespace GeradorDeCodigo
{
    public class FactoryConexao
    {
        public static IConexao GetConexao(TiposDeBancoDeDados bd)
        {

            if (TiposDeBancoDeDados.ORACLE == bd)
            {
                return ConexaoOracle.GetInstace();
            }
            else
                if (TiposDeBancoDeDados.SQLSERVER == bd)
                {
                    return ConexaoSqlServer.GetInstace();
                }
                else
                    return null;
            

        }

    }
}
