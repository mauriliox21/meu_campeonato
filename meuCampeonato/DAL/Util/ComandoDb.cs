using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Util
{
    //Classe criada para abstração, para que as classes DAL não conhenção o mecanismo de conexão com o banco de dados
    public class ComandoDb
    {
        public SqlCommand Comando { get; }

        public ComandoDb(string sql, ContextoDb contexto)
        {
            Comando = new SqlCommand(sql, (SqlConnection)contexto.Contexto.Database.Connection);
        }

        public void IncluirParametro(SqlParameter parametro)
        {
            Comando.Parameters.Add(parametro);
        }

    }
}
