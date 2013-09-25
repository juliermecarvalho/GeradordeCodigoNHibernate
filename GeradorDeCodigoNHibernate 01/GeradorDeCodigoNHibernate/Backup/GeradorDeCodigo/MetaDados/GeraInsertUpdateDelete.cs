using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using GeradorDeCodigo.MetaDados;
using GeradorDeCodigo.Enum;

namespace GeradorDeCodigo
{
    public class GeraInsertUpdateDelete
    {
        TiposDeBancoDeDados BancodeDados;

        public GeraInsertUpdateDelete(TiposDeBancoDeDados BancodeDados)
        {
            this.BancodeDados = BancodeDados;
        }

        /// <summary>
        /// Monta a instrução SQL de Insert.
        /// </summary>
        /// <param name="NomeDaTabela"></param>
        /// <returns></returns>
        public string MontaSqlInsert(string NomeDaTabela)
        {
            ITabelasECampos tabelasecampos = TabelaECamposFactoy.GetTabelasECampos(BancodeDados);

            if (tabelasecampos.VerificaSeExisteTabela(NomeDaTabela))
            {
                string Campos = string.Empty;
                string Parametros = string.Empty;


                foreach (string campo in tabelasecampos.CamposDaTabela(NomeDaTabela))
                {
                    if (tabelasecampos.CamposIdentityDaTabela(NomeDaTabela, campo))
                    {
                        Campos += campo + ", ";
                        Parametros += "@" + campo + ", ";
                    }

                }
                if (Campos.Length > 1)
                {
                    Campos = Campos.Substring(0, (Campos.Length - 2)).ToUpper();
                }

                if (Parametros.Length > 1)
                {
                    Parametros = Parametros.Substring(0, (Parametros.Length - 2)).ToUpper();
                }


                return "INSERT INTO [" + NomeDaTabela + "] (" + Campos + ") VALUES (" + Parametros + ")";
            }
            else
            {
                MessageBox.Show(Messagen.TABELANAOEXISTE, Messagen.ATENCAO, MessageBoxButtons.OK);
                return string.Empty;
            }
        }

        /// <summary>
        /// Monta instrução Update.
        /// </summary>
        /// <param name="NomeDaTabela"></param>
        /// <returns></returns>
        public string MontaSqlUpdate(string NomeDaTabela)
        {
            ITabelasECampos tabelasecampos = TabelaECamposFactoy.GetTabelasECampos(BancodeDados);

            if (tabelasecampos.VerificaSeExisteTabela(NomeDaTabela))
            {
                string Campos = string.Empty;
                string Parametros = string.Empty;

                foreach (string campo in tabelasecampos.CamposDaTabela(NomeDaTabela))
                {
                    if (tabelasecampos.CamposIdentityDaTabela(NomeDaTabela, campo))
                    {
                        Campos += campo + " = @" + campo + ", ";
                    }
                }

                foreach (string campo in tabelasecampos.CamposChavesDaTabela(NomeDaTabela))
                {
                    Parametros += campo + " = @" + campo + " AND ";
                }
                if (Campos.Length > 1)
                {
                    Campos = Campos.Substring(0, (Campos.Length - 2)).ToUpper();
                }

                if (Parametros.Length > 4)
                {
                    Parametros = Parametros.Substring(0, (Parametros.Length - 5)).ToUpper();
                }


                return "UPDATE [" + NomeDaTabela + "] SET " + Campos + " WHERE " + Parametros;
            }
            else
            {
                MessageBox.Show(Messagen.TABELANAOEXISTE, Messagen.ATENCAO, MessageBoxButtons.OK);
                return string.Empty;
            }
        }
        /// <summary>
        /// Monta a instrução Delete.
        /// </summary>
        /// <param name="NomeDaTabela"></param>
        /// <returns></returns>
        public string MontaSqlDelete(string NomeDaTabela)
        {
            ITabelasECampos tabelasecampos = TabelaECamposFactoy.GetTabelasECampos(BancodeDados);

            if (tabelasecampos.VerificaSeExisteTabela(NomeDaTabela))
            {
                string Parametros = string.Empty;


                foreach (string campo in tabelasecampos.CamposChavesDaTabela(NomeDaTabela))
                {
                    Parametros += campo + " = @" + campo + " AND ";
                }

                if (Parametros.Length > 4)
                {
                    Parametros = Parametros.Substring(0, (Parametros.Length - 5)).ToUpper();
                }

                return "DELETE FROM [" + NomeDaTabela + "] WHERE  " + Parametros;
            }
            else
            {
                MessageBox.Show(Messagen.TABELANAOEXISTE, Messagen.ATENCAO, MessageBoxButtons.OK);
                return string.Empty;
            }
        }

        /// <summary>
        /// Monta instrução select.
        /// </summary>
        /// <param name="NomeDaTabela"></param>
        /// <returns></returns>
        public string MontaSqlSelectKey(string NomeDaTabela)
        {
            ITabelasECampos tabelasecampos = TabelaECamposFactoy.GetTabelasECampos(BancodeDados);

            if (tabelasecampos.VerificaSeExisteTabela(NomeDaTabela))
            {

                string Parametros = string.Empty;


                foreach (string campo in tabelasecampos.CamposChavesDaTabela(NomeDaTabela))
                {
                    Parametros += campo + " = @" + campo + " AND ";
                }


                if (Parametros.Length > 4)
                {
                    Parametros = Parametros.Substring(0, (Parametros.Length - 5)).ToUpper();
                }


                return "SELECT * FROM [" + NomeDaTabela + "] WHERE " + Parametros;
            }
            else
            {
                MessageBox.Show(Messagen.TABELANAOEXISTE, Messagen.ATENCAO, MessageBoxButtons.OK);
                return string.Empty;
            }
        }
        /// <summary>
        /// Monta instrução select com chave.
        /// </summary>
        /// <param name="NomeDaTabela"></param>
        /// <returns></returns>
        public string MontaSqlSelect(string NomeDaTabela)
        {
            ITabelasECampos tabelasecampos = TabelaECamposFactoy.GetTabelasECampos(BancodeDados);

            if (tabelasecampos.VerificaSeExisteTabela(NomeDaTabela))
            {

                return "SELECT * FROM [" + NomeDaTabela + "]";
            }
            else
            {
                MessageBox.Show(Messagen.TABELANAOEXISTE, Messagen.ATENCAO, MessageBoxButtons.OK);
                return string.Empty;
            }
        }

    }
}
