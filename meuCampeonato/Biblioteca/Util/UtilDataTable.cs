using System;
using System.Data;

namespace Biblioteca.Util
{
    public class UtilDataTable
    {
        /// <summary>
        /// Retorna uma string com o valor da primeira linha da coluna solicitada. 
        /// Caso a coluna não exista na tabela ou o valor da coluna seja "null" retorna uma string vazia
        /// </summary>
        public static string CapturarCampoString(DataTable tabela, string coluna)
        {
            return CapturarCampoString(tabela.Rows[0], coluna);
        }

        /// <summary>
        /// Retorna uma string com o valor da coluna solicitada. 
        /// Caso a coluna não exista na tabela ou o valor da coluna seja "null" retorna uma string vazia
        /// </summary>
        public static string CapturarCampoString(DataRow linha, string coluna)
        {
            string resultado = "";

            if (linha.Table.Columns.Contains(coluna))
            {
                if(linha[coluna] != null)
                    resultado = linha[coluna].ToString();
            }

            return resultado;
        }

        /// <summary>
        /// Retorna um "int" com o valor da coluna solicitada. 
        /// Caso a coluna não exista na tabela ou o valor da coluna seja "null" irá retornar o valor padrão passado como parametro
        /// </summary>
        public static int CapturarCampoInteiro(DataRow linha, string coluna, int valorPadrao)
        {
            int resultado = valorPadrao;

            if (linha.Table.Columns.Contains(coluna))
            {
                if (linha[coluna] != null)
                    resultado = int.Parse(linha[coluna].ToString());
            }

            return resultado;
        }

        /// <summary>
        /// Retorna um "DateTime" com o valor da coluna solicitada. 
        /// Caso a coluna não exista na tabela ou o valor da coluna seja "null" irá retornar o valor padrão passado como parametro
        /// </summary>
        public static DateTime CapturarCampoData(DataRow linha, string coluna, DateTime valorPadrao)
        {
            DateTime resultado = valorPadrao;

            if (linha.Table.Columns.Contains(coluna))
            {
                if (linha[coluna] != null)
                {
                    string dataString = linha[coluna].ToString().Split(' ')[0];
                    string horaString = linha[coluna].ToString().Split(' ')[1];

                    int dia = int.Parse(dataString.Split('-')[0]);
                    int mes = int.Parse(dataString.Split('-')[1]);
                    int ano = int.Parse(dataString.Split('-')[2]);

                    int hora = int.Parse(horaString.Split('-')[0]);
                    int min = int.Parse(horaString.Split('-')[1]);
                    int seg = int.Parse(horaString.Split('-')[2]);

                    resultado = new DateTime(ano, mes, dia, hora, min, seg);
                }
            }

            return resultado;
        }
    }
}
