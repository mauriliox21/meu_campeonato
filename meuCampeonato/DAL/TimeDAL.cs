using DAL.Util;
using System.Collections;

namespace DAL
{
    public class TimeDAL : BaseDAL
    {
        public SortedList Incluir(ContextoDb contexto, SortedList parametros)
        {
            ComandoDb query = new ComandoDb("STP_TIME_INCLUIR", contexto);
            query.IncluirParametro(AcessoDb.FormatarParametro(parametros, "NM_TIME", false));

            SortedList resultado = FormatarResultado(AcessoDb.ExecutarQuery(contexto, query.Comando));
            
            return resultado;
        }

        public SortedList Consultar(ContextoDb contexto, SortedList parametros)
        {
            ComandoDb query = new ComandoDb("STP_TIME_CONSULTAR", contexto);
            query.IncluirParametro(AcessoDb.FormatarParametro(parametros, "NM_TIME", false));

            SortedList resultado = FormatarResultado(AcessoDb.ExecutarQuery(contexto, query.Comando));

            return resultado;
        }
    }
}
