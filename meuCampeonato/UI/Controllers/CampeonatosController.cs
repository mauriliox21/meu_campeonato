using BLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UI.Models;

namespace UI.Controllers
{
    public class CampeonatosController : ApiController
    {
        public HttpResponseMessage Get([FromBody] CampeonatoModel campeonato)
        {
            SortedList parametros = campeonato.ConverterParaModeloSistema();

            CampeonatoBLL consultarBLL = new CampeonatoBLL();
            SortedList resultado = consultarBLL.Consultar(parametros);

            RetornoModel retornoInterface = new RetornoModel();

            HttpResponseMessage resposta = Request.CreateResponse<CampeonatoModel[]>(HttpStatusCode.OK, CampeonatoModel.ConverterListaParaModeloInterce((DataTable)resultado["retorno"]));

            return resposta;
        }
        public SortedList Post([FromBody] CampeonatoModel campeonato)
        {
            SortedList parametros = campeonato.ConverterParaModeloSistema();

            CampeonatoBLL incluirBLL = new CampeonatoBLL();
            SortedList resultado = incluirBLL.Incluir(parametros);

            return resultado;
        }
    }
}
