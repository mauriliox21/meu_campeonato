using Biblioteca;
using DAL.Util;
using System;
using System.Collections;

namespace BLL
{
    public class BaseBLL
    {
        public ContextoDb ContextoAtual { get; }

        public BaseBLL()
        {
            ContextoAtual = new ContextoDb();
        }

        public BaseBLL(ContextoDb Contexto)
        {
            ContextoAtual = new ContextoDb(Contexto);
        }

        public void SetRollback()
        {
            ContextoAtual.SetRollback();
        }

        public void SetCommit()
        {
            ContextoAtual.SetCommit();
        }

        public SortedList FormatarResultadoErro(Exception erro)
        {
            //observação Lembrar de alterar este metodo para usar variaveis de ambiente
            SortedList resultado = new SortedList
            {
                { "tipoRetorno", "erro" },
                { "mensagem", erro.Message }
            };

            return resultado;
        }

        public bool VerificarResultadoSucesso(SortedList resultado)
        {
            string tipoRetorno = UtilSortedList.CapturarString(resultado, "tipoRetorno");

            return tipoRetorno.Equals("sucesso");
        }
    }
}
