using MongoDB.Driver.Linq;
using System.Linq.Expressions;
namespace WebAPIProject.Models
{
    public static class IMongoQueryableExtensions
    {
        public static IMongoQueryable<TEntity> OrderByCustom<TEntity>(this IMongoQueryable<TEntity> items, string sortBy, string sortOrder)
        {
            var type = typeof(TEntity);
            var expression2 = Expression.Parameter(type, "t");
            var property = type.GetProperty(sortBy);
            var expression1 = Expression.MakeMemberAccess(expression2, property);
            var lamda = Expression.Lambda(expression1,expression2);
            var result = Expression.Call(
                typeof(Queryable),
                sortOrder == "desc" ? "OrderByDescending" : "OrderBy",
                new Type[] { type, property.PropertyType},
                items.Expression,
                Expression.Quote(lamda)
                );
            return (IMongoQueryable<TEntity>)items.Provider.CreateQuery<TEntity>(result); 
        }
    }
}
