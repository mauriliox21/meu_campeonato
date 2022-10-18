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
                paramConsultaCampTime.Add("SQ_CAMPEONATO", parametros["SQ_CAMPEONATO"]);
                if (consultaCampTime.CampeonatoTemTodosTimes(paramConsultaCampTime))
                {
                    //Inicia uma transacao que apenas esta classe pode reverter ou "commitar"
                    ContextoAtual.CriarTransacao();

                    bool simulacaoCompleta = false;
                    bool ocorreuErro = false;

                    while (!simulacaoCompleta && !ocorreuErro)
                    {

                    }
                    paramConsultaCampTime.Add("ST_ELIMINADO", "N");
                    SortedList reultConsultaTimeClassificado = consultaCampTime.Consultar(paramConsultaCampTime);

                    if (VerificarResultadoSucesso(reultConsultaTimeClassificado))
                    {
                        DataTable timesClassificados = UtilSortedList.CapturarDataTable(reultConsultaTimeClassificado, "retorno");
                        int[] timesSorteados = UtilJogo.SortearTimes(8);

                        //inclusão da fase 
                        FaseBLL incluiFaseBLL = new FaseBLL(ContextoAtual);
                        parametros.Add("NM_FASE", "Quartas de Final");
                        SortedList resultInclusaoFase = incluiFaseBLL.Incluir(parametros);

                        if (VerificarResultadoSucesso(resultInclusaoFase))
                        {
                            DataTable retInclusaoFase = UtilSortedList.CapturarDataTable(resultInclusaoFase, "retorno");

                            for (int i = 0; i < timesClassificados.Rows.Count; i =+2)
                            {
                                DataRow time1 = timesClassificados.Rows[timesSorteados[i]];
                                DataRow time2 = timesClassificados.Rows[timesSorteados[i + 1]];

                                int[] placar = UtilJogo.SortearPlacares(8);
                                int placarTime1 = placar[0];
                                int placarTime2 = placar[1];

                                //verificar qual time foi o elimidado
                                string sqTimeEliminado;
                                if(placarTime1 > placarTime2)
                                {
                                    sqTimeEliminado = UtilDataTable.CapturarCampoString(time2, "SQ_TIME");
                                }
                                else if(placarTime2 > placarTime1)
                                {
                                    sqTimeEliminado = UtilDataTable.CapturarCampoString(time1, "SQ_TIME");
                                }
                                else //empate
                                {
                                    sqTimeEliminado = UtilJogo.CriterioDesempateTimeEliminado(time1, time2);
                                }

                                //inclusão jogo
                                SortedList paramInclusaoJogo = new SortedList();
                                paramInclusaoJogo.Add("SQ_FASE", UtilDataTable.CapturarCampoString(retInclusaoFase, "CD_CHAVE_REGISTRO"));
                                paramInclusaoJogo.Add("SQ_TIME_1", UtilDataTable.CapturarCampoString(time1, "SQ_TIME"));
                                paramInclusaoJogo.Add("SQ_TIME_2", UtilDataTable.CapturarCampoString(time2, "SQ_TIME"));
                                paramInclusaoJogo.Add("SQ_TIME_2", placarTime1);
                                paramInclusaoJogo.Add("SQ_TIME_2", placarTime2);

                                JogoBLL incluiJogoBLL = new JogoBLL(ContextoAtual);
                                SortedList resiltInclusaoJogo = incluiJogoBLL.Incluir(paramInclusaoJogo);

                                if (VerificarResultadoSucesso(paramInclusaoJogo))
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

                                    if (VerificarResultadoSucesso(resultAlteracaoTime))
                                    {
                                        pontuacaoAtual = UtilDataTable.CapturarCampoInteiro(time2, "NR_PONTUACAO", 0);
                                        elimindado = UtilDataTable.CapturarCampoString(time1, "SQ_TIME").Equals(sqTimeEliminado) ? "S" : "N";
                                        paramAlteracaoTime["SQ_CAMPEONATO_TIME"] = UtilDataTable.CapturarCampoString(time1, "SQ_CAMPEONATO_TIME");
                                        paramAlteracaoTime["NR_PONTUACAO"] = pontuacaoAtual + (placarTime2 - placarTime1);
                                        paramAlteracaoTime["ST_ELIMINADO"] = elimindado;

                                        resultAlteracaoTime = alteraCampTime.Alterar(paramAlteracaoTime);

                                        if (VerificarResultadoSucesso(resultAlteracaoTime))
                                        {
                                            
                                        }
                                    }

                                }


                            }

                        }

                    }
                }
            }
            catch (Exception erro)
            {
                resultado = FormatarResultadoErroSistema(erro);
            }

            return resultado;
        }
    }
}
