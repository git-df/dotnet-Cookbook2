using Domain.Enums;
using Domain.Requests;
using System.Linq.Expressions;
using System.Reflection;

namespace Application.Helpers
{
    public static class FilteringHelper
    {
        private static readonly List<FilterType> OneValueFilterTypes =
        [
            FilterType.EqualValue,
            FilterType.NotEqualValue,
            FilterType.ContainsValue
        ];

        private static readonly List<FilterType> ManyValuesFilterTypes =
        [
            FilterType.ManyEqualValues,
            FilterType.ManyNotEqualValues,
            FilterType.ManyContainsValue
        ];

        public static IQueryable<T> ApplyFiltering<T>(this IQueryable<T> source, List<FIlteringRequest> filterRequest)
        {
            if (source is null || filterRequest is null || filterRequest.Count < 1)
                return source;

            Expression finalExpression = null;
            var parameter = Expression.Parameter(typeof(T), "x");

            foreach (var filter in filterRequest)
            {
                var property = typeof(T).GetProperty(filter.PropertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (property == null)
                    continue;

                //One value filters
                if (OneValueFilterTypes.Contains(filter.Type) && filter.Value is not null)
                {
                    var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                    var constant = Expression.Constant(filter.Value, property.PropertyType);

                    switch (filter.Type)
                    {
                        //EqualValue
                        case FilterType.EqualValue:
                            var equalsExpression = Expression.Equal(propertyAccess, Expression.Convert(constant, property.PropertyType));
                            finalExpression = finalExpression == null ? equalsExpression : Expression.AndAlso(finalExpression, equalsExpression);
                            break;

                        //NotEqualValue
                        case FilterType.NotEqualValue:
                            var notEqualsExpression = Expression.NotEqual(propertyAccess, Expression.Convert(constant, property.PropertyType));
                            finalExpression = finalExpression == null ? notEqualsExpression : Expression.AndAlso(finalExpression, notEqualsExpression);
                            break;

                        //ContainsValue
                        case FilterType.ContainsValue:
                            if (filter.Value is string && property.PropertyType == typeof(string))
                            {
                                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                                var containsExpression = Expression.Call(propertyAccess, containsMethod, constant);
                                finalExpression = finalExpression == null ? containsExpression : Expression.AndAlso(finalExpression, containsExpression);
                            }
                            break;
                        default:
                            continue;
                    }
                }

                //Many values filters
                if (ManyValuesFilterTypes.Contains(filter.Type) && filter.Values is not null && filter.Values.Count > 0)
                {
                    Expression orExpression = null;
                    var propertyAccess = Expression.MakeMemberAccess(parameter, property);

                    switch (filter.Type)
                    {
                        //ManyEqualValues
                        case FilterType.ManyEqualValues:
                            foreach (var value in filter.Values)
                            {
                                var constant = Expression.Constant(value, property.PropertyType);
                                var equals = Expression.Equal(propertyAccess, Expression.Convert(constant, property.PropertyType));
                                orExpression = orExpression == null ? equals : Expression.OrElse(orExpression, equals);
                            }
                            finalExpression = finalExpression == null ? orExpression : Expression.AndAlso(finalExpression, orExpression);
                            break;

                        //ManyNotEqualValues
                        case FilterType.ManyNotEqualValues:
                            foreach (var value in filter.Values)
                            {
                                var constant = Expression.Constant(value, property.PropertyType);
                                var notEquals = Expression.NotEqual(propertyAccess, Expression.Convert(constant, property.PropertyType));
                                orExpression = orExpression == null ? notEquals : Expression.OrElse(orExpression, notEquals);
                            }
                            finalExpression = finalExpression == null ? orExpression : Expression.AndAlso(finalExpression, orExpression);
                            break;

                        //ManyContainsValue
                        case FilterType.ManyContainsValue:
                            foreach (var value in filter.Values)
                            {
                                if (value is string && property.PropertyType == typeof(string))
                                {
                                    var constant = Expression.Constant(value);
                                    var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                                    var contains = Expression.Call(propertyAccess, containsMethod, constant);
                                    orExpression = orExpression == null ? contains : Expression.OrElse(orExpression, contains);
                                }
                            }
                            finalExpression = finalExpression == null ? orExpression : Expression.AndAlso(finalExpression, orExpression);
                            break;
                        default:
                            continue;
                    }
                }

                //ValueBetween
                if (filter.Type == FilterType.ValueBetween && filter.MinValue is not null && filter.MaxValue is not null)
                {

                    Expression rangeExpression = null;
                    if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                    {
                        rangeExpression = BuildDateRangeExpression(property, filter, parameter);
                    }
                    else if (IsNumericType(property.PropertyType))
                    {
                        rangeExpression = BuildNumericRangeExpression(property, filter, parameter);
                    }

                    if (rangeExpression != null)
                    {
                        finalExpression = finalExpression == null ? rangeExpression : Expression.AndAlso(finalExpression, rangeExpression);
                    }

                    continue;
                }
            }

            if (finalExpression is null)
                return source;

            var lambda = Expression.Lambda<Func<T, bool>>(finalExpression, parameter);
            return source.Where(lambda);
        }
        private static Expression BuildDateRangeExpression(PropertyInfo property, FIlteringRequest filter, ParameterExpression parameter)
        {
            var minValue = filter.MinValue != null ? (DateTime?)Convert.ChangeType(filter.MinValue, typeof(DateTime)) : null;
            var maxValue = filter.MaxValue != null ? (DateTime?)Convert.ChangeType(filter.MaxValue, typeof(DateTime)) : null;

            var propertyAccess = Expression.MakeMemberAccess(parameter, property);

            Expression dateRangeExpression = null;
            if (minValue.HasValue)
            {
                var lowerBoundValue = Expression.Constant(minValue.Value, typeof(DateTime));
                dateRangeExpression = Expression.GreaterThanOrEqual(propertyAccess, lowerBoundValue);
            }
            if (maxValue.HasValue)
            {
                var upperBoundValue = Expression.Constant(maxValue.Value, typeof(DateTime));
                var upperExpr = Expression.LessThanOrEqual(propertyAccess, upperBoundValue);
                dateRangeExpression = dateRangeExpression == null ? upperExpr : Expression.AndAlso(dateRangeExpression, upperExpr);
            }

            return dateRangeExpression;
        }

        private static Expression BuildNumericRangeExpression(PropertyInfo property, FIlteringRequest filter, ParameterExpression parameter)
        {
            var minValue = filter.MinValue != null ? Convert.ChangeType(filter.MinValue, property.PropertyType) : null;
            var maxValue = filter.MaxValue != null ? Convert.ChangeType(filter.MaxValue, property.PropertyType) : null;

            var propertyAccess = Expression.MakeMemberAccess(parameter, property);

            Expression numericRangeExpression = null;
            if (minValue != null)
            {
                var lowerBoundValue = Expression.Constant(minValue, property.PropertyType);
                numericRangeExpression = Expression.GreaterThanOrEqual(propertyAccess, lowerBoundValue);
            }
            if (maxValue != null)
            {
                var upperBoundValue = Expression.Constant(maxValue, property.PropertyType);
                var upperExpr = Expression.LessThanOrEqual(propertyAccess, upperBoundValue);
                numericRangeExpression = numericRangeExpression == null ? upperExpr : Expression.AndAlso(numericRangeExpression, upperExpr);
            }

            return numericRangeExpression;
        }

        private static bool IsNumericType(Type type)
        {
            return Type.GetTypeCode(type) switch
            {
                TypeCode.Byte or TypeCode.SByte or TypeCode.UInt16 or TypeCode.UInt32 or TypeCode.UInt64 or
                TypeCode.Int16 or TypeCode.Int32 or TypeCode.Int64 or TypeCode.Decimal or TypeCode.Double or TypeCode.Single => true,
                _ => false,
            };
        }
    }
}
