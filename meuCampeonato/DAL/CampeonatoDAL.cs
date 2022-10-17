using DAL.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CampeonatoDAL : BaseDAL
    {
        public SortedList Incluir(ContextoDb contexto, SortedList parametros)
        {
            SortedList resultado = new SortedList();

            ComandoDb query = new ComandoDb("STP_CAMPEONATO_INCLUIR", contexto);
            query.IncluirParametro(AcessoDb.FormatarParametro(parametros, "NM_CAMPEONATO", false));
            resultado = FormatarResultado(AcessoDb.ExecutarQuery(contexto, query.Comando));
            
            return resultado;
        }

        public SortedList Consultar(ContextoDb contexto, SortedList parametros)
        {
            SortedList resultado = new SortedList();

            ComandoDb query = new ComandoDb("STP_CAMPEONATO_CONSULTAR", contexto);
            query.IncluirParametro(AcessoDb.FormatarParametro(parametros, "NM_CAMPEONATO", false));
            resultado = FormatarResultado(AcessoDb.ExecutarQuery(contexto, query.Comando));

            return resultado;
        }


    }
}
