using Biblioteca;
using System.Collections;

namespace DAL
{
    public class CampeonatoTimeDAL : BaseDAL
    {
        public CampeonatoTimeDAL(ContextoDb contexto) : base(contexto) { }

        public SortedList Incluir(SortedList parametros)
        {
            ComandoDb query = new ComandoDb("STP_CAMPEONATO_TIME_INCLUIR", ContextoAtual);
            query.IncluirParametro(AcessoDb.FormatarParametro(parametros, "SQ_CAMPEONATO", true));
            query.IncluirParametro(AcessoDb.FormatarParametro(parametros, "SQ_TIME", true));

            SortedList resultado = FormatarResultado(AcessoDb.ExecutarQueryManutencao(ContextoAtual, query.Comando));
            
            return resultado;
        }

        public SortedList Consultar(SortedList parametros)
        {
            ComandoDb query = new ComandoDb("STP_CAMPEONATO_TIME_CONSULTAR", new ContextoDb());
            query.IncluirParametro(AcessoDb.FormatarParametro(parametros, "SQ_CAMPEONATO", false));
            query.IncluirParametro(AcessoDb.FormatarParametro(parametros, "ST_ELIMINADO", false));

            SortedList resultado = FormatarResultado(AcessoDb.ExecutarQuery(query.Comando));

            return resultado;
        }

        public SortedList Alterar(SortedList parametros)
        {
            ComandoDb query = new ComandoDb("STP_CAMPEONATO_TIME_ALTERAR", ContextoAtual);
            query.IncluirParametro(AcessoDb.FormatarParametro(parametros, "SQ_CAMPEONATO_TIME", true));
            query.IncluirParametro(AcessoDb.FormatarParametro(parametros, "NR_PONTUACAO", true));
            query.IncluirParametro(AcessoDb.FormatarParametro(parametros, "ST_ELIMINADO", false));
            query.IncluirParametro(AcessoDb.FormatarParametro(parametros, "NR_QUANTIDADE_VITORIA", true));
            query.IncluirParametro(AcessoDb.FormatarParametro(parametros, "NR_COLOCACAO", true));

            SortedList resultado = FormatarResultado(AcessoDb.ExecutarQueryManutencao(ContextoAtual, query.Comando));

            return resultado;
        }
    }
}
