using Biblioteca;
using Biblioteca.Util;
using System.Collections;
using System.Data;
using System.Runtime.Remoting.Contexts;

namespace DAL
{
    public class BaseDAL
    {
        public ContextoDb ContextoAtual { get; }
        public BaseDAL(ContextoDb contexto)
        {
            ContextoAtual = contexto;
        }
        public SortedList FormatarResultado(DataTable retorno)
        {
            SortedList resultado = new SortedList();

            if(retorno.Rows.Count > 0)
            {
                resultado.Add("tipoRetorno", UtilDataTable.CapturarCampoString(retorno, "CT_TIPO_RETORNO"));
                resultado.Add("mensagem", UtilDataTable.CapturarCampoString(retorno, "CT_MENSAGEM"));
                resultado.Add("retorno", retorno);
            }
            //Caso a execução não retorne nenhum registro nem gere erro, trata-se de uma consulta sem registros (que é considerada sucesso)
            else
            {
                resultado.Add("tipoRetorno", "sucesso");
                resultado.Add("mensagem", "Nenhum registro encontrado");
                resultado.Add("retorno", retorno);
            }

            return resultado;
        }
    }
}
