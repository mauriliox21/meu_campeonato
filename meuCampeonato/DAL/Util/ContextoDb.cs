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

        public DbContextTransaction CriarTransacao()
        {
            //DbContextTransaction transacao;

            //if(TransacaoHerdada != null)
            //{
            //    transacao = TransacaoHerdada;
            //}
            //else 
            if (Transacao == null)
            {
                Transacao = Contexto.Database.BeginTransaction();
                //transacao = Transacao;
            }
            //else
            //{
            //    transacao = Transacao;
            //}

            return Transacao;
        }

        public void SetRollback()
        {
            if (Transacao != null && Transacao.UnderlyingTransaction.Connection != null)
                Transacao.Rollback();
        }

        public void SetCommit()
        {
            if (Transacao != null && Transacao.UnderlyingTransaction.Connection != null)
                Transacao.Commit();
        }
    }
}
