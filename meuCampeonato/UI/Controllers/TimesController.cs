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
    public class TimesController : ApiController
    {
        public SortedList Post()
        {
            SortedList parametro = new SortedList();
            parametro.Add("SQ_CAMPEONATO", "5");
            parametro.Add("NM_TIME", "Machester");

            TimeBLL incluirBLL = new TimeBLL();
            SortedList resultado = incluirBLL.Incluir(parametro);

            return resultado;

        }
    }
}
