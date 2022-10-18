using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
    public class ContextoDb
    {
        public DbContext Contexto;
        public DbContextTransaction Transacao;
        private DbContextTransaction TransacaoHerdada;

        public ContextoDb()
        {
            Contexto = AcessoDb.CapturarContextoDb();
        }
        public ContextoDb(ContextoDb contextoDb)
        {
            Contexto = contextoDb.Contexto;
            TransacaoHerdada = contextoDb.Transacao;
        }

        public DbContextTransaction CapturarTransacao()
        {
            DbContextTransaction transacao;

            //caso herde a transacao existente usa ela, para que uma transaçao só possa ser revertida ou "commitada" pela classe que a criou
            if (TransacaoHerdada != null)
            {
                transacao = TransacaoHerdada;
            }
            else 
            {
                if (Transacao == null)
                    Transacao = Contexto.Database.BeginTransaction();

                transacao = Transacao;
            }

            return transacao;
        }

        public void CriarTransacao()
        {
            Transacao = Contexto.Database.BeginTransaction();
        }

        public void SetRollback()
        {
            //if (Transacao != null && Transacao.UnderlyingTransaction.Connection != null)
            if (Transacao != null)
                    Transacao.Rollback();

            Transacao = null;

        }

        public void SetCommit()
        {
            //if (Transacao != null && Transacao.UnderlyingTransaction.Connection != null)
            if (Transacao != null)
                Transacao.Commit();

            Transacao = null;
        }
    }
}
