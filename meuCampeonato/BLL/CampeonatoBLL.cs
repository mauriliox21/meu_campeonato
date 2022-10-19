using Biblioteca;
using Biblioteca.Util;
using DAL;
using System;
using System.Collections;
using System.Data;
using System.Linq;

namespace BLL
{
    public class CampeonatoBLL : BaseBLL
    {
        public CampeonatoBLL() : base() { }
        public CampeonatoBLL(ContextoDb contexto) : base(contexto) { }

        public SortedList Incluir(SortedList parametros)
        {
            SortedList resultado = new SortedList();

            try
            {
                CampeonatoDAL incluirDAL = new CampeonatoDAL(ContextoAtual);
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

        public SortedList Consultar(SortedList parametros)
        {
            SortedList resultado = new SortedList();

            try
            {
                CampeonatoDAL consultarDAL = new CampeonatoDAL(ContextoAtual);
                resultado = consultarDAL.Consultar(parametros);

            }
            catch (Exception erro)
            {
                resultado = FormatarResultadoErroSistema(erro);
            }

            return resultado;
        }

        public SortedList SimularCampeonato(SortedList parametros)
        {
            SortedList resultado = new SortedList();

            try
            {
                CampeonatoTimeBLL consultaCampTime = new CampeonatoTimeBLL();
                
                SortedList paramConsultaCampTime = new SortedList();
                paramConsultaCampTime["SQ_CAMPEONATO"] = parametros["SQ_CAMPEONATO"];
                if (consultaCampTime.CampeonatoTemTodosTimes(paramConsultaCampTime))
                {
                    //Inicia uma transacao que apenas esta classe pode reverter ou "commitar"
                    ContextoAtual.CriarTransacao();

                    bool simulacaoCompleta = false;
                    bool ocorreuErro = false;

                    paramConsultaCampTime["ST_ELIMINADO"] = "N";
                    while (!simulacaoCompleta && !ocorreuErro)
                    {
                        #region Busca todos os times do campeonato que ainda não estão eliminados
                        SortedList reultConsultaTimeClassificado = consultaCampTime.Consultar(paramConsultaCampTime);
                        if (!VerificarResultadoSucesso(reultConsultaTimeClassificado))
                        {
                            //caso ocorra erro não executa mais nenhum procedimento, e salva o resultado erro para devolver para interface
                            ocorreuErro = true;
                            resultado = reultConsultaTimeClassificado;
                        }
                        #endregion

                        #region Inclui uma nova fase no campeonato
                        SortedList resultInclusaoFase = new SortedList();
                        if (!ocorreuErro)
                        {
                            //verifica o nome da fase
                            DataTable timesClassificados = UtilSortedList.CapturarDataTable(reultConsultaTimeClassificado, "retorno");
                            string nomeFase = "";
                            if (timesClassificados.Rows.Count == 8)
                                nomeFase = "Quartas de Final";
                            else if (timesClassificados.Rows.Count == 4)
                                nomeFase = "Semi Finais";
                            else
                                nomeFase = "Final";

                            //inclusão da fase 
                            FaseBLL incluiFaseBLL = new FaseBLL(ContextoAtual);
                            parametros["NM_FASE"] = nomeFase;
                            resultInclusaoFase = incluiFaseBLL.Incluir(parametros);

                            if (!VerificarResultadoSucesso(resultInclusaoFase))
                            {
                                //caso ocorra erro não executa mais nenhum procedimento, e salva o resultado erro para devolver para interface
                                ocorreuErro = true;
                                resultado = resultInclusaoFase;
                            }
                        }
                        #endregion

                        
                        if (!ocorreuErro)
                        {
                            DataTable retInclusaoFase = UtilSortedList.CapturarDataTable(resultInclusaoFase, "retorno");
                            DataTable timesClassificados = UtilSortedList.CapturarDataTable(reultConsultaTimeClassificado, "retorno");

                            //gera uma sequência de numeros que não se repetem, cada numero equivale ao indice de um time, assim os times se enfrentaram de forma aleatoria
                            int[] timesSorteados = UtilJogo.SortearTimes(timesClassificados.Rows.Count, timesClassificados.Rows.Count - 1);

                            for (int i = 0; i < timesClassificados.Rows.Count && !ocorreuErro; i += 2)
                            {
                                #region Simulação do Jogo
                                //usa a sequência de numeros aleatorios para escolher os times 
                                DataRow time1 = timesClassificados.Rows[timesSorteados[i]];
                                DataRow time2 = timesClassificados.Rows[timesSorteados[i + 1]];

                                //usa uma sequencia de numeros aleatorios para gerar o placar do jogo
                                int[] placar = UtilJogo.SortearPlacares(8);
                                int placarTime1 = placar[0];
                                int placarTime2 = placar[1];

                                //verifica qual time foi o elimidado
                                string sqTimeEliminado;
                                if (placarTime1 > placarTime2)
                                {
                                    sqTimeEliminado = UtilDataTable.CapturarCampoString(time2, "SQ_TIME");
                                }
                                else if (placarTime2 > placarTime1)
                                {
                                    sqTimeEliminado = UtilDataTable.CapturarCampoString(time1, "SQ_TIME");
                                }
                                else //empate
                                {
                                    sqTimeEliminado = UtilJogo.CriterioDesempateTimeEliminado(time1, time2);
                                }
                                #endregion

                                #region Inclui o jogo na fase
                                SortedList paramInclusaoJogo = new SortedList();
                                paramInclusaoJogo.Add("SQ_FASE", UtilDataTable.CapturarCampoString(retInclusaoFase, "CD_CHAVE_REGISTRO"));
                                paramInclusaoJogo.Add("SQ_TIME_1", UtilDataTable.CapturarCampoString(time1, "SQ_TIME"));
                                paramInclusaoJogo.Add("SQ_TIME_2", UtilDataTable.CapturarCampoString(time2, "SQ_TIME"));
                                paramInclusaoJogo.Add("NR_PLACAR_TIME_1", placarTime1);
                                paramInclusaoJogo.Add("NR_PLACAR_TIME_2", placarTime2);

                                JogoBLL incluiJogoBLL = new JogoBLL(ContextoAtual);
                                SortedList resiltInclusaoJogo = incluiJogoBLL.Incluir(paramInclusaoJogo);

                                if (!VerificarResultadoSucesso(resiltInclusaoJogo))
                                {
                                    //caso ocorra erro não executa mais nenhum procedimento, e salva o resultado erro para devolver para interface
                                    ocorreuErro = true;
                                    resultado = resiltInclusaoJogo;
                                }
                                #endregion

                                #region Alterar os times que participaram da partida (ajustar a pontuacao e eliminar o time perdedor)
                                if (!ocorreuErro)
                                {
                                    //atualizar times
                                    CampeonatoTimeBLL alteraCampTime = new CampeonatoTimeBLL(ContextoAtual);

                                    int pontuacaoAtual = UtilDataTable.CapturarCampoInteiro(time1, "NR_PONTUACAO", 0);
                                    string elimindado = UtilDataTable.CapturarCampoString(time1, "SQ_TIME").Equals(sqTimeEliminado) ? "S" : "N";
                                    SortedList paramAlteracaoTime = new SortedList();
                                    paramAlteracaoTime.Add("SQ_CAMPEONATO_TIME", UtilDataTable.CapturarCampoString(time1, "SQ_CAMPEONATO_TIME"));
                                    paramAlteracaoTime.Add("NR_PONTUACAO", pontuacaoAtual + (placarTime1 - placarTime2));
                                    paramAlteracaoTime.Add("ST_ELIMINADO", elimindado);

                                    SortedList resultAlteracaoTime = alteraCampTime.Alterar(paramAlteracaoTime);

                                    if (!VerificarResultadoSucesso(resultAlteracaoTime))
                                    {
                                        //caso ocorra erro não executa mais nenhum procedimento, e salva o resultado erro para devolver para interface
                                        ocorreuErro = true;
                                        resultado = resultAlteracaoTime;
                                    }

                                    if (!ocorreuErro)
                                    {
                                        pontuacaoAtual = UtilDataTable.CapturarCampoInteiro(time2, "NR_PONTUACAO", 0);
                                        elimindado = UtilDataTable.CapturarCampoString(time2, "SQ_TIME").Equals(sqTimeEliminado) ? "S" : "N";
                                        paramAlteracaoTime["SQ_CAMPEONATO_TIME"] = UtilDataTable.CapturarCampoString(time2, "SQ_CAMPEONATO_TIME");
                                        paramAlteracaoTime["NR_PONTUACAO"] = pontuacaoAtual + (placarTime2 - placarTime1);
                                        paramAlteracaoTime["ST_ELIMINADO"] = elimindado;

                                        resultAlteracaoTime = alteraCampTime.Alterar(paramAlteracaoTime);

                                        if (!VerificarResultadoSucesso(resultAlteracaoTime))
                                        {
                                            //caso ocorra erro não executa mais nenhum procedimento, e salva o resultado erro para devolver para interface
                                            ocorreuErro = true;
                                            resultado = resultAlteracaoTime;
                                        }
                                    }
                                }
                                #endregion
                            }
                        }

                        if (!ocorreuErro)
                        {
                            DataTable timesClassificados = UtilSortedList.CapturarDataTable(reultConsultaTimeClassificado, "retorno");
                            simulacaoCompleta = timesClassificados.Rows.Count == 2;
                        }
                    }

                    //caso algum dos procedimentos falhe reverte todos o procedimentos que foram feitos
                    if (ocorreuErro)
                        SetRollback();
                    else
                        SetCommit();
                }
                else
                {
                    resultado = FormatarResultadoErro("Este campeonato não tem times suficientes para executar a simulação");
                }
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
