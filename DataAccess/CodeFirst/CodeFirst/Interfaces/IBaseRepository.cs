namespace CodeFirst.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Models;

    public interface IBaseRepository<TEntity, in TKey> : IRepository
        where TEntity : IEntity<TKey>
    {
        TEntity Get(TKey id);

        IQueryable<TEntity> GetAsQueryable(bool noTracking = true);

        IEnumerable<TEntity> Get();

        IEnumerable<TEntity> Get(params Expression<Func<TEntity, object>>[] included);

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> expression);

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] included);

        IEnumerable<TEntity> GetAsNoTracking();

        IEnumerable<TEntity> GetAsNoTracking(params Expression<Func<TEntity, object>>[] included);

        IEnumerable<TEntity> GetAsNoTracking(Expression<Func<TEntity, bool>> expression);

        IEnumerable<TEntity> GetAsNoTracking(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] included);

        bool Any(Expression<Func<TEntity, bool>> expression);

        bool Any(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] included);

        int Count(Expression<Func<TEntity, bool>> expression);

        int Count(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] included);

        TEntity Add(TEntity entity);

        IEnumerable<TEntity> Add(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Update(IEnumerable<TEntity> entities);

        TEntity AddOrUpdate(TEntity entity);

        void AddOrUpdate(IEnumerable<TEntity> entities);

        void Delete(TKey id);

        void Delete(TEntity entity);

        void Delete(IEnumerable<TKey> ids);

        void Delete(IEnumerable<TEntity> entities);

        void Delete(Expression<Func<TEntity, bool>> expression);

        void Detach(IEnumerable<TEntity> entities);

        void Detach(TEntity entity);

        #region Async

        Task<TEntity> GetAsync(TKey id);

        Task<IEnumerable<TEntity>> GetAsync();

        Task<IEnumerable<TEntity>> GetAsync(params Expression<Func<TEntity, object>>[] included);

        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression);

        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] included);

        Task<IEnumerable<TEntity>> GetAsNoTrackingAsync();

        Task<IEnumerable<TEntity>> GetAsNoTrackingAsync(params Expression<Func<TEntity, object>>[] included);

        Task<IEnumerable<TEntity>> GetAsNoTrackingAsync(Expression<Func<TEntity, bool>> expression);

        Task<IEnumerable<TEntity>> GetAsNoTrackingAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] included);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] included);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> expression);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] included);

        #endregion
    }
}
