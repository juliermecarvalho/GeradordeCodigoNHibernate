using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.SqlClient;
using GeradorDeCodigo.Enum;

namespace GeradorDeCodigo.MetaDados
{

    public class TabelasECamposSqlServer : ITabelasECampos
    {

        private TiposDeBancoDeDados BD;

        public TabelasECamposSqlServer(TiposDeBancoDeDados BD)
        {
            this.BD = BD;
        }



        /// <summary>
        /// Verifica se existe a tabela no banco de dados.
        /// </summary>
        /// <param name="NomeDaTabela"></param>
        /// <returns></returns>
        public bool VerificaSeExisteTabela(string tabela)
        {
            string SQL = @"SELECT  SO.NAME AS TABELA
                            FROM SYSOBJECTS SO
                                LEFT JOIN SYSCOLUMNS SC ON SO.ID = SC.ID
                                LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CPK ON CPK.TABLE_NAME = SO.NAME AND CPK.COLUMN_NAME = SC.NAME
                            WHERE SO.XTYPE = 'U' AND SO.NAME <> 'DTPROPERTIES'
                                AND SO.NAME = @NAME";
            IConexao conexao = FactoryConexao.GetConexao(BD);
            using (var select = new SqlCommand(SQL, conexao.Sqlconnection))
            {
                select.Parameters.AddWithValue("@NAME", tabela);
                return select.ExecuteScalar() != null;

            }

        }


        /// <summary>
        /// Retorna todas as tabela.
        /// </summary>
        /// <param name="NomeDaTabela"></param>
        /// <returns></returns>
        public List<string> TabelasDoBanco()
        {

            string SQL = @"SELECT  DISTINCT(SO.NAME) AS TABELA	
                                        FROM SYSOBJECTS SO
                                            LEFT JOIN SYSCOLUMNS SC ON SO.ID = SC.ID
                                        WHERE SO.XTYPE = 'U' 
                                          AND SO.NAME <> 'DTPROPERTIES'
                                          AND SO.NAME <> 'SYSDIAGRAMS'";
            IConexao conexao = FactoryConexao.GetConexao(BD);
            using (var select = new SqlCommand(SQL, conexao.Sqlconnection))
            {
                List<string> CamposDaTabela = new List<string>();
                var read = select.ExecuteReader();

                while (read.Read())
                {
                    CamposDaTabela.Add(read["TABELA"].ToString().ToUpper());
                }
                read.Close();
                conexao.Sqlconnection.Close();
                return CamposDaTabela;
            }

        }

