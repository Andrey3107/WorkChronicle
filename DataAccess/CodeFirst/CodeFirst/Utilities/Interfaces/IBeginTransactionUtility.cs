namespace CodeFirst.Utilities.Interfaces
{
    using System.Data.Entity;

    using CodeFirst.Interfaces;

    using Enums;

    public interface IBeginTransactionUtility
    {
        ITransaction BeginTransaction(DbContext context, IsolationLevel isolationLevel);
    }
}
