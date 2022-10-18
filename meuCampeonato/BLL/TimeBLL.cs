using Biblioteca;
using DAL;
using DAL.Util;
using System;
using System.Collections;
using System.Data;

namespace BLL
{
    public class TimeBLL : BaseBLL
    {
        public TimeBLL() : base() { }
        public TimeBLL(ContextoDb contexto) : base(contexto) { }

        public SortedList Incluir(SortedList parametros)
        {
            SortedList resultado = new SortedList();

            try
            {
                string sqTime = "";
                bool ocorreuErro = false;

                #region Verifica se o time existe para não ser incluido duplicado
                SortedList resultadoConsultaTime = Consultar(parametros);

                if (VerificarResultadoSucesso(resultadoConsultaTime))
                {
                    DataTable retornoConsultaTime = UtilSortedList.CapturarDataTable(resultadoConsultaTime, "retorno");
                    if (retornoConsultaTime.Rows.Count > 0)
                    {
                        sqTime = UtilDataTable.CapturarCampoString(retornoConsultaTime, "SQ_TIME");
                    }
                }
                else
                {
                    //caso ocorra erro não executa mais nenhum procedimento, e salva o resultado erro para devolver para interface
                    ocorreuErro = true;
                    resultado = resultadoConsultaTime;
                }
                #endregion

                #region caso o time não exista na base cria o registro do time
                if (sqTime == "" && !ocorreuErro)
                {
                    TimeDAL incluirDAL = new TimeDAL();
                    resultado = incluirDAL.Incluir(ContextoAtual, parametros);

                    if (VerificarResultadoSucesso(resultado))
                    {
                        DataTable retornoInclusaoTime = UtilSortedList.CapturarDataTable(resultado, "retorno");
                        sqTime = UtilDataTable.CapturarCampoString(retornoInclusaoTime, "CD_CHAVE_REGISTRO");
                    }
                    else
                    {
                        //caso ocorra erro não executa mais nenhum procedimento
                        ocorreuErro = true;
                    }
                }
                #endregion

                #region Inclui o time no campeonato
                if (!ocorreuErro)
                {
                    parametros.Add("SQ_TIME", sqTime);
                    CampeonatoTimeBLL campeonatoTimeIncluirBLL = new CampeonatoTimeBLL(ContextoAtual);
                    SortedList resultadoCampeonatoTime = campeonatoTimeIncluirBLL.Incluir(parametros);

                    if (!VerificarResultadoSucesso(resultadoCampeonatoTime))
                    {
                        //caso ocorra erro não executa mais nenhum procedimento, e salva o resultado erro para devolver para interface
                        ocorreuErro = true;
                        resultado = resultadoCampeonatoTime;
                    }
                }
                #endregion

                //caso não seja sucesso toda a reverte a transação
                if (!ocorreuErro)
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
                TimeDAL consultarDAL = new TimeDAL();
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
