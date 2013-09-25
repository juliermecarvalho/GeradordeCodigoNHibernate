using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using GeradorDeCodigo.MetaDados;
using GeradorDeCodigo.Enum;
using System.Xml;


namespace GeradorDeCodigo.GerarClasses
{
    public class GerarArquivoCSharp
    {
        private TiposDeBancoDeDados BancoDeDados;
        private static string CaminhoInicial = @"C:\";
        private bool geraNHibernateHelper;
        private bool geraEntidades;
        // string Lazy, bool RetiraS, List<string> ListaTabelas, bool PropriedadesAutomaticas
        private bool geraclassesRepositorio;
        private string lazy;
        private bool retiraS;
        private bool propriedadesAutomaticas;

        public bool PropriedadesAutomaticas
        {
            get { return propriedadesAutomaticas; }
            set { propriedadesAutomaticas = value; }
        }

        public bool RetiraS
        {
            get { return retiraS; }
            set { retiraS = value; }
        }



        public string Lazy
        {
            get { return lazy; }
            set { lazy = value; }
        }



        public bool GeraclassesRepositorio
        {
            get { return geraclassesRepositorio; }
            set { geraclassesRepositorio = value; }
        }


        public bool GeraEntidades
        {
            get { return geraEntidades; }
            set { geraEntidades = value; }
        }

        public bool GeraNHibernateHelper
        {
            get { return geraNHibernateHelper; }
            set { geraNHibernateHelper = value; }
        }

        public GerarArquivoCSharp(TiposDeBancoDeDados BancoDeDados)
        {
            this.BancoDeDados = BancoDeDados;
        }




        private void GeraArquivoXMLDeConfigNHibernate(string NomeNamespace, string caminho, bool RetiraS, List<string> ListaTabelas)
        {
            TrataNome tratanome = new TrataNome();
            ITabelasECampos tabelasecampos = TabelaECamposFactoy.GetTabelasECampos(BancoDeDados);

            DirectoryInfo diretorio = new DirectoryInfo(caminho);

            if (!diretorio.Exists)
            {
                diretorio.Create();
            }

            StreamWriter aFile = new StreamWriter(caminho + "\\hibernate.cfg.xml", false, Encoding.ASCII);




            aFile.WriteLine("<?xml version=\"1.0\"?>");
            aFile.WriteLine("<hibernate-configuration xmlns=\"urn:nhibernate-configuration-2.2\">");
            aFile.WriteLine("\t<session-factory>");
            aFile.WriteLine("");
            aFile.WriteLine("\t<!-- configurations -->");

            aFile.WriteLine("\t\t<property name=\"connection.provider\">NHibernate.Connection.DriverConnectionProvider</property>");
            aFile.WriteLine("\t\t<property name=\"dialect\">" + tabelasecampos.DialectDoBancodeDados() + "</property>");
            aFile.WriteLine("\t\t<property name=\"connection.driver_class\">" + tabelasecampos.ConnectionDriver() + "</property>");
            aFile.WriteLine("\t\t<property name=\"connection.connection_string\">" + StringConexao.Stringconexao + "</property>");
            aFile.WriteLine("\t\t<property name=\"proxyfactory.factory_class\">NHibernate.ByteCode.LinFu.ProxyFactoryFactory, NHibernate.ByteCode.LinFu</property>");
            aFile.WriteLine("\t\t<property name=\"show_sql\">true</property>");
            aFile.WriteLine("");
            aFile.WriteLine("\t<!-- mapping files -->");


            foreach (string tabelas in ListaTabelas)
            {
                if (tabelasecampos.CamposChavesDaTabela(tabelas).Count > 0)
                {

                    aFile.WriteLine("\t\t<mapping resource=\"" + NomeNamespace + ".Mapeamentos." +
                        tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(tabelas, RetiraS) + ".hbm.xml\" assembly=\"" +
                        NomeNamespace + "\"/>");
                }
            }


            aFile.WriteLine("");
            aFile.WriteLine("</session-factory>");
            aFile.WriteLine("</hibernate-configuration>");
            aFile.Close();

        }

