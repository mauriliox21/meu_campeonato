using Biblioteca;
using System.Collections;

namespace DAL
{
    public class TimeDAL : BaseDAL
    {
        public TimeDAL(ContextoDb contexto) : base(contexto) { }

        public SortedList Incluir(SortedList parametros)
        {
            ComandoDb query = new ComandoDb("STP_TIME_INCLUIR", ContextoAtual);
            query.IncluirParametro(AcessoDb.FormatarParametro(parametros, "NM_TIME", false));

            SortedList resultado = FormatarResultado(AcessoDb.ExecutarQueryManutencao(ContextoAtual, query.Comando));
            
            return resultado;
        }

        public SortedList Consultar(SortedList parametros)
        {
            ComandoDb query = new ComandoDb("STP_TIME_CONSULTAR", new ContextoDb());
            query.IncluirParametro(AcessoDb.FormatarParametro(parametros, "NM_TIME", false));

            SortedList resultado = FormatarResultado(AcessoDb.ExecutarQuery(query.Comando));

            return resultado;
        }
    }
}
