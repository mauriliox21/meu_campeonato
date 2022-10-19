using Biblioteca.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace UI.Models
{
    public class ChaveRegistroModel
    {
        public string codigoRegistro { get; set; }

        public static ChaveRegistroModel ConverterParaModeloInterce(DataTable chaveRegistroSistema)
        {
            ChaveRegistroModel chaveRegistroInterface = new ChaveRegistroModel();
            chaveRegistroInterface.codigoRegistro = UtilDataTable.CapturarCampoString(chaveRegistroSistema, "CD_CHAVE_REGISTRO");

            return chaveRegistroInterface;
        }
    }
}