        private void GeraArquivoXMLDaEntidades(string NomeNamespace, string caminho, string NomeDaTabela, string Lazy, string NomeAssembly, bool RetiraS)
        {


            TrataNome tratanome = new TrataNome();
            ITabelasECampos tabelasecampos = TabelaECamposFactoy.GetTabelasECampos(BancoDeDados);


            List<string> Listacampos = tabelasecampos.CamposDaTabela(NomeDaTabela);
            DirectoryInfo diretorio = new DirectoryInfo(caminho);

            if (!diretorio.Exists)
            {
                diretorio.Create();
            }

            StreamWriter aFile = new StreamWriter(caminho + "\\" + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(NomeDaTabela, RetiraS) + ".hbm.xml", false, Encoding.ASCII);
            //duas variaveis 
            //para o nome da tabela e campos
            //o sql server aceita nome de campos e tabelas separados
            //por isso o careter especial sqlserver [nome da tabela]
            string Abre = string.Empty;
            string Fecha = string.Empty;
            if (BancoDeDados == TiposDeBancoDeDados.SQLSERVER)
            {
                Abre = "[";
                Fecha = "]";
            }


            aFile.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            aFile.WriteLine("<hibernate-mapping xmlns=\"urn:nhibernate-mapping-2.2\" auto-import=\"true\">");
            aFile.WriteLine("\t<class name=\"" + NomeNamespace + "." + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(NomeDaTabela, RetiraS)
                + ", " + NomeAssembly + "\" table=\"" + Abre + NomeDaTabela + Fecha + "\" lazy=\"" + Lazy + "\">");
            aFile.WriteLine("");
            //para carantir que id venha primeiro no xml
            string ID = string.Empty;
            string Property = string.Empty;
            int count = 0;
            foreach (string campos in Listacampos)
            {


                if (tabelasecampos.CampoChavesDaTabela(NomeDaTabela, campos))
                {
                    if (tabelasecampos.CamposChavesDaTabela(NomeDaTabela).Count == 1)
                    {

                        if (tabelasecampos.TipoDadoCSharp(NomeDaTabela, campos) == "string")
                        {
                            ID += "\t\t<id name=\"" +
                                tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(campos, RetiraS) + "\" column= \"" + Abre + campos + Fecha +
                                "\" type=\"String\" length=\"" + tabelasecampos.TamanhodoCamposString(NomeDaTabela, campos) + "\">\n";
                        }
                        else
                        {
                            ID += "\t\t<id name=\"" +
                                tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(campos, RetiraS) + "\" column= \"" + Abre + campos + Fecha +
                                "\" type=\"" + tabelasecampos.TipoDadoCSharp(NomeDaTabela, campos) + "\">\n";

                        }

                        if (tabelasecampos.CamposIdentityDaTabela(NomeDaTabela, campos))
                        {
                            ID += "\t\t\t<generator class=\"native\" />\n";
                        }
                        else
                        {
                            ID += "\t\t\t<generator class=\"assigned\" />\n";

                        }
                        ID += "\t\t</id>\n\n";
                    }
                    else
                    {

                        if (count == 0)
                        {
                            ID += "\t\t<composite-id name=\"Pk\" class=\"" + NomeNamespace + "." + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(NomeDaTabela, RetiraS)
                                + "+PK, " + NomeNamespace + "\">\n";
                        }
                        count++;


                        //
                        if (tabelasecampos.NomeDaTabelaDaChaveEstrgeira(NomeDaTabela, campos) != "NULL")
                        {

                            ID += "\t\t\t<key-many-to-one name=\"" +
                                tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(campos, RetiraS) + "\" column= \"" + Abre + campos + Fecha +
                                "\" class=\"" + NomeNamespace + "." + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(tabelasecampos.NomeDaTabelaDaChaveEstrgeira(NomeDaTabela, campos), RetiraS) + ", " + NomeNamespace + "\"/>\n";


                        }
                        else
                        {
                            if (tabelasecampos.TipoDadoCSharp(NomeDaTabela, campos) == "string")
                            {
                                ID += "\t\t\t<key-property name=\"" +
                                    tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(campos, RetiraS) + "\" column= \"" + Abre + campos + Fecha +
                                    "\" type=\"String\" length=\"" + tabelasecampos.TamanhodoCamposString(NomeDaTabela, campos) + "\"/>\n";
                            }
                            else
                            {
                                ID += "\t\t\t<key-property name=\"" +
                                    tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(campos, RetiraS) + "\" column= \"" + Abre + campos + Fecha +
                                    "\" type=\"" + tabelasecampos.TipoDadoCSharp(NomeDaTabela, campos) + "\"/>\n";

                            }
                        }


                        if (tabelasecampos.CamposChavesDaTabela(NomeDaTabela).Count == count)
                        {
                            ID += "\t\t</composite-id>\n\n";
                        }
                    }

                }
                else//fim do ID
                {


                    if (tabelasecampos.NomeDaTabelaDaChaveEstrgeira(NomeDaTabela, campos) != "NULL")
                    {

                        Property += "\t\t<many-to-one name=\"" +
                            tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(campos, RetiraS) + "\" column= \"" + Abre + campos + Fecha +
                            "\" class=\"" + NomeNamespace + "." + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(tabelasecampos.NomeDaTabelaDaChaveEstrgeira(NomeDaTabela, campos), RetiraS)
                            + ", " + NomeNamespace + "\" not-null=\"" +
                        tabelasecampos.CampoOBrigatorio(NomeDaTabela, campos).ToString().ToLower() + "\"/>\n";


                    }
                    else
                    {
                        if (tabelasecampos.TipoDadoCSharp(NomeDaTabela, campos) == "string")
                        {
                            Property += "\t\t<property name=\"" +
                                tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(campos, RetiraS) + "\" column= \"" + Abre + campos + Fecha +
                                "\" type=\"String\" length=\"" + tabelasecampos.TamanhodoCamposString(NomeDaTabela, campos) + "\" not-null=\"" +
                                tabelasecampos.CampoOBrigatorio(NomeDaTabela, campos).ToString().ToLower() + "\"/>\n";
                        }
                        else
                        {
                            if (tabelasecampos.TipoDadoCSharp(NomeDaTabela, campos) == "byte[]")
                            {
                                Property += "\t\t<property name=\"" +
                                    tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(campos, RetiraS) + "\" column= \"" + Abre + campos + Fecha +
                                    "\" type=\"BinaryBlob\" not-null=\"" +
                                tabelasecampos.CampoOBrigatorio(NomeDaTabela, campos).ToString().ToLower() + "\"/>\n";

                            }
                            else
                            {
                                Property += "\t\t<property name=\"" +
                                    tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(campos, RetiraS) + "\" column= \"" + Abre + campos + Fecha +
                                    "\" type=\"" + tabelasecampos.TipoDadoCSharp(NomeDaTabela, campos) + "\" not-null=\"" +
                                tabelasecampos.CampoOBrigatorio(NomeDaTabela, campos).ToString().ToLower() + "\"/>\n";

                            }
                        }
                    }
                }
            }



            aFile.Write(ID);
            aFile.Write(Property);

            aFile.WriteLine("");
            string nomedatabelapassou = string.Empty;
            int i = 0;
            foreach (string tabela in tabelasecampos.TabelasOndeARelacionamento(NomeDaTabela))
            {


                if (nomedatabelapassou == tabela)
                {
                    i += 1;
                    nomedatabelapassou = tabela;
                }
                else
                {
                    i = 0;
                    nomedatabelapassou = tabela;
                }

                if (i > 0)
                {
                    aFile.Write("\t\t<bag name=\"" + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(tabelasecampos.CamposChavesEstrageiraDaTabela(tabela)[i], RetiraS) + "List\"");
                    aFile.Write(" inverse=\"true\"");
                    aFile.Write(" lazy=\"" + Lazy + "\"");
                    aFile.Write(" cascade=\"all\">\n");
                    aFile.WriteLine("\t\t\t<key column=\"" + Abre + tabelasecampos.CamposChavesEstrageiraDaTabela(tabela)[i] + Fecha + "\"/>");
                    aFile.WriteLine("\t\t\t<one-to-many class=\""
                        + NomeNamespace + "." + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(tabela, RetiraS) + "\"/>");
                    aFile.WriteLine("\t\t</bag>");
                }
                else
                {

                    aFile.Write("\t\t<bag name=\"" + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(tabela, RetiraS) + "List\"");
                    aFile.Write(" inverse=\"true\"");
                    aFile.Write(" lazy=\"" + Lazy + "\"");
                    aFile.Write(" cascade=\"all\">\n");
                    aFile.WriteLine("\t\t\t<key column=\"" + Abre + tabelasecampos.CamposChavesEstrageiraDaTabela(tabela)[i] + Fecha + "\"/>");
                    aFile.WriteLine("\t\t\t<one-to-many class=\""
                        + NomeNamespace + "." + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(tabela, RetiraS) + "\"/>");
                    aFile.WriteLine("\t\t</bag>");
                }

            }

            aFile.WriteLine("");
            aFile.WriteLine("</class>");
            aFile.WriteLine("</hibernate-mapping>");
            aFile.Close();

        }


        private void GeraArquivoEntidades(string NomeNamespace, string caminho, string NomeDaTabela, bool RetiraS, bool PropriedadesAutomaticas)
        {


            TrataNome tratanome = new TrataNome();
            ITabelasECampos tabelasecampos = TabelaECamposFactoy.GetTabelasECampos(BancoDeDados);


            List<string> Listacampos = tabelasecampos.CamposDaTabela(NomeDaTabela);
            DirectoryInfo diretorio = new DirectoryInfo(caminho);

            if (!diretorio.Exists)
            {
                diretorio.Create();
            }

            StreamWriter aFile = new StreamWriter(caminho + "\\" + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(NomeDaTabela, RetiraS) + ".cs", false, Encoding.ASCII);

            GeraUsing(aFile);
            aFile.Write("using System.Collections.Generic;\n");
            aFile.Write("\n");
            aFile.Write("namespace " + NomeNamespace + "\n");

            aFile.Write("{\n");
            aFile.Write("    [Serializable]\n");
            aFile.Write("    public class " + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(NomeDaTabela, RetiraS) + "\n");
            aFile.Write("    {\n\n");



            /*Gera as variaveis privadas
             */
            this.GeraVariaveis(NomeDaTabela, tratanome, tabelasecampos, Listacampos, aFile, RetiraS, PropriedadesAutomaticas);

            /*Gera os Construtores
             */

            this.GeraConstrutores(NomeDaTabela, tratanome, tabelasecampos, Listacampos, aFile, RetiraS);

            /*Gera os Get e Set
             */
            this.GeraGetSet(NomeDaTabela, tratanome, tabelasecampos, Listacampos, aFile, RetiraS, PropriedadesAutomaticas);



            aFile.Write("    }\n");
            aFile.Write("}\n");
            aFile.Close();

        }