        public string TamanhodoCamposString(string tabela, string campo)
        {
            if (!string.IsNullOrEmpty(tabela) && !string.IsNullOrEmpty(campo))
            {
                var toReturn = string.Empty;
                string SQL = @"SELECT  DISTINCT(SC.NAME) AS CAMPO, SC.LENGTH
	                                FROM SYSOBJECTS SO
		                                LEFT JOIN SYSCOLUMNS SC ON SO.ID = SC.ID
		                                LEFT JOIN SYSTYPES ST ON SC.XTYPE = ST.XTYPE
		                                LEFT JOIN SYSCOMMENTS SM ON SC.CDEFAULT = SM.ID
		                                LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CPK ON CPK.TABLE_NAME = SO.NAME AND CPK.COLUMN_NAME = SC.NAME
	                                WHERE SO.XTYPE = 'U' AND SO.NAME <> 'DTPROPERTIES'
									AND SO.NAME = @COLUMN_NAME 
									AND SC.NAME = @TABLE_NAME";
                IConexao conexao = FactoryConexao.GetConexao(BD);
                using (var select = new SqlCommand(SQL, conexao.Sqlconnection))
                {
                    select.Parameters.AddWithValue("@COLUMN_NAME", tabela);
                    select.Parameters.AddWithValue("@TABLE_NAME", campo);
                    var read = select.ExecuteReader();
                    while (read.Read())
                    {
                        toReturn = read["LENGTH"].ToString();
                    }
                    conexao.Sqlconnection.Close();
                    return toReturn;

                }

            }
            else
            {
                MessageBox.Show(Messagen.IMFORMARTABELAECAMPO, Messagen.ATENCAO, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return string.Empty;
            }

        }
        /// <summary>
        /// Retorna os campos da tabela.
        /// </summary>
        /// <param name="NomeDaTabela"></param>
        /// <returns></returns>
        public List<string> CamposDaTabela(string tabela)
        {
            if (!string.IsNullOrEmpty(tabela))
            {


                string SQL = @"SELECT  DISTINCT(SC.NAME) AS CAMPO, SC.COLID
	                                FROM SYSOBJECTS SO
		                                LEFT JOIN SYSCOLUMNS SC ON SO.ID = SC.ID
		                                LEFT JOIN SYSTYPES ST ON SC.XTYPE = ST.XTYPE
		                                LEFT JOIN SYSCOMMENTS SM ON SC.CDEFAULT = SM.ID
		                                LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CPK ON CPK.TABLE_NAME = SO.NAME AND CPK.COLUMN_NAME = SC.NAME
	                                WHERE SO.XTYPE = 'U' AND SO.NAME <> 'DTPROPERTIES'
	                                  AND SO.NAME = @NAME
                                      ORDER BY SC.COLID";
                IConexao conexao = FactoryConexao.GetConexao(BD);
                using (var select = new SqlCommand(SQL, conexao.Sqlconnection))
                {
                    select.Parameters.AddWithValue("@NAME", tabela);
                    List<string> CamposDaTabela = new List<string>();
                    var read = select.ExecuteReader();
                    string teste = string.Empty;
                    while (read.Read())
                    {
                        CamposDaTabela.Add(read["CAMPO"].ToString().ToUpper());
                    }
                    read.Close();
                    conexao.Sqlconnection.Close();
                    return CamposDaTabela;
                }
            }
            else
            {
                MessageBox.Show(Messagen.IMFORMARTABELAECAMPO, Messagen.ATENCAO, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

        }

        /// <summary>
        /// Retorna os campos Chaves da tabela.
        /// </summary>
        /// <param name="NomeDaTabela"></param>
        /// <returns></returns>
        public List<string> CamposChavesDaTabela(string tabela)
        {
            if (!string.IsNullOrEmpty(tabela))
            {
                string SQL = @"SELECT   SC.NAME AS CAMPO
	                                FROM SYSOBJECTS SO
		                                LEFT JOIN SYSCOLUMNS SC ON SO.ID = SC.ID
		                                LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CPK ON CPK.TABLE_NAME = SO.NAME AND CPK.COLUMN_NAME = SC.NAME
	                                WHERE SO.XTYPE = 'U' 
		                                AND CPK.TABLE_NAME = @TABLE_NAME
		                                AND (SELECT OBJECTPROPERTY(OBJECT_ID(CPK.CONSTRAINT_NAME), 'ISPRIMARYKEY')) = 1";
                IConexao conexao = FactoryConexao.GetConexao(BD);
                using (var select = new SqlCommand(SQL, conexao.Sqlconnection))
                {
                    select.Parameters.AddWithValue("@TABLE_NAME", tabela);
                    List<string> CamposDaTabela = new List<string>();
                    var read = select.ExecuteReader();

                    while (read.Read())
                    {
                        CamposDaTabela.Add(read["CAMPO"].ToString().ToUpper());
                    }
                    read.Close();
                    conexao.Sqlconnection.Close();
                    return CamposDaTabela;
                }
            }
            else
            {
                MessageBox.Show(Messagen.IMFORMARTABELAECAMPO, Messagen.ATENCAO, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

        }

        public bool CampoChavesDaTabela(string tabela, string campo)
        {
            if (!string.IsNullOrEmpty(tabela) && !string.IsNullOrEmpty(campo))
            {

                string SQL = @"SELECT   SC.NAME AS CAMPO
	                                FROM SYSOBJECTS SO
		                                LEFT JOIN SYSCOLUMNS SC ON SO.ID = SC.ID
		                                LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CPK ON CPK.TABLE_NAME = SO.NAME AND CPK.COLUMN_NAME = SC.NAME
	                                WHERE SO.XTYPE = 'U' 
		                                AND CPK.TABLE_NAME = @TABLE_NAME
										AND SC.NAME = @CAMPO
		                                AND (SELECT OBJECTPROPERTY(OBJECT_ID(CPK.CONSTRAINT_NAME), 'ISPRIMARYKEY')) = 1";
                IConexao conexao = FactoryConexao.GetConexao(BD);
                using (var select = new SqlCommand(SQL, conexao.Sqlconnection))
                {
                    select.Parameters.AddWithValue("@TABLE_NAME", tabela);
                    select.Parameters.AddWithValue("@CAMPO", campo);
                    var read = select.ExecuteReader();
                    bool toReturn = false;
                    while (read.Read())
                    {
                        toReturn = (read["CAMPO"].ToString().ToUpper()) != string.Empty;
                    }
                    read.Close();
                    conexao.Sqlconnection.Close();
                    return toReturn;
                }

            }
            else
            {
                MessageBox.Show(Messagen.IMFORMARTABELAECAMPO, Messagen.ATENCAO, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

        }

        public string DialectDoBancodeDados()
        {
            string SQL = @"SELECT @@VERSION";
            IConexao conexao = FactoryConexao.GetConexao(BD);
            using (var select = new SqlCommand(SQL, conexao.Sqlconnection))
            {
                var read = select.ExecuteReader();
                string toReturn = string.Empty;
                while (read.Read())
                {
                    toReturn = (read[0].ToString().ToUpper());
                }
                read.Close();
                conexao.Sqlconnection.Close();

                if (toReturn.Substring(22, 4) == "2000")
                {
                    toReturn = "2000";
                }
                else
                    if (toReturn.Substring(21, 4) == "2005")
                    {
                        toReturn = "2005";
                    }
                    else
                        if (toReturn.Substring(21, 4) == "2008")
                        {
                            toReturn = "2008";
                        }
                        else
                        {
                            toReturn = "2005";
                        }

                toReturn = "NHibernate.Dialect.MsSql" + toReturn + "Dialect";
                return toReturn;
            }

        }

        public string ConnectionDriver()
        {
            return "NHibernate.Driver.SqlClientDriver";
        }



        public string CampoChavesDaTabela(string tabela)
        {

            if (!string.IsNullOrEmpty(tabela))
            {

                string SQL = @"SELECT   SC.NAME AS CAMPO
	                                FROM SYSOBJECTS SO
		                                LEFT JOIN SYSCOLUMNS SC ON SO.ID = SC.ID
		                                LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CPK ON CPK.TABLE_NAME = SO.NAME AND CPK.COLUMN_NAME = SC.NAME
	                                WHERE SO.XTYPE = 'U' 
		                                AND CPK.TABLE_NAME = @TABLE_NAME
		                                AND (SELECT OBJECTPROPERTY(OBJECT_ID(CPK.CONSTRAINT_NAME), 'ISPRIMARYKEY')) = 1";
                IConexao conexao = FactoryConexao.GetConexao(BD);
                using (var select = new SqlCommand(SQL, conexao.Sqlconnection))
                {
                    select.Parameters.AddWithValue("@TABLE_NAME", tabela);
                    var read = select.ExecuteReader();
                    string toReturn = string.Empty;
                    while (read.Read())
                    {
                        toReturn = (read["CAMPO"].ToString().ToUpper());
                    }
                    read.Close();
                    conexao.Sqlconnection.Close();
                    return toReturn;
                }

            }
            else
            {
                MessageBox.Show(Messagen.IMFORMARTABELAECAMPO, Messagen.ATENCAO, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

        }

        public bool CampoOBrigatorio(string tabela, string campo)
        {
            if (!string.IsNullOrEmpty(tabela) && !string.IsNullOrEmpty(campo))
            {
                var toReturn = false;
                string SQL = @"SELECT  SC.ISNULLABLE
	                                FROM SYSOBJECTS SO
		                                LEFT JOIN SYSCOLUMNS SC ON SO.ID = SC.ID
		                                LEFT JOIN SYSTYPES ST ON SC.XTYPE = ST.XTYPE
		                                LEFT JOIN SYSCOMMENTS SM ON SC.CDEFAULT = SM.ID
		                                LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CPK ON CPK.TABLE_NAME = SO.NAME AND CPK.COLUMN_NAME = SC.NAME
	                                WHERE SO.XTYPE = 'U' AND SO.NAME <> 'DTPROPERTIES'
	                                  AND SO.NAME = @TABLE_NAME
									  AND SC.NAME = @COLUMN_NAME";
                IConexao conexao = FactoryConexao.GetConexao(BD);
                using (var select = new SqlCommand(SQL, conexao.Sqlconnection))
                {
                    select.Parameters.AddWithValue("@COLUMN_NAME", campo);
                    select.Parameters.AddWithValue("@TABLE_NAME", tabela);
                    var read = select.ExecuteReader();
                    while (read.Read())
                    {
                        toReturn = int.Parse(read["ISNULLABLE"].ToString()) == 0;
                    }
                    conexao.Sqlconnection.Close();
                    return toReturn;
                }

            }
            else
            {
                MessageBox.Show(Messagen.IMFORMARTABELAECAMPO, Messagen.ATENCAO, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }


        }


        public List<string> TabelasOndeARelacionamento(string tabela)
        {
            if (!string.IsNullOrEmpty(tabela))
            {
                var toReturn = new List<string>();
                string SQL = @"SELECT (SO.NAME)
                                FROM SYSOBJECTS SO
                                    LEFT JOIN SYSCOLUMNS SC ON SO.ID = SC.ID
                                    LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CPK ON CPK.TABLE_NAME = SO.NAME AND CPK.COLUMN_NAME = SC.NAME
                                WHERE SO.XTYPE = 'U' AND SO.NAME <> 'DTPROPERTIES'
                                      AND  (SELECT  
                                             TABLE_PK =(SELECT B.TABLE_NAME FROM INFORMATION_SCHEMA.CONSTRAINT_TABLE_USAGE B 
                                             WHERE B.CONSTRAINT_NAME = A.UNIQUE_CONSTRAINT_NAME)
		                                    FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS A 
                                            WHERE CONSTRAINT_NAME = CPK.CONSTRAINT_NAME) = @TABLE_NAME";
                IConexao conexao = FactoryConexao.GetConexao(BD);
                using (var select = new SqlCommand(SQL, conexao.Sqlconnection))
                {
                    select.Parameters.AddWithValue("@TABLE_NAME", tabela);
                    var read = select.ExecuteReader();
                    while (read.Read())
                    {
                        toReturn.Add(read["NAME"].ToString());
                    }
                    conexao.Sqlconnection.Close();
                    return toReturn;
                }

            }
            else
            {
                MessageBox.Show(Messagen.IMFORMARTABELAECAMPO, Messagen.ATENCAO, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

        }


        public bool CamposIdentityDaTabela(string tabela, string campo)
        {

            if (!string.IsNullOrEmpty(tabela) && !string.IsNullOrEmpty(campo))
            {
                // var toReturn = false;
                string SQL = @"SELECT COLUMN_NAME
                                FROM INFORMATION_SCHEMA.COLUMNS
                                    WHERE COLUMNPROPERTY(OBJECT_ID(TABLE_NAME),COLUMN_NAME,'ISIDENTITY') = 1
                                AND COLUMN_NAME = @COLUMN_NAME 
                                AND TABLE_NAME = @TABLE_NAME";
                IConexao conexao = FactoryConexao.GetConexao(BD);
                using (var select = new SqlCommand(SQL, conexao.Sqlconnection))
                {
                    select.Parameters.AddWithValue("@COLUMN_NAME", campo);
                    select.Parameters.AddWithValue("@TABLE_NAME", tabela);
                    return select.ExecuteScalar() != null;
                    //conexao.Sqlconnection.Close();
                }

            }
            else
            {
                MessageBox.Show(Messagen.IMFORMARTABELAECAMPO, Messagen.ATENCAO, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

        }


        /// <summary>
        /// Retorna os campos Chaves estrageiras da tabela.
        /// </summary>
        /// <param name="NomeDaTabela"></param>
        /// <returns></returns>
        public List<string> CamposChavesEstrageiraDaTabela(string tabela)
        {
            if (!string.IsNullOrEmpty(tabela))
            {
                string SQL = @"SELECT  SC.NAME AS CAMPO
                                FROM SYSOBJECTS SO
                                    LEFT JOIN SYSCOLUMNS SC ON SO.ID = SC.ID
                                    LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CPK ON CPK.TABLE_NAME = SO.NAME AND CPK.COLUMN_NAME = SC.NAME
                                WHERE SO.XTYPE = 'U' AND SO.NAME <> 'DTPROPERTIES'
                                    AND SO.NAME = @NAME
                                    AND (SELECT OBJECTPROPERTY(OBJECT_ID(CPK.CONSTRAINT_NAME), 'ISFOREIGNKEY')) = 1";
                IConexao conexao = FactoryConexao.GetConexao(BD);
                using (var select = new SqlCommand(SQL, conexao.Sqlconnection))
                {
                    select.Parameters.AddWithValue("@NAME", tabela);
                    List<string> CamposDaTabela = new List<string>();
                    var read = select.ExecuteReader();

                    while (read.Read())
                    {
                        CamposDaTabela.Add(read["CAMPO"].ToString().ToUpper());

                    }
                    read.Close();
                    conexao.Sqlconnection.Close();
                    return CamposDaTabela;
                }
            }
            else
            {
                MessageBox.Show(Messagen.IMFORMARTABELAECAMPO, Messagen.ATENCAO, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

        }



        public string NomeDaTabelaDaChaveEstrgeira(string tabela, string campo)
        {
            if (!string.IsNullOrEmpty(campo) && !string.IsNullOrEmpty(tabela))
            {
                string SQL = @"SELECT 	                            
							        CASE 
                                    WHEN (SELECT OBJECTPROPERTY(OBJECT_ID(CPK.CONSTRAINT_NAME), 'ISFOREIGNKEY')) = 1 THEN 
                                    (SELECT  
	                                     TABLE_PK =(SELECT B.TABLE_NAME FROM INFORMATION_SCHEMA.CONSTRAINT_TABLE_USAGE B 
                                         WHERE B.CONSTRAINT_NAME = A.UNIQUE_CONSTRAINT_NAME)
                                     FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS A WHERE CONSTRAINT_NAME = CPK.CONSTRAINT_NAME)

                                ELSE 'NULL' END AS FK_TABELA
                            FROM SYSOBJECTS SO
                                LEFT JOIN SYSCOLUMNS SC ON SO.ID = SC.ID
                                LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CPK ON CPK.TABLE_NAME = SO.NAME AND CPK.COLUMN_NAME = SC.NAME
                            WHERE SO.XTYPE = 'U' AND SO.NAME <> 'DTPROPERTIES'
							    AND SO.NAME = @TABELA
							    AND SC.NAME = @CAMPO";

                IConexao conexao = FactoryConexao.GetConexao(BD);
                using (var select = new SqlCommand(SQL, conexao.Sqlconnection))
                {
                    string TipoDeCampoDaTabela = string.Empty;
                    select.Parameters.AddWithValue("@CAMPO", campo);
                    select.Parameters.AddWithValue("@TABELA", tabela);
                    var read = select.ExecuteReader();
                    read.Read();
                    TipoDeCampoDaTabela = read["FK_TABELA"].ToString().ToUpper();
                    read.Close();
                    conexao.Sqlconnection.Close();
                    return TipoDeCampoDaTabela;
                }

            }
            else
            {
                MessageBox.Show(Messagen.IMFORMARTABELAECAMPO, Messagen.ATENCAO, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return string.Empty;
            }
        }


        /// <summary>
        /// Retorna o tipo do campo de acordo qual é no banco de dados
        /// </summary>
        /// <returns></returns>
        public string TipoDadoBD(string tabela, string campo)
        {
            if (!string.IsNullOrEmpty(campo) && !string.IsNullOrEmpty(tabela))
            {
                string SQL = @"SELECT   ST.NAME AS TIPO
                                FROM SYSOBJECTS SO
	                                LEFT JOIN SYSCOLUMNS SC ON SO.ID = SC.ID
	                                LEFT JOIN SYSTYPES ST ON SC.XTYPE = ST.XTYPE		                                
	                                LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CPK ON CPK.TABLE_NAME = SO.NAME AND CPK.COLUMN_NAME = SC.NAME
                                WHERE SO.XTYPE = 'U' AND SO.NAME <> 'DTPROPERTIES'
						            AND SO.NAME = @TABELA
						            AND SC.NAME = @CAMPO";

                IConexao conexao = FactoryConexao.GetConexao(BD);
                using (var select = new SqlCommand(SQL, conexao.Sqlconnection))
                {
                    select.Parameters.AddWithValue("@CAMPO", campo);
                    select.Parameters.AddWithValue("@TABELA", tabela);
                    string TipoDeCampoDaTabela = string.Empty;
                    var read = select.ExecuteReader();
                    read.Read();
                    TipoDeCampoDaTabela = read["TIPO"].ToString().ToUpper();
                    read.Close();
                    conexao.Sqlconnection.Close();
                    return TipoDeCampoDaTabela;
                }

            }
            else
            {
                MessageBox.Show(Messagen.IMFORMARTABELAECAMPO, Messagen.ATENCAO, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return string.Empty;
            }
        }
        /// <summary>
        /// Retorna o tipo do campo de acordo com c#
        /// </summary>
        /// <returns></returns>
        public string TipoDadoCSharp(string tabela, string campo)
        {
            string TipoDado = this.TipoDadoBD(tabela, campo).ToLower();

            if (TipoDado == "int" || TipoDado == "smallint")
            {
                return "int";
            }
            else
                if (TipoDado == "varchar" || TipoDado == "nvarchar" || TipoDado == "char"
                    || TipoDado == "ntext" || TipoDado == "nchar" || TipoDado == "sysname"
                    || TipoDado == "bit")
                {
                    return "string";
                }
                else
                    if (TipoDado == "datetime" || TipoDado == "smalldatetime")
                    {
                        return "DateTime";
                    }
                    else
                        if (TipoDado == "bigint")
                        {
                            return "long";
                        }
                        else
                            if (TipoDado == "money" || TipoDado == "decimal"
                                || TipoDado == "numeric" || TipoDado == "real"
                                || TipoDado == "smallmoney")
                            {
                                return "decimal";
                            }
                            else
                                if (TipoDado == "float")
                                {
                                    return "double";
                                }
                                else
                                    if (TipoDado == "image" || TipoDado == "timestamp"
                                        || TipoDado == "binary" || TipoDado == "varbinary")
                                    {
                                        return "byte[]";
                                    }
                                    else
                                        if (TipoDado == "bit")
                                        {
                                            return "bool";
                                        }

            return string.Empty;
        }


    }
}
