using Biblioteca.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace UI.Models
{
    public class RespostaModel
    {
        public string mensagem { get; set; }
        public string status { get; set; }
        public object retorno { get; set; }

        public static RespostaModel ConverterListaParaModeloInterce(SortedList retornoSistema)
        {
            RespostaModel retornoInterface = new RespostaModel();
            retornoInterface.mensagem = UtilSortedList.CapturarString(retornoSistema, "mensagem");
            retornoInterface.status = UtilSortedList.CapturarString(retornoSistema, "tipoRetorno");

            return retornoInterface;
        }
    }
}