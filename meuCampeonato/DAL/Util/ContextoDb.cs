using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Util
{
    public class ContextoDb
    {
        public DbContext Contexto;
        public DbContextTransaction Transacao;

        public ContextoDb()
        {
            Contexto = AcessoDb.CapturarContextoDb();
        }

        public DbContextTransaction CriarTransacao()
        {
            if (Transacao == null)
                Transacao = Contexto.Database.BeginTransaction();

            return Transacao;
        }
    }
}
