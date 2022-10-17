using DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CampeonatoBLL : BaseBLL
    {
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
