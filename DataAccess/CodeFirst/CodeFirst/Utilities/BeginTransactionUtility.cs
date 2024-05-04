namespace CodeFirst.Utilities
{
    using System.Data.Entity;

    using CodeFirst.Interfaces;

    using Enums;

    using Interfaces;

    public class BeginTransactionUtility : IBeginTransactionUtility
    {
        public ITransaction BeginTransaction(DbContext context, IsolationLevel isolationLevel)
        {
            return new CustomDbContextTransaction(context, (System.Data.IsolationLevel)isolationLevel);
        }
    }
}
