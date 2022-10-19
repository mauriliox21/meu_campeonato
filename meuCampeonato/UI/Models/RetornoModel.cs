using Biblioteca.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace UI.Models
{
    public class RetornoModel
    {
        public string mensagem { get; set; }
        public string status { get; set; }
        public object retorno { get; set; }

        public static RetornoModel ConverterListaParaModeloInterce(SortedList retornoSistema)
        {
            RetornoModel retornoInterface = new RetornoModel();
            retornoInterface.mensagem = UtilSortedList.CapturarString(retornoSistema, "mensagem");
            retornoInterface.status = UtilSortedList.CapturarString(retornoSistema, "tipoRetorno");

            return retornoInterface;
        }
    }
}