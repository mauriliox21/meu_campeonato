using Biblioteca;
using Biblioteca.Util;
using DAL;
using System;
using System.Collections;
using System.Data;

namespace BLL
{
    public class FaseBLL : BaseBLL
    {
        public FaseBLL() : base() { }
        public FaseBLL(ContextoDb contexto) : base(contexto) { }

        public SortedList Incluir(SortedList parametros)
        {
            SortedList resultado = new SortedList();

            try
            {
                FaseDAL incluirDAL = new FaseDAL(ContextoAtual);
                resultado = incluirDAL.Incluir(parametros);

                //caso não seja sucesso reverte a transação
                if (!VerificarResultadoSucesso(resultado))
                    SetRollback();
                
            }
            catch (Exception erro)
            {
                resultado = FormatarResultadoErroSistema(erro);
                SetRollback();
            }

            return resultado;
        }

        public SortedList Consultar(SortedList parametros)
        {
            SortedList resultado = new SortedList();

            try
            {
                FaseDAL consultarDAL = new FaseDAL(ContextoAtual);
                resultado = consultarDAL.Consultar(parametros);

            }
            catch (Exception erro)
            {
                resultado = FormatarResultadoErroSistema(erro);
            }

            return resultado;
        }
    }
}
