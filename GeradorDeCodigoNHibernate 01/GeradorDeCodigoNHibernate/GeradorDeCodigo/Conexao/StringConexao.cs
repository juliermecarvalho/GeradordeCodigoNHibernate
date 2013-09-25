namespace GeradorDeCodigo
{
    public class StringConexao
    {
        private static readonly StringConexao instance = new StringConexao();
        private static string stringconexao;

        public static string Stringconexao
        {
            get { return StringConexao.stringconexao; }
        }

        private StringConexao()
        {
        }

        public static StringConexao GetInstance(string Source)
        {
            stringconexao = @"Data Source=" + Source + ";Integrated Security=True";
            return instance;
        }
        public static StringConexao GetInstance(string Source, string Catalog)
        {
            stringconexao = @"Data Source=" + Source + ";Initial Catalog=" + Catalog + ";Integrated Security=True";
            return instance;
        }
        public static StringConexao GetInstance(string Source, string ID, string Password)
        {
            stringconexao = @"Data Source=" + Source + ";User ID=" + ID + ";Password=" + Password;
            return instance;
        }
        public static StringConexao GetInstance(string Source, string Catalog, string ID, string Password)
        {
            stringconexao = @"Data Source=" + Source + ";Initial Catalog=" + Catalog + ";User ID=" + ID + ";Password=" + Password;
            return instance;
        }

    }
}
