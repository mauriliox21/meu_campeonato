using BLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Models;

namespace API.Controllers
{
    public class TimesController : ApiController
    {
        public HttpResponseMessage Post([FromBody] TimeModel time)
        {
            SortedList parametro = time.ConverterParaModeloSistema();

            TimeBLL incluirBLL = new TimeBLL();
            SortedList resultado = incluirBLL.Incluir(parametro);

            RespostaModel retornoInterface = RespostaModel.ConverterListaParaModeloInterce(resultado);

            HttpResponseMessage resposta;

            if (retornoInterface.status.Equals("sucesso"))
            {
                ChaveRegistroModel chaveRegistro = ChaveRegistroModel.ConverterParaModeloInterce((DataTable)resultado["retorno"]);
                if (resultado.ContainsKey("SQ_TIME"))
                    chaveRegistro.codigoRegistro = resultado["SQ_TIME"].ToString();

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