        private void GeraConstrutores(string NomeDaTabela, TrataNome tratanome, ITabelasECampos tabelasecampos, List<string> Listacampos, StreamWriter aFile, bool RetiraS)
        {

            aFile.Write("\n");
            aFile.Write("        public " + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(NomeDaTabela, RetiraS) + "()\n");
            aFile.Write("        {\n");
            aFile.Write("        }\n\n");

            aFile.Write("        public " + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(NomeDaTabela, RetiraS) + "(");
            string linha = string.Empty;
            var count = tabelasecampos.CamposChavesDaTabela(NomeDaTabela).Count;
            bool verificaparametro = true;
            bool verificathis = true;
            foreach (string campo in Listacampos)
            {
                if (count > 1 && verificaparametro)
                {
                    linha += "PK Pk, ";

                    verificaparametro = false;
                }

                if (count == 1 || !tabelasecampos.CampoChavesDaTabela(NomeDaTabela, campo))
                {


                    if (tabelasecampos.NomeDaTabelaDaChaveEstrgeira(NomeDaTabela, campo) != "NULL")
                    {
                        linha += tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(tabelasecampos.NomeDaTabelaDaChaveEstrgeira(NomeDaTabela, campo), RetiraS)
                            + " " + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(campo, RetiraS) + ", ";
                    }
                    else
                    {
                        linha += tabelasecampos.TipoDadoCSharp(NomeDaTabela, campo) + " " + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(campo, RetiraS) + ", ";
                    }
                }


            }

            linha = linha.Substring(0, linha.Length - 2);
            aFile.Write(linha);
            aFile.Write(")\n");
            aFile.Write("        {\n");


            foreach (string campo in Listacampos)
            {

                if (count > 1 && verificathis)
                {
                    aFile.Write("            this.Pk = Pk;\n");

                    verificathis = false;
                }

                if (count == 1 || !tabelasecampos.CampoChavesDaTabela(NomeDaTabela, campo))
                {

                    aFile.Write("            this." + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(campo, RetiraS) +
                        " = " + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(campo, RetiraS) + ";\n");
                }
            }

            aFile.Write("        }");


            aFile.Write("\n");

        }


        private void GeraUsing(StreamWriter aFile)
        {
            aFile.Write("using System;\n");

        }



        private void GeraGetSet(string NomeDaTabela, TrataNome tratanome, ITabelasECampos tabelasecampos, List<string> Listacampos, StreamWriter aFile, bool RetiraS, bool PropriedadesAutomaticas)
        {

            aFile.Write("\n");
            var count = tabelasecampos.CamposChavesDaTabela(NomeDaTabela).Count;
            if (count > 1)
            {
                aFile.Write("\t\tpublic virtual PK Pk");
                if (!PropriedadesAutomaticas)
                {
                    aFile.Write("\n");
                    aFile.WriteLine("\t\t{");
                    aFile.WriteLine("\t\t\tget { return this.pk; }");
                    aFile.WriteLine("\t\t\tset { pk = value; }");
                    aFile.WriteLine("\t\t}");
                    aFile.Write("\n");
                }
                else
                {
                    aFile.Write(" { get; set; }\n");
                }
            }


            foreach (string campos in Listacampos)
            {
                if (count == 1 || !tabelasecampos.CampoChavesDaTabela(NomeDaTabela, campos))
                {
                    string linha;
                    if (tabelasecampos.NomeDaTabelaDaChaveEstrgeira(NomeDaTabela, campos) != "NULL")
                    {
                        linha = "        public virtual " + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(tabelasecampos.NomeDaTabelaDaChaveEstrgeira(NomeDaTabela, campos), RetiraS) +
                            " " + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(campos.ToLower(), RetiraS);
                        aFile.Write(linha);
                        if (!PropriedadesAutomaticas)
                        {
                            aFile.Write("\n");
                            aFile.Write("        {\n");
                            aFile.Write("            get{ return this." + tratanome.ConverteParaMinusculoERetiraUnderline(campos.ToLower(), RetiraS) + "; }\n");
                            aFile.Write("            set{ this." + tratanome.ConverteParaMinusculoERetiraUnderline(campos.ToLower(), RetiraS) + " = value; }\n");
                            aFile.Write("        }\n");
                            aFile.Write("\n");
                        }
                        else
                        {
                            aFile.Write(" { get; set; }\n");

                        }

                    }
                    else
                    {

                        linha = "        public virtual " + tabelasecampos.TipoDadoCSharp(NomeDaTabela, campos) +
                            " " + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(campos.ToLower(), RetiraS);
                        aFile.Write(linha);
                        if (!PropriedadesAutomaticas)
                        {
                            aFile.Write("\n");
                            aFile.Write("        {\n");
                            aFile.Write("            get{ return this." + tratanome.ConverteParaMinusculoERetiraUnderline(campos.ToLower(), RetiraS) + "; }\n");
                            aFile.Write("            set{ this." + tratanome.ConverteParaMinusculoERetiraUnderline(campos.ToLower(), RetiraS) + " = value; }\n");
                            aFile.Write("        }\n");
                            aFile.Write("\n");
                        }
                        else
                        {
                            aFile.Write(" { get; set; }\n");

                        }
                    }

                }

            }
            string nomedatabelapassou = string.Empty;
            int i = 0;
            foreach (string tabela in tabelasecampos.TabelasOndeARelacionamento(NomeDaTabela))
            {
                if (nomedatabelapassou == tabela)
                {
                    i += 1;
                    nomedatabelapassou = tabela;
                }
                else
                {
                    i = 0;
                    nomedatabelapassou = tabela;
                }
                if (i > 0)
                {

                    aFile.Write("        public virtual IList<" + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(tabela, RetiraS) +
                         "> " + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(tabelasecampos.CamposChavesEstrageiraDaTabela(tabela)[i], RetiraS) + "List");

                    if (!PropriedadesAutomaticas)
                    {
                        aFile.Write("\n");
                        aFile.Write("        {\n");
                        aFile.Write("            get{ return this." + tratanome.ConverteParaMinusculoERetiraUnderline(tabelasecampos.CamposChavesEstrageiraDaTabela(tabela)[i], RetiraS) + "List; }\n");
                        aFile.Write("            set{ this." + tratanome.ConverteParaMinusculoERetiraUnderline(tabelasecampos.CamposChavesEstrageiraDaTabela(tabela)[i], RetiraS) + "List = value; }\n");
                        aFile.Write("        }\n");
                        aFile.Write("\n");
                    }
                    else
                    {
                        aFile.Write(" { get; set; }\n");

                    }

                }
                else
                {

                    aFile.Write("        public virtual IList<" + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(tabela, RetiraS) +
                    "> " + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(tabela, RetiraS) + "List");
                    if (!PropriedadesAutomaticas)
                    {
                        aFile.Write("\n");
                        aFile.Write("        {\n");
                        aFile.Write("            get{ return this." + tratanome.ConverteParaMinusculoERetiraUnderline(tabela, RetiraS) + "List; }\n");
                        aFile.Write("            set{ this." + tratanome.ConverteParaMinusculoERetiraUnderline(tabela, RetiraS) + "List = value; }\n");
                        aFile.Write("        }\n");
                        aFile.Write("\n");
                    }
                    else
                    {
                        aFile.Write(" { get; set; }\n");

                    }

                }

            }

            aFile.Write("\n");

        }

