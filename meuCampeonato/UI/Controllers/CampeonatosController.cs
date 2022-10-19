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

            RespostaModel retornoInterface = RespostaModel.ConverterListaParaModeloInterce(resultado);

            HttpResponseMessage resposta;

            if (retornoInterface.status.Equals("sucesso"))
            {
                retornoInterface.retorno = CampeonatoModel.ConverterListaParaModeloInterce((DataTable)resultado["retorno"]);
                resposta = Request.CreateResponse<RespostaModel>(HttpStatusCode.OK, retornoInterface);
            }
            else
            {
                retornoInterface.retorno = "";
                resposta = Request.CreateResponse<RespostaModel>(HttpStatusCode.InternalServerError, retornoInterface);
            }

            return resposta;
        }

        public HttpResponseMessage Get([FromUri] string sq)
        {
            SortedList parametros = new SortedList();
            parametros.Add("SQ_CAMPEONATO", sq);

            CampeonatoBLL consultarBLL = new CampeonatoBLL();
            SortedList resultado = consultarBLL.Detalhar(parametros);

            RespostaModel retornoInterface = RespostaModel.ConverterListaParaModeloInterce(resultado);

            HttpResponseMessage resposta;

            if (retornoInterface.status.Equals("sucesso"))
            {
                CampeonatoModel campeonatoInterface = CampeonatoModel.ConverterListaParaModeloInterce((DataTable)resultado["retorno"])[0];
                LinhaColocacaoModel[] colocacaoInterface = LinhaColocacaoModel.ConverterListaParaModeloInterce((DataTable)resultado["retornoColocacao"]);
                retornoInterface.retorno = new SortedList{
                    { "campeonato", campeonatoInterface },
                    { "times", colocacaoInterface }
                };
                resposta = Request.CreateResponse<RespostaModel>(HttpStatusCode.OK, retornoInterface);
            }
            else
            {
                retornoInterface.retorno = "";
                resposta = Request.CreateResponse<RespostaModel>(HttpStatusCode.InternalServerError, retornoInterface);
            }

            return resposta;
        }

        public HttpResponseMessage Post([FromBody] CampeonatoModel campeonato)
        {
            SortedList parametros = campeonato.ConverterParaModeloSistema();

            CampeonatoBLL incluirBLL = new CampeonatoBLL();
            SortedList resultado = incluirBLL.Incluir(parametros);

            RespostaModel retornoInterface = RespostaModel.ConverterListaParaModeloInterce(resultado);

            HttpResponseMessage resposta;

            if (retornoInterface.status.Equals("sucesso"))
            {
                ChaveRegistroModel chaveRegistro = ChaveRegistroModel.ConverterParaModeloInterce((DataTable)resultado["retorno"]);
                retornoInterface.retorno = chaveRegistro;

                resposta = Request.CreateResponse<RespostaModel>(HttpStatusCode.Created, retornoInterface);
            }
            else
            {
                retornoInterface.retorno = "";
                resposta = Request.CreateResponse<RespostaModel>(HttpStatusCode.InternalServerError, retornoInterface);
            }

            return resposta;
        }
    }
}
