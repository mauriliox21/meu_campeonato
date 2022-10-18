using BLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace UI.Controllers
{
    public class CampeonatosController : ApiController
    {
        [HttpGet]
        public SortedList Get()
        {
            SortedList parametros = new SortedList();
            parametros.Add("NM_CAMPEONATO", "");

            CampeonatoBLL consultarBLL = new CampeonatoBLL();
            SortedList resultado = consultarBLL.Consultar(parametros);

            return resultado;
        }
        public SortedList Post()
        {
            SortedList parametros = new SortedList();
            parametros.Add("NM_CAMPEONATO", "1° Campeonato");

            CampeonatoBLL incluirBLL = new CampeonatoBLL();
            SortedList resultado = incluirBLL.Incluir(parametros);

            return resultado;
        }
    }
}
