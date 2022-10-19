using Biblioteca;
using System.Collections;

namespace DAL
{
    public class FaseDAL : BaseDAL
    {
        public FaseDAL(ContextoDb contexto) : base(contexto) { }

        public SortedList Incluir(SortedList parametros)
        {
            ComandoDb query = new ComandoDb("STP_FASE_INCLUIR", ContextoAtual);
            query.IncluirParametro(AcessoDb.FormatarParametro(parametros, "SQ_CAMPEONATO", true));
            query.IncluirParametro(AcessoDb.FormatarParametro(parametros, "NM_FASE", false));

            SortedList resultado = FormatarResultado(AcessoDb.ExecutarQueryManutencao(ContextoAtual, query.Comando));
            
            return resultado;
        }
    }
}
