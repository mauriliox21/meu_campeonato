using Biblioteca;
using System.Collections;

namespace DAL
{
    public class CampeonatoDAL : BaseDAL
    {
        public CampeonatoDAL(ContextoDb contexto) : base(contexto) { }

        public SortedList Incluir(SortedList parametros)
        {
            ComandoDb query = new ComandoDb("STP_CAMPEONATO_INCLUIR", ContextoAtual);
            query.IncluirParametro(AcessoDb.FormatarParametro(parametros, "NM_CAMPEONATO", false));

            SortedList resultado = FormatarResultado(AcessoDb.ExecutarQueryManutencao(ContextoAtual, query.Comando));
            
            return resultado;
        }

        public SortedList Consultar(SortedList parametros)
        {
            ComandoDb query = new ComandoDb("STP_CAMPEONATO_CONSULTAR", new ContextoDb());
            query.IncluirParametro(AcessoDb.FormatarParametro(parametros, "NM_CAMPEONATO", false));

            SortedList resultado = FormatarResultado(AcessoDb.ExecutarQuery(query.Comando));

            return resultado;
        }

        public SortedList Detalhar(SortedList parametros)
        {
            ComandoDb query = new ComandoDb("STP_CAMPEONATO_DETALHAR", new ContextoDb());
            query.IncluirParametro(AcessoDb.FormatarParametro(parametros, "SQ_CAMPEONATO", true));

            SortedList resultado = FormatarResultado(AcessoDb.ExecutarQuery(query.Comando));

            return resultado;
        }

        public SortedList SimulacaoCampeonatoConsultar(SortedList parametros)
        {
            ComandoDb query = new ComandoDb("STP_SIMULACAO_CAMPEONATO_CONSULTAR", new ContextoDb());
            query.IncluirParametro(AcessoDb.FormatarParametro(parametros, "SQ_CAMPEONATO", true));

            SortedList resultado = FormatarResultado(AcessoDb.ExecutarQuery(query.Comando));

            return resultado;
        }
    }
}
