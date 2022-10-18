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

        public static int[] SortearTimes(int qtdTimes)
        {
            int[] timesSorteados = { };

            Random rdm = new Random();
            while (timesSorteados.Length < qtdTimes)
            {
                int indice = rdm.Next(qtdTimes);
                if (!timesSorteados.Contains(indice))
                    timesSorteados.Append(indice);
            }

            return timesSorteados;
        }

        public static int[] SortearPlacares(int maxPlacar)
        {
            int[] timesSorteados = { };

            Random rdm = new Random();
            while (timesSorteados.Length < 2)
            {
                int indice = rdm.Next(maxPlacar);
                if (!timesSorteados.Contains(indice))
                    timesSorteados.Append(indice);
            }

            return timesSorteados;
        }
    }
}
