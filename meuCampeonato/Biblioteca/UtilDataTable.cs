using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
