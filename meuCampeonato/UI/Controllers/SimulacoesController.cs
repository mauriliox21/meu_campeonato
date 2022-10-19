using BLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Services.Description;
using API.Models;

namespace API.Controllers
{
    public class SimulacoesController : ApiController
    {
        [HttpGet]
        public void get()
        {

        }
        public HttpResponseMessage get([FromUri] string sq)
        {
            SortedList parametros = new SortedList();
            parametros.Add("SQ_CAMPEONATO", sq);
            CampeonatoBLL simulacaoIncluir = new CampeonatoBLL();

            SortedList resultado = simulacaoIncluir.SimulacaoCampeonatoConsultar(parametros);

            RespostaModel retornoInterface = RespostaModel.ConverterListaParaModeloInterce(resultado);

            HttpResponseMessage resposta;

            if (retornoInterface.status.Equals("sucesso"))
            {
                LinhaSimulacaoModel[] simulacao = LinhaSimulacaoModel.ConverterListaParaModeloInterce((DataTable)resultado["retorno"]);
                LinhaColocacaoModel[] colocacao = LinhaColocacaoModel.ConverterListaParaModeloInterce((DataTable)resultado["retornoColocacao"]);

                retornoInterface.retorno = new SortedList{
                    { "chaveamento", simulacao },
                    { "colocacao", colocacao }
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

        public HttpResponseMessage post([FromUri] string sq)
        {
            SortedList parametros = new SortedList();
            parametros.Add("SQ_CAMPEONATO", sq);
            CampeonatoBLL simulacaoIncluir = new CampeonatoBLL();

            SortedList resultado = simulacaoIncluir.SimulacaoCampeonatoIncluir(parametros);

            RespostaModel retornoInterface = RespostaModel.ConverterListaParaModeloInterce(resultado);

            retornoInterface.retorno = "";

            HttpResponseMessage resposta;

            if (retornoInterface.status.Equals("sucesso"))
                resposta = Request.CreateResponse<RespostaModel>(HttpStatusCode.Created, retornoInterface);
            else
                resposta = Request.CreateResponse<RespostaModel>(HttpStatusCode.InternalServerError, retornoInterface);

            return resposta;
        }
    }
}
