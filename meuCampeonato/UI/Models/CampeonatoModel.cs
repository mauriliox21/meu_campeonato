using Biblioteca.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace UI.Models
{
    public class CampeonatoModel
    {
        public string sqCampeonato { get; set; }
        public string nomeCampeonato { get; set; }
        public string nomeTimeCampeao { get; set; }

        public SortedList ConverterParaModeloSistema()
        {
            SortedList campeonatoSistema = new SortedList();
            campeonatoSistema.Add("SQ_CAMPEONATO", this.sqCampeonato);
            campeonatoSistema.Add("NM_CAMPEONATO", this.nomeCampeonato);

            return campeonatoSistema;
        }

        public static CampeonatoModel ConverterParaModeloInterce(DataRow campeonatoSistema)
        {
            CampeonatoModel campeonatoInterface = new CampeonatoModel();
            campeonatoInterface.sqCampeonato = UtilDataTable.CapturarCampoString(campeonatoSistema, "SQ_CAMPEONATO");
            campeonatoInterface.nomeCampeonato = UtilDataTable.CapturarCampoString(campeonatoSistema, "NM_CAMPEONATO");
            campeonatoInterface.nomeTimeCampeao = UtilDataTable.CapturarCampoString(campeonatoSistema, "NM_TIME_CAMPEAO");

            return campeonatoInterface;
        }

        public static CampeonatoModel[] ConverterListaParaModeloInterce(DataTable listaCampeonatoSistema)
        {
            CampeonatoModel[] listaCampeonatoInterface = new CampeonatoModel[listaCampeonatoSistema.Rows.Count];

            for (int i = 0; i < listaCampeonatoSistema.Rows.Count; i++)
            {
                listaCampeonatoInterface[i] = ConverterParaModeloInterce(listaCampeonatoSistema.Rows[i]);
            }

            return listaCampeonatoInterface;
        }
    }
}