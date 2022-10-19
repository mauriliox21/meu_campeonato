using BLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UI.Models;

namespace UI.Controllers
{
    public class TimesController : ApiController
    {
        public SortedList Post([FromBody] TimeModel time)
        {
            SortedList parametro = time.ConverterParaModeloSistema();

            TimeBLL incluirBLL = new TimeBLL();
            SortedList resultado = incluirBLL.Incluir(parametro);

            return resultado;

        }
    }
}
