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

        public static SqlParameter FormatarParametro(SortedList fonteDados, string nome, bool alfanumerico)
        {
            string valor = fonteDados[nome].ToString();
            SqlParameter parametro;

            if (alfanumerico)
                parametro = new SqlParameter("@" + nome, SqlDbType.VarChar);
            else
                parametro = new SqlParameter(nome, SqlDbType.Int);

            if (string.IsNullOrEmpty(valor))
            {
                if (alfanumerico)
                    parametro.Value = "";
                else
                    parametro.Value = null;
            }
            else
            {
                parametro.Value = valor;
            }

            parametro.Direction = ParameterDirection.InputOutput;
            parametro.IsNullable = false;

            return parametro;

        }

        public static DataTable ExecutarQuery(string sql, ContextoDb contexto, SqlCommand comando)
        {
            DataTable tabela = new DataTable();

            comando.Transaction = (SqlTransaction)contexto.CriarTransacao().UnderlyingTransaction;

            using (SqlDataReader reader = comando.ExecuteReader())
            {
                tabela.Load(reader);
            }

            return tabela;
        }
    }
}
