using System.Collections;
using System.Data;

namespace Biblioteca.Util
{
    public class UtilSortedList
    {
        /// <summary>
        /// Retorna uma string com o valor do objeto da chave solicitada. 
        /// Caso a chave não exista na lista ou o valor do objeto seja "null" retorna uma string vazia
        /// </summary>
        public static string CapturarString(SortedList lista, string chave)
        {
            string resultado = "";

            if (lista.Contains(chave) && lista[chave] != null)
                resultado = lista[chave].ToString();

            return resultado;
        }

        /// <summary>
        /// Retorna um DataTable com a tabela do objeto da chave solicitada. 
        /// Caso a chave não exista na lista ou o valor do objeto não seja do tipo DataTable retorna um DataTable "null"
        /// </summary>
        public static DataTable CapturarDataTable(SortedList lista, string chave)
        {
            DataTable resultado = null;

            if (lista.Contains(chave) && lista[chave].GetType() == typeof(DataTable))
                resultado = (DataTable)lista[chave];

            return resultado;
        }
    }
}
