using GeradorDeCodigo.Enum;

namespace GeradorDeCodigo.MetaDados
{
    public class TabelaECamposFactoy
    {
       
        public static ITabelasECampos GetTabelasECampos(TiposDeBancoDeDados tipodebancodedados)
        {
            


            if (tipodebancodedados == TiposDeBancoDeDados.SQLSERVER)
            {
                return new TabelasECamposSqlServer(TiposDeBancoDeDados.SQLSERVER);
            }
            else
                if (tipodebancodedados == TiposDeBancoDeDados.ORACLE)
                {
                    return new TabelasECamposOralcer(TiposDeBancoDeDados.ORACLE);
                }
                else
                {
                    return null;
                }

        }
    }
}
