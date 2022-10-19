using Biblioteca;
using Biblioteca.Util;
using DAL;
using System;
using System.Collections;
using System.Data;

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
                if (!CampeonatoTemTodosTimes(parametros))
                {
                    CampeonatoTimeDAL incluirDAL = new CampeonatoTimeDAL(ContextoAtual);
                    resultado = incluirDAL.Incluir(parametros);

                    //caso não seja sucesso reverte a transação
                    if (VerificarResultadoSucesso(resultado))
                        SetCommit();
                    else
                        SetRollback();
                }
                else
                {
                    resultado = FormatarResultadoErro("Time não pode ser incluido. Este campeonato já tem o máximo de times possíveis");
                }
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
                string mensagem = "";
                UtilValidacao.ValidarParametroInteiro(parametros["SQ_CAMPEONATO"], "sqCampeonato", ref mensagem, true);

                if (mensagem == "")
                { 
                    CampeonatoTimeDAL consultarDAL = new CampeonatoTimeDAL(ContextoAtual);
                    resultado = consultarDAL.Consultar(parametros);
                }
                else
                {
                    resultado = FormatarResultadoErro(mensagem);
                }

            }
            catch (Exception erro)
            {
                resultado = FormatarResultadoErroSistema(erro);
            }

            return resultado;
        }

        public SortedList Alterar(SortedList parametros)
        {
            SortedList resultado = new SortedList();

            try
            {
                CampeonatoTimeDAL alterarDAL = new CampeonatoTimeDAL(ContextoAtual);
                resultado = alterarDAL.Alterar(parametros);

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

        public SortedList ColocacaoCampeonatoTimeConsultar(SortedList parametros)
        {
            SortedList resultado = new SortedList();
            try
            {
                CampeonatoTimeDAL consultarDAL = new CampeonatoTimeDAL(ContextoAtual);
                resultado = consultarDAL.ColocacaoCampeonatoTimeConsultar(parametros);
            }
            catch (Exception erro)
            {
                resultado = FormatarResultadoErroSistema(erro);
            }

            return resultado;
        }

        public bool CampeonatoTemTodosTimes(SortedList parametros)
        {
            bool CampTimeMax = false;

            SortedList resultadoConsultaCampTime = Consultar(parametros);
            if (VerificarResultadoSucesso(resultadoConsultaCampTime))
            {
                DataTable retornoConsultaCampTime = UtilSortedList.CapturarDataTable(resultadoConsultaCampTime, "retorno");
                CampTimeMax = (retornoConsultaCampTime.Rows.Count == 8);
            }

            return CampTimeMax;
        }
    }
}
