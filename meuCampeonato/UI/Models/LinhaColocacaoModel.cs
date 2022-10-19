using Biblioteca.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace UI.Models
{
    public class LinhaColocacaoModel
    {
        public string colocacao { get; set; }
        public string nomeTime { get; set; }
        public string pontuacao { get; set; }
        public string quantidadeVitoria { get; set; }
        public string dataInscricao { get; set; }

        public static LinhaColocacaoModel ConverterParaModeloInterce(DataRow linhaSimulacaoSistema)
        {
            LinhaColocacaoModel linhaSimulacaoInterface = new LinhaColocacaoModel();
            linhaSimulacaoInterface.nomeTime = UtilDataTable.CapturarCampoString(linhaSimulacaoSistema, "NM_TIME");
            linhaSimulacaoInterface.quantidadeVitoria = UtilDataTable.CapturarCampoString(linhaSimulacaoSistema, "NR_QUANTIDADE_VITORIA");
            linhaSimulacaoInterface.pontuacao = UtilDataTable.CapturarCampoString(linhaSimulacaoSistema, "NR_PONTUACAO");
            linhaSimulacaoInterface.dataInscricao = UtilDataTable.CapturarCampoString(linhaSimulacaoSistema, "DT_INSCRICAO");

            return linhaSimulacaoInterface;
        }

        public static LinhaColocacaoModel[] ConverterListaParaModeloInterce(DataTable tabelaSimulacaoSistema)
        {
            LinhaColocacaoModel[] tabelaSimulacaoInterface = new LinhaColocacaoModel[tabelaSimulacaoSistema.Rows.Count];

            for (int i = 0; i < tabelaSimulacaoSistema.Rows.Count; i++)
            {
                LinhaColocacaoModel linhaSimulacaoInterface = ConverterParaModeloInterce(tabelaSimulacaoSistema.Rows[i]);
                linhaSimulacaoInterface.colocacao = (i + 1).ToString();
                tabelaSimulacaoInterface[i] = linhaSimulacaoInterface;
            }

            return tabelaSimulacaoInterface;
        }
    }
}