        private void GeraVariaveis(string NomeDaTabela, TrataNome tratanome, ITabelasECampos tabelasecampos, List<string> Listacampos, StreamWriter aFile, bool RetiraS, bool PropriedadesAutomaticas)
        {

            int count = tabelasecampos.CamposChavesDaTabela(NomeDaTabela).Count;
            if (count > 1)
            {

                GeraClassePK(NomeDaTabela, tratanome, tabelasecampos, aFile, RetiraS, PropriedadesAutomaticas);
                if (PropriedadesAutomaticas)
                    return;
                aFile.WriteLine("\t\tprivate PK pk;");
            }

            if (PropriedadesAutomaticas)
                return;
            foreach (string campos in Listacampos)
            {
                if (count == 1 || !tabelasecampos.CampoChavesDaTabela(NomeDaTabela, campos))
                {

                    string linha;
                    if (tabelasecampos.NomeDaTabelaDaChaveEstrgeira(NomeDaTabela, campos) != "NULL")
                    {

                        linha = "        private " + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(tabelasecampos.NomeDaTabelaDaChaveEstrgeira(NomeDaTabela, campos), RetiraS) +
                           " " + tratanome.ConverteParaMinusculoERetiraUnderline(campos.ToLower(), RetiraS) + ";\n";

                    }
                    else
                    {
                        linha = "        private " + tabelasecampos.TipoDadoCSharp(NomeDaTabela, campos) +
                           " " + tratanome.ConverteParaMinusculoERetiraUnderline(campos.ToLower(), RetiraS) + ";\n";
                    }
                    aFile.Write(linha);
                }
            }

            string nomedatabelapassou = string.Empty;
            int i = 0;
            foreach (string tabela in tabelasecampos.TabelasOndeARelacionamento(NomeDaTabela))
            {
                if (nomedatabelapassou == tabela)
                {
                    i += 1;
                    nomedatabelapassou = tabela;
                }
                else
                {
                    i = 0;
                    nomedatabelapassou = tabela;
                }



                if (i > 0)
                {
                    aFile.Write("        private IList<" + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(tabela, RetiraS) +
                       "> " + tratanome.ConverteParaMinusculoERetiraUnderline(tabelasecampos.CamposChavesEstrageiraDaTabela(tabela)[i], RetiraS) + "List;\n");

                }
                else
                {

                    aFile.Write("        private IList<" + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(tabela, RetiraS) +
                               "> " + tratanome.ConverteParaMinusculoERetiraUnderline(tabela, RetiraS) + "List;\n");

                }
            }

            aFile.Write("\n");

        }


        private void GeraClassePK(string NomeDaTabela, TrataNome tratanome, ITabelasECampos tabelasecampos, StreamWriter aFile, bool RetiraS, bool PropriedadesAutomaticas)
        {

            aFile.WriteLine("\t\t[Serializable]");
            aFile.WriteLine("\t\tpublic class PK");
            aFile.WriteLine("\t\t{");
            string linha = string.Empty;
            if (!PropriedadesAutomaticas)
            {
                foreach (string campos in tabelasecampos.CamposChavesDaTabela(NomeDaTabela))
                {
                    if (tabelasecampos.NomeDaTabelaDaChaveEstrgeira(NomeDaTabela, campos) != "NULL")
                    {

                        linha = "\t\t\tprivate " + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(tabelasecampos.NomeDaTabelaDaChaveEstrgeira(NomeDaTabela, campos), RetiraS) +
                           " " + tratanome.ConverteParaMinusculoERetiraUnderline(campos.ToLower(), RetiraS) + ";\n";
                    }
                    else
                    {
                        linha = "\t\t\tprivate " + tabelasecampos.TipoDadoCSharp(NomeDaTabela, campos) +
                           " " + tratanome.ConverteParaMinusculoERetiraUnderline(campos.ToLower(), RetiraS) + ";\n";
                    }
                    aFile.Write(linha);
                }
                aFile.Write("\n");
            }


            //Contrutores

            aFile.Write("\t        public PK()\n");
            aFile.Write("\t        {\n");
            aFile.Write("\t        }\n\n");

            aFile.Write("\t        public PK(");
            linha = string.Empty;
            foreach (string campo in tabelasecampos.CamposChavesDaTabela(NomeDaTabela))
            {

                if (tabelasecampos.NomeDaTabelaDaChaveEstrgeira(NomeDaTabela, campo) != "NULL")
                {
                    linha += tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(tabelasecampos.NomeDaTabelaDaChaveEstrgeira(NomeDaTabela, campo), RetiraS)
                        + " " + tratanome.ConverteParaMinusculoERetiraUnderline(campo, RetiraS) + ", ";
                }
                else
                {
                    linha += tabelasecampos.TipoDadoCSharp(NomeDaTabela, campo) + " " + tratanome.ConverteParaMinusculoERetiraUnderline(campo, RetiraS) + ", ";
                }

            }

            linha = linha.Substring(0, linha.Length - 2);
            aFile.Write(linha);
            aFile.Write(")\n");
            aFile.Write("\t        {\n");


            foreach (string campo in tabelasecampos.CamposChavesDaTabela(NomeDaTabela))
            {
                aFile.Write("\t            this." + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(campo, RetiraS) +
                       " = " + tratanome.ConverteParaMinusculoERetiraUnderline(campo, RetiraS) + ";\n");
            }

            aFile.Write("\t        }\n\n");

            linha = string.Empty;
            foreach (string campos in tabelasecampos.CamposChavesDaTabela(NomeDaTabela))
            {
                if (tabelasecampos.NomeDaTabelaDaChaveEstrgeira(NomeDaTabela, campos) != "NULL")
                {
                    linha = "\t        public virtual " + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(tabelasecampos.NomeDaTabelaDaChaveEstrgeira(NomeDaTabela, campos), RetiraS) +
                        " " + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(campos.ToLower(), RetiraS);
                    aFile.Write(linha);
                    if (!PropriedadesAutomaticas)
                    {
                        aFile.Write("\n");
                        aFile.Write("\t        {\n");
                        aFile.Write("\t            get{ return this." + tratanome.ConverteParaMinusculoERetiraUnderline(campos.ToLower(), RetiraS) + "; }\n");
                        aFile.Write("\t            set{ this." + tratanome.ConverteParaMinusculoERetiraUnderline(campos.ToLower(), RetiraS) + " = value; }\n");
                        aFile.Write("\t        }\n");
                        aFile.Write("\n");
                    }
                    else
                    {
                        aFile.Write(" { get; set; }\n");
                    }

                }
                else
                {

                    linha = "\t        public virtual " + tabelasecampos.TipoDadoCSharp(NomeDaTabela, campos) +
                        " " + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(campos.ToLower(), RetiraS);
                    aFile.Write(linha);
                    if (!PropriedadesAutomaticas)
                    {
                        aFile.Write("\n");
                        aFile.Write("\t        {\n");
                        aFile.Write("\t            get{ return this." + tratanome.ConverteParaMinusculoERetiraUnderline(campos.ToLower(), RetiraS) + "; }\n");
                        aFile.Write("\t            set{ this." + tratanome.ConverteParaMinusculoERetiraUnderline(campos.ToLower(), RetiraS) + " = value; }\n");
                        aFile.Write("\t        }\n");
                        aFile.Write("\n");
                    }
                    else
                    {
                        aFile.Write(" { get; set; }\n");
                    }

                }

            }
            aFile.Write("\n\n");
            aFile.WriteLine("\t\t\tpublic override bool Equals(object obj)");
            aFile.WriteLine("\t\t\t{");
            aFile.WriteLine("\t\t\t    return base.Equals(obj);");
            aFile.WriteLine("\t\t\t}\n");
            aFile.WriteLine("\t\t\tpublic override int GetHashCode()");
            aFile.WriteLine("\t\t\t{");
            aFile.WriteLine("\t\t\t    return base.GetHashCode();");
            aFile.WriteLine("\t\t\t}\n");
            aFile.WriteLine("\t\t}\n");
        }


