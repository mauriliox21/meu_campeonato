using BLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace UI.Controllers
{
    public class SimulacoesController : ApiController
    {
        public void post()
        {
            SortedList parametros = new SortedList();
            parametros.Add("SQ_CAMPEONATO", "5");
            CampeonatoBLL simulacaoIncluir = new CampeonatoBLL();

            simulacaoIncluir.SimularCampeonato(parametros);
            
        }
    }
}
