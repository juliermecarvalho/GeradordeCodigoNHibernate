using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeradorDeCodigo.MetaDados;
using System.Windows.Forms;
using GeradorDeCodigo.Enum;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;


namespace GeradorDeCodigo
{
    public class ControlaInterface
    {

        public bool VerificaConexao(string ServeName)
        {
            if (ServeName != string.Empty)
            {
                StringConexao.GetInstance(ServeName);
                IConexao con =  FactoryConexao.GetConexao(TiposDeBancoDeDados.SQLSERVER);
                return con.TesteConexao();
            }
            return false;

        }

        public bool VerificaConexao(string ServeName, string DataBase)
        {
            if (ServeName != string.Empty
                && DataBase != string.Empty)
            {
                StringConexao.GetInstance(ServeName, DataBase);
                IConexao con = FactoryConexao.GetConexao(TiposDeBancoDeDados.SQLSERVER);
                return con.TesteConexao();

            }
            return false;
        }

        public bool VerificaConexao(string ServeName, string Login, string Senha, TiposDeBancoDeDados BD)
        {
            if (ServeName != string.Empty
                && Login != string.Empty
                && Senha != string.Empty)
            {
                StringConexao.GetInstance(ServeName, Login, Senha);
                IConexao con = FactoryConexao.GetConexao(BD);
                return con.TesteConexao();

            }
            return false;
        }

        public bool VerificaConexao(string ServeName, string DataBase,
            string Login, string Senha)
        {
            if (ServeName != string.Empty
                && DataBase != string.Empty
                && Login != string.Empty
                && Senha != string.Empty)
            {
                StringConexao.GetInstance(ServeName, DataBase,
                    Login, Senha);
                IConexao con = FactoryConexao.GetConexao(TiposDeBancoDeDados.SQLSERVER);
                return con.TesteConexao();
            }
            return false;
        }


        public void ListaTabelas(CheckedListBox chkTabelas, TiposDeBancoDeDados bd, bool checkd)
        {
            ITabelasECampos tabelasECampos = TabelaECamposFactoy.GetTabelasECampos(bd);
            chkTabelas.Items.Clear();
            foreach (string NomeDaTabela in tabelasECampos.TabelasDoBanco())
            {
                chkTabelas.Items.Add(NomeDaTabela, checkd);
            }
        }

     

      

        public void AdicionaSeverCombox(ComboBox cmb)
        {
            SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
            System.Data.DataTable table = instance.GetDataSources();

            DisplayData(table, cmb);

        }

        private void DisplayData(DataTable table, ComboBox cmb)
        {
            string serviname = string.Empty;
            string instancename = string.Empty;
            foreach (System.Data.DataRow row in table.Rows)
            {
                foreach (System.Data.DataColumn col in table.Columns)
                {
                    if (col.ColumnName == "ServerName")
                    {
                        serviname = "" + row[col];
                    }
                    if (col.ColumnName == "InstanceName")
                    {
                        if (!string.IsNullOrEmpty(row[col].ToString()))
                            instancename = @"\" + row[col];
                    }

                }
                cmb.Items.Add(serviname + instancename);
                serviname = string.Empty;
                instancename = string.Empty;
            }
        }

        public void AdicionaDataBaseCombox(ComboBox cmb)
        {
            string SQL = @"sp_helpdb";
            IConexao conexao = FactoryConexao.GetConexao(TiposDeBancoDeDados.SQLSERVER);
            
            using (var select = new SqlCommand(SQL, conexao.Sqlconnection))
            {    
                var read = select.ExecuteReader();
                while (read.Read())
                {
                    cmb.Items.Add(read[0].ToString());
                }
                read.Close();
            }
            
        }
    }
}
