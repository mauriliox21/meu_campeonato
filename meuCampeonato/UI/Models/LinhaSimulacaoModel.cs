using Biblioteca.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class LinhaSimulacaoModel
    {
        public string fase { get; set; }
        public string nomeTime1 { get; set; }
        public string placarTime1 { get; set; }
        public string nomeTime2 { get; set; }
        public string placarTime2 { get; set; }

        public static LinhaSimulacaoModel ConverterParaModeloInterce(DataRow linhaSimulacaoSistema)
        {
            LinhaSimulacaoModel linhaSimulacaoInterface = new LinhaSimulacaoModel();
            linhaSimulacaoInterface.fase = UtilDataTable.CapturarCampoString(linhaSimulacaoSistema, "NM_FASE");
            linhaSimulacaoInterface.nomeTime1 = UtilDataTable.CapturarCampoString(linhaSimulacaoSistema, "NM_TIME_1");
            linhaSimulacaoInterface.placarTime1 = UtilDataTable.CapturarCampoString(linhaSimulacaoSistema, "NR_PLACAR_TIME_1");
            linhaSimulacaoInterface.nomeTime2 = UtilDataTable.CapturarCampoString(linhaSimulacaoSistema, "NM_TIME_2");
            linhaSimulacaoInterface.placarTime2 = UtilDataTable.CapturarCampoString(linhaSimulacaoSistema, "NR_PLACAR_TIME_2");

            return linhaSimulacaoInterface;
        }

        public static LinhaSimulacaoModel[] ConverterListaParaModeloInterce(DataTable tabelaSimulacaoSistema)
        {
            LinhaSimulacaoModel[] tabelaSimulacaoInterface = new LinhaSimulacaoModel[tabelaSimulacaoSistema.Rows.Count];

            for (int i = 0; i < tabelaSimulacaoSistema.Rows.Count; i++)
            {
                tabelaSimulacaoInterface[i] = ConverterParaModeloInterce(tabelaSimulacaoSistema.Rows[i]);
            }

            return tabelaSimulacaoInterface;
        }
    }
}