using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GeradorDeCodigo.MetaDados;
using GeradorDeCodigo.GerarClasses;
using System.IO;
using System.Data.Sql;
using System.Data.SqlClient;
using GeradorDeCodigo.Enum;

namespace GeradorDeCodigo
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
            cmbLazy.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            bool verifica = false;
            ControlaInterface controlainterface = new ControlaInterface();
            TiposDeBancoDeDados BD;
            if (tbcBD.SelectedTab.Text == "SqlServer")
            {
                BD = TiposDeBancoDeDados.SQLSERVER;
                if (chkWindwsAuthentication.Checked)
                {
                    if (cmbServeName.Text != string.Empty)
                    {
                        verifica = controlainterface.VerificaConexao(cmbServeName.Text, cmbDataBase.Text);
                    }
                }
                else
                {
                    if (cmbServeName.Text != string.Empty
                        && txtLogin.Text != string.Empty
                        && txtSenha.Text != string.Empty)
                    {
                        verifica = controlainterface.VerificaConexao(cmbServeName.Text, cmbDataBase.Text,
                             txtLogin.Text, txtSenha.Text);
                    }
                }
            }

            else
            {
                BD = TiposDeBancoDeDados.ORACLE;
                verifica = controlainterface.VerificaConexao(@"(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" + txtLocalHost.Text
                    + ")(PORT=" + txtPorta.Text + ")))(CONNECT_DATA=(SERVICE_NAME=" + txtSID.Text + ")))",
                    txtNomeUsuario.Text, txtSenhaOralce.Text, BD);
            }

            if (verifica)
            {
                List<string> ListaTabelas = new List<string>();
                foreach (object itemChecked in chkTabelas.CheckedItems)
                {
                    ListaTabelas.Add(itemChecked.ToString());
                }
                if (ListaTabelas.Count > 0)
                {
                    
                    GerarArquivoCSharp gerarArquivoCSharp = new GerarArquivoCSharp(BD);

                    gerarArquivoCSharp.GeraNHibernateHelper = chkGeraNHirbenateHelper.Checked;
                    gerarArquivoCSharp.GeraEntidades = chkEntidades.Checked;
                    gerarArquivoCSharp.GeraclassesRepositorio = chkRepositorio.Checked;
                    gerarArquivoCSharp.Lazy = cmbLazy.Text.ToLower();
                    gerarArquivoCSharp.RetiraS = chkRetiraS.Checked;
                    gerarArquivoCSharp.PropriedadesAutomaticas = chkPropriedadesAutomaticas.Checked;

                    gerarArquivoCSharp.GeraArquivo(progressBar, ListaTabelas);


                }
                else
                {
                    MessageBox.Show(Messagen.SELECIONARTABELA, Messagen.ATENCAO, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show(Messagen.FALHANACONEXAO, Messagen.ATENCAO, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void chkWindwsAuthentication_CheckedChanged(object sender, EventArgs e)
        {
            txtLogin.Enabled = !((CheckBox)sender).Checked;
            txtSenha.Enabled = !((CheckBox)sender).Checked;
        }

        private void btnTesteConexao_Click(object sender, EventArgs e)
        {
            bool verifica = false;
            ControlaInterface controlainterface = new ControlaInterface();
            if (tbcBD.SelectedTab.Text == "SqlServer")
            {
                if (chkWindwsAuthentication.Checked)
                {
                    if (cmbServeName.Text != string.Empty)
                    {
                        verifica = controlainterface.VerificaConexao(cmbServeName.Text);
                    }
                }
                else
                {
                    if (cmbServeName.Text != string.Empty
                        && txtLogin.Text != string.Empty
                        && txtSenha.Text != string.Empty)
                    {
                        verifica = controlainterface.VerificaConexao(cmbServeName.Text, cmbDataBase.Text,
                             txtLogin.Text, txtSenha.Text);
                    }
                }
            }
            else
            {
                verifica = controlainterface.VerificaConexao(@"(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" + txtLocalHost.Text
                    + ")(PORT=" + txtPorta.Text + ")))(CONNECT_DATA=(SERVICE_NAME=" + txtSID.Text + ")))",
                    txtNomeUsuario.Text, txtSenhaOralce.Text, TiposDeBancoDeDados.ORACLE);
            }

            if (verifica)
            {
                MessageBox.Show(Messagen.CONEXAOREALIZADACOMSUCESSO, Messagen.ATENCAO, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(Messagen.FALHANACONEXAO, Messagen.ATENCAO, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void cmbDataBase_SelectedIndexChanged(object sender, EventArgs e)
        {
            ControlaInterface controlainterface = new ControlaInterface();
            if (chkWindwsAuthentication.Checked)
            {
                controlainterface.VerificaConexao(cmbServeName.Text, cmbDataBase.Text);
            }
            else
            {
                controlainterface.VerificaConexao(cmbServeName.Text, cmbDataBase.Text,
                    txtLogin.Text, txtSenha.Text);
            }
        }



        private void cmbServeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool verifica = false;
            ControlaInterface controlainterface = new ControlaInterface();
            if (chkWindwsAuthentication.Checked)
            {
                if (cmbServeName.Text != string.Empty)
                {
                    verifica = controlainterface.VerificaConexao(cmbServeName.Text);
                }
            }
            else
            {
                if (cmbServeName.Text != string.Empty
                    && txtLogin.Text != string.Empty
                    && txtSenha.Text != string.Empty)
                {
                    TiposDeBancoDeDados BD;
                    if (tbcBD.SelectedTab.Text == "SqlServer")
                    {
                        BD = TiposDeBancoDeDados.SQLSERVER;
                    }
                    else
                    {
                        BD = TiposDeBancoDeDados.ORACLE;
                    }
                    verifica = controlainterface.VerificaConexao(cmbServeName.Text, 
                         txtLogin.Text, txtSenha.Text, BD);
                }
            }

            if (verifica)
            {


                controlainterface.AdicionaDataBaseCombox(cmbDataBase);
            }

        }

        private void btnListarTabelas_Click(object sender, EventArgs e)
        {
            bool verifica = false;
            TiposDeBancoDeDados BD;
            ControlaInterface controlainterface = new ControlaInterface();
            if (tbcBD.SelectedTab.Text == "SqlServer")
            {
                BD = TiposDeBancoDeDados.SQLSERVER;

                if (chkWindwsAuthentication.Checked)
                {
                    if (cmbServeName.Text != string.Empty)
                    {
                        verifica = controlainterface.VerificaConexao(cmbServeName.Text, cmbDataBase.Text);
                    }
                }
                else
                {
                    if (cmbServeName.Text != string.Empty
                        && txtLogin.Text != string.Empty
                        && txtSenha.Text != string.Empty)
                    {
                        verifica = controlainterface.VerificaConexao(cmbServeName.Text, cmbDataBase.Text,
                             txtLogin.Text, txtSenha.Text);
                    }
                }
            }
            else
            {
                BD = TiposDeBancoDeDados.ORACLE;

                verifica = controlainterface.VerificaConexao(@"(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" + txtLocalHost.Text
                    + ")(PORT=" + txtPorta.Text + ")))(CONNECT_DATA=(SERVICE_NAME=" + txtSID.Text + ")))",
                    txtNomeUsuario.Text, txtSenhaOralce.Text, BD);
            }



            if (verifica)
            {
                controlainterface.ListaTabelas(chkTabelas, BD, true);
            }

        }

        private void Nenhum_Click(object sender, EventArgs e)
        {
            bool verifica = false;
            ControlaInterface controlainterface = new ControlaInterface();
            if (chkWindwsAuthentication.Checked)
            {
                if (cmbServeName.Text != string.Empty)
                {
                    verifica = controlainterface.VerificaConexao(cmbServeName.Text, cmbDataBase.Text);
                }
            }
            else
            {
                if (cmbServeName.Text != string.Empty
                    && txtLogin.Text != string.Empty
                    && txtSenha.Text != string.Empty)
                {
                    verifica = controlainterface.VerificaConexao(cmbServeName.Text, cmbDataBase.Text,
                         txtLogin.Text, txtSenha.Text);
                }
            }

            if (verifica)
            {

                controlainterface.ListaTabelas(chkTabelas, TiposDeBancoDeDados.SQLSERVER, false);
            }

        }

        private void btnTodos_Click(object sender, EventArgs e)
        {
            bool verifica = false;
            ControlaInterface controlainterface = new ControlaInterface();
            if (chkWindwsAuthentication.Checked)
            {
                if (cmbServeName.Text != string.Empty)
                {
                    verifica = controlainterface.VerificaConexao(cmbServeName.Text, cmbDataBase.Text);
                }
            }
            else
            {
                if (cmbServeName.Text != string.Empty
                    && txtLogin.Text != string.Empty
                    && txtSenha.Text != string.Empty)
                {
                    verifica = controlainterface.VerificaConexao(cmbServeName.Text, cmbDataBase.Text,
                         txtLogin.Text, txtSenha.Text);
                }
            }

            if (verifica)
            {
                controlainterface.ListaTabelas(chkTabelas, TiposDeBancoDeDados.SQLSERVER, true);
            }

        }

        private void btnServeName_Click(object sender, EventArgs e)
        {
            cmbServeName.Items.Clear();
            ControlaInterface oontrolainterface = new ControlaInterface();
            oontrolainterface.AdicionaSeverCombox(cmbServeName);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            ControlaInterface controlainterface = new ControlaInterface();
            controlainterface.VerificaConexao(@"(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))
(CONNECT_DATA=(SERVICE_NAME=xe)))", "HR", "hr", TiposDeBancoDeDados.ORACLE);

        }

       



    }
}
