using Biblioteca;
using Biblioteca.Util;
using DAL;
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
                string mensagem = "";
                UtilValidacao.ValidarParametroInteiro(parametros["SQ_CAMPEONATO"], "sqCampeonato", ref mensagem, true);
                UtilValidacao.ValidarParametroAlfanumerico(parametros["NM_TIME"], "nomeTime", ref mensagem, 100, true);
                if (mensagem == "")
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
                        TimeDAL incluirDAL = new TimeDAL(ContextoAtual);
                        resultado = incluirDAL.Incluir(parametros);

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
                        parametros["SQ_TIME"] = sqTime;
                        CampeonatoTimeBLL campeonatoTimeIncluirBLL = new CampeonatoTimeBLL(ContextoAtual);
                        SortedList resultadoCampeonatoTime = campeonatoTimeIncluirBLL.Incluir(parametros);

                        if (!VerificarResultadoSucesso(resultadoCampeonatoTime))
                        {
                            //caso ocorra erro não executa mais nenhum procedimento, e salva o resultado erro para devolver para interface
                            ocorreuErro = true;
                            resultado = resultadoCampeonatoTime;
                        }
                        else
                        {
                            //caso não tenha sido necessario criar um novo time
                            if (!resultado.ContainsKey("retorno"))
                            {
                                resultado = resultadoCampeonatoTime;
                                //para uso da interface
                                resultadoCampeonatoTime["SQ_TIME"] = sqTime;
                            }
                        }

                    }
                    #endregion

                    //caso não seja sucesso toda a reverte a transação
                    if (!ocorreuErro)
                        SetCommit();
                    else
                        SetRollback();
                }
                else
                {
                    resultado = FormatarResultadoErro(mensagem);
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
                TimeDAL consultarDAL = new TimeDAL(ContextoAtual);
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
