using Biblioteca;
using Biblioteca.Util;
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

        public BaseBLL(ContextoDb contexto)
        {
            ContextoAtual = new ContextoDb(contexto);
        }

        public void SetRollback()
        {
            ContextoAtual.SetRollback();
        }

        public void SetCommit()
        {
            ContextoAtual.SetCommit();
        }

        public SortedList FormatarResultadoErroSistema(Exception erro)
        {
            //observação Lembrar de alterar este metodo para usar variaveis de ambiente
            return FormatarResultadoErro("Erro Interno do Sistema");
        }

        public SortedList FormatarResultadoErro(string mensagem)
        {
            SortedList resultado = new SortedList
            {
                { "tipoRetorno", "erro" },
                { "mensagem", mensagem }
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
