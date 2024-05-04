namespace CodeFirst.Extensions
{
    using System;
    using System.Collections;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    public static class QueryableExtensions
    {
        public static IQueryable<TEntity> IncludeMultiple<TEntity>(this IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }

        public static IQueryable<TEntity> TryFilterBy<TEntity, TData>(this IQueryable<TEntity> query, TData? filter, Expression<Func<TEntity, bool>> expression)
            where TData : struct
        {
            return query.TryFilterBy(filter.HasValue, expression);
        }

        public static IQueryable<TEntity> TryFilterBy<TEntity>(this IQueryable<TEntity> query, string filter, Expression<Func<TEntity, bool>> expression)
        {
            return query.TryFilterBy(!string.IsNullOrWhiteSpace(filter), expression);
        }

        public static IQueryable<TEntity> TryFilterBy<TEntity, TData>(this IQueryable<TEntity> query, TData filter, Expression<Func<TEntity, bool>> expression)
            where TData : IList
        {
            return query.TryFilterBy(filter?.Count > 0, expression);
        }

        public static IQueryable<TEntity> TryFilterBy<TEntity>(this IQueryable<TEntity> query, bool condition, Expression<Func<TEntity, bool>> expression)
        {
            return condition ? query.Where(expression) : query;
        }

        public static IQueryable<TEntity> ContainsEnum<TEntity, TEnum>(this IQueryable<TEntity> query, Expression<Func<TEntity, TEnum>> enumProp, TEnum[] defaultFilteringValues)
            where TEnum : struct
        {
            var biExpr = defaultFilteringValues.Select(enumVal => Expression.Equal(enumProp.Body, Expression.Constant(enumVal))).Aggregate((argBExp, bExp) => Expression.OrElse(argBExp, bExp));
            var expr = Expression.Lambda<Func<TEntity, bool>>(biExpr, enumProp.Parameters.Single());
            return query.Where(expr);
        }

        public static IQueryable<TEntity> EqualsEnum<TEntity, TEnum>(this IQueryable<TEntity> query, Expression<Func<TEntity, TEnum>> enumProp, string filter)
            where TEnum : struct
        {
            TEnum enumFilter;
            if (Enum.TryParse(filter, out enumFilter))
            {
                Expression eq = Expression.Equal(enumProp.Body, Expression.Constant(enumFilter));
                var expr = Expression.Lambda<Func<TEntity, bool>>(eq, enumProp.Parameters.Single());
                return query.Where(expr);
            }

            return query;
        }
    }
}
