using System;
using System.Collections.Generic;

namespace GeradorDeCodigo.MetaDados
{
    public interface ITabelasECampos
    {

        List<string> CamposChavesDaTabela(string tabela);
        List<string> CamposChavesEstrageiraDaTabela(string tabela);
        List<string> CamposDaTabela(string tabela);
        string CampoChavesDaTabela(string tabela);
        bool CampoChavesDaTabela(string tabela, string campo);
        bool CampoOBrigatorio(string tabela, string campo);
        bool CamposIdentityDaTabela(string tabela, string campo);
        string TamanhodoCamposString(string tabela, string campo);
        string NomeDaTabelaDaChaveEstrgeira(string tabela, string campo);
        List<string> TabelasDoBanco();
        string TipoDadoBD(string tabela, string campo);
        string TipoDadoCSharp(string tabela, string campo);
        bool VerificaSeExisteTabela(string tabela);
        string DialectDoBancodeDados();
        string ConnectionDriver();
        List<string> TabelasOndeARelacionamento(string Tabela);
    }
}
