using Biblioteca;
using Biblioteca.Util;
using DAL;
using System;
using System.Collections;
using System.Data;
using System.Linq;

namespace BLL
{
    public class JogoBLL : BaseBLL
    {
        public JogoBLL() : base() { }
        public JogoBLL(ContextoDb contexto) : base(contexto) { }

        public SortedList Incluir(SortedList parametros)
        {
            SortedList resultado = new SortedList();

            try
            {
                JogoDAL incluirDAL = new JogoDAL(ContextoAtual);
                resultado = incluirDAL.Incluir(parametros);

                //caso não seja sucesso reverte a transação
                if (VerificarResultadoSucesso(resultado))
                    SetCommit();
                else
                    SetRollback();
                
            }
            catch (Exception erro)
            {
                resultado = FormatarResultadoErroSistema(erro);
                SetRollback();
            }

            return resultado;
        }
    }
}
