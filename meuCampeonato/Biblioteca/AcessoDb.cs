using Biblioteca;
using Biblioteca.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
    public class AcessoDb
    {
        private static string CapturarStringConexao()
        {
            try
            {
                return ConfigurationManager.ConnectionStrings["conexaoSQLServer"].ConnectionString;
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public static DbContext CapturarContextoDb()
        {
            try
            {
                DbContext contex = new DbContext(CapturarStringConexao());
                contex.Database.Connection.Open();
                return contex;
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        /// <summary>
        /// Cria e configura um objeto SqlParameter 
        /// </summary>
        public static SqlParameter FormatarParametro(SortedList fonteDados, string nome, bool permiteNulo)
        {
            string valor = UtilSortedList.CapturarString(fonteDados, nome);
            SqlParameter parametro;

            if (string.IsNullOrEmpty(valor))
            {
                if (permiteNulo)
                    parametro = new SqlParameter(nome, null);
                else
                    parametro = new SqlParameter(nome, "");
            }
            else
            {
                parametro = new SqlParameter(nome, valor);
            }

            return parametro;

        }

        /// <summary>
        /// Executa queries que alterem a base de dados ex: incluir, alterar, excluir
        /// </summary>
        public static DataTable ExecutarQueryManutencao(ContextoDb contexto, SqlCommand comando)
        {
            DataTable tabela = new DataTable();

            //Para minimizar o risco de "falsos positivos" verifica se a query é compativél com o método
            if (!comando.CommandText.EndsWith("INCLUIR") && !comando.CommandText.EndsWith("ALTERAR"))
            {
                string mensagemErro = "Não é recomendavel executar uma query que não seja de \"Manutenção\" através do método AcessoDb.ExecutarQueryManutencao(). "
                                    + "Tentativiva de execução: " + comando.CommandText;
                throw new Exception(mensagemErro);

            }

            //Inclui os parametros na chamada da execução da procedure ex: STP_CAMPEONATO_INCLUIR @NM_CAMPEONATO = N'1° Campeonato'
            IncluirParametrosQuery(comando);

            comando.Transaction = (SqlTransaction)contexto.CapturarTransacao().UnderlyingTransaction;

            using (SqlDataReader reader = comando.ExecuteReader())
            {
                tabela.Load(reader);
            }

            return tabela;
        }

        /// <summary>
        /// Executa queries que não alterem a base de dados ex: consultar
        /// </summary>
        public static DataTable ExecutarQuery(SqlCommand comando)
        {
            DataTable tabela = new DataTable();

            //Para minimizar o risco de "falsos positivos" verifica se a query é compativél com o método
            if (!comando.CommandText.EndsWith("CONSULTAR") && !comando.CommandText.EndsWith("DETALHAR"))
            {
                string mensagemErro = "Não é possivel executar uma query que não seja de \"Consulta\" através do método AcessoDb.ExecutarQuery(). "
                                    + "Tentativiva de execução: " + comando.CommandText;
                throw new Exception(mensagemErro);
            }

            //Inclui os parametros na chamada da execução da procedure ex: STP_CAMPEONATO_INCLUIR @NM_CAMPEONATO = N'1° Campeonato'
            IncluirParametrosQuery(comando);

            using (SqlDataReader reader = comando.ExecuteReader())
            {
                tabela.Load(reader);
            }

            return tabela;
        }

        /// <summary>
        /// Inclui os parametros da Collection "Parameters" na propriedade "CommandText" do SqlCommand
        /// Ex: "CommandText" STP_CAMPEONATO_INCLUIR vai se tornar STP_CAMPEONATO_INCLUIR @NM_CAMPEONATO = N'1° Campeonato'
        /// </summary>
        public static void IncluirParametrosQuery(SqlCommand comando)
        {
            int index = 0;
            foreach (SqlParameter parametro in comando.Parameters)
            {
                if (index != 0)
                    comando.CommandText += " , ";

                if (parametro.Value != null)
                    comando.CommandText += $" @{parametro.ParameterName} = N'{parametro.Value}'";
                else
                    comando.CommandText += $" @{parametro.ParameterName} = null";

                index++;
            }
        }
        
    }
}
