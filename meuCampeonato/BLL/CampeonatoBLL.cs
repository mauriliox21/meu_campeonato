﻿using DAL;
using DAL.Util;
using System;
using System.Collections;

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
                CampeonatoDAL incluirDAL = new CampeonatoDAL();
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
                CampeonatoDAL consultarDAL = new CampeonatoDAL();
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