        public void GeraArquivo(ProgressBar progressBar, List<string> ListaTabelas)
        {
            ITabelasECampos tabelasECamposSqlServer = TabelaECamposFactoy.GetTabelasECampos(BancoDeDados);

            progressBar.Maximum = ListaTabelas.Count;


            string NomeAssembly = string.Empty;


            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = GerarArquivoCSharp.CaminhoInicial;
            openFileDialog.Filter = "csproj files (*.csproj)|*.csproj";
            openFileDialog.RestoreDirectory = true;
            string caminho;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                caminho = openFileDialog.FileName;


                string NomeDoArquivo = @"\" + openFileDialog.SafeFileName.ToString();
                string Nomenamespace = NomeDoArquivo.Substring(1, NomeDoArquivo.Length - 8);



                caminho = caminho.Substring(0, caminho.Length - (7 + Nomenamespace.Length));

                GerarArquivoCSharp.CaminhoInicial = caminho;

                DirectoryInfo diretorio = new DirectoryInfo(caminho + @"\DLL NHibernate");

                if (!diretorio.Exists)
                {
                    diretorio.Create();
                }



                string[] files = System.IO.Directory.GetFiles(Application.StartupPath + @"\DLL NHibernate");
                string fileName;
                string destFile;

                foreach (string s in files)
                {

                    fileName = System.IO.Path.GetFileName(s);
                    destFile = System.IO.Path.Combine(caminho + @"\DLL NHibernate", fileName);
                    System.IO.File.Copy(s, destFile, true);
                }

                string caminhoEntidades = caminho + @"\Entidades";
                string caminhoMapeamentos = caminho + @"\Mapeamentos";
                string caminhoConexao = caminho + @"\Conexao";
                string caminhoRepositorios = caminho + @"\Repositorios";
                string caminhoGeral = caminho + @"\Geral";
                string caminhoNHibernateHelp = caminho + @"\NHibernateHelper";

                IncluirDLLNoProjeto(caminho + NomeDoArquivo);
                if (this.GeraNHibernateHelper)
                {
                    this.ClasseNHibernateHelp(Nomenamespace, caminhoNHibernateHelp);
                    this.IncluirArquivoNoProjeto(caminho + NomeDoArquivo, @"NHibernateHelper\" + "NHibernateHelper.cs", true, "Compile", ref NomeAssembly);

                    this.GeraArquivoXMLDeConfigNHibernate(Nomenamespace, caminho, this.RetiraS, ListaTabelas);
                    this.IncluirArquivoNoProjeto(caminho + NomeDoArquivo, "hibernate.cfg.xml", true, "Content", ref NomeAssembly);
                }

                TrataNome tratanome = new TrataNome();
                foreach (string tabela in ListaTabelas)
                {
                    if (this.GeraEntidades)
                    {
                        //Gera os arquivos de class
                        this.GeraArquivoEntidades(Nomenamespace, caminhoEntidades, tabela, this.RetiraS, this.PropriedadesAutomaticas);
                        //Incluir os arquivos no projeto
                        this.IncluirArquivoNoProjeto(caminho + NomeDoArquivo,
                            @"Entidades\" + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(tabela, this.RetiraS) + ".cs", this.GeraEntidades, "Compile", ref NomeAssembly);
                        this.GeraArquivoXMLDaEntidades(Nomenamespace, caminhoMapeamentos, tabela, this.Lazy, NomeAssembly, this.RetiraS);
                        this.IncluirArquivoNoProjeto(caminho + NomeDoArquivo,
                            @"Mapeamentos\" + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(tabela, this.RetiraS) + ".hbm.xml", this.GeraEntidades, "EmbeddedResource", ref NomeAssembly);
                    }

                    if (this.GeraclassesRepositorio)
                    {
                        this.IncluirArquivoNoProjeto(caminho + NomeDoArquivo,
                            @"Repositorios\" + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(tabela, this.RetiraS) + "Repositorios.cs", this.GeraclassesRepositorio, "Compile", ref NomeAssembly);
                        this.GeraArquivosRepositorios(Nomenamespace, caminhoRepositorios, tabela, this.RetiraS);
                    }
                    progressBar.Value += 1;
                }
                MessageBox.Show("Operação Realizada com Sucesso!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }

            progressBar.Value = 0;
        }

        private void GeraArquivosRepositorios(string NomeNamespace, string caminho, string NomeDaTabela, bool RetiraS)
        {
            TrataNome tratanome = new TrataNome();
            ITabelasECampos tabelasecampos = TabelaECamposFactoy.GetTabelasECampos(BancoDeDados);

            DirectoryInfo diretorio = new DirectoryInfo(caminho);

            if (!diretorio.Exists)
            {
                diretorio.Create();
            }
            StreamWriter aFile = new StreamWriter(caminho + "\\" + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(NomeDaTabela, RetiraS) + "Repositorios.cs", false, Encoding.ASCII);

            GeraUsing(aFile);
            aFile.Write("using System.Data.SqlClient;\n");
            aFile.Write("using System.Collections.Generic;\n");
            aFile.Write("using System.Collections;\n");
            aFile.Write("using NHibernate;\n");
            aFile.Write("using System.Linq;\n");
            aFile.Write("using NHibernate.Linq;\n\n");


            aFile.Write("\n");
            aFile.Write("namespace " + NomeNamespace + "\n");
            aFile.Write("{\n");
            aFile.Write("    public class " + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(NomeDaTabela, RetiraS) + "Repositorios \n");
            aFile.Write("    {\n\n");


            /*Método responsalver por gera o salvar
             **/
            this.GeraMetodoSalvar(NomeDaTabela, tratanome, aFile, RetiraS);


            if (!string.IsNullOrEmpty(tabelasecampos.CampoChavesDaTabela(NomeDaTabela)))
            {

                /*Método responsalver por gera o atualizar
                **/
                this.GeraMetodoAtualizar(NomeDaTabela, tratanome, aFile, RetiraS);

                /*Método responsalver por gera o deletar
                **/
                this.GeraMetodoDeletar(NomeDaTabela, tratanome, aFile, RetiraS);

                /*Método responsalver por gera get
                **/
                this.GeraMetodoGetKey(NomeDaTabela, tratanome, tabelasecampos, aFile, RetiraS);
            }
            this.GeraMetodoGet(NomeDaTabela, tratanome, aFile, RetiraS);

            aFile.Write("    }\n");
            aFile.Write("}\n");

            aFile.Close();


        }




        public void GeraMetodoGetKey(string NomeDaTabela, TrataNome tratanome, ITabelasECampos tabelasecampos, StreamWriter aFile, bool RetiraS)
        {

            aFile.Write("\n");

            aFile.Write("       public " + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(NomeDaTabela, RetiraS) + " Get" +
                tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(NomeDaTabela, RetiraS) + "(object id)\n");
            aFile.Write("       {\n");

            aFile.Write("           using (ISession session = NHibernateHelper.OpenSession())\n");
            aFile.Write("           {\n");

            aFile.Write("               " + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(NomeDaTabela, RetiraS)
                + " toReturn =  session.Get(typeof("
                + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(NomeDaTabela, RetiraS) + "), id) as " +
                tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(NomeDaTabela, RetiraS) + ";\n");
            aFile.Write("               session.Close();\n");
            aFile.Write("               return toReturn;\n");
            aFile.Write("           }\n");


            aFile.Write("       }\n");
            aFile.Write("\n");

            aFile.Write("       public " + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(NomeDaTabela, RetiraS) + " Get" +
                tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(NomeDaTabela, RetiraS) + "(object id, ISession session)\n");
            aFile.Write("       {\n");


            aFile.Write("           return  session.Get(typeof("
                + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(NomeDaTabela, RetiraS) + "), id) as " +
                  tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(NomeDaTabela, RetiraS) + ";\n");

            aFile.Write("       }\n");
            aFile.Write("\n");
        }

        private void GeraMetodoGet(string NomeDaTabela, TrataNome tratanome, StreamWriter aFile, bool RetiraS)
        {
            aFile.Write("\n");

            aFile.Write("       public virtual IList<" +
                tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(NomeDaTabela, RetiraS) + "> Get" +
                tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(NomeDaTabela, RetiraS) + "()\n");
            aFile.Write("       {\n");

            aFile.Write("           using (ISession session = NHibernateHelper.OpenSession())\n");
            aFile.Write("           {\n");

            aFile.Write(@"               return (from " + tratanome.ConverteParaMinusculoERetiraUnderline(NomeDaTabela, RetiraS)
                + " in session.Linq<" +
                tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(NomeDaTabela, RetiraS) + ">()"
                + "\n \t\t\t\t\t  select " +
                tratanome.ConverteParaMinusculoERetiraUnderline(NomeDaTabela, RetiraS) + ").ToList();\n");
            aFile.Write("           }\n");
            aFile.Write("       }\n");
            aFile.Write("\n");

            aFile.Write("       public virtual IList<" +
                tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(NomeDaTabela, RetiraS) + "> Get" +
                tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(NomeDaTabela, RetiraS) + "(ISession session)\n");
            aFile.Write("       {\n");

            aFile.Write(@"          return (from " +
                tratanome.ConverteParaMinusculoERetiraUnderline(NomeDaTabela, RetiraS) + " in session.Linq<" +
                tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(NomeDaTabela, RetiraS) + ">()"
                + "\n \t\t\t\t  select " +
                tratanome.ConverteParaMinusculoERetiraUnderline(NomeDaTabela, RetiraS) + ").ToList();\n"); aFile.Write("       }\n");
            aFile.Write("\n");


            aFile.Write("\n");

        }

        private void GeraMetodoSalvar(string NomeDaTabela, TrataNome tratanome, StreamWriter aFile, bool RetiraS)
        {
            aFile.Write("       public virtual void Save(" + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(NomeDaTabela, RetiraS) + " " +
                tratanome.ConverteParaMinusculoERetiraUnderline(NomeDaTabela, RetiraS) + ")\n");
            aFile.Write("       {\n");
            aFile.Write("           using (ISession session = NHibernateHelper.OpenSession())\n");
            aFile.Write("           {\n");
            aFile.Write("               using (ITransaction transaction = session.BeginTransaction())\n");
            aFile.Write("               {\n");
            aFile.Write("                   try\n");
            aFile.Write("                   {\n");
            aFile.Write("                       session.Save(" + tratanome.ConverteParaMinusculoERetiraUnderline(NomeDaTabela, RetiraS) + ");\n");
            aFile.Write("                       transaction.Commit();\n");
            aFile.Write("                       session.Flush();\n");
            aFile.Write("                       session.Close();\n");
            aFile.Write("                   }\n");
            aFile.Write("                   catch (NHibernate.HibernateException)\n");
            aFile.Write("                   {\n");
            aFile.Write("                       transaction.Rollback();\n");
            aFile.Write("                   }\n");
            aFile.Write("               }\n");
            aFile.Write("           }\n");
            aFile.Write("       }\n");
            aFile.Write("\n");

            aFile.Write("       public virtual void Save(" + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(NomeDaTabela, RetiraS) + " " +
                tratanome.ConverteParaMinusculoERetiraUnderline(NomeDaTabela, RetiraS) + ", ISession session)\n");
            aFile.Write("       {\n");
            aFile.Write("           session.Save(" + tratanome.ConverteParaMinusculoERetiraUnderline(NomeDaTabela, RetiraS) + ");\n");
            aFile.Write("       }\n");
            aFile.Write("\n");


        }

        private void GeraMetodoAtualizar(string NomeDaTabela, TrataNome tratanome, StreamWriter aFile, bool RetiraS)
        {
            aFile.Write("       public virtual void Update(" + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(NomeDaTabela, RetiraS) + " " +
                tratanome.ConverteParaMinusculoERetiraUnderline(NomeDaTabela, RetiraS) + ")\n");
            aFile.Write("       {\n");
            aFile.Write("           using (ISession session = NHibernateHelper.OpenSession())\n");
            aFile.Write("           {\n");
            aFile.Write("               using (ITransaction transaction = session.BeginTransaction())\n");
            aFile.Write("               {\n");
            aFile.Write("                   try\n");
            aFile.Write("                   {\n");
            aFile.Write("                       session.Update(" + tratanome.ConverteParaMinusculoERetiraUnderline(NomeDaTabela, RetiraS) + ");\n");
            aFile.Write("                       transaction.Commit();\n");
            aFile.Write("                       session.Flush();\n");
            aFile.Write("                       session.Close();\n");
            aFile.Write("                   }\n");
            aFile.Write("                   catch (NHibernate.HibernateException)\n");
            aFile.Write("                   {\n");
            aFile.Write("                       transaction.Rollback();\n");
            aFile.Write("                   }\n");
            aFile.Write("               }\n");
            aFile.Write("           }\n");
            aFile.Write("       }\n");
            aFile.Write("\n");

            aFile.Write("       public virtual void Update(" + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(NomeDaTabela, RetiraS) + " " +
                tratanome.ConverteParaMinusculoERetiraUnderline(NomeDaTabela, RetiraS) + ", ISession session)\n");
            aFile.Write("       {\n");
            aFile.Write("           session.Update(" + tratanome.ConverteParaMinusculoERetiraUnderline(NomeDaTabela, RetiraS) + ");\n");
            aFile.Write("       }\n");
            aFile.Write("\n");
        }



        private void GeraMetodoDeletar(string NomeDaTabela, TrataNome tratanome, StreamWriter aFile, bool RetiraS)
        {
            aFile.Write("       public virtual void Delete(" + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(NomeDaTabela, RetiraS) + " " +
                tratanome.ConverteParaMinusculoERetiraUnderline(NomeDaTabela, RetiraS) + ")\n");
            aFile.Write("       {\n");
            aFile.Write("           using (ISession session = NHibernateHelper.OpenSession())\n");
            aFile.Write("           {\n");
            aFile.Write("               using (ITransaction transaction = session.BeginTransaction())\n");
            aFile.Write("               {\n");
            aFile.Write("                   try\n");
            aFile.Write("                   {\n");
            aFile.Write("                       session.Delete(" + tratanome.ConverteParaMinusculoERetiraUnderline(NomeDaTabela, RetiraS) + ");\n");
            aFile.Write("                       transaction.Commit();\n");
            aFile.Write("                       session.Flush();\n");
            aFile.Write("                       session.Close();\n");
            aFile.Write("                   }\n");
            aFile.Write("                   catch (NHibernate.HibernateException)\n");
            aFile.Write("                   {\n");
            aFile.Write("                       transaction.Rollback();\n");
            aFile.Write("                   }\n");
            aFile.Write("               }\n");
            aFile.Write("           }\n");
            aFile.Write("       }\n");
            aFile.Write("\n");

            aFile.Write("       public virtual void Delete(" + tratanome.ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(NomeDaTabela, RetiraS) + " " +
                tratanome.ConverteParaMinusculoERetiraUnderline(NomeDaTabela, RetiraS) + ", ISession session)\n");
            aFile.Write("       {\n");
            aFile.Write("           session.Delete(" + tratanome.ConverteParaMinusculoERetiraUnderline(NomeDaTabela, RetiraS) + ");\n");
            aFile.Write("       }\n");
            aFile.Write("\n");
        }



        private void ClasseNHibernateHelp(string NomeNamespace, string caminho)
        {
            DirectoryInfo diretorio = new DirectoryInfo(caminho);

            if (!diretorio.Exists)
            {
                diretorio.Create();
            }

            StreamWriter aFile = new StreamWriter(caminho + "\\" + "NHibernateHelper.cs", false, Encoding.ASCII);

            aFile.WriteLine("using NHibernate.Cfg;");
            aFile.WriteLine("using NHibernate;");
            aFile.WriteLine("");

            aFile.WriteLine("namespace " + NomeNamespace);
            aFile.WriteLine("{");
            aFile.WriteLine("\tpublic class NHibernateHelper");
            aFile.WriteLine("\t{");
            aFile.WriteLine("\t\tprivate static ISessionFactory sessionFactory;");
            aFile.WriteLine("");
            aFile.WriteLine("");
            aFile.WriteLine("\t\tprivate NHibernateHelper()");
            aFile.WriteLine("\t\t{");
            aFile.WriteLine("\t\t}");
            aFile.WriteLine("");
            aFile.WriteLine("\t\tpublic static ISession OpenSession()");
            aFile.WriteLine("\t\t{");
            aFile.WriteLine("\t\t\treturn SessionFactory().OpenSession();");
            aFile.WriteLine("\t\t}");

            aFile.WriteLine("\t\tprivate static ISessionFactory SessionFactory()");
            aFile.WriteLine("\t\t{");
            aFile.WriteLine("\t\t\tif (sessionFactory != null)");
            aFile.WriteLine("\t\t\t{");
            aFile.WriteLine("\t\t\t\treturn sessionFactory;");
            aFile.WriteLine("\t\t\t}");

            aFile.WriteLine("\t\t\tvar config = new Configuration().Configure();");
            aFile.WriteLine("\t\t\tsessionFactory = config.BuildSessionFactory();");
            aFile.WriteLine("");
            aFile.WriteLine("\t\t\treturn sessionFactory;");
            aFile.WriteLine("\t\t}");
            aFile.WriteLine("\t}");
            aFile.WriteLine("}");
            aFile.Close();

        }

        private void IncluirDLLNoProjeto(string caminho)
        {
            string filename = caminho;
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            XmlNodeList nosItemGroup = doc.GetElementsByTagName("ItemGroup");
            XmlNode no = nosItemGroup[0];

            string valor = @"Antlr3.Runtime, Version=3.1.0.39271, Culture=neutral, PublicKeyToken=3a9cab8f8d22bfb7, processorArchitecture=MSIL";

            XmlNodeList noReference = doc.GetElementsByTagName("Reference");
            foreach (XmlNode i in noReference)
            {
                if (i.Attributes["Include"].Value == valor)
                    return;
            }


            XmlNode novono = doc.CreateElement("Reference", no.NamespaceURI);
            XmlAttribute novoAtrib = doc.CreateAttribute("Include");
            novoAtrib.Value = "Antlr3.Runtime, Version=3.1.0.39271, Culture=neutral, PublicKeyToken=3a9cab8f8d22bfb7, processorArchitecture=MSIL";
            novono.Attributes.Append(novoAtrib);
            XmlNode noFilho = doc.CreateElement("SpecificVersion", no.NamespaceURI);
            noFilho.InnerText = "False";
            XmlNode noNeto = doc.CreateElement("HintPath", no.NamespaceURI);
            noNeto.InnerText = @"DLL NHibernate\Antlr3.Runtime.dll";
            novono.AppendChild(noFilho);
            novono.AppendChild(noNeto);
            no.AppendChild(novono);


            XmlNode novono1 = doc.CreateElement("Reference", no.NamespaceURI);
            XmlAttribute novoAtrib1 = doc.CreateAttribute("Include");
            novoAtrib1.Value = "Castle.Core, Version=1.1.0.0, Culture=neutral, PublicKeyToken=407dd0808d44fbdc, processorArchitecture=MSIL";
            novono1.Attributes.Append(novoAtrib1);
            XmlNode noFilho1 = doc.CreateElement("SpecificVersion", no.NamespaceURI);
            noFilho1.InnerText = "False";
            XmlNode noNeto1 = doc.CreateElement("HintPath", no.NamespaceURI);
            noNeto1.InnerText = @"DLL NHibernate\Castle.Core.dll";
            novono1.AppendChild(noFilho1);
            novono1.AppendChild(noNeto1);
            no.AppendChild(novono1);


            XmlNode novono2 = doc.CreateElement("Reference", no.NamespaceURI);
            XmlAttribute novoAtrib2 = doc.CreateAttribute("Include");
            novoAtrib2.Value = "Castle.DynamicProxy2, Version=2.1.0.0, Culture=neutral, PublicKeyToken=407dd0808d44fbdc, processorArchitecture=MSIL";
            novono2.Attributes.Append(novoAtrib2);
            XmlNode noFilho2 = doc.CreateElement("SpecificVersion", no.NamespaceURI);
            noFilho2.InnerText = "False";
            XmlNode noNeto2 = doc.CreateElement("HintPath", no.NamespaceURI);
            noNeto2.InnerText = @"DLL NHibernate\Castle.DynamicProxy2.dll";
            novono2.AppendChild(noFilho2);
            novono2.AppendChild(noNeto2);
            no.AppendChild(novono2);


            XmlNode novono3 = doc.CreateElement("Reference", no.NamespaceURI);
            XmlAttribute novoAtrib3 = doc.CreateAttribute("Include");
            novoAtrib3.Value = "Iesi.Collections, Version=1.0.1.0, Culture=neutral, PublicKeyToken=aa95f207798dfdb4, processorArchitecture=MSIL";
            novono3.Attributes.Append(novoAtrib3);
            XmlNode noFilho3 = doc.CreateElement("SpecificVersion", no.NamespaceURI);
            noFilho3.InnerText = "False";
            XmlNode noNeto3 = doc.CreateElement("HintPath", no.NamespaceURI);
            noNeto3.InnerText = @"DLL NHibernate\Iesi.Collections.dll";
            novono3.AppendChild(noFilho3);
            novono3.AppendChild(noNeto3);
            no.AppendChild(novono3);


            XmlNode novono4 = doc.CreateElement("Reference", no.NamespaceURI);
            XmlAttribute novoAtrib4 = doc.CreateAttribute("Include");
            novoAtrib4.Value = "LinFu.DynamicProxy, Version=1.0.3.14911, Culture=neutral, PublicKeyToken=62a6874124340d6e, processorArchitecture=MSIL";
            novono4.Attributes.Append(novoAtrib4);
            XmlNode noFilho4 = doc.CreateElement("SpecificVersion", no.NamespaceURI);
            noFilho4.InnerText = "False";
            XmlNode noNeto4 = doc.CreateElement("HintPath", no.NamespaceURI);
            noNeto4.InnerText = @"DLL NHibernate\LinFu.DynamicProxy.dll";
            novono4.AppendChild(noFilho4);
            novono4.AppendChild(noNeto4);
            no.AppendChild(novono4);


            XmlNode novono5 = doc.CreateElement("Reference", no.NamespaceURI);
            XmlAttribute novoAtrib5 = doc.CreateAttribute("Include");
            novoAtrib5.Value = "log4net, Version=1.2.10.0, Culture=neutral, PublicKeyToken=1b44e1d426115821, processorArchitecture=MSIL";
            novono5.Attributes.Append(novoAtrib5);
            XmlNode noFilho5 = doc.CreateElement("SpecificVersion", no.NamespaceURI);
            noFilho5.InnerText = "False";
            XmlNode noNeto5 = doc.CreateElement("HintPath", no.NamespaceURI);
            noNeto5.InnerText = @"DLL NHibernate\log4net.dll";
            novono5.AppendChild(noFilho5);
            novono5.AppendChild(noNeto5);
            no.AppendChild(novono5);


            XmlNode novono6 = doc.CreateElement("Reference", no.NamespaceURI);
            XmlAttribute novoAtrib6 = doc.CreateAttribute("Include");
            novoAtrib6.Value = "NHibernate, Version=2.1.0.4000, Culture=neutral, PublicKeyToken=aa95f207798dfdb4, processorArchitecture=MSIL";
            novono6.Attributes.Append(novoAtrib6);
            XmlNode noFilho6 = doc.CreateElement("SpecificVersion", no.NamespaceURI);
            noFilho6.InnerText = "False";
            XmlNode noNeto6 = doc.CreateElement("HintPath", no.NamespaceURI);
            noNeto6.InnerText = @"DLL NHibernate\NHibernate.dll";
            novono6.AppendChild(noFilho6);
            novono6.AppendChild(noNeto6);
            no.AppendChild(novono6);


            XmlNode novono7 = doc.CreateElement("Reference", no.NamespaceURI);
            XmlAttribute novoAtrib7 = doc.CreateAttribute("Include");
            novoAtrib7.Value = "NHibernate.ByteCode.Castle, Version=2.1.2.4000, Culture=neutral, PublicKeyToken=aa95f207798dfdb4, processorArchitecture=MSIL";
            novono7.Attributes.Append(novoAtrib7);
            XmlNode noFilho7 = doc.CreateElement("SpecificVersion", no.NamespaceURI);
            noFilho7.InnerText = "False";
            XmlNode noNeto7 = doc.CreateElement("HintPath", no.NamespaceURI);
            noNeto7.InnerText = @"DLL NHibernate\NHibernate.ByteCode.Castle.dll";
            novono7.AppendChild(noFilho7);
            novono7.AppendChild(noNeto7);
            no.AppendChild(novono7);

            XmlNode novono8 = doc.CreateElement("Reference", no.NamespaceURI);
            XmlAttribute novoAtrib8 = doc.CreateAttribute("Include");
            novoAtrib8.Value = "NHibernate.ByteCode.LinFu, Version=2.1.0.4000, Culture=neutral, PublicKeyToken=aa95f207798dfdb4, processorArchitecture=MSIL";
            novono8.Attributes.Append(novoAtrib8);
            XmlNode noFilho8 = doc.CreateElement("SpecificVersion", no.NamespaceURI);
            noFilho8.InnerText = "False";
            XmlNode noNeto8 = doc.CreateElement("HintPath", no.NamespaceURI);
            noNeto8.InnerText = @"DLL NHibernate\NHibernate.ByteCode.LinFu.dll";
            novono8.AppendChild(noFilho8);
            novono8.AppendChild(noNeto8);
            no.AppendChild(novono8);


            XmlNode novono9 = doc.CreateElement("Reference", no.NamespaceURI);
            XmlAttribute novoAtrib9 = doc.CreateAttribute("Include");
            novoAtrib9.Value = "NHibernate.Linq, Version=1.0.0.4000, Culture=neutral, PublicKeyToken=444cf6a87fdab271, processorArchitecture=MSIL";
            novono9.Attributes.Append(novoAtrib9);
            XmlNode noFilho9 = doc.CreateElement("SpecificVersion", no.NamespaceURI);
            noFilho9.InnerText = "False";
            XmlNode noNeto9 = doc.CreateElement("HintPath", no.NamespaceURI);
            noNeto9.InnerText = @"DLL NHibernate\NHibernate.Linq.dll";
            novono9.AppendChild(noFilho9);
            novono9.AppendChild(noNeto9);
            no.AppendChild(novono9);


            doc.Save(filename);




        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="caminho"></param>
        /// <param name="arquivo"></param>
        private void IncluirArquivoNoProjeto(string caminho, string arquivo, bool chk, string NomedoNoFilho, ref string NomeAssembly)
        {
            if (chk)
            {
                string filename = caminho;

                XmlDocument doc = new XmlDocument();
                doc.Load(filename);

                XmlNodeList nosItemGroup = doc.GetElementsByTagName("ItemGroup");
                //Recupera o nome do assembly
                if (NomeAssembly == string.Empty)
                {
                    XmlNodeList nosPropertyGroup = doc.GetElementsByTagName("AssemblyName");

                    foreach (XmlNode no in nosPropertyGroup)
                    {
                        NomeAssembly = no.InnerXml;
                    }
                }

                foreach (XmlNode no in nosItemGroup)
                {

                    /*para não incluir um arquivo duas vezes no projeto!!!*/
                    bool entra = true;

                    XmlNodeList node = doc.GetElementsByTagName(NomedoNoFilho);
                    foreach (XmlNode i in node)
                    {
                        if (i.Attributes["Include"].Value == arquivo)
                            entra = false;
                    }
                    if (entra)
                    {
                        /*para incluir um arquivo no projeto!!!*/
                        XmlNode novono = doc.CreateElement(NomedoNoFilho, no.NamespaceURI);
                        XmlAttribute novoAtrib = doc.CreateAttribute("Include");
                        novoAtrib.Value = arquivo;
                        novono.Attributes.Append(novoAtrib);
                        if (NomedoNoFilho == "Content")
                        {
                            XmlNode noFilho = doc.CreateElement("CopyToOutputDirectory", no.NamespaceURI);
                            noFilho.InnerText = "Always";

                            novono.AppendChild(noFilho);
                        }
                        no.AppendChild(novono);
                        doc.Save(filename);
                    }

                }
            }
        }
    }
}
