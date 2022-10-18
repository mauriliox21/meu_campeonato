using System.Data;

namespace Biblioteca
{
    public class UtilDataTable
    {
        public static string CapturarCampoString(DataTable tabela, string coluna)
        {
            return CapturarCampoString(tabela.Rows[0], coluna);
        }

        public static string CapturarCampoString(DataRow linha, string coluna)
        {
            string resultado = "";

            if (linha.Table.Columns.Contains(coluna))
            {
                resultado = linha[coluna].ToString();
            }

            return resultado;
        }
    }
}
