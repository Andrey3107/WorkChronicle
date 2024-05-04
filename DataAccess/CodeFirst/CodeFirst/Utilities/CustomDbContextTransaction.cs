namespace CodeFirst.Utilities
{
    using System;
    using System.Data;
    using System.Data.Entity;

    using CodeFirst.Interfaces;

    public class CustomDbContextTransaction : ITransaction
    {
        private bool isDisposed;

        private readonly DbContextTransaction dbContextTransaction;

        public CustomDbContextTransaction(DbContext context, IsolationLevel isolatedLevel)
        {
            dbContextTransaction = context.Database.BeginTransaction(isolatedLevel);
            DbTransaction = dbContextTransaction.UnderlyingTransaction;
            isDisposed = false;
        }

        public CustomDbContextTransaction(DbContextTransaction transaction)
        {
            dbContextTransaction = transaction;
            DbTransaction = transaction.UnderlyingTransaction;
            isDisposed = false;
        }

        public IDbTransaction DbTransaction { get; }

        public void Commit()
        {
            dbContextTransaction.Commit();
        }

        public void Rollback()
        {
            dbContextTransaction.Rollback();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    dbContextTransaction.Dispose();
                }
            }

            isDisposed = true;
        }
    }
}
