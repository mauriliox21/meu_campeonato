using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Util
{
    public class UtilJogo
    {
        /// <summary>
        /// Usa o criterio de desempate para saber qual time foi eliminado no jogo e retorna seu SQ
        /// </summary>
        public static string CriterioDesempateTimeEliminado(DataRow dadosTime1, DataRow dadosTime2)
        {
            string sqTimeEliminado;

            //primeiro criterio de desempate é pontuação
            int pontuacaoTime1 = UtilDataTable.CapturarCampoInteiro(dadosTime1, "NR_PONTUACAO", 0);
            int pontuacaoTime2 = UtilDataTable.CapturarCampoInteiro(dadosTime2, "NR_PONTUACAO", 0);

            if (pontuacaoTime1 > pontuacaoTime2)
            {
                sqTimeEliminado = UtilDataTable.CapturarCampoString(dadosTime2, "SQ_TIME");
            }
            else if (pontuacaoTime2 > pontuacaoTime1)
            {
                sqTimeEliminado = UtilDataTable.CapturarCampoString(dadosTime1, "SQ_TIME");
            }
            else
            {
                DateTime dataInsTime1 = UtilDataTable.CapturarCampoData(dadosTime1, "DT_INSCRICAO", DateTime.Now);
                DateTime dataInsTime2 = UtilDataTable.CapturarCampoData(dadosTime2, "DT_INSCRICAO", DateTime.Now);
                //segundo criterio de desempate é data da inscrição
                if (dataInsTime1 > dataInsTime2)
                {
                    sqTimeEliminado = UtilDataTable.CapturarCampoString(dadosTime2, "SQ_TIME");
                }
                else
                {
                    sqTimeEliminado = UtilDataTable.CapturarCampoString(dadosTime1, "SQ_TIME");
                }
            }

            return sqTimeEliminado;
        }

        /// <summary>
        /// Retorna uma sequência de numeros aleatorios que não se repetem
        /// </summary>
        public static int[] SortearTimes(int qtdTimes, int indiceMax)
        {
            //usa um array de string para comparar pois quando se usa array de int o ultimo valor do array vai ser sempre 0

            string[] timesSorteadosComparacao = new string[qtdTimes];
            int[] timesSorteados = new int[qtdTimes];

            Random rdm = new Random();
            int contador = 0;
            while (contador < qtdTimes)
            {
                int indice = rdm.Next(0, indiceMax + 1);
                if (!timesSorteadosComparacao.Contains(indice.ToString()))
                {
                    timesSorteadosComparacao[contador] = indice.ToString(); 
                    timesSorteados[contador] = indice;
                    contador++;
                }
            }

            return timesSorteados;
        }

        /// <summary>
        /// Retorna uma sequência de dois numeros aleatorios que podem ser repetidos
        /// </summary>
        public static int[] SortearPlacares(int indiceMax)
        {
            int[] placares = new int[2];

            Random rdm = new Random();
            for (int i = 0; i < placares.Length; i++)
            {
                placares[i] = rdm.Next(indiceMax + 1);
            }

            return placares;
        }
    }
}
