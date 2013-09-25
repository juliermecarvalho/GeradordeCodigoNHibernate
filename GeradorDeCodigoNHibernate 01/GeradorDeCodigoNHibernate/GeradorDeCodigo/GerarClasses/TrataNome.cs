namespace GeradorDeCodigo.GerarClasses
{
    public class TrataNome
    {

        public TrataNome()
        {
        }

        /// <summary>
        /// Converte a primeira letra para Maisculo independe se 
        /// se estiver tudo maisculo ou minusculo.
        /// </summary>
        /// <param name="Palavra"></param>
        /// <returns></returns>
        public string ConverteAPrimeiraLetraParaMaisculo(string Palavra, bool RetiraS)
        {
            if (RetiraS)
            {
                Palavra = RetiraSNoFim(Palavra);
            }

            Palavra = Palavra.Replace(" ", "");
            System.Globalization.CultureInfo cultureinfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            Palavra = Palavra.ToLower();
            Palavra = cultureinfo.TextInfo.ToTitleCase(Palavra);
            return Palavra;
        }
        /// <summary>
        /// Converte a primeira letra para maisculo retirando o Underline
        /// e colocado a proxima com maiuscula
        /// </summary>
        /// <returns></returns>
        public string ConverteAPrimeiraLetraParaMaisculoERetiraUnderline(string Palavra, bool RetiraS)
        {
            if (RetiraS)
            {
                Palavra = RetiraSNoFim(Palavra);
            }

            Palavra = Palavra.Replace(" ", "");
            System.Globalization.CultureInfo cultureinfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            Palavra = Palavra.ToLower();
            Palavra = Palavra.Replace("_", " ");
            Palavra = cultureinfo.TextInfo.ToTitleCase(Palavra);
            Palavra = Palavra.Replace(" ", string.Empty);
            return Palavra;
        }


        public string ConverteParaMinusculoERetiraUnderline(string Palavra, bool RetiraS)
        {
            if (RetiraS)
            {
                Palavra = RetiraSNoFim(Palavra);
            }

            Palavra = Palavra.Replace(" ", "");
            Palavra = Palavra.ToLower();
            Palavra = Palavra.Replace("_", string.Empty);
            return Palavra;
        }

        private string RetiraSNoFim(string Palavra)
        {
            if (!string.IsNullOrEmpty(Palavra))
            {
                if (Palavra.Substring(Palavra.Length - 1, 1) == "s" ||
                    Palavra.Substring(Palavra.Length - 1, 1) == "S")
                {
                    Palavra = Palavra.Remove(Palavra.Length - 1);
                }
            }
            return Palavra;

        }

    }


}
