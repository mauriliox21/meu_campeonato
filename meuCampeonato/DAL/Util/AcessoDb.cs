using Biblioteca;
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

namespace DAL.Util
{
    internal class AcessoDb
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

        public static DataTable ExecutarQuery(ContextoDb contexto, SqlCommand comando)
        {
            DataTable tabela = new DataTable();

            //Inclui os parametros na chamada da execução da procedure ex: STP_CAMPEONATO_INCLUIR @NM_CAMPEONATO
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

            comando.Transaction = (SqlTransaction)contexto.CriarTransacao().UnderlyingTransaction;

            using (SqlDataReader reader = comando.ExecuteReader())
            {
                tabela.Load(reader);
            }

            return tabela;
        }
    }
}
