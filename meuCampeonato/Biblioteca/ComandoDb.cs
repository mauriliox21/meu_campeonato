using System.Data.SqlClient;

namespace Biblioteca
{
    //Classe criada para abstração, para que as classes DAL não conhenção o mecanismo de conexão com o banco de dados
    public class ComandoDb
    {
        public SqlCommand Comando { get; }

        public ComandoDb(string sql, ContextoDb contexto)
        {
            Comando = new SqlCommand(sql, (SqlConnection)contexto.Contexto.Database.Connection);
        }

        /// <summary>
        /// Inclui um objeto SqlParameter na Collection Parameters da propriedade "Comando" desta instância classe
        /// </summary>
        public void IncluirParametro(SqlParameter parametro)
        {
            Comando.Parameters.Add(parametro);
        }

    }
}
