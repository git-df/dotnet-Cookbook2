using Domain.Requests;
using System.Linq.Expressions;
using System.Reflection;

namespace Application.Helpers
{
    public static class SortingHelper
    {

        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> source, SortingRequest sortingRequest) where T : class
        {
            if (!ValidSortingOptions<T>(sortingRequest))
                return source;

            var property = typeof(T).GetProperty(sortingRequest.OrderBy, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            var parameter = Expression.Parameter(typeof(T), "x");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);

            MethodCallExpression resultExp = Expression.Call(
                typeof(Queryable),
                sortingRequest.OrderByDescending ? "OrderByDescending" : "OrderBy",
                new Type[] { typeof(T), property.PropertyType },
                source.Expression,
                Expression.Quote(orderByExp)
            );

            return source.Provider.CreateQuery<T>(resultExp);
        }

        private static bool ValidSortingOptions<T>(SortingRequest sortingRequest) where T : class
            => typeof(T).GetProperty(sortingRequest.OrderBy, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase) != null;
    }
}
