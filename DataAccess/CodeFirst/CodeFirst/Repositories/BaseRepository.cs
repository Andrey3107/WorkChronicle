namespace CodeFirst.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Extensions;

    using Interfaces;

    using Models;

    public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        public BaseRepository(ApplicationDbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        protected ApplicationDbContext Context { get; }

        protected DbSet<TEntity> DbSet { get; }

        public virtual TEntity Get(TKey id)
        {
            return DbSet.Find(id);
        }

        public IQueryable<TEntity> GetAsQueryable(bool noTracking = true)
        {
            return noTracking ? DbSet.AsNoTracking().AsQueryable() : DbSet.AsQueryable();
        }

        public IEnumerable<TEntity> Get()
        {
            return DbSet.ToList();
        }

        public IEnumerable<TEntity> Get(params Expression<Func<TEntity, object>>[] included)
        {
            return DbSet.IncludeMultiple(included).ToList();
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> expression)
        {
            return DbSet.Where(expression).ToList();
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] included)
        {
            return DbSet.IncludeMultiple(included).Where(expression).ToList();
        }

        public IEnumerable<TEntity> GetAsNoTracking()
        {
            return DbSet.AsNoTracking().ToList();
        }

        public IEnumerable<TEntity> GetAsNoTracking(params Expression<Func<TEntity, object>>[] included)
        {
            return DbSet.IncludeMultiple(included).AsNoTracking().ToList();
        }

        public IEnumerable<TEntity> GetAsNoTracking(Expression<Func<TEntity, bool>> expression)
        {
            return DbSet.Where(expression).AsNoTracking().ToList();
        }

        public IEnumerable<TEntity> GetAsNoTracking(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] included)
        {
            return DbSet.IncludeMultiple(included).Where(expression).AsNoTracking().ToList();
        }

        public bool Any(Expression<Func<TEntity, bool>> expression)
        {
            return DbSet.Where(expression).Any();
        }

        public bool Any(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] included)
        {
            return DbSet.IncludeMultiple(included).Any(expression);
        }

        public int Count(Expression<Func<TEntity, bool>> expression)
        {
            return DbSet.Count(expression);
        }

        public int Count(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] included)
        {
            return DbSet.IncludeMultiple(included).Count(expression);
        }

        public virtual TEntity Add(TEntity entity)
        {
            return DbSet.Add(entity);
        }

        public virtual IEnumerable<TEntity> Add(IEnumerable<TEntity> entities)
        {
            return DbSet.AddRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }

            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Update(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (Context.Entry(entity).State == EntityState.Detached)
                {
                    DbSet.Attach(entity);
                }

                Context.Entry(entity).State = EntityState.Modified;
            }
        }

        public virtual TEntity AddOrUpdate(TEntity entity)
        {
            if (EqualityComparer<TKey>.Default.Equals(entity.Id, default(TKey)))
            {
                entity = Add(entity);
            }
            else
            {
                Update(entity);
            }

            return entity;
        }

        public virtual void AddOrUpdate(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                AddOrUpdate(entity);
            }
        }

        public virtual void Delete(TKey id)
        {
            var entityToDelete = Get(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }

            DbSet.Remove(entity);
        }

        public virtual void Delete(IEnumerable<TKey> ids)
        {
            var entities = new List<TEntity>();

            foreach (var id in ids)
            {
                var entity = Get(id);
                entities.Add(entity);
            }

            Delete(entities);
        }

        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            entities = entities.ToList();

            foreach (var entity in entities)
            {
                if (Context.Entry(entity).State == EntityState.Detached)
                {
                    DbSet.Attach(entity);
                }
            }

            DbSet.RemoveRange(entities);
        }

        public void Delete(Expression<Func<TEntity, bool>> expression)
        {
            Delete(GetWithTracking(expression));
        }

        public virtual void Detach(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Detached;
        }

        public virtual void Detach(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Detach(entity);
            }
        }

        #region Async

        public virtual async Task<TEntity> GetAsync(TKey id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(params Expression<Func<TEntity, object>>[] included)
        {
            return await DbSet.IncludeMultiple(included).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await DbSet.Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] included)
        {
            return await DbSet.IncludeMultiple(included).Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAsNoTrackingAsync()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAsNoTrackingAsync(params Expression<Func<TEntity, object>>[] included)
        {
            return await DbSet.IncludeMultiple(included).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAsNoTrackingAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await DbSet.Where(expression).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAsNoTrackingAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] included)
        {
            return await DbSet.IncludeMultiple(included).Where(expression).AsNoTracking().ToListAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await DbSet.Where(expression).AnyAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] included)
        {
            return await DbSet.IncludeMultiple(included).AnyAsync(expression);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await DbSet.CountAsync(expression);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] included)
        {
            return await DbSet.IncludeMultiple(included).CountAsync(expression);
        }

        #endregion

        private IEnumerable<TEntity> GetWithTracking(Expression<Func<TEntity, bool>> expression)
        {
            return DbSet.Where(expression).ToList();
        }
    }
}
