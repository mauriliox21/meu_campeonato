using Biblioteca;
using System.Collections;

namespace DAL
{
    public class JogoDAL : BaseDAL
    {
        public JogoDAL(ContextoDb contexto) : base(contexto) { }

        public SortedList Incluir(SortedList parametros)
        {
            ComandoDb query = new ComandoDb("STP_JOGO_INCLUIR", ContextoAtual);
            query.IncluirParametro(AcessoDb.FormatarParametro(parametros, "SQ_FASE", true));
            query.IncluirParametro(AcessoDb.FormatarParametro(parametros, "SQ_TIME_1", true));
            query.IncluirParametro(AcessoDb.FormatarParametro(parametros, "SQ_TIME_2", true));
            query.IncluirParametro(AcessoDb.FormatarParametro(parametros, "NR_PLACAR_TIME_1", true));
            query.IncluirParametro(AcessoDb.FormatarParametro(parametros, "NR_PLACAR_TIME_2", true));

            SortedList resultado = FormatarResultado(AcessoDb.ExecutarQueryManutencao(ContextoAtual, query.Comando));
            
            return resultado;
        }

        public SortedList Consultar(SortedList parametros)
        {
            ComandoDb query = new ComandoDb("STP_JOGO_CONSULTAR", new ContextoDb());
            query.IncluirParametro(AcessoDb.FormatarParametro(parametros, "NM_CAMPEONATO", false));

            SortedList resultado = FormatarResultado(AcessoDb.ExecutarQuery(query.Comando));

            return resultado;
        }


    }
}
