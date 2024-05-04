namespace CodeFirst.Interfaces
{
    using System;
    using System.Data;

    public interface ITransaction : IDisposable
    {
        IDbTransaction DbTransaction { get; }

        void Commit();

        void Rollback();
    }
}
