using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using GeradorDeCodigo.Enum;
using Oracle.DataAccess.Client;

namespace GeradorDeCodigo.MetaDados
{
    public class TabelasECamposOralcer : ITabelasECampos
    {
        private TiposDeBancoDeDados BD;
        private string owner;

        private string Recuperaowner()
        {
            string owner = StringConexao.Stringconexao;
            owner = owner.Substring(owner.IndexOf("ID") + 3, (owner.Length - 1) - (owner.IndexOf("ID") + 3));
            owner = owner.Substring(0, owner.IndexOf(";"));
            return owner.ToUpper();
        }

        public TabelasECamposOralcer(TiposDeBancoDeDados BD)
        {
            this.BD = BD;
            this.owner = Recuperaowner();
        }
        /// <summary>
        /// Verifica se existe a tabela no banco de dados.
        /// </summary>
        /// <param name="NomeDaTabela"></param>
        /// <returns></returns>
        public bool VerificaSeExisteTabela(string tabela)
        {
            string SQL = @"SELECT OBJECT_NAME
                            FROM ALL_OBJECTS
                            WHERE OBJECT_TYPE = 'TABLE'
                            AND OWNER         = :OWNER
                            AND OBJECT_NAME   = :TABLE_NAME";
            IConexao conexao = FactoryConexao.GetConexao(BD);
            using (var select = new OracleCommand(SQL, conexao.Oralceconnection))
            {
                select.Parameters.Add("OWNER", this.owner);
                select.Parameters.Add("TABLE_NAME", tabela);
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

            string SQL = @"SELECT OBJECT_NAME
                            FROM ALL_OBJECTS
                            WHERE OBJECT_TYPE = 'TABLE'
                            AND OWNER         = :OWNER";
            IConexao conexao = FactoryConexao.GetConexao(BD);
            using (var select = new OracleCommand(SQL, conexao.Oralceconnection))
            {
                select.Parameters.Add("OWNER", this.owner);
                List<string> CamposDaTabela = new List<string>();
                var read = select.ExecuteReader();

                while (read.Read())
                {
                    CamposDaTabela.Add(read["OBJECT_NAME"].ToString().ToUpper());
                }
                read.Close();
                conexao.Oralceconnection.Close();
                return CamposDaTabela;
            }
            
        }

        public string TamanhodoCamposString(string tabela, string campo)
        {
            if (!string.IsNullOrEmpty(tabela) && !string.IsNullOrEmpty(campo))
            {
                var toReturn = string.Empty;
                string SQL = @"SELECT DATA_LENGTH
                                FROM ALL_TAB_COLUMNS
                               WHERE OWNER = :OWNER
                                AND TABLE_NAME = :TABLE_NAME
                                AND COLUMN_NAME = :COLUMN_NAME
                                ORDER BY COLUMN_ID";
                IConexao conexao = FactoryConexao.GetConexao(BD);
                using (var select = new OracleCommand(SQL, conexao.Oralceconnection))
                {
                    select.Parameters.Add("OWNER", this.owner);
                    select.Parameters.Add("TABLE_NAME", tabela);
                    select.Parameters.Add("COLUMN_NAME", campo);
                    var read = select.ExecuteReader();
                    while (read.Read())
                    {
                        toReturn = read["DATA_LENGTH"].ToString();
                    }
                    conexao.Oralceconnection.Close();
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


                string SQL = @"SELECT COLUMN_NAME
                                FROM ALL_TAB_COLUMNS
                               WHERE OWNER = :OWNER
                                AND TABLE_NAME = :TABLE_NAME                            
                                ORDER BY COLUMN_ID";
                IConexao conexao = FactoryConexao.GetConexao(BD);
                using (var select = new OracleCommand(SQL, conexao.Oralceconnection))
                {
                    select.Parameters.Add("OWNER", this.owner);
                    select.Parameters.Add("TABLE_NAME", tabela);
                    List<string> CamposDaTabela = new List<string>();
                    var read = select.ExecuteReader();
                    string teste = string.Empty;
                    while (read.Read())
                    {
                        CamposDaTabela.Add(read["COLUMN_NAME"].ToString().ToUpper());
                    }
                    read.Close();
                    conexao.Oralceconnection.Close();
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
                string SQL = @"SELECT ALL_CONS_COLUMNS.COLUMN_NAME
                                FROM ALL_CONSTRAINTS, ALL_CONS_COLUMNS 
                                WHERE ALL_CONSTRAINTS.OWNER         = :OWNER
                                AND ALL_CONSTRAINTS.TABLE_NAME      = :TABLE_NAME
                                AND ALL_CONSTRAINTS.CONSTRAINT_TYPE = 'P'
                                AND ALL_CONSTRAINTS.OWNER = ALL_CONS_COLUMNS.OWNER
                                AND ALL_CONSTRAINTS.CONSTRAINT_NAME = ALL_CONS_COLUMNS.CONSTRAINT_NAME";
                IConexao conexao = FactoryConexao.GetConexao(BD);
                using (var select = new OracleCommand(SQL, conexao.Oralceconnection))
                {
                    select.Parameters.Add("OWNER", this.owner);
                    select.Parameters.Add("TABLE_NAME", tabela);
                    List<string> CamposDaTabela = new List<string>();
                    var read = select.ExecuteReader();

                    while (read.Read())
                    {
                        CamposDaTabela.Add(read["COLUMN_NAME"].ToString().ToUpper());
                    }
                    read.Close();
                    conexao.Oralceconnection.Close();
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

                string SQL = @"SELECT ALL_CONS_COLUMNS.COLUMN_NAME
                                FROM ALL_CONSTRAINTS, ALL_CONS_COLUMNS 
                                WHERE ALL_CONSTRAINTS.OWNER         = :OWNER
                                AND ALL_CONSTRAINTS.TABLE_NAME      = :TABLE_NAME
                                AND ALL_CONS_COLUMNS.COLUMN_NAME    = :COLUMN_NAME
                                AND ALL_CONSTRAINTS.CONSTRAINT_TYPE = 'P'
                                AND ALL_CONSTRAINTS.OWNER = ALL_CONS_COLUMNS.OWNER
                                AND ALL_CONSTRAINTS.CONSTRAINT_NAME = ALL_CONS_COLUMNS.CONSTRAINT_NAME";
                IConexao conexao = FactoryConexao.GetConexao(BD);
                using (var select = new OracleCommand(SQL, conexao.Oralceconnection))
                {
                    select.Parameters.Add("OWNER", this.owner);
                    select.Parameters.Add("TABLE_NAME", tabela);
                    select.Parameters.Add("COLUMN_NAME", campo);
                    var read = select.ExecuteReader();
                    bool toReturn = false;
                    while (read.Read())
                    {
                        toReturn = (read["COLUMN_NAME"].ToString().ToUpper()) != string.Empty;
                    }
                    read.Close();
                    conexao.Oralceconnection.Close();
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
            string SQL = @"SELECT BANNER FROM V$VERSION";
            IConexao conexao = FactoryConexao.GetConexao(BD);
            using (var select = new OracleCommand(SQL, conexao.Oralceconnection))
            {
                var read = select.ExecuteReader();
                string toReturn = string.Empty;
                while (read.Read())
                {
                    toReturn = read[0].ToString().ToUpper();
                }
                read.Close();
                conexao.Oralceconnection.Close();
                if (toReturn.Substring(0, 19) == "Oracle Database 10g")
                {
                    return "NHibernate.Dialect.Oracle10gDialect";
                }
                else
                    if(toReturn.Substring(0, 18) == "Oracle Database 9i")
                    {
                        return "NHibernate.Dialect.Oracle9iDialect";
                    }
                    else
                        if (toReturn.Substring(0, 18) == "Oracle Database 8i")
                        {
                            return "NHibernate.Dialect.Oracle8iDialect";
                        }
                        else
                            if (toReturn.Substring(0, 19) == "Oracle Database 11g")
                            {
                                return "NHibernate.Dialect.Oracle11gDialect";
                            }
            }
            
            return "NHibernate.Dialect.Oracle10gDialect";
        }

        public string ConnectionDriver()
        {
            return "NHibernate.Driver.OracleClientDriver";
        }



        public string CampoChavesDaTabela(string tabela)
        {

            if (!string.IsNullOrEmpty(tabela))
            {

                string SQL = @"SELECT ALL_CONS_COLUMNS.COLUMN_NAME
                                FROM ALL_CONSTRAINTS, ALL_CONS_COLUMNS 
                                WHERE ALL_CONSTRAINTS.OWNER         = :OWNER
                                AND ALL_CONSTRAINTS.TABLE_NAME      = :TABLE_NAME
                                AND ALL_CONSTRAINTS.CONSTRAINT_TYPE = 'P'
                                AND ALL_CONSTRAINTS.OWNER = ALL_CONS_COLUMNS.OWNER
                                AND ALL_CONSTRAINTS.CONSTRAINT_NAME = ALL_CONS_COLUMNS.CONSTRAINT_NAME";
                IConexao conexao = FactoryConexao.GetConexao(BD);
                using (var select = new OracleCommand(SQL, conexao.Oralceconnection))
                {
                    select.Parameters.Add("OWNER", this.owner);
                    select.Parameters.Add("TABLE_NAME", tabela);
                    var read = select.ExecuteReader();
                    string toReturn = string.Empty;
                    while (read.Read())
                    {
                        toReturn = (read["COLUMN_NAME"].ToString().ToUpper());
                    }
                    read.Close();
                    conexao.Oralceconnection.Close();
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
                string SQL = @"SELECT NULLABLE
                                FROM ALL_TAB_COLUMNS
                               WHERE OWNER = :OWNER
                                AND TABLE_NAME = :TABLE_NAME                            
                                AND COLUMN_NAME = :COLUMN_NAME
                                ORDER BY COLUMN_ID";
                IConexao conexao = FactoryConexao.GetConexao(BD);
                using (var select = new OracleCommand(SQL, conexao.Oralceconnection))
                {
                    select.Parameters.Add("OWNER", this.owner);
                    select.Parameters.Add("TABLE_NAME", tabela);
                    select.Parameters.Add("COLUMN_NAME", campo);
                    
                    var read = select.ExecuteReader();
                    while (read.Read())
                    {
                        toReturn = read["NULLABLE"].ToString() == "N";
                    }
                    conexao.Oralceconnection.Close();
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
                string SQL = @"SELECT FILHA.TABLE_NAME
                                FROM USER_CONSTRAINTS PAI
                                INNER JOIN USER_CONSTRAINTS FILHA
                                ON FILHA.R_CONSTRAINT_NAME = PAI.CONSTRAINT_NAME
                                INNER JOIN USER_CONS_COLUMNS COLUNA
                                ON COLUNA.CONSTRAINT_NAME = PAI.CONSTRAINT_NAME
                                WHERE PAI.TABLE_NAME    = :TABLE_NAME
                                AND OWNER = :OWNER";
                IConexao conexao = FactoryConexao.GetConexao(BD);
                using (var select = new OracleCommand(SQL, conexao.Oralceconnection))
                {
                    
                    select.Parameters.Add("TABLE_NAME", tabela);
                    select.Parameters.Add("OWNER", this.owner);
                    var read = select.ExecuteReader();
                    while (read.Read())
                    {
                        toReturn.Add(read["TABLE_NAME"].ToString());
                    }
                    conexao.Oralceconnection.Close();
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

            return false;

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
                string SQL = @"SELECT COLUNA.COLUMN_NAME
                                FROM USER_CONSTRAINTS PAI
                                INNER JOIN USER_CONSTRAINTS FILHA
                                ON FILHA.R_CONSTRAINT_NAME = PAI.CONSTRAINT_NAME
                                INNER JOIN USER_CONS_COLUMNS COLUNA
                                ON COLUNA.CONSTRAINT_NAME = PAI.CONSTRAINT_NAME
                                WHERE FILHA.TABLE_NAME    = :TABLE_NAME
                                AND OWNER = :OWNER";

                IConexao conexao = FactoryConexao.GetConexao(BD);
                using (var select = new OracleCommand(SQL, conexao.Oralceconnection))
                {
                    
                    select.Parameters.Add("TABLE_NAME", tabela);
                    select.Parameters.Add("OWNER", this.owner);
                    List<string> CamposDaTabela = new List<string>();
                    var read = select.ExecuteReader();

                    while (read.Read())
                    {
                        CamposDaTabela.Add(read["COLUMN_NAME"].ToString().ToUpper());

                    }
                    read.Close();
                    conexao.Oralceconnection.Close();
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
                string SQL = @"SELECT FILHA.TABLE_NAME AS TABLE_NAME, PAI.TABLE_NAME AS NAME
                                FROM USER_CONSTRAINTS PAI
                                INNER JOIN USER_CONSTRAINTS FILHA
                                ON FILHA.R_CONSTRAINT_NAME = PAI.CONSTRAINT_NAME
                                INNER JOIN USER_CONS_COLUMNS COLUNA
                                ON COLUNA.CONSTRAINT_NAME = PAI.CONSTRAINT_NAME
                                WHERE FILHA.TABLE_NAME    = :TABLE_NAME
                                AND COLUNA.COLUMN_NAME    = :COLUMN_NAME
                                AND OWNER = :OWNER";

                IConexao conexao = FactoryConexao.GetConexao(BD);
                using (var select = new OracleCommand(SQL, conexao.Oralceconnection))
                {
                    string TipoDeCampoDaTabela = string.Empty;
                    select.Parameters.Add("TABLE_NAME", tabela);
                    select.Parameters.Add("COLUMN_NAME", campo);
                    select.Parameters.Add("OWNER", this.owner);

                    var read = select.ExecuteReader();
                    read.Read();

                    try
                    {
                        if (read["TABLE_NAME"].ToString().ToUpper() == read["NAME"].ToString().ToUpper())
                        {
                            return "NULL";
                        }
                        else

                        TipoDeCampoDaTabela = read["NAME"].ToString().ToUpper();
                    }
                    catch                     
                    {

                        return "NULL";
                    }
                    read.Close();
                    conexao.Oralceconnection.Close();
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
                string SQL = @"SELECT ALL_TAB_COLUMNS.DATA_TYPE, ALL_TAB_COLUMNS.DATA_SCALE
                                FROM ALL_TAB_COLUMNS
                               WHERE ALL_TAB_COLUMNS.OWNER = :OWNER
                                AND ALL_TAB_COLUMNS.TABLE_NAME = :TABLE_NAME
                                AND ALL_TAB_COLUMNS.COLUMN_NAME = :COLUMN_NAME";

                IConexao conexao = FactoryConexao.GetConexao(BD);
                using (var select = new OracleCommand(SQL, conexao.Oralceconnection))
                {
                    select.Parameters.Add("OWNER", this.owner);
                    select.Parameters.Add("TABLE_NAME", tabela);
                    select.Parameters.Add("COLUMN_NAME", campo);
                    
                    string TipoDeCampoDaTabela = string.Empty;
                    var read = select.ExecuteReader();
                    while (read.Read())
                    {
                        TipoDeCampoDaTabela = read["DATA_TYPE"].ToString().ToUpper();
                        if (TipoDeCampoDaTabela == "NUMBER")
                        {
                            if (read["DATA_SCALE"].ToString() == "0" || string.IsNullOrEmpty(read["DATA_SCALE"].ToString()))
                            {
                                TipoDeCampoDaTabela = "INT";
                            }

                        }
                    }
                    read.Close();
                    conexao.Oralceconnection.Close();
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
            string TipoDado = this.TipoDadoBD(tabela, campo);

            if (TipoDado == "INT")
            {
                return "int";
            }
            else
                if (TipoDado == "VARCHAR2" || TipoDado == "VARCHAR" || TipoDado == "NVARCHAR2"
                    || TipoDado == "NCHAR" || TipoDado == "CHAR")
                {
                    return "string";
                }
                else
                    if (TipoDado == "TIMESTAMP" || TipoDado == "DATE")
                    {
                        return "DateTime";
                    }
                    else
                        if (TipoDado == "bigint")
                        {
                            return "long";
                        }
                        else
                            if (TipoDado == "LONG" || TipoDado == "REAL"
                                || TipoDado == "NUMBER")
                            {
                                return "decimal";
                            }
                            else
                                if (TipoDado == "FLOAT")
                                {
                                    return "double";
                                }
                                else
                                    if (TipoDado == "LONG RAW" || TipoDado == "CLOB"
                                        || TipoDado == "NCLOB" || TipoDado == "BLOB")
                                    {
                                        return "byte[]";
                                    }


            return string.Empty;
        }


    }
}
