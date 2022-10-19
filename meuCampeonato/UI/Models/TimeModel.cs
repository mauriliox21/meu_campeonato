using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Models
{
    public class TimeModel
    {
        public string sqCampeonato { get; set; }
        public string sqTime { get; set; }
        public string nomeTime { get; set; }

        public SortedList ConverterParaModeloSistema()
        {
            SortedList campeonatoSistema = new SortedList();
            campeonatoSistema.Add("SQ_CAMPEONATO", this.sqCampeonato);
            if(this.sqTime != null)
                campeonatoSistema.Add("SQ_TIME", this.sqTime);
            campeonatoSistema.Add("NM_TIME", this.nomeTime);

            return campeonatoSistema;
        }
    }
}