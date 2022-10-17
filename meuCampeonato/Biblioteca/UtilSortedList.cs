using System.Collections;

namespace Biblioteca
{
    public class UtilSortedList
    {
        public static string CapturarCampoString(SortedList lista, string chave)
        {
            string resultado = "";

            if (lista.Contains(chave))
                resultado = lista[chave].ToString();

            return resultado;
        }
    }
}
