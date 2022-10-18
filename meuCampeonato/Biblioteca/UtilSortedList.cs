using System.Collections;
using System.Data;

namespace Biblioteca
{
    public class UtilSortedList
    {
        public static string CapturarString(SortedList lista, string chave)
        {
            string resultado = "";

            if (lista.Contains(chave))
                resultado = lista[chave].ToString();

            return resultado;
        }

        public static DataTable CapturarDataTable(SortedList lista, string chave)
        {
            DataTable resultado = null;

            if (lista.Contains(chave) && lista[chave].GetType() == typeof(DataTable))
                resultado = (DataTable)lista[chave];

            return resultado;
        }
    }
}
