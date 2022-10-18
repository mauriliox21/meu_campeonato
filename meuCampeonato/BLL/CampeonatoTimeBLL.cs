using DAL;
using DAL.Util;
using System;
using System.Collections;

namespace BLL
{
    public class CampeonatoTimeBLL : BaseBLL
    {
        public CampeonatoTimeBLL() : base() { }
        public CampeonatoTimeBLL(ContextoDb contexto) : base(contexto) { }

        public SortedList Incluir(SortedList parametros)
        {
            SortedList resultado = new SortedList();

            try
            {
                CampeonatoTimeDAL incluirDAL = new CampeonatoTimeDAL();
                resultado = incluirDAL.Incluir(ContextoAtual, parametros);

                //caso não seja sucesso reverte a transação
                if (VerificarResultadoSucesso(resultado))
                    SetCommit();
                else
                    SetRollback();
                
            }
            catch (Exception erro)
            {
                resultado = FormatarResultadoErro(erro);
                SetRollback();
            }

            return resultado;
        }

        public SortedList Consultar(SortedList parametros)
        {
            SortedList resultado = new SortedList();

            try
            {
                CampeonatoTimeDAL consultarDAL = new CampeonatoTimeDAL();
                resultado = consultarDAL.Consultar(ContextoAtual, parametros);

                //caso não seja sucesso reverte a transação
                if (VerificarResultadoSucesso(resultado))
                    SetCommit();
                else
                    SetRollback();

            }
            catch (Exception erro)
            {
                resultado = FormatarResultadoErro(erro);
                SetRollback();
            }

            return resultado;
        }
    }
